using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;


namespace EQ2Emu_Database_Editor {
    public partial class Form1 : Form {
        private MySqlConnection connection;
        private bool connection_good;
        private int seconds_connected;
        private string user;
        private string connecteddb;

        public static string server_path;
        public static string database_path;
        public static string username_path;
        public static string password_path;

        public Form1() {
            InitializeComponent();
            connection = null;
            connection_good = false;
            seconds_connected = 0;
            user = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string myConnectionString;

            Settings.Load();
            myConnectionString = "server=" + server_path + ";uid=" + username_path + ";pwd=" + password_path + ";database=" + database_path + ";";

            if (String.IsNullOrEmpty(server_path) || String.IsNullOrEmpty(username_path) || String.IsNullOrEmpty(password_path) || String.IsNullOrEmpty(database_path))
            {
                Form_Connect db_connection = new Form_Connect();
                db_connection.ShowDialog();
                Settings.Load();
                myConnectionString = "server=" + server_path + ";uid=" + username_path + ";pwd=" + password_path + ";database=" + database_path + ";";
                try
                {
                    connection = new MySqlConnection();
                    connection.ConnectionString = myConnectionString;
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                SetUserString();
                CalculateTimeConnected();
                timer_status.Start();
            }
            else
            {
                try
                {
                    connection = new MySqlConnection();
                    connection.ConnectionString = myConnectionString;
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                SetUserString();
                CalculateTimeConnected();
                timer_status.Start();
            }
          }

        private void linkLabel_characters_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            TabPage new_page = new TabPage("Character: <none>");
            TabControl_Spawns.Page_Character character_page = new TabControl_Spawns.Page_Character(connection, ref new_page);
            new_page.Controls.Add(character_page);
            tabControl_main.TabPages.Add(new_page);
        }

        private void linkLabel_items_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            TabPage new_page = new TabPage("Item: <none>");
            TabControl_Spawns.Page_Item spawn_page = new TabControl_Spawns.Page_Item(connection, ref new_page);
            new_page.Controls.Add(spawn_page);
            tabControl_main.TabPages.Add(new_page);
        }
        
        private void linkLabel_quests_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            TabPage new_page = new TabPage("Quests: <none>");
            TabControl_Spawns.Page_Quests quest_page = new TabControl_Spawns.Page_Quests(connection, ref new_page);
            new_page.Controls.Add(quest_page);
            tabControl_main.TabPages.Add(new_page);

        }
        
        private void linkLabel_spawns_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            TabPage new_page = new TabPage("Spawn: <none>");
            TabControl_Spawns.Page_Spawn spawn_page = new TabControl_Spawns.Page_Spawn(connection, ref new_page);
            new_page.Controls.Add(spawn_page);
            tabControl_main.TabPages.Add(new_page);
        }

        private void linkLabel_spells_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            TabPage new_page = new TabPage("Spell: <none>");
            TabControl_Spawns.Page_Spell spell_page = new TabControl_Spawns.Page_Spell(connection, ref new_page);
            new_page.Controls.Add(spell_page);
            tabControl_main.TabPages.Add(new_page);
        }

        private void linkLabel_zones_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            TabPage new_page = new TabPage("Zone: <none>");
            TabControl_Spawns.Page_Zone zone_page = new TabControl_Spawns.Page_Zone(connection, ref new_page);
            new_page.Controls.Add(zone_page);
            tabControl_main.TabPages.Add(new_page);
        }

        private void linkLabel_serverdetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            TabPage new_page = new TabPage("Server Details");
            TabControl_Spawns.Page_ServerDetails serverdetails_page = new TabControl_Spawns.Page_ServerDetails(connection, ref new_page);
            new_page.Controls.Add(serverdetails_page);
            tabControl_main.TabPages.Add(new_page);
        }

        private void linkLabel_Bulk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TabPage new_page = new TabPage("Bulk");
            TabControl_Spawns.Page_Bulk bulk_page = new TabControl_Spawns.Page_Bulk(connection, ref new_page);
            new_page.Controls.Add(bulk_page);
            tabControl_main.TabPages.Add(new_page);
        }

        /*********************************************************************************************************************************
         *                                               MENU ITEMS
         *********************************************************************************************************************************/

        private void menuItem_file_exit_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void menuItem_view_tabs_normal_Click(object sender, EventArgs e) {
            menuItem_view_tabs_normal.Checked = true;
            menuItem_view_tabs_buttons.Checked = false;
            menuItem_view_tabs_flatbuttons.Checked = false;
            tabControl_main.Appearance = TabAppearance.Normal;
        }

        private void menuItem_view_tabs_buttons_Click(object sender, EventArgs e) {
            menuItem_view_tabs_normal.Checked = false;
            menuItem_view_tabs_buttons.Checked = true;
            menuItem_view_tabs_flatbuttons.Checked = false;
            tabControl_main.Appearance = TabAppearance.Buttons;
        }

        private void menuItem_view_tabs_flatbuttons_Click(object sender, EventArgs e) {
            menuItem_view_tabs_normal.Checked = false;
            menuItem_view_tabs_buttons.Checked = false;
            menuItem_view_tabs_flatbuttons.Checked = true;
            tabControl_main.Appearance = TabAppearance.FlatButtons;
        }

        private void menuItem_calculators_itemqueststep_Click(object sender, EventArgs e) {
            new Form_Calculator("Item/Quest Step").Show();
        }

        private void menuItem_whatsnew_Click(object sender, EventArgs e) {
            new Form_WhatsNew().ShowDialog();
        }

        private void menuItem_about_Click(object sender, EventArgs e) {
            new Form_About().ShowDialog();
        }

        private void dBLoginToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string myConnectionString;

            Form_Connect db_connection = new Form_Connect();
            db_connection.ShowDialog();
            Settings.Load();
            myConnectionString = "server=" + server_path + ";uid=" + username_path + ";pwd=" + password_path + ";database=" + database_path + ";";
            try
            {
                connection = new MySqlConnection();
                connection.ConnectionString = myConnectionString;
                connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            tabControl_main.TabPages.Clear();
            seconds_connected = 0;
            user = "";

            SetUserString();
            CalculateTimeConnected();
            timer_status.Start();
        }

        /*********************************************************************************************************************************
         *                                               STATUS
         *********************************************************************************************************************************/

        private void timer_status_Tick(object sender, EventArgs e) {
            if (connection != null && connection.Ping()) {
                pictureBox_status.BackColor = Color.Green;
                seconds_connected++;
                CalculateTimeConnected();
            }
            else {
                pictureBox_status.BackColor = Color.Red;
                label_connected_user.Text = "Disconnected";
            }
        }

        private void CalculateTimeConnected() {
            int hours = (seconds_connected / 60 / 60) % 60;
            int minutes = (seconds_connected / 60) % 60;
            int seconds = seconds_connected % 60;

            string hours_s = hours.ToString();
            string minutes_s = minutes.ToString();
            string seconds_s = seconds.ToString();

            if (hours_s.Length < 2)
                hours_s = "0" + hours_s;
            if (minutes_s.Length < 2)
                minutes_s = "0" + minutes_s;
            if (seconds_s.Length < 2)
                seconds_s = "0" + seconds_s;
            
            label_status.Text = hours_s + ":" + minutes_s + ":" + seconds_s;
        }

        private void SetUserString()
        {
            connecteddb = database_path;
            user = username_path;

            label_connected_user.Text = "Connected as " + user + " to database " + connecteddb;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void waypointParserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_Waypoint_Parser().Show();
        }
    }
}