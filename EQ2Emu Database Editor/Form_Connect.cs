using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EQ2Emu_Database_Editor {
    public partial class Form_Connect : Form {
        private MySqlConnection connection;
        private bool connection_good;

        public static string server_path;
        public static string database_path;
        public static string username_path;
        public static string password_path;

        public Form_Connect()
        {
            InitializeComponent();
            Settings.Load();
            fillTextBoxServer();
            fillTextBoxDatabase();
            fillTextBoxUserName();
            fillTextBoxPassword();
        }

        private void fillTextBoxServer()
        {
            textBox_server.Text = server_path;
        }

        private void fillTextBoxDatabase()
        {
            textBox_database.Text = database_path;
        }

        private void fillTextBoxUserName()
        {
            textBox_user.Text = username_path;
        }
        private void fillTextBoxPassword()
        {
            textBox_password.Text = password_path;
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox_server.Text))
            {
                MessageBox.Show("Server path not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (String.IsNullOrEmpty(textBox_user.Text))
            {
                MessageBox.Show("Username not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (String.IsNullOrEmpty(textBox_password.Text))
            {
                MessageBox.Show("Password not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (String.IsNullOrEmpty(textBox_database.Text))
            {
                MessageBox.Show("Database not set!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                server_path = textBox_server.Text;
                username_path = textBox_user.Text;
                password_path = textBox_password.Text;
                database_path = textBox_database.Text;

                saveSettings();

                this.Close();
            }
        }

        private void saveSettings()
        {
            Settings.Save(server_path, database_path, username_path, password_path);
            MessageBox.Show("Credentials Saved. Attempting to connect.");
        }

        private void button_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public MySqlConnection GetConnection() {
            return connection;
        }

        public string GetUser() {
            return textBox_user.Text;
        }

        public bool ConnectionIsGood() {
            return connection_good;
        }
    }
}