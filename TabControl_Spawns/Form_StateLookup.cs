using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MySql.Data.MySqlClient;

struct visual_state {
    public string id;
    public string name;
};


namespace TabControl_Spawns {
    public partial class Form_StateLookup : Form {
        private ArrayList list;
        private MySqlEngine db;
        public string ReturnValue { get; set; }

        public Form_StateLookup(MySqlEngine db) {
            InitializeComponent();
            list = new ArrayList();
            this.db = db;
        }

        private void Form_StateLookup_Load(object sender, EventArgs e) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT visual_state_id, name " +
                                                       "FROM visual_states");
            if (reader != null) {
                while (reader.Read()) {
                    visual_state vs;
                    vs.id = reader.GetString(0);
                    vs.name = reader.GetString(1);
                    list.Add(vs);
                }
                reader.Close();
            }

            int count = 0;
            for (int i = 0; i < list.Count; i++) {
                ListViewItem item = new ListViewItem(((visual_state)list[i]).name);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((visual_state)list[i]).id));
                listView_visualstates.Items.Add(item);
                count++;
            }
            label_display.Text = "Displaying " + count + " visual states.";
        }

        private void textBox_search_TextChanged(object sender, EventArgs e) {
            listView_visualstates.Items.Clear();
            string search_string = textBox_search.Text.ToLower();
            int count = 0;

            for (int i = 0; i < list.Count; i++) {
                string name = ((visual_state)list[i]).name;
                string id = ((visual_state)list[i]).id;
                ListViewItem item = new ListViewItem(name);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, id));

                if ((name.ToLower()).Contains(search_string) || id.Contains(search_string)) {
                    listView_visualstates.Items.Add(item);
                    count++;
                }
            }
            label_display.Text = "Displaying " + count + " visual states";
        }

        private void listView_visualstates_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int intselectedindex = listView_visualstates.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                this.ReturnValue= listView_visualstates.Items[intselectedindex].SubItems[1].Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}