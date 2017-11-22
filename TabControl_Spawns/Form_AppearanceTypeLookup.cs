using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

struct EquipType {
    public string id;
    public string name;
};

namespace TabControl_Spawns {
    public partial class Form_AppearanceTypeLookup : Form {
        private ArrayList list;
        private MySqlEngine db;
        private ArrayList creatures;
        private ArrayList npc_wearables;
        private ArrayList wearable_items;
        private ArrayList slots;

        public string ReturnValue { get; set; }

        public Form_AppearanceTypeLookup(MySqlEngine db) {
            InitializeComponent();
            creatures = new ArrayList();
            npc_wearables = new ArrayList();
            wearable_items = new ArrayList();
            slots = new ArrayList();
            list = new ArrayList();
            this.db = db;
        }

        private void Search()
        {
            listView_equiptypes.Items.Clear();
            list.Clear();

            string query = "SELECT appearances.appearance_id, appearances.name FROM appearances WHERE name LIKE '%" + textBox_search_1.Text + "%' AND name LIKE '%" + textBox_search_2.Text + "%' ";
  
            if (comboBox_creature_types.SelectedIndex > 0)
            {
                int creature_type = GetCreatureId(comboBox_creature_types.SelectedItem.ToString());
                query += "AND creature_type = " + creature_type + " ";
            }

            if (comboBox_npc_wearable_types.SelectedIndex > 0) {
                int npc_wearable_type = GetWearableTypeId(comboBox_npc_wearable_types.SelectedItem.ToString());
                query += "AND npc_wearable_types = " + npc_wearable_type + " ";
            }

            if (comboBox_wearable_items.SelectedIndex > 0)
            {
                int wearable_item = GetWearableItemId(comboBox_wearable_items.SelectedItem.ToString());
                query += "AND wearable_item_types = " + wearable_item + " ";
            }

            if (comboBox_slots.SelectedIndex > 0)
            {
                int slot = GetSlotId(comboBox_slots.SelectedItem.ToString());
                query += "AND slot = " + slot + " ";
            }

            query += " LIMIT 500";

            MySqlDataReader reader = db.RunSelectQuery(query);

            if (reader != null)
            {
                while (reader.Read())
                {
                    EquipType es;
                    es.id = reader.GetString(0);
                    es.name = reader.GetString(1);
                    list.Add(es);
                }
                reader.Close();
            }

            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem item = new ListViewItem(((EquipType)list[i]).name);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, ((EquipType)list[i]).id));
                listView_equiptypes.Items.Add(item);
                count++;
            }
            label_display.Text = "Displaying " + count + " equip types.";
        }

        private void Form_EquipTypeLookup_Load(object sender, EventArgs e) {
            LoadCreatureTypes();
            LoadNpcWearables();
            LoadWearableItems();
            LoadSlots();
            LoadLastSettings();
            Search();
        }

        #region "Slots"

        private void LoadSlots()
        {
            if (slots.Count == 0)
            {
                slots.Add(new Slots(0, ""));
                slots.Add(new Slots(1, "Chest"));
                slots.Add(new Slots(2, "Feet"));
                slots.Add(new Slots(3, "Forearms"));
                slots.Add(new Slots(4, "Hand"));
                slots.Add(new Slots(5, "Head"));
                slots.Add(new Slots(6, "Legs"));
                slots.Add(new Slots(7, "Legs no skirt"));
                slots.Add(new Slots(8, "Pauldron Left"));
                slots.Add(new Slots(9, "Pauldron Right"));
                slots.Add(new Slots(10, "Shoulders"));
                slots.Add(new Slots(11, "Shoulders with Pauldrons"));
                slots.Add(new Slots(12, "Skirt"));
                slots.Add(new Slots(13, "Upper Chest"));
            }
            for (int i = 0; i < slots.Count; i++)
                comboBox_slots.Items.Add(((Slots)slots[i]).slot);

            comboBox_slots.SelectedIndex = 0;
        }


        private int GetSlotId(string item)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                Slots type = (Slots)slots[i];
                if (item == type.slot)
                    return type.id;
            }
            return -1;
        }

        private string GetSlotName(int id)
        {
            for (int i = 0; i < npc_wearables.Count; i++)
            {
                Slots type = (Slots)slots[i];
                if (id == type.id)
                    return type.slot;
            }
            return null;
        }

        #endregion

        #region "Wearable Items"

        private void LoadWearableItems(){

            if (wearable_items.Count == 0)
            {
                wearable_items.Add(new WearableItem(0, ""));
                wearable_items.Add(new WearableItem(1, "_exp07"));
                wearable_items.Add(new WearableItem(2, "cloak"));
                wearable_items.Add(new WearableItem(3, "snapons"));
                wearable_items.Add(new WearableItem(4, "woven"));
                wearable_items.Add(new WearableItem(5, "vanguard"));
                wearable_items.Add(new WearableItem(6, "tradesman"));
                wearable_items.Add(new WearableItem(7, "plate"));
                wearable_items.Add(new WearableItem(8, "magus"));
                wearable_items.Add(new WearableItem(9, "chain"));
                wearable_items.Add(new WearableItem(10, "profesion_hats"));
                wearable_items.Add(new WearableItem(11, "brigandine"));
                wearable_items.Add(new WearableItem(12, "leather"));
                wearable_items.Add(new WearableItem(13, "_exp08"));
                wearable_items.Add(new WearableItem(14, "clothing"));
                wearable_items.Add(new WearableItem(15, "journeyman"));
                wearable_items.Add(new WearableItem(16, "ghost"));
                wearable_items.Add(new WearableItem(17, "npcskirts"));
                wearable_items.Add(new WearableItem(18, "monk"));
                wearable_items.Add(new WearableItem(19, "starter_clothes"));
                wearable_items.Add(new WearableItem(20, "heavy_cloth"));
                wearable_items.Add(new WearableItem(21, "white_robe"));
                wearable_items.Add(new WearableItem(22, "heavy_plate"));
                wearable_items.Add(new WearableItem(23, "halloween_masks"));
                wearable_items.Add(new WearableItem(24, "heavy_chain"));
                wearable_items.Add(new WearableItem(25, "christmas_hats"));
                wearable_items.Add(new WearableItem(26, "pauldrons"));
                wearable_items.Add(new WearableItem(27, "_exp02"));
                wearable_items.Add(new WearableItem(28, "_exp03"));
                wearable_items.Add(new WearableItem(29, "character_create"));
                wearable_items.Add(new WearableItem(30, "accessories"));
                wearable_items.Add(new WearableItem(31, "_exp04"));
                wearable_items.Add(new WearableItem(32, "jester_hat"));
                wearable_items.Add(new WearableItem(33, "sallet_helmet"));
                wearable_items.Add(new WearableItem(34, "crown_of_tearis"));
                wearable_items.Add(new WearableItem(35, "_exp05"));
                wearable_items.Add(new WearableItem(36, "_exp06"));
                wearable_items.Add(new WearableItem(37, "elemental_crowns"));
                wearable_items.Add(new WearableItem(38, "robe_02"));
                wearable_items.Add(new WearableItem(39, "raf_helm"));
                wearable_items.Add(new WearableItem(40, "valkyrie"));
                wearable_items.Add(new WearableItem(41, "_exp09"));
                wearable_items.Add(new WearableItem(42, "_exp10"));
                wearable_items.Add(new WearableItem(43, "_exp11"));
                wearable_items.Add(new WearableItem(44, "_exp12"));
            }
            for (int i = 0; i < wearable_items.Count; i++)
                comboBox_wearable_items.Items.Add(((WearableItem)wearable_items[i]).item);

            comboBox_wearable_items.SelectedIndex = 0;
        }

        private int GetWearableItemId(string item)
        {
            for (int i = 0; i < wearable_items.Count; i++)
            {
                WearableItem type = (WearableItem)wearable_items[i];
                if (item == type.item)
                    return type.id;
            }
            return -1;
        }

        private string GetWearableItemName(int id)
        {
            for (int i = 0; i < npc_wearables.Count; i++)
            {
                WearableItem type = (WearableItem)wearable_items[i];
                if (id == type.id)
                    return type.item;
            }
            return null;
        }

        #endregion

        #region "NPC Wearables"

        private void LoadNpcWearables()
        {
            if (npc_wearables.Count == 0)
            {
                npc_wearables.Add(new NPCWearableType(0, ""));
                npc_wearables.Add(new NPCWearableType(1, "Coldaine"));
                npc_wearables.Add(new NPCWearableType(2, "Bolgin"));
                npc_wearables.Add(new NPCWearableType(3, "Ahkeva"));
                npc_wearables.Add(new NPCWearableType(4, "Construct Necro"));
                npc_wearables.Add(new NPCWearableType(5, "Crystaline Folk Blue"));
                npc_wearables.Add(new NPCWearableType(6, "Crystaline Folk Halas"));
                npc_wearables.Add(new NPCWearableType(7, "Crystaline Folk Red"));
                npc_wearables.Add(new NPCWearableType(8, "Cultist"));
                npc_wearables.Add(new NPCWearableType(9, "Death Knight Merc"));
                npc_wearables.Add(new NPCWearableType(10, "Dire Bear Armor"));
                npc_wearables.Add(new NPCWearableType(11, "Dracurion"));
                npc_wearables.Add(new NPCWearableType(12, "Efreeti Blue Turban"));
                npc_wearables.Add(new NPCWearableType(13, "Frostfell"));
                npc_wearables.Add(new NPCWearableType(14, "Fungusman 02"));
                npc_wearables.Add(new NPCWearableType(15, "Frost Giant - Kurns"));
                npc_wearables.Add(new NPCWearableType(16, "Grimling"));
                npc_wearables.Add(new NPCWearableType(17, "Half Elf Children"));
                npc_wearables.Add(new NPCWearableType(18, "Hydra"));
                npc_wearables.Add(new NPCWearableType(19, "Ice Shade"));
                npc_wearables.Add(new NPCWearableType(20, "Lava Pygmy"));
                npc_wearables.Add(new NPCWearableType(21, "Ogre - Ogguk"));
                npc_wearables.Add(new NPCWearableType(22, "Pirates"));
                npc_wearables.Add(new NPCWearableType(23, "Sea Nayad"));
                npc_wearables.Add(new NPCWearableType(24, "Shissar"));
                npc_wearables.Add(new NPCWearableType(25, "Sifaye Accessories"));
                npc_wearables.Add(new NPCWearableType(26, "Snow Orc"));
                npc_wearables.Add(new NPCWearableType(27, "Talonite Wearable"));
                npc_wearables.Add(new NPCWearableType(28, "Tserrina"));
                npc_wearables.Add(new NPCWearableType(29, "Ulthork Undead"));
                npc_wearables.Add(new NPCWearableType(30, "Voidman Arms"));
            }
            for (int i = 0; i < creatures.Count; i++)
                comboBox_npc_wearable_types.Items.Add(((NPCWearableType)npc_wearables[i]).item);

            comboBox_npc_wearable_types.SelectedIndex = 0;
        }

        private int GetWearableTypeId(string item)
        {
            for (int i = 0; i < npc_wearables.Count; i++)
            {
                NPCWearableType type = (NPCWearableType)npc_wearables[i];
                if (item == type.item)
                    return type.id;
            }
            return -1;
        }

        private string GetWearableTypeName(int id)
        {
            for (int i = 0; i < npc_wearables.Count; i++)
            {
                NPCWearableType type = (NPCWearableType)npc_wearables[i];
                if (id == type.id)
                    return type.item;
            }
            return null;
        }

        #endregion
        
        #region "Creature Type"

        private void LoadCreatureTypes()
        {

            if (creatures.Count == 0)
            {
                creatures.Add(new Creature(0, ""));
                creatures.Add(new Creature(1, "Boss"));
                creatures.Add(new Creature(2, "Creature"));
                creatures.Add(new Creature(3, "Pets"));
                creatures.Add(new Creature(4, "Tu"));
            }
            for (int i = 0; i < creatures.Count; i++)
                comboBox_creature_types.Items.Add(((Creature)creatures[i]).creature);

            comboBox_creature_types.SelectedIndex = 0;
        }

        private int GetCreatureId(string creature)
        {
            for (int i = 0; i < creatures.Count; i++)
            {
                Creature type = (Creature)creatures[i];
                if (creature == type.creature)
                    return type.id;
            }
            return -1;
        }

        private string GetCreatureName(int id)
        {
            for (int i = 0; i < creatures.Count; i++)
            {
                Creature type = (Creature)creatures[i];
                if (id == type.id)
                    return type.creature;
            }
            return null;
        }
        #endregion

        private void button_close_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void textBox_search_TextChanged(object sender, EventArgs e) {
            Properties.Settings.Default.AppearanceSearchText1 = textBox_search_1.Text;
            if (checkBox_searchwhiletyping.Checked)
                Search();
        }

        private void button_search_Click(object sender, EventArgs e) {
            Search();
        }



        private void listView_equiptypes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int intselectedindex = listView_equiptypes.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                this.ReturnValue = listView_equiptypes.Items[intselectedindex].SubItems[1].Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void listView_equiptypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listView_equiptypes.SelectedIndices.Count>0)
            {
                int intselectedindex = listView_equiptypes.SelectedIndices[0];
                string selected_id = listView_equiptypes.Items[intselectedindex].SubItems[1].Text;
                pbViewer.Image = (Image)Properties.Resources.ResourceManager.GetObject(selected_id);
             }
        }

        private void comboBox_creature_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void comboBox_npc_wearable_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void comboBox_wearable_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void comboBox_slots_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void LoadLastSettings()
        {
            comboBox_creature_types.SelectedItem = Properties.Settings.Default.AppearanceCreateType;
            comboBox_npc_wearable_types.SelectedItem = Properties.Settings.Default.AppearanceWearableType;
            comboBox_wearable_items.SelectedItem = Properties.Settings.Default.AppearanceWearableItems;
            comboBox_slots.SelectedItem = Properties.Settings.Default.AppearanceSlots;
            textBox_search_1.Text = Properties.Settings.Default.AppearanceSearchText1;
        }

        private void Form_AppearanceTypeLookup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.AppearanceWearableType = (string)comboBox_npc_wearable_types.SelectedItem;
            Properties.Settings.Default.AppearanceSlots = (string)comboBox_slots.SelectedItem;
            Properties.Settings.Default.AppearanceWearableItems = (string)comboBox_wearable_items.SelectedItem;
            Properties.Settings.Default.AppearanceCreateType = (string)comboBox_creature_types.SelectedItem;
            Properties.Settings.Default.Save();
        }

        void textBox_search_1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        void textBox_search_2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void textBox_search_2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Search();
        }
    }
}