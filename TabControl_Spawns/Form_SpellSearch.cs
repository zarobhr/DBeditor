using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using MySql.Data.MySqlClient;

namespace TabControl_Spawns
{
    public partial class Form_SpellSearch : Form
    {
        public Form_SpellSearch()
        {
            InitializeComponent();
        }

        struct spell
        {
            public string id;
            public string name;
        };

        private ArrayList list;
        private MySqlEngine db;
        public string ReturnId { get; set; }
        public Form_SpellSearch(MySqlEngine db)
        {
            InitializeComponent();
            list = new ArrayList();
            this.db = db;
        }

        private void button_Spell_Search_Click(object sender, EventArgs e)
        {
            LoadSpells();
        }

        private void LoadSpells()
        {
            listView_spell_search_results.Items.Clear();
            list.Clear();

            MySqlDataReader reader = db.RunSelectQuery("SELECT s.id, s.name FROM spells s " +
                                                       "WHERE s.name LIKE '%" + textBox_spell_search.Text + "%' LIMIT 250");

            if (reader != null)
            {
                while (reader.Read())
                {
                    spell s;
                    s.id = reader.GetString(0);
                    s.name = reader.GetString(1);

                    list.Add(s);
                }
                reader.Close();
            }

            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem spell = new ListViewItem(((spell)list[i]).id);
                spell.SubItems.Add(new ListViewItem.ListViewSubItem(spell, ((spell)list[i]).name));
                listView_spell_search_results.Items.Add(spell);
                count++;
            }
            label_display.Text = "Displaying " + count + " items.";
        }

        private void listView_spell_search_results_DoubleClick(object sender, EventArgs e)
        {
            int intselectedindex = listView_spell_search_results.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                this.ReturnId = listView_spell_search_results.Items[intselectedindex].SubItems[0].Text;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
