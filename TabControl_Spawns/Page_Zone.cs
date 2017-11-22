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
    public partial class Page_Zone : UserControl {
        private MySqlEngine db;
        private TabPage owner;
        private ArrayList zones;
        private ArrayList transports;

        public Page_Zone(MySqlConnection connection, ref TabPage owner) {
            InitializeComponent();
            this.db = new MySqlEngine(connection);
            this.owner = owner;
            zones = new ArrayList();
            transports = new ArrayList();
        }

        private void Page_Zone_Load(object sender, EventArgs e) {
            PopulateZonesComboBox();
            comboBox_select_zone.SelectedItem = Properties.Settings.Default.LastZone;
        }

        private bool PopulateZonesComboBox() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, description " +
                                                       "FROM zones");
            if (reader != null) {
                comboBox_select_zone.Items.Clear();
                while (reader.Read())
                    comboBox_select_zone.Items.Add(reader.GetString(1) + "   (" + reader.GetString(0) + ")");
                reader.Close();
                return true;
            }
            return false;
        }

        private void comboBox_select_zone_SelectedIndexChanged(object sender, EventArgs e) {
            int zone_id = GetSelectedZoneID();
            if (zone_id == -1)
                return;

            ResetZone();
            ResetRevivePoints(true);

            InitializeZones();
            InitializeTransportTypes();
            LoadZone(zone_id);
            LoadRevivePoints(zone_id);
            ResetTransporters(true);
            LoadTransporters(zone_id);

            Properties.Settings.Default.LastZone = (string)comboBox_select_zone.SelectedItem;
            Properties.Settings.Default.Save();

            tabControl_main.Visible = true;
        }

        private void InitializeZones() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, description " +
                                                       "FROM zones");
            if (reader != null) {
                zones.Clear();
                comboBox_revivepoints_respawnzoneid.Items.Clear();
                zones.Add(new Zone(0, "None"));
                comboBox_revivepoints_respawnzoneid.Items.Add(((Zone)zones[0]).zone);
                comboBox_transport_dest_zone.Items.Add(((Zone)zones[0]).zone);
                comboBox_trigger_dest_zone.Items.Add(((Zone)zones[0]).zone);
                while (reader.Read()) {
                    zones.Add(new Zone(reader.GetInt32(0), reader.GetString(1)));
                    comboBox_revivepoints_respawnzoneid.Items.Add(reader.GetString(1));
                    comboBox_transport_dest_zone.Items.Add(reader.GetString(1));
                    comboBox_trigger_dest_zone.Items.Add(reader.GetString(1));
                }
                reader.Close();
            }
        }

        #region "Revive Points"

        private void LoadRevivePoints(int zone_id)
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, location_name, zone_id, respawn_zone_id, safe_x, safe_y, safe_z, heading " +
                                                       "FROM revive_points " +
                                                       "WHERE zone_id=" + zone_id);
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
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(7)));
                    listView_revivepoints.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_revivepoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_revivepoints.SelectedIndices.Count == 0 || listView_revivepoints.SelectedIndices[0] == -1)
            {
                ResetRevivePoints(false);
                return;
            }

            ListViewItem item = listView_revivepoints.Items[listView_revivepoints.SelectedIndices[0]];
            textBox_revivepoints_id.Text = item.Text;
            textBox_revivepoints_locationname.Text = item.SubItems[1].Text;
            textBox_revivepoints_zoneid.Text = item.SubItems[2].Text;
            comboBox_revivepoints_respawnzoneid.SelectedItem = GetZoneName(Convert.ToInt32(item.SubItems[3].Text));
            textBox_revivepoints_safex.Text = item.SubItems[4].Text;
            textBox_revivepoints_safey.Text = item.SubItems[5].Text;
            textBox_revivepoints_safez.Text = item.SubItems[6].Text;
            textBox_revivepoints_heading.Text = item.SubItems[7].Text;

            button_revivepoints_insert.Enabled = true;
            button_revivepoints_update.Enabled = true;
            button_revivepoints_remove.Enabled = true;
        }

        private void button_revivepoints_insert_Click(object sender, EventArgs e)
        {
            string location_name = db.RemoveEscapeCharacters(textBox_revivepoints_locationname.Text);
            int zone_id = GetSelectedZoneID();
            int respawn_zone_id = GetZoneID((string)comboBox_revivepoints_respawnzoneid.SelectedItem);
            string safe_x = textBox_revivepoints_safex.Text;
            string safe_y = textBox_revivepoints_safey.Text;
            string safe_z = textBox_revivepoints_safez.Text;
            string heading = textBox_revivepoints_heading.Text;

            int rows = db.RunQuery("INSERT INTO revive_points (location_name, zone_id, respawn_zone_id, safe_x, safe_y, safe_z, heading) " +
                                   "VALUES ('" + location_name + "', " + zone_id + ", " + respawn_zone_id + ", " + safe_x + ", " + safe_y + ", " + safe_z + ", " + heading + ")");
            if (rows > 0)
            {
                ResetRevivePoints(true);
                LoadRevivePoints(zone_id);
            }
        }

        private void button_revivepoints_update_Click(object sender, EventArgs e)
        {
            string id = textBox_revivepoints_id.Text;
            string location_name = db.RemoveEscapeCharacters(textBox_revivepoints_locationname.Text);
            string zone_id = textBox_revivepoints_zoneid.Text;
            int respawn_zone_id = GetZoneID((string)comboBox_revivepoints_respawnzoneid.SelectedItem);
            string safe_x = textBox_revivepoints_safex.Text;
            string safe_y = textBox_revivepoints_safey.Text;
            string safe_z = textBox_revivepoints_safez.Text;
            string heading = textBox_revivepoints_heading.Text;

            int rows = db.RunQuery("UPDATE revive_points " +
                                   "SET location_name='" + location_name + "', zone_id=" + zone_id + ", respawn_zone_id=" + respawn_zone_id + ", safe_x=" + safe_x + ", safe_y=" + safe_y + ", safe_z=" + safe_z + ", heading=" + heading + " " +
                                   "WHERE id=" + id);
            if (rows > 0)
            {
                ResetRevivePoints(true);
                LoadRevivePoints(Convert.ToInt32(zone_id));
            }
        }

        private void button_revivepoints_remove_Click(object sender, EventArgs e)
        {
            string id = textBox_revivepoints_id.Text;

            int rows = db.RunQuery("DELETE FROM revive_points " +
                                   "WHERE id=" + id);

            if (rows > 0)
            {
                ResetRevivePoints(true);
                LoadRevivePoints(GetSelectedZoneID());
            }
        }

        private void ResetRevivePoints(bool include_listview)
        {
            if (include_listview)
                listView_revivepoints.Items.Clear();

            textBox_revivepoints_id.Clear();
            textBox_revivepoints_locationname.Clear();
            textBox_revivepoints_zoneid.Clear();
            comboBox_revivepoints_respawnzoneid.SelectedItem = "None";
            textBox_revivepoints_safex.Clear();
            textBox_revivepoints_safey.Clear();
            textBox_revivepoints_safez.Clear();
            textBox_revivepoints_heading.Clear();

            button_revivepoints_update.Enabled = false;
            button_revivepoints_remove.Enabled = false;
        }

        #endregion

        #region "Zones"

        private void LoadZone(int zone_id)
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, expansion_id, name, file, description, safe_x, safe_y, safe_z, safe_heading, underworld, xp_modifier, min_recommended, max_recommended, zone_type, always_loaded, city_zone, weather_allowed, min_status, min_level, max_level, start_zone, instance_type, default_reenter_time, default_reset_time, default_lockout_time, force_group_to_zone, lua_script, shutdown_timer, zone_motd, ruleset_id, login_checksum " +
                                                       "FROM zones " +
                                                       "WHERE id=" + zone_id);
            if (reader != null)
            {
                if (reader.Read())
                {
                    textBox_zone_id.Text = reader.GetString(0);
                    textBox_zone_expansionid.Text = reader.GetString(1);
                    textBox_zone_name.Text = reader.GetString(2);
                    textBox_zone_file.Text = reader.GetString(3);
                    textBox_zone_description.Text = reader.GetString(4);
                    textBox_zone_safex.Text = reader.GetString(5);
                    textBox_zone_safey.Text = reader.GetString(6);
                    textBox_zone_safez.Text = reader.GetString(7);
                    textBox_zone_safeheading.Text = reader.GetString(8);
                    textBox_zone_underworld.Text = reader.GetString(9);
                    textBox_zone_xpmodifier.Text = reader.GetString(10);
                    textBox_zone_minrecommended.Text = reader.GetString(11);
                    textBox_zone_maxrecommended.Text = reader.GetString(12);
                    textBox_zone_zonetype.Text = reader.GetString(13);
                    checkBox_zone_alwaysloaded.Checked = Convert.ToBoolean(reader.GetInt32(14));
                    checkBox_zone_cityzone.Checked = Convert.ToBoolean(reader.GetInt32(15));
                    checkBox_zone_weatherallowed.Checked = Convert.ToBoolean(reader.GetInt32(16));
                    textBox_zone_minstatus.Text = reader.GetString(17);
                    textBox_zone_minlevel.Text = reader.GetString(18);
                    textBox_zone_maxlevel.Text = reader.GetString(19);
                    //textBox_zone_startzone.Text = reader.GetString(20);
                    comboBox_zone_instancetype.SelectedItem = reader.GetString(21);
                    textBox_zone_defalutreentertime.Text = reader.GetString(22);
                    textBox_zone_defaultresettime.Text = reader.GetString(23);
                    textBox_zone_defaultlockouttime.Text = reader.GetString(24);
                    checkBox_zone_forcegrouptozone.Checked = Convert.ToBoolean(reader.GetInt32(25));
                    textBox_zone_luascript.Text = reader.GetString(26);
                    textBox_zone_shutdowntimer.Text = reader.GetString(27);
                    try { textBox_zone_zonemotd.Text = reader.GetString(28); }
                    catch (Exception ex) { textBox_zone_zonemotd.Text = ""; }
                    textBox_zone_rulesetid.Text = reader.GetString(29);
                    textBox_zone_loginchecksum.Text = reader.GetString(30);

                    owner.Text = "Zone: " + textBox_zone_description.Text;
                }
                reader.Close();
            }
        }

        private void button_zone_update_Click(object sender, EventArgs e)
        {
            string id = textBox_zone_id.Text;
            string expansion_id = textBox_zone_expansionid.Text;
            string name = db.RemoveEscapeCharacters(textBox_zone_name.Text);
            string file = db.RemoveEscapeCharacters(textBox_zone_file.Text);
            string description = db.RemoveEscapeCharacters(textBox_zone_description.Text);
            string safe_x = textBox_zone_safex.Text;
            string safe_y = textBox_zone_safey.Text;
            string safe_z = textBox_zone_safez.Text;
            string underworld = textBox_zone_underworld.Text;
            string min_recommended = textBox_zone_minrecommended.Text;
            string max_recommended = textBox_zone_maxrecommended.Text;
            string zone_type = db.RemoveEscapeCharacters(textBox_zone_zonetype.Text);
            int always_loaded = Convert.ToInt32(checkBox_zone_alwaysloaded.Checked);
            int city_zone = Convert.ToInt32(checkBox_zone_cityzone.Checked);
            string weather_allowed = checkBox_zone_weatherallowed.Checked ? "1" : "0";
            string min_status = textBox_zone_minstatus.Text;
            string min_level = textBox_zone_minlevel.Text;
            //string start_zone = comboBox_zone_startzone.SelectedItem;
            string instance_type = comboBox_zone_instancetype.SelectedItem.ToString();
            string lua_script = db.RemoveEscapeCharacters(textBox_zone_luascript.Text);
            string shutdown_timer = textBox_zone_shutdowntimer.Text;
            string zone_motd = db.RemoveEscapeCharacters(textBox_zone_zonemotd.Text);

            int rows = db.RunQuery("UPDATE zones " +
                                   "SET expansion_id=" + expansion_id + ", name='" + name + "', file='" + file + "', description='" + description + "', safe_x=" + safe_x + ", safe_y=" + safe_y + ", safe_z=" + safe_z + ", underworld=" + underworld + ", min_recommended=" + min_recommended + ", max_recommended=" + max_recommended + ", zone_type='" + zone_type + "', always_loaded=" + always_loaded + ", city_zone=" + city_zone + ", min_status=" + min_status + ", min_level=" + min_level + ", start_zone=0, instance_type='" + instance_type + "', lua_script='" + lua_script + "', shutdown_timer=" + shutdown_timer + ", zone_motd='" + zone_motd + "', weather_allowed=" + weather_allowed + " "+
                                   "WHERE id=" + id);
            if (rows > 0)
            {
                PopulateZonesComboBox();
                comboBox_select_zone.SelectedItem = textBox_zone_description.Text + "   (" + id + ")";
                LoadZone(Convert.ToInt32(id));
            }
        }

        private void ResetZone()
        {
        }

        #endregion

        #region "Transporters"

        private void InitializeTransportTypes()
        {
            transports.Add(new Transport(0, ""));
            transports.Add(new Transport(1, "Zone"));
            transports.Add(new Transport(2, "Location"));
            transports.Add(new Transport(3, "General Transport"));
        }

        private void LoadTransporters(int zone_id)
        {

            MySqlDataReader reader = db.RunSelectQuery("SELECT t.id, transport_id, transport_type, destination_zone_id, z1.Description, destination_x, destination_y, destination_z, destination_heading, cost, t.min_level, t.max_level, " +
                                                        "trigger_location_zone_id, z2.Description, trigger_location_x, trigger_location_y, trigger_location_z, trigger_radius, quest_req, quest_step_req, quest_completed, map_x, map_y, display_name, message " +
                                                        "FROM transporters t " +
                                                        "LEFT OUTER JOIN " +
                                                        "zones z1 ON t.destination_zone_id=z1.id " +
                                                        "LEFT OUTER JOIN " +
                                                        "zones z2 ON t.trigger_location_zone_id=z2.id " +
                                                        "WHERE trigger_location_zone_id=" + zone_id);

            if (reader != null)
            {
                while (reader.Read())
                {
                    ListViewItem item_1 = new ListViewItem(reader.GetString(0));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(1)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(2)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(3)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(4)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(5)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(6)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(7)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(8)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(9)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(10)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(11)));
                    item_1.SubItems.Add(new ListViewItem.ListViewSubItem(item_1, reader.GetString(12)));
                    listViewTransporters_1.Items.Add(item_1);

                    ListViewItem item_2 = new ListViewItem(reader.GetString(13));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(13)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(14)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(15)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(16)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(17)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(18)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(19)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(20)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(21)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(22)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(23)));
                    item_2.SubItems.Add(new ListViewItem.ListViewSubItem(item_2, reader.GetString(24)));
                    listViewTransporters_2.Items.Add(item_2);
                }
                reader.Close();
            }
        }

        private void listViewTransporters_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTransporters_1.SelectedItems.Count < 1)
            {
                ResetTransporters(false);
                button_transporters_save.Enabled = false;
                button_transporters_remove.Enabled = false;
                return;
            }
            else
            {
                int row = listViewTransporters_1.SelectedIndices[0];
                listViewTransporters_2.Items[row].Selected = true;
                button_transporters_save.Enabled = true;
                button_transporters_remove.Enabled = true;
            }

            ListViewItem transport_1 = listViewTransporters_1.Items[listViewTransporters_1.SelectedIndices[0]];
            textBox_transport_id.Tag = transport_1.SubItems[0].Text;
            textBox_transport_id.Text = transport_1.SubItems[1].Text;
            comboBox_transport_type.SelectedItem = transport_1.SubItems[2].Text;
            comboBox_transport_dest_zone.SelectedItem = GetZoneName(Convert.ToInt32(transport_1.SubItems[3].Text));
            textBox_transport_dest_x.Text = transport_1.SubItems[5].Text;
            textBox_transport_dest_y.Text = transport_1.SubItems[6].Text;
            textBox_transport_dest_z.Text = transport_1.SubItems[7].Text;
            textBox_transport_dest_heading.Text = transport_1.SubItems[8].Text;
            textBox_transport_cost.Text = transport_1.SubItems[9].Text;
            textBox_transport_min_level.Text = transport_1.SubItems[10].Text;
            textBox_transport_max_level.Text = transport_1.SubItems[11].Text;

            ListViewItem transport_2 = listViewTransporters_2.Items[listViewTransporters_2.SelectedIndices[0]];
            comboBox_trigger_dest_zone.SelectedItem = transport_2.SubItems[1].Text;
            textBox_transport_trigger_x.Text = transport_2.SubItems[2].Text;
            textBox_transport_trigger_y.Text = transport_2.SubItems[3].Text;
            textBox_transport_trigger_z.Text = transport_2.SubItems[4].Text;
            textBox_transport_trigger_radius.Text = transport_2.SubItems[5].Text;
            textBox_transport_trigger_quest_req.Text = transport_2.SubItems[6].Text;
            textBox_transport_trigger_quest_step_req.Text = transport_2.SubItems[7].Text;
            textBox_transport_trigger_quest_completed.Text = transport_2.SubItems[8].Text;
            textBox_transport_trigger_map_x.Text = transport_2.SubItems[9].Text;
            textBox_transport_trigger_map_y.Text = transport_2.SubItems[10].Text;
            textBox_transporters_display_name.Text = transport_2.SubItems[11].Text;
            textBox_transporters_message.Text = transport_2.SubItems[12].Text;
        }

        private void listViewTransporters_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTransporters_2.SelectedItems.Count > 0)
            {
                int row = listViewTransporters_2.SelectedIndices[0];
                listViewTransporters_1.Items[row].Selected = true;
                button_transporters_save.Enabled = true;
                button_transporters_remove.Enabled = true;
            } else
            {
                button_transporters_save.Enabled = false;
                button_transporters_remove.Enabled = false;
            }
        }

        private void ResetTransporters(bool include_listview)
        {
            if (include_listview)
            {
                listViewTransporters_1.Items.Clear();
                listViewTransporters_2.Items.Clear();
            }

            textBox_transport_id.Tag = "";
            textBox_transport_id.Clear();
            comboBox_transport_type.SelectedIndex = -1;
            comboBox_transport_dest_zone.SelectedIndex = -1;
            textBox_transport_dest_x.Clear();
            textBox_transport_dest_y.Clear();
            textBox_transport_dest_z.Clear();
            textBox_transport_dest_heading.Clear();
            textBox_transport_cost.Clear();
            textBox_transport_min_level.Clear();
            textBox_transport_max_level.Clear();
            comboBox_trigger_dest_zone.SelectedIndex = -1;
            textBox_transport_trigger_x.Clear();
            textBox_transport_trigger_y.Clear();
            textBox_transport_trigger_z.Clear();
            textBox_transport_trigger_radius.Clear();
            textBox_transport_trigger_quest_req.Clear();
            textBox_transport_trigger_quest_step_req.Clear();
            textBox_transport_trigger_quest_completed.Clear();
            textBox_transport_trigger_map_x.Clear();
            textBox_transport_trigger_map_y.Clear();
            textBox_transporters_display_name.Clear();
            textBox_transporters_message.Clear();
        }

        private void button_transporters_save_Click(object sender, EventArgs e)
        {
            string id = string.IsNullOrEmpty((string)textBox_transport_id.Tag) ? "0" : (string)textBox_transport_id.Tag;
            string transport_id = string.IsNullOrEmpty(textBox_transport_id.Text) ? "0" : textBox_transport_id.Text;
            int transport_type = GetTransportID((string)comboBox_transport_type.SelectedItem);
            int transport_dest_zone_id = GetZoneID((string)comboBox_transport_dest_zone.SelectedItem);
            string transport_dest_x = string.IsNullOrEmpty(textBox_transport_dest_x.Text) ? "0" : textBox_transport_dest_x.Text;
            string transport_dest_y = string.IsNullOrEmpty(textBox_transport_dest_y.Text) ? "0" : textBox_transport_dest_y.Text;
            string transport_dest_z = string.IsNullOrEmpty(textBox_transport_dest_z.Text) ? "0" : textBox_transport_dest_z.Text;
            string transport_dest_heading = string.IsNullOrEmpty(textBox_transport_dest_heading.Text) ? "0" : textBox_transport_dest_heading.Text;
            string transport_cost = string.IsNullOrEmpty(textBox_transport_cost.Text) ? "0" : textBox_transport_cost.Text;
            string transport_min_level = string.IsNullOrEmpty(textBox_transport_min_level.Text) ? "0" : textBox_transport_min_level.Text;
            string transport_max_level = string.IsNullOrEmpty(textBox_transport_max_level.Text) ? "0" : textBox_transport_max_level.Text;
            int transport_trigger_dest_zone = GetZoneID((string)comboBox_trigger_dest_zone.SelectedItem);
            string transport_trigger_x = string.IsNullOrEmpty(textBox_transport_trigger_x.Text) ? "0" : textBox_transport_trigger_x.Text;
            string transport_trigger_y = string.IsNullOrEmpty(textBox_transport_trigger_y.Text) ? "0" : textBox_transport_trigger_y.Text;
            string transport_trigger_z = string.IsNullOrEmpty(textBox_transport_trigger_z.Text) ? "0" : textBox_transport_trigger_z.Text;
            string transport_radius = string.IsNullOrEmpty(textBox_transport_trigger_radius.Text) ? "0" : textBox_transport_trigger_radius.Text;
            string transport_quest_req = string.IsNullOrEmpty(textBox_transport_trigger_quest_req.Text) ? "0" : textBox_transport_trigger_quest_req.Text;
            string transport_quest_step_req = string.IsNullOrEmpty(textBox_transport_trigger_quest_step_req.Text) ? "0" : textBox_transport_trigger_quest_step_req.Text;
            string transport_quest_completed = string.IsNullOrEmpty(textBox_transport_trigger_quest_completed.Text) ? "0" : textBox_transport_trigger_quest_completed.Text;
            string transport_trigger_map_x = string.IsNullOrEmpty(textBox_transport_trigger_map_x.Text) ? "0" : textBox_transport_trigger_map_x.Text;
            string transport_trigger_map_y = string.IsNullOrEmpty(textBox_transport_trigger_map_y.Text) ? "0" : textBox_transport_trigger_map_y.Text;
            string transport_display_name = string.IsNullOrEmpty(textBox_transporters_display_name.Text) ? "0" : textBox_transporters_display_name.Text;
            string transport_message = string.IsNullOrEmpty(textBox_transporters_message.Text) ? "0" : textBox_transporters_message.Text;

            int rows = db.RunQuery("UPDATE transporters SET transport_id=" + transport_id + ", transport_type=" + transport_type + ", destination_zone_id=" + transport_dest_zone_id + ", destination_x=" + transport_dest_x + ", destination_y=" + transport_dest_y + " " +
                                   ", destination_z=" + transport_dest_z + ", destination_heading= " + transport_dest_heading + ", trigger_location_zone_id=" + transport_trigger_dest_zone + ", trigger_location_x=" + transport_trigger_x + ", trigger_location_y=" + transport_trigger_y + " " +
                                   ", trigger_location_z=" + transport_trigger_z + ", trigger_radius=" + transport_radius + ", cost=" + transport_cost + ", message='" + transport_message + "', min_level=" + transport_min_level + ", max_level=" + transport_max_level + " " +
                                   ", quest_req=" + transport_quest_req + ", quest_step_req=" + transport_quest_step_req + ", quest_completed=" + transport_quest_completed + ", map_x=" + transport_trigger_map_x + ", map_y=" + transport_trigger_map_y + ", display_name='" + transport_display_name + "' " +
                                   "WHERE id=" + id);
            if (rows > 0)
            {
                ResetTransporters(true);
                LoadTransporters(GetSelectedZoneID());
            }
        }

        private void button_transporters_insert_Click(object sender, EventArgs e)
        {
            string id = string.IsNullOrEmpty((string)textBox_transport_id.Tag) ? "0" : (string)textBox_transport_id.Tag;
            string transport_id = string.IsNullOrEmpty(textBox_transport_id.Text) ? "0" : textBox_transport_id.Text;
            int transport_type = GetTransportID((string)comboBox_transport_type.SelectedItem);
            int transport_dest_zone_id = GetZoneID((string)comboBox_transport_dest_zone.SelectedItem) == -1 ? 1 : GetZoneID((string)comboBox_transport_dest_zone.SelectedItem);
            string transport_dest_x = string.IsNullOrEmpty(textBox_transport_dest_x.Text) ? "0" : textBox_transport_dest_x.Text;
            string transport_dest_y = string.IsNullOrEmpty(textBox_transport_dest_y.Text) ? "0" : textBox_transport_dest_y.Text;
            string transport_dest_z = string.IsNullOrEmpty(textBox_transport_dest_z.Text) ? "0" : textBox_transport_dest_z.Text;
            string transport_dest_heading = string.IsNullOrEmpty(textBox_transport_dest_heading.Text) ? "0" : textBox_transport_dest_heading.Text;
            string transport_cost = string.IsNullOrEmpty(textBox_transport_cost.Text) ? "0" : textBox_transport_cost.Text;
            string transport_min_level = string.IsNullOrEmpty(textBox_transport_min_level.Text) ? "0" : textBox_transport_min_level.Text;
            string transport_max_level = string.IsNullOrEmpty(textBox_transport_max_level.Text) ? "0" : textBox_transport_max_level.Text;
            int transport_trigger_zone_id = GetZoneID((string)comboBox_trigger_dest_zone.SelectedItem) == -1 ? 1 : GetZoneID((string)comboBox_trigger_dest_zone.SelectedItem);
            string transport_trigger_x = string.IsNullOrEmpty(textBox_transport_trigger_x.Text) ? "0" : textBox_transport_trigger_x.Text;
            string transport_trigger_y = string.IsNullOrEmpty(textBox_transport_trigger_y.Text) ? "0" : textBox_transport_trigger_y.Text;
            string transport_trigger_z = string.IsNullOrEmpty(textBox_transport_trigger_z.Text) ? "0" : textBox_transport_trigger_z.Text;
            string transport_radius = string.IsNullOrEmpty(textBox_transport_trigger_radius.Text) ? "0" : textBox_transport_trigger_radius.Text;
            string transport_quest_req = string.IsNullOrEmpty(textBox_transport_trigger_quest_req.Text) ? "0" : textBox_transport_trigger_quest_req.Text;
            string transport_quest_step_req = string.IsNullOrEmpty(textBox_transport_trigger_quest_step_req.Text) ? "0" : textBox_transport_trigger_quest_step_req.Text;
            string transport_quest_completed = string.IsNullOrEmpty(textBox_transport_trigger_quest_completed.Text) ? "0" : textBox_transport_trigger_quest_completed.Text;
            string transport_trigger_map_x = string.IsNullOrEmpty(textBox_transport_trigger_map_x.Text) ? "0" : textBox_transport_trigger_map_x.Text;
            string transport_trigger_map_y = string.IsNullOrEmpty(textBox_transport_trigger_map_y.Text) ? "0" : textBox_transport_trigger_map_y.Text;
            string transport_display_name = string.IsNullOrEmpty(textBox_transporters_display_name.Text) ? "" : textBox_transporters_display_name.Text;
            string transport_message = string.IsNullOrEmpty(textBox_transporters_message.Text) ? "" : textBox_transporters_message.Text;

            int rows = db.RunQuery("INSERT INTO transporters (transport_id, transport_type, destination_zone_id, destination_x, destination_y, destination_z, destination_heading, trigger_location_zone_id, " +
                                   "trigger_location_x, trigger_location_y, trigger_location_z, trigger_radius, cost, message, min_level, max_level, quest_req, quest_step_req, quest_completed, map_x, map_y, display_name) VALUES " +
                                   "(" + transport_id + ", " + transport_type + ", " + transport_dest_zone_id + ", " + transport_dest_x +
                                   ", " + transport_dest_y + ", " + transport_dest_z + ", " + transport_dest_heading + ", " + transport_trigger_zone_id + ", " + transport_trigger_x + ", " + transport_trigger_y +
                                   ", " + transport_trigger_z + ", " + transport_radius + ", " + transport_cost + ", '" + transport_message + "', " + transport_min_level + ", " + transport_max_level +
                                   ", " + transport_quest_req + ", " + transport_quest_step_req + ", " + transport_quest_completed + ", " + transport_trigger_map_x + ", " + transport_trigger_map_y + ", '" + transport_display_name + "')");



            if (rows > 0)
            {
                ResetTransporters(true);
                LoadTransporters(GetSelectedZoneID());
            }
        }

        private void button_transporters_remove_Click(object sender, EventArgs e)
        {
            if (listViewTransporters_1.SelectedItems.Count > 0)
            {
                string id = (string)textBox_transport_id.Tag;

                int rows = db.RunQuery("DELETE FROM transporters " +
                                       "WHERE id=" + id);

                if (rows > 0)
                {
                    ResetTransporters(true);
                    LoadTransporters(GetSelectedZoneID());
                }
            }
        }

        private void button_transporters_insert_MouseHover(object sender, EventArgs e)
        {
            toolTip_zone.SetToolTip(button_transporters_insert, "Insert");
        }

        private void button_transporters_remove_MouseHover(object sender, EventArgs e)
        {
            toolTip_zone.SetToolTip(button_transporters_remove, "Remove");
        }

        private void button_transporters_save_MouseHover(object sender, EventArgs e)
        {
            toolTip_zone.SetToolTip(button_transporters_save, "Save");
        }

        #endregion

        #region "Miscellaneous"

        private void button_close_Click(object sender, EventArgs e)
        {
            owner.Dispose();
        }

        private int GetZoneID(string zone)
        {
            for (int i = 0; i < zones.Count; i++)
            {
                Zone type = (Zone)zones[i];
                if (zone == type.zone)
                    return type.id;
            }
            return -1;
        }

        private string GetZoneName(int id)
        {
            for (int i = 0; i < zones.Count; i++)
            {
                Zone type = (Zone)zones[i];
                if (id == type.id)
                    return type.zone;
            }
            return null;
        }

        private int GetTransportID(string transport)
        {
            for (int i = 0; i < transports.Count; i++)
            {
                Transport type = (Transport)transports[i];
                if (transport == type.type)
                    return type.id;
            }
            return -1;
        }

        private string GetTransportName(int id)
        {
            for (int i = 0; i < transports.Count; i++)
            {
                Transport type = (Transport)transports[i];
                if (id == type.id)
                    return type.type;
            }
            return null;
        }

        private int GetSelectedZoneID()
        {
            if (comboBox_select_zone.SelectedItem == null)
                return -1;

            string search_for = "   (";
            string zone_name = (string)comboBox_select_zone.SelectedItem;
            zone_name = zone_name.Substring(zone_name.IndexOf(search_for) + search_for.Length);
            zone_name = zone_name.Remove(zone_name.Length - 1);
            return Convert.ToInt32(zone_name);
        }

        #endregion

    }
}
