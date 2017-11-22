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
    public partial class Form_LootTableSearch : Form
    {
        struct loottable
        {
            public string id;
            public string name;
            public string using_;
        };

        private ArrayList list;
        private MySqlEngine db;
        public string ReturnId { get; set; }
        public Form_LootTableSearch(MySqlEngine db)
        {
            InitializeComponent();
            list = new ArrayList();
            this.db = db;
        }

        private void button_LootTable_Search_Click(object sender, EventArgs e)
        {
            LoadLootTables();
        }

        private void LoadLootTables()
        {
            listView_LootTable_Search_Results.Items.Clear();
            list.Clear();

            MySqlDataReader reader = db.RunSelectQuery("SELECT l.id, l.name, COUNT(s.spawn_id) as using_ FROM loottable l " +
                                                        "LEFT OUTER JOIN " +
                                                        "spawn_loot s ON l.id = s.loottable_id " +
                                                        "WHERE l.name LIKE '%" +  textBoxLootTableSearch.Text + "%' GROUP BY id LIMIT 250");

            if (reader != null)
            {
                while (reader.Read())
                {
                    loottable lt;
                    lt.id = reader.GetString(0);
                    lt.name = reader.GetString(1);
                    lt.using_ = reader.GetString(2);

                    list.Add(lt);
                }
                reader.Close();
            }

            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem item = new ListViewItem(((loottable)list[i]).id);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((loottable)list[i]).name));
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((loottable)list[i]).using_));
                listView_LootTable_Search_Results.Items.Add(item);
                count++;
            }
            label_display.Text = "Displaying " + count + " loottables.";
        }

        private void Form_LootTableSearch_Load(object sender, EventArgs e)
        {
            LoadLootTables();
        }

        private void listView_LootTable_Search_Results_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intselectedindex = listView_LootTable_Search_Results.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                this.ReturnId = listView_LootTable_Search_Results.Items[intselectedindex].SubItems[0].Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
