using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TabControl_Spawns {
    public partial class Form_SpawnScript : Form {
        private MySqlConnection connection;
        private MySqlEngine db;
        private LUAInterface lua_interface;
        private int id;
        private int spawn_id;
        private int spawn_group_id;
        private int spawn_entry_id;
        private bool saved;
        private bool closing;
        private int option;

        public Form_SpawnScript(MySqlConnection connection, MySqlEngine db, int id, int spawn_id, int spawn_group_id, int spawn_entry_id) {
            InitializeComponent();
            this.connection = connection;
            this.db = db;
            this.lua_interface = new LUAInterface();
            this.id = id;
            this.spawn_id = spawn_id;
            this.spawn_group_id = spawn_group_id;
            this.spawn_entry_id = spawn_entry_id;
            saved = true;
            closing = false;
            option = 0;
        }

        private void Form_SpawnScript_Load(object sender, EventArgs e) {
            bool file_exists = false;
            string script_name = "<none>";
            MySqlDataReader reader = db.RunSelectQuery("SELECT lua_script " +
                                                       "FROM spawn_scripts " +
                                                       "WHERE id=" + id);
            if (reader != null) {
                if (reader.Read()) {
                    script_name = reader.GetString(0);
                    label_scriptname.Text = script_name;
                    this.Text = script_name;
                    if (lua_interface.LoadLUAFile(ref textBox_script, script_name))
                        file_exists = true;
                }
                reader.Close();
            }
            if (!file_exists)
                textBox_script.Text = lua_interface.GetSpawnScriptDefaultHeader(script_name, spawn_id, spawn_group_id, spawn_entry_id);
            if (this.Text[0] == '*')
                this.Text = this.Text.Remove(0, 1);
            lua_interface.CheckLUASyntaxWholeFile(ref textBox_script);
            saved = true;
        }

        private void textBox_script_TextChanged(object sender, EventArgs e) {
            option = lua_interface.CheckLUASyntax(ref textBox_script, textBox_script.SelectionStart, option);
            if (saved)
                this.Text = "*" + this.Text;
            saved = false;
        }

        private void button_save_Click(object sender, EventArgs e) {
            if (lua_interface.SaveLUAFile(ref textBox_script, label_scriptname.Text)) {
                if (!saved)
                    this.Text = this.Text.Remove(0, 1);
                saved = true;
            }
        }

        private void CloseForm() {
            if (!closing) {
                if (!saved) {
                    DialogResult result = MessageBox.Show("The script '" + label_scriptname.Text + "' has changed and has not been saved.\n\nDo you want to save the changes now?", "Spawn Script Changed!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes) {
                        if (lua_interface.SaveLUAFile(ref textBox_script, label_scriptname.Text)) {
                            closing = true;
                            this.Close();
                        }
                    }
                    else if (result == DialogResult.No) {
                        closing = true;
                        this.Close();
                    }
                }
                else {
                    closing = true;
                    this.Close();
                }
            }
        }

        private void button_close_Click(object sender, EventArgs e) {
            CloseForm();
        }

        private void Form_SpawnScript_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            CloseForm();
        }
    }
}