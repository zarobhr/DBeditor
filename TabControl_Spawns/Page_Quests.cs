using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace TabControl_Spawns
{
    public partial class Page_Quests : UserControl
    {
        private TabPage owner;
        private MySqlEngine db;
        private MySqlConnection connection;

        public static string server_path;
        public static string database_path;
        public static string username_path;
        public static string password_path;
        public static string questparser_path;
        public static string luaeditor_path;
        public static string questscript_path;
        public string nextdetailsid;

        private ArrayList details_types;
        private ArrayList details_subtypes;

        public Page_Quests(MySqlConnection connection, ref TabPage owner)
        {
            InitializeComponent();
            this.connection = connection;
            this.owner = owner;
            db = new MySqlEngine(this.connection);

            details_types = new ArrayList();
            details_subtypes = new ArrayList();
            InitializeDetailsTypes();
            InitializeDetailsSubTypes();
        }

        private void Page_Quests_Load(object sender, EventArgs e)
        {
            InitializeQuestComboBox();
        }

        private void comboBox_quest_select_zone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_quest_select_zone.SelectedItem == null)
                return;

            label_select_quest.Visible = true;
            comboBox_quest_select_quest.SelectedItem = null;
            comboBox_quest_select_quest.Visible = true;
            owner.Text = "Quest: <none>";

            string type = (string)comboBox_quest_select_zone.SelectedItem;
            int start = type.IndexOf("(");
            int stop = type.IndexOf(")");
            string name = type.Substring(start + 1, stop - start - 1);

            MySqlDataReader reader = db.RunSelectQuery("SELECT quest_id, name " +
                                                       "FROM quests " +
                                                       "WHERE lua_script RLIKE '" + name + "'");

            if (reader != null)
            {
                comboBox_quest_select_quest.Items.Clear();
                while (reader.Read())
                    comboBox_quest_select_quest.Items.Add(reader.GetString(0) + "  (" + reader.GetString(1) + ")");
                reader.Close();
            }
        }

        private void comboBox_quest_select_quest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetDetailsTab(true);
            ResetQuestTab();
            LoadQuest();
            LoadDetails();
            tabControl_main.Visible = true;
            tabControl_main.TabPages.Remove(tabPage_quest_script);
        }

        private void InitializeQuestComboBox()
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, name, description " +
                                                       "FROM zones " +
                                                       "WHERE id in (SELECT zone_id FROM spawn_location_placement) " +
                                                       "AND name IN (SELECT DISTINCT SUBSTR(SUBSTRING_INDEX(lua_script, '/', 2), 8) AS zone_name FROM quests);");

            if (reader != null)
            {
                comboBox_quest_select_zone.Items.Clear();
                while (reader.Read())
                    comboBox_quest_select_zone.Items.Add(reader.GetString(2) + "   (" + reader.GetString(1) + ")");
                reader.Close();
            }
            owner.Text = "Quest: <none>";
        }

        private void InitializeDetailsTypes()
        {
            details_types.Add(new DetailsType(0, "Prereq"));
            details_types.Add(new DetailsType(1, "Reward"));
            for (int i = 0; i < details_types.Count; i++)
                comboBox_quest_details_type.Items.Add(((DetailsType)details_types[i]).details_type);
        }

        private void InitializeDetailsSubTypes()
        {
            details_subtypes.Add(new DetailsSubtype(0, "Experience"));
            details_subtypes.Add(new DetailsSubtype(1, "Faction"));
            details_subtypes.Add(new DetailsSubtype(2, "Item"));
            details_subtypes.Add(new DetailsSubtype(3, "Quest"));
            details_subtypes.Add(new DetailsSubtype(4, "Race"));
            details_subtypes.Add(new DetailsSubtype(5, "AdvClass"));
            details_subtypes.Add(new DetailsSubtype(6, "AdvLevel"));
            details_subtypes.Add(new DetailsSubtype(7, "TSClass"));
            details_subtypes.Add(new DetailsSubtype(8, "TSLevel"));
            details_subtypes.Add(new DetailsSubtype(9, "Coin"));
            details_subtypes.Add(new DetailsSubtype(10, "Selectable"));
            details_subtypes.Add(new DetailsSubtype(11, "MaxCoin"));
            details_subtypes.Add(new DetailsSubtype(12, "MaxAdvLevel"));
            details_subtypes.Add(new DetailsSubtype(13, "MaxTSLevel"));
            details_subtypes.Add(new DetailsSubtype(14, "TSExperience"));
            for (int i = 0; i < details_subtypes.Count; i++)
                comboBox_quest_details_subtype.Items.Add(((DetailsSubtype)details_subtypes[i]).details_subtype);
        }

        /*********************************************************************************************************************************
         *                                               Reset Tabs
         *********************************************************************************************************************************/

        private void ResetQuestTab()
        {
            textBox_quest_description_id.Clear();
            textBox_quest_description_spawnid.Clear();
            textBox_quest_description_type.Clear();
            textBox_quest_description_level.Clear();
            textBox_quest_description_enclevel.Clear();
            textBox_quest_description_zone.Clear();
            textBox_quest_description_luascript.Clear();
            textBox_quest_description_name.Clear();
            textBox_quest_description_description.Clear();
            textBox_quest_description_completedtext.Clear();
        }

        private void ResetDetailsTab(bool include_listview)
        {
            if (include_listview)
            listView_quest_details.Items.Clear();

            textBox_quest_details_id.Clear();
            textBox_quest_details_questid.Clear();
            textBox_quest_details_factionid.Clear();
            comboBox_quest_details_type.SelectedItem = -1;
            comboBox_quest_details_subtype.SelectedItem = -1;
            textBox_quest_details_value.Clear();
            textBox_quest_details_quantity.Clear();
        }

        /*********************************************************************************************************************************
         *                                               Load
         *********************************************************************************************************************************/

        private void LoadQuest()
        {
            if (comboBox_quest_select_quest.SelectedItem == null)
                return;

            string search_for = "  (";
            string quest_id = (string)comboBox_quest_select_quest.SelectedItem;
            quest_id = quest_id.Substring(0, quest_id.IndexOf(search_for) + 1);
            quest_id = quest_id.Remove(quest_id.Length - 1);

            MySqlDataReader reader = db.RunSelectQuery("SELECT quest_id, name, type, zone, level, enc_level, description, spawn_id, completed_text, lua_script FROM quests WHERE quest_id = " + quest_id);

            if (reader != null)
            {
                if (reader.Read())
                {
                    textBox_quest_description_id.Text = reader.GetString(0);
                    textBox_quest_description_name.Text = reader.GetString(1);
                    textBox_quest_description_type.Text = reader.GetString(2);
                    textBox_quest_description_zone.Text = reader.GetString(3);
                    textBox_quest_description_level.Text = reader.GetString(4);
                    textBox_quest_description_enclevel.Text = reader.GetString(5);
                    try
                    {
                        textBox_quest_description_description.Text = reader.GetString(6);
                    }
                    catch
                    {
                        textBox_quest_description_description.Text = "";
                    }
                    textBox_quest_description_spawnid.Text = reader.GetString(7);
                    try
                    {
                        textBox_quest_description_completedtext.Text = reader.GetString(8);
                    }
                    catch
                    {
                        textBox_quest_description_completedtext.Text = "";
                    }
                    try
                    {
                        textBox_quest_description_luascript.Text = reader.GetString(9);
                    }
                    catch
                    {
                        textBox_quest_description_luascript.Text = "";
                    }
                    reader.Close();

                    owner.Text = "Quest: " + textBox_quest_description_name.Text;
                }
            }
            reader.Close();
            return;
        }

        private void LoadDetails()
        {
            string search_for = "  (";
            string quest_id = (string)comboBox_quest_select_quest.SelectedItem;
            quest_id = quest_id.Substring(0, quest_id.IndexOf(search_for) + 1);
            quest_id = quest_id.Remove(quest_id.Length - 1);

            MySqlDataReader reader = db.RunSelectQuery("SELECT id, quest_id, type, subtype, value, faction_id, quantity " +
                "FROM quest_details " + "WHERE quest_details.quest_id=" + quest_id);

            if (reader != null)
            {
                while (reader.Read())
                {
                    ListViewItem details = new ListViewItem(reader.GetString(0));
                    details.SubItems.Add(new ListViewItem.ListViewSubItem(details, reader.GetString(1)));
                    details.SubItems.Add(new ListViewItem.ListViewSubItem(details, reader.GetString(2)));
                    details.SubItems.Add(new ListViewItem.ListViewSubItem(details, reader.GetString(3)));
                    details.SubItems.Add(new ListViewItem.ListViewSubItem(details, reader.GetString(4)));
                    details.SubItems.Add(new ListViewItem.ListViewSubItem(details, reader.GetString(5)));
                    details.SubItems.Add(new ListViewItem.ListViewSubItem(details, reader.GetString(6)));
                    listView_quest_details.Items.Add(details);
                    textBox_quest_details_questid.Text = reader.GetString(1);
                }
                reader.Close();
            }
            LoadDetailsId();
        }

        private void LoadDetailsId()
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT max(id) " + "FROM quest_details");
            if (reader != null)
            {
                if (reader.Read())
                {
                    int currentdetailsid = int.Parse(reader.GetString(0));
                    currentdetailsid++;
                    nextdetailsid = currentdetailsid.ToString();
                    textBox_quest_details_id.Text = nextdetailsid;
                    //textBox_quest_details_questid.Text = textBox_quest_description_id.Text;
                    reader.Close();
                }
            }
        }

        private void listView_quest_details_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_quest_details.SelectedIndices.Count == 0 || listView_quest_details.SelectedIndices[0] == -1)
            {
                ResetDetailsTab(false);
                return;
            }

            ListViewItem details = listView_quest_details.Items[listView_quest_details.SelectedIndices[0]];
            textBox_quest_details_id.Text = details.Text;
            textBox_quest_details_questid.Text = details.SubItems[1].Text;
            comboBox_quest_details_type.Text = details.SubItems[2].Text;
            comboBox_quest_details_subtype.Text = details.SubItems[3].Text;
            textBox_quest_details_value.Text = details.SubItems[4].Text;
            textBox_quest_details_factionid.Text = details.SubItems[5].Text;
            textBox_quest_details_quantity.Text = details.SubItems[6].Text;
        }

        /*********************************************************************************************************************************
         *                                               Buttons
         *********************************************************************************************************************************/

        private void button_close_Click(object sender, EventArgs e)
        {
            owner.Dispose();
        }

        private void button_quest_description_update_Click(object sender, EventArgs e)
        {
            int questid = int.Parse(textBox_quest_description_id.Text);
            string questname = db.RemoveEscapeCharacters(textBox_quest_description_name.Text);
            string questtype = db.RemoveEscapeCharacters(textBox_quest_description_type.Text);
            string questzone = db.RemoveEscapeCharacters(textBox_quest_description_zone.Text);
            int questlevel = int.Parse(textBox_quest_description_level.Text);
            int questenclevel = int.Parse(textBox_quest_description_enclevel.Text);
            string questdescription = db.RemoveEscapeCharacters(textBox_quest_description_description.Text);
            int questspawnid = int.Parse(textBox_quest_description_spawnid.Text);
            string questcompletedtext = db.RemoveEscapeCharacters(textBox_quest_description_completedtext.Text);
            string questluaspawnscripts = db.RemoveEscapeCharacters(textBox_quest_description_luascript.Text);

            int rows = db.RunQuery("UPDATE quests " +
                                    "SET name='" + questname + "', type='" + questtype + "', zone='" + questzone + "', level=" + questlevel + ", enc_level=" + questenclevel + ", description='" + questdescription + "', spawn_id=" + questspawnid + ", completed_text='" + questcompletedtext + "', lua_script='" + questluaspawnscripts + "' " +
                                    "WHERE quest_id=" + questid);
            if (rows > 0)
            {
                System.Media.SystemSounds.Beep.Play();
            }

            ResetDetailsTab(true);
            ResetQuestTab();

            LoadQuest();
            LoadDetails();
        }

        private void button_quest_description_remove_Click(object sender, EventArgs e)
        {
            int questid = int.Parse(textBox_quest_description_id.Text);

            DialogResult dlgresults = MessageBox.Show("Are you sure you want to delete this quest?.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresults == DialogResult.Yes)
            {
                int rows = db.RunQuery("DELETE FROM quests Where quest_id=" + questid);
                if (rows > 0)
                {
                    MessageBox.Show("Quest successfully deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ResetDetailsTab(true);
                ResetQuestTab();
                comboBox_quest_select_quest.Items.Clear();
                comboBox_quest_select_quest.SelectedItem = -1;
                InitializeQuestComboBox();
            }
            else if (dlgresults == DialogResult.No)
            {
                MessageBox.Show("Quest was not deleted.", "Cancled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button_quest_details_insert_Click(object sender, EventArgs e)
        {
            int detailsid = int.Parse(textBox_quest_details_id.Text);
            int detailsquestid = int.Parse(textBox_quest_details_questid.Text);
            string detailstype = (string)comboBox_quest_details_type.SelectedItem;
            string detailssubtype = (string)comboBox_quest_details_subtype.SelectedItem;
            int detailsvalue = int.Parse(textBox_quest_details_value.Text);
            int detailsfactionid = int.Parse(textBox_quest_details_factionid.Text);
            int detailsquantity = int.Parse(textBox_quest_details_quantity.Text);

            int rows = db.RunQuery("INSERT INTO quest_details (quest_id, type, subtype, value, faction_id, quantity) " +
                "VALUES (" + detailsquestid + ", '" + detailstype + "', '" + detailssubtype + "', " + detailsvalue + ", " + detailsfactionid + ", " + detailsquantity + ")");
            if (rows > 0)
            {
                listView_quest_details.Items.Clear();
                ResetDetailsTab(true);
                LoadDetails();
                MessageBox.Show("Details successfully added to quest", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_quest_details_update_Click(object sender, EventArgs e)
        {
            int detailsid = int.Parse(textBox_quest_details_id.Text);
            int detailsquestid = int.Parse(textBox_quest_details_questid.Text);
            string detailstype = (string)comboBox_quest_details_type.SelectedItem;
            string detailssubtype = (string)comboBox_quest_details_subtype.SelectedItem;
            int detailsvalue = int.Parse(textBox_quest_details_value.Text);
            int detailsfactionid = int.Parse(textBox_quest_details_factionid.Text);
            int detailsquantity = int.Parse(textBox_quest_details_quantity.Text);

            int rows = db.RunQuery("UPDATE quest_details " +
                                    "SET quest_id=" + detailsquestid + ", type='" + detailstype + "', subtype='" + detailssubtype + "', value=" + detailsvalue + ", faction_id=" + detailsfactionid + ", quantity=" + detailsquantity + " " +
                                    "WHERE id=" + detailsid);

            if (rows > 0)
            {
                listView_quest_details.Items.Clear();
                ResetDetailsTab(true);
                LoadDetails();
                MessageBox.Show("Details successfully successfully updated", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_quest_details_remove_Click(object sender, EventArgs e)
        {
            int detailsid = int.Parse(textBox_quest_details_id.Text);

            DialogResult dlgresults = MessageBox.Show("Are you sure you want to delete this quest?.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgresults == DialogResult.Yes)
            {
                int rows = db.RunQuery("DELETE FROM quest_details Where id=" + detailsid);
                if (rows > 0)
                {
                    MessageBox.Show("Quest details successfully deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                listView_quest_details.Items.Clear();
                ResetDetailsTab(true);
                LoadDetails();
            }
            else if (dlgresults == DialogResult.No)
            {
                MessageBox.Show("Quest details were not deleted.", "Cancled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button_quest_descripton_insert_Click(object sender, EventArgs e)
        {
            Form_NewQuest add_newquest = new Form_NewQuest(connection, db);
            add_newquest.ShowDialog();

            ResetDetailsTab(true);
            ResetQuestTab();
            comboBox_quest_select_quest.Items.Clear();
            comboBox_quest_select_quest.SelectedItem = -1;
            InitializeQuestComboBox();
        }

        private void tabPage_quest_description_Click(object sender, EventArgs e)
        {

        }
    }
}
