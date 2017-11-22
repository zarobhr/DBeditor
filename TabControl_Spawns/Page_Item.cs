using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Specialized;

namespace TabControl_Spawns {
    public partial class Page_Item : UserControl {
        private const int MAX_ITEM_STAT_TYPES = 8;
        private const int MAX_ITEM_STAT_SUBTYPES = 100;

        private MySqlConnection connection;
        private TabPage owner;
        private MySqlEngine db;
        private ArrayList tiers;
        private ArrayList types;
        private ArrayList skills;
        private ArrayList wield_styles;
        private ArrayList damage_types;
        private ArrayList food_types;
        private ArrayList classes;
        private ArrayList tradeskill_classes;
        private ArrayList adventure_classes;
        private ArrayList satiations;
        private ArrayList spell_tiers;
        private string[,] stats;

        public Page_Item(MySqlConnection connection, ref TabPage owner) {
            InitializeComponent();
            this.connection = connection;
            this.owner = owner;
            db = new MySqlEngine(this.connection);

            tiers = new ArrayList();
            types = new ArrayList();
            skills = new ArrayList();
            wield_styles = new ArrayList();
            damage_types = new ArrayList();
            food_types = new ArrayList();
            classes = new ArrayList();
            tradeskill_classes = new ArrayList();
            adventure_classes = new ArrayList();
            satiations = new ArrayList();
            spell_tiers = new ArrayList();
            stats = new string[MAX_ITEM_STAT_TYPES, MAX_ITEM_STAT_SUBTYPES];

            InitializeItemTiers();
            InitializeSpellTiers();
            InitializeItemTypes();
            InitializeItemStats();
            InitializeItemWieldStyles();
            InitializeItemDamageTypes();
            InitializeFoodTypes();
            //InitializeClasses();
            //InitializeTradeskillClasses();
            //InitializeAdventureClasses();
            InitializeSatiations();
        }

