using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TabControl_Spawns
{
    public partial class NewQuest : Form
    {
        private MySqlEngine db;
        public NewQuest()
        {
            InitializeComponent();
        }

        private void button_newquest_add_Click(object sender, EventArgs e)
        {
            string spawn_id = textBox_newquest_spawnid.Text;
            string type = textBox_newquest_type.Text;
            string level = textBox_newquest_level.Text;
            string enclevel = textBox_newquest_enclevel.Text;
            string zone = textBox_newquest_zone.Text;
            string luascript = textBox_newquest_luascript.Text;
            string name = textBox_newquest_name.Text;
            string description = textBox_newquest_description.Text;
            string completedtext = textBox_newquest_completedtext.Text;

            int rows = db.RunQuery("INSERT INTO quests (spawn_id, type, level, enc_level, zone, lua_script, name, description, completed_text) " +
                                       "VALUES ('" + spawn_id + "', '" + type + "', '" + level + "', '" + enclevel + "', '" + zone + "', '" + luascript + "', '" + name + "', '" + description + "', '" + completedtext + "')");

            this.Close();
        }
    }
}
