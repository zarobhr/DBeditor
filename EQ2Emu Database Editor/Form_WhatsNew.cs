using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EQ2Emu_Database_Editor {
    public partial class Form_WhatsNew : Form {
        public Form_WhatsNew() {
            InitializeComponent();
        }

        private void Form_WhatsNew_Load(object sender, EventArgs e) {
            AddDate(6, 4, 2009, true);
            AddChange("Added initial support for characters page.");
            AddDate(6, 3, 2009, false);
            AddChange("Spawn groups now display in the drop down menus.");
            AddChange("Spawn entries now display in the drop down menus.");
            AddChange("Made some enhancements to the spawn scripts page.");
            AddChange("Added a form to edit and save spawn scripts in.");
            AddChange("Added 'Get Script Name' button to the spawn scripts page. This button will get the proper script name for the spawn.");
            AddChange("Added LUA syntax highlighting.");
            AddChange("Added a refresh spawn button");
            AddChange("Minor fixes and enhancements.");
            AddDate(5, 27, 2009, false);
            AddChange("Added labels to appearances and visual states fields for better readability.");
            AddChange("Added a calculator to calculate item and quest step icons as requested by Fipp.");
            AddDate(5, 25, 2009, false);
            AddChange("Added new field in spawn called size_offset which was added to the database today.");
            AddDate(5, 23, 2009, false);
            AddChange("Satiation now displays in the drop down menus.");
            AddChange("Added merchants.");
            AddChange("Fixed entity commands that have more than one entity command per ID to display all of the entity commands associated with it.");
            AddDate(5, 21, 2009, false);
            AddChange("Factions now display in the drop down menus.");
            AddChange("Finished zones");
            AddDate(5, 20, 2009, false);
            AddChange("Added color sliders.");
            AddChange("Wing types now display in the drop down menus.");
            AddChange("Encounter Levels now display in the drop down menus.");
            AddChange("Added spawn duplicating.");
            AddDate(5, 19, 2009, false);
            AddChange("Race names now show in the drop down menus.");
            AddChange("Class names now show in the drop down menus.");
            AddChange("Gender names now show in the drop down menus.");
            AddChange("Appearance Equip Slot names now show in the drop down menus.");
            AddChange("Appearance Types now show in the drop down menu.");
            AddChange("Hair Types now show in the drop down menus.");
            AddChange("Facial Hair Types now show in the drop down menus.");
            AddChange("Chest Types now show in the drop down menus.");
            AddChange("Leg Types now show in the drop down menus.");
            AddChange("Fixed a crash when loading signs with no title.");
            AddChange("Fixed a bug with new drop down menus.");

            this.ActiveControl = button_close;
        }

        private void AddDate(int month, int day, int year, bool first_date) {
            string month_s = month.ToString();
            string day_s = day.ToString();
            string year_s = year.ToString();

            if (month_s.Length < 2)
                month_s = "0" + month_s;
            if (day_s.Length < 2)
                day_s = "0" + day_s;

            if (first_date)
                textBox_whatsnew.Text += ("==" + month_s + "/" + day_s + "/" + year_s + "\r\n");
            else
                textBox_whatsnew.Text += ("\r\n==" + month_s + "/" + day_s + "/" + year_s + "\r\n");
        }

        private void AddChange(string text) {
            textBox_whatsnew.Text += ("-- " + text + "\r\n");
        }

        private void button_close_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}