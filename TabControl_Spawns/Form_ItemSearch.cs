using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MySql.Data.MySqlClient;

namespace TabControl_Spawns
{
    public partial class Form_ItemSearch : Form
    {
        struct item
        {
            public string id;
            public string name;
        };

        private ArrayList list;
        private MySqlEngine db;
        public string ReturnId { get; set; }
        public Form_ItemSearch(MySqlEngine db)
        {
            InitializeComponent();
            list = new ArrayList();
            this.db = db;
        }

        private void button_Item_Search_Click(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void LoadItems()
        {
            listView_Items_Search_Results.Items.Clear();
            list.Clear();

            MySqlDataReader reader = db.RunSelectQuery("SELECT i.id, i.name FROM items i " + 
                                                       "WHERE i.name LIKE '%" + textBoxItemSearch.Text + "%' LIMIT 250");

            if (reader != null)
            {
                while (reader.Read())
                {
                    item i;
                    i.id = reader.GetString(0);
                    i.name = reader.GetString(1);

                    list.Add(i);
                }
                reader.Close();
            }

            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem item = new ListViewItem(((item)list[i]).id);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((item)list[i]).name));
                listView_Items_Search_Results.Items.Add(item);
                count++;
            }
            label_display.Text = "Displaying " + count + " items.";
        }

        private void listView_Items_Search_Results_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int intselectedindex = listView_Items_Search_Results.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
              this.ReturnId = listView_Items_Search_Results.Items[intselectedindex].SubItems[0].Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
