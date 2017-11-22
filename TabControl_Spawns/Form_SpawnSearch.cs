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
    public partial class Form_SpawnSearch : Form
    {
        struct spawn
        {
            public string id;
            public string name;
            public string type;
            public string zone;
            public string zone_short;
            public string name_long;
        };

        private ArrayList list;
        private MySqlEngine db;
        public string ReturnId { get; set; }
        public string ReturnName { get; set; }
        public string ReturnType { get; set; }
        public string ReturnZone { get; set; }

        public Form_SpawnSearch(MySqlEngine db)
        {
            InitializeComponent();
            list = new ArrayList();
            this.db = db;
        }

        private void button_Spawn_Search_Click(object sender, EventArgs e)
        {
            if (textBoxSpawnSearch.Text.Length>0) {
                listView_Spawn_Search_Results.Items.Clear();

                MySqlDataReader reader = db.RunSelectQuery("SELECT s.id, s.name, CASE when g.id THEN 'Ground' WHEN si.id THEN 'Sign' WHEN w.id THEN 'Widget' WHEN o.id THEN 'Object' WHEN n.id THEN 'NPC' ELSE 'UNKNOWN' END as _TYPE, o.id as object_id, n.id as npc_id, w.id as widget_id, si.id as signs_id, g.id as ground_id, CONCAT(z.description, '   (', z.name, ')') as zone, z.name as zone_short, CONCAT(s.name, '   (',s.id,')') as name_long " +
                                  "FROM " +
                                  "spawn s " +
                                  "LEFT OUTER JOIN " +
                                  "spawn_objects o ON s.id=o.spawn_id " +
                                  "LEFT OUTER JOIN " +
                                  "spawn_npcs n ON s.id=n.spawn_id " +
                                  "LEFT OUTER JOIN " +
                                  "spawn_ground g ON s.id=g.spawn_id " +
                                  "LEFT OUTER JOIN " +
                                  "spawn_widgets w ON s.id=w.spawn_id " +
                                  "LEFT OUTER JOIN " +
                                  "spawn_signs si ON s.id=si.spawn_id " +
                                  "INNER JOIN " +
                                  "zones z ON z.id=SUBSTRING(s.id,1,LENGTH(s.id)-4) " +
                                  "WHERE s.name RLIKE '" + textBoxSpawnSearch.Text + "' ORDER BY z.name, s.name LIMIT 250");

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        spawn sp;
                        sp.id = reader.GetString(0);
                        sp.name = reader.GetString(1);
                        sp.type = reader.GetString(2);
                        sp.zone = reader.GetString(8);
                        sp.zone_short = reader.GetString(9);
                        sp.name_long = reader.GetString(10);
                        list.Add(sp);
                    }
                    reader.Close();
                }

                int count = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    ListViewItem item = new ListViewItem(((spawn)list[i]).id);
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((spawn)list[i]).name));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((spawn)list[i]).type));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((spawn)list[i]).zone));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((spawn)list[i]).zone_short));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((spawn)list[i]).name_long));
                    listView_Spawn_Search_Results.Items.Add(item);
                    count++;
                }
                label_display.Text = "Displaying " + count + " spawns.";
            }
        }

        private void listView_Spawn_Search_Results_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int intselectedindex = listView_Spawn_Search_Results.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                this.ReturnId = listView_Spawn_Search_Results.Items[intselectedindex].SubItems[0].Text;
                this.ReturnType = listView_Spawn_Search_Results.Items[intselectedindex].SubItems[2].Text;
                this.ReturnZone = listView_Spawn_Search_Results.Items[intselectedindex].SubItems[3].Text;
                this.ReturnName = listView_Spawn_Search_Results.Items[intselectedindex].SubItems[5].Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
