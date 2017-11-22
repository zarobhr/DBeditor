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
    public partial class Page_Character : UserControl {
        private MySqlEngine db;
        private TabPage owner;
        private ArrayList races;
        private ArrayList classes;
        private ArrayList genders;
        private ArrayList zones;
        private ArrayList hair_types;
        private ArrayList hair_face_types;
        private ArrayList wing_types;
        private ArrayList chest_types;
        private ArrayList legs_types;

        public Page_Character(MySqlConnection connection, ref TabPage owner) {
            InitializeComponent();
            this.db = new MySqlEngine(connection);
            this.owner = owner;
            races = new ArrayList();
            classes = new ArrayList();
            genders = new ArrayList();
            zones = new ArrayList();
            hair_types = new ArrayList();
            hair_face_types = new ArrayList();
            wing_types = new ArrayList();
            chest_types = new ArrayList();
            legs_types = new ArrayList();

            InitializeRaces();
            InitializeClasses();
            InitializeGenders();
            InitializeHairTypes();
            InitializeHairFaceTypes();
            InitializeWingTypes();
            InitializeChestTypes();
            InitializeLegsTypes();
        }

        private void Page_Character_Load(object sender, EventArgs e) {
            PopulateCharacterComboBox();

            comboBox_select_character.SelectedItem = Properties.Settings.Default.LastCharacter;
        }

        private bool PopulateCharacterComboBox() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, name " +
                                                       "FROM characters");
            if (reader != null) {
                comboBox_select_character.Items.Clear();
                while (reader.Read())
                    comboBox_select_character.Items.Add(reader.GetString(1) + "   (" + reader.GetString(0) + ")");
                reader.Close();
                return true;
            }
            return false;
        }

        private int GetAccountId()
        {
            int account_id = 0;
            MySqlDataReader reader = db.RunSelectQuery("SELECT account_id " +
                                                       "FROM characters " +
                                                       "WHERE id=" + GetSelectedCharacterID());
            if (reader != null)
            {
                if (reader.Read())
                    account_id = reader.GetInt32(0);
                reader.Close();
            }
            return account_id;
        }

        private void comboBox_select_character_SelectedIndexChanged(object sender, EventArgs e) {
            int char_id = GetSelectedCharacterID();
            if (char_id == -1)
                return;

            ResetCharacter();

            InitializeZones();
            LoadCharacter(char_id);
            LoadCharacterInventory(char_id);

            tabControl_main.Visible = true;

            Properties.Settings.Default.LastCharacter = (string)comboBox_select_character.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private int GetSelectedCharacterID() {
            if (comboBox_select_character.SelectedItem == null)
                return -1;

            string search_for = "   (";
            string char_name = (string)comboBox_select_character.SelectedItem;
            char_name = char_name.Substring(char_name.IndexOf(search_for) + search_for.Length);
            char_name = char_name.Remove(char_name.Length - 1);
            return Convert.ToInt32(char_name);
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
            for (int i = 0; i < races.Count; i++)
                comboBox_character_race.Items.Add(((Race)races[i]).race);
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
                comboBox_character_class.Items.Add(((Class)classes[i]).class_name);
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
                comboBox_character_gender.Items.Add(((Gender)genders[i]).gender);
        }

        private int GetGenderID(string gender) {
            for (int i = 0; i < genders.Count; i++) {
                Gender type = (Gender)genders[i];
                if (gender == type.gender)
                    return type.id;
            }
            return -1;
        }

        private void InitializeZones() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, description " +
                                                       "FROM zones");
            if (reader != null) {
                zones.Clear();
                comboBox_character_currentzoneid.Items.Clear();
                zones.Add(new Zone(0, "None"));
                comboBox_character_currentzoneid.Items.Add(((Zone)zones[0]).zone);
                while (reader.Read()) {
                    zones.Add(new Zone(reader.GetInt32(0), reader.GetString(1)));
                    comboBox_character_currentzoneid.Items.Add(reader.GetString(1));
                }
                reader.Close();
            }
        }

        private int GetZoneID(string zone) {
            for (int i = 0; i < zones.Count; i++) {
                Zone type = (Zone)zones[i];
                if (zone == type.zone)
                    return type.id;
            }
            return -1;
        }

        private string GetZoneName(int id) {
            for (int i = 0; i < zones.Count; i++) {
                Zone type = (Zone)zones[i];
                if (id == type.id)
                    return type.zone;
            }
            return null;
        }

        private string GetGenderName(int id) {
            for (int i = 0; i < genders.Count; i++) {
                Gender type = (Gender)genders[i];
                if (id == type.id)
                    return type.gender;
            }
            return null;
        }

        private void InitializeHairTypes() {
            if (hair_types.Count == 0) {
                hair_types.Add(new Hair(0, "None"));
                hair_types.Add(new Hair(1113, "Hair1113"));
                hair_types.Add(new Hair(1114, "Hair1114"));
                hair_types.Add(new Hair(1115, "Hair1115"));
                hair_types.Add(new Hair(1116, "Hair1116"));
                hair_types.Add(new Hair(1119, "Hair1119"));
                hair_types.Add(new Hair(1120, "Hair1120"));
                hair_types.Add(new Hair(1121, "Hair1121"));
                hair_types.Add(new Hair(1122, "Hair1122"));
                hair_types.Add(new Hair(1123, "Hair1123"));
                hair_types.Add(new Hair(1124, "Hair1124"));
                hair_types.Add(new Hair(1125, "Hair1125"));
                hair_types.Add(new Hair(1126, "Hair1126"));
                hair_types.Add(new Hair(1127, "Hair1127"));
                hair_types.Add(new Hair(1128, "Hair1128"));
                hair_types.Add(new Hair(1129, "Hair1129"));
                hair_types.Add(new Hair(1130, "Hair1130"));
                hair_types.Add(new Hair(1131, "Hair1131"));
                hair_types.Add(new Hair(1132, "Hair1132"));
                hair_types.Add(new Hair(1133, "Hair1133"));
                hair_types.Add(new Hair(1134, "Hair1134"));
                hair_types.Add(new Hair(1135, "Hair1135"));
                hair_types.Add(new Hair(1136, "Hair1136"));
                hair_types.Add(new Hair(1137, "Hair1137"));
                hair_types.Add(new Hair(1138, "Hair1138"));
                hair_types.Add(new Hair(1139, "Hair1139"));
                hair_types.Add(new Hair(1140, "Hair1140"));
                hair_types.Add(new Hair(7352, "Hair7352"));
            }
            for (int i = 0; i < hair_types.Count; i++) {
                comboBox_character_hairtype.Items.Add(((Hair)hair_types[i]).hair);
                comboBox_character_sogahairtype.Items.Add(((Hair)hair_types[i]).hair);
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
                comboBox_character_facialhairtype.Items.Add(((HairFace)hair_face_types[i]).hair_face);
                comboBox_character_sogafacialhairtype.Items.Add(((HairFace)hair_face_types[i]).hair_face);
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
            for (int i = 0; i < chest_types.Count; i++) {
                comboBox_character_chesttype.Items.Add(((ChestType)chest_types[i]).chest_type);
                comboBox_character_sogachesttype.Items.Add(((ChestType)chest_types[i]).chest_type);
            }
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
            for (int i = 0; i < legs_types.Count; i++) {
                comboBox_character_legstype.Items.Add(((LegsType)legs_types[i]).legs_type);
                comboBox_character_sogalegstype.Items.Add(((LegsType)legs_types[i]).legs_type);
            }
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
            for (int i = 0; i < wing_types.Count; i++) {
                comboBox_character_wingtype.Items.Add(((WingType)wing_types[i]).wing_type);
                comboBox_character_sogawingtype.Items.Add(((WingType)wing_types[i]).wing_type);
            }
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

        /*************************************************************************************************************************
         *                                             CHARACTER
         *************************************************************************************************************************/

        private void LoadCharacter(int char_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, account_id, server_id, name, race, class, gender, deity, body_size, body_age, current_zone_id, level, tradeskill_level, soga_wing_type, soga_chest_type, soga_legs_type, soga_hair_type, soga_facial_hair_type, soga_model_type, legs_type, chest_type, wing_type, hair_type, facial_hair_type, model_type, x, y, z, heading, instance_id, starting_city, deleted, unix_timestamp, created_date, last_played, admin_status, is_online " +
                                                       "FROM characters " +
                                                       "WHERE id=" + char_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_character_id.Text = reader.GetString(0);
                    textBox_character_accountid.Text = reader.GetString(1);
                    textBox_character_serverid.Text = reader.GetString(2);
                    textBox_character_name.Text = reader.GetString(3);
                    comboBox_character_race.SelectedItem = GetRaceName(reader.GetInt32(4));
                    comboBox_character_class.SelectedItem = GetClassName(reader.GetInt32(5));
                    comboBox_character_gender.SelectedItem = GetGenderName(reader.GetInt32(6));
                    //comboBox_character_diety.SelectedItem = reader.GetInt32(7);
                    textBox_character_bodysize.Text = reader.GetString(8);
                    textBox_character_bodyage.Text = reader.GetString(9);
                    comboBox_character_currentzoneid.SelectedItem = GetZoneName(reader.GetInt32(10));
                    textBox_character_level.Text = reader.GetString(11);
                    textBox_character_tradeskilllevel.Text = reader.GetString(12);
                    comboBox_character_sogawingtype.SelectedItem = GetWingTypeName(reader.GetInt32(13));
                    comboBox_character_sogachesttype.SelectedItem = GetChestTypeName(reader.GetInt32(14));
                    comboBox_character_sogalegstype.SelectedItem = GetLegsTypeName(reader.GetInt32(15));
                    comboBox_character_sogahairtype.SelectedItem = GetHairTypeName(reader.GetInt32(16));
                    comboBox_character_sogafacialhairtype.SelectedItem = GetHairFaceTypeName(reader.GetInt32(17));
                    textBox_character_sogamodeltype.Text = reader.GetString(18);
                    comboBox_character_legstype.SelectedItem = GetLegsTypeName(reader.GetInt32(19));
                    comboBox_character_chesttype.SelectedItem = GetChestTypeName(reader.GetInt32(20));
                    comboBox_character_wingtype.SelectedItem = GetWingTypeName(reader.GetInt32(21));
                    comboBox_character_hairtype.SelectedItem = GetHairTypeName(reader.GetInt32(22));
                    comboBox_character_facialhairtype.SelectedItem = GetHairFaceTypeName(reader.GetInt32(23));
                    textBox_character_modeltype.Text = reader.GetString(24);
                    textBox_character_x.Text = reader.GetString(25);
                    textBox_character_y.Text = reader.GetString(26);
                    textBox_character_z.Text = reader.GetString(27);
                    textBox_character_heading.Text = reader.GetString(28);
                    textBox_character_instanceid.Text = reader.GetString(29);
                    textBox_character_startingcity.Text = reader.GetString(30);
                    checkBox_character_deleted.Checked = Convert.ToBoolean(reader.GetInt32(31));
                    textBox_character_unixtimestamp.Text = reader.GetString(32);
                    textBox_character_createddate.Text = reader.GetString(33);
                    textBox_character_lastplayed.Text = reader.GetMySqlDateTime(34).ToString();
                    textBox_character_adminstatus.Text = reader.GetString(35);
                    checkBox_character_isonline.Checked = Convert.ToBoolean(reader.GetInt32(36));
                    reader.Close();

                    string model_type = GetAppearanceName(Convert.ToInt32(textBox_character_modeltype.Text));
                    string soga_model_type = GetAppearanceName(Convert.ToInt32(textBox_character_sogamodeltype.Text));

                    label_character_modeltype.Text = model_type;
                    label_character_sogamodeltype.Text = soga_model_type;

                    owner.Text = "Character: " + textBox_character_name.Text;
                }
            }
        }

        private void button_character_update_Click(object sender, EventArgs e) {
            string id = textBox_character_id.Text;
            string name = db.RemoveEscapeCharacters(textBox_character_name.Text);
            int race = GetRaceID((string)comboBox_character_race.SelectedItem);
            int class_ = GetClassID((string)comboBox_character_class.SelectedItem);
            int gender = GetGenderID((string)comboBox_character_gender.SelectedItem);
            //int deity = (string)comboBox_character_diety.SelectedItem;
            string body_size = textBox_character_bodysize.Text;
            string body_age = textBox_character_bodyage.Text;
            int current_zone_id = GetZoneID((string)comboBox_character_currentzoneid.SelectedItem);
            string level = textBox_character_level.Text;
            string tradeskill_level = textBox_character_tradeskilllevel.Text;
            int soga_wing_type = GetWingTypeID((string)comboBox_character_sogawingtype.SelectedItem);
            int soga_chest_type = GetChestTypeID((string)comboBox_character_sogachesttype.SelectedItem);
            int soga_legs_type = GetLegsTypeID((string)comboBox_character_sogalegstype.SelectedItem);
            int soga_hair_type = GetHairTypeID((string)comboBox_character_sogahairtype.SelectedItem);
            int soga_facial_hair_type = GetHairFaceTypeID((string)comboBox_character_sogafacialhairtype.SelectedItem);
            string soga_model_type = textBox_character_sogamodeltype.Text;
            int wing_type = GetWingTypeID((string)comboBox_character_wingtype.SelectedItem);
            int chest_type = GetChestTypeID((string)comboBox_character_chesttype.SelectedItem);
            int legs_type = GetLegsTypeID((string)comboBox_character_legstype.SelectedItem);
            int hair_type = GetHairTypeID((string)comboBox_character_hairtype.SelectedItem);
            int facial_hair_type = GetHairFaceTypeID((string)comboBox_character_facialhairtype.SelectedItem);
            string model_type = textBox_character_modeltype.Text;
            string x = textBox_character_x.Text;
            string y = textBox_character_y.Text;
            string z = textBox_character_z.Text;
            string heading = textBox_character_heading.Text;
            string instance_id = textBox_character_instanceid.Text;
            string starting_city = textBox_character_startingcity.Text;
            int deleted = Convert.ToInt32(checkBox_character_deleted.Checked);
            string admin_status = textBox_character_adminstatus.Text;
            int is_online = Convert.ToInt32(checkBox_character_isonline.Checked);

            int rows = db.RunQuery("UPDATE characters " +
                                   "SET `name`='" + name + "', `race`=" + race + ", `class`=" + class_ + ", gender=" + gender + ", body_size=" + body_size + ", body_age=" + body_age + ", current_zone_id=" + current_zone_id + ", level=" + level + ", tradeskill_level=" + tradeskill_level + ", soga_wing_type=" + soga_wing_type + ", soga_chest_type=" + soga_chest_type + ", soga_legs_type=" + soga_legs_type + ", soga_hair_type=" + soga_hair_type + ", soga_facial_hair_type=" + soga_facial_hair_type + ", soga_model_type=" + soga_model_type + ", legs_type=" + legs_type + ", chest_type=" + chest_type + ", wing_type=" + wing_type + ", hair_type=" + hair_type + ", facial_hair_type=" + facial_hair_type + ", model_type=" + model_type + ", x=" + x + ", y=" + y + ", z=" + z + ", heading=" + heading + ", instance_id=" + instance_id + ", starting_city=" + starting_city + ", deleted=" + deleted + ", admin_status=" + admin_status + ", is_online=" + is_online + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                PopulateCharacterComboBox();
                comboBox_select_character.SelectedItem = name + "   (" + id + ")";
                LoadCharacter(Convert.ToInt32(id));
            }
        }

        private void ResetCharacter() {

            ResetCharacterInventory(true);
        }


        #region "Inventory"

        private void LoadCharacterInventory(int char_id)
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT c.id, account_id, char_id, item_id, CONCAT(i.name, ' (', i.id, ')'), type, bag_id, slot, creator, condition_, attuned, c.count, max_sell_value, login_checksum " +
                                                       "FROM character_items c " +
                                                       "INNER JOIN " +
                                                       "items i ON c.item_id=i.id " +
                                                       "WHERE char_id=" + char_id);

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
                    listView_character_inventory.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_character_inventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_character_inventory.SelectedIndices.Count == 0 || listView_character_inventory.SelectedIndices[0] == -1)
            {
                ResetCharacterInventory(false);
                return;
            }

            ListViewItem item = listView_character_inventory.Items[listView_character_inventory.SelectedIndices[0]];

            textBox_character_inventory_id.Text = item.Text;
            textBox_character_inventory_account_id.Text = item.SubItems[1].Text;
            textBox_character_inventory_char_id.Text = item.SubItems[2].Text;
            textBox_character_inventory_item.Text = item.SubItems[3].Text;
            textBox_character_inventory_bag_id.Text = item.SubItems[4].Text;
            comboBox_character_inventory_type.SelectedItem = item.SubItems[5].Text;
            textBox_character_inventory_bag_id.Text = item.SubItems[6].Text;
            textBox_character_inventory_slot_id.Text = item.SubItems[7].Text;
            textBox_character_inventory_creator.Text = item.SubItems[8].Text;
            textBox_character_inventory_condition.Text = item.SubItems[9].Text;
            textBox_character_inventory_attuned.Text = item.SubItems[10].Text;
            textBox_character_inventory_count.Text = item.SubItems[11].Text;
            textBox_character_inventory_max_sell.Text = item.SubItems[12].Text;
            textBox_character_inventory_checksum.Text = item.SubItems[13].Text;

            button_character_inventory_insert.Enabled = true;
            button_character_inventory_remove.Enabled = true;
            button_character_inventory_update.Enabled = true;

        }

        private void ResetCharacterInventory(bool include_listview)
        {
            if (include_listview)
                listView_character_inventory.Items.Clear();

            textBox_character_inventory_id.Clear();
            textBox_character_inventory_item.Clear();
            textBox_character_inventory_bag_id.Clear();
            comboBox_character_inventory_type.SelectedIndex = -1;
            textBox_character_inventory_bag_id.Clear();
            textBox_character_inventory_slot_id.Clear();
            textBox_character_inventory_creator.Clear();
            textBox_character_inventory_condition.Clear();
            textBox_character_inventory_attuned.Clear();
            textBox_character_inventory_count.Clear();
            textBox_character_inventory_max_sell.Clear();
            textBox_character_inventory_checksum.Clear();
           
            button_character_inventory_update.Enabled = false;
            button_character_inventory_remove.Enabled = false;
        }

        private void button_character_inventory_insert_Click(object sender, EventArgs e)
        {
            string type = string.IsNullOrEmpty(comboBox_character_inventory_type.SelectedText) ? "0" : comboBox_character_inventory_type.SelectedText;
            int account_id = GetAccountId();
            int char_id = GetSelectedCharacterID();
            string item_id = string.IsNullOrEmpty(textBox_character_inventory_item.Text) ? "1" : textBox_character_inventory_item.Text;
            string bag_id = string.IsNullOrEmpty(textBox_character_inventory_bag_id.Text) ? "0" : textBox_character_inventory_bag_id.Text;
            string slot_id = string.IsNullOrEmpty(textBox_character_inventory_slot_id.Text) ? "0" : textBox_character_inventory_slot_id.Text;
            string creator = string.IsNullOrEmpty(textBox_character_inventory_creator.Text) ? "0" : textBox_character_inventory_creator.Text;
            string condition = string.IsNullOrEmpty(textBox_character_inventory_condition.Text) ? "0" : textBox_character_inventory_condition.Text;
            string attuned = string.IsNullOrEmpty(textBox_character_inventory_attuned.Text) ? "0" : textBox_character_inventory_attuned.Text;
            string count = string.IsNullOrEmpty(textBox_character_inventory_count.Text) ? "0" : textBox_character_inventory_count.Text;
            string max_sell = string.IsNullOrEmpty(textBox_character_inventory_max_sell.Text) ? "0" : textBox_character_inventory_max_sell.Text;
            string checksum = string.IsNullOrEmpty(textBox_character_inventory_checksum.Text) ? "0" : textBox_character_inventory_checksum.Text;

            int rows = db.RunQuery("INSERT INTO character_items (type, account_id, char_id, bag_id, slot, item_id, creator, condition_, attuned, count, max_sell_value, login_checksum) " +
                                   "VALUES (" + type + ", " + account_id + ", " + char_id + ", " + bag_id + ", " + slot_id + ", " + item_id + ", " + creator + ", " + condition + ", " + attuned + ", " + count + ", " + max_sell + ", " + checksum + ")");
            if (rows > 0)
            {
                ResetCharacterInventory(true);
                LoadCharacterInventory(GetSelectedCharacterID());
            }
        }

        #endregion

        #region "Other"

        private void button_close_Click(object sender, EventArgs e)
        {
            owner.Dispose();
        }

        private void linkLabel_modeltypelookup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://eq2emu-reference.wetpaint.com/page/Category%3ACreature+Masterlist");
        }

        private void btnCharSafeSpot_Click(object sender, EventArgs e)
        {
            string id = textBox_character_id.Text;
            string name = db.RemoveEscapeCharacters(textBox_character_name.Text);

            int rows = db.RunQuery("UPDATE characters c " +
                        "INNER JOIN " +
                        "zones z ON c.current_zone_id=z.id " +
                        "SET c.x=z.safe_x, c.y=z.safe_y, c.z=z.safe_z, c.heading=z.safe_heading " +
                        "WHERE c.id=" + id);
            if (rows > 0)
            {
                PopulateCharacterComboBox();
                comboBox_select_character.SelectedItem = name + "   (" + id + ")";
                LoadCharacter(Convert.ToInt32(id));
            }
        }

        #endregion

        private void button_character_inventory_remove_Click(object sender, EventArgs e)
        {
            string id = textBox_character_inventory_id.Text;

            int rows = db.RunQuery("DELETE FROM character_items " +
                                   "WHERE id=" + id);
            if (rows > 0)
            {
                ResetCharacterInventory(true);
                LoadCharacterInventory(GetSelectedCharacterID());
            }
        }

        private void button_character_inventory_update_Click(object sender, EventArgs e)
        {
            string id = textBox_character_inventory_id.Text;
            string type = string.IsNullOrEmpty(comboBox_character_inventory_type.SelectedItem.ToString()) ? "0" : comboBox_character_inventory_type.SelectedItem.ToString();
            int account_id = GetAccountId();
            int char_id = GetSelectedCharacterID();
            string item_id = string.IsNullOrEmpty(textBox_character_inventory_item.Text) ? "1" : textBox_character_inventory_item.Text;
            string bag_id = string.IsNullOrEmpty(textBox_character_inventory_bag_id.Text) ? "0" : textBox_character_inventory_bag_id.Text;
            string slot_id = string.IsNullOrEmpty(textBox_character_inventory_slot_id.Text) ? "0" : textBox_character_inventory_slot_id.Text;
            string creator = string.IsNullOrEmpty(textBox_character_inventory_creator.Text) ? "" : textBox_character_inventory_creator.Text;
            string condition = string.IsNullOrEmpty(textBox_character_inventory_condition.Text) ? "0" : textBox_character_inventory_condition.Text;
            string attuned = string.IsNullOrEmpty(textBox_character_inventory_attuned.Text) ? "0" : textBox_character_inventory_attuned.Text;
            string count = string.IsNullOrEmpty(textBox_character_inventory_count.Text) ? "0" : textBox_character_inventory_count.Text;
            string max_sell = string.IsNullOrEmpty(textBox_character_inventory_max_sell.Text) ? "0" : textBox_character_inventory_max_sell.Text;
            string checksum = string.IsNullOrEmpty(textBox_character_inventory_checksum.Text) ? "0" : textBox_character_inventory_checksum.Text;

            int rows = db.RunQuery("UPDATE character_items " +
                                   "SET type='" + type + "', bag_id=" + bag_id + ", slot=" + slot_id + ", item_id=" + item_id + ", creator='" + creator + "', condition_=" + condition + ", attuned=" + attuned + ", count=" + count + ", max_sell_value=" + max_sell + ", login_checksum=" + checksum + " "  +
                                   "WHERE id=" + id);
            if (rows > 0)
            {
                ResetCharacterInventory(true);
                LoadCharacterInventory(GetSelectedCharacterID());
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void button_character_inventory_item_search_Click(object sender, EventArgs e)
        {
            var Form_ItemSearch = new Form_ItemSearch(db);
            Form_ItemSearch.ShowDialog();

            string item_id = Form_ItemSearch.ReturnId;

            textBox_character_inventory_item.Text = item_id;
        }
    }
}
