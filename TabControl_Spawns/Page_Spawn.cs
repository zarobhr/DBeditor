using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data.OleDb;
using System.Data;

namespace TabControl_Spawns
{
    public partial class Page_Spawn : UserControl {
        private TabPage owner;
        private MySqlEngine db;
        private MySqlConnection connection;
        private ArrayList entity_commands;
        private ArrayList races;
        private ArrayList classes;
        private ArrayList genders;
        private ArrayList attack_types;
        private ArrayList appearance_equip_slots;
        private ArrayList appearance_types;
        private ArrayList hair_types;
        private ArrayList hair_face_types;
        private ArrayList chest_types;
        private ArrayList legs_types;
        private ArrayList wing_types;
        private ArrayList enc_levels;
        private ArrayList factions;
        private ArrayList spawn_entries;
        private ArrayList spawn_groups;
        public Page_Spawn(MySqlConnection connection, ref TabPage owner) {
            InitializeComponent();
            this.connection = connection;
            this.owner = owner;
            db = new MySqlEngine(this.connection);
            entity_commands = new ArrayList();
            races = new ArrayList();
            classes = new ArrayList();
            genders = new ArrayList();
            attack_types = new ArrayList();
            appearance_equip_slots = new ArrayList();
            appearance_types = new ArrayList();
            hair_types = new ArrayList();
            hair_face_types = new ArrayList();
            chest_types = new ArrayList();
            legs_types = new ArrayList();
            wing_types = new ArrayList();
            enc_levels = new ArrayList();
            factions = new ArrayList();
            spawn_entries = new ArrayList();
            spawn_groups = new ArrayList();

            InitializeRaces();
            InitializeClasses();
            InitializeGenders();
            InitializeAttackTypes();
            InitializeAppearanceEquipSlots();
            InitializeAppearanceTypes();
            InitializeHairTypes();
            InitializeHairFaceTypes();
            InitializeChestTypes();
            InitializeLegsTypes();
            InitializeWingTypes();
            InitializeEncounterLevels();

        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
        }

        private void Page_Spawn_Load(object sender, EventArgs e) {
            string SQL = "SELECT z.id, name, description "+
                         "FROM zones z ";
            
         //   if (Settings.DisplayUnpopulatedZones==0)
             //   SQL+="INNER JOIN "+
                 //    "spawn_location_placement p ON p.zone_id=z.id "+
                  //   "GROUP BY z.id";

            MySqlDataReader reader = db.RunSelectQuery(SQL);
            if (reader != null) {
                comboBox_select_zone.Items.Clear();
                while (reader.Read())
                    comboBox_select_zone.Items.Add(reader.GetString(2) + "   (" + reader.GetString(1) + ")");
                reader.Close();
            }
            LoadLastSettings();
        }

        private void LoadLastSettings()
        {
            comboBox_select_zone.SelectedItem = Properties.Settings.Default.LastZone;
            comboBox_select_type.SelectedItem = Properties.Settings.Default.LastType;
            comboBox_select_spawn.SelectedItem = Properties.Settings.Default.LastSpawn;
        }

        private void comboBox_select_zone_SelectedIndexChanged(object sender, EventArgs e) {
            label_select_type.Visible = true;
            comboBox_select_type.SelectedItem = null;
            comboBox_select_type.Visible = true;
            
            label_select_spawn.Visible = false;
            comboBox_select_spawn.Visible = false;

            owner.Text = "Spawn: <none>";
            tabControl_loot.Visible = false;
            button_duplicatespawn.Enabled = false;
            button_refresh.Enabled = false;

            Properties.Settings.Default.LastZone = (string)comboBox_select_zone.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private void comboBox_select_type_SelectedIndexChanged(object sender, EventArgs e) {
            if (PopulateSpawnsComboBox()) {
                label_select_spawn.Visible = true;
                comboBox_select_spawn.Visible = true;
            }
            owner.Text = "Spawn: <none>";
            tabControl_loot.Visible = false;
            button_duplicatespawn.Enabled = false;
            button_refresh.Enabled = false;

            Properties.Settings.Default.LastType = (string)comboBox_select_type.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private void comboBox_select_spawn_SelectedIndexChanged(object sender, EventArgs e) {
            int spawn_id = GetSelectedSpawnID();
            if (spawn_id == -1)
                return;

            string type = (string)comboBox_select_type.SelectedItem;

            ResetSpawn();
            ResetNPC();
            ResetNPCAppearance(true);
            ResetNPCAppearanceEquip(true);
            ResetSign();
            ResetWidget();
            ResetGround(true, "all");
            ResetSpawnScripts(true);
            ResetZoneSpawnEntry(true);
            ResetZoneSpawns(true);
            ResetMerchant(true, "all");
            ResetLootTableEntry(true);
            ResetLootDropEntry(true);

            InitializeEntityCommands();
            InitializeFactions();
            InitializeSpawnEntriesAndGroups();
            LoadSpawn(spawn_id);
            LoadSpawnLocationEntry(spawn_id);
            LoadSpawnLocationPlacement(spawn_id);
            LoadSpawnScripts(spawn_id);
            LoadLootTableEntry(spawn_id);

            //Remove not needed tabs.
            if (!type.Equals("NPC")) {
                tabControl_loot.TabPages.Remove(tabPage_npc);
                tabControl_loot.TabPages.Remove(tabPage_npc_appearance);
                tabControl_loot.TabPages.Remove(tabPage_npc_appearance_equip);
            }
            if (!type.Equals("Object"))
                tabControl_loot.TabPages.Remove(tabPage_object);
            if (!type.Equals("Sign"))
                tabControl_loot.TabPages.Remove(tabPage_sign);
            if (!type.Equals("Widget"))
                tabControl_loot.TabPages.Remove(tabPage_widget);
            if (!type.Equals("Ground"))
                tabControl_loot.TabPages.Remove(tabPage_ground);

            //Load needed tabs and information.
            if (type.Equals("NPC")) {
                if (!tabControl_loot.TabPages.Contains(tabPage_npc)){
                    tabControl_loot.TabPages.Insert(1, tabPage_npc);
                    tabControl_loot.TabPages.Insert(2, tabPage_npc_appearance);
                    tabControl_loot.TabPages.Insert(3, tabPage_npc_appearance_equip);
                }
                LoadNPC(spawn_id);
                LoadNPCAppearance(spawn_id);
                LoadNPCAppearanceEquip(spawn_id);
                int merchant_id = GetMerchantID(spawn_id);
                if (merchant_id > 0)
                    LoadMerchant(merchant_id);
            }
            else if (type.Equals("Object")) {
                if (!tabControl_loot.TabPages.Contains(tabPage_object))
                    tabControl_loot.TabPages.Insert(1, tabPage_object);
                LoadObject(spawn_id);
            }
            else if (type.Equals("Sign")) {
                if (!tabControl_loot.TabPages.Contains(tabPage_sign))
                    tabControl_loot.TabPages.Insert(1, tabPage_sign);
                LoadSign(spawn_id);
            }
            else if (type.Equals("Widget")) {
                if (!tabControl_loot.TabPages.Contains(tabPage_widget))
                    tabControl_loot.TabPages.Insert(1, tabPage_widget);
                LoadWidget(spawn_id);
            }
            else if (type.Equals("Ground")) {
                if (!tabControl_loot.TabPages.Contains(tabPage_ground))
                    tabControl_loot.TabPages.Insert(1, tabPage_ground);
                LoadGround(spawn_id);
            }

            tabControl_loot.Visible = true;
            button_duplicatespawn.Enabled = true;
            button_refresh.Enabled = true;

            Properties.Settings.Default.LastSpawn = (string)comboBox_select_spawn.SelectedItem;
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

        private string GetSelectedZoneName() {
            string search_for = "   (";
            int index = comboBox_select_zone.SelectedIndex;
            if (index == -1)
                return null;
            string zone_name = (string)comboBox_select_zone.Items[index];
            zone_name = zone_name.Substring(zone_name.IndexOf(search_for) + search_for.Length);
            zone_name = zone_name.Remove(zone_name.Length - 1);
            return zone_name;
        }

        private string RemoveSpacesAndEscape(string str) {
            str = str.Replace(" ", "");
            str = db.RemoveEscapeCharacters(str);
            return str;
        }

        private int GetSelectedSpawnID() {
            if (comboBox_select_spawn.SelectedItem == null)
                return -1;

            string search_for = "   (";
            string spawn_name = (string)comboBox_select_spawn.SelectedItem;
            spawn_name = spawn_name.Substring(spawn_name.IndexOf(search_for) + search_for.Length);
            spawn_name = spawn_name.Remove(spawn_name.Length - 1);
            return Convert.ToInt32(spawn_name);
        }

        private string GetSelectedSpawnName() {
            string search_for = "   (";
            int index = comboBox_select_spawn.SelectedIndex;
            if (index == -1)
                return null;
            string spawn_name = (string)comboBox_select_spawn.Items[index];
            spawn_name = spawn_name.Substring(0, spawn_name.IndexOf(search_for));
            return spawn_name;
        }

        private bool PopulateSpawnsComboBox() {
            if (comboBox_select_type.SelectedItem == null || GetSelectedZoneID() == -1)
                return false;

            string spawn_type_table = null;
            string type = (string)comboBox_select_type.SelectedItem;
            if (type.Equals("NPC"))
                spawn_type_table = "spawn_npcs";
            else if (type.Equals("Object"))
                spawn_type_table = "spawn_objects";
            else if (type.Equals("Widget"))
                spawn_type_table = "spawn_widgets";
            else if (type.Equals("Sign"))
                spawn_type_table = "spawn_signs";
            else if (type.Equals("Ground"))
                spawn_type_table = "spawn_ground";

            if (spawn_type_table == null)
                return false;
            
            MySqlDataReader reader = db.RunSelectQuery("SELECT s.id AS spawn_id, s.name " +
                                                       "FROM spawn s " +
                                                       "INNER JOIN " +
                                                       spawn_type_table + " t ON s.id=t.spawn_id " +
                                                       "WHERE s.id LIKE '" + GetSelectedZoneID() + "____' " +
                                                       "GROUP BY s.id");
            
            if (reader != null) {
                comboBox_select_spawn.Items.Clear();
                while (reader.Read())
                    comboBox_select_spawn.Items.Add(reader.GetString(1) + "   (" + reader.GetString(0) + ")");
                reader.Close();
                return true;
            }
            return false;
        }

        private void InitializeEntityCommands() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT command_list_id, command_text " +
                                                       "FROM entity_commands");
            if (reader != null) {
                entity_commands.Clear();
                comboBox_spawn_commandprimary.Items.Clear();
                comboBox_spawn_commandsecondary.Items.Clear();
                comboBox_spawn_commandprimary.Items.Add("");
                comboBox_spawn_commandsecondary.Items.Add("");
                entity_commands.Add(new EntityCommand(0, ""));
                while (reader.Read()) {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int index = FindEntityCommandWithSameID(id);
                    if (index != -1)
                         ((EntityCommand)entity_commands[index]).entity_command += ", " + name;
                    else
                        entity_commands.Add(new EntityCommand(id, name));
                }
                reader.Close();
                for (int i = 0; i < entity_commands.Count; i++) {
                    EntityCommand e_command = (EntityCommand)entity_commands[i];
                    comboBox_spawn_commandprimary.Items.Add(e_command.entity_command);
                    comboBox_spawn_commandsecondary.Items.Add(e_command.entity_command);
                }
            }
        }

        private int FindEntityCommandWithSameID(int id) {
            for (int i = 0; i < entity_commands.Count; i++)
                if (((EntityCommand)entity_commands[i]).id == id)
                    return i;
            return -1;
        }

        private int GetEntityCommandID(string entity_command) {
            for (int i = 0; i < entity_commands.Count; i++) {
                EntityCommand type = (EntityCommand)entity_commands[i];
                if (entity_command == type.entity_command)
                    return type.id;
            }
            return -1;
        }

        private string GetEntityCommandName(int id) {
            for (int i = 0; i < entity_commands.Count; i++) {
                EntityCommand type = (EntityCommand)entity_commands[i];
                if (id == type.id)
                    return type.entity_command;
            }
            return null;
        }

        private void InitializeRaces() {
            races.Add(new Race(0, "Barbarian"));
            races.Add(new Race(1, "Dark Elf"));
            races.Add(new Race(2, "Dwarf"));
            races.Add(new Race(3, "Erudite"));
            races.Add(new Race(4, "Froglok"));
            races.Add(new Race(5, "Gnome"));
            races.Add(new Race(6, "Half Elf"));
            races.Add(new Race(7, "Halfling"));
            races.Add(new Race(8, "High Elf"));
            races.Add(new Race(9, "Human"));
            races.Add(new Race(10, "Iksar"));
            races.Add(new Race(11, "Kerra"));
            races.Add(new Race(12, "Ogre"));
            races.Add(new Race(13, "Ratonga"));
            races.Add(new Race(14, "Troll"));
            races.Add(new Race(15, "Wood Elf"));
            races.Add(new Race(16, "Fae"));
            races.Add(new Race(17, "Arasai"));
            races.Add(new Race(18, "Sarnak"));
            races.Add(new Race(255, "None"));
            comboBox_spawn_race.Items.Clear();
            for (int i = 0; i < races.Count; i++)
                comboBox_spawn_race.Items.Add(((Race)races[i]).race);
        }

        private int GetRaceID(string race) {
            for (int i = 0; i < races.Count; i++) {
                Race type = (Race)races[i];
                if (race == type.race)
                    return type.id;
            }
            return -1;
        }

        private string GetRaceName(int id) {
            for (int i = 0; i < races.Count; i++) {
                Race type = (Race)races[i];
                if (id == type.id)
                    return type.race;
            }
            return null;
        }

        private void InitializeClasses() {
            if (classes.Count == 0) {
                classes.Add(new Class(0, "Commoner"));
                classes.Add(new Class(1, "Fighter"));
                classes.Add(new Class(2, "Warrior"));
                classes.Add(new Class(3, "Guardian"));
                classes.Add(new Class(4, "Berserker"));
                classes.Add(new Class(5, "Brawler"));
                classes.Add(new Class(6, "Monk"));
                classes.Add(new Class(7, "Bruiser"));
                classes.Add(new Class(8, "Crusader"));
                classes.Add(new Class(9, "Shadowknight"));
                classes.Add(new Class(10, "Paladin"));
                classes.Add(new Class(11, "Priest"));
                classes.Add(new Class(12, "Cleric"));
                classes.Add(new Class(13, "Templar"));
                classes.Add(new Class(14, "Inquisitor"));
                classes.Add(new Class(15, "Druid"));
                classes.Add(new Class(16, "Warden"));
                classes.Add(new Class(17, "Fury"));
                classes.Add(new Class(18, "Shaman"));
                classes.Add(new Class(19, "Mystic"));
                classes.Add(new Class(20, "Defiler"));
                classes.Add(new Class(21, "Mage"));
                classes.Add(new Class(22, "Sorcerer"));
                classes.Add(new Class(23, "Wizard"));
                classes.Add(new Class(24, "Warlock"));
                classes.Add(new Class(25, "Enchenter"));
                classes.Add(new Class(26, "Illusionist"));
                classes.Add(new Class(27, "Coercer"));
                classes.Add(new Class(28, "Summoner"));
                classes.Add(new Class(29, "Conjuror"));
                classes.Add(new Class(30, "Necromancer"));
                classes.Add(new Class(31, "Scout"));
                classes.Add(new Class(32, "Rogue"));
                classes.Add(new Class(33, "Swashbuckler"));
                classes.Add(new Class(34, "Brigand"));
                classes.Add(new Class(35, "Bard"));
                classes.Add(new Class(36, "Troubador"));
                classes.Add(new Class(37, "Dirge"));
                classes.Add(new Class(38, "Predator"));
                classes.Add(new Class(39, "Ranger"));
                classes.Add(new Class(40, "Assasin"));
                classes.Add(new Class(255, "All"));
            }
            for (int i = 0; i < classes.Count; i++)
                comboBox_spawn_npc_class.Items.Add(((Class)classes[i]).class_name);
        }

        private int GetClassID(string class_name) {
            for (int i = 0; i < classes.Count; i++) {
                Class class_ = (Class)classes[i];
                if (class_name == class_.class_name)
                    return class_.id;
            }
            return -1;
        }

        private string GetClassName(int class_id) {
            for (int i = 0; i < classes.Count; i++) {
                Class class_ = (Class)classes[i];
                if (class_id == class_.id)
                    return class_.class_name;
            }
            return null;
        }

        private void InitializeGenders() {
            if (genders.Count == 0) {
                genders.Add(new Gender(0, "None"));
                genders.Add(new Gender(1, "Male"));
                genders.Add(new Gender(2, "Female"));
            }
            for (int i = 0; i < genders.Count; i++)
                comboBox_spawn_npc_gender.Items.Add(((Gender)genders[i]).gender);
        }

        private int GetGenderID(string gender) {
            for (int i = 0; i < genders.Count; i++) {
                Gender type = (Gender)genders[i];
                if (gender == type.gender)
                    return type.id;
            }
            return -1;
        }

        private string GetGenderName(int id) {
            for (int i = 0; i < genders.Count; i++) {
                Gender type = (Gender)genders[i];
                if (id == type.id)
                    return type.gender;
            }
            return null;
        }

        private void InitializeAttackTypes()
        {
            if (attack_types.Count == 0)
            {
                attack_types.Add(new AttackType(0, "Slashing"));
                attack_types.Add(new AttackType(1, "Crushing"));
                attack_types.Add(new AttackType(2, "Piercing"));
                attack_types.Add(new AttackType(3, "Heat"));
                attack_types.Add(new AttackType(4, "Cold"));
                attack_types.Add(new AttackType(5, "Magic"));
                attack_types.Add(new AttackType(6, "Mental"));
                attack_types.Add(new AttackType(7, "Divine"));
                attack_types.Add(new AttackType(8, "Disease"));
                attack_types.Add(new AttackType(9, "Poison"));
                attack_types.Add(new AttackType(10, "Drowning Damage"));
                attack_types.Add(new AttackType(11, "Falling Damage"));
            }
            for (int i = 0; i < attack_types.Count; i++)
                comboBox_npc_attack_type.Items.Add(((AttackType)attack_types[i]).attack_type);
        }

        private int GetAttackTypeID(string attack_type)
        {
            for (int i = 0; i < attack_types.Count; i++)
            {
                AttackType type = (AttackType)attack_types[i];
                if (attack_type == type.attack_type)
                    return type.id;
            }
            return -1;
        }

        private string GetAttackTypeName(int id)
        {
            for (int i = 0; i < attack_types.Count; i++)
            {
                AttackType type = (AttackType)attack_types[i];
                if (id == type.id)
                    return type.attack_type;
            }
            return null;
        }

        private void InitializeAppearanceEquipSlots() {
            if (appearance_equip_slots.Count == 0) {
                appearance_equip_slots.Add(new AppearanceEquipSlot(0, "Weapon Primary"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(1, "Weapon Secondary"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(2, "Head"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(3, "Chest"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(4, "Shoulders"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(5, "Forearms"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(6, "Hands"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(7, "Legs"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(8, "Feet"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(9, "Ring Left"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(10, "Ring Right"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(11, "Ear Left"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(12, "Ear Right"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(13, "Neck"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(14, "Wrist Left"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(15, "Wrist Right"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(16, "Range"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(17, "Ammo"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(18, "Waist"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(19, "Cloak"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(20, "Charm 1"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(21, "Charm 2"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(22, "Food"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(23, "Drink"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(24, "Texture"));
                appearance_equip_slots.Add(new AppearanceEquipSlot(25, "Unknown"));
            }
            for (int i = 0; i < appearance_equip_slots.Count; i++)
                comboBox_npcappearanceequip_slotid.Items.Add(((AppearanceEquipSlot)appearance_equip_slots[i]).slot);
        }

        private int GetAppearanceEquipSlotID(string slot) {
            for (int i = 0; i < appearance_equip_slots.Count; i++) {
                AppearanceEquipSlot type = (AppearanceEquipSlot)appearance_equip_slots[i];
                if (slot == type.slot)
                    return type.id;
            }
            return -1;
        }

        private string GetAppearanceEquipSlotName(int id) {
            for (int i = 0; i < appearance_equip_slots.Count; i++) {
                AppearanceEquipSlot type = (AppearanceEquipSlot)appearance_equip_slots[i];
                if (id == type.id)
                    return type.slot;
            }
            return null;
        }

        private void InitializeAppearanceTypes() {
            if (appearance_types.Count == 0) {
                appearance_types.Add(new AppearanceType("skin_color", "Skin Color"));
                appearance_types.Add(new AppearanceType("eye_color", "Eye Color"));
                appearance_types.Add(new AppearanceType("hair_color1", "Hair Color 1"));
                appearance_types.Add(new AppearanceType("hair_color2", "Hair Color 2"));
                appearance_types.Add(new AppearanceType("hair_highlight", "Hair Highlight"));
                appearance_types.Add(new AppearanceType("hair_type_color", "Hair Type Color"));
                appearance_types.Add(new AppearanceType("hair_type_highlight_color", "Hair Type Highlight Color"));
                appearance_types.Add(new AppearanceType("hair_face_color", "Hair Face Color"));
                appearance_types.Add(new AppearanceType("hair_face_highlight_color", "Hair Face Highlight Color"));
                appearance_types.Add(new AppearanceType("wing_color1", "Wing Color 1"));
                appearance_types.Add(new AppearanceType("wing_color2", "Wing Color 2"));
                appearance_types.Add(new AppearanceType("shirt_color", "Shirt Color"));
                appearance_types.Add(new AppearanceType("unknown_chest_color", "Unknown Chest Color"));
                appearance_types.Add(new AppearanceType("pants_color", "Pants Color"));
                appearance_types.Add(new AppearanceType("unknown_legs_color", "Unknown Legs Color"));
                appearance_types.Add(new AppearanceType("unknown9", "Unknown9"));
                appearance_types.Add(new AppearanceType("eye_type", "Eye Type"));
                appearance_types.Add(new AppearanceType("ear_type", "Ear Type"));
                appearance_types.Add(new AppearanceType("eye_brow_type", "Eye Brow Type"));
                appearance_types.Add(new AppearanceType("cheek_type", "Cheek Type"));
                appearance_types.Add(new AppearanceType("lip_type", "Lip Type"));
                appearance_types.Add(new AppearanceType("chin_type", "Chin Type"));
                appearance_types.Add(new AppearanceType("nose_type", "Nose Type"));
                appearance_types.Add(new AppearanceType("body_size", "Body Size"));
                appearance_types.Add(new AppearanceType("soga_skin_color", "Soga Skin Color"));
                appearance_types.Add(new AppearanceType("soga_eye_color", "Soga Eye Color"));
                appearance_types.Add(new AppearanceType("soga_hair_color1", "Soga Hair Color 1"));
                appearance_types.Add(new AppearanceType("soga_hair_color2", "Soga Hair Color 2"));
                appearance_types.Add(new AppearanceType("soga_hair_highlight", "Soga Hair Highlight"));
                appearance_types.Add(new AppearanceType("soga_hair_type_color", "Soga Hair Type Color"));
                appearance_types.Add(new AppearanceType("soga_hair_type_highlight_color", "Soga Hair Type Highlight Color"));
                appearance_types.Add(new AppearanceType("soga_hair_face_color", "Soga Hair Face Color"));
                appearance_types.Add(new AppearanceType("soga_hair_face_highlight_color", "Soga Hair Face Highlight Color"));
                appearance_types.Add(new AppearanceType("soga_wing_color1", "Soga Wing Color 1"));
                appearance_types.Add(new AppearanceType("soga_wing_color2", "Soga Wing Color 2"));
                appearance_types.Add(new AppearanceType("soga_shirt_color", "Soga Shirt Color"));
                appearance_types.Add(new AppearanceType("soga_unknown_chest_color", "Soga Unknown Chest Color"));
                appearance_types.Add(new AppearanceType("soga_pants_color", "Soga Pants Color"));
                appearance_types.Add(new AppearanceType("soga_unknown_legs_color", "Soga Unknown Legs Color"));
                appearance_types.Add(new AppearanceType("soga_unknown13", "Soga Unknown13"));
                appearance_types.Add(new AppearanceType("soga_eye_type", "Soga Eye Type"));
                appearance_types.Add(new AppearanceType("soga_ear_type", "Soga Ear Type"));
                appearance_types.Add(new AppearanceType("soga_eye_brow_type", "Soga Eye Brow Type"));
                appearance_types.Add(new AppearanceType("soga_cheek_type", "Soga Cheek Type"));
                appearance_types.Add(new AppearanceType("soga_lip_type", "Soga Lip Type"));
                appearance_types.Add(new AppearanceType("soga_chin_type", "Soga Chin Type"));
                appearance_types.Add(new AppearanceType("soga_nose_type", "Soga Nose Type"));
            }
            for (int i = 0; i < appearance_types.Count; i++)
                comboBox_npcappearance_type.Items.Add(((AppearanceType)appearance_types[i]).type);
        }

        private string GetAppearanceTypeID(string type) {
            for (int i = 0; i < appearance_types.Count; i++) {
                AppearanceType type_ = (AppearanceType)appearance_types[i];
                if (type == type_.type)
                    return type_.id;
            }
            return null;
        }

        private string GetAppearanceTypeName(string id) {
            for (int i = 0; i < appearance_types.Count; i++) {
                AppearanceType type = (AppearanceType)appearance_types[i];
                if (id == type.id)
                    return type.type;
            }
            return null;
        }

        private void InitializeHairTypes() {
            if (hair_types.Count == 0) {
                hair_types.Add(new Hair(0, "None"));
                hair_types.Add(new Hair(1113, "Hair000"));
                hair_types.Add(new Hair(1133, "Hair001"));
                hair_types.Add(new Hair(1136, "Hair002"));
                hair_types.Add(new Hair(1137, "Hair003"));
                hair_types.Add(new Hair(1138, "Hair004"));
                hair_types.Add(new Hair(1139, "Hair005"));
                hair_types.Add(new Hair(1140, "Hair006"));
                hair_types.Add(new Hair(1134, "Hair007"));
                hair_types.Add(new Hair(1135, "Hair008"));
                hair_types.Add(new Hair(1128, "Hair009"));
                hair_types.Add(new Hair(1127, "Hair010"));
                hair_types.Add(new Hair(1126, "Hair011"));
                hair_types.Add(new Hair(1125, "Hair012"));
                hair_types.Add(new Hair(1124, "Hair013"));
                hair_types.Add(new Hair(1129, "Hair014"));
                hair_types.Add(new Hair(1130, "Hair015"));
                hair_types.Add(new Hair(1131, "Hair016"));
                hair_types.Add(new Hair(1132, "Hair017"));
                hair_types.Add(new Hair(1115, "Hair018"));
                hair_types.Add(new Hair(1114, "Hair019"));
                hair_types.Add(new Hair(1116, "Hair020"));
                hair_types.Add(new Hair(1119, "Hair021"));
                hair_types.Add(new Hair(1120, "Hair022"));
                hair_types.Add(new Hair(1123, "Hair023"));
                hair_types.Add(new Hair(1121, "Hair024"));
                hair_types.Add(new Hair(1122, "Hair025"));
                hair_types.Add(new Hair(7352, "Hair026"));
            }
            for (int i = 0; i < hair_types.Count; i++) {
                comboBox_spawn_npc_hairtype.Items.Add(((Hair)hair_types[i]).hair);
                comboBox_spawn_npc_shairtype.Items.Add(((Hair)hair_types[i]).hair);
            }
        }

        private int GetHairTypeID(string hair_type) {
            for (int i = 0; i < hair_types.Count; i++) {
                Hair type = (Hair)hair_types[i];
                if (hair_type == type.hair)
                    return type.id;
            }
            return -1;
        }

        private string GetHairTypeName(int id) {
            for (int i = 0; i < hair_types.Count; i++) {
                Hair type = (Hair)hair_types[i];
                if (id == type.id)
                    return type.hair;
            }
            return null;
        }

        private void InitializeHairFaceTypes() {
            if (hair_face_types.Count == 0) {
                hair_face_types.Add(new HairFace(0, "None"));
                hair_face_types.Add(new HairFace(1184, "Face000"));
                hair_face_types.Add(new HairFace(1185, "Face001"));
                hair_face_types.Add(new HairFace(1186, "Face002"));
                hair_face_types.Add(new HairFace(1187, "Face003"));
                hair_face_types.Add(new HairFace(1188, "Face004"));
                hair_face_types.Add(new HairFace(1189, "Face005"));
                hair_face_types.Add(new HairFace(1190, "Face006"));
                hair_face_types.Add(new HairFace(1191, "Face007"));
                hair_face_types.Add(new HairFace(1164, "Face008"));
                hair_face_types.Add(new HairFace(1165, "Face009"));
                hair_face_types.Add(new HairFace(1166, "Face010"));
                hair_face_types.Add(new HairFace(1167, "Face011"));
                hair_face_types.Add(new HairFace(1168, "Face012"));
                hair_face_types.Add(new HairFace(1169, "Face013"));
                hair_face_types.Add(new HairFace(1170, "Face014"));
                hair_face_types.Add(new HairFace(1171, "Face015"));
                hair_face_types.Add(new HairFace(1172, "Face016"));
                hair_face_types.Add(new HairFace(1173, "Face017"));
                hair_face_types.Add(new HairFace(1174, "Face018"));
                hair_face_types.Add(new HairFace(1175, "Face019"));
                hair_face_types.Add(new HairFace(1176, "Face020"));
                hair_face_types.Add(new HairFace(1177, "Face021"));
                hair_face_types.Add(new HairFace(1178, "Face022"));
                hair_face_types.Add(new HairFace(1179, "Face023"));
                hair_face_types.Add(new HairFace(1180, "Face024"));
                hair_face_types.Add(new HairFace(1181, "Face025"));
                hair_face_types.Add(new HairFace(1182, "Face026"));
                hair_face_types.Add(new HairFace(1183, "Face027"));
            }
            for (int i = 0; i < hair_face_types.Count; i++) {
                comboBox_spawn_npc_facialhairtype.Items.Add(((HairFace)hair_face_types[i]).hair_face);
                comboBox_spawn_npc_sfacialhairtype.Items.Add(((HairFace)hair_face_types[i]).hair_face);
            }
        }

        private int GetHairFaceTypeID(string hair_face_type) {
            for (int i = 0; i < hair_face_types.Count; i++) {
                HairFace type = (HairFace)hair_face_types[i];
                if (hair_face_type == type.hair_face)
                    return type.id;
            }
            return -1;
        }

        private string GetHairFaceTypeName(int id) {
            for (int i = 0; i < hair_face_types.Count; i++) {
                HairFace type = (HairFace)hair_face_types[i];
                if (id == type.id)
                    return type.hair_face;
            }
            return null;
        }

        private void InitializeChestTypes() {
            if (chest_types.Count == 0) {
                chest_types.Add(new ChestType(0, "None"));
                chest_types.Add(new ChestType(7733, "Sarnak Female"));
                chest_types.Add(new ChestType(7731, "Sarnak Male"));
                chest_types.Add(new ChestType(5457, "Barbarian Female"));
                chest_types.Add(new ChestType(5461, "Barbarian Male"));
                chest_types.Add(new ChestType(5465, "Dark Elf Female"));
                chest_types.Add(new ChestType(5469, "Dark Elf Male"));
                chest_types.Add(new ChestType(5473, "Dwarf Female"));
                chest_types.Add(new ChestType(5477, "Dwarf Male"));
                chest_types.Add(new ChestType(5481, "Erudite Female"));
                chest_types.Add(new ChestType(5485, "Erudite Male"));
                chest_types.Add(new ChestType(5489, "Froglok Female"));
                chest_types.Add(new ChestType(5493, "Froglok Male"));
                chest_types.Add(new ChestType(5497, "Gnome Female"));
                chest_types.Add(new ChestType(5501, "Gnome Male"));
                chest_types.Add(new ChestType(5505, "Half Elf Female"));
                chest_types.Add(new ChestType(5509, "Half Elf Male"));
                chest_types.Add(new ChestType(5513, "Halfling Female"));
                chest_types.Add(new ChestType(5517, "Halfling Male"));
                chest_types.Add(new ChestType(5521, "High Elf Female"));
                chest_types.Add(new ChestType(5525, "High Elf Male"));
                chest_types.Add(new ChestType(5529, "Human Female"));
                chest_types.Add(new ChestType(5533, "Human Male"));
                chest_types.Add(new ChestType(5537, "Iksar Female"));
                chest_types.Add(new ChestType(5541, "Iksar Male"));
                chest_types.Add(new ChestType(5545, "Kerra Female"));
                chest_types.Add(new ChestType(5549, "Kerra Male"));
                chest_types.Add(new ChestType(5553, "Ogre Female"));
                chest_types.Add(new ChestType(5557, "Ogre Male"));
                chest_types.Add(new ChestType(5561, "Ratonga Female"));
                chest_types.Add(new ChestType(5565, "Troll Female"));
                chest_types.Add(new ChestType(5569, "Troll Male"));
                chest_types.Add(new ChestType(5573, "Wood Elf Female"));
                chest_types.Add(new ChestType(5577, "Wood Elf Male"));
                chest_types.Add(new ChestType(5725, "Ratonga Male"));
                chest_types.Add(new ChestType(6293, "Fae Male"));
                chest_types.Add(new ChestType(6297, "Fae Female"));
                chest_types.Add(new ChestType(7020, "Fae Dark Male"));
                chest_types.Add(new ChestType(7024, "Fae Dark Female"));
                chest_types.Add(new ChestType(7028, "Fae Light Male"));
                chest_types.Add(new ChestType(7032, "Fae Light Female"));

            }
            for (int i = 0; i < chest_types.Count; i++)
                comboBox_spawn_npc_chesttype.Items.Add(((ChestType)chest_types[i]).chest_type);
        }

        private int GetChestTypeID(string chest_type) {
            for (int i = 0; i < chest_types.Count; i++) {
                ChestType type = (ChestType)chest_types[i];
                if (chest_type == type.chest_type)
                    return type.id;
            }
            return -1;
        }

        private string GetChestTypeName(int id) {
            for (int i = 0; i < chest_types.Count; i++) {
                ChestType type = (ChestType)chest_types[i];
                if (id == type.id)
                    return type.chest_type;
            }
            return null;
        }

        private void InitializeLegsTypes() {
            if (legs_types.Count == 0) {
                legs_types.Add(new LegsType(0, "None"));
                legs_types.Add(new LegsType(7734, "Sarnak Female"));
                legs_types.Add(new LegsType(7732, "Sarnak Male"));
                legs_types.Add(new LegsType(5458, "Barbarian Female"));
                legs_types.Add(new LegsType(5462, "Barbarian Male"));
                legs_types.Add(new LegsType(5466, "Dark Elf Female"));
                legs_types.Add(new LegsType(5470, "Dark Elf Male"));
                legs_types.Add(new LegsType(5474, "Dwarf Female"));
                legs_types.Add(new LegsType(5478, "Dwarf Male"));
                legs_types.Add(new LegsType(5482, "Erudite Female"));
                legs_types.Add(new LegsType(5486, "Erudite Male"));
                legs_types.Add(new LegsType(5490, "Froglok Female"));
                legs_types.Add(new LegsType(5494, "Froglok Male"));
                legs_types.Add(new LegsType(5498, "Gnome Female"));
                legs_types.Add(new LegsType(5502, "Gnome Male"));
                legs_types.Add(new LegsType(5506, "Half Elf Female"));
                legs_types.Add(new LegsType(5510, "Half Elf Male"));
                legs_types.Add(new LegsType(5514, "Halfling Female"));
                legs_types.Add(new LegsType(5518, "Halfling Male"));
                legs_types.Add(new LegsType(5522, "High Elf Female"));
                legs_types.Add(new LegsType(5526, "High Elf Male"));
                legs_types.Add(new LegsType(5530, "Human Female"));
                legs_types.Add(new LegsType(5531, "Human Male"));
                legs_types.Add(new LegsType(5538, "Iksar Female"));
                legs_types.Add(new LegsType(5542, "Iksar Male"));
                legs_types.Add(new LegsType(5546, "Kerra Female"));
                legs_types.Add(new LegsType(5550, "Kerra Male"));
                legs_types.Add(new LegsType(5554, "Ogre Female"));
                legs_types.Add(new LegsType(5558, "Ogre Male"));
                legs_types.Add(new LegsType(5562, "Ratonga Female"));
                legs_types.Add(new LegsType(5566, "Troll Female"));
                legs_types.Add(new LegsType(5570, "Troll Male"));
                legs_types.Add(new LegsType(5574, "Wood Elf Female"));
                legs_types.Add(new LegsType(5578, "Wood Elf Male"));
                legs_types.Add(new LegsType(5726, "Ratonga Male"));
                legs_types.Add(new LegsType(6294, "Fae Male"));
                legs_types.Add(new LegsType(6298, "Fae Female"));
                legs_types.Add(new LegsType(7021, "Fae Dark Male"));
                legs_types.Add(new LegsType(7025, "Fae Dark Female"));
                legs_types.Add(new LegsType(7029, "Fae Light Male"));
                legs_types.Add(new LegsType(7033, "Fae Light Female"));

            }
            for (int i = 0; i < legs_types.Count; i++)
                comboBox_spawn_npc_legstype.Items.Add(((LegsType)legs_types[i]).legs_type);
        }

        private int GetLegsTypeID(string legs_type) {
            for (int i = 0; i < legs_types.Count; i++) {
                LegsType type = (LegsType)legs_types[i];
                if (legs_type == type.legs_type)
                    return type.id;
            }
            return -1;
        }

        private string GetLegsTypeName(int id) {
            for (int i = 0; i < legs_types.Count; i++) {
                LegsType type = (LegsType)legs_types[i];
                if (id == type.id)
                    return type.legs_type;
            }
            return null;
        }

        private void InitializeWingTypes() {
            if (wing_types.Count == 0) {
                wing_types.Add(new WingType(0, "None"));
                wing_types.Add(new WingType(6279, "Wing12"));
                wing_types.Add(new WingType(6278, "Wing11"));
                wing_types.Add(new WingType(6468, "Wing10_t"));
                wing_types.Add(new WingType(6277, "Wing10"));
                wing_types.Add(new WingType(6467, "Wing09_t"));
                wing_types.Add(new WingType(6276, "Wing09"));
                wing_types.Add(new WingType(6466, "Wing08_t"));
                wing_types.Add(new WingType(6275, "Wing08"));
                wing_types.Add(new WingType(6465, "Wing07_t"));
                wing_types.Add(new WingType(6274, "Wing07"));
                wing_types.Add(new WingType(6464, "Wing06_t"));
                wing_types.Add(new WingType(6273, "Wing06"));
                wing_types.Add(new WingType(6463, "Wing05_t"));
                wing_types.Add(new WingType(6272, "Wing05"));
                wing_types.Add(new WingType(6462, "Wing04_t"));
                wing_types.Add(new WingType(6271, "Wing04"));
                wing_types.Add(new WingType(6461, "Wing03_t"));
                wing_types.Add(new WingType(6233, "Wing03"));
                wing_types.Add(new WingType(6460, "Wing02_t"));
                wing_types.Add(new WingType(6232, "Wing02"));
                wing_types.Add(new WingType(6459, "Wing01_t"));
                wing_types.Add(new WingType(6231, "Wing01"));
                wing_types.Add(new WingType(7119, "Dark Wing12_t"));
                wing_types.Add(new WingType(7118, "Dark Wing12"));
                wing_types.Add(new WingType(7117, "Dark Wing11_t"));
                wing_types.Add(new WingType(7116, "Dark Wing11"));
                wing_types.Add(new WingType(7117, "Dark Wing10_t"));
                wing_types.Add(new WingType(7114, "Dark Wing10"));
                wing_types.Add(new WingType(7113, "Dark Wing09_t"));
                wing_types.Add(new WingType(7112, "Dark Wing09"));
                wing_types.Add(new WingType(7111, "Dark Wing08_t"));
                wing_types.Add(new WingType(7110, "Dark Wing08"));
                wing_types.Add(new WingType(7109, "Dark Wing07_t"));
                wing_types.Add(new WingType(7108, "Dark Wing07"));
                wing_types.Add(new WingType(7107, "Dark Wing06_t"));
                wing_types.Add(new WingType(7106, "Dark Wing06"));
                wing_types.Add(new WingType(7105, "Dark Wing05_t"));
                wing_types.Add(new WingType(7104, "Dark Wing05"));
                wing_types.Add(new WingType(7103, "Dark Wing04_t"));
                wing_types.Add(new WingType(7102, "Dark Wing04"));
                wing_types.Add(new WingType(7101, "Dark Wing03_t"));
                wing_types.Add(new WingType(7100, "Dark Wing03"));
                wing_types.Add(new WingType(7099, "Dark Wing02_t"));
                wing_types.Add(new WingType(7098, "Dark Wing02"));
                wing_types.Add(new WingType(7097, "Dark Wing01_t"));
                wing_types.Add(new WingType(7096, "Dark Wing01"));
            }
            for (int i = 0; i < wing_types.Count; i++)
                comboBox_spawn_npc_wingtype.Items.Add(((WingType)wing_types[i]).wing_type);
        }

        private int GetWingTypeID(string wing_type) {
            for (int i = 0; i < wing_types.Count; i++) {
                WingType type = (WingType)wing_types[i];
                if (wing_type == type.wing_type)
                    return type.id;
            }
            return -1;
        }

        private string GetWingTypeName(int id) {
            for (int i = 0; i < wing_types.Count; i++) {
                WingType type = (WingType)wing_types[i];
                if (id == type.id)
                    return type.wing_type;
            }
            return null;
        }

        private void InitializeEncounterLevels() {
            if (enc_levels.Count == 0) {
                enc_levels.Add(new EncounterLevel(0, "None"));
                enc_levels.Add(new EncounterLevel(1, "Down 3"));
                enc_levels.Add(new EncounterLevel(3, "Down 2"));
                enc_levels.Add(new EncounterLevel(5, "Down 1"));
                enc_levels.Add(new EncounterLevel(6, "Normal"));
                enc_levels.Add(new EncounterLevel(7, "Up 1"));
                enc_levels.Add(new EncounterLevel(8, "Up 2"));
                enc_levels.Add(new EncounterLevel(9, "Up 3"));
                enc_levels.Add(new EncounterLevel(10, "Epic"));

            }
            for (int i = 0; i < enc_levels.Count; i++)
                comboBox_spawn_npc_enclevel.Items.Add(((EncounterLevel)enc_levels[i]).enc_level);
        }

        private int GetEncounterLevelID(string enc_level) {
            for (int i = 0; i < enc_levels.Count; i++) {
                EncounterLevel type = (EncounterLevel)enc_levels[i];
                if (enc_level == type.enc_level)
                    return type.id;
            }
            return -1;
        }

        private string GetEncounterLevelName(int id) {
            for (int i = 0; i < enc_levels.Count; i++) {
                EncounterLevel type = (EncounterLevel)enc_levels[i];
                if (id == type.id)
                    return type.enc_level;
            }
            return null;
        }

        private void InitializeFactions() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, name " +
                                                       "FROM factions");
            if (reader != null) {
                factions.Clear();
                comboBox_spawn_factionid.Items.Clear();
                factions.Add(new Faction(0, "None"));
                comboBox_spawn_factionid.Items.Add(((Faction)factions[0]).faction);
                while (reader.Read()) {
                    Faction faction = new Faction(reader.GetInt32(0), reader.GetString(1));
                    factions.Add(faction);
                    comboBox_spawn_factionid.Items.Add(faction.faction);
                }
                reader.Close();
            }
        }

        private int GetFactionID(string faction) {
            for (int i = 0; i < factions.Count; i++) {
                Faction type = (Faction)factions[i];
                if (faction == type.faction)
                    return type.id;
            }
            return -1;
        }

        private string GetFactionName(int id) {
            for (int i = 0; i < factions.Count; i++) {
                Faction type = (Faction)factions[i];
                if (id == type.id)
                    return type.faction;
            }
            return null;
        }

        private int GetMerchantID(int spawn_id) {
            int merchant_id = 0;
            MySqlDataReader reader = db.RunSelectQuery("SELECT merchant_id " +
                                                       "FROM spawn " +
                                                       "WHERE id=" + spawn_id);
            if (reader != null) {
                if (reader.Read())
                    merchant_id = reader.GetInt32(0);
                reader.Close();
            }
            return merchant_id;
        }

        private string GetAppearanceName(int appearance_id) {
            if (appearance_id <= 0)
                return null;
            string appearance_name = null;
            MySqlDataReader reader = db.RunSelectQuery("SELECT name " +
                                                       "FROM appearances " +
                                                       "WHERE appearance_id=" + appearance_id);
            if (reader != null) {
                if (reader.Read())
                    appearance_name = reader.GetString(0);
                reader.Close();
            }
            return appearance_name;
        }

        private string GetVisualStateName(int visual_state_id) {
            if (visual_state_id <= 0)
                return null;
            string visual_state_name = null;
            MySqlDataReader reader = db.RunSelectQuery("SELECT name " +
                                                       "FROM visual_states " +
                                                       "WHERE visual_state_id=" + visual_state_id);
            if (reader != null) {
                if (reader.Read())
                    visual_state_name = reader.GetString(0);
                reader.Close();
            }
            return visual_state_name;
        }

        private void InitializeSpawnEntriesAndGroups() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, spawn_location_id " +
                                                       "FROM spawn_location_entry " +
                                                       "WHERE spawn_id=" + GetSelectedSpawnID());
            if (reader != null) {
                spawn_entries.Clear();
                spawn_groups.Clear();
                comboBox_spawnscripts_spawnentryid.Items.Clear();
                comboBox_spawnscripts_spawngroupid.Items.Clear();
                spawn_entries.Add(new SpawnEntry(0));
                spawn_groups.Add(new SpawnGroup(0, "None"));
                comboBox_spawnscripts_spawnentryid.Items.Add(((SpawnEntry)spawn_entries[0]).id.ToString());
                comboBox_spawnscripts_spawngroupid.Items.Add(((SpawnGroup)spawn_groups[0]).id + "   (" + ((SpawnGroup)spawn_groups[0]).name + ")");
                while (reader.Read()) {
                    spawn_entries.Add(new SpawnEntry(reader.GetInt32(0)));
                    spawn_groups.Add(new SpawnGroup(reader.GetInt32(1), ""));
                }
                reader.Close();
            }
            for (int i = 1; i < spawn_groups.Count; i++) {
                reader = db.RunSelectQuery("SELECT name " +
                                           "FROM spawn_location_name " +
                                           "WHERE id=" + ((SpawnGroup)spawn_groups[i]).id);
                if (reader != null) {
                    if (reader.Read())
                        ((SpawnGroup)spawn_groups[i]).name = reader.GetString(0);
                    reader.Close();
                }
            }
            for (int i = 1; i < spawn_entries.Count; i++)
                comboBox_spawnscripts_spawnentryid.Items.Add(((SpawnEntry)spawn_entries[i]).id.ToString());
            for (int i = 1; i < spawn_groups.Count; i++)
                comboBox_spawnscripts_spawngroupid.Items.Add(((SpawnGroup)spawn_groups[i]).id + "   (" + ((SpawnGroup)spawn_groups[i]).name + ")");
        }

        private int GetSpawnEntryID(string spawn_entry_id_str) {
            int spawn_entry_id = -1;
            try {
                spawn_entry_id = Convert.ToInt32(spawn_entry_id_str);
            }
            catch (Exception ex) {}
            return spawn_entry_id;
        }

        private int GetSpawnGroupID(string spawn_group_str) {
            int spawn_group_id = -1;
            string search_for = "   (";
            try {
                spawn_group_str = spawn_group_str.Substring(0, spawn_group_str.IndexOf(search_for));
                spawn_group_id = Convert.ToInt32(spawn_group_str);
            }
            catch (Exception ex) {}
            return spawn_group_id;
        }

        private string GetSpawnGroupCombBoxStyle(int spawn_group_id) {
            for (int i = 0; i < spawn_groups.Count; i++)
                if (spawn_group_id == ((SpawnGroup)spawn_groups[i]).id)
                    return ((SpawnGroup)spawn_groups[i]).id + "   (" + ((SpawnGroup)spawn_groups[i]).name + ")";
          
            return null;
        }

        private void tabControl_main_SelectedIndexChanged(object sender, System.EventArgs e) {
            //previous_page = tabControl_main.SelectedTab;
        }

        /*************************************************************************************************************************
         *                                             SPAWN
         *************************************************************************************************************************/

        private void LoadSpawn(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, name, sub_title, race, model_type, size, size_offset, targetable, show_name, command_primary, command_secondary, visual_state, attackable, show_level, show_command_icon, display_hand_icon, faction_id, collision_radius, hp, power, merchant_id, transport_id, merchant_type, savagery, dissonance " +
                                                       "FROM spawn " +
                                                       "WHERE spawn.id=" + spawn_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_spawn_id.Text = reader.GetString(0);
                    textBox_spawn_name.Text = reader.GetString(1);
                    try {textBox_spawn_subtitle.Text = reader.GetString(2);} catch (Exception ex) {textBox_spawn_subtitle.Text = "";}
                    comboBox_spawn_race.SelectedItem = GetRaceName(reader.GetInt32(3));
                    textBox_spawn_modeltype.Text = reader.GetString(4);
                    textBox_spawn_size.Text = reader.GetString(5);
                    textBox_spawn_sizeoffset.Text = reader.GetString(6);
                    textBox_spawn_visualstate.Text = reader.GetString(11);
                    textBox_spawn_collisionradius.Text = reader.GetString(17);
                    textBox_spawn_hp.Text = reader.GetString(18);
                    textBox_spawn_power.Text = reader.GetString(19);
                    comboBox_spawn_factionid.SelectedItem = GetFactionName(reader.GetInt32(16));
                    textBox_spawn_merchantid.Text = reader.GetString(20);
                    textBox_spawn_transportid.Text = reader.GetString(21);
                    textBox_spawn_merchanttype.Text = reader.GetString(22);
                    comboBox_spawn_commandprimary.SelectedItem = GetEntityCommandName(reader.GetInt32(9));
                    comboBox_spawn_commandsecondary.SelectedItem = GetEntityCommandName(reader.GetInt32(10));
                    checkBox_spawn_targetable.Checked = reader.GetBoolean(7);
                    checkBox_spawn_showname.Checked = reader.GetBoolean(8);
                    checkBox_spawn_attackable.Checked = reader.GetBoolean(12);
                    checkBox_spawn_showlevel.Checked = reader.GetBoolean(13);
                    checkBox_spawn_showcommandicon.Checked = reader.GetBoolean(14);
                    checkBox_spawn_displayhandicon.Checked = reader.GetBoolean(15);
                    textBox_spawn_savagery.Text = reader.GetString(16);
                    textBox_spawn_disonance.Text = reader.GetString(17);
                    reader.Close();

                    string model_type = GetAppearanceName(Convert.ToInt32(textBox_spawn_modeltype.Text));
                    string visual_type = GetVisualStateName(Convert.ToInt32(textBox_spawn_visualstate.Text));
                    if (model_type != null && model_type.Length > 45)
                        model_type = model_type.Insert(45, "\n");
                    if (visual_type != null && visual_type.Length > 45)
                        visual_type = visual_type.Insert(45, "\n");

                    label_spawn_modeltype.Text = model_type;
                    label_spawn_visualstate.Text = visual_type;

                    //if (Convert.ToInt32(textBox_spawn_merchantid.Text) == 0)
                    //   tabControl_loot.TabPages.Remove(tabPage_merchant);
                    //else if (!tabControl_loot.TabPages.Contains(tabPage_merchant))
                    //    tabControl_loot.TabPages.Add(tabPage_merchant);

                    owner.Text = "Spawn: " + textBox_spawn_name.Text;
                }
            }
        }

        private void button_spawn_update_Click(object sender, EventArgs e) {
            string id = textBox_spawn_id.Text;
            string name = db.RemoveEscapeCharacters(textBox_spawn_name.Text);
            string sub_title = db.RemoveEscapeCharacters(textBox_spawn_subtitle.Text);
            int race = GetRaceID((string)comboBox_spawn_race.SelectedItem);
            string model_type = textBox_spawn_modeltype.Text;
            string size = textBox_spawn_size.Text;
            string size_offset = textBox_spawn_sizeoffset.Text;
            string targetable = (Convert.ToInt32(checkBox_spawn_targetable.Checked)).ToString();
            string show_name = (Convert.ToInt32(checkBox_spawn_showname.Checked)).ToString();
            int command_primary = GetEntityCommandID((string)comboBox_spawn_commandprimary.SelectedItem);
            int command_secondary = GetEntityCommandID((string)comboBox_spawn_commandsecondary.SelectedItem);
            string visual_state = textBox_spawn_visualstate.Text;
            string attackable = (Convert.ToInt32(checkBox_spawn_attackable.Checked)).ToString();
            string show_level = (Convert.ToInt32(checkBox_spawn_showlevel.Checked)).ToString();
            string show_command_icon = (Convert.ToInt32(checkBox_spawn_showcommandicon.Checked)).ToString();
            string display_hand_icon = (Convert.ToInt32(checkBox_spawn_displayhandicon.Checked)).ToString();
            int faction_id = GetFactionID((string)comboBox_spawn_factionid.SelectedItem);
            string collision_radius = textBox_spawn_collisionradius.Text;
            string hp = textBox_spawn_hp.Text;
            string power = textBox_spawn_power.Text;
            string merchant_id = textBox_spawn_merchantid.Text;
            string transport_id = textBox_spawn_transportid.Text;
            string merchant_type = textBox_spawn_merchanttype.Text;

            int rows = db.RunQuery("UPDATE spawn " +
                                   "SET name='" + name + "', sub_title='" + sub_title + "', race=" + race + ", model_type=" + model_type + ", size=" + size + ", size_offset=" + size_offset + ", targetable=" + targetable + ", show_name=" + show_name + ", command_primary=" + command_primary + ", command_secondary=" + command_secondary + ", visual_state=" + visual_state + ", attackable=" + attackable + ", show_level=" + show_level + ", show_command_icon=" + show_command_icon + ", display_hand_icon=" + display_hand_icon + ", faction_id=" + faction_id + ", collision_radius=" + collision_radius + ", hp=" + hp + ", power=" + power + ", merchant_id=" + merchant_id + ", transport_id=" + transport_id + ", merchant_type=" + merchant_type + " " +
                                   "WHERE spawn.id=" + id);
            if (rows > 0) {
                PopulateSpawnsComboBox();
                comboBox_select_spawn.SelectedItem = name + "   (" + id + ")";
                LoadSpawn(Convert.ToInt32(id));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void ResetSpawn() {
        }

        /*************************************************************************************************************************
        *                                           Need to add in Locations tab code here
        *************************************************************************************************************************/
        
        /*************************************************************************************************************************
        *                                Zonespawngroup Notes updating to use the spawn_location_name table
        *************************************************************************************************************************/

        /*************************************************************************************************************************
        *                                Zonespawnentry Notes updating to use the spawn_location_entry table
        *************************************************************************************************************************/

        private void LoadSpawnLocationEntry(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT sle.id, spawn_id, spawn_location_id, spawnpercentage, name " +
                                                       "FROM spawn_location_entry sle " +
                                                       "INNER JOIN " +
                                                       "spawn_location_name sln ON sle.spawn_location_id = sln.id " +
                                                       "WHERE spawn_id=" + spawn_id);
            if (reader != null)
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    listView_zonespawnentry.Items.Add(item);
                }
                reader.Close();
            }

            if (listView_zonespawnentry.Items.Count == 1)
            {
                listView_zonespawnentry.Items[0].Selected = true;
                listView_zonespawnentry.Items[0].Focused = true;
            }
        }

        private void listView_zonespawnentry_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_zonespawnentry.SelectedIndices.Count == 0 || listView_zonespawnentry.SelectedIndices[0] == -1) {
                ResetZoneSpawnEntry(false);
                return;
            }

            ListViewItem item = listView_zonespawnentry.Items[listView_zonespawnentry.SelectedIndices[0]];
            textBox_zonespawnentry_id.Text = item.Text;
            textBox_zonespawnentry_spawnid.Text = item.SubItems[1].Text;
            textBox_zonespawnentry_spawngroupid.Text = item.SubItems[2].Text;
            textBox_zonespawnentry_spawnpercentage.Text = item.SubItems[3].Text;
            textBox_zonespawngroup_name.Text = item.SubItems[4].Text;

            button_zonespawnentry_insert.Enabled = true;
            button_zonespawnentry_update.Enabled = true;
            button_zonespawnentry_remove.Enabled = true;
        }

        private void button_zonespawnentry_insert_Click(object sender, EventArgs e) {
            int NextSpawnLocationID = GetNextSpawnLocationID();
            zone_spawn_name_insert(NextSpawnLocationID);
            zone_spawn_entry_insert(NextSpawnLocationID);
        }

        private void zone_spawn_name_insert(int NextSpawnLocationID)
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id FROM spawn_location_name WHERE id =" + NextSpawnLocationID);

            int existing_id = -1;

            if (reader != null)
            {
                if (reader.Read())
                    existing_id = reader.GetInt32(0) + 1;
                reader.Close();
            }

            string name = db.RemoveEscapeCharacters(textBox_zonespawngroup_name.Text);

            if (name == "")
            {
                name = "Editor Default";
            }

            if (existing_id < 0)
            {
                db.RunQuery("INSERT INTO spawn_location_name (id, name) " +
                                   "VALUES (" + GetNextSpawnLocationID() + ", '" + name + "')");
            }
        }

        private void zone_spawn_entry_insert(int NextSpawnLocationID)
        {
            string id = textBox_zonespawnentry_id.Text;
            string spawn_id = GetSelectedSpawnID().ToString();
            string spawngroupid = textBox_zonespawnentry_spawngroupid.Text;
            string spawnpercentage = textBox_zonespawnentry_spawnpercentage.Text;

            if (spawngroupid == "")
            {
                spawngroupid = NextSpawnLocationID.ToString();
            }

            if (spawnpercentage == "")
            {
                spawnpercentage = "100";
            }

            int rows = db.RunQuery("INSERT INTO spawn_location_entry (id, spawn_id, spawn_location_id, spawnpercentage) " +
                                   "VALUES (" + GetNextSpawnLocationEntryID() + ", " + spawn_id + ", " + spawngroupid + ", " + spawnpercentage + ")");
            if (rows > 0)
            {
                ResetZoneSpawnEntry(true);
                LoadSpawnLocationEntry(Convert.ToInt32(spawn_id));
            }
        }

        private void button_zonespawnentry_update_Click(object sender, EventArgs e) {
            string id = textBox_zonespawnentry_id.Text;
            string spawn_id = textBox_zonespawnentry_spawnid.Text;
            string spawnlocationid = textBox_zonespawnentry_spawngroupid.Text;
            string spawnpercentage = textBox_zonespawnentry_spawnpercentage.Text;
            string spawnlocationname = textBox_zonespawngroup_name.Text;

            int rows = 0;
            rows += db.RunQuery("UPDATE spawn_location_entry " +
                                   "SET id=" + id + ", spawn_id=" + spawn_id + ", spawn_location_id=" + spawnlocationid + ", spawnpercentage=" + spawnpercentage + " " +
                                   "WHERE spawn_location_entry.id=" + id);

            rows += db.RunQuery("UPDATE spawn_location_name " +
                                   "SET name='" + spawnlocationname.Replace("'", "''") + "' " +
                                   "WHERE id=" + spawnlocationid);
            if (rows > 0)
            {
                ResetZoneSpawnEntry(true);
                LoadSpawnLocationEntry(Convert.ToInt32(spawn_id));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_zonespawnentry_remove_Click(object sender, EventArgs e) {
            spawn_location_entry_remove();
            spawn_location_name_remove();

            string spawn_id = textBox_zonespawnentry_spawnid.Text;

            ResetZoneSpawns(true);
            ResetZoneSpawnEntry(true);
            LoadSpawnLocationPlacement(Convert.ToInt32(textBox_spawn_id.Text));
            LoadSpawnLocationEntry(Convert.ToInt32(textBox_spawn_id.Text));
        }

        private void spawn_location_entry_remove()
        {
            string id = textBox_zonespawnentry_id.Text;
            string spawn_id = textBox_zonespawnentry_spawnid.Text;

            db.RunQuery("DELETE FROM spawn_location_entry " +
                        "WHERE spawn_location_entry.id=" + id);
        }
        private void spawn_location_name_remove()
        {
            string spawn_location_id = textBox_zonespawnentry_spawngroupid.Text;

            MySqlDataReader reader = db.RunSelectQuery("SELECT COUNT(id) FROM spawn_location_entry WHERE spawn_location_id =" + spawn_location_id);

            int existing_id = -1;

            if (reader != null)
            {
                if (reader.Read())
                    existing_id = reader.GetInt32(0);
                reader.Close();
            }

            if (existing_id < 1)
            {
                db.RunQuery("DELETE FROM spawn_location_name " +
                        "WHERE spawn_location_name.id=" + spawn_location_id);
            }
        }

        private void ResetZoneSpawnEntry(bool include_listview) {
            if (include_listview)
                listView_zonespawnentry.Items.Clear();

            textBox_zonespawnentry_id.Clear();
            textBox_zonespawnentry_spawnid.Clear();
            textBox_zonespawnentry_spawngroupid.Clear();
            textBox_zonespawnentry_spawnpercentage.Clear();

            button_zonespawnentry_update.Enabled = false;
            button_zonespawnentry_remove.Enabled = false;
        }

        /*************************************************************************************************************************
        *                                Zonespawns Notes updating to use the spawn_location_placement table
        *************************************************************************************************************************/

        private void LoadSpawnLocationPlacement(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT spawn_location_placement.id, spawn_location_placement.zone_id, spawn_location_placement.spawn_location_id, x, y, z, x_offset, y_offset, z_offset, heading, pitch, roll, respawn, expire_timer, expire_offset, grid_id " +
                                                       "FROM spawn_location_placement, spawn_location_entry " +
                                                       "WHERE spawn_location_placement.spawn_location_id= spawn_location_entry.spawn_location_id AND spawn_location_entry.spawn_id=" + spawn_id);
            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(5)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(6)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(7)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(8)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(9)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(10)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(11)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(12)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(13)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(14)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(15)));
                    listView_zonespawns.Items.Add(item);
                }
                reader.Close();
            }

            if (listView_zonespawns.Items.Count == 1)
            {
                listView_zonespawns.Items[0].Selected = true;
                listView_zonespawns.Items[0].Focused = true;
            }
        }

        private void listView_zonespawns_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_zonespawns.SelectedIndices.Count == 0 || listView_zonespawns.SelectedIndices[0] == -1) {
                ResetZoneSpawns(false);
                return;
            }

            ListViewItem item = listView_zonespawns.Items[listView_zonespawns.SelectedIndices[0]];
            textBox_zonespawns_id.Text = item.Text;
            textBox_zonespawns_zoneid.Text = item.SubItems[1].Text;
            textBox_zonespawns_spawnlocationid.Text = item.SubItems[2].Text;
            textBox_zonespawns_x.Text = item.SubItems[3].Text;
            textBox_zonespawns_y.Text = item.SubItems[4].Text;
            textBox_zonespawns_z.Text = item.SubItems[5].Text;
            textBox_zonespawns_xoffset.Text = item.SubItems[6].Text;
            textBox_zonespawns_yoffset.Text = item.SubItems[7].Text;
            textBox_zonespawns_zoffset.Text = item.SubItems[8].Text;
            textBox_zonespawns_heading.Text = item.SubItems[9].Text;
            textBox_zonespawns_pitch.Text = item.SubItems[10].Text;
            textBox_zonespawns_roll.Text = item.SubItems[11].Text;
            textBox_zonespawns_respawn.Text = item.SubItems[12].Text;
            textBox_zonespawns_expiretimer.Text = item.SubItems[13].Text;
            textBox_zonespawns_expireoffset.Text = item.SubItems[14].Text;
            textBox_zonespawns_gridid.Text = item.SubItems[15].Text;
            textBox_location.Text = "/move " + item.SubItems[3].Text + " " + item.SubItems[4].Text + " " + item.SubItems[5].Text;

            button_zonespawns_insert.Enabled = true;
            button_zonespawns_update.Enabled = true;
            button_zonespawns_remove.Enabled = true;
        }

        private void button_zonespawns_insert_Click(object sender, EventArgs e) {
            string spawn_location_id = "0";

            if (listView_zonespawnentry.Items.Count == 0)
            {
                MessageBox.Show("Please add a spawn location entry and select it before adding a spawn location placement");
                return;
            }
            else if (listView_zonespawnentry.Items.Count == 1)
            {
                spawn_location_id = listView_zonespawnentry.Items[0].SubItems[2].Text;
            }
            else if (listView_zonespawnentry.SelectedIndices.Count == 0)
            {
                MessageBox.Show("There are multiple spawn location placements, please select one");
                return;
            }
            else 
            {
                spawn_location_id = listView_zonespawnentry.Items[listView_zonespawnentry.SelectedIndices[0]].SubItems[2].Text;
            }

            string zone_id = GetSelectedZoneID().ToString();
            string x = string.IsNullOrEmpty(textBox_zonespawns_x.Text) ? "0" : textBox_zonespawns_x.Text;
            string y = string.IsNullOrEmpty(textBox_zonespawns_y.Text) ? "0" : textBox_zonespawns_y.Text;
            string z = string.IsNullOrEmpty(textBox_zonespawns_z.Text) ? "0" : textBox_zonespawns_z.Text;
            string x_offset = string.IsNullOrEmpty(textBox_zonespawns_xoffset.Text) ? "0" : textBox_zonespawns_xoffset.Text;
            string y_offset = string.IsNullOrEmpty(textBox_zonespawns_yoffset.Text) ? "0" : textBox_zonespawns_yoffset.Text;
            string z_offset = string.IsNullOrEmpty(textBox_zonespawns_zoffset.Text) ? "0" : textBox_zonespawns_zoffset.Text;
            string heading = string.IsNullOrEmpty(textBox_zonespawns_heading.Text) ? "0" : textBox_zonespawns_heading.Text;
            string respawn = string.IsNullOrEmpty(textBox_zonespawns_respawn.Text) ? "300" : textBox_zonespawns_respawn.Text;
            string grid_id = string.IsNullOrEmpty(textBox_zonespawns_gridid.Text) ? "0" : textBox_zonespawns_gridid.Text;

            int rows = db.RunQuery("INSERT INTO spawn_location_placement (id, zone_id, spawn_location_id, x, y, z, x_offset, y_offset, z_offset, heading, respawn, grid_id) " +
                                   "VALUES (" + GetNextSpawnLocationPlacementID() + ", " + zone_id + ", " + spawn_location_id + ", " + x + ", " + y + ", " + z + ", " + x_offset + ", " + y_offset + ", " + z_offset + ", " + heading + ", " + respawn + ", " + grid_id + ")");
            if (rows > 0)
            {
                ResetZoneSpawns(true);
                LoadSpawnLocationPlacement(Convert.ToInt32(textBox_spawn_id.Text));
            }
        }

        private void button_zonespawns_update_Click(object sender, EventArgs e) {
            string id = textBox_zonespawns_id.Text;
            string zone_id = textBox_zonespawns_zoneid.Text;
            string spawn_location_id = textBox_zonespawns_spawnlocationid.Text;
            string x = textBox_zonespawns_x.Text;
            string y = textBox_zonespawns_y.Text;
            string z = textBox_zonespawns_z.Text;
            string x_offset = textBox_zonespawns_xoffset.Text;
            string y_offset = textBox_zonespawns_yoffset.Text;
            string z_offset = textBox_zonespawns_zoffset.Text;
            string heading = textBox_zonespawns_heading.Text;
            string pitch = textBox_zonespawns_pitch.Text;
            string roll = textBox_zonespawns_roll.Text;
            string respawn = textBox_zonespawns_respawn.Text;
            string expire_timer = textBox_zonespawns_expiretimer.Text;
            string expire_offset = textBox_zonespawns_expireoffset.Text;
            string grid_id = textBox_zonespawns_gridid.Text;

            int rows = db.RunQuery("UPDATE spawn_location_placement " +
                                   "SET id=" + id + ", zone_id=" + zone_id + ", spawn_location_id=" + spawn_location_id + ", x=" + x + ", y=" + y + ", z=" + z + ", x_offset=" + x_offset + ", y_offset=" + y_offset + ", z_offset=" + z_offset + ", heading=" + heading + ", pitch=" + pitch + ", roll=" + roll + ", respawn=" + respawn + ", expire_timer=" + expire_timer + ", expire_offset=" + expire_offset + ", grid_id=" + grid_id + " " +
                                   "WHERE spawn_location_placement.id=" + id);
            if (rows > 0) {
                ResetZoneSpawns(true);
                LoadSpawnLocationPlacement(Convert.ToInt32(textBox_spawn_id.Text));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_zonespawns_remove_Click(object sender, EventArgs e) {
            string id = textBox_zonespawns_id.Text;

            int rows = db.RunQuery("DELETE FROM spawn_location_placement " +
                                   "WHERE spawn_location_placement.id=" + id);
            if (rows > 0) {
                ResetZoneSpawns(true);
                ResetZoneSpawnEntry(true);
                LoadSpawnLocationPlacement(Convert.ToInt32(textBox_spawn_id.Text));
                LoadSpawnLocationEntry(Convert.ToInt32(textBox_spawn_id.Text));
            }
        }

        private void ResetZoneSpawns(bool include_listview) {
            if (include_listview)
                listView_zonespawns.Items.Clear();

            textBox_zonespawns_id.Clear();
            textBox_zonespawns_zoneid.Clear();
            textBox_zonespawns_spawnlocationid.Clear();
            textBox_zonespawns_x.Clear();
            textBox_zonespawns_y.Clear();
            textBox_zonespawns_z.Clear();
            textBox_zonespawns_xoffset.Clear();
            textBox_zonespawns_yoffset.Clear();
            textBox_zonespawns_zoffset.Clear();
            textBox_zonespawns_heading.Clear();
            textBox_zonespawns_respawn.Clear();
            textBox_zonespawns_gridid.Clear();

            button_zonespawns_update.Enabled = false;
            button_zonespawns_remove.Enabled = false;
        }

        /*************************************************************************************************************************
        *                                             Spawnscripts
        *************************************************************************************************************************/

        private void LoadSpawnScripts(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, spawn_id, spawnentry_id, spawn_location_id, lua_script " +
                                                       "FROM spawn_scripts " +
                                                       "WHERE spawn_id=" + spawn_id + " ");
            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    listView_spawnscripts.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_spawnscripts_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_spawnscripts.SelectedIndices.Count == 0 || listView_spawnscripts.SelectedIndices[0] == -1) {
                ResetSpawnScripts(false);
                return;
            }

            ListViewItem item = listView_spawnscripts.Items[listView_spawnscripts.SelectedIndices[0]];
            textBox_spawnscripts_id.Text = item.Text;
            textBox_spawnscripts_spawnid.Text = item.SubItems[1].Text;
            comboBox_spawnscripts_spawnentryid.SelectedItem = item.SubItems[2].Text;
            comboBox_spawnscripts_spawngroupid.SelectedItem = GetSpawnGroupCombBoxStyle(Convert.ToInt32(item.SubItems[3].Text));
            textBox_spawnscripts_luascript.Text = item.SubItems[4].Text;

            button_spawnscripts_update.Enabled = true;
            button_spawnscripts_remove.Enabled = true;
            button_spawnscripts_openscript.Enabled = true;
        }

        private void button_spawnscripts_insert_Click(object sender, EventArgs e) {
            string spawn_id = GetSelectedSpawnID().ToString();
            string spawnentry_id = string.IsNullOrEmpty((string)comboBox_spawnscripts_spawnentryid.SelectedItem) ? "0" : (string)comboBox_spawnscripts_spawnentryid.SelectedItem;
            int spawn_location_id = GetSpawnGroupID((string)comboBox_spawnscripts_spawngroupid.SelectedItem);
            string lua_script = string.IsNullOrEmpty(db.RemoveEscapeCharacters(textBox_spawnscripts_luascript.Text)) ? ("SpawnScripts/" + RemoveSpacesAndEscape(GetSelectedZoneName()) + "/" + RemoveSpacesAndEscape(GetSelectedSpawnName()) + ".lua") : textBox_spawnscripts_luascript.Text;

            if (Convert.ToInt32(spawnentry_id) > 0 || spawn_location_id > 0)
                spawn_id = "0";

            int rows = db.RunQuery("INSERT INTO spawn_scripts (spawn_id, spawnentry_id, spawn_location_id, lua_script) " +
                                   "VALUES (" + spawn_id + ", " + spawnentry_id + ", " + spawn_location_id + ", '" + lua_script + "')");
            if (rows > 0) {
                ResetSpawnScripts(true);
                LoadSpawnScripts(GetSelectedSpawnID());
            }
        }

        private void button_spawnscripts_update_Click(object sender, EventArgs e) {
            string id = textBox_spawnscripts_id.Text;
            string spawn_id = textBox_spawnscripts_spawnid.Text;
            string spawnentry_id = (string)comboBox_spawnscripts_spawnentryid.SelectedItem;
            int spawn_location_id = GetSpawnGroupID((string)comboBox_spawnscripts_spawngroupid.SelectedItem);
            string lua_script = db.RemoveEscapeCharacters(textBox_spawnscripts_luascript.Text);
            if (Convert.ToInt32(spawnentry_id) > 0 || spawn_location_id > 0)
                spawn_id = "0";

            int rows = db.RunQuery("UPDATE spawn_scripts " +
                                   "SET id=" + id + ", spawn_id=" + spawn_id + ", spawnentry_id=" + spawn_location_id + ", spawn_location_id=" + spawn_location_id + ", lua_script='" + lua_script + "' " +
                                   "WHERE spawn_scripts.id=" + id);
            if (rows > 0) {
                ResetSpawnScripts(true);
                LoadSpawnScripts(Convert.ToInt32(spawn_id));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_spawnscripts_remove_Click(object sender, EventArgs e) {
            string id = textBox_spawnscripts_id.Text;

            int rows = db.RunQuery("DELETE FROM spawn_scripts " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetSpawnScripts(true);
                LoadSpawnScripts(GetSelectedSpawnID());
            }
        }

        private void button_spawnscripts_openscript_Click(object sender, EventArgs e) {
            new Form_SpawnScript(connection, db, Convert.ToInt32(textBox_spawnscripts_id.Text), Convert.ToInt32(textBox_spawnscripts_spawnid.Text), GetSpawnGroupID((string)comboBox_spawnscripts_spawngroupid.SelectedItem), GetSpawnEntryID((string)comboBox_spawnscripts_spawnentryid.SelectedItem)).Show();
        }

        private void button_spawnscripts_getscriptname_Click(object sender, EventArgs e) {
            textBox_spawnscripts_luascript.Text = "SpawnScripts/" + RemoveSpacesAndEscape(GetSelectedZoneName()) + "/" + RemoveSpacesAndEscape(GetSelectedSpawnName().Replace("-","")) + ".lua";
        }

        private void ResetSpawnScripts(bool include_listview) {
            if (include_listview)
                listView_spawnscripts.Items.Clear();
            textBox_spawnscripts_id.Clear();
            textBox_spawnscripts_spawnid.Clear();
            try {
                comboBox_spawnscripts_spawnentryid.SelectedIndex = 0;
                comboBox_spawnscripts_spawngroupid.SelectedIndex = 0;
            }
            catch (Exception ex) {
                comboBox_spawnscripts_spawnentryid.SelectedItem = null;
                comboBox_spawnscripts_spawngroupid.SelectedItem = null;
            }
            textBox_spawnscripts_luascript.Clear();
            button_spawnscripts_update.Enabled = false;
            button_spawnscripts_remove.Enabled = false;
            button_spawnscripts_openscript.Enabled = false;
        }

        /*************************************************************************************************************************
        *                                             NPC
        *************************************************************************************************************************/

        private void LoadNPC(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT spawn_npcs.id, spawn_npcs.spawn_id, spawn_npcs.min_level, spawn_npcs.max_level, spawn_npcs.enc_level, spawn_npcs.class_, spawn_npcs.gender, spawn_npcs.min_group_size, spawn_npcs.max_group_size, spawn_npcs.hair_type_id, spawn_npcs.facial_hair_type_id, spawn_npcs.wing_type_id, spawn_npcs.chest_type_id, spawn_npcs.legs_type_id, spawn_npcs.soga_hair_type_id, spawn_npcs.soga_facial_hair_type_id, spawn_npcs.soga_model_type, spawn_npcs.heroic_flag, spawn_npcs.action_state, spawn_npcs.mood_state, spawn_npcs.initial_state, activity_status, " +
                                                       "str , sta, wis, intel, agi, heat, cold, poison, disease, magic, mental, divine, aggro_radius, attack_type, equipment_list_id, spell_list_Id, skill_list_id, secondary_spell_list_id, secondary_skill_list_id, cast_percentage, ai_strategy, emote_state  " +
                                                       "FROM spawn_npcs " +
                                                       "WHERE spawn_npcs.spawn_id=" + spawn_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_spawn_npc_id.Text = reader.GetString(0);
                    textBox_spawn_npc_spawnid.Text = reader.GetString(1);
                    textBox_spawn_npc_minlevel.Text = reader.GetString(2);
                    textBox_spawn_npc_maxlevel.Text = reader.GetString(3);
                    comboBox_spawn_npc_enclevel.SelectedItem = GetEncounterLevelName(reader.GetInt32(4));
                    comboBox_spawn_npc_class.SelectedItem = GetClassName(reader.GetInt32(5));
                    comboBox_spawn_npc_gender.SelectedItem = GetGenderName(reader.GetInt32(6));
                    textBox_spawn_npc_mingroupsize.Text = reader.GetString(7);
                    textBox_spawn_npc_maxgroupsize.Text = reader.GetString(8);
                    comboBox_spawn_npc_hairtype.SelectedItem = GetHairTypeName(reader.GetInt32(9));
                    comboBox_spawn_npc_facialhairtype.SelectedItem = GetHairFaceTypeName(reader.GetInt32(10));
                    comboBox_spawn_npc_wingtype.SelectedItem = GetWingTypeName(reader.GetInt32(11));
                    comboBox_spawn_npc_chesttype.SelectedItem = GetChestTypeName(reader.GetInt32(12));
                    comboBox_spawn_npc_legstype.SelectedItem = GetLegsTypeName(reader.GetInt32(13));
                    comboBox_spawn_npc_shairtype.SelectedItem = GetHairTypeName(reader.GetInt32(14));
                    comboBox_spawn_npc_sfacialhairtype.SelectedItem = GetHairFaceTypeName(reader.GetInt32(15));
                    textBox_spawn_npc_smodeltype.Text = reader.GetString(16);
                    textBox_spawn_npc_heroicflag.Text = reader.GetString(17);
                    textBox_spawn_npc_actionstate.Text = reader.GetString(18);
                    textBox_spawn_npc_moodstate.Text = reader.GetString(19);
                    textBox_spawn_npc_initialstate.Text = reader.GetString(20);
                    textBox_spawn_npc_activitystatus.Text = reader.GetString(21);
                    textBox_spawn_npc_str.Text = reader.GetString(22);
                    textBox_spawn_npc_sta.Text = reader.GetString(23);
                    textBox_spawn_npc_wis.Text = reader.GetString(24);
                    textBox_spawn_npc_intel.Text = reader.GetString(25);
                    textBox_spawn_npc_agi.Text = reader.GetString(26);
                    textBox_spawn_npc_heat.Text = reader.GetString(27);
                    textBox_spawn_npc_cold.Text = reader.GetString(28);
                    textBox_spawn_npc_poison.Text = reader.GetString(29);
                    textBox_spawn_npc_disease.Text = reader.GetString(30);
                    textBox_spawn_npc_magic.Text = reader.GetString(31);
                    textBox_spawn_npc_mental.Text = reader.GetString(32);
                    textBox_spawn_npc_divine.Text = reader.GetString(33);
                    textBox_spawn_npc_aggroradius.Text = reader.GetString(34);
                    comboBox_npc_attack_type.SelectedItem = GetAttackTypeName(reader.GetInt32(35));
                    textBox_spawn_npc_equiplistid.Text = reader.GetString(36);
                    textBox_spawn_npc_spelllistid.Text = reader.GetString(37);
                    textBox_spawn_npc_skilllistid.Text = reader.GetString(38);
                    textBox_spawn_npc_secondspelllistid.Text = reader.GetString(39);
                    textBox_spawn_npc_secondskilllistid.Text = reader.GetString(40);
                    textBox_spawn_npc_castpercentage.Text = reader.GetString(41);
                    comboBox_spawn_npc_aistrategy.SelectedItem = reader.GetString(42);
                    textBox_spawn_npc_emotestate.Text = reader.GetString(43);

                    reader.Close();

                    label_npc_initialstate.Text = GetVisualStateName(Convert.ToInt32(textBox_spawn_npc_initialstate.Text));
                    label_npc_actionstate.Text = GetVisualStateName(Convert.ToInt32(textBox_spawn_npc_actionstate.Text));
                    label_npc_moodstate.Text = GetVisualStateName(Convert.ToInt32(textBox_spawn_npc_moodstate.Text));
                    label_npc_emotestate.Text = GetVisualStateName(Convert.ToInt32(textBox_spawn_npc_emotestate.Text));
                }
            }
        }

        private void button_npc_update_Click(object sender, EventArgs e) {
            string id = textBox_spawn_npc_id.Text;
            string spawn_id = textBox_spawn_npc_spawnid.Text;
            string min_level = textBox_spawn_npc_minlevel.Text;
            string max_level = textBox_spawn_npc_maxlevel.Text;
            int enc_level = GetEncounterLevelID((string)comboBox_spawn_npc_enclevel.SelectedItem);
            int class_ = GetClassID((string)comboBox_spawn_npc_class.SelectedItem);
            int gender = GetGenderID((string)comboBox_spawn_npc_gender.SelectedItem);
            string min_group_size = textBox_spawn_npc_mingroupsize.Text;
            string max_group_size = textBox_spawn_npc_maxgroupsize.Text;
            int hair_type_id = GetHairTypeID((string)comboBox_spawn_npc_hairtype.SelectedItem);
            int facial_hair_type_id = GetHairFaceTypeID((string)comboBox_spawn_npc_facialhairtype.SelectedItem);
            int wing_type_id = GetWingTypeID((string)comboBox_spawn_npc_wingtype.SelectedItem);
            int chest_type_id = GetChestTypeID((string)comboBox_spawn_npc_chesttype.SelectedItem);
            int legs_type_id = GetLegsTypeID((string)comboBox_spawn_npc_legstype.SelectedItem);
            int soga_hair_type_id = GetHairTypeID((string)comboBox_spawn_npc_shairtype.SelectedItem);
            int soga_facial_hair_type_id = GetHairFaceTypeID((string)comboBox_spawn_npc_sfacialhairtype.SelectedItem);
            int attack_type = GetAttackTypeID((string)comboBox_npc_attack_type.SelectedItem);
            string soga_model_type = textBox_spawn_npc_smodeltype.Text;
            string heroic_flag = textBox_spawn_npc_heroicflag.Text;
            string action_state = textBox_spawn_npc_actionstate.Text;
            string mood_state = textBox_spawn_npc_moodstate.Text;
            string initial_state = textBox_spawn_npc_initialstate.Text;
            string activity_status = textBox_spawn_npc_activitystatus.Text;
            string str = textBox_spawn_npc_str.Text;
            string sta = textBox_spawn_npc_sta.Text;
            string wis = textBox_spawn_npc_wis.Text;
            string intel = textBox_spawn_npc_intel.Text;
            string agi = textBox_spawn_npc_agi.Text;
            string heat = textBox_spawn_npc_heat.Text;
            string cold = textBox_spawn_npc_cold.Text;
            string poison = textBox_spawn_npc_poison.Text;
            string disease = textBox_spawn_npc_disease.Text;
            string magic = textBox_spawn_npc_magic.Text;
            string mental = textBox_spawn_npc_mental.Text;
            string divine = textBox_spawn_npc_divine.Text;
            string aggro_radius = textBox_spawn_npc_aggroradius.Text;
            string equipment_list_id = textBox_spawn_npc_equiplistid.Text;
            string spell_list_id = textBox_spawn_npc_spelllistid.Text;
            string skill_list_id = textBox_spawn_npc_skilllistid.Text;
            string secondary_spell_list_id = textBox_spawn_npc_secondspelllistid.Text;
            string secondary_skill_list_id = textBox_spawn_npc_secondskilllistid.Text;
            string cast_percentage = textBox_spawn_npc_castpercentage.Text;
            string ai_strategy = comboBox_spawn_npc_aistrategy.SelectedItem.ToString();
            string emote_state = textBox_spawn_npc_emotestate.Text;

            int rows = db.RunQuery("UPDATE spawn_npcs " +
                                   "SET id=" + id + ", spawn_id=" + spawn_id + ", min_level=" + min_level + ", max_level=" + max_level + ", enc_level=" + enc_level + ", class_=" + class_ + ", gender=" + gender + ", min_group_size=" + min_group_size + ", max_group_size=" + max_group_size + ", hair_type_id=" + hair_type_id + ", facial_hair_type_id=" + facial_hair_type_id + ", wing_type_id=" + wing_type_id + ", chest_type_id=" + chest_type_id + ", legs_type_id=" + legs_type_id + ", soga_hair_type_id=" + soga_hair_type_id + ", soga_facial_hair_type_id=" + soga_facial_hair_type_id + ", soga_model_type=" + soga_model_type + ", heroic_flag=" + heroic_flag + ", action_state=" + action_state + ", mood_state=" + mood_state + ", initial_state=" + initial_state + ", activity_status=" + activity_status + ", action_state=" + action_state + ", " +
                                   "str=" + str + ", sta=" + sta + ", wis=" + wis +", intel=" + intel + ", agi=" + agi + ", heat=" + heat + ", cold=" + cold + ", poison=" + poison + ", disease = " + disease + ", magic=" + magic + ", mental=" + mental + ", divine=" + divine + ", aggro_radius=" + aggro_radius + ", attack_type=" + attack_type + ", equipment_list_id=" + equipment_list_id + ", spell_list_id=" + spell_list_id + ", skill_list_Id=" + skill_list_id + ", secondary_spell_list_id=" + secondary_spell_list_id + ", secondary_skill_list_id=" + secondary_skill_list_id + ", cast_percentage=" + cast_percentage + ", ai_strategy='" + ai_strategy + "' , emote_state=" + emote_state + " " +
                                   "WHERE spawn_npcs.id=" + id);
            if (rows > 0)
                LoadNPC(Convert.ToInt32(spawn_id));
            System.Media.SystemSounds.Beep.Play();
        }

        private void ResetNPC() {
        }

        /*************************************************************************************************************************
        *                                             NPC Appearance
        *************************************************************************************************************************/

        private void LoadNPCAppearance(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT npc_appearance.id, npc_appearance.spawn_id, npc_appearance.signed_value, npc_appearance.type, npc_appearance.red, npc_appearance.green, npc_appearance.blue " +
                                                       "FROM npc_appearance " +
                                                       "WHERE npc_appearance.spawn_id=" + spawn_id);
            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(5)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(6)));
                    listView_npc_appearance.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_npc_appearance_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_npc_appearance.SelectedIndices.Count == 0 || listView_npc_appearance.SelectedIndices[0] == -1) {
                ResetNPCAppearance(false);
                return;
            }

            ListViewItem item = listView_npc_appearance.Items[listView_npc_appearance.SelectedIndices[0]];
            textBox_npcappearance_id.Text = item.Text;
            textBox_npcappearance_spawnid.Text = item.SubItems[1].Text;
            comboBox_npcappearance_signedvalue.SelectedItem = Convert.ToString(Convert.ToBoolean(Convert.ToInt32(item.SubItems[2].Text)));
            comboBox_npcappearance_type.SelectedItem = GetAppearanceTypeName(item.SubItems[3].Text);
            textBox_npcappearance_red.Text = item.SubItems[4].Text;
            textBox_npcappearance_green.Text = item.SubItems[5].Text;
            textBox_npcappearance_blue.Text = item.SubItems[6].Text;

            button_npcappearance_insert.Enabled = true;
            button_npcappearance_update.Enabled = true;
            button_npcappearance_remove.Enabled = true;
        }

        private void button_npcappearance_insert_Click(object sender, EventArgs e) {
            string spawn_id = GetSelectedSpawnID().ToString();
            int signed_value = Convert.ToInt32(Convert.ToBoolean((string)comboBox_npcappearance_signedvalue.SelectedItem));
            string type = GetAppearanceTypeID((string)comboBox_npcappearance_type.SelectedItem);
            string red = string.IsNullOrEmpty(textBox_npcappearance_red.Text) ? "0" : textBox_npcappearance_red.Text;
            string green = string.IsNullOrEmpty(textBox_npcappearance_green.Text) ? "0" : textBox_npcappearance_green.Text;
            string blue = string.IsNullOrEmpty(textBox_npcappearance_blue.Text) ? "0" : textBox_npcappearance_blue.Text;
            
            int rows = db.RunQuery("INSERT INTO npc_appearance (spawn_id, signed_value, type, red, green, blue) " +
                                   "VALUES (" + spawn_id + ", " + signed_value + ", '" + type + "', " + red + ", " + green + ", " + blue + ")");
            if (rows > 0) {
                ResetNPCAppearance(true);
                LoadNPCAppearance(Convert.ToInt32(spawn_id));
            }
        }

        private void button_npcappearance_update_Click(object sender, EventArgs e) {
                        string id = textBox_npcappearance_id.Text;
            string spawn_id = textBox_npcappearance_spawnid.Text;
            int signed_value = Convert.ToInt32(Convert.ToBoolean((string)comboBox_npcappearance_signedvalue.SelectedItem));
            string type = GetAppearanceTypeID((string)comboBox_npcappearance_type.SelectedItem);
            string red = textBox_npcappearance_red.Text;
            string green = textBox_npcappearance_green.Text;
            string blue = textBox_npcappearance_blue.Text;

            int rows = db.RunQuery("UPDATE npc_appearance " +
                                   "SET id=" + id + ", spawn_id=" + spawn_id + ", signed_value=" + signed_value + ", type='" + type + "', red=" + red + ", green=" + green + ", blue=" + blue + " " +
                                   "WHERE npc_appearance.id=" + id);
            if (rows > 0)
            {
                ResetNPCAppearance(true);
                LoadNPCAppearance(Convert.ToInt32(spawn_id));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_npcappearance_remove_Click(object sender, EventArgs e) {
            string id = textBox_npcappearance_id.Text;
            string spawn_id = textBox_npcappearance_spawnid.Text;

            int rows = db.RunQuery("DELETE FROM npc_appearance " +
                                   "WHERE npc_appearance.id=" + id);

            if (rows > 0) {
                ResetNPCAppearance(true);
                LoadNPCAppearance(Convert.ToInt32(spawn_id));
            }
        }

        private void ResetNPCAppearance(bool include_listview) {
            if (include_listview)
                listView_npc_appearance.Items.Clear();

            textBox_npcappearance_id.Clear();
            textBox_npcappearance_spawnid.Clear();
            comboBox_npcappearance_signedvalue.SelectedItem = "False";
            comboBox_npcappearance_type.SelectedItem = null;
            textBox_npcappearance_red.Clear();
            textBox_npcappearance_green.Clear();
            textBox_npcappearance_blue.Clear();
            trackBar_npcappearance_red.Value = 0;
            trackBar_npcappearance_green.Value = 0;
            trackBar_npcappearance_blue.Value = 0;
            pictureBox_npcappearance_color.BackColor = Color.Black;

            button_npcappearance_update.Enabled = false;
            button_npcappearance_remove.Enabled = false;
        }

        private void textBox_npcappearance_red_TextChanged(object sender, EventArgs e) {
            if (this.ActiveControl == sender || this.ActiveControl == listView_npc_appearance) {
                try {
                    trackBar_npcappearance_red.Value = Convert.ToInt32(textBox_npcappearance_red.Text);
                }
                catch (Exception ex) {
                    trackBar_npcappearance_red.Value = 0;
                }
            }
            CheckColor(trackBar_npcappearance_red.Value, trackBar_npcappearance_green.Value, trackBar_npcappearance_blue.Value, ref pictureBox_npcappearance_color);
        }

        private void textBox_npcappearance_green_TextChanged(object sender, EventArgs e) {
            if (this.ActiveControl == sender || this.ActiveControl == listView_npc_appearance) {
                try {
                    trackBar_npcappearance_green.Value = Convert.ToInt32(textBox_npcappearance_green.Text);
                }
                catch (Exception ex) {
                    trackBar_npcappearance_green.Value = 0;
                }
            }
            CheckColor(trackBar_npcappearance_red.Value, trackBar_npcappearance_green.Value, trackBar_npcappearance_blue.Value, ref pictureBox_npcappearance_color);
        }

        private void textBox_npcappearance_blue_TextChanged(object sender, EventArgs e) {
            if (this.ActiveControl == sender || this.ActiveControl == listView_npc_appearance) {
                try {
                    trackBar_npcappearance_blue.Value = Convert.ToInt32(textBox_npcappearance_blue.Text);
                }
                catch (Exception ex) {
                    trackBar_npcappearance_blue.Value = 0;
                }
            }
            CheckColor(trackBar_npcappearance_red.Value, trackBar_npcappearance_green.Value, trackBar_npcappearance_blue.Value, ref pictureBox_npcappearance_color);
        }

        private void trackBar_npcappearance_red_Scroll(object sender, EventArgs e) {
            textBox_npcappearance_red.Text = trackBar_npcappearance_red.Value.ToString();
        }

        private void trackBar_npcappearance_green_Scroll(object sender, EventArgs e) {
            textBox_npcappearance_green.Text = trackBar_npcappearance_green.Value.ToString();
        }

        private void trackBar_npcappearance_blue_Scroll(object sender, EventArgs e) {
            textBox_npcappearance_blue.Text = trackBar_npcappearance_blue.Value.ToString();
        }

        /*************************************************************************************************************************
        *                                             NPC Appearance Equip
        *************************************************************************************************************************/

        private void LoadNPCAppearanceEquip(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT e.id, e.spawn_id, e.slot_id, s.slot, e.equip_type, a.name, e.red, e.green, e.blue, e.highlight_red, e.highlight_green, e.highlight_blue " +
                                                        "FROM npc_appearance_equip e " +
                                                        "INNER JOIN " +
                                                        "npc_appearance_equip_slot s ON s.id=e.slot_id " +
                                                        "LEFT OUTER JOIN " +
                                                        "appearances a ON e.equip_type=a.appearance_id  WHERE e.spawn_id=" + spawn_id);

            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(5)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(6)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(7)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(8)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(9)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(10)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(11)));
                    listView_appearance_equip.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_appearance_equip_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_appearance_equip.SelectedIndices.Count == 0 || listView_appearance_equip.SelectedIndices[0] == -1) {
                ResetNPCAppearanceEquip(false);
                return;
            }

            ListViewItem item = listView_appearance_equip.Items[listView_appearance_equip.SelectedIndices[0]];
            textBox_npcappearanceequip_id.Text = item.Text;
            textBox_npcappearanceequip_spawnid.Text = item.SubItems[1].Text;
            comboBox_npcappearanceequip_slotid.Text = GetAppearanceEquipSlotName(Convert.ToInt32(item.SubItems[2].Text));
            textBox_npcappearanceequip_equiptype.Text = item.SubItems[4].Text;
            textBox_npcappearanceequip_red.Text = item.SubItems[6].Text;
            textBox_npcappearanceequip_green.Text = item.SubItems[7].Text;
            textBox_npcappearanceequip_blue.Text = item.SubItems[8].Text;
            textBox_npcappearanceequip_highlightred.Text = item.SubItems[9].Text;
            textBox_npcappearanceequip_highlightgreen.Text = item.SubItems[10].Text;
            textBox_npcappearanceequip_highlightblue.Text = item.SubItems[11].Text;

            string equip_type = GetAppearanceName(Convert.ToInt32(textBox_npcappearanceequip_equiptype.Text));
            if (equip_type.Length > 40)
                equip_type = equip_type.Insert(40, "\r\n");
            label_npcappearanceequip_equiptype.Text = equip_type;

            button_npcappearanceequip_update.Enabled = true;
            button_npcappearanceequip_remove.Enabled = true;
        }

        private void button_npcappearanceequip_insert_Click(object sender, EventArgs e) {
            string spawn_id = GetSelectedSpawnID().ToString();
            int slot_id = GetAppearanceEquipSlotID((string)comboBox_npcappearanceequip_slotid.SelectedItem);
            string equip_type = string.IsNullOrEmpty(textBox_npcappearanceequip_equiptype.Text) ? "1" : textBox_npcappearanceequip_equiptype.Text;
            string red = string.IsNullOrEmpty(textBox_npcappearanceequip_red.Text) ? "0" : textBox_npcappearanceequip_red.Text;
            string green = string.IsNullOrEmpty(textBox_npcappearanceequip_green.Text) ? "0" : textBox_npcappearanceequip_green.Text;
            string blue = string.IsNullOrEmpty(textBox_npcappearanceequip_blue.Text) ? "0" : textBox_npcappearanceequip_blue.Text;
            string highlight_red = string.IsNullOrEmpty(textBox_npcappearanceequip_highlightred.Text) ? "0" : textBox_npcappearanceequip_highlightred.Text;
            string highlight_green = string.IsNullOrEmpty(textBox_npcappearanceequip_highlightgreen.Text) ? "0" : textBox_npcappearanceequip_highlightgreen.Text;
            string highlight_blue = string.IsNullOrEmpty(textBox_npcappearanceequip_highlightblue.Text) ? "0" : textBox_npcappearanceequip_highlightblue.Text;

            int rows = db.RunQuery("INSERT INTO npc_appearance_equip (spawn_id, slot_id, equip_type, red, green, blue, highlight_red, highlight_green, highlight_blue) " +
                                   "VALUES (" + spawn_id + ", " + slot_id + ", " + equip_type + ", " + red + ", " + green + ", " + blue + ", " + highlight_red + ", " + highlight_green + ", " + highlight_blue + ")");

            if (rows > 0) {
                ResetNPCAppearanceEquip(true);
                LoadNPCAppearanceEquip(Convert.ToInt32(spawn_id));
            }
        }

        private void button_npcappearanceequip_update_Click(object sender, EventArgs e) {
            string id = textBox_npcappearanceequip_id.Text;
            string spawn_id = textBox_npcappearanceequip_spawnid.Text;
            int slot_id = GetAppearanceEquipSlotID((string)comboBox_npcappearanceequip_slotid.SelectedItem);
            string equip_type = textBox_npcappearanceequip_equiptype.Text;
            string red = textBox_npcappearanceequip_red.Text;
            string green = textBox_npcappearanceequip_green.Text;
            string blue = textBox_npcappearanceequip_blue.Text;
            string highlight_red = textBox_npcappearanceequip_highlightred.Text;
            string highlight_green = textBox_npcappearanceequip_highlightgreen.Text;
            string highlight_blue = textBox_npcappearanceequip_highlightblue.Text;

            int rows = db.RunQuery("UPDATE npc_appearance_equip " +
                                   "SET id=" + id + ", spawn_id=" + spawn_id + ", slot_id=" + slot_id + ", equip_type=" + equip_type + ", red=" + red + ", green=" + green + ", blue=" + blue + ", highlight_red=" + highlight_red + ", highlight_green=" + highlight_green + ", highlight_blue=" + highlight_blue + " " +
                                   "WHERE npc_appearance_equip.id=" + id);
            if (rows > 0) {
                ResetNPCAppearanceEquip(true);
                LoadNPCAppearanceEquip(Convert.ToInt32(spawn_id));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_npcappearanceequip_remove_Click(object sender, EventArgs e) {
            string id = textBox_npcappearanceequip_id.Text;
            string spawn_id = textBox_npcappearanceequip_spawnid.Text;

            int rows = db.RunQuery("DELETE FROM npc_appearance_equip " +
                                   "WHERE npc_appearance_equip.id=" + id);

            if (rows > 0) {
                ResetNPCAppearanceEquip(true);
                LoadNPCAppearanceEquip(Convert.ToInt32(spawn_id));
            }
        }

        private void ResetNPCAppearanceEquip(bool include_listview) {
            if (include_listview)
                listView_appearance_equip.Items.Clear();

            textBox_npcappearanceequip_id.Clear();
            textBox_npcappearanceequip_spawnid.Clear();
            comboBox_npcappearanceequip_slotid.SelectedItem = null;
            textBox_npcappearanceequip_equiptype.Clear();
            textBox_npcappearanceequip_red.Clear();
            textBox_npcappearanceequip_green.Clear();
            textBox_npcappearanceequip_blue.Clear();
            textBox_npcappearanceequip_highlightred.Clear();
            textBox_npcappearanceequip_highlightgreen.Clear();
            textBox_npcappearanceequip_highlightblue.Clear();
            trackBar_npcappearanceequip_red.Value = 0;
            trackBar_npcappearanceequip_green.Value = 0;
            trackBar_npcappearanceequip_blue.Value = 0;
            trackBar_npcappearanceequip_highlightred.Value = 0;
            trackBar_npcappearanceequip_highlightgreen.Value = 0;
            trackBar_npcappearanceequip_highlightblue.Value = 0;
            pictureBox_npcappearanceequip_color1.BackColor = Color.Black;
            pictureBox_npcappearanceequip_color2.BackColor = Color.Black;

            button_npcappearanceequip_update.Enabled = false;
            button_npcappearanceequip_remove.Enabled = false;
        }

        private void textBox_npcappearanceequip_red_TextChanged(object sender, EventArgs e) {
            if (this.ActiveControl == sender || this.ActiveControl == listView_appearance_equip) {
                try {
                    trackBar_npcappearanceequip_red.Value = Convert.ToInt32(textBox_npcappearanceequip_red.Text);
                }
                catch (Exception ex) {
                    trackBar_npcappearanceequip_red.Value = 0;
                }
            }
            CheckColor(trackBar_npcappearanceequip_red.Value, trackBar_npcappearanceequip_green.Value, trackBar_npcappearanceequip_blue.Value, ref pictureBox_npcappearanceequip_color1);
        }

        private void textBox_npcappearanceequip_green_TextChanged(object sender, EventArgs e) {
            if (this.ActiveControl == sender || this.ActiveControl == listView_appearance_equip) {
                try {
                    trackBar_npcappearanceequip_green.Value = Convert.ToInt32(textBox_npcappearanceequip_green.Text);
                }
                catch (Exception ex) {
                    trackBar_npcappearanceequip_green.Value = 0;
                }
            }
            CheckColor(trackBar_npcappearanceequip_red.Value, trackBar_npcappearanceequip_green.Value, trackBar_npcappearanceequip_blue.Value, ref pictureBox_npcappearanceequip_color1);
        }

        private void textBox_npcappearanceequip_blue_TextChanged(object sender, EventArgs e) {
            if (this.ActiveControl == sender || this.ActiveControl == listView_appearance_equip) {
                try {
                    trackBar_npcappearanceequip_blue.Value = Convert.ToInt32(textBox_npcappearanceequip_blue.Text);
                }
                catch (Exception ex) {
                    trackBar_npcappearanceequip_blue.Value = 0;
                }
            }
            CheckColor(trackBar_npcappearanceequip_red.Value, trackBar_npcappearanceequip_green.Value, trackBar_npcappearanceequip_blue.Value, ref pictureBox_npcappearanceequip_color1);
        }

        private void textBox_npcappearanceequip_highlightred_TextChanged(object sender, EventArgs e) {
            if (this.ActiveControl == sender || this.ActiveControl == listView_appearance_equip) {
                try {
                    trackBar_npcappearanceequip_highlightred.Value = Convert.ToInt32(textBox_npcappearanceequip_highlightred.Text);
                }
                catch (Exception ex) {
                    trackBar_npcappearanceequip_highlightred.Value = 0;
                }
            }
            CheckColor(trackBar_npcappearanceequip_highlightred.Value, trackBar_npcappearanceequip_highlightgreen.Value, trackBar_npcappearanceequip_highlightblue.Value, ref pictureBox_npcappearanceequip_color2);
        }

        private void textBox_npcappearanceequip_highlightgreen_TextChanged(object sender, EventArgs e) {
            if (this.ActiveControl == sender || this.ActiveControl == listView_appearance_equip) {
                try {
                    trackBar_npcappearanceequip_highlightgreen.Value = Convert.ToInt32(textBox_npcappearanceequip_highlightgreen.Text);
                }
                catch (Exception ex) {
                    trackBar_npcappearanceequip_highlightgreen.Value = 0;
                }
            }
            CheckColor(trackBar_npcappearanceequip_highlightred.Value, trackBar_npcappearanceequip_highlightgreen.Value, trackBar_npcappearanceequip_highlightblue.Value, ref pictureBox_npcappearanceequip_color2);
        }

        private void textBox_npcappearanceequip_highlightblue_TextChanged(object sender, EventArgs e) {
            if (this.ActiveControl == sender || this.ActiveControl == listView_appearance_equip) {
                try {
                    trackBar_npcappearanceequip_highlightblue.Value = Convert.ToInt32(textBox_npcappearanceequip_highlightblue.Text);
                }
                catch (Exception ex) {
                    trackBar_npcappearanceequip_highlightblue.Value = 0;
                }
            }
            CheckColor(trackBar_npcappearanceequip_highlightred.Value, trackBar_npcappearanceequip_highlightgreen.Value, trackBar_npcappearanceequip_highlightblue.Value, ref pictureBox_npcappearanceequip_color2);
        }

        private void trackBar_npcappearanceequip_red_Scroll(object sender, EventArgs e) {
            textBox_npcappearanceequip_red.Text = trackBar_npcappearanceequip_red.Value.ToString();
        }

        private void trackBar_npcappearanceequip_green_Scroll(object sender, EventArgs e) {
            textBox_npcappearanceequip_green.Text = trackBar_npcappearanceequip_green.Value.ToString();
        }

        private void trackBar_npcappearanceequip_blue_Scroll(object sender, EventArgs e) {
            textBox_npcappearanceequip_blue.Text = trackBar_npcappearanceequip_blue.Value.ToString(); 
        }

        private void trackBar_npcappearanceequip_highlightred_Scroll(object sender, EventArgs e) {
            textBox_npcappearanceequip_highlightred.Text = trackBar_npcappearanceequip_highlightred.Value.ToString();
        }

        private void trackBar_npcappearanceequip_highlightgreen_Scroll(object sender, EventArgs e) {
            textBox_npcappearanceequip_highlightgreen.Text = trackBar_npcappearanceequip_highlightgreen.Value.ToString();
        }

        private void trackBar_npcappearanceequip_highlightblue_Scroll(object sender, EventArgs e) {
            textBox_npcappearanceequip_highlightblue.Text = trackBar_npcappearanceequip_highlightblue.Value.ToString();
        }

        

        /*************************************************************************************************************************
        *                                             Object
        *************************************************************************************************************************/

        private void LoadObject(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT spawn_objects.id, spawn_objects.spawn_id, spawn_objects.device_id " +
                                                       "FROM spawn_objects " +
                                                       "WHERE spawn_objects.spawn_id=" + spawn_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_object_id.Text = reader.GetString(0);
                    textBox_object_spawnid.Text = reader.GetString(1);
                    textBox_object_deviceid.Text = reader.GetString(2);
                }
                reader.Close();
            }
        }

        private void ResetObject() {
            textBox_object_id.Clear();
            textBox_object_spawnid.Clear();
        }

        /*************************************************************************************************************************
        *                                             Sign
        *************************************************************************************************************************/

        private void LoadSign(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT spawn_signs.id, spawn_signs.spawn_id, spawn_signs.type, spawn_signs.zone_id, spawn_signs.widget_id, spawn_signs.title, spawn_signs.widget_x, spawn_signs.widget_y, spawn_signs.widget_z, spawn_signs.icon, spawn_signs.description, spawn_signs.sign_distance, spawn_signs.zone_x, spawn_signs.zone_y, spawn_signs.zone_y, spawn_signs.zone_heading, spawn_signs.include_heading, spawn_signs.include_location " +
                                                       "FROM spawn_signs " +
                                                       "WHERE spawn_signs.spawn_id=" + spawn_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_sign_id.Text = reader.GetString(0);
                    textBox_sign_spawnid.Text = reader.GetString(1);
                    textBox_sign_type.Text = reader.GetString(2);
                    textBox_sign_zoneid.Text = reader.GetString(3);
                    textBox_sign_widgetid.Text = reader.GetString(4);
                    try {textBox_sign_title.Text = reader.GetString(5);} catch (Exception ex) {textBox_sign_title.Text = "";}
                    textBox_sign_widgetx.Text = reader.GetString(6);
                    textBox_sign_widgety.Text = reader.GetString(7);
                    textBox_sign_widgetz.Text = reader.GetString(8);
                    textBox_sign_icon.Text = reader.GetString(9);
                    textBox_sign_description.Text = reader.GetString(10);
                    textBox_sign_signdistance.Text = reader.GetString(11);
                    textBox_sign_zonex.Text = reader.GetString(12);
                    textBox_sign_zoney.Text = reader.GetString(13);
                    textBox_sign_zonez.Text = reader.GetString(14);
                    textBox_sign_zoneheading.Text = reader.GetString(15);
                    comboBox_sign_includeheading.SelectedItem = Convert.ToBoolean(reader.GetInt32(16)).ToString();
                    comboBox_sign_includelocation.SelectedItem = Convert.ToBoolean(reader.GetInt32(17)).ToString();
                }
                reader.Close();
            }
        }

        private void button_sign_update_Click(object sender, EventArgs e) {
            string id = textBox_sign_id.Text;
            string spawn_id = textBox_sign_spawnid.Text;
            string type = textBox_sign_type.Text;
            string zone_id = textBox_sign_zoneid.Text;
            string widget_id = textBox_sign_widgetid.Text;
            string title = db.RemoveEscapeCharacters(textBox_sign_title.Text);
            string widget_x = textBox_sign_widgetx.Text;
            string widget_y = textBox_sign_widgety.Text;
            string widget_z = textBox_sign_widgetz.Text;
            string icon = textBox_sign_icon.Text;
            string description = db.RemoveEscapeCharacters(textBox_sign_description.Text);
            string sign_distance = textBox_sign_signdistance.Text;
            string zone_x = textBox_sign_zonex.Text;
            string zone_y = textBox_sign_zoney.Text;
            string zone_z = textBox_sign_zonez.Text;
            string zone_heading = textBox_sign_zoneheading.Text;
            int include_heading = Convert.ToInt32(Convert.ToBoolean((string)comboBox_sign_includeheading.SelectedItem));
            int include_location = Convert.ToInt32(Convert.ToBoolean((string)comboBox_sign_includelocation.SelectedItem));

            if (description == "")
                description = null;

            int rows = db.RunQuery("UPDATE spawn_signs " +
                                   "SET id=" + id + ", spawn_id=" + spawn_id + ", type='" + type + "', zone_id=" + zone_id + ", widget_id=" + widget_id + ", title='" + title + "', widget_x=" + widget_x + ", widget_y=" + widget_y + ", widget_z=" + widget_z + ", icon=" + icon + ", description='" + description + "', sign_distance=" + sign_distance + ", zone_x=" + zone_x + ", zone_y=" + zone_y + ", zone_z=" + zone_z + ", zone_heading=" + zone_heading + ", include_heading=" + include_heading + ", include_location=" + include_location + " " +
                                   "WHERE spawn_signs.id=" + id);
            if (rows > 0) {
                ResetSign();
                LoadSign(Convert.ToInt32(spawn_id));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void ResetSign() {
            textBox_sign_id.Clear();
            textBox_sign_spawnid.Clear();
            textBox_sign_type.Clear();
            textBox_sign_zoneid.Clear();
            textBox_sign_widgetid.Clear();
            textBox_sign_title.Clear();
            textBox_sign_widgetx.Clear();
            textBox_sign_widgety.Clear();
            textBox_sign_widgetz.Clear();
            textBox_sign_icon.Clear();
            textBox_sign_description.Clear();
            textBox_sign_signdistance.Clear();
            textBox_sign_zonex.Clear();
            textBox_sign_zoney.Clear();
            textBox_sign_zonez.Clear();
            textBox_sign_zoneheading.Clear();
            comboBox_sign_includeheading.SelectedItem = "False";
            comboBox_sign_includelocation.SelectedItem = "False";
        }

        /*************************************************************************************************************************
        *                                             Widget
        *************************************************************************************************************************/

        private void LoadWidget(int spawn_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT spawn_widgets.id, spawn_widgets.spawn_id, spawn_widgets.widget_id, spawn_widgets.widget_x, spawn_widgets.widget_y, spawn_widgets.widget_z, spawn_widgets.include_heading, spawn_widgets.include_location, spawn_widgets.icon, spawn_widgets.type, spawn_widgets.open_heading, spawn_widgets.closed_heading, spawn_widgets.open_y, spawn_widgets.action_spawn_id, spawn_widgets.open_sound_file, spawn_widgets.close_sound_file, spawn_widgets.open_duration, spawn_widgets.close_y, spawn_widgets.house_id, spawn_widgets.linked_spawn_id " +
                                                       "FROM spawn_widgets " +
                                                       "WHERE spawn_widgets.spawn_id=" + spawn_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_widget_id.Text = reader.GetString(0);
                    textBox_widget_spawnid.Text = reader.GetString(1);
                    textBox_widget_widgetid.Text = reader.GetString(2);
                    textBox_widget_widgetx.Text = reader.GetString(3);
                    textBox_widget_widgety.Text = reader.GetString(4);
                    textBox_widget_widgetz.Text = reader.GetString(5);
                    textBox_widget_includeheading.Text = reader.GetString(6);
                    textBox_widget_includelocation.Text = reader.GetString(7);
                    textBox_widget_icon.Text = reader.GetString(8);
                    textBox_widget_type.Text = reader.GetString(9);
                    textBox_widget_openheading.Text = reader.GetString(10);
                    textBox_widget_closedheading.Text = reader.GetString(11);
                    textBox_widget_openy.Text = reader.GetString(12);
                    textBox_widget_actionspawnid.Text = reader.GetString(13);
                    textBox_widget_opensoundfile.Text = reader.GetString(14);
                    textBox_widget_closesoundfile.Text = reader.GetString(15);
                    textBox_widget_openduration.Text = reader.GetString(16);
                    textBox_widget_closey.Text = reader.GetString(17);
                    textBox_widget_HouseID.Text = reader.GetString(18);
                    textBox_widget_linkedspawnid.Text = reader.GetString(19);
                }
                reader.Close();
            }
        }

        private void button_widget_update_Click(object sender, EventArgs e) {
            string id = textBox_widget_id.Text;
            string spawn_id = textBox_widget_spawnid.Text;
            string widget_id = textBox_widget_widgetid.Text;
            string widget_x = textBox_widget_widgetx.Text;
            string widget_y = textBox_widget_widgety.Text;
            string widget_z = textBox_widget_widgetz.Text;
            string include_heading = textBox_widget_includeheading.Text;
            string include_location = textBox_widget_includelocation.Text;
            string icon = textBox_widget_icon.Text;
            string type = db.RemoveEscapeCharacters(textBox_widget_type.Text);
            string open_heading = textBox_widget_openheading.Text;
            string closed_heading = textBox_widget_closedheading.Text;
            string open_y = textBox_widget_openy.Text;
            string action_spawn_id = textBox_widget_actionspawnid.Text;
            string open_sound_file = db.RemoveEscapeCharacters(textBox_widget_opensoundfile.Text);
            string close_sound_file = db.RemoveEscapeCharacters(textBox_widget_closesoundfile.Text);
            string open_duration = textBox_widget_openduration.Text;
            string close_y = textBox_widget_closey.Text;
            string linked_spawn_id = textBox_widget_linkedspawnid.Text;
            string house_id = textBox_widget_HouseID.Text;

            int rows = db.RunQuery("UPDATE spawn_widgets " +
                                   "SET id=" + id + ", spawn_id=" + spawn_id + ", widget_id=" + widget_id + ", widget_x=" + widget_x + ", widget_y=" + widget_y + ", widget_z=" + widget_z + ", include_heading=" + include_heading + ", include_location=" + include_location + ", icon=" + icon + ", type='" + type + "', open_heading=" + open_heading + ", closed_heading=" + closed_heading + ", open_y=" + open_y + ", action_spawn_id=" + action_spawn_id + ", open_sound_file='" + open_sound_file + "', close_sound_file='" + close_sound_file + "', open_duration=" + open_duration + ", close_y=" + close_y + ", house_id=" + house_id + ", linked_spawn_id=" + linked_spawn_id + " " +
                                   "WHERE spawn_widgets.id=" + id);
            if (rows > 0) {
                ResetWidget();
                LoadWidget(Convert.ToInt32(spawn_id));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void ResetWidget() {
            textBox_widget_id.Clear();
            textBox_widget_spawnid.Clear();
            textBox_widget_widgetid.Clear();
            textBox_widget_widgetx.Clear();
            textBox_widget_widgety.Clear();
            textBox_widget_widgetz.Clear();
            textBox_widget_includeheading.Clear();
            textBox_widget_includelocation.Clear();
            textBox_widget_icon.Clear();
            textBox_widget_type.Clear();
            textBox_widget_openheading.Clear();
            textBox_widget_closedheading.Clear();
            textBox_widget_openy.Clear();
            textBox_widget_actionspawnid.Clear();
            textBox_widget_opensoundfile.Clear();
            textBox_widget_closesoundfile.Clear();
            textBox_widget_openduration.Clear();
            textBox_widget_closey.Clear();
            textBox_widget_HouseID.Clear();
            textBox_widget_linkedspawnid.Clear();
        }

        /*************************************************************************************************************************
        *                                             Ground
        *************************************************************************************************************************/


        #region "Ground Spawns"


        private void LoadGround(int spawn_id)
        {
            int groundspawn_entry_id = 0;

            MySqlDataReader reader = db.RunSelectQuery("SELECT s.id, s.spawn_id, s.number_harvests, s.num_attempts_per_harvest, s.groundspawn_id, s.collection_skill " +
                                                       "FROM spawn_ground s " +
                                                       "WHERE s.spawn_id=" + spawn_id);
            if (reader != null)
            {
                if (reader.Read())
                {
                    textBox_ground_id.Text = reader.GetString(0);
                    textBox_ground_numberharvests.Text = reader.GetString(2);
                    textBox_ground_numattemptsperharvest.Text = reader.GetString(3);
                    textBox_ground_groundspawnid.Text = reader.GetString(4);
                    comboBox_ground_collection_skill.SelectedItem = reader.GetString(5);
                    groundspawn_entry_id = reader.GetInt32(4);
                }
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT g.id, g.groundspawn_id, g.item_id, CONCAT('(',i.id,') ',i.name), g.is_rare, g.grid_id " +
                                       "FROM groundspawn_items g " +
                                       "INNER JOIN " +
                                       "items i ON g.item_id=i.id " +
                                       "WHERE g.groundspawn_id=" + groundspawn_entry_id);
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

                    listView_groundspawn_items.Items.Add(item);
                }
                reader.Close();
            }

            reader = db.RunSelectQuery("SELECT g.id, g.groundspawn_id, g.min_skill_level, g.min_adventure_level, g.bonus_table, g.harvest1, g.harvest3, g.harvest5, g.harvest_imbue, g.harvest_rare, g.harvest10, g.harvest_coin, g.enabled, g.tablename " +
                                        "FROM groundspawns g " +                        
                                        "WHERE groundspawn_id=" + groundspawn_entry_id);

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
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(8)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(9)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(10)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(11)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(12)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(13)));

                    listView_groundspawn.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void button_ground_update_Click(object sender, EventArgs e)
        {
            string id = textBox_ground_id.Text;
            string number_harvests = textBox_ground_numberharvests.Text;
            string num_attempts_per_harvest = textBox_ground_numattemptsperharvest.Text;
            string groundspawn_entry_id = textBox_ground_groundspawnid.Text;
            string collection_skill = comboBox_ground_collection_skill.SelectedItem.ToString();

            int rows = db.RunQuery("UPDATE spawn_ground " +
                                   "SET number_harvests=" + number_harvests + ", num_attempts_per_harvest=" + num_attempts_per_harvest + ", groundspawn_id=" + groundspawn_entry_id + ", collection_skill='" + collection_skill + "' " +
                                   "WHERE spawn_ground.id=" + id);
            if (rows > 0)
            {
                ResetGround(true, "all");
                LoadGround(GetSelectedSpawnID());
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_groundspawnitems_insert_Click(object sender, EventArgs e)
        {
            string groundspawn_id = textBox_ground_groundspawnid.Text;
            string item_id = string.IsNullOrEmpty(textBox_groundspawnitems_itemid.Text) ? "1" : textBox_groundspawnitems_itemid.Text;
            string rare = string.IsNullOrEmpty(textBox_groundspawnitems_rare.Text) ? "0" : textBox_groundspawnitems_rare.Text;
            string grid_id = string.IsNullOrEmpty(textBox_groundspawnitems_gridid.Text) ? "0" : textBox_groundspawnitems_gridid.Text;

            int rows = db.RunQuery("INSERT INTO groundspawn_items (groundspawn_id, item_id, is_rare, grid_id) " +
                                   "VALUES (" + groundspawn_id + ", " + item_id + ", " + rare + ", " + grid_id + ")");
            if (rows > 0)
            {
                ResetGround(true, "groundspawn_item");
                LoadGround(Convert.ToInt32(GetSelectedSpawnID().ToString()));
            }
        }

        private void listView_groundspawn_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_groundspawn_items.SelectedIndices.Count == 0 || listView_groundspawn_items.SelectedIndices[0] == -1)
            {
                ResetGround(false, "groundspawn_item");
                return;
            }

            ListViewItem item = listView_groundspawn_items.Items[listView_groundspawn_items.SelectedIndices[0]];
            textBox_groundspawnitems_id.Text = item.Text;
            textBox_groundspawnitems_groundspawnid.Text = item.SubItems[1].Text;
            textBox_groundspawnitems_itemid.Text = item.SubItems[2].Text;
            textBox_groundspawnitems_rare.Text = item.SubItems[4].Text;
            textBox_groundspawnitems_gridid.Text = item.SubItems[5].Text;

            button_groundspawnitems_insert.Enabled = true;
            button_groundspawnitems_update.Enabled = true;
            button_groundspawnitems_remove.Enabled = true;
        }

        private void listView_groundspawn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_groundspawn.SelectedIndices.Count == 0 || listView_groundspawn.SelectedIndices[0] == -1)
            {
                ResetGround(false, "groundspawn");
                return;
            }

            ListViewItem item = listView_groundspawn.Items[listView_groundspawn.SelectedIndices[0]];

            textBox_ground_id.Text = item.Text;
            textBox_groundspawn_groundspawn_id.Text = item.SubItems[1].Text;
            textBox_groundspawn_minskill.Text = item.SubItems[2].Text;
            textBox_groundspawn_adv_level.Text = item.SubItems[3].Text;
            textBox_groundspawn_bonus_table.Text = item.SubItems[4].Text;
            textBox_groundspawn_harvest1.Text= item.SubItems[5].Text;
            textBox_groundspawn_harvest3.Text= item.SubItems[6].Text;
            textBox_groundspawn_harvest5.Text = item.SubItems[7].Text;
            textBox_groundspawn_harvest_imbue.Text= item.SubItems[8].Text;
            textBox_groundspawn_harvest_rare.Text= item.SubItems[9].Text;
            textBox_groundspawn_harvest10.Text= item.SubItems[10].Text;
            textBox_groundspawn_harvest_coin.Text= item.SubItems[11].Text;
            textBox_groundspawn_harvest_enabled.Text = item.SubItems[12].Text;
            textBox_groundspawn_table_name.Text = item.SubItems[13].Text;


            button_groundspawn_insert.Enabled = true;
            button_groundspawn_update.Enabled = true;
            button_groundspawn_remove.Enabled = true;
        }

        private void button_groundspawnitems_update_Click(object sender, EventArgs e)
        {
            string id = textBox_groundspawnitems_id.Text;
            string groundspawn_entry_id = textBox_groundspawnitems_groundspawnid.Text;
            string item_id = textBox_groundspawnitems_itemid.Text;
            string is_rare = textBox_groundspawnitems_rare.Text;
            string grid_id = textBox_groundspawnitems_gridid.Text;

            int rows = db.RunQuery("UPDATE groundspawn_items " +
                                   "SET item_id=" + item_id + ", is_rare=" + is_rare + ", grid_id=" + grid_id + " " +
                                   "WHERE groundspawn_items.id=" + id);
            if (rows > 0)
            {
                ResetGround(true, "all");
                LoadGround(Convert.ToInt32(GetSelectedSpawnID().ToString()));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private int GetNextGroundspawnId()
        {
            int new_groundspawn_id = -1;
            MySqlDataReader reader = db.RunSelectQuery("SELECT MAX(groundspawn_id) " +
                                                       "FROM groundspawn_items");
            if (reader != null)
            {
                if (reader.Read())
                    new_groundspawn_id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            return new_groundspawn_id;
        }

        private void button_groundspawnbonus_update_Click(object sender, EventArgs e)
        {
           string id = textBox_ground_id.Text;
           string groundspawn_groundspawn_id = textBox_groundspawn_groundspawn_id.Text;
           string groundspawn_minskill = textBox_groundspawn_minskill.Text;
           string groundspawn_adv_level = textBox_groundspawn_adv_level.Text;
           string groundspawn_bonus_table = textBox_groundspawn_bonus_table.Text;
           string groundspawn_harvest1 = textBox_groundspawn_harvest1.Text;
           string groundspawn_harvest3 = textBox_groundspawn_harvest3.Text;
           string groundspawn_harvest5 = textBox_groundspawn_harvest5.Text;
           string groundspawn_harvest_imbue = textBox_groundspawn_harvest_imbue.Text;
           string groundspawn_harvest_rare = textBox_groundspawn_harvest_rare.Text;
           string groundspawn_harvest10 = textBox_groundspawn_harvest10.Text;
           string groundspawn_harvest_coin = textBox_groundspawn_harvest_coin.Text;
           string groundspawn_harvest_enabled = textBox_groundspawn_harvest_enabled.Text;
           string groundspawn_table_name = textBox_groundspawn_table_name.Text;

           int rows = db.RunQuery("UPDATE groundspawns " +
                                  "SET min_skill_level=" + groundspawn_minskill + ", min_adventure_level=" + groundspawn_minskill + ", bonus_table=" + groundspawn_bonus_table + ", harvest1=" + groundspawn_harvest1 + ", harvest3=" + groundspawn_harvest3 + ", " +
                                  "harvest5=" + groundspawn_harvest5 + ", harvest_imbue=" + groundspawn_harvest_imbue + ", harvest_rare=" + groundspawn_harvest_rare + ", harvest10=" + groundspawn_harvest10 + ", harvest_coin=" + groundspawn_harvest_coin + ", enabled=" + groundspawn_harvest_enabled + ", tablename='" + groundspawn_table_name + "' " +
                                  "WHERE id = " + id);
           if (rows > 0)
           {
               ResetGround(true, "all");
               LoadGround(Convert.ToInt32(GetSelectedSpawnID().ToString()));
               System.Media.SystemSounds.Beep.Play();
           }
        }

        private void button_groundspawnitems_item_search_Click(object sender, EventArgs e)
        {
            var Form_ItemSearch = new Form_ItemSearch(db);
            Form_ItemSearch.ShowDialog();

            string item_id = Form_ItemSearch.ReturnId;

            textBox_groundspawnitems_itemid.Text = item_id;
        }

        private void button_groundspawnitems_remove_Click(object sender, EventArgs e)
        {
            string id = textBox_groundspawnitems_id.Text;

            int rows = db.RunQuery("DELETE FROM groundspawn_items " +
                                   "WHERE id=" + id);

            if (rows > 0)
            {
                ResetGround(true, "groundspawn_item");
                LoadGround(Convert.ToInt32(GetSelectedSpawnID().ToString()));
            }
        }

        private void button_groundspawnbonus_remove_Click(object sender, EventArgs e)
        {
            string id = textBox_npcappearanceequip_id.Text;

            int rows = db.RunQuery("DELETE FROM groundspawn_bonus " +
                                   "WHERE groundspawn_bonus.id=" + id);

            if (rows > 0)
            {
                ResetGround(true, "groundspawn");
                LoadGround(Convert.ToInt32(GetSelectedSpawnID().ToString()));
            }
        }

        private void ResetGround(bool include_listview, string table)
        {
            if (table.Equals("groundspawn_item") || table.Equals("both") || table.Equals("all"))
            {
                if (include_listview)
                    listView_groundspawn_items.Items.Clear();

                textBox_groundspawnitems_id.Clear();
                textBox_groundspawnitems_groundspawnid.Clear();
                textBox_groundspawnitems_itemid.Clear();
                textBox_groundspawnitems_rare.Clear();
                textBox_groundspawnitems_gridid.Clear();
                //textBox_groundspawnitems_minrarelevel.Clear();
                //textBox_groundspawnitems_rareitem.Clear();
                //textBox_groundspawnitems_triggersbonuscheck.Clear();

                button_groundspawnitems_update.Enabled = false;
                button_groundspawnitems_remove.Enabled = false;
            }
            if (table.Equals("groundspawn") || table.Equals("both") || table.Equals("all"))
            {
                if (include_listview)
                    listView_groundspawn.Items.Clear();

                textBox_groundspawn_groundspawn_id.Clear();
                textBox_groundspawn_minskill.Clear();
                textBox_groundspawn_adv_level.Clear();
                textBox_groundspawn_bonus_table.Clear();
                textBox_groundspawn_harvest1.Clear();
                textBox_groundspawn_harvest3.Clear();
                textBox_groundspawn_harvest5.Clear();
                textBox_groundspawn_harvest_imbue.Clear(); ;
                textBox_groundspawn_harvest_rare.Clear();
                textBox_groundspawn_harvest10.Clear();
                textBox_groundspawn_harvest_coin.Clear();
                textBox_groundspawn_harvest_enabled.Clear();
                textBox_groundspawn_table_name.Clear();


                button_groundspawn_update.Enabled = false;
                button_groundspawn_remove.Enabled = false;
            }
            if (table.Equals("none") || table.Equals("all"))
            {
                textBox_ground_id.Clear();
                //textBox_ground_spawnid.Clear();
                textBox_ground_numberharvests.Clear();
                textBox_ground_numattemptsperharvest.Clear();
                textBox_ground_groundspawnid.Clear();
                comboBox_ground_collection_skill.SelectedIndex = -1;
            }
        }

        #endregion

    

        /*************************************************************************************************************************
        *                                             Merchant
        *************************************************************************************************************************/

        private void LoadMerchant(int merchant_id) {
            ArrayList inventory_ids = new ArrayList();
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, merchant_id, inventory_id, description " +
                                                       "FROM merchants " +
                                                       "WHERE merchant_id=" + merchant_id);
            if (reader != null) {
                if (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    inventory_ids.Add(reader.GetInt32(2));
                    listView_merchants.Items.Add(item);
                }
                reader.Close();
            }

            for (int i = 0; i < inventory_ids.Count; i++) {
                reader = db.RunSelectQuery("SELECT mi.id, inventory_id, item_id, quantity, price_item_id, price_item_qty, price_item2_id, price_item2_qty, price_status, price_coins, price_stationcash, i.name " +
                                           "FROM merchant_inventory mi " +
                                           "INNER JOIN " +
                                           "items i ON mi.item_id = i.id " +
                                           "WHERE inventory_id=" + (int)inventory_ids[i]);
                if (reader != null) {
                    while (reader.Read()) {
                        ListViewItem item = new ListViewItem(reader.GetString(0));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(5)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(6)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(7)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(8)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(9)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(10)));
                        item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(11)));
                        listView_merchantinventory.Items.Add(item);
                    }
                    reader.Close();
                }
            }
        }

        private void listView_merchants_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_merchants.SelectedIndices.Count == 0 || listView_merchants.SelectedIndices[0] == -1) {
                button_merchantinventory_insert.Enabled = false;
                ResetMerchant(false, "merchants");
                return;
            }

            ListViewItem item = listView_merchants.Items[listView_merchants.SelectedIndices[0]];
            textBox_merchant_id.Text = item.Text;
            textBox_merchant_merchantid.Text = item.SubItems[1].Text;
            textBox_merchant_inventoryid.Text = item.SubItems[2].Text;
            textBox_merchant_description.Text = item.SubItems[3].Text;

            button_merchantinventory_insert.Enabled = true;
            button_merchant_insert.Enabled = true;
            button_merchant_update.Enabled = true;
            button_merchant_remove.Enabled = true;
        }

        private void listView_merchantinventory_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_merchantinventory.SelectedIndices.Count == 0 || listView_merchantinventory.SelectedIndices[0] == -1) {
                ResetMerchant(false, "inventory");
                return;
            }

            ListViewItem item = listView_merchantinventory.Items[listView_merchantinventory.SelectedIndices[0]];
            textBox_merchantinventory_id.Text = item.Text;
            textBox_merchantinventory_inventoryid.Text = item.SubItems[1].Text;
            textBox_merchantinventory_itemid.Text = item.SubItems[2].Text;
            textBox_merchantinventory_quantity.Text = item.SubItems[3].Text;
            textBox_merchantinventory_price_item1_id.Text = item.SubItems[4].Text;
            textBox_merchantinventory_price_item1_qty.Text = item.SubItems[5].Text;
            textBox_merchantinventory_price_item2_id.Text = item.SubItems[6].Text;
            textBox_merchantinventory_price_item2_qty.Text = item.SubItems[7].Text;
            textBox_merchantinventory_price_status.Text = item.SubItems[8].Text;
            textBox_merchantinventory_price_coins.Text = item.SubItems[9].Text;
            textBox_merchantinventory_price_stationcash.Text = item.SubItems[10].Text;

            button_merchantinventory_insert.Enabled = true;
            button_merchantinventory_update.Enabled = true;
            button_merchantinventory_remove.Enabled = true;
        }

        private void button_merchant_insert_Click(object sender, EventArgs e) {
            int merchant_id = GetMerchantID(GetSelectedSpawnID());

            if (merchant_id==0)
            {
                merchant_id = GetNextMerchantID();
            }

            string inventory_id = string.IsNullOrEmpty(textBox_merchant_inventoryid.Text) ? "0" : GetNextMerchantInventoryID(merchant_id).ToString();

            string description = string.IsNullOrEmpty(db.RemoveEscapeCharacters(textBox_merchant_description.Text)) ? textBox_spawn_name.Text : textBox_merchant_description.Text;

            int rows = db.RunQuery("INSERT INTO merchants (merchant_id, inventory_id, description) " +
                                   "VALUES (" + merchant_id + ", " + inventory_id + ", '" + description + "')");

            db.RunQuery("UPDATE spawn SET merchant_id=" + merchant_id + " WHERE id=" + GetSelectedSpawnID());
                                  
            if (rows > 0) {
                ResetMerchant(true, "all");
                LoadMerchant(merchant_id);
                LoadSpawn(GetSelectedSpawnID());
            }
        }

        private void button_merchantinventory_insert_Click(object sender, EventArgs e) {
            if (listView_merchants.Items.Count == 1 && listView_merchants.SelectedIndices.Count == 0)
            {
                listView_merchants.Items[0].Selected = true;
            }

            if (listView_merchants.SelectedIndices.Count == 0 || listView_merchants.SelectedIndices[0] == -1)
            {
                MessageBox.Show("Please select a merchant inventory");
                return;
            }

            string inventory_id = string.IsNullOrEmpty(textBox_merchant_inventoryid.Text) ? "0" : textBox_merchant_inventoryid.Text;
            string item_id = string.IsNullOrEmpty(textBox_merchantinventory_itemid.Text) ? "1" : textBox_merchantinventory_itemid.Text;
            string quantity = string.IsNullOrEmpty(textBox_merchantinventory_quantity.Text) ? "65535" : textBox_merchantinventory_quantity.Text;
            string price_item1_id = string.IsNullOrEmpty(textBox_merchantinventory_price_item1_id.Text) ? "0" : textBox_merchantinventory_price_item1_id.Text;
            string price_item1_qty = string.IsNullOrEmpty(textBox_merchantinventory_price_item1_qty.Text) ? "0" : textBox_merchantinventory_price_item1_qty.Text;
            string price_item2_id = string.IsNullOrEmpty(textBox_merchantinventory_price_item2_id.Text) ? "0" : textBox_merchantinventory_price_item2_id.Text;
            string price_item2_qty = string.IsNullOrEmpty(textBox_merchantinventory_price_item2_qty.Text) ? "0" : textBox_merchantinventory_price_item2_qty.Text;
            string price_status = string.IsNullOrEmpty(textBox_merchantinventory_price_status.Text) ? "0" : textBox_merchantinventory_price_status.Text;
            string price_coins = string.IsNullOrEmpty(textBox_merchantinventory_price_coins.Text) ? "0" : textBox_merchantinventory_price_coins.Text;
            string price_stationcash = string.IsNullOrEmpty(textBox_merchantinventory_price_stationcash.Text) ? "0" : textBox_merchantinventory_price_stationcash.Text;

            int rows = db.RunQuery("INSERT INTO merchant_inventory (inventory_id, item_id, quantity, price_item_id, price_item_qty, price_item2_id, price_item2_qty, price_status, price_coins, price_stationcash) " +
                                   "VALUES (" + inventory_id + ", " + item_id + ", " + quantity + ", " + price_item1_id + ", " + price_item1_qty + ", " + price_item2_id + ", " + price_item2_qty +
                                   ", " + price_status + ", " + price_coins + ", " + price_stationcash + ")");
            if (rows > 0) {
                ResetMerchant(true, "all");
                LoadMerchant(GetMerchantID(GetSelectedSpawnID()));
            }
        }

        private void button_merchant_update_Click(object sender, EventArgs e) {
            string id = textBox_merchant_id.Text;
            int merchant_id = GetMerchantID(GetSelectedSpawnID());
            string inventory_id = textBox_merchant_inventoryid.Text;
            string description = db.RemoveEscapeCharacters(textBox_merchant_description.Text);

            int rows = db.RunQuery("UPDATE merchants " +
                                   "SET id=" + id + ", merchant_id=" + merchant_id + ", inventory_id=" + inventory_id + ", description='" + description + "' " +
                                   "WHERE id=" + id + ";UPDATE spawn SET merchant_id=" + merchant_id + " WHERE id=" + GetSelectedSpawnID());
            if (rows > 0) {
                ResetMerchant(true, "all");
                LoadMerchant(Convert.ToInt32(merchant_id));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_merchantinventory_update_Click(object sender, EventArgs e) {
            string id = string.IsNullOrEmpty(textBox_merchantinventory_id.Text) ? "0" : textBox_merchantinventory_id.Text;
            string inventory_id = string.IsNullOrEmpty(textBox_merchantinventory_inventoryid.Text) ? "0" : textBox_merchantinventory_inventoryid.Text;
            string item_id = string.IsNullOrEmpty(textBox_merchantinventory_itemid.Text) ? "0" : textBox_merchantinventory_itemid.Text;
            string quantity = string.IsNullOrEmpty(textBox_merchantinventory_quantity.Text) ? "0" : textBox_merchantinventory_quantity.Text;
            string price_item1_id = string.IsNullOrEmpty(textBox_merchantinventory_price_item1_id.Text) ? "0" : textBox_merchantinventory_price_item1_id.Text;
            string price_item1_qty = string.IsNullOrEmpty(textBox_merchantinventory_price_item1_qty.Text) ? "0" : textBox_merchantinventory_price_item1_qty.Text;
            string price_item2_id = string.IsNullOrEmpty(textBox_merchantinventory_price_item2_id.Text) ? "0" : textBox_merchantinventory_price_item2_id.Text;
            string price_item2_qty = string.IsNullOrEmpty(textBox_merchantinventory_price_item2_qty.Text) ? "0" : textBox_merchantinventory_price_item2_qty.Text;
            string price_status = string.IsNullOrEmpty(textBox_merchantinventory_price_status.Text) ? "0" : textBox_merchantinventory_price_status.Text;
            string price_coins = string.IsNullOrEmpty(textBox_merchantinventory_price_coins.Text) ? "0" : textBox_merchantinventory_price_coins.Text;
            string price_stationcash = string.IsNullOrEmpty(textBox_merchantinventory_price_stationcash.Text) ? "0" : textBox_merchantinventory_price_stationcash.Text;

            int rows = db.RunQuery("UPDATE merchant_inventory " +
                                   "SET id=" + id + ", inventory_id=" + inventory_id + ", item_id=" + item_id + ", quantity=" + quantity + ", price_item_id=" + price_item1_id + ",price_item_qty=" + price_item1_qty + 
                                   ", price_item2_id=" + price_item2_id + ", price_item2_qty=" + price_item2_qty + ", price_status=" + price_status + ", price_coins=" + price_coins + ", price_stationcash=" + price_stationcash + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetMerchant(true, "all");
                LoadMerchant(GetMerchantID(GetSelectedSpawnID()));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_merchant_remove_Click(object sender, EventArgs e) {
            string id = textBox_merchant_id.Text;

            int rows = db.RunQuery("DELETE FROM merchants " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetMerchant(true, "all");
               // LoadMerchant(Convert.ToInt32(textBox_merchant_merchantid.Text));
            }
        }

        private void button_merchantinventory_remove_Click(object sender, EventArgs e) {
            string id = textBox_merchantinventory_id.Text;

            int rows = db.RunQuery("DELETE FROM merchant_inventory " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetMerchant(true, "all");
                LoadMerchant(GetMerchantID(GetSelectedSpawnID()));
            }
        }

        private void ResetMerchant(bool include_listview, string table) {
            if (table == "all" || table == "merchants") {
                if (include_listview)
                    listView_merchants.Items.Clear();
                textBox_merchant_id.Clear();
                textBox_merchant_inventoryid.Clear();
                textBox_merchant_merchantid.Clear();
                textBox_merchant_description.Clear();
                button_merchant_update.Enabled = false;
                button_merchant_remove.Enabled = false;
            }

            if (table == "all" || table == "inventory") {
                if (include_listview)
                    listView_merchantinventory.Items.Clear();
                textBox_merchantinventory_id.Clear();
                textBox_merchantinventory_inventoryid.Clear();
                textBox_merchantinventory_itemid.Clear();
                textBox_merchantinventory_quantity.Clear();
                textBox_merchantinventory_price_item1_id.Clear();
                textBox_merchantinventory_price_item1_qty.Clear();
                textBox_merchantinventory_price_item2_id.Clear();
                textBox_merchantinventory_price_item2_qty.Clear();
                textBox_merchantinventory_price_status.Clear();
                textBox_merchantinventory_price_coins.Clear();
                textBox_merchantinventory_price_stationcash.Clear();

                button_merchantinventory_update.Enabled = false;
                button_merchantinventory_remove.Enabled = false;
            }
        }

        /*************************************************************************************************************************
        *                                             Misc
        *************************************************************************************************************************/

        private void CheckColor(int red, int green, int blue, ref PictureBox color_box) {
            Color color = Color.FromArgb(red, green, blue);
            color_box.BackColor = color;
        }

        private void button_state_lookup2_Click(object sender, EventArgs e) {
            new Form_StateLookup(db).Show();
        }

        private void button_state_lookup1_Click(object sender, EventArgs e) {
            new Form_StateLookup(db).Show();
        }

        private void linkLabel_modeltypelookup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("http://eq2emu-reference.wetpaint.com/page/Category%3ACreature+Masterlist");
        }

        private void button_duplicatespawn_Click(object sender, EventArgs e) {
            new Form_DuplicateSpawn((string)comboBox_select_type.SelectedItem, GetSelectedSpawnID(), GetSelectedZoneID(), ref db).ShowDialog();
            PopulateSpawnsComboBox();
        }

        private void button_close_Click(object sender, EventArgs e) {
            owner.Dispose();
        }

        private void button_refresh_Click(object sender, EventArgs e) {
            string zone = (string)comboBox_select_zone.SelectedItem;
            string type = (string)comboBox_select_type.SelectedItem;
            string spawn = (string)comboBox_select_spawn.SelectedItem;
            comboBox_select_zone.SelectedItem = null;
            comboBox_select_type.SelectedItem = null;
            comboBox_select_spawn.SelectedItem = null;
            comboBox_select_zone.SelectedItem = zone;
            comboBox_select_type.SelectedItem = type;
            comboBox_select_spawn.SelectedItem = spawn;
        }

        private int GetNextSpawnLocationID()
        {
            int new_spawn_location_id = -1;
            MySqlDataReader reader = db.RunSelectQuery("SELECT IFNULL(MAX(spawn_location_id),1) " +
                                                       "FROM spawn_location_entry WHERE spawn_location_id;");
            if (reader != null)
            {
                if (reader.Read())
                    new_spawn_location_id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            return new_spawn_location_id;
        }

        private int GetNextLootTableID()
        {
            int new_loot_table_id = -1;
            MySqlDataReader reader = db.RunSelectQuery("SELECT IFNULL(MAX(id), 0) " +
                                                       "FROM loottable");
            if (reader != null)
            {
                if (reader.Read())
                    new_loot_table_id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            return new_loot_table_id;
        }

        private int GetNextLootDropID()
        {
            int new_loot_drop_id = -1;
            MySqlDataReader reader = db.RunSelectQuery("SELECT IFNULL(MAX(id), 0) " +
                                                       "FROM lootdrop");
            if (reader != null)
            {
                if (reader.Read())
                    new_loot_drop_id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            return new_loot_drop_id;
        }

        private int GetNextSpawnLocationPlacementID()
        {
            int new_spawn_location_placement_id = -1;
            MySqlDataReader reader = db.RunSelectQuery("SELECT IFNULL(MAX(id), 0) " +
                                                       "FROM spawn_location_placement;");
            if (reader != null)
            {
                if (reader.Read())
                    new_spawn_location_placement_id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            return new_spawn_location_placement_id;
        }

        private int GetNextSpawnLocationEntryID()
        {
            int new_spawn_location_entry_id = -1;
            string sql = "SELECT IFNULL(MAX(id), 0) FROM spawn_location_entry;";
            MySqlDataReader reader = db.RunSelectQuery(sql);

            if (reader != null)
            {
                if (reader.Read())
                    new_spawn_location_entry_id = reader.GetInt32(0) + 1;
                reader.Close();
            }
            return new_spawn_location_entry_id;
        }

        private int GetNextMerchantID()
        {
            int merchant_id = 0;
            MySqlDataReader reader = db.RunSelectQuery("SELECT IFNULL(MAX(merchant_id),0)+1 " +
                                                       "FROM merchants ");
            if (reader != null)
            {
                if (reader.Read())
                    merchant_id = reader.GetInt32(0);
                reader.Close();
            }
            return merchant_id;
        }

        private int GetNextMerchantInventoryID(int merchant_id)
        {
            int inventory_id = 0;
            MySqlDataReader reader = db.RunSelectQuery("SELECT IFNULL(MAX(inventory_id),0)+1 " +
                                                       "FROM merchants " +
                                                       "WHERE merchant_id=" + merchant_id);
            if (reader != null)
            {
                if (reader.Read())
                    inventory_id = reader.GetInt32(0);
                reader.Close();
            }
            return inventory_id;
        }

        private void NPCAppearanceChange(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            trackBar_npcappearance_red.Value = pb.BackColor.R;
            trackBar_npcappearance_blue.Value = pb.BackColor.B;
            trackBar_npcappearance_green.Value = pb.BackColor.G;

            textBox_npcappearance_red.Text = pb.BackColor.R.ToString();
            textBox_npcappearance_blue.Text = pb.BackColor.B.ToString();
            textBox_npcappearance_green.Text = pb.BackColor.G.ToString();

            CheckColor(trackBar_npcappearance_red.Value, trackBar_npcappearance_green.Value, trackBar_npcappearance_blue.Value, ref pictureBox_npcappearance_color);
        }

        private void NPCAppearanceEquipChange(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            trackBar_npcappearanceequip_red.Value = pb.BackColor.R;
            trackBar_npcappearanceequip_blue.Value = pb.BackColor.B;
            trackBar_npcappearanceequip_green.Value = pb.BackColor.G;

            textBox_npcappearanceequip_red.Text = pb.BackColor.R.ToString();
            textBox_npcappearanceequip_blue.Text = pb.BackColor.B.ToString();
            textBox_npcappearanceequip_green.Text = pb.BackColor.G.ToString();

            CheckColor(trackBar_npcappearanceequip_red.Value, trackBar_npcappearanceequip_green.Value, trackBar_npcappearanceequip_blue.Value, ref pictureBox_npcappearanceequip_color1);
        }

        private void NPCAppearanceEquiHighlightChange(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            trackBar_npcappearanceequip_highlightred.Value = pb.BackColor.R;
            trackBar_npcappearanceequip_highlightblue.Value = pb.BackColor.B;
            trackBar_npcappearanceequip_highlightgreen.Value = pb.BackColor.G;

            textBox_npcappearanceequip_highlightred.Text = pb.BackColor.R.ToString();
            textBox_npcappearanceequip_highlightblue.Text = pb.BackColor.B.ToString();
            textBox_npcappearanceequip_highlightgreen.Text = pb.BackColor.G.ToString();

            CheckColor(trackBar_npcappearanceequip_highlightred.Value, trackBar_npcappearanceequip_highlightgreen.Value, trackBar_npcappearanceequip_highlightblue.Value, ref pictureBox_npcappearanceequip_color2);
        }

        private void btnInititalStateLookup_Click(object sender, EventArgs e)
        {
            var Form_InititalStateLookup = new Form_StateLookup(db);
            Form_InititalStateLookup.ShowDialog();
            textBox_spawn_npc_initialstate.Text = string.IsNullOrEmpty(Form_InititalStateLookup.ReturnValue) ? "0" : Form_InititalStateLookup.ReturnValue;
        }

        private void btnActionStateLookup_Click(object sender, EventArgs e)
        {
            var Form_ActionStateLookup = new Form_StateLookup(db);
            Form_ActionStateLookup.ShowDialog();
            textBox_spawn_npc_actionstate.Text = string.IsNullOrEmpty(Form_ActionStateLookup.ReturnValue) ? "0" : Form_ActionStateLookup.ReturnValue;
        }

        private void btnMoodStateLookup_Click(object sender, EventArgs e)
        {
            var Form_ActionStateLookup = new Form_StateLookup(db);
            Form_ActionStateLookup.ShowDialog();
            textBox_spawn_npc_moodstate.Text = string.IsNullOrEmpty(Form_ActionStateLookup.ReturnValue) ? "0" : Form_ActionStateLookup.ReturnValue;
        }

        private void btnEmoteStateLookup_Click(object sender, EventArgs e)
        {
            var Form_ActionStateLookup = new Form_StateLookup(db);
            Form_ActionStateLookup.ShowDialog();
            textBox_spawn_npc_emotestate.Text = string.IsNullOrEmpty(Form_ActionStateLookup.ReturnValue) ? "0" : Form_ActionStateLookup.ReturnValue;
        }

        private void btnVisualState_Click(object sender, EventArgs e)
        {
            var Form_ActionStateLookup = new Form_StateLookup(db);
            Form_ActionStateLookup.ShowDialog();
            textBox_spawn_visualstate.Text = string.IsNullOrEmpty(Form_ActionStateLookup.ReturnValue) ? "0" : Form_ActionStateLookup.ReturnValue;
        }

        private void btnEquipTypeLookup_Click(object sender, EventArgs e)
        {
            var Form_EquipTypeLookup = new Form_AppearanceTypeLookup(db);
            Form_EquipTypeLookup.ShowDialog();
            textBox_npcappearanceequip_equiptype.Text = string.IsNullOrEmpty(Form_EquipTypeLookup.ReturnValue) ? "0" : Form_EquipTypeLookup.ReturnValue;
        }

        private void button_spawn_search_Click(object sender, EventArgs e)
        {
            var Form_SpawnSearch = new Form_SpawnSearch(db);
            Form_SpawnSearch.ShowDialog();

            comboBox_select_zone.SelectedItem = Form_SpawnSearch.ReturnZone;
            comboBox_select_type.SelectedItem = Form_SpawnSearch.ReturnType;
            comboBox_select_spawn.SelectedItem = Form_SpawnSearch.ReturnName;
        }

        private void LoadLootDropEntry(string loottable_id)
        { 
            MySqlDataReader reader = db.RunSelectQuery("SELECT ld.id, ld.loot_table_id, ld.item_id, CONCAT('(', i.id, ') ', i.name) as item_name, ld.item_charges, ld.equip_item,  ld.probability " +
                                                       "FROM lootdrop ld " +
                                                       "INNER JOIN " +
                                                       "items i ON ld.item_id=i.id " +
                                                       "WHERE ld.loot_table_id=" + loottable_id);

            if (reader != null)
            {
                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3))); // Full item name
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(5)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(6)));
                    listView_lootdrop.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_lootdrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = listView_lootdrop.SelectedItems.Count;
            if (count > 0)
            {
                if (listView_lootdrop.SelectedIndices.Count == 0 || listView_lootdrop.SelectedIndices[0] == -1)
                {
                    return;
                }

                ListViewItem item = listView_lootdrop.Items[listView_lootdrop.SelectedIndices[0]];
                textBox_Lootdrop_ID.Text = item.Text;
                textBox_Lootdrop_ItemID.Text = item.SubItems[2].Text;
                textBox_Lootdrop_Charges.Text = item.SubItems[4].Text;
                textBox_Lootdrop_Equip.Text = item.SubItems[5].Text;
                textBox_Lootdrop_Probability.Text = item.SubItems[6].Text;
                button_LootDrop_Delete.Enabled = true;
                button_LootDrop_Save.Enabled = true;
            } else
            {
                button_LootDrop_Delete.Enabled = false;
                button_LootDrop_Save.Enabled = false;
            }
        }

        private void button_LootDrop_Insert_Click(object sender, EventArgs e)
        {
            string loot_table_id = textBox_Loottable_ID.Text;
            string item_id = string.IsNullOrEmpty(textBox_Lootdrop_ItemID.Text) ? "13005" : textBox_Lootdrop_ItemID.Text;
            string item_charges = string.IsNullOrEmpty(textBox_Lootdrop_Charges.Text) ? "1" : textBox_Lootdrop_Charges.Text;
            string equip_item = string.IsNullOrEmpty(textBox_Lootdrop_Equip.Text) ? "0" : textBox_Lootdrop_Equip.Text;
            string probability = string.IsNullOrEmpty(textBox_Lootdrop_Probability.Text) ? "100" : textBox_Lootdrop_Probability.Text;

            int rows = db.RunQuery("INSERT INTO lootdrop (id, loot_table_id, item_id, item_charges, equip_item, probability) " +
                                   "VALUES (" + GetNextLootDropID() + ", " + loot_table_id + ", " + item_id + ", " + item_charges + ", " + equip_item + ", " + probability + ")");
            if (rows > 0)
            {
                ResetLootDropEntry(true);
                LoadLootDropEntry(loot_table_id);
            }
        }

        private void button_LootDrop_Delete_Click(object sender, EventArgs e)
        {
            string loot_table_id = textBox_Loottable_ID.Text;
            string id = textBox_Lootdrop_ID.Text;

            int rows = db.RunQuery("DELETE FROM lootdrop " +
                                   "WHERE id=" + id);
            if (rows > 0)
            {
                ResetLootDropEntry(true);
                LoadLootDropEntry(loot_table_id);
            }
        }

        private void ResetLootDropEntry(bool include_listview)
        {
            if (include_listview)
                listView_lootdrop.Items.Clear();

            textBox_Lootdrop_ID.Clear();
            textBox_Lootdrop_ItemID.Clear();
            textBox_Lootdrop_Charges.Clear();
            textBox_Lootdrop_Equip.Clear();
            textBox_Lootdrop_Probability.Clear();

            button_LootDrop_Save.Enabled = false;
            button_LootDrop_Delete.Enabled = false;
        }

        private void button_LootDrop_Save_Click(object sender, EventArgs e)
        {
            string id = textBox_Lootdrop_ID.Text;
            string loot_table_id = textBox_Loottable_ID.Text;
            string item_id = string.IsNullOrEmpty(textBox_Lootdrop_ItemID.Text) ? "1" : textBox_Lootdrop_ItemID.Text;
            string item_charges = string.IsNullOrEmpty(textBox_Lootdrop_Charges.Text) ? "0" : textBox_Lootdrop_Charges.Text;
            string equip_item = string.IsNullOrEmpty(textBox_Lootdrop_Equip.Text) ? "0" : textBox_Lootdrop_Equip.Text;
            string probability = string.IsNullOrEmpty(textBox_Lootdrop_Probability.Text) ? "100" : textBox_Lootdrop_Probability.Text;

            int rows = db.RunQuery("UPDATE lootdrop " +
                                   "SET id=" + id + ", item_id=" + item_id + ", item_charges=" + item_charges + ", equip_item=" + equip_item + ", probability=" + probability + " " +
                                   "WHERE id=" + id + " AND loot_table_id=" + loot_table_id); 
            if (rows > 0)
            {
                ResetLootDropEntry(true);
                LoadLootDropEntry(loot_table_id);
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void ResetLootTableEntry(bool include_listview)
        {
            if (include_listview)
                listView_loottable.Items.Clear();

            textBox_Loottable_ID.Clear();
            textBox_Loottable_Lootdrop_Probability.Clear();
            textBox_Loottable_Maxcoin.Clear();
            textBox_Loottable_Maxloot.Clear();
            textBox_Loottable_Mincoin.Clear();
            textBox_Loottable_Minloot.Clear();
            textBox_Loottable_Name.Clear();
            textBox_Loottable_Coinprobability.Clear();

            button_LootTable_Delete.Enabled = false;
            button_LootTable_Drop.Enabled = false;
            button_LootTable_Save.Enabled = false;
        }

        private void LoadLootTableEntry(int spawn_id)
        {
            listView_loottable.Items.Clear();

            MySqlDataReader reader = db.RunSelectQuery("SELECT lt.id, lt.name, lt.mincoin, lt.maxcoin, lt.minlootitems, lt.maxlootitems, lt.lootdrop_probability, lt.coin_probability " +
                                                        "FROM " +
                                                        "loottable lt " +
                                                        "INNER JOIN " +
                                                        "spawn_loot sl ON lt.id = sl.loottable_id " +
                                                        "WHERE sl.spawn_id =" + spawn_id);
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
                    listView_loottable.Items.Add(item);
                }
                reader.Close();
            }

            if (listView_loottable.Items.Count > 0) {
                listView_loottable.Items[0].Selected = true;
                //listView_loottable_SelectedIndexChanged(null, null);

            }
        }

        private void listView_loottable_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView_lootdrop.Items.Clear();
            int count = listView_loottable.SelectedItems.Count;
            if (count > 0)
            {
                LoadLootDropEntry(listView_loottable.Items[listView_loottable.SelectedItems[0].Index].Text);

                if (listView_loottable.SelectedIndices.Count == 0 || listView_loottable.SelectedIndices[0] == -1)
                {
                    return;
                }

                ListViewItem item = listView_loottable.Items[listView_loottable.SelectedIndices[0]];
                textBox_Loottable_ID.Text = item.Text;
                textBox_Loottable_Name.Text = item.SubItems[1].Text;
                textBox_Loottable_Mincoin.Text = item.SubItems[2].Text;
                textBox_Loottable_Maxcoin.Text = item.SubItems[3].Text;
                textBox_Loottable_Minloot.Text = item.SubItems[4].Text;
                textBox_Loottable_Maxloot.Text = item.SubItems[5].Text;
                textBox_Loottable_Lootdrop_Probability.Text = item.SubItems[6].Text;
                textBox_Loottable_Coinprobability.Text = item.SubItems[7].Text;

                button_LootTable_Drop.Enabled = true;
                button_LootTable_Delete.Enabled = true;
                button_LootTable_Save.Enabled = true;
                button_LootDrop_Insert.Enabled = true;
            }
            else
            {
                button_LootTable_Drop.Enabled = false;
                button_LootTable_Delete.Enabled = false;
                button_LootTable_Save.Enabled = false;
                button_LootDrop_Insert.Enabled = false;
            }
        }

        private void button_LootTable_Insert_Click(object sender, EventArgs e)
        {
            int level = int.Parse(textBox_spawn_npc_maxlevel.Text);
            double coin = Math.Pow(((level / 2) + 2), 4);

            int spawn_id = GetSelectedSpawnID();
            int next_loot_table_id = GetNextLootTableID();
            string name = string.IsNullOrEmpty(textBox_Loottable_Name.Text) ? textBox_spawn_name.Text : textBox_Loottable_Name.Text;
            string mincoin = string.IsNullOrEmpty(textBox_Loottable_Mincoin.Text) ? coin.ToString() : textBox_Loottable_Mincoin.Text;
            string maxcoin = string.IsNullOrEmpty(textBox_Loottable_Maxcoin.Text) ? (coin*4).ToString() : textBox_Loottable_Maxcoin.Text;
            string minloot = string.IsNullOrEmpty(textBox_Loottable_Minloot.Text) ? "0" : textBox_Loottable_Minloot.Text;
            string maxloot = string.IsNullOrEmpty(textBox_Loottable_Maxloot.Text) ? "1" : textBox_Loottable_Maxloot.Text;
            string lootdrop_probability = string.IsNullOrEmpty(textBox_Loottable_Lootdrop_Probability.Text) ? "100" : textBox_Loottable_Lootdrop_Probability.Text;
            string coin_probability = string.IsNullOrEmpty(textBox_Loottable_Coinprobability.Text) ? "100" : textBox_Loottable_Coinprobability.Text;
            

            int rows = db.RunQuery("INSERT INTO loottable (id, name, mincoin, maxcoin, minlootitems, maxlootitems, lootdrop_probability, coin_probability) " +
                                   "VALUES (" + next_loot_table_id + ", '" + name + "', " + mincoin + ", " + maxcoin + ", " + minloot + ", " + maxloot + ", " + lootdrop_probability + ", " + coin_probability + "); " +
                                   "INSERT INTO spawn_loot (spawn_id, loottable_id) " +
                                   "VALUES (" + spawn_id + ", " + next_loot_table_id + ")");
            if (rows > 0)
            {
                ResetLootDropEntry(true);
                LoadLootTableEntry(spawn_id);
            }
        }

        private void button_LootTable_Assign_Click(object sender, EventArgs e)
        {
            var Form_LootTableSearch = new Form_LootTableSearch(db);
            Form_LootTableSearch.ShowDialog();

            string loottable_id = Form_LootTableSearch.ReturnId;

            if (loottable_id != null) {
                Assign_Spawn_To_LootTable(loottable_id, GetSelectedSpawnID());
            }
        }

        private void Assign_Spawn_To_LootTable(string loottable_id, int spawn_id)
        {
            int rows = db.RunQuery("INSERT INTO spawn_loot (spawn_id, loottable_id) VALUES (" + spawn_id + ", " + loottable_id + ")");
            if (rows > 0)
            {
                ResetLootDropEntry(true);
                LoadLootTableEntry(spawn_id);
            }
        }

        private void button_LootTable_Drop_Click(object sender, EventArgs e)
        {
            int count = listView_loottable.SelectedItems.Count;
            if (count > 0)
            {
                if (listView_loottable.SelectedIndices.Count == 0 || listView_loottable.SelectedIndices[0] == -1)
                {
                    return;
                }

                ListViewItem item = listView_loottable.Items[listView_loottable.SelectedIndices[0]];
                string loottable_id = item.Text;
                int spawn_id = GetSelectedSpawnID();

                int rows = db.RunQuery("DELETE FROM spawn_loot WHERE spawn_id=" + spawn_id + " AND loottable_id=" + loottable_id);
                if (rows > 0)
                {
                    ResetLootTableEntry(true);
                    LoadLootTableEntry(spawn_id);
                }
            }
        }

        private void button_LootTable_Delete_Click(object sender, EventArgs e)
        {
            int count = listView_loottable.SelectedItems.Count;
            if (count > 0)
            {
                if (listView_loottable.SelectedIndices.Count == 0 || listView_loottable.SelectedIndices[0] == -1)
                {
                    return;
                }

                ListViewItem item = listView_loottable.Items[listView_loottable.SelectedIndices[0]];
                string loottable_id = item.Text;
                int spawn_id = GetSelectedSpawnID();

                int rows = db.RunQuery("DELETE FROM loottable WHERE id=" + loottable_id);
                if (rows > 0)
                {
                    ResetLootTableEntry(true);
                    LoadLootTableEntry(spawn_id);
                }
            }
        }

        private void button_LootTable_Save_Click(object sender, EventArgs e)
        {
            int spawn_id = GetSelectedSpawnID();
            string id = textBox_Loottable_ID.Text;
            string name = string.IsNullOrEmpty(textBox_Loottable_Name.Text) ? "1" : textBox_Loottable_Name.Text;
            string mincoin = string.IsNullOrEmpty(textBox_Loottable_Mincoin.Text) ? "1" : textBox_Loottable_Mincoin.Text;
            string maxcoin = string.IsNullOrEmpty(textBox_Loottable_Maxcoin.Text) ? "1" : textBox_Loottable_Maxcoin.Text;
            string minloot = string.IsNullOrEmpty(textBox_Loottable_Minloot.Text) ? "1" : textBox_Loottable_Minloot.Text;
            string maxloot = string.IsNullOrEmpty(textBox_Loottable_Maxloot.Text) ? "1" : textBox_Loottable_Maxloot.Text;
            string coin_probability = string.IsNullOrEmpty(textBox_Loottable_Coinprobability.Text) ? "1" : textBox_Loottable_Coinprobability.Text;
            string loot_probability = string.IsNullOrEmpty(textBox_Loottable_Lootdrop_Probability.Text) ? "1" : textBox_Loottable_Lootdrop_Probability.Text;
            int rows = db.RunQuery("UPDATE loottable " +
                                   "SET name='" + name + "', mincoin=" + mincoin + ", maxcoin=" + maxcoin + ", minlootitems=" + minloot + ", maxlootitems=" + maxloot + ", lootdrop_probability=" + loot_probability + ", coin_probability=" + coin_probability + " " +
                                   "WHERE id=" + id);
            if (rows > 0)
            {
                ResetLootTableEntry(true);
                LoadLootTableEntry(spawn_id);
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_Item_Lookup_Click(object sender, EventArgs e)
        {

            var Form_ItemSearch = new Form_ItemSearch(db);
            Form_ItemSearch.ShowDialog();

            string item_id = Form_ItemSearch.ReturnId;

            textBox_Lootdrop_ItemID.Text = item_id;
        }

        private void button_merchantinventory_item_search_Click(object sender, EventArgs e)
        {
            var Form_ItemSearch = new Form_ItemSearch(db);
            Form_ItemSearch.ShowDialog();

            string item_id = Form_ItemSearch.ReturnId;

            textBox_merchantinventory_itemid.Text = item_id;
        }

        #region "Tool Tips"

        private void button_spawn_search_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_spawn_search, "Search");
        }

        private void button_duplicatespawn_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_duplicatespawn, "Duplicate Spawn");
        }

        private void button_refresh_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_refresh, "Refresh");
        }

        private void button_close_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_close, "Close");
        }

        private void button_merchant_insert_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_merchant_insert, "Insert");
        }

        private void button_merchant_remove_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_merchant_remove, "Remove");
        }

        private void button_merchant_update_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_merchant_update, "Update");
        }

        private void button_merchantinventory_item_search_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_merchantinventory_item_search, "Search");
        }

        private void button_merchantinventory_insert_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_merchantinventory_insert, "Insert");
        }

        private void button_merchantinventory_remove_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_merchantinventory_remove, "Remove");
        }

        private void button_merchantinventory_update_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_merchantinventory_update, "Update");
        }

        private void button_LootTable_Insert_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_LootTable_Insert, "Insert");
        }

        private void button_LootTable_Delete_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_LootTable_Delete, "Delete");
        }

        private void button_LootTable_Drop_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_LootTable_Drop, "Drop Loottable from this NPC");
        }

        private void button_LootTable_Save_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_LootTable_Save, "Save");
        }

        private void button_LootTable_Assign_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_LootTable_Assign, "Assign LootTable");
        }

        private void button_Item_Lookup_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_Item_Lookup, "Search");
        }

        private void button_LootDrop_Insert_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_LootDrop_Insert, "Insert");
        }

        private void button_LootDrop_Delete_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_LootDrop_Delete, "Delete");
        }

        private void button_LootDrop_Save_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_LootDrop_Save, "Save");
        }

        #endregion

        private void button_spawn_delete_Click(object sender, EventArgs e)
        {
            int id = GetSelectedSpawnID();

            int rows = db.RunQuery("DELETE FROM spawn WHERE id=" + id + ";DELETE FROM spawn_scripts WHERE spawn_id=" + id);
            if (rows > 0)
            {
                string zone = (string)comboBox_select_zone.SelectedItem;
                string type = (string)comboBox_select_type.SelectedItem;
                string spawn = (string)comboBox_select_spawn.SelectedItem;
                comboBox_select_zone.SelectedItem = null;
                comboBox_select_type.SelectedItem = null;
                comboBox_select_spawn.SelectedItem = null;
                comboBox_select_zone.SelectedItem = zone;
                comboBox_select_type.SelectedItem = type;
                comboBox_select_spawn.SelectedItem = spawn;
            }
        }

        private void button_spawn_delete_MouseHover(object sender, EventArgs e)
        {
            toolTip_spawn.SetToolTip(button_spawn_delete, "Delete Spawn");
        }

        private void buttonModelType_Click(object sender, EventArgs e)
        {
            var Form_ModelTypeLookup = new Form_AppearanceTypeLookup(db);
            Form_ModelTypeLookup.ShowDialog();

            if (Form_ModelTypeLookup.ReturnValue != null)
            {
                textBox_spawn_modeltype.Text = Form_ModelTypeLookup.ReturnValue;
                textBox_spawn_npc_smodeltype.Text = textBox_spawn_modeltype.Text;
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage_spawn_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox119_Click(object sender, EventArgs e)
        {

        }
    }
}
