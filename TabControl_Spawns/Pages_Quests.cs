using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace TabControl_Spawns
{
    public partial class Pages_Quests : UserControl
    {
        private MySqlEngine db;
        private TabPage owner;
        private LUAInterface lua_interface;

        public Pages_Quests(MySqlConnection connection, ref TabPage owner)
        {
            InitializeComponent();
            this.db = new MySqlEngine(connection);
            this.owner = owner;
            lua_interface = new LUAInterface();
        }

        private void pages_Quest_Load(object sender, EventArgs e)
        {
            PopulateZonesComboBox();
        }

        private bool PopulateZonesComboBox()
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, description " +
                                                       "FROM zones");
            if (reader != null)
            {
                comboBox_select_zone.Items.Clear();
                while (reader.Read())
                    comboBox_select_zone.Items.Add(reader.GetString(1) + "   (" + reader.GetString(0) + ")");
                reader.Close();
                return true;
            }
            return false;
        }

        private void comboBox_select_zone_SelectedIndexChanged(object sender, EventArgs e)
        {
            int zone_id = GetSelectedZoneID();
            if (zone_id == -1)
                return;

            ResetZone();
            //Details(true);

            tabControl_main.Visible = true;
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

        private void ResetZone()
        {

        }

        private void button_close_Click(object sender, EventArgs e)
        {
            owner.Dispose();
        }
    }
}
