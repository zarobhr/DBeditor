using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace TabControl_Spawns {
    public partial class Form_NewItem : Form {
        private ArrayList types;

        public Form_NewItem() {
            InitializeComponent();

            types = new ArrayList();

            InitializeItemTypes();
        }

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
            types.Add(new ItemType(11, "Thrown"));
            types.Add(new ItemType(4, "Weapon"));
            for (int i = 0; i < types.Count; i++)
                comboBox_itemtype.Items.Add(((ItemType)types[i]).type);
            comboBox_itemtype.SelectedItem = "Normal";
        }

        public string GetItemName() {
            return textBox_itemname.Text;
        }

        public string GetItemType() {
            return (string)comboBox_itemtype.SelectedItem;
        }

        public bool LoadItemAfterInsert() {
            return checkBox_loaditem.Checked;
        }

        private void button_ok_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e) {
            this.Close();
        }

    }
}