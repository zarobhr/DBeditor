using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TabControl_Spawns {
    public class MySqlEngine {
        private MySqlConnection connection;

        public MySqlEngine(MySqlConnection connection) {
            this.connection = connection;
        }

        public MySqlDataReader RunSelectQuery(string query) {
            MySqlDataReader reader = null;
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;

            try {
                reader = command.ExecuteReader();
                return reader;
            }
            catch (MySqlException ex) {
                if (reader != null)
                    reader.Close();
                MessageBox.Show(query + "\n\n" + ex.Message, "Error Running SQL Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public int RunQuery(string query) {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;

            try {
                return command.ExecuteNonQuery();
            }
            catch (MySqlException ex) {
                MessageBox.Show(query + "\n\n" + ex.Message, "Error Running SQL Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        public string RemoveEscapeCharacters(string str) {
            str = str.Replace("\\", "\\\\"); //this MUST be the first check
            str = str.Replace("'", "\\'");
            str = str.Replace("\"", "\\\"");
            return str;
        }
    }
}
