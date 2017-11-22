using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TabControl_Spawns
{
    public partial class Form_NewQuest : Form
    {
        private MySqlEngine db;
        private MySqlConnection connection;

        public static string server_path;
        public static string database_path;
        public static string username_path;
        public static string password_path;

        public string nextquestid;

        public Form_NewQuest(MySqlConnection connection, MySqlEngine db)
        {
            this.connection = connection;
            this.db = db;
            //db = new MySqlEngine(this.connection);

            InitializeComponent();
            LoadQuestId();
        }

        private void LoadQuestId()
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT max(quest_id) " + "FROM quests");

            if (reader != null)
            {
                if (reader.Read())
                {
                    int currentquestid = int.Parse(reader.GetString(0));
                    currentquestid++;
                    nextquestid = currentquestid.ToString();
                    textBox_newquest_questid.Text = nextquestid;
                    reader.Close();
                }

            }
        }

        public void displayXml(QuestTag qtags)
        {
            // Look for a better way to convert byte to string.
            int qtagslevel;
            qtagslevel = qtags.Level;
            string textlevel;
            textlevel = qtagslevel.ToString();

            textBox_NewQuestType.Text = qtags.Category;
            textBox_NewQuestZone.Text = qtags.Category;
            textBox_NewQuestLevel.Text = textlevel;
            textBox_NewQuestDescription.Text = qtags.StarterText;
            textBox_NewQuestCompletedtext.Text = qtags.CompletionText;

        }

        /*********************************************************************************************************************************
         *                                               Tags
         *********************************************************************************************************************************/

        public class QuestTag
        {
            public string Category;
            public byte Level;
            public string CompletionText;
            public string StarterText;
        }


        /*********************************************************************************************************************************
         *                                               Buttons
         *********************************************************************************************************************************/

        private void btn_NewQuestCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_NewQuestAdd_Click(object sender, EventArgs e)
        {
            int questid = int.Parse(textBox_newquest_questid.Text);
            string questname = db.RemoveEscapeCharacters(textBox_NewQuestName.Text);
            string questtype = db.RemoveEscapeCharacters(textBox_NewQuestType.Text);
            string questzone = db.RemoveEscapeCharacters(textBox_NewQuestZone.Text);
            int questlevel = int.Parse(textBox_NewQuestLevel.Text);
            int questenclevel = int.Parse(textBox_NewQuestEncLevel.Text);
            string questdescription = db.RemoveEscapeCharacters(textBox_NewQuestDescription.Text);
            int questspawnid = int.Parse(textBox_newquest_spawnId.Text);
            string questcompletedtext = db.RemoveEscapeCharacters(textBox_NewQuestCompletedtext.Text);
            string questluaspawnscripts = db.RemoveEscapeCharacters(textBox_NewQuestLuaScript.Text);

            int rows = db.RunQuery("INSERT INTO quests (quest_id, name, type, zone, level, enc_level, description, spawn_id, completed_text, lua_script) " +
                "VALUES (" + questid + ", '" + questname + "', '" + questtype + "', '" + questzone + "', " + questlevel + ", " + questenclevel + ", '" + questdescription + "', " + questspawnid + ", '" + questcompletedtext + "', '" + questluaspawnscripts + "')");
            if (rows > 0)
            {
                MessageBox.Show("New Quest added to database", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_NewQuestParse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_NewQuestName.Text))
            {
                MessageBox.Show("No Quest Name entered", "Missing Quest Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string url = "http://census.daybreakgames.com/xml/get/eq2/quest?name=";
            QuestTag qtag = null;
            XmlReader reader = XmlReader.Create(url + textBox_NewQuestName.Text);

            while (reader.Read())
            {
                // Every result from census.daybreakgames.com will have a quest_list element, even if no quests are returned
                // we will check the "returned" attribute to ensure we actually have a quest to parse.
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "quest_list")
                {
                    // If returned = 0 then we have no quest
                    if (reader.GetAttribute("returned") == "0")
                    {
                        reader.Close();
                        MessageBox.Show("Could not find " + textBox_NewQuestName.Text + " on census.daybreakgames.com", "Quest not found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Get the quest data from the quest element
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "quest")
                {
                    qtag = new QuestTag();
                    qtag.Category = reader.GetAttribute("category");
                    qtag.Level = byte.Parse(reader.GetAttribute("level"));
                    qtag.CompletionText = reader.GetAttribute("completion_text");
                    qtag.StarterText = reader.GetAttribute("starter_text");
                }

                // We only want to parse one quest so break the read loop at the first end element for a quest
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "quest")
                    break;
            }
            reader.Close();

            displayXml(qtag);
        }

        private void btn_NewQuestAdd_Click_1(object sender, EventArgs e)
        {
            int questid = int.Parse(textBox_newquest_questid.Text);
            string questname = db.RemoveEscapeCharacters(textBox_NewQuestName.Text);
            string questtype = db.RemoveEscapeCharacters(textBox_NewQuestType.Text);
            string questzone = db.RemoveEscapeCharacters(textBox_NewQuestZone.Text);
            int questlevel = int.Parse(textBox_NewQuestLevel.Text);
            int questenclevel = int.Parse(textBox_NewQuestEncLevel.Text);
            string questdescription = db.RemoveEscapeCharacters(textBox_NewQuestDescription.Text);
            int questspawnid = int.Parse(textBox_newquest_spawnId.Text);
            string questcompletedtext = db.RemoveEscapeCharacters(textBox_NewQuestCompletedtext.Text);
            string questluaspawnscripts = db.RemoveEscapeCharacters(textBox_NewQuestLuaScript.Text);

            int rows = db.RunQuery("INSERT INTO quests (quest_id, name, type, zone, level, enc_level, description, spawn_id, completed_text, lua_script) " +
                "VALUES (" + questid + ", '" + questname + "', '" + questtype + "', '" + questzone + "', " + questlevel + ", " + questenclevel + ", '" + questdescription + "', " + questspawnid + ", '" + questcompletedtext + "', '" + questluaspawnscripts + "')");
            if (rows > 0)
            {
                MessageBox.Show("New Quest added to database", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_NewQuestCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