        private void comboBox_select_type_SelectedIndexChanged(object sender, EventArgs e) {
            if (PopulateItemsComboBox()) {
                label_select_item.Visible = true;
                comboBox_select_item.Visible = true;
            }
            owner.Text = "Item: <none>";
            tabControl_main.Visible = false;


            Properties.Settings.Default.LastItemType = (string)comboBox_select_type.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private void comboBox_select_item_SelectedIndexChanged(object sender, EventArgs e) {
            int item_id = GetSelectedItemID();
            if (item_id == -1)
                return;

            string type = (string)comboBox_select_type.SelectedItem;

            ResetItem();
            ResetItemDetailsSkill();
            ResetItemAppearance();
            //ResetItemClasses(true);
            ResetArmor();
            ResetBag();
            ResetFood();
            ResetRange();
            ResetShield();
            ResetWeapon();
            ResetItemEffects(true);
            ResetItemStats(true);

            InitializeSkills();
            LoadItem(item_id);
            LoadItemDetailSkill(item_id);
            LoadItemAppearance(item_id);
            LoadItemClasses(item_id);
            LoadItemEffects(item_id);
            LoadItemStats(item_id);

            if (!type.Equals("Normal"))
            {
            }
            if (!type.Equals("Armor"))
                tabControl_main.TabPages.Remove(tabPage_details_armor);
            if (!type.Equals("Food"))
                tabControl_main.TabPages.Remove(tabPage_details_food);
            if (!type.Equals("Bag"))
                tabControl_main.TabPages.Remove(tabPage_details_bag);
            if (!type.Equals("Weapon"))
                tabControl_main.TabPages.Remove(tabPage_details_weapon);
            if (!type.Equals("Ranged"))
                tabControl_main.TabPages.Remove(tabPage_details_range);
            if (!type.Equals("Shield"))
                tabControl_main.TabPages.Remove(tabPage_details_shield);
            if (!type.Equals("Spell"))
            {
                tabControl_main.TabPages.Remove(tabPage_details_skill);
            }
            if (!type.Equals("Recipe"))
            {
                tabControl_main.TabPages.Remove(tabPage_details_recipe);
                tabControl_main.TabPages.Remove(tabPage_details_recipe_items);
            }
            if (!type.Equals("Book"))
            {
                //tabControl_main.TabPages.Remove(tabPage_details_book);
            }
            if (!type.Equals("House"))
                tabControl_main.TabPages.Remove(tabPage_details_house);
            if (!type.Equals("Thrown"))
                tabControl_main.TabPages.Remove(tabPage_details_thrown);
            if (!type.Equals("Bauble"))
                tabControl_main.TabPages.Remove(tabPage_details_bauble);
            if (!type.Equals("House Container"))
                tabControl_main.TabPages.Remove(tabPage_details_house_container);
            if (!type.Equals("Adornment"))
            {
                //tabControl_main.TabPages.Remove(tabPage_details_adornment);
            }
            if (!type.Equals("Pattern Set"))
                tabControl_main.TabPages.Remove(tabPage_details_pattern);
            if (!type.Equals("Item Set"))
                tabControl_main.TabPages.Remove(tabPage_details_armorset);

            if (type.Equals("Normal"))
            {
            }
            else if (type.Equals("Armor"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_armor))
                    tabControl_main.TabPages.Insert(3, tabPage_details_armor);
                LoadArmor(item_id);
            }
            else if (type.Equals("Food"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_food))
                    tabControl_main.TabPages.Insert(3, tabPage_details_food);
                LoadFood(item_id);
            }
            else if (type.Equals("Bag"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_bag))
                    tabControl_main.TabPages.Insert(3, tabPage_details_bag);
                LoadBag(item_id);
            }
            else if (type.Equals("Weapon"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_weapon))
                    tabControl_main.TabPages.Insert(3, tabPage_details_weapon);
                LoadWeapon(item_id);
            }
            else if (type.Equals("Ranged"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_range))
                    tabControl_main.TabPages.Insert(3, tabPage_details_range);
                LoadRange(item_id);
            }
            else if (type.Equals("Shield"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_shield))
                    tabControl_main.TabPages.Insert(3, tabPage_details_shield);
                LoadShield(item_id);
            }
            else if (type.Equals("Spell"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_skill))
                    tabControl_main.TabPages.Insert(3, tabPage_details_skill);
                //LoadSkill(item_id);
                //LoadSkills(item_id);
            }
            else if (type.Equals("Recipe"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_recipe))
                    tabControl_main.TabPages.Insert(3, tabPage_details_recipe);
                if (!tabControl_main.TabPages.Contains(tabPage_details_recipe_items))
                    tabControl_main.TabPages.Insert(3, tabPage_details_recipe_items);
                //LoadRecipe(item_id);
                //LoadRecipeItems(item_id);
            }
            else if (type.Equals("Book"))
            {
                //if (!tabControl_main.TabPages.Contains(tabPage_details_book))
                //    tabControl_main.TabPages.Insert(3, tabPage_details_book);
                //LoadBook(item_id);
            }
            else if (type.Equals("Decoration"))
            {
                // add in tabPage_details_decorations
            }
            else if (type.Equals("Dungeon Maker"))
            {
                // add in tabPage_details_dungeon_maker
            }
            else if (type.Equals("Marketplace"))
            {
                // add in tabPage_details_marketplace
            }
            else if (type.Equals("Profile"))
            {
                // add in tabPage_details_profile
            }
            else if (type.Equals("Scroll"))
            {
                // add in tabPage_details_profile
            }
            else if (type.Equals("House"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_house))
                    tabControl_main.TabPages.Insert(3, tabPage_details_house);
                //LoadHouse(item_id);
            }
            else if (type.Equals("Thrown"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_thrown))
                    tabControl_main.TabPages.Insert(3, tabPage_details_thrown);
                //LoadThrown(item_id);
            }
            else if (type.Equals("Bauble"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_bauble))
                    tabControl_main.TabPages.Insert(3, tabPage_details_bauble);
                //LoadBauble(item_id);
            }
            else if (type.Equals("House Container"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_house_container))
                    tabControl_main.TabPages.Insert(3, tabPage_details_house_container);
                //LoadHouseContainer(item_id);
            }
            else if (type.Equals("Adornment"))
            {
                //if (!tabControl_main.TabPages.Contains(tabPage_details_armor))
                //    tabControl_main.TabPages.Insert(3, tabPage_details_armor);
                //LoadAdornment(item_id);
            }
            else if (type.Equals("Pattern Set"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_pattern))
                    tabControl_main.TabPages.Insert(3, tabPage_details_pattern);
                //LoadPattern(item_id);
            }
            else if (type.Equals("Item Set"))
            {
                if (!tabControl_main.TabPages.Contains(tabPage_details_armorset))
                    tabControl_main.TabPages.Insert(3, tabPage_details_armorset);
                //LoadArmorSet(item_id);
            }
            else
            {
                MessageBox.Show("Could not determine item type.\nType: " + type, "Unknown Item Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Properties.Settings.Default.LastItemId = (string)comboBox_select_item.SelectedItem;
            Properties.Settings.Default.Save();

            tabControl_main.Visible = true;
        }

        private bool PopulateItemsComboBox() {
            if (comboBox_select_type.SelectedItem == null)
                return false;

            string type = (string)comboBox_select_type.SelectedItem;

            MySqlDataReader reader = db.RunSelectQuery("SELECT DISTINCT items.id, items.name " +
                                                       "FROM items " +
                                                       "WHERE items.item_type='" + type + "'");

            if (reader != null) {
                comboBox_select_item.Items.Clear();
                while (reader.Read())
                    comboBox_select_item.Items.Add(reader.GetString(1) + "   (" + reader.GetString(0) + ")");
                reader.Close();
                return true;
            }
            return false;
        }

        private int GetSelectedItemID() {
            if (comboBox_select_item.SelectedItem == null)
                return -1;

            string search_for = "   (";
            string item_name = (string)comboBox_select_item.SelectedItem;
            item_name = item_name.Substring(item_name.IndexOf(search_for) + search_for.Length);
            item_name = item_name.Remove(item_name.Length - 1);
            return Convert.ToInt32(item_name);
        }

        private void InitializeItemTiers() {
            tiers.Add(new ItemTier(0, "None"));
            tiers.Add(new ItemTier(1, "Tier 1"));
            tiers.Add(new ItemTier(2, "Tier 2"));
            tiers.Add(new ItemTier(3, "Uncommon"));
            tiers.Add(new ItemTier(4, "Treasured"));
            tiers.Add(new ItemTier(5, "Tier 5"));
            tiers.Add(new ItemTier(6, "Tier 6"));
            tiers.Add(new ItemTier(7, "Legendary"));
            tiers.Add(new ItemTier(8, "Tier 8"));
            tiers.Add(new ItemTier(9, "Fabled"));
            tiers.Add(new ItemTier(12, "Mythical"));
            for (int i = 0; i < tiers.Count; i++) { 
                comboBox_item_tier.Items.Add(((ItemTier)tiers[i]).tier);
            }
        }

        private void InitializeSpellTiers()
        {
            spell_tiers.Add(new SpellTier(0, " "));
            spell_tiers.Add(new SpellTier(1, "Apprentice I"));
            spell_tiers.Add(new SpellTier(2, "Apprentice II"));
            spell_tiers.Add(new SpellTier(3, "Apprentice III"));
            spell_tiers.Add(new SpellTier(4, "Apprentice IV"));
            spell_tiers.Add(new SpellTier(5, "Adept I"));
            spell_tiers.Add(new SpellTier(6, "Adept II"));
            spell_tiers.Add(new SpellTier(7, "Adept III"));
            spell_tiers.Add(new SpellTier(9, "Master I"));
            spell_tiers.Add(new SpellTier(10, "Master II"));
            for (int i = 0; i < spell_tiers.Count; i++)
            {
                comboBox_item_details_skill_tier.Items.Add(((SpellTier)spell_tiers[i]).tier);
            }
         }


        /*
         * This is the old code..
        private void InitializeItemTypes() {
            types.Add(new ItemType(14, "Adornment"));
            types.Add(new ItemType(1, "Armor"));
            types.Add(new ItemType(16, "Armor Set"));
            types.Add(new ItemType(3, "Bag"));
            types.Add(new ItemType(12, "Bauble"));
            types.Add(new ItemType(9, "Book"));
            types.Add(new ItemType(2, "Food"));
            types.Add(new ItemType(10, "House"));
            types.Add(new ItemType(13, "House Container"));
            types.Add(new ItemType(0, "Normal"));
            types.Add(new ItemType(15, "Pattern"));
            types.Add(new ItemType(5, "Ranged"));
            types.Add(new ItemType(8, "Recipe"));
            types.Add(new ItemType(6, "Shield"));
            types.Add(new ItemType(7, "Spell"));
            types.Add(new ItemType(11, "Thrown"));f
            types.Add(new ItemType(4, "Weapon"));
            for (int i = 0; i < types.Count; i++) {
                comboBox_select_type.Items.Add(((ItemType)types[i]).type);
                comboBox_item_itemtype.Items.Add(((ItemType)types[i]).type);
            }
        }
         * */
        
        private void InitializeItemTypes()
        {
            types.Add(new ItemType(13, "Adornment"));
            types.Add(new ItemType(3, "Armor"));
            types.Add(new ItemType(18, "Item Set"));
            types.Add(new ItemType(5, "Bag"));
            types.Add(new ItemType(9, "Bauble"));
            types.Add(new ItemType(19, "Book"));
            types.Add(new ItemType(20, "Decoration"));
            types.Add(new ItemType(21, "Dungeon Maker"));
            types.Add(new ItemType(8, "Food"));
            //types.Add(new ItemType(14, "Generic Adornment"));
            types.Add(new ItemType(10, "House"));
            types.Add(new ItemType(12, "House Container"));
            types.Add(new ItemType(22, "Marketplace"));
            types.Add(new ItemType(0, "Normal"));
            types.Add(new ItemType(17, "Pattern Set"));
            types.Add(new ItemType(16, "Profile"));
            types.Add(new ItemType(2, "Ranged"));
            types.Add(new ItemType(7, "Recipe"));
            types.Add(new ItemType(4, "Shield"));
            types.Add(new ItemType(6, "Scroll"));
            //types.Add(new ItemType(7, "Spell"));
            types.Add(new ItemType(11, "Thrown"));
            types.Add(new ItemType(1, "Weapon"));
            for (int i = 0; i < types.Count; i++)
            {
                comboBox_select_type.Items.Add(((ItemType)types[i]).type);
                comboBox_item_itemtype.Items.Add(((ItemType)types[i]).type);
            }
        }
        
        private void InitializeSkills() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, name " +
                                                       "FROM skills");
            if (reader != null) {
                skills.Clear();
                comboBox_item_skillidreq.Items.Clear();
                comboBox_item_skillidreq2.Items.Clear();
                skills.Add(new Skill(0, "None"));
                comboBox_item_skillidreq.Items.Add(((Skill)skills[0]).skill);
                comboBox_item_skillidreq2.Items.Add(((Skill)skills[0]).skill);
                while (reader.Read()) {
                    skills.Add(new Skill(reader.GetInt64(0), reader.GetString(1)));
                    comboBox_item_skillidreq.Items.Add(reader.GetString(1));
                    comboBox_item_skillidreq2.Items.Add(reader.GetString(1));
                }
                reader.Close();
            }
        }

        private void InitializeItemStats() {
            stats[0, 0] = "Strength";
            stats[0, 1] = "Stamina";
            stats[0, 2] = "Agility";
            stats[0, 3] = "Wisdom";
            stats[0, 4] = "Intelligence";

            stats[2, 0] = "vs Slash";
            stats[2, 1] = "vs Crush";
            stats[2, 2] = "vs Pierce";
            stats[2, 3] = "vs Heat";
            stats[2, 4] = "vs Cold";
            stats[2, 5] = "vs Magic";
            stats[2, 6] = "vs Mental";
            stats[2, 7] = "vs Divine";
            stats[2, 8] = "vs Disease";
            stats[2, 9] = "vs Poison";
            stats[2, 10] = "vs Drowning";
            stats[2, 11] = "vs Falling";
            stats[2, 12] = "vs Pain";
            stats[2, 13] = "vs Melee";

            stats[3, 0] = "Damage Slash";
            stats[3, 1] = "Damage Crush";
            stats[3, 2] = "Damage Pierce";
            stats[3, 3] = "Damage Heat";
            stats[3, 4] = "Damage Cold";
            stats[3, 5] = "Damage Magic";
            stats[3, 6] = "Damage Mental";
            stats[3, 7] = "Damage Divine";
            stats[3, 8] = "Damage Disease";
            stats[3, 9] = "Damage Poison";
            stats[3, 10] = "Damage Drowning";
            stats[3, 11] = "Damage Falling";
            stats[3, 12] = "Damage Pain";
            stats[3, 13] = "Damage Melee";

            stats[5, 0] = "Health";
            stats[5, 1] = "Power";
            stats[5, 2] = "Concentration";

            stats[6, 0] = "Health Regeneration";
            stats[6, 1] = "Mana Regeneration";
            stats[6, 2] = "Health Regeneration PPT";
            stats[6, 3] = "Mana Regeneration PPT";
            stats[6, 4] = "In Combat Health Regeneration PPT";
            stats[6, 5] = "In Combat Power Regeneration PPT";
            stats[6, 6] = "Max Health";
            stats[6, 7] = "Max Health Percentage";
            stats[6, 8] = "Speed";
            stats[6, 9] = "Slow";
            stats[6, 10] = "Mount Speed";
            stats[6, 11] = "Offensive Speed";
            stats[6, 12] = "Attack Speed";
            stats[6, 13] = "Max Power";
            stats[6, 14] = "Max Power Percentage";
            stats[6, 15] = "Max Attack Percentage";
            stats[6, 16] = "Blur Vision";
            stats[6, 17] = "Magic Level Immunity";
            stats[6, 18] = "Hate Gain Mod";
            stats[6, 19] = "Combat EXP Mod";
            stats[6, 20] = "Tradeskill EXP Mod";
            stats[6, 21] = "Achievement EXP Mod";
            stats[6, 22] = "Size Mod";
            stats[6, 23] = "Damage Per Second";
            stats[6, 24] = "Stealth";
            stats[6, 25] = "Invis";
            stats[6, 26] = "See Stealth";
            stats[6, 27] = "See Invis";
            stats[6, 28] = "Effective Level Mod";
            stats[6, 29] = "Riposte Chance";
            stats[6, 30] = "Parry Chance";
            stats[6, 31] = "Dodge Chance";
            stats[6, 32] = "AE Auto Attack Chance";
            stats[6, 33] = "Double Attack Chance";
            stats[6, 34] = "Ranged Double Attack Chance";
            stats[6, 35] = "Spell Double Attack Chance";
            stats[6, 36] = "Flurry";
            stats[6, 37] = "Extra Harvest Chance";
            stats[6, 38] = "Extra Shield Block Chance";
            stats[6, 39] = "Deflect Chance";
            stats[6, 40] = "Item Health Regeneration PPT";
            stats[6, 41] = "Power Health Regeneration PPT";
            stats[6, 42] = "Melee Crit Chance";
            stats[6, 43] = "Ranged Crit Chance";
            stats[6, 44] = "Damage Spell Crit Chance";
            stats[6, 45] = "Heal Spell Crit Chance";
            stats[6, 46] = "Melee Crit Bonus";
            stats[6, 47] = "Ranged Crit Bonus";
            stats[6, 48] = "Damage Spell Crit Bonus";
            stats[6, 49] = "Healh Spell Crit Bonus";
            stats[6, 50] = "Unconscious Health Mod";
            stats[6, 51] = "Spell Time Reuse Percentage";
            stats[6, 52] = "Spell Time Recover Percentage";
            stats[6, 53] = "Spell Time Cast Percentage";
            stats[6, 54] = "Melee Weapon Range";
            stats[6, 55] = "Ranged Weapon Range";
            stats[6, 56] = "Falling Damage Reduction";
            stats[6, 57] = "Shield Effectiveness";
            stats[6, 58] = "Riposte Damage";
            stats[6, 59] = "Minimum Deflection Chance";
            stats[6, 60] = "Movement Weave";
            stats[6, 61] = "In Combat Health Regeneration";
            stats[6, 62] = "In Combat Power Regeneration";
            stats[6, 63] = "Contest Speed Boost";
            stats[6, 64] = "Tracking Avoidance";
            stats[6, 65] = "Stealth Invis Speed Mod";
            stats[6, 66] = "Loot Coin";
            stats[6, 67] = "Armor Mitigation Increase";
            stats[6, 68] = "Ammo Conservation";
            stats[6, 69] = "Strikethrough";
            stats[6, 70] = "Status Bonus";
            stats[6, 71] = "Accuracy";
            stats[6, 72] = "Counterstrike";
            stats[6, 73] = "Shield Bash";
            stats[6, 74] = "Weapon Damage Bonus";
            stats[6, 75] = "Additional Riposte Chance";
            stats[6, 76] = "Critical Mitigation";
            stats[6, 77] = "Combat Art Damage";
            stats[6, 78] = "Spell Damage";
            stats[6, 79] = "Heal Amount";
            stats[6, 80] = "Taunt Amount";

            stats[7, 0] = "Spell Damage";
            stats[7, 1] = "Heal Amount";
            stats[7, 2] = "Spell and Heal";

            for (int i = 0; i < MAX_ITEM_STAT_TYPES; i++)
                for (int j = 0; j < MAX_ITEM_STAT_SUBTYPES; j++) {
                    if (stats[i, j] == null)
                        break;
                    comboBox_itemstats_stats.Items.Add(stats[i, j]);
                }
        }

        private void InitializeItemWieldStyles() {
            wield_styles.Add(new ItemWieldStyle(1, "Dual Wield"));
            wield_styles.Add(new ItemWieldStyle(2, "Single Handed"));
            wield_styles.Add(new ItemWieldStyle(4, "Two Handed"));
            for (int i = 0; i < wield_styles.Count; i++)
                comboBox_detailsweapon_wieldstyle.Items.Add(((ItemWieldStyle)wield_styles[i]).wield_style);
        }

        private void InitializeItemDamageTypes() {
            damage_types.Add(new ItemDamageType(0, "Slashing"));
            damage_types.Add(new ItemDamageType(1, "Crushing"));
            damage_types.Add(new ItemDamageType(2, "Piercing"));
            damage_types.Add(new ItemDamageType(4, "Damage Type 4"));
            for (int i = 0; i < damage_types.Count; i++)
                comboBox_detailsweapon_damagetype.Items.Add(((ItemDamageType)damage_types[i]).damage_type);
        }

        private void InitializeFoodTypes() {
            food_types.Add(new FoodType(0, "Drink"));
            food_types.Add(new FoodType(1, "Food"));
            for (int i = 0; i < food_types.Count; i++)
                comboBox_detailsfood_type.Items.Add(((FoodType)food_types[i]).food_type);
        }

        private void InitializeSatiations() {
            satiations.Add(new Satiation(0, "None"));
            satiations.Add(new Satiation(1, "Low"));
            satiations.Add(new Satiation(2, "Average"));
            satiations.Add(new Satiation(3, "High"));
            satiations.Add(new Satiation(4, "Superior"));
            satiations.Add(new Satiation(5, "Superior"));
            //satiations.Add(new Satiation(6, "Satiation 6"));
            //satiations.Add(new Satiation(7, "Satiation 7"));
            //satiations.Add(new Satiation(9, "Satiation 9"));
            for (int i = 0; i < satiations.Count; i++)
                comboBox_detailsfood_satiation.Items.Add(((Satiation)satiations[i]).satiation);
        }

        private int[] GetItemStatTypes(string stat) {
            int[] types = new int[2];
            for (int i = 0; i < MAX_ITEM_STAT_TYPES; i++)
                for (int j = 0; j < MAX_ITEM_STAT_SUBTYPES; j++) {
                    if (stats[i, j] == null)
                        break;
                    if (stats[i, j] == stat) {
                        types[0] = i;
                        types[1] = j;
                        return types;
                    }
                }
            types[0] = -1;
            types[1] = -1;
            return types;
        }

        private string GetItemStatName(int type, int sub_type) {
            return stats[type, sub_type];
        }

        private int GetWieldStyle(string wield_style) {
            for (int i = 0; i < wield_styles.Count; i++) {
                ItemWieldStyle style = (ItemWieldStyle)wield_styles[i];
                if (wield_style == style.wield_style)
                    return style.id;
            }
            return -1;
        }

        private string GetWieldStyleName(int wield_style) {
            for (int i = 0; i < wield_styles.Count; i++) {
                ItemWieldStyle style = (ItemWieldStyle)wield_styles[i];
                if (wield_style == style.id)
                    return style.wield_style;
            }
            return null;
        }

        private int GetDamageType(string damage_type) {
            for (int i = 0; i < damage_types.Count; i++) {
                ItemDamageType type = (ItemDamageType)damage_types[i];
                if (damage_type == type.damage_type)
                    return type.id;
            }
            return -1;
        }

        private string GetDamageTypeName(int damage_type) {
            for (int i = 0; i < damage_types.Count; i++) {
                ItemDamageType type = (ItemDamageType)damage_types[i];
                if (damage_type == type.id)
                    return type.damage_type;
            }
            return null;
        }

        private int GetFoodTypeID(string food_type) {
            for (int i = 0; i < food_types.Count; i++) {
                FoodType type = (FoodType)food_types[i];
                if (food_type == type.food_type)
                    return type.id;
            }
            return -1;
        }

        private string GetFodTypeName(int id) {
            for (int i = 0; i < food_types.Count; i++) {
                FoodType type = (FoodType)food_types[i];
                if (id == type.id)
                    return type.food_type;
            }
            return null;
        }

        private int GetItemTierID(string item_tier) {
            for (int i = 0; i < tiers.Count; i++) {
                ItemTier type = (ItemTier)tiers[i];
                if (item_tier == type.tier)
                    return type.id;
            }
            return -1;
        }

        private string GetItemTierName(int id) {
            for (int i = 0; i < tiers.Count; i++) {
                ItemTier type = (ItemTier)tiers[i];
                if (id == type.id)
                    return type.tier;
            }
            return null;
        }

        private int GetSpellTierID(string spell_tier)
        {
            for (int i = 0; i < spell_tiers.Count; i++)
            {
                SpellTier type = (SpellTier)spell_tiers[i];
                if (spell_tier == type.tier)
                    return type.id;
            }
            return -1;
        }

        private string GetSpellTierName(int id)
        {
            for (int i = 0; i < spell_tiers.Count; i++)
            {
                SpellTier type = (SpellTier)spell_tiers[i];
                if (id == type.id)
                    return type.tier;
            }
            return null;
        }

        private long GetSkillID(string skill) {
            for (int i = 0; i < skills.Count; i++) {
                Skill type = (Skill)skills[i];
                if (skill == type.skill)
                    return type.id;
            }
            return -1;
        }

        private string GetSkillName(long id) {
            for (int i = 0; i < skills.Count; i++) {
                Skill type = (Skill)skills[i];
                if (id == type.id)
                    return type.skill;
            }
            return null;
        }
        /*
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
                classes.Add(new Class(41, "Animalist"));
                classes.Add(new Class(42, "Beastlord"));
                classes.Add(new Class(43, "Shaper"));
                classes.Add(new Class(44, "Channeler"));
                classes.Add(new Class(255, "All"));
            }
            for (int i = 0; i < classes.Count; i++)
                comboBox_itemclasses_classid.Items.Add(((Class)classes[i]).class_name);
        }
        */

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

        /*
        private void InitializeAdventureClasses()
        {
            if (adventure_classes.Count == 0)
            {
                adventure_classes.Add(new Class(255, "All"));
            }
            for (int i = 0; i < adventure_classes.Count; i++)
                comboBox_item_adventureclasses.Items.Add(((Class)adventure_classes[i]).class_name);
        }
        

        private void InitializeTradeskillClasses()
        {
            if (tradeskill_classes.Count == 0) {
                tradeskill_classes.Add(new Class(255, "All"));
            }
            for (int i = 0; i < tradeskill_classes.Count; i++)
                comboBox_itemclasses_tradeskillclassid.Items.Add(((Class)tradeskill_classes[i]).class_name);
        }
        */

        private int GetTradeskillClassID(string class_name) {
            for (int i = 0; i < tradeskill_classes.Count; i++) {
                Class class_ = (Class)tradeskill_classes[i];
                if (class_name == class_.class_name)
                    return class_.id;
            }
            return -1;
        }

        private string GetTradeskillClassName(int class_id) {
            for (int i = 0; i < tradeskill_classes.Count; i++) {
                Class class_ = (Class)tradeskill_classes[i];
                if (class_id == class_.id)
                    return class_.class_name;
            }
            return null;
        }

        private int GetSatiationID(string satiation) {
            for (int i = 0; i < satiations.Count; i++) {
                Satiation class_ = (Satiation)satiations[i];
                if (satiation == class_.satiation)
                    return class_.id;
            }
            return -1;
        }

        private string GetSatiationName(int id) {
            for (int i = 0; i < satiations.Count; i++) {
                Satiation class_ = (Satiation)satiations[i];
                if (id == class_.id)
                    return class_.satiation;
            }
            return null;
        }

        private bool IsSlot(int slots, int slot_id) {
            return Convert.ToBoolean(slots & slot_id);
        }

        private int CheckSlot(bool is_checked, int slot_id) {
            if (is_checked)
                return slot_id;
            return 0;
        }

        /*************************************************************************************************************************
         *                                             ITEM
         *************************************************************************************************************************/

        private void LoadItem(int item_id) {          //        0  1        2         3    4       5     6                7            8          9          10        11          12          13      14     15          16      17      18        19       20         21         22          23        24           25          26          27      28           29            30        31         32         33         34     35       36         37          38          39              40               41             42                43               44                 45                 45                 47                 48              49           50             51                52                     53                      54                    55           56           57       58                                                                      
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, name, item_type, icon, count, tier, skill_id_req, skill_id_req2, skill_min, weight, description, show_name, attuneable, artifact, lore, temporary, notrade, novalue, nozone, nodestroy, crafted, good_only, evil_only, stacklore, lore_equip, flags_16384, flags_32768, ornate, heirloom, appearance_only, unlocked, norepair, refined, flags2_256, usable, slots, set_name, sell_price, stack_count, collectable, adornment_slot1, adornment_slot2, adornment_slot3, adornment_slot4, adornment_slot5, adornment_slot6, adornment_description, offers_quest_id, part_of_quest_id, quest_unknown, max_charges, display_charges, recommended_level, adventure_default_level, tradeskill_default_level, soe_item_id, soe_item_crc, lua_script, harvest " +
                                                       "FROM items " +
                                                       "WHERE items.id=" + item_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_item_id.Text = reader.GetString(0);
                    textBox_item_name.Text = reader.GetString(1);
                    comboBox_item_itemtype.Text = reader.GetString(2);
                    textBox_item_icon.Text = reader.GetString(3);
                    textBox_item_spell_tier.Text = reader.GetString(4);
                    comboBox_item_tier.SelectedItem = GetItemTierName(reader.GetInt32(5));
                    comboBox_item_skillidreq.SelectedItem = GetSkillName(reader.GetInt64(6));
                    comboBox_item_skillidreq2.SelectedItem = GetSkillName(reader.GetInt64(7));
                    textBox_item_skillmin.Text = reader.GetString(8);
                    textBox_item_weight.Text = reader.GetString(9);
                    try {textBox_item_description.Text = reader.GetString(10);} catch (Exception ex) {textBox_item_description.Text = "";}
                    checkBox_item_showname.Checked = Convert.ToBoolean(reader.GetInt32(11));
                    checkBox_item_attuneable.Checked = Convert.ToBoolean(reader.GetInt32(12));
                    checkBox_item_artifact.Checked = Convert.ToBoolean(reader.GetInt32(13));
                    checkBox_item_lore.Checked = Convert.ToBoolean(reader.GetInt32(14));
                    checkBox_item_temporary.Checked = Convert.ToBoolean(reader.GetInt32(15));
                    checkBox_item_notrade.Checked = Convert.ToBoolean(reader.GetInt32(16));
                    checkBox_item_novalue.Checked = Convert.ToBoolean(reader.GetInt32(17));
                    checkBox_item_nozone.Checked = Convert.ToBoolean(reader.GetInt32(18));
                    checkBox_item_nodestroy.Checked = Convert.ToBoolean(reader.GetInt32(19));
                    checkBox_item_crafted.Checked = Convert.ToBoolean(reader.GetInt32(20));
                    checkBox_item_goodonly.Checked = Convert.ToBoolean(reader.GetInt32(21));
                    checkBox_item_evilonly.Checked = Convert.ToBoolean(reader.GetInt32(22));
                    checkBox_item_stacklore.Checked = Convert.ToBoolean(reader.GetInt32(23));
                    checkBox_item_loreequip.Checked = Convert.ToBoolean(reader.GetInt32(24));
                    checkBox_item_flags16384.Checked = Convert.ToBoolean(reader.GetInt32(25));
                    checkBox_item_flags32768.Checked = Convert.ToBoolean(reader.GetInt32(26));
                    checkBox_item_ornate.Checked = Convert.ToBoolean(reader.GetInt32(27));
                    checkBox_item_heirloom.Checked = Convert.ToBoolean(reader.GetInt32(28));
                    checkBox_item_appearanceonly.Checked = Convert.ToBoolean(reader.GetInt32(29));
                    checkBox_item_unlocked.Checked = Convert.ToBoolean(reader.GetInt32(30));
                    checkBox_item_norepair.Checked = Convert.ToBoolean(reader.GetInt32(31));
                    checkBox_item_flags264.Checked = Convert.ToBoolean(reader.GetInt32(32));
                    checkBox_item_flags2256.Checked = Convert.ToBoolean(reader.GetInt32(33));
                    checkBox_item_usable.Checked = Convert.ToBoolean(reader.GetInt32(34));
                    int slots = reader.GetInt32(35);
                    try {textBox_item_setname.Text = reader.GetString(36);} catch (Exception ex) {textBox_item_setname.Text = "";}
                    textBox_item_sellprice.Text = reader.GetString(37);
                    textBox_item_stackcount.Text = reader.GetString(38);
                    checkBox_item_collectable.Checked = Convert.ToBoolean(reader.GetInt32(39));
                    textBox_item_adornmentslot1.Text = reader.GetString(40);
                    textBox_item_adornmentslot2.Text = reader.GetString(41);
                    textBox_item_adornmentslot3.Text = reader.GetString(42);
                    textBox_item_adornmentslot4.Text = reader.GetString(43);
                    textBox_item_adornmentslot5.Text = reader.GetString(44);
                    textBox_item_adornmentslot6.Text = reader.GetString(45);
                    try {textBox_item_adornmentdescription.Text = reader.GetString(46);} catch (Exception ex) {textBox_item_adornmentdescription.Text = "";}
                    textBox_item_offersquestid.Text = reader.GetString(47);
                    textBox_item_partofquestid.Text = reader.GetString(48);
                    checkBox_item_questunknown.Checked = Convert.ToBoolean(reader.GetInt32(49));
                    textBox_item_maxcharges.Text = reader.GetString(50);
                    checkBox_item_displaycharges.Checked = Convert.ToBoolean(reader.GetInt32(51));
                    textBox_item_recommendedlevel.Text = reader.GetString(52);
                    textBox_item_adventuredefaultlevel.Text = reader.GetString(53);
                    textBox_item_tradeskilldefalutlevel.Text = reader.GetString(54);
                    textBox_item_soeid.Text = reader.GetString(55);
                    textBox_item_soeitemcrc.Text = reader.GetString(56);
                    try { textBox_item_luascript.Text = reader.GetString(57); }
                    catch (Exception ex) { textBox_item_luascript.Text = ""; }
                    checkBox_item_harvest.Checked = Convert.ToBoolean(reader.GetInt32(58));
                    checkBox_item_head.Checked = IsSlot(slots, 4);
                    checkBox_item_cloak.Checked = IsSlot(slots, 524288);
                    checkBox_item_chest.Checked = IsSlot(slots, 8);
                    checkBox_item_shoulders.Checked = IsSlot(slots, 16);
                    checkBox_item_forearms.Checked = IsSlot(slots, 32);
                    checkBox_item_hands.Checked = IsSlot(slots, 64);
                    checkBox_item_waist.Checked = IsSlot(slots, 262144);
                    checkBox_item_legs.Checked = IsSlot(slots, 128);
                    checkBox_item_feet.Checked = IsSlot(slots, 256);
                    checkBox_item_food.Checked = IsSlot(slots, 4194304);
                    checkBox_item_drink.Checked = IsSlot(slots, 8388608);
                    checkBox_item_primary.Checked = IsSlot(slots, 1);
                    checkBox_item_secondary.Checked = IsSlot(slots, 2);
                    checkBox_item_neck.Checked = IsSlot(slots, 8192);
                    checkBox_item_ear1.Checked = IsSlot(slots, 2048);
                    checkBox_item_ear2.Checked = IsSlot(slots, 4096);
                    checkBox_item_ring1.Checked = IsSlot(slots, 512);
                    checkBox_item_ring2.Checked = IsSlot(slots, 1024);
                    checkBox_item_wrist1.Checked = IsSlot(slots, 16384);
                    checkBox_item_wrist2.Checked = IsSlot(slots, 32768);
                    checkBox_item_charm1.Checked = IsSlot(slots, 1048576);
                    checkBox_item_charm2.Checked = IsSlot(slots, 2097152);
                    checkBox_item_ammo.Checked = IsSlot(slots, 131072);
                    checkBox_item_range.Checked = IsSlot(slots, 65536);
                    checkBox_item_texture.Checked = IsSlot(slots, 16777216);

                    pb_item_icon.ImageLocation = @"icons/items/" + reader.GetString(3) + ".jpg";

                    owner.Text = "Item: " + textBox_item_name.Text;
                    reader.Close();
                }
            }
        }

        private void LoadItemDetailSkill(int item_id)
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT spell_id, spell_tier, IFNULL(s.name,'') " +
                                                       "FROM item_details_skill ids LEFT OUTER JOIN spells s ON ids.spell_id=s.id " +
                                                       "WHERE ids.item_id=" + item_id);
            if (reader != null)
            {
                if (reader.Read())
                {
                    textBox_item_details_skill_id.Text = reader.GetString(0);
                    comboBox_item_details_skill_tier.SelectedItem = GetSpellTierName(reader.GetInt32(1));
                    label_item_details_skill_spell.Text = reader.GetString(2);
                }
                reader.Close();
            }
        }

        private void button_newitem_Click(object sender, EventArgs e) {
            Form_NewItem newitem_form = new Form_NewItem();
            DialogResult result = newitem_form.ShowDialog();
            if (result == DialogResult.OK) {
                string name = db.RemoveEscapeCharacters(newitem_form.GetItemName());
                string item_type = newitem_form.GetItemType();
                int rows = db.RunQuery("INSERT INTO items (name, item_type, icon, count, tier, skill_id_req, skill_id_req2, skill_min, weight, description, show_name, attuneable, artifact, lore, temporary, notrade, novalue, nozone, nodestroy, crafted, good_only, evil_only, slots, set_name, sell_price, stack_count, collectable, adornment_description, offers_quest_id, part_of_quest_id, quest_unknown, max_charges, recommended_level) " +
                                       "VALUES ('" + name + "', '" + item_type + "', 0, 0, 0, 0, 0, 0, 0, null, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, null, 0, 0, 0, null, 0, 0, 0, 0, 0)");
                if (rows > 0 && newitem_form.LoadItemAfterInsert()) {
                    MySqlDataReader reader = db.RunSelectQuery("SELECT id, name " +
                                                               "FROM items " +
                                                               "WHERE name='" + name + "'");
                    if (reader != null) {
                        if (reader.Read()) {
                            string id = reader.GetString(0);
                            string item_name = reader.GetString(1);
                            reader.Close();
                            if ((string)comboBox_select_type.SelectedItem == item_type)
                                PopulateItemsComboBox();
                            else
                                comboBox_select_type.SelectedItem = item_type;
                            comboBox_select_item.SelectedItem = item_name + "   (" + id + ")";
                        }
                        if (!reader.IsClosed)
                            reader.Close();
                    }
                }
            }
        }

        private void button_item_update_Click(object sender, EventArgs e) {
            string id = textBox_item_id.Text;
            string name = db.RemoveEscapeCharacters(textBox_item_name.Text);
            string item_type = (string)comboBox_item_itemtype.SelectedItem;
            string icon = textBox_item_icon.Text;
            string count = textBox_item_spell_tier.Text;
            int tier = GetItemTierID((string)comboBox_item_tier.SelectedItem);
            long skill_id_req = GetSkillID((string)comboBox_item_skillidreq.SelectedItem);
            long skill_id_req2 = GetSkillID((string)comboBox_item_skillidreq2.SelectedItem);
            string skill_min = textBox_item_skillmin.Text;
            string skill_max = textBox_item_skillmax.Text;
            string weight = textBox_item_weight.Text;
            string description = db.RemoveEscapeCharacters(textBox_item_description.Text);
            int show_name = Convert.ToInt32(checkBox_item_showname.Checked);
            int attuneable = Convert.ToInt32(checkBox_item_attuneable.Checked);
            int artifact = Convert.ToInt32(checkBox_item_artifact.Checked);
            int lore = Convert.ToInt32(checkBox_item_lore.Checked);
            int temporary = Convert.ToInt32(checkBox_item_temporary.Checked);
            int notrade = Convert.ToInt32(checkBox_item_notrade.Checked);
            int novalue = Convert.ToInt32(checkBox_item_novalue.Checked);
            int nozone = Convert.ToInt32(checkBox_item_nozone.Checked);
            int nodestroy = Convert.ToInt32(checkBox_item_nodestroy.Checked);
            int crafted = Convert.ToInt32(checkBox_item_crafted.Checked);
            int good_only = Convert.ToInt32(checkBox_item_goodonly.Checked);
            int evil_only = Convert.ToInt32(checkBox_item_evilonly.Checked);
            int stacklore = Convert.ToInt32(checkBox_item_stacklore.Checked);
            int lore_equip = Convert.ToInt32(checkBox_item_loreequip.Checked);
            int flags_16384 = Convert.ToInt32(checkBox_item_flags16384.Checked);
            int flags_32768 = Convert.ToInt32(checkBox_item_flags32768.Checked);
            int ornate = Convert.ToInt32(checkBox_item_ornate.Checked);
            int heriloom = Convert.ToInt32(checkBox_item_heirloom.Checked);
            int appearance_only = Convert.ToInt32(checkBox_item_appearanceonly.Checked);
            int unlocked = Convert.ToInt32(checkBox_item_unlocked.Checked);
            int norepair = Convert.ToInt32(checkBox_item_norepair.Checked);
            int flags2_64 = Convert.ToInt32(checkBox_item_flags264.Checked);
            int flags2_256 = Convert.ToInt32(checkBox_item_flags2256.Checked);
            int usable = Convert.ToInt32(checkBox_item_usable.Checked);
            int slots = 0;
            string set_name = db.RemoveEscapeCharacters(textBox_item_setname.Text);
            string sell_price = textBox_item_sellprice.Text;
            string stack_count = textBox_item_stackcount.Text;
            int collectable = Convert.ToInt32(checkBox_item_collectable.Checked);
            string adornment_description = db.RemoveEscapeCharacters(textBox_item_adornmentdescription.Text);
            string offers_quest_id = textBox_item_offersquestid.Text;
            string part_of_quest_id = textBox_item_partofquestid.Text;
            int quest_unknown = Convert.ToInt32(checkBox_item_questunknown.Checked);
            string max_charges = textBox_item_maxcharges.Text;
            int display_charges = Convert.ToInt32(checkBox_item_displaycharges.Checked);
            string recommended_level = textBox_item_recommendedlevel.Text;
            string adventure_default_level = textBox_item_adventuredefaultlevel.Text;
            string tradeskill_default_level = textBox_item_tradeskilldefalutlevel.Text;
            string lua_script = db.RemoveEscapeCharacters(textBox_item_luascript.Text);
            int harvest = Convert.ToInt32(checkBox_item_harvest.Checked);

            slots += CheckSlot(checkBox_item_head.Checked, 4);
            slots += CheckSlot(checkBox_item_cloak.Checked, 524288);
            slots += CheckSlot(checkBox_item_chest.Checked, 8);
            slots += CheckSlot(checkBox_item_shoulders.Checked, 16);
            slots += CheckSlot(checkBox_item_forearms.Checked, 32);
            slots += CheckSlot(checkBox_item_hands.Checked, 64);
            slots += CheckSlot(checkBox_item_waist.Checked, 262144);
            slots += CheckSlot(checkBox_item_legs.Checked, 128);
            slots += CheckSlot(checkBox_item_feet.Checked, 256);
            slots += CheckSlot(checkBox_item_food.Checked, 4194304);
            slots += CheckSlot(checkBox_item_drink.Checked, 8388608);
            slots += CheckSlot(checkBox_item_primary.Checked, 1);
            slots += CheckSlot(checkBox_item_secondary.Checked, 2);
            slots += CheckSlot(checkBox_item_neck.Checked, 8192);
            slots += CheckSlot(checkBox_item_ear1.Checked, 2048);
            slots += CheckSlot(checkBox_item_ear2.Checked, 4096);
            slots += CheckSlot(checkBox_item_ring1.Checked, 512);
            slots += CheckSlot(checkBox_item_ring2.Checked, 1024);
            slots += CheckSlot(checkBox_item_wrist1.Checked, 16384);
            slots += CheckSlot(checkBox_item_wrist2.Checked, 32768);
            slots += CheckSlot(checkBox_item_charm1.Checked, 1048576);
            slots += CheckSlot(checkBox_item_charm2.Checked, 2097152);
            slots += CheckSlot(checkBox_item_ammo.Checked, 131072);
            slots += CheckSlot(checkBox_item_range.Checked, 65536);
            slots += CheckSlot(checkBox_item_texture.Checked, 16777216);

            int rows = db.RunQuery("UPDATE items " +
                                   "SET name='" + name + "', item_type='" + item_type + "', icon=" + icon + ", count=" + count + ", tier=" + tier + ", skill_id_req=" + skill_id_req + ", skill_id_req2=" + skill_id_req2 + ", skill_min=" + skill_min + ", weight=" + weight + ", description='" + description + "', show_name=" + show_name + ", attuneable=" + attuneable + ", artifact=" + artifact + ", lore=" + lore + ", temporary=" + temporary + ", notrade=" + notrade + ", novalue=" + novalue + ", nozone=" + nozone + ", nodestroy=" + nodestroy + ", crafted=" + crafted + ", good_only=" + good_only + ", evil_only=" + evil_only + ", stacklore=" + stacklore + ", lore_equip=" + lore_equip + ", flags_16384=" + flags_16384 + ", flags_32768=" + flags_32768 + ", ornate=" + ornate + ", heirloom=" + heriloom + ", appearance_only=" + appearance_only + ", unlocked=" + unlocked + ", norepair=" + norepair + ", refined=" + flags2_64 + ", flags2_256=" + flags2_256 + ", usable=" + usable + ", slots=" + slots + ", set_name='" + set_name + "', sell_price=" + sell_price + ", stack_count=" + stack_count + ", collectable=" + collectable + ", adornment_description='" + adornment_description + "', offers_quest_id=" + offers_quest_id + ", part_of_quest_id=" + part_of_quest_id + ", quest_unknown=" + quest_unknown + ", max_charges=" + max_charges + ", display_charges=" + display_charges + ", recommended_level=" + recommended_level + ", adventure_default_level=" + adventure_default_level + ", tradeskill_default_level=" + tradeskill_default_level + ", lua_script='" + lua_script + "', harvest=" + harvest + " " +
                                   "WHERE items.id=" + id);
            if (rows > 0) {
                if (item_type == (string)comboBox_select_type.SelectedItem)
                    PopulateItemsComboBox();
                else
                    comboBox_select_type.SelectedItem = item_type;    
                comboBox_select_item.SelectedItem = textBox_item_name.Text + "   (" + id + ")";
                LoadItem(Convert.ToInt32(id));
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void ResetItem() {
        }

        /*************************************************************************************************************************
         *                                             ITEM APPEARANCE
         *************************************************************************************************************************/

        private void LoadItemAppearance(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT item_appearances.id, item_appearances.item_id, item_appearances.equip_type, item_appearances.red, item_appearances.green, item_appearances.blue, item_appearances.highlight_red, item_appearances.highlight_green, item_appearances.highlight_blue " +
                                                       "FROM item_appearances " +
                                                       "WHERE item_appearances.item_id=" + item_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_itemappearance_id.Text = reader.GetString(0);
                    textBox_itemappearance_itemid.Text =  reader.GetString(1);
                    textBox_itemappearance_equiptype.Text =  reader.GetString(2);
                    textBox_itemappearance_red.Text =  reader.GetString(3);
                    textBox_itemappearance_green.Text =  reader.GetString(4);
                    textBox_itemappearance_blue.Text =  reader.GetString(5);
                    textBox_itemappearance_highlightred.Text =  reader.GetString(6);
                    textBox_itemappearance_highlightgreen.Text = reader.GetString(7);
                    textBox_itemappearance_highlightblue.Text = reader.GetString(8);
                    button_itemappearance_insert.Enabled = false;
                    button_itemappearance_update.Enabled = true;
                    button_itemappearance_remove.Enabled = true;
                }
                else {
                    button_itemappearance_insert.Enabled = true;
                    button_itemappearance_update.Enabled = false;
                    button_itemappearance_remove.Enabled = false;
                }
                reader.Close();
            }
        }

        private void button_itemappearance_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            string equip_type = textBox_itemappearance_equiptype.Text;
            string red = textBox_itemappearance_red.Text;
            string green = textBox_itemappearance_green.Text;
            string blue = textBox_itemappearance_blue.Text;
            string highlight_red = textBox_itemappearance_highlightred.Text;
            string highlight_green = textBox_itemappearance_highlightgreen.Text;
            string highlight_blue = textBox_itemappearance_highlightblue.Text;

            int rows = db.RunQuery("INSERT INTO item_appearances (item_id, equip_type, red, green, blue, highlight_red, highlight_green, highlight_blue) " +
                                   "VALUES (" + item_id + ", " + equip_type + ", " + red + ", " + green + ", " + blue + ", " + highlight_red + ", " + highlight_green + ", " + highlight_blue + ")");
            if (rows > 0) {
                ResetItemAppearance();
                LoadItemAppearance(Convert.ToInt32(item_id));
            }
        }

        private void button_itemappearance_update_Click(object sender, EventArgs e) {
            string id = textBox_itemappearance_id.Text;
            string item_id = textBox_itemappearance_itemid.Text;
            string equip_type = textBox_itemappearance_equiptype.Text;
            string red = textBox_itemappearance_red.Text;
            string green = textBox_itemappearance_green.Text;
            string blue = textBox_itemappearance_blue.Text;
            string highlight_red = textBox_itemappearance_highlightred.Text;
            string highlight_green = textBox_itemappearance_highlightgreen.Text;
            string highlight_blue = textBox_itemappearance_highlightblue.Text;

            int rows = db.RunQuery("UPDATE item_appearances " +
                                   "SET item_id=" + item_id + ", equip_type=" + equip_type + ", red=" + red + ", green=" + green + ", blue=" + blue + ", highlight_red=" + highlight_red + ", highlight_green=" + highlight_green + ", highlight_blue=" + highlight_blue + " " +
                                   "WHERE item_appearances.id=" + id);
            if (rows > 0) {
                ResetItemAppearance();
                LoadItemAppearance(Convert.ToInt32(item_id));
            }
        }

        private void button_itemappearance_remove_Click(object sender, EventArgs e) {
            string id = textBox_itemappearance_id.Text;

            int rows = db.RunQuery("DELETE FROM item_appearances " +
                                   "WHERE item_appearances.id=" + id);
            if (rows > 0) {
                ResetItemAppearance();
                LoadItemAppearance(GetSelectedItemID());
            }
        }

        private void ResetItemAppearance() {
            textBox_itemappearance_id.Clear();
            textBox_itemappearance_itemid.Clear();
            textBox_itemappearance_equiptype.Clear();
            textBox_itemappearance_red.Clear();
            textBox_itemappearance_green.Clear();
            textBox_itemappearance_blue.Clear();
            textBox_itemappearance_highlightred.Clear();
            textBox_itemappearance_highlightgreen.Clear();
            textBox_itemappearance_highlightblue.Clear();

            button_itemappearance_insert.Enabled = false;
            button_itemappearance_update.Enabled = false;
            button_itemappearance_remove.Enabled = false;
        }

        private void ResetItemDetailsSkill()
        {
            textBox_item_details_skill_id.Clear();
            comboBox_item_details_skill_tier.SelectedIndex = -1;
            label_item_details_skill_spell.Text = "";
        }

        /*************************************************************************************************************************
         *                                             ITEM CLASSES
         *************************************************************************************************************************/
        /*
        private void LoadItemClasses(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT item_classes.id, item_classes.item_id, item_classes.class_id, item_classes.tradeskill_class_id, item_classes.level " +
                                                       "FROM item_classes " +
                                                       "WHERE item_classes.item_id=" + item_id);
            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    listView_itemclasses.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_itemclasses_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_itemclasses.SelectedIndices.Count == 0 || listView_itemclasses.SelectedIndices[0] == -1) {
                ResetItemClasses(false);
                return;
            }

            ListViewItem item = listView_itemclasses.Items[listView_itemclasses.SelectedIndices[0]];
            textBox_itemclasses_id.Text = item.Text;
            textBox_itemclasses_itemid.Text = item.SubItems[1].Text;
            comboBox_itemclasses_classid.SelectedItem = GetClassName(Convert.ToInt32(item.SubItems[2].Text));
            comboBox_itemclasses_tradeskillclassid.SelectedItem = GetTradeskillClassName(Convert.ToInt32(item.SubItems[3].Text));
            textBox_itemclasses_level.Text = item.SubItems[4].Text;

            button_itemclasses_insert.Enabled = true;
            button_itemclasses_update.Enabled = true;
            button_itemclasses_remove.Enabled = true;
        }

        private void button_itemclasses_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            int class_id = GetClassID((string)comboBox_itemclasses_classid.SelectedItem);
            int tradeskill_class_id = GetTradeskillClassID((string)comboBox_itemclasses_tradeskillclassid.SelectedItem);
            string level = textBox_itemclasses_level.Text;

            int rows = db.RunQuery("INSERT INTO item_classes (item_id, class_id, tradeskill_class_id, level) " +
                                   "VALUES (" + item_id + ", " + class_id + ", " + tradeskill_class_id + ", " + level + ")");
            if (rows > 0) {
                ResetItemClasses(true);
                LoadItemClasses(Convert.ToInt32(item_id));
            }
        }
        */

        private void button_itemclasses_remove_Click(object sender, EventArgs e) {
            /*
            string id = textBox_itemclasses_id.Text;

            int rows = db.RunQuery("DELETE FROM item_classes " +
                                   "WHERE item_classes.id=" + id);

            if (rows > 0) {
                ResetItemClasses(true);
                LoadItemClasses(GetSelectedItemID());
            }
            */
        }

        /*
        private void ResetItemClasses(bool include_listview) {
            if (include_listview)
                listView_itemclasses.Items.Clear();

            textBox_itemclasses_id.Clear();
            textBox_itemclasses_itemid.Clear();
            comboBox_itemclasses_classid.SelectedItem = "All";
            comboBox_itemclasses_tradeskillclassid.SelectedItem = "All";
            textBox_itemclasses_level.Clear();

            button_itemclasses_update.Enabled = false;
            button_itemclasses_remove.Enabled = false;
        }
        */
        /*************************************************************************************************************************
         *                                             ITEM EFFECTS
         *************************************************************************************************************************/

        private void LoadItemEffects(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT item_effects.id, item_effects.item_id, item_effects.effect, item_effects.percentage, item_effects.bullet " +
                                                       "FROM item_effects " +
                                                       "WHERE item_effects.item_id=" + item_id);
            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    listView_itemeffects.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_itemeffects_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_itemeffects.SelectedIndices.Count == 0 || listView_itemeffects.SelectedIndices[0] == -1) {
                ResetItemEffects(false);
                return;
            }

            ListViewItem item = listView_itemeffects.Items[listView_itemeffects.SelectedIndices[0]];
            textBox_itemeffects_id.Text = item.Text;
            textBox_itemeffects_itemid.Text = item.SubItems[1].Text;
            textBox_itemeffects_effect.Text = item.SubItems[2].Text;
            textBox_itemeffects_percentage.Text = item.SubItems[3].Text;
            textBox_itemeffects_bullet.Text = item.SubItems[4].Text;

            button_itemeffects_insert.Enabled = true;
            button_itemeffects_update.Enabled = true;
            button_itemeffects_remove.Enabled = true;
        }

        private void button_itemeffects_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            string effect = db.RemoveEscapeCharacters(textBox_itemeffects_effect.Text);
            string percentage = textBox_itemeffects_percentage.Text;
            string bullet = textBox_itemeffects_bullet.Text;

            int rows = db.RunQuery("INSERT INTO item_effects (item_id, effect, percentage, bullet) " +
                                   "VALUES (" + item_id + ", '" + effect + "', " + percentage + ", '" + bullet + "')");
            if (rows > 0) {
                ResetItemEffects(true);
                LoadItemEffects(Convert.ToInt32(item_id));
            }
        }

        private void button_itemeffects_update_Click(object sender, EventArgs e) {
            string id = textBox_itemeffects_id.Text;
            string item_id = GetSelectedItemID().ToString();
            string effect = db.RemoveEscapeCharacters(textBox_itemeffects_effect.Text);
            string percentage = textBox_itemeffects_percentage.Text; 
            string bullet = textBox_itemeffects_bullet.Text;

            int rows = db.RunQuery("UPDATE item_effects " +
                                   "SET item_id=" + item_id + ", effect='" + effect + "', percentage=" + percentage + ", bullet=" + bullet + " " +
                                   "WHERE item_effects.id=" + id);
            if (rows > 0) {
                ResetItemEffects(true);
                LoadItemEffects(Convert.ToInt32(item_id));
            }
        }

        private void button_itemeffects_remove_Click(object sender, EventArgs e) {
            string id = textBox_itemeffects_id.Text;

            int rows = db.RunQuery("DELETE FROM item_effects " +
                                   "WHERE item_effects.id=" + id);

            if (rows > 0) {
                ResetItemEffects(true);
                LoadItemEffects(GetSelectedItemID());
            }
        }

        private void ResetItemEffects(bool include_listview) {
            if (include_listview)
                listView_itemeffects.Items.Clear();

            textBox_itemeffects_id.Clear();
            textBox_itemeffects_itemid.Clear();
            textBox_itemeffects_effect.Clear();
            textBox_itemeffects_percentage.Clear();
            textBox_itemeffects_bullet.Clear();

            button_itemeffects_update.Enabled = false;
            button_itemeffects_remove.Enabled = false;
        }

        private void ResetItemDetailSkill(bool include_listview)
        {
            if (include_listview)
                listView_itemeffects.Items.Clear();

            textBox_itemeffects_id.Clear();
            textBox_itemeffects_itemid.Clear();
            textBox_itemeffects_effect.Clear();
            textBox_itemeffects_percentage.Clear();
            textBox_itemeffects_bullet.Clear();

            button_itemeffects_update.Enabled = false;
            button_itemeffects_remove.Enabled = false;
        }


        /*************************************************************************************************************************
         *                                             ITEM STATS
         *************************************************************************************************************************/

        private void LoadItemStats(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT item_stats.id, item_stats.item_id, item_stats.type, item_stats.subtype, item_stats.value, item_stats.text, item_stats.description " +
                                                       "FROM item_stats " +
                                                       "WHERE item_stats.item_id=" + item_id);
            if (reader != null) {
                while (reader.Read()) {
                    string text;
                    string description;
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    try {text = reader.GetString(5);} catch (Exception ex) {text = null;}
                    try {description = reader.GetString(6);} catch (Exception ex) {description = null;}
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, text));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, description));
                    listView_itemstats.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_itemstats_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_itemstats.SelectedIndices.Count == 0 || listView_itemstats.SelectedIndices[0] == -1) {
                ResetItemStats(false);
                return;
            }

            ListViewItem item = listView_itemstats.Items[listView_itemstats.SelectedIndices[0]];
            textBox_itemstats_id.Text = item.Text;
            textBox_itemstats_itemid.Text = item.SubItems[1].Text;
            int type = Convert.ToInt32(item.SubItems[2].Text);
            int sub_type = Convert.ToInt32(item.SubItems[3].Text);
            textBox_itemstats_value.Text = item.SubItems[4].Text;
            textBox_itemstats_text.Text = item.SubItems[5].Text;
            textBox_itemstats_description.Text = item.SubItems[6].Text;

            comboBox_itemstats_stats.SelectedItem = GetItemStatName(type, sub_type);

            button_itemstats_insert.Enabled = true;
            button_itemstats_update.Enabled = true;
            button_itemstats_remove.Enabled = true;
        }

        private void button_itemstats_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            string stat = (string)comboBox_itemstats_stats.SelectedItem;
            string value = textBox_itemstats_value.Text;
            string text = db.RemoveEscapeCharacters(textBox_itemstats_text.Text);
            string description = db.RemoveEscapeCharacters(textBox_itemstats_description.Text);

            if (text == "")
                text = null;
            if (description == "")
                description = null;

            int[] types = GetItemStatTypes(stat);

            int rows = db.RunQuery("INSERT INTO item_stats (item_id, type, subtype, value, text, description) " +
                                   "VALUES (" + item_id + ", " + types[0] + ", " + types[1] + ", " + value + ", '" + text + "', '" + description + "')");
            if (rows > 0) {
                ResetItemStats(true);
                LoadItemStats(Convert.ToInt32(item_id));
            }
        }

        private void button_itemstats_update_Click(object sender, EventArgs e) {
            string id = textBox_itemstats_id.Text;
            string item_id = textBox_itemstats_itemid.Text;
            string stat = (string)comboBox_itemstats_stats.SelectedItem;
            string value = textBox_itemstats_value.Text;
            string text = db.RemoveEscapeCharacters(textBox_itemstats_text.Text);
            string description = db.RemoveEscapeCharacters(textBox_itemstats_description.Text);

            if (text == "")
                text = null;
            if (description == "")
                description = null;

            int[] types = GetItemStatTypes(stat);

            int rows = db.RunQuery("UPDATE item_stats " +
                                   "SET item_id=" + item_id + ", type=" + types[0] + ", subtype=" + types[1] + ", value=" + value + ", text='" + text + "', description='" + description + "' " +
                                   "WHERE item_stats.id=" + id);
            if (rows > 0) {
                ResetItemStats(true);
                LoadItemStats(Convert.ToInt32(item_id));
            }
        }

        private void button_itemstats_remove_Click(object sender, EventArgs e) {
            string id = textBox_itemstats_id.Text;

            int rows = db.RunQuery("DELETE FROM item_stats " +
                                   "WHERE item_stats.id=" + id);

            if (rows > 0) {
                ResetItemStats(true);
                LoadItemStats(GetSelectedItemID());
            }
        }

        private void ResetItemStats(bool include_listview) {
            if (include_listview)
                listView_itemstats.Items.Clear();

            textBox_itemstats_id.Clear();
            textBox_itemstats_itemid.Clear();
            comboBox_itemstats_stats.SelectedItem = null;
            textBox_itemstats_value.Clear();
            textBox_itemstats_text.Clear();
            textBox_itemstats_description.Clear();

            button_itemstats_update.Enabled = false;
            button_itemstats_remove.Enabled = false;
        }

        /*************************************************************************************************************************
        *                                             ARMOR DETAILS
        **************************************************************************************************************************/

        private void LoadArmor(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, item_id, mitigation_low, mitigation_high " +
                                                       "FROM item_details_armor " +
                                                       "WHERE item_id=" + item_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_detailsarmor_id.Text = reader.GetString(0);
                    textBox_detailsarmor_itemid.Text = reader.GetString(1);
                    textBox_detailsarmor_mitigationlow.Text = reader.GetString(2);
                    textBox_detailsarmor_mitigationhigh.Text = reader.GetString(3);
                    button_detailsarmor_insert.Enabled = false;
                    button_detailsarmor_update.Enabled = true;
                    button_detailsarmor_remove.Enabled = true;
                }
                else {
                    button_detailsarmor_insert.Enabled = true;
                    button_detailsarmor_update.Enabled = false;
                    button_detailsarmor_remove.Enabled = false;
                }
                reader.Close();
            }
        }

        private void button_detailsarmor_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            string mitigation_low = textBox_detailsarmor_mitigationlow.Text;
            string mitigation_high = textBox_detailsarmor_mitigationhigh.Text;

            int rows = db.RunQuery("INSERT INTO item_details_armor (item_id, mitigation_low, mitigation_high) " +
                                   "VALUES (" + item_id + ", " + mitigation_low + ", " + mitigation_high + ")");
            if (rows > 0) {
                ResetArmor();
                LoadArmor(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsarmor_update_Click(object sender, EventArgs e) {
            string id = textBox_detailsarmor_id.Text;
            string item_id = textBox_detailsarmor_itemid.Text;
            string mitigation_low = textBox_detailsarmor_mitigationlow.Text;
            string mitigation_high = textBox_detailsarmor_mitigationhigh.Text;

            int rows = db.RunQuery("UPDATE item_details_armor " +
                                   "SET item_id=" + item_id + ", mitigation_low=" + mitigation_low + ", mitigation_high=" + mitigation_high + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetArmor();
                LoadArmor(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsarmor_remove_Click(object sender, EventArgs e) {
            string id = textBox_detailsarmor_id.Text;

            int rows = db.RunQuery("DELETE FROM item_details_armor " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetArmor();
                LoadArmor(GetSelectedItemID());
            }
        }

        private void ResetArmor() {
            textBox_detailsarmor_id.Clear();
            textBox_detailsarmor_itemid.Clear();
            textBox_detailsarmor_mitigationlow.Clear();
            textBox_detailsarmor_mitigationhigh.Clear();

            button_detailsarmor_insert.Enabled = false;
            button_detailsarmor_update.Enabled = false;
            button_detailsarmor_remove.Enabled = false;
        }

        /*************************************************************************************************************************
        *                                             WEAPON DETAILS
        **************************************************************************************************************************/

        private void LoadWeapon(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, item_id, wield_style, damage_type, damage_low1, damage_low2, damage_low3, damage_high1, damage_high2, damage_high3, delay, damage_rating " +
                                                       "FROM item_details_weapon " +
                                                       "WHERE item_id=" + item_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_detailsweapon_id.Text = reader.GetString(0);
                    textBox_detailsweapon_itemid.Text = reader.GetString(1);
                    comboBox_detailsweapon_wieldstyle.SelectedItem = GetWieldStyleName(reader.GetInt32(2));
                    comboBox_detailsweapon_damagetype.SelectedItem = GetDamageTypeName(reader.GetInt32(3));
                    textBox_detailsweapon_damagelow1.Text = reader.GetString(4);
                    textBox_detailsweapon_damagelow2.Text = reader.GetString(5);
                    textBox_detailsweapon_damagelow3.Text = reader.GetString(6);
                    textBox_detailsweapon_damagehigh1.Text = reader.GetString(7);
                    textBox_detailsweapon_damagehigh2.Text = reader.GetString(8);
                    textBox_detailsweapon_damagehigh3.Text = reader.GetString(9);
                    textBox_detailsweapon_delay.Text = reader.GetString(10);
                    textBox_detailsweapon_damagerating.Text = reader.GetString(11);

                    button_detailsweapon_insert.Enabled = false;
                    button_detailsweapon_update.Enabled = true;
                    button_detailsweapon_remove.Enabled = true;
                }
                else {
                    button_detailsweapon_insert.Enabled = true;
                    button_detailsweapon_update.Enabled = false;
                    button_detailsweapon_remove.Enabled = false;
                }
                reader.Close();
            }
        }

        private void button_detailsweapon_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            int wield_style = GetWieldStyle((string)comboBox_detailsweapon_wieldstyle.SelectedItem);
            int damage_type = GetDamageType((string)comboBox_detailsweapon_damagetype.SelectedItem);
            string damage_low1 = textBox_detailsweapon_damagelow1.Text;
            string damage_low2 = textBox_detailsweapon_damagelow2.Text;
            string damage_low3 = textBox_detailsweapon_damagelow3.Text;
            string damage_high1 = textBox_detailsweapon_damagehigh1.Text;
            string damage_high2 = textBox_detailsweapon_damagehigh2.Text;
            string damage_high3 = textBox_detailsweapon_damagehigh3.Text;
            string delay = textBox_detailsweapon_delay.Text;
            string damage_rating = textBox_detailsweapon_damagerating.Text;

            int rows = db.RunQuery("INSERT INTO item_details_weapon (item_id, wield_style, damage_type, damage_low1, damage_low2, damage_low3, damage_high1, damage_high2, damage_high3, delay, damage_rating) " +
                                   "VALUES (" + item_id + ", " + wield_style + ", " + damage_type + ", " + damage_low1 + ", " + damage_low2 + ", " + damage_low3 + ", " + damage_high1 + ", " + damage_high2 + ", " + damage_high3 + ", " + delay + ", " + damage_rating + ")");
            if (rows > 0) {
                ResetWeapon();
                LoadWeapon(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsweapon_update_Click(object sender, EventArgs e) {
            string id = textBox_detailsweapon_id.Text;
            string item_id = textBox_detailsweapon_itemid.Text;
            int wield_style = GetWieldStyle((string)comboBox_detailsweapon_wieldstyle.SelectedItem);
            int damage_type = GetDamageType((string)comboBox_detailsweapon_damagetype.SelectedItem);
            string damage_low1 = textBox_detailsweapon_damagelow1.Text;
            string damage_low2 = textBox_detailsweapon_damagelow2.Text;
            string damage_low3 = textBox_detailsweapon_damagelow3.Text;
            string damage_high1 = textBox_detailsweapon_damagehigh1.Text;
            string damage_high2 = textBox_detailsweapon_damagehigh2.Text;
            string damage_high3 = textBox_detailsweapon_damagehigh3.Text;
            string delay = textBox_detailsweapon_delay.Text;
            string damage_rating = textBox_detailsweapon_damagerating.Text;

            int rows = db.RunQuery("UPDATE item_details_weapon " +
                                   "SET item_id=" + item_id + ", wield_style=" + wield_style + ", damage_type=" + damage_type + ", damage_low1=" + damage_low1 + ", damage_low2=" + damage_low2 + ", damage_low3=" + damage_low3 + ", damage_high1=" + damage_high1 + ", damage_high2=" + damage_high2 + ", damage_high3=" + damage_high3 + ", delay=" + delay + ", damage_rating=" + damage_rating + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetWeapon();
                LoadWeapon(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsweapon_remove_Click(object sender, EventArgs e) {
            string id = textBox_detailsweapon_id.Text;

            int rows = db.RunQuery("DELETE FROM item_details_weapon " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetWeapon();
                LoadWeapon(GetSelectedItemID());
            }
        }

        private void ResetWeapon() {
            textBox_detailsweapon_id.Clear();
            textBox_detailsweapon_itemid.Clear();
            textBox_detailsweapon_damagehigh1.Clear();
            textBox_detailsweapon_damagehigh2.Clear();
            textBox_detailsweapon_damagehigh3.Clear();
            textBox_detailsweapon_damagelow1.Clear();
            textBox_detailsweapon_damagelow2.Clear();
            textBox_detailsweapon_damagelow3.Clear();
            textBox_detailsweapon_damagerating.Clear();
            textBox_detailsweapon_delay.Clear();
            comboBox_detailsweapon_damagetype.SelectedItem = null;
            comboBox_detailsweapon_wieldstyle.SelectedItem = null;

            button_detailsweapon_insert.Enabled = false;
            button_detailsweapon_update.Enabled = false;
            button_detailsweapon_remove.Enabled = false;
        }

        /*************************************************************************************************************************
        *                                             RANGE DETAILS
        **************************************************************************************************************************/

        private void LoadRange(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, item_id, damage_low1, damage_low2, damage_low3, damage_high1, damage_high2, damage_high3, range_low, range_high, delay, damage_rating " +
                                                       "FROM item_details_range " +
                                                       "WHERE item_id=" + item_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_detailsrange_id.Text = reader.GetString(0);
                    textBox_detailsrange_itemid.Text = reader.GetString(1);
                    textBox_detailsrange_damagelow1.Text = reader.GetString(2);
                    textBox_detailsrange_damagelow2.Text = reader.GetString(3);
                    textBox_detailsrange_damagelow3.Text = reader.GetString(4);
                    textBox_detailsrange_damagehigh1.Text = reader.GetString(5);
                    textBox_detailsrange_damagehigh2.Text = reader.GetString(6);
                    textBox_detailsrange_damagehigh3.Text = reader.GetString(7);
                    textBox_detailsrange_rangelow.Text = reader.GetString(8);
                    textBox_detailsrange_rangehigh.Text = reader.GetString(9);
                    textBox_detailsrange_delay.Text = reader.GetString(10);
                    textBox_detailsrange_damagerating.Text = reader.GetString(11);

                    button_detailsrange_insert.Enabled = false;
                    button_detailsrange_update.Enabled = true;
                    button_detailsrange_remove.Enabled = true;
                }
                else {
                    button_detailsrange_insert.Enabled = true;
                    button_detailsrange_update.Enabled = false;
                    button_detailsrange_remove.Enabled = false;
                }
                reader.Close();
            }
        }

        private void button_detailsrange_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            string damage_low1 = textBox_detailsrange_damagelow1.Text;
            string damage_low2 = textBox_detailsrange_damagelow2.Text;
            string damage_low3 = textBox_detailsrange_damagelow3.Text;
            string damage_high1 = textBox_detailsrange_damagehigh1.Text;
            string damage_high2 = textBox_detailsrange_damagehigh2.Text;
            string damage_high3 = textBox_detailsrange_damagehigh3.Text;
            string range_low = textBox_detailsrange_rangelow.Text;
            string range_high = textBox_detailsrange_rangehigh.Text;
            string delay = textBox_detailsrange_delay.Text;
            string damage_rating = textBox_detailsrange_damagerating.Text;

            int rows = db.RunQuery("INSERT INTO item_details_range (item_id, damage_low1, damage_low2, damage_low3, damage_high1, damage_high2, damage_high3, range_low, range_high, delay, damage_rating) " +
                                   "VALUES (" + item_id + ", " + damage_low1 + ", " + damage_low2 + ", " + damage_low3 + ", " + damage_high1 + ", " + damage_high2 + ", " + damage_high3 + ", " + range_low + ", " + range_high + ", " + delay + ", " + damage_rating + ")");
            if (rows > 0) {
                ResetRange();
                LoadRange(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsrange_update_Click(object sender, EventArgs e) {
            string id = textBox_detailsrange_id.Text;
            string item_id = GetSelectedItemID().ToString();
            string damage_low1 = textBox_detailsrange_damagelow1.Text;
            string damage_low2 = textBox_detailsrange_damagelow2.Text;
            string damage_low3 = textBox_detailsrange_damagelow3.Text;
            string damage_high1 = textBox_detailsrange_damagehigh1.Text;
            string damage_high2 = textBox_detailsrange_damagehigh2.Text;
            string damage_high3 = textBox_detailsrange_damagehigh3.Text;
            string range_low = textBox_detailsrange_rangelow.Text;
            string range_high = textBox_detailsrange_rangehigh.Text;
            string delay = textBox_detailsrange_delay.Text;
            string damage_rating = textBox_detailsrange_damagerating.Text;

            int rows = db.RunQuery("UPDATE item_details_range " +
                                   "SET item_id=" + item_id + ", damage_low1=" + damage_low1 + ", damage_low2=" + damage_low2 + ", damage_low3=" + damage_low3 + ", damage_high1=" + damage_high1 + ", damage_high2=" + damage_high2 + ", damage_high3=" + damage_high3 + ", range_low=" + range_low + ", range_high=" + range_high + ", delay=" + delay + ", damage_rating=" + damage_rating + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetRange();
                LoadRange(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsrange_remove_Click(object sender, EventArgs e) {
            string id = textBox_detailsrange_id.Text;

            int rows = db.RunQuery("DELETE FROM item_details_range " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetRange();
                LoadRange(GetSelectedItemID());
            }
        }

        private void ResetRange() {
            textBox_detailsrange_id.Clear();
            textBox_detailsrange_itemid.Clear();
            textBox_detailsrange_damagelow1.Clear();
            textBox_detailsrange_damagelow2.Clear();
            textBox_detailsrange_damagelow3.Clear();
            textBox_detailsrange_damagehigh1.Clear();
            textBox_detailsrange_damagehigh2.Clear();
            textBox_detailsrange_damagehigh3.Clear();
            textBox_detailsrange_rangelow.Clear();
            textBox_detailsrange_rangehigh.Clear();
            textBox_detailsrange_delay.Clear();
            textBox_detailsrange_damagerating.Clear();

            button_detailsrange_insert.Enabled = false;
            button_detailsrange_update.Enabled = false;
            button_detailsrange_remove.Enabled = false;
        }

        /*************************************************************************************************************************
        *                                             BAG DETAILS
        **************************************************************************************************************************/

        private void LoadBag(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, item_id, num_slots, weight_reduction, unknown12 " +
                                                       "FROM item_details_bag " +
                                                       "WHERE item_id=" + item_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_detailsbag_id.Text = reader.GetString(0);
                    textBox_detailsbag_itemid.Text = reader.GetString(1);
                    textBox_detailsbag_numslots.Text = reader.GetString(2);
                    textBox_detailsbag_weightreduction.Text = reader.GetString(3);
                    textBox_detailsbag_unknown12.Text = reader.GetString(4);

                    button_detailsbag_insert.Enabled = false;
                    button_detailsbag_update.Enabled = true;
                    button_detailsbag_remove.Enabled = true;
                }
                else {
                    button_detailsbag_insert.Enabled = true;
                    button_detailsbag_update.Enabled = false;
                    button_detailsbag_remove.Enabled = false;
                }
                reader.Close();
            }
        }

        private void button_detailsbag_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            string num_slots = textBox_detailsbag_numslots.Text;
            string weight_reduction = textBox_detailsbag_weightreduction.Text;
            string unknown12 = textBox_detailsbag_unknown12.Text;

            int rows = db.RunQuery("INSERT INTO item_details_bag (item_id, num_slots, weight_reduction, unknown12) " +
                                   "VALUES (" + item_id + ", " + num_slots + ", " + weight_reduction + ", " + unknown12 + ")");
            if (rows > 0) {
                ResetBag();
                LoadBag(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsbag_update_Click(object sender, EventArgs e) {
            string id = textBox_detailsbag_id.Text;
            string item_id = GetSelectedItemID().ToString();
            string num_slots = textBox_detailsbag_numslots.Text;
            string weight_reduction = textBox_detailsbag_weightreduction.Text;
            string unknown12 = textBox_detailsbag_unknown12.Text;

            int rows = db.RunQuery("UPDATE item_details_bag " +
                                   "SET item_id=" + item_id + ", item_id=" + item_id + ", num_slots=" + num_slots + ", weight_reduction=" + weight_reduction + ", unknown12=" + unknown12 + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetBag();
                LoadBag(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsbag_remove_Click(object sender, EventArgs e) {
            string id = textBox_detailsbag_id.Text;

            int rows = db.RunQuery("DELETE FROM item_details_bag " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetBag();
                LoadBag(GetSelectedItemID());
            }
        }

        private void ResetBag() {
            textBox_detailsbag_id.Clear();
            textBox_detailsbag_itemid.Clear();
            textBox_detailsbag_numslots.Clear();
            textBox_detailsbag_weightreduction.Clear();
            textBox_detailsbag_unknown12.Clear();

            button_detailsbag_insert.Enabled = false;
            button_detailsbag_update.Enabled = false;
            button_detailsbag_remove.Enabled = false;
        }

        /*************************************************************************************************************************
        *                                             SHIELD DETAILS
        **************************************************************************************************************************/

        private void LoadShield(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, item_id, mitigation_low, mitigation_high " +
                                                       "FROM item_details_shield " +
                                                       "WHERE item_id=" + item_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_detailsshield_id.Text = reader.GetString(0);
                    textBox_detailsshield_itemid.Text = reader.GetString(1);
                    textBox_detailsshield_mitigationlow.Text = reader.GetString(2);
                    textBox_detailsshield_mitigationhigh.Text = reader.GetString(3);
                    button_detailsshield_insert.Enabled = false;
                    button_detailsshield_update.Enabled = true;
                    button_detailsshield_remove.Enabled = true;
                }
                else {
                    button_detailsshield_insert.Enabled = true;
                    button_detailsshield_update.Enabled = false;
                    button_detailsshield_remove.Enabled = false;
                }
                reader.Close();
            }
        }

        private void button_detailsshield_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            string mitigation_low = textBox_detailsshield_mitigationlow.Text;
            string mitigation_high = textBox_detailsshield_mitigationhigh.Text;

            int rows = db.RunQuery("INSERT INTO item_details_shield (item_id, mitigation_low, mitigation_high) " +
                                   "VALUES (" + item_id + ", " + mitigation_low + ", " + mitigation_high + ")");
            if (rows > 0) {
                ResetShield();
                LoadShield(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsshield_update_Click(object sender, EventArgs e) {
            string id = textBox_detailsshield_id.Text;
            string item_id = textBox_detailsshield_itemid.Text;
            string mitigation_low = textBox_detailsshield_mitigationlow.Text;
            string mitigation_high = textBox_detailsshield_mitigationhigh.Text;

            int rows = db.RunQuery("UPDATE item_details_shield " +
                                   "SET item_id=" + item_id + ", mitigation_low=" + mitigation_low + ", mitigation_high=" + mitigation_high + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetShield();
                LoadShield(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsshield_remove_Click(object sender, EventArgs e) {
            string id = textBox_detailsshield_id.Text;

            int rows = db.RunQuery("DELETE FROM item_details_shield " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetShield();
                LoadShield(GetSelectedItemID());
            }
        }

        private void ResetShield() {
            textBox_detailsshield_id.Clear();
            textBox_detailsshield_itemid.Clear();
            textBox_detailsshield_mitigationlow.Clear();
            textBox_detailsshield_mitigationhigh.Clear();

            button_detailsshield_insert.Enabled = false;
            button_detailsshield_update.Enabled = false;
            button_detailsshield_remove.Enabled = false;
        }

        /*************************************************************************************************************************
         *                                             FOOD DETAILS
         **************************************************************************************************************************/

        private void LoadFood(int item_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, item_id, type, level, duration, satiation " +
                                                       "FROM item_details_food " +
                                                       "WHERE item_id=" + item_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_detailsfood_id.Text = reader.GetString(0);
                    textBox_detailsfood_itemid.Text = reader.GetString(1);
                    comboBox_detailsfood_type.SelectedItem = GetFodTypeName(reader.GetInt32(2));
                    textBox_detailsfood_level.Text = reader.GetString(3);
                    textBox_detailsfood_duration.Text = reader.GetString(4);
                    comboBox_detailsfood_satiation.SelectedItem = GetSatiationName(reader.GetInt32(5));
                    button_detailsfood_insert.Enabled = false;
                    button_detailsfood_update.Enabled = true;
                    button_detailsfood_remove.Enabled = true;
                }
                else {
                    button_detailsfood_insert.Enabled = true;
                    button_detailsfood_update.Enabled = false;
                    button_detailsfood_remove.Enabled = false;
                }
                reader.Close();
            }
        }

        private void button_detailsfood_insert_Click(object sender, EventArgs e) {
            string item_id = GetSelectedItemID().ToString();
            int type = GetFoodTypeID((string)comboBox_detailsfood_type.SelectedItem);
            string level = textBox_detailsfood_level.Text;
            string duration = textBox_detailsfood_duration.Text;
            int satiation = GetSatiationID((string)comboBox_detailsfood_satiation.SelectedItem);

            int rows = db.RunQuery("INSERT INTO item_details_food (item_id, type, level, duration, satiation) " +
                                   "VALUES (" + item_id + ", " + type + ", " + level + ", " + duration + ", " + satiation + ")");
            if (rows > 0) {
                ResetFood();
                LoadFood(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsfood_update_Click(object sender, EventArgs e) {
            string id = textBox_detailsfood_id.Text;
            string item_id = GetSelectedItemID().ToString();
            int type = GetFoodTypeID((string)comboBox_detailsfood_type.SelectedItem);
            string level = textBox_detailsfood_level.Text;
            string duration = textBox_detailsfood_duration.Text;
            int satiation = GetSatiationID((string)comboBox_detailsfood_satiation.SelectedItem);

            int rows = db.RunQuery("UPDATE item_details_food " +
                                   "SET item_id=" + item_id + ", type=" + type + ", level=" + level + ", duration=" + duration + ", satiation=" + satiation + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetFood();
                LoadFood(Convert.ToInt32(item_id));
            }
        }

        private void button_detailsfood_remove_Click(object sender, EventArgs e) {
            string id = textBox_detailsfood_id.Text;

            int rows = db.RunQuery("DELETE FROM item_details_food " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetFood();
                LoadFood(GetSelectedItemID());
            }
        }

        private void ResetFood() {
            textBox_detailsfood_id.Clear();
            textBox_detailsfood_itemid.Clear();
            textBox_detailsfood_level.Clear();
            textBox_detailsfood_duration.Clear();
            comboBox_detailsfood_satiation.SelectedItem = "Low";
            comboBox_detailsfood_type.SelectedIndex = 0;

            button_detailsfood_insert.Enabled = false;
            button_detailsfood_update.Enabled = false;
            button_detailsfood_remove.Enabled = false;
        }

        private void button_close_Click(object sender, EventArgs e) {
            owner.Dispose();
        }

        private void Page_Item_Load(object sender, EventArgs e)
        {
            LoadLastSettings();
        }

        private void LoadLastSettings()
        {
            comboBox_select_type.SelectedItem = Properties.Settings.Default.LastItemType;
            comboBox_select_item.SelectedItem = Properties.Settings.Default.LastItemId;
        }

        private void button_item_details_skill_delete_Click(object sender, EventArgs e)
        {
            string id = textBox_item_id.Text;

            int rows = db.RunQuery("DELETE FROM item_details_skill " +
                                   "WHERE item_id=" + id);
            if (rows > 0)
            {
                ResetItemDetailsSkill();
            }
        }

        private void button_item_spell_search_Click(object sender, EventArgs e)
        {
            var Form_SpellSearch = new Form_SpellSearch(db);
            Form_SpellSearch.ShowDialog();
            
            string item_id = textBox_item_id.Text;
            string spell_id = Form_SpellSearch.ReturnId;
            int spell_tier = GetSpellTierID((string)comboBox_item_details_skill_tier.SelectedItem);

            db.RunQuery("DELETE FROM item_details_skill " +
                                   "WHERE item_id=" + item_id);

            int rows = db.RunQuery("INSERT INTO item_details_skill (item_id, spell_id, spell_tier) VALUES " +
                                   "(" + item_id + ", " + spell_id + ", " + spell_tier + ")");

            if (rows > 0)
            {
                ResetItemDetailsSkill();
                LoadItemDetailSkill(Convert.ToInt32(item_id));
            }
        }

        private void button_item_details_skill_save_Click(object sender, EventArgs e)
        {
            string item_id = textBox_item_id.Text;
            string spell_id = string.IsNullOrEmpty(textBox_item_details_skill_id.Text) ? "1" : textBox_item_details_skill_id.Text;
            int spell_tier = GetSpellTierID((string)comboBox_item_details_skill_tier.SelectedItem);

            int rows = db.RunQuery("UPDATE item_details_skill SET spell_id=" + spell_id + ", spell_tier=" + spell_tier + " " +
                                   "WHERE item_id=" + item_id);

            if (rows > 0)
            {
                ResetItemDetailsSkill();
                LoadItemDetailSkill(Convert.ToInt32(item_id));
            }
        }

        private void button_item_details_skill_save_MouseHover(object sender, EventArgs e)
        {
            toolTip_item.SetToolTip(button_item_details_skill_save, "Save");
        }

        private void button_item_spell_search_MouseHover(object sender, EventArgs e)
        {
            toolTip_item.SetToolTip(button_item_spell_search, "Search");
        }

        private void button_item_details_skill_delete_MouseHover(object sender, EventArgs e)
        {
            toolTip_item.SetToolTip(button_item_details_skill_delete, "Delete");
        }

        private void ResetAdventureClasses()
        {
            checkBox_item_classes_animalist.Checked = false;
            checkBox_item_classes_assasin.Checked = false;
            checkBox_item_classes_beastlord.Checked = false;
            checkBox_item_classes_berserker.Checked = false;
            checkBox_item_classes_bard.Checked = false;
            checkBox_item_classes_brawler.Checked = false;
            checkBox_item_classes_brigand.Checked = false;
            checkBox_item_classes_bruiser.Checked = false;
            checkBox_item_classes_conjuror.Checked = false;
            checkBox_item_classes_coercer.Checked = false;
            checkBox_item_classes_cleric.Checked = false;
            checkBox_item_classes_crusader.Checked = false;
            checkBox_item_classes_commoner.Checked = false;
            checkBox_item_classes_channeler.Checked = false;
            checkBox_item_classes_dirge.Checked = false;
            checkBox_item_classes_defiler.Checked = false;
            checkBox_item_classes_druid.Checked = false;
            checkBox_item_classes_fury.Checked = false;
            checkBox_item_classes_fighter.Checked = false;
            checkBox_item_classes_guardian.Checked = false;
            checkBox_item_classes_illusionist.Checked = false;
            checkBox_item_classes_inquisitor.Checked = false;
            checkBox_item_classes_mystic.Checked = false;
            checkBox_item_classes_mage.Checked = false;
            checkBox_item_classes_monk.Checked = false;
            checkBox_item_classes_necromancer.Checked = false;
            checkBox_item_classes_priest.Checked = false;
            checkBox_item_classes_predator.Checked = false;
            checkBox_item_classes_paladin.Checked = false;
            checkBox_item_classes_rogue.Checked = false;
            checkBox_item_classes_swashbuckler.Checked = false;
            checkBox_item_classes_summoner.Checked = false;
            checkBox_item_classes_sorcerer.Checked = false;
            checkBox_item_classes_shaper.Checked = false;
            checkBox_item_classes_shaman.Checked = false;
            checkBox_item_classes_shadowknight.Checked = false;
            checkBox_item_classes_scout.Checked = false;
            checkBox_item_classes_troubador.Checked = false;
            checkBox_item_classes_templar.Checked = false;
            checkBox_item_classes_wizard.Checked = false;
            checkBox_item_classes_warrior.Checked = false;
            checkBox_item_classes_warlock.Checked = false;
            checkBox_item_classes_warden.Checked = false;
            checkBox_item_classes_ranger.Checked = false;
            checkBox_item_classes_woodworker.Checked = false;
            checkBox_item_classes_weaponsmith.Checked = false;
            checkBox_item_classes_tailor.Checked = false;
            checkBox_item_classes_scholar.Checked = false;
            checkBox_item_classes_sage.Checked = false;
            checkBox_item_classes_provisioner.Checked = false;
            checkBox_item_classes_outfitter.Checked = false;
            checkBox_item_classes_jeweler.Checked = false;
            checkBox_item_classes_armorer.Checked = false;
            checkBox_item_classes_craftsman.Checked = false;
            checkBox_item_classes_carpenter.Checked = false;
            checkBox_item_classes_artisan.Checked = false;
            checkBox_item_classes_alchemist.Checked = false;
        }

        private void LoadItemClasses(int id) {
            ResetAdventureClasses();


            StringCollection adv_class = new StringCollection();

            MySqlDataReader reader = db.RunSelectQuery("SELECT class " +
                                                       "FROM character_classes c " +
                                                       "WHERE c.bitmask & (SELECT adventure_classes FROM items WHERE id=" + 8844 + ")");
            if (reader != null)
            {
                while (reader.Read())
                {
                    adv_class.Add(reader.GetString(0));
                }
                reader.Close();
            }
            

            // It may be more efficient to only iterate the entire collection of controls once and throw those into their own collection
            // but the impact is probably minimal


            foreach (TabPage t in tabControl_main.TabPages)
            {
                if (t.Name == "tabPage_classes")
                {
                    foreach (Control c in t.Controls)
                    {
                        foreach (Control s in c.Controls)
                        {
                            var checkbox = s as CheckBox;
                            if (checkbox != null)
                            {
                                if (adv_class.Contains(checkbox.Text))
                                {
                                    checkbox.Checked = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button_itemclasses_update_Click(object sender, EventArgs e)
        {
            long bitmask = 0;

                foreach (TabPage t in tabControl_main.TabPages)
                {
                    if (t.Name == "tabPage_classes")
                    {
                        foreach (Control c in t.Controls)
                        {
                            if (c.Name == "groupBox_adventureclasses") {
                                foreach (Control s in c.Controls)
                                {
                                    var checkbox = s as CheckBox;
                                    if (checkbox != null && checkbox.Tag != null && checkbox.Checked == true)
                                    {

                                        bitmask += long.Parse(checkbox.Tag.ToString());
                                    }
                                }
                            }
                        }
                    }
                }

                string id = GetSelectedItemID().ToString();

                int rows = db.RunQuery("UPDATE items SET adventure_classes=" + bitmask + " WHERE id=" + id);

                if (rows > 0)
                {
                    LoadItemClasses(int.Parse(id));
                }
        }

        private void button_item_delete_Click(object sender, EventArgs e)
        {
            string id = textBox_item_id.Text;

            int rows = db.RunQuery("DELETE FROM items WHERE id=" + id);
            if (rows > 0)
            {
               PopulateItemsComboBox();

            }
        }

        #region "Tool Tips"

        private void button_newitem_MouseHover(object sender, EventArgs e)
        {
            toolTip_item.SetToolTip(button_newitem, "Insert");
        }

        private void button_item_delete_MouseHover(object sender, EventArgs e)
        {
            toolTip_item.SetToolTip(button_item_delete, "Delete");
        }

        private void button_close_MouseHover(object sender, EventArgs e)
        {
            toolTip_item.SetToolTip(button_close, "Close");
        }

        #endregion

        private void button_icon_search_Click(object sender, EventArgs e)
        {
            Form_IconSearch frmIconSearch = new Form_IconSearch("items");
            frmIconSearch.ShowDialog();
            textBox_item_icon.Text = string.IsNullOrEmpty(frmIconSearch.ReturnValue) ? "0" : frmIconSearch.ReturnValue;
        }
    } 
}