using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace TabControl_Spawns {
    public partial class Page_ServerDetails : UserControl {
        private const int NUM_THREADS = 7;
        private const int MAX_NUM_HIGHEST_LEVEL_CHARACTERS = 10;
        private MySqlEngine db;
        private TabPage owner;
        private Label[] labels_highestlevelcharacters_names;
        private Label[] labels_highestlevelcharacters_levels;

        public Page_ServerDetails(MySqlConnection connection, ref TabPage owner) {
            InitializeComponent();
            db = new MySqlEngine(connection);
            this.owner = owner;
            labels_highestlevelcharacters_names = new Label[MAX_NUM_HIGHEST_LEVEL_CHARACTERS];
            labels_highestlevelcharacters_levels = new Label[MAX_NUM_HIGHEST_LEVEL_CHARACTERS];

            InitializeHighestLevelCharactersLabels();
        }

        private void InitializeHighestLevelCharactersLabels() {
            labels_highestlevelcharacters_names[0] = label_highestlevelcharacters_0;
            labels_highestlevelcharacters_names[1] = label_highestlevelcharacters_1;
            labels_highestlevelcharacters_names[2] = label_highestlevelcharacters_2;
            labels_highestlevelcharacters_names[3] = label_highestlevelcharacters_3;
            labels_highestlevelcharacters_names[4] = label_highestlevelcharacters_4;
            labels_highestlevelcharacters_names[5] = label_highestlevelcharacters_5;
            labels_highestlevelcharacters_names[6] = label_highestlevelcharacters_6;
            labels_highestlevelcharacters_names[7] = label_highestlevelcharacters_7;
            labels_highestlevelcharacters_names[8] = label_highestlevelcharacters_8;
            labels_highestlevelcharacters_names[9] = label_highestlevelcharacters_9;

            labels_highestlevelcharacters_levels[0] = label_highestlevelcharacters_level0;
            labels_highestlevelcharacters_levels[1] = label_highestlevelcharacters_level1;
            labels_highestlevelcharacters_levels[2] = label_highestlevelcharacters_level2;
            labels_highestlevelcharacters_levels[3] = label_highestlevelcharacters_level3;
            labels_highestlevelcharacters_levels[4] = label_highestlevelcharacters_level4;
            labels_highestlevelcharacters_levels[5] = label_highestlevelcharacters_level5;
            labels_highestlevelcharacters_levels[6] = label_highestlevelcharacters_level6;
            labels_highestlevelcharacters_levels[7] = label_highestlevelcharacters_level7;
            labels_highestlevelcharacters_levels[8] = label_highestlevelcharacters_level8;
            labels_highestlevelcharacters_levels[9] = label_highestlevelcharacters_level9;
        }

        private void Page_ServerDetails_Load(object sender, EventArgs e) {
            new Thread(new ThreadStart(this.ThreadStartLoadItems)).Start();
            new Thread(new ThreadStart(this.ThreadStartLoadSpawns)).Start();
            new Thread(new ThreadStart(this.ThreadStartLoadQuests)).Start();
            new Thread(new ThreadStart(this.ThreadStartLoadSpells)).Start();
            new Thread(new ThreadStart(this.ThreadStartLoadZones)).Start();
            new Thread(new ThreadStart(this.ThreadStartLoadScripts)).Start();
            new Thread(new ThreadStart(this.ThreadStartLoadHighestLevelCharacters)).Start();
        }
        
        private void ThreadStartLoadItems() {
            this.BeginInvoke(new MethodInvoker(this.LoadItems));
        }

        private void ThreadStartLoadSpawns() {
            this.BeginInvoke(new MethodInvoker(this.LoadSpawns));
        }

        private void ThreadStartLoadQuests() {
            this.BeginInvoke(new MethodInvoker(this.LoadQuests));
        }

        private void ThreadStartLoadSpells() {
            this.BeginInvoke(new MethodInvoker(this.LoadSpells));
        }

        private void ThreadStartLoadZones() {
            this.BeginInvoke(new MethodInvoker(this.LoadZones));
        }

        private void ThreadStartLoadScripts() {
            this.BeginInvoke(new MethodInvoker(this.LoadScripts));
        }

        private void ThreadStartLoadHighestLevelCharacters() {
            this.BeginInvoke(new MethodInvoker(this.LoadHighestLevelCharacters));
        }

        private void LoadItems() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT COUNT(*) FROM items");
            if (reader != null) {
                if (reader.Read())
                    label_items_total.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Normal'");
            if (reader != null) {
                if (reader.Read())
                    label_items_normal.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Armor'");
            if (reader != null) {
                if (reader.Read())
                    label_items_armor.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Armor Set'");
            if (reader != null) {
                if (reader.Read())
                    label_items_armorsets.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Bag'");
            if (reader != null) {
                if (reader.Read())
                    label_items_bags.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Bauble'");
            if (reader != null) {
                if (reader.Read())
                    label_items_baubles.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Food'");
            if (reader != null) {
                if (reader.Read())
                    label_items_food.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='House'");
            if (reader != null) {
                if (reader.Read())
                    label_items_house.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='House Container'");
            if (reader != null) {
                if (reader.Read())
                    label_items_housecontainers.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Pattern'");
            if (reader != null) {
                if (reader.Read())
                    label_items_patterns.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Ranged'");
            if (reader != null) {
                if (reader.Read())
                    label_items_ranged.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Recipe'");
            if (reader != null) {
                if (reader.Read())
                    label_items_recipes.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Shield'");
            if (reader != null) {
                if (reader.Read())
                    label_items_shields.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Weapon'");
            if (reader != null) {
                if (reader.Read())
                    label_items_weapons.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Spell'");
            if (reader != null) {
                if (reader.Read())
                    label_items_spells.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Book'");
            if (reader != null) {
                if (reader.Read())
                    label_items_books.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Thrown'");
            if (reader != null) {
                if (reader.Read())
                    label_items_thrown.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM items WHERE item_type='Adornment'");
            if (reader != null) {
                if (reader.Read())
                    label_items_adornments.Text = reader.GetString(0);
                reader.Close();
            }
        }

        private void LoadSpawns() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn");
            if (reader != null) {
                if (reader.Read())
                    label_spawns_total.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn_npcs");
            if (reader != null) {
                if (reader.Read())
                    label_spawns_npcs.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn_objects");
            if (reader != null) {
                if (reader.Read())
                    label_spawns_objects.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn_widgets");
            if (reader != null) {
                if (reader.Read())
                    label_spawns_widgets.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn_signs");
            if (reader != null) {
                if (reader.Read())
                    label_spawns_signs.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn_ground");
            if (reader != null) {
                if (reader.Read())
                    label_spawns_ground.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn");
            if (reader != null) {
                if (reader.Read())
                    label_spawns_inzones.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn WHERE `merchant_id`>0");
            if (reader != null) {
                if (reader.Read())
                    label_spawns_merchants.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn WHERE `transport_id`>0");
            if (reader != null) {
                if (reader.Read())
                    label_spawns_transporters.Text = reader.GetString(0);
                reader.Close();
            }
        }

        private void LoadQuests() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT COUNT(*) FROM quests");
            if (reader != null) {
                if (reader.Read())
                    label_quests_total.Text = reader.GetString(0);
                reader.Close();
            }
        }

        private void LoadSpells() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT COUNT(*) FROM spells");
            if (reader != null) {
                if (reader.Read())
                    label_spells_total.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spells WHERE `spell_book_type`=0");
            if (reader != null) {
                if (reader.Read())
                    label_spells_spells.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spells WHERE `spell_book_type`=1");
            if (reader != null) {
                if (reader.Read())
                    label_spells_combatarts.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spells WHERE `spell_book_type`=2");
            if (reader != null) {
                if (reader.Read())
                    label_spells_abilities.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spells WHERE `spell_book_type`=3");
            if (reader != null) {
                if (reader.Read())
                    label_spells_tradeskills.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spells WHERE `spell_book_type`=4");
            if (reader != null) {
                if (reader.Read())
                    label_spells_notshown.Text = reader.GetString(0);
                reader.Close();
            }
        }

        private void LoadZones() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT COUNT(*) FROM zones");
            if (reader != null) {
                if (reader.Read())
                    label_zones_total.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM revive_points");
            if (reader != null) {
                if (reader.Read())
                    label_zones_revivepoints.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM zones WHERE `city_zone`>0");
            if (reader != null) {
                if (reader.Read())
                    label_zones_cityzones.Text = reader.GetString(0);
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM zones WHERE `always_loaded`>0");
            if (reader != null) {
                if (reader.Read())
                    label_zones_alwaysloaded.Text = reader.GetString(0);
                reader.Close();
            }
        }

        private void LoadScripts() {
            int num_scripts = 0;

            MySqlDataReader reader = db.RunSelectQuery("SELECT COUNT(*) FROM spawn_scripts");
            if (reader != null) {
                if (reader.Read()) {
                    label_scripts_spawnscripts.Text = reader.GetString(0);
                    num_scripts += reader.GetInt32(0);
                }
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM quests");
            if (reader != null) {
                if (reader.Read()) {
                    label_scripts_questscripts.Text = reader.GetString(0);
                    num_scripts += reader.GetInt32(0);
                }
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM spells WHERE NOT `lua_script`=''");
            if (reader != null) {
                if (reader.Read()) {
                    label_scripts_spellscripts.Text = reader.GetString(0);
                    num_scripts += reader.GetInt32(0);
                }
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT COUNT(*) FROM zones WHERE NOT `lua_script`=''");
            if (reader != null) {
                if (reader.Read()) {
                    label_scripts_zonescripts.Text = reader.GetString(0);
                    num_scripts += reader.GetInt32(0);
                }
                reader.Close();
            }

            label_scripts_total.Text = num_scripts.ToString();
        }

        private void LoadHighestLevelCharacters() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT `name`, `level` " +
                                       "FROM characters " +
                                       "ORDER BY level DESC");
            if (reader != null) {
                for (int i = 0; i < MAX_NUM_HIGHEST_LEVEL_CHARACTERS; i++) {
                    if (reader.Read()) {
                        labels_highestlevelcharacters_names[i].Text = reader.GetString(0);
                        labels_highestlevelcharacters_levels[i].Text = "Level: " + reader.GetString(1);
                    }
                    else
                        break;
                }
                reader.Close();
            }
        }

        private void button_close_Click(object sender, EventArgs e) {
            owner.Dispose();
        }
    }
}
