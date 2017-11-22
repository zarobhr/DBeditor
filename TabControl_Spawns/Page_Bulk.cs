using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace TabControl_Spawns {
    public partial class Page_Bulk : UserControl {
        private MySqlEngine db;
        private TabPage owner;

        public Page_Bulk(MySqlConnection connection, ref TabPage owner) {
            InitializeComponent();
            this.db = new MySqlEngine(connection);
            this.owner = owner;
            
        }

        private void LoadLastSettings()
        {
            comboBox_select_zone.SelectedItem = Properties.Settings.Default.BulkLastZone;
        }

        private void Page_Bulk_Load(object sender, EventArgs e) {
            comboBox_filter.SelectedIndex = 1;

            string SQL = "SELECT z.id, name, description " +
                         "FROM zones z ";

            if (Settings.DisplayUnpopulatedZones == 0)
                SQL += "INNER JOIN " +
                     "spawn_location_placement p ON p.zone_id=z.id " +
                     "GROUP BY z.id";

            MySqlDataReader reader = db.RunSelectQuery(SQL);
            if (reader != null)
            {
                comboBox_select_zone.Items.Clear();
                while (reader.Read())
                    comboBox_select_zone.Items.Add(reader.GetString(2) + "   (" + reader.GetString(1) + ")");
                reader.Close();
            }
            LoadLastSettings();
            LoadLootTables();
        }

        private void LoadLootTables()
        {
            ListView_bulk_loottables.Items.Clear();

            MySqlDataReader reader = db.RunSelectQuery("SELECT lt.id, lt.name, lt.mincoin, lt.maxcoin, lt.minlootitems, lt.maxlootitems, lt.coin_probability " +
                                                        "FROM loottable lt; ");
            if (reader != null)
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(5)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(6)));
                    ListView_bulk_loottables.Items.Add(item);
                }
                reader.Close();
            }

            if (ListView_bulk_loottables.Items.Count == 1)
            {
                ListView_bulk_loottables.Items[0].Selected = true;
                ListView_bulk_loottables.Items[0].Focused = true;
            }
        }

        private void ListView_bulk_loottables_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSpawns(GetSelectedZoneID());

            if (ListView_bulk_loottables.SelectedItems.Count > 0)
                SpawnsByLootTableID(Int64.Parse(ListView_bulk_loottables.Items[ListView_bulk_loottables.SelectedIndices[0]].Text));
        }

        private void SpawnsByLootTableID(Int64 loot_table)
        {
            
            ListView_bulk_spawns.SelectedItems.Clear();

            MySqlDataReader reader = db.RunSelectQuery( "SELECT s.id " +
                                                        "FROM spawn s " +
                                                        "INNER JOIN " +
                                                        "spawn_loot l ON s.id=l.spawn_id " +
                                                        "WHERE loottable_id=" + loot_table);

            if (reader != null)
            {
                while (reader.Read())
                {
                    Int64 spawn_id = reader.GetInt64(0);

                    foreach (ListViewItem i in ListView_bulk_spawns.Items)
                    {
                        if (Int64.Parse(i.Text.ToString()) == spawn_id)
                        {
                            ListView_bulk_spawns.Items[i.Index].Checked = true;
                        }
                    }
                }
                reader.Close();
            }
        }

        private void LoadSpawns(Int32 zone_id)
        {
            ListView_bulk_spawns.Items.Clear();

            string where = "WHERE s.id LIKE '" + zone_id + "____' ";

            if (textBox_filter_1.Text != "" || textBox_filter_1.Text != "")
            {
                where += "AND ";
            }

            if (textBox_filter_1.Text != "" && textBox_filter_2.Text != "")
            {
                where += "(";
            }

             if (textBox_filter_1.Text != "")
            {
                where += "s.name LIKE '%" + textBox_filter_1.Text + "%' ";
            }
 
            if (textBox_filter_1.Text != "" && textBox_filter_2.Text != "")
            {
                where += comboBox_filter.Text;
            }

            if (textBox_filter_2.Text != "")
            {
                where += " s.name LIKE '%" + textBox_filter_2.Text + "%' ";
            }

            if (textBox_filter_1.Text != "" && textBox_filter_2.Text != "")
            {
                where += ")";
            }



            MySqlDataReader reader = db.RunSelectQuery( "SELECT s.id, name, CONCAT(min_level, '-', max_level) as level " +
                                                        "FROM spawn s " +
                                                        "INNER JOIN " +
                                                        "spawn_npcs n ON s.id=n.spawn_id " +
                                                         where +
                                                        "ORDER BY name;");
            if (reader != null)
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    ListView_bulk_spawns.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void comboBox_select_zone_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSpawns(GetSelectedZoneID());
            Properties.Settings.Default.BulkLastZone = (string)comboBox_select_zone.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private int GetSelectedZoneID() {
            string zone_name = GetSelectedZoneName();
            if (zone_name == null)
                return -1;
            MySqlDataReader reader = db.RunSelectQuery("SELECT zones.id " +
                                                       "FROM zones "  +
                                                       "WHERE zones.name='" + zone_name + "'");
            if (reader != null) {
                if (reader.Read()) {
                    int zone_id = reader.GetInt32(0);
                    reader.Close();
                    return zone_id;
                }
            }
            return -1;
        }

        private string GetSelectedZoneName()
        {
            string search_for = "   (";
            int index = comboBox_select_zone.SelectedIndex;
            if (index == -1)
                return null;
            string zone_name = (string)comboBox_select_zone.Items[index];
            zone_name = zone_name.Substring(zone_name.IndexOf(search_for) + search_for.Length);
            zone_name = zone_name.Remove(zone_name.Length - 1);
            return zone_name;
        }

        private void button_bulk_loot_update_Click(object sender, EventArgs e)
        {
            Int32 zone_id = GetSelectedZoneID();
            Int64 loottable_id = Int64.Parse(ListView_bulk_loottables.Items[ListView_bulk_loottables.SelectedIndices[0]].Text);

            db.RunQuery("DELETE " +
                        "FROM spawn_loot " +
                        "WHERE spawn_id  " +
                        "LIKE '" + zone_id + "____' AND loottable_id = " + loottable_id);

            int rows = 0;

            foreach (ListViewItem i in ListView_bulk_spawns.Items)
            {
                if (ListView_bulk_spawns.Items[i.Index].Checked == true)
                {
                    rows += db.RunQuery("INSERT INTO spawn_loot(spawn_id, loottable_id) VALUES (" + Int64.Parse(ListView_bulk_spawns.Items[i.Index].Text) + ", " + loottable_id + ");");
                }
                ListView_bulk_spawns.Items[i.Index].Checked = false;
            }

            if (rows > 0)
            {
                ListView_bulk_loottables_SelectedIndexChanged(this, null);
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void textBox_filter_1_KeyUp(object sender, KeyEventArgs e)
        {
            LoadSpawns(GetSelectedZoneID());

            if (ListView_bulk_loottables.SelectedItems.Count > 0)
                SpawnsByLootTableID(Int64.Parse(ListView_bulk_loottables.Items[ListView_bulk_loottables.SelectedIndices[0]].Text));
        }

        private void comboBox_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSpawns(GetSelectedZoneID());

            if (ListView_bulk_loottables.SelectedItems.Count > 0)
                SpawnsByLootTableID(Int64.Parse(ListView_bulk_loottables.Items[ListView_bulk_loottables.SelectedIndices[0]].Text));
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            owner.Dispose();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            LoadLootTables();
            LoadSpawns(GetSelectedZoneID());

            if (ListView_bulk_loottables.SelectedItems.Count > 0)
                SpawnsByLootTableID(Int64.Parse(ListView_bulk_loottables.Items[ListView_bulk_loottables.SelectedIndices[0]].Text));
        }

        private void textBox_filter_2_KeyUp(object sender, KeyEventArgs e)
        {
            LoadSpawns(GetSelectedZoneID());

            if (ListView_bulk_loottables.SelectedItems.Count > 0)
                SpawnsByLootTableID(Int64.Parse(ListView_bulk_loottables.Items[ListView_bulk_loottables.SelectedIndices[0]].Text));
        }
    }
}
