using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

struct NPCAppearanceInfo {
    public int spawn_id;
    public int signed_value;
    public string type;
    public int red;
    public int green;
    public int blue;
};

struct NPCAppearanceEquipInfo {
    public int spawn_id;
    public int slot_id;
    public int equip_type;
    public int red;
    public int green;
    public int blue;
    public int highlight_red;
    public int highlight_green;
    public int highlight_blue;
};

namespace TabControl_Spawns {
    public partial class Form_DuplicateSpawn : Form {
        private string type;
        private int spawn_id;
        private int zone_id;
        private MySqlEngine db;
        private ArrayList npc_appearances;
        private ArrayList npc_appearance_equps;

        public Form_DuplicateSpawn(string type, int spawn_id, int zone_id, ref MySqlEngine db) {
            InitializeComponent();
            this.type = type;
            this.spawn_id = spawn_id;
            this.zone_id = zone_id;
            this.db = db;
            npc_appearances = new ArrayList();
            npc_appearance_equps = new ArrayList();
        }

        private void Form_DuplicateSpawn_Load(object sender, EventArgs e) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT name " +
                                                       "FROM spawn " +
                                                       "WHERE spawn.id=" + spawn_id);
            if (reader != null) {
                if (reader.Read())
                    label_duplicateinfo.Text = "Duplicating \"" + reader.GetString(0) + "\" (" + spawn_id + ")";
                reader.Close();
            }
        }

        private void button_duplicate_Click(object sender, EventArgs e) {
            int new_spawn_id = GetNextSpawnID();
            if (new_spawn_id != -1) {
                if (Duplicate(new_spawn_id)) {
                    MessageBox.Show("Spawn duplicated with Spawn ID " + new_spawn_id + ".\nYou must /spawn the spawn and /spawn add new to save it into the zone.", "Spawn Duplicated");
                    this.Close();
                }
            }
            else
                MessageBox.Show("There was an error duplicating your spawn!");
        }

        private int GetNextSpawnID() {
            int new_spawn_id = -1;
            MySqlDataReader reader = db.RunSelectQuery("SELECT MAX(id) " +
                                                       "FROM spawn WHERE id LIKE '" + zone_id + "____'");
            if (reader != null) {
                if (reader.Read())
                    new_spawn_id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            return new_spawn_id;
        }

        private bool Duplicate(int new_spawn_id) {
            if (SaveSpawnInfo(new_spawn_id)) {
                if (type == "NPC") {
                    if (SaveNPCInfo(new_spawn_id))
                        if (SaveNPCAppearanceInfo(new_spawn_id))
                            if (SaveNPCAppearanceEquipInfo(new_spawn_id))
                                return true;
                }
                else if (type == "Object")
                    if (SaveObjectInfo(new_spawn_id))
                        return true;
                else if (type == "Sign")
                    return false;
                else if (type == "Widget")
                    return false;
                else if (type == "GroundSpawn")
                    return false;
            }
            return false;
        }

        private bool SaveSpawnInfo(int new_spawn_id) {
            bool success = false;
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, name, sub_title, race, model_type, size, targetable, show_name, command_primary, command_secondary, visual_state, attackable, show_level, show_command_icon, display_hand_icon, faction_id, collision_radius, hp, power, merchant_id, transport_id, merchant_type " +
                                                       "FROM spawn " +
                                                       "WHERE spawn.id=" + spawn_id);
            if (reader != null) {
                if (reader.Read()) {
                    string sub_title;
                    string id = reader.GetString(0);
                    string name = db.RemoveEscapeCharacters(reader.GetString(1));
                    try {sub_title = db.RemoveEscapeCharacters(reader.GetString(2));} catch (Exception ex) {sub_title = null;}
                    string race = reader.GetString(3);
                    string model_type = reader.GetString(4);
                    string size = reader.GetString(5);
                    string targetable = reader.GetString(6);
                    string show_name = reader.GetString(7);
                    string command_primary = reader.GetString(8);
                    string command_secondary = reader.GetString(9);
                    string visual_state = reader.GetString(10);
                    string attackable = reader.GetString(11);
                    string show_level = reader.GetString(12);
                    string show_command_icon = reader.GetString(13);
                    string display_hand_icon = reader.GetString(14);
                    string faction_id = reader.GetString(15);
                    string collision_radius = reader.GetString(16);
                    string hp = reader.GetString(17);
                    string power = reader.GetString(18);
                    string merchant_id = reader.GetString(19);
                    string transport_id = reader.GetString(20);
                    string merchant_type = reader.GetString(21);
                    reader.Close();

                    if (textBox_name.Text != null && textBox_name.Text.Length > 0)
                        name = textBox_name.Text;

                    int rows = db.RunQuery("INSERT INTO spawn (id, name, sub_title, race, model_type, size, targetable, show_name, command_primary, command_secondary, visual_state, attackable, show_level, show_command_icon, display_hand_icon, faction_id, collision_radius, hp, power, merchant_id, transport_id, merchant_type) " +
                                           "VALUES (" + new_spawn_id + ", '" + name + "', '" + sub_title + "', " + race + ", " + model_type + ", " + size + ", " + targetable + ", " + show_name + ", " + command_primary + ", " + command_secondary + ", " + visual_state + ", " + attackable + ", " + show_level + ", " + show_command_icon + ", " + display_hand_icon + ", " + faction_id + ", " + collision_radius + ", " + hp + ", " + power + ", " + merchant_id + ", " + transport_id + ", " + merchant_id + ")");
                    if (rows > 0)
                        success = true;
                }
                reader.Close();
            }
            return success;
        }

        private bool SaveNPCInfo(int new_spawn_id) {
            bool success = false;
            MySqlDataReader reader = db.RunSelectQuery("SELECT spawn_npcs.min_level, spawn_npcs.max_level, spawn_npcs.enc_level, spawn_npcs.class_, spawn_npcs.gender, spawn_npcs.min_group_size, spawn_npcs.max_group_size, spawn_npcs.hair_type_id, spawn_npcs.facial_hair_type_id, spawn_npcs.wing_type_id, spawn_npcs.chest_type_id, spawn_npcs.legs_type_id, spawn_npcs.soga_hair_type_id, spawn_npcs.soga_facial_hair_type_id, spawn_npcs.soga_model_type, spawn_npcs.heroic_flag, spawn_npcs.action_state, spawn_npcs.mood_state, spawn_npcs.initial_state, activity_status " +
                                                       "FROM spawn_npcs " +
                                                       "WHERE spawn_npcs.spawn_id=" + spawn_id);
            if (reader != null) {
                if (reader.Read()) {
                    string min_level = reader.GetString(0);
                    string max_level = reader.GetString(1);
                    string enc_level = reader.GetString(2);
                    string class_ = reader.GetString(3);
                    string gender = reader.GetString(4);
                    string min_group_size = reader.GetString(5);
                    string max_group_size = reader.GetString(6);
                    string hair_type_id = reader.GetString(7);
                    string facial_hair_type_id = reader.GetString(8);
                    string wing_type_id = reader.GetString(9);
                    string chest_type_id = reader.GetString(10);
                    string legs_type_id = reader.GetString(11);
                    string soga_hair_type_id = reader.GetString(12);
                    string soga_facial_hair_type_id = reader.GetString(13);
                    string soga_model_type = reader.GetString(14);
                    string heroic_flag = reader.GetString(15);
                    string action_state = reader.GetString(16);
                    string mood_state = reader.GetString(17);
                    string initial_state = reader.GetString(18);
                    string activity_status = reader.GetString(19);
                    reader.Close();

                    int rows = db.RunQuery("INSERT INTO spawn_npcs (spawn_id, min_level, max_level, enc_level, class_, gender, min_group_size, max_group_size, hair_type_id, facial_hair_type_id, wing_type_id, chest_type_id, legs_type_id, soga_hair_type_id, soga_facial_hair_type_id, soga_model_type, heroic_flag, action_state, mood_state, initial_state, activity_status) " +
                                           "VALUES (" + new_spawn_id + ", " + min_level + ", " + max_level + ", " + enc_level + ", " + class_ + ", " + gender + ", " + min_group_size + ", " + max_group_size + ", " + hair_type_id + ", " + facial_hair_type_id + ", " + wing_type_id + ", " + chest_type_id + ", " + legs_type_id + ", " + soga_hair_type_id + ", " + soga_facial_hair_type_id + ", " + soga_model_type + ", " + heroic_flag + ", " + action_state + ", " + mood_state + ", " + initial_state + ", " + activity_status + ")");
                    if (rows > 0)
                        success = true;
                }
                reader.Close();
            }
            return success;
        }

        private bool SaveNPCAppearanceInfo(int new_spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT signed_value, type, red, green, blue " +
                                                       "FROM npc_appearance " +
                                                       "WHERE spawn_id=" + spawn_id);
            if (reader != null) {
                while (reader.Read()) {
                    NPCAppearanceInfo info = new NPCAppearanceInfo();
                    info.spawn_id = new_spawn_id;
                    info.signed_value = reader.GetInt32(0);
                    info.type = reader.GetString(1);
                    info.red = reader.GetInt32(2);
                    info.green = reader.GetInt32(3);
                    info.blue = reader.GetInt32(4);
                    npc_appearances.Add(info);
                }
                 reader.Close();
            }
            else
                return false;

            for (int i = 0; i < npc_appearances.Count; i++) {
                NPCAppearanceInfo info = (NPCAppearanceInfo)npc_appearances[i];
                int rows = db.RunQuery("INSERT INTO npc_appearance (spawn_id, signed_value, type, red, green, blue) " +
                                       "VALUES (" + info.spawn_id + ", " + info.signed_value + ", '" + info.type + "', " + info.red + ", " + info.green + ", " + info.blue + ")");
                if (rows <= 0)
                    return false;
            }
            return true;
        }

        private bool SaveNPCAppearanceEquipInfo(int new_spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT slot_id, equip_type, red, green, blue, highlight_red, highlight_green, highlight_blue " +
                                                       "FROM npc_appearance_equip " +
                                                       "WHERE spawn_id=" + spawn_id);
            if (reader != null) {
                while (reader.Read()) {
                    NPCAppearanceEquipInfo info = new NPCAppearanceEquipInfo();
                    info.spawn_id = new_spawn_id;
                    info.slot_id = reader.GetInt32(0);
                    info.equip_type = reader.GetInt32(1);
                    info.red = reader.GetInt32(2);
                    info.green = reader.GetInt32(3);
                    info.blue = reader.GetInt32(4);
                    info.highlight_red = reader.GetInt32(2);
                    info.highlight_green = reader.GetInt32(3);
                    info.highlight_blue = reader.GetInt32(4);
                    npc_appearance_equps.Add(info);
                }
                reader.Close();
            }
            else
                return false;

            for (int i = 0; i < npc_appearance_equps.Count; i++) {
                NPCAppearanceEquipInfo info = (NPCAppearanceEquipInfo)npc_appearance_equps[i];
                int rows = db.RunQuery("INSERT INTO npc_appearance_equip (spawn_id, slot_id, equip_type, red, green, blue, highlight_red, highlight_green, highlight_blue) " +
                                        "VALUES (" + info.spawn_id + ", " + info.slot_id + ", " + info.equip_type + ", " + info.red + ", " + info.green + ", " + info.blue + ", " + info.highlight_red + ", " + info.highlight_green + ", " + info.highlight_blue+ ")");
                if (rows <= 0)
                    return false;
            }
            return true;
        }

        private bool SaveObjectInfo(int new_spawn_id)
        {
            bool success = false;
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, spawn_id, device_id  " +
                                                       "FROM spawn_objects " +
                                                       "WHERE spawn_objects.spawn_id=" + spawn_id);
            if (reader != null)
            {
                if (reader.Read())
                {
                    string id = reader.GetString(0);
                    string device_id = reader.GetString(2);
                   
                    reader.Close();

                    int rows = db.RunQuery("INSERT INTO spawn_objects (spawn_id, device_id) " +
                                           "VALUES (" + new_spawn_id + ", " + device_id + ")");
                    if (rows > 0)
                        success = true;
                }
                reader.Close();
            }
            return success;
        }

        private void button_cancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}