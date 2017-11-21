using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace EQ2Emu_Database_Editor
{
    public partial class Form_Waypoint_Parser : Form
    {
        

        public Form_Waypoint_Parser()
        {
            InitializeComponent();
        }

        private void Form_Waypoint_Parser_Load(object sender, EventArgs e)
        {
        }

        private void button_parse_Click(object sender, EventArgs e)
        {
            string Output = "";

            string[] lines = richTextBox_log_input.Text.Split('\n');

            foreach (string line in lines)
            {

                // Here we call Regex for random pause
                Match matchRandom = Regex.Match(line, @"RandomPause", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (matchRandom.Success)
                {
                    Match RandomAmounts = Regex.Match(line, @"\([0-9]*\,[0-9]*\,[0-9]*\)", RegexOptions.IgnoreCase);
                    if (RandomAmounts.Success) {
                        string[] values = RandomAmounts.Value.Replace("(","").Replace(")","").Split(',');
                        if (values.Count() == 3)
                        {
                            checkBox_random_enable.Checked = true;
                            textBox_min_base.Text = values[0];
                            textBox_random_low.Text = values[1];
                            textBox_random_high.Text = values[2];
                        }
                    }
                }

                // Here we call Regex for default pause
                Match matchDefault = Regex.Match(line, @"DefaultPause", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (matchDefault.Success)
                {
                    Match DefaultAmount = Regex.Match(line, @"DefaultPause.*\([0-9]*\)", RegexOptions.IgnoreCase);
                    if (DefaultAmount.Success)
                    {
                        string value = DefaultAmount.Value.Replace("DefaultPause","").Replace("(", "").Replace(")", "").Trim();
                        textBox_default_time.Text = value;
                        checkBox_random_enable.Checked= false;
                    }
                }

                // Here we call Regex.Match.
                Match matchPosition = Regex.Match(line, @"Your location is.*Your orientation is", RegexOptions.IgnoreCase);

                // Here we check the Match instance.
                if (matchPosition.Success)
                {
                    if (matchPosition.Success)
                    {
                        string[] location = matchPosition.Value.Replace(@"Your location is ","").Replace(@".  Your orientation is","").Split(',');
                        if (location.Count() == 3)
                        {
                            string x = location[0].Trim();
                            string y = location[1].Trim();
                            string z = location[2].Trim();

                            Output += "MovementLoopAddLocation(NPC, ";
                            Output += x + ", ";
                            Output += y + ", ";
                            Output += z + ", ";
                            Output += textBox_walk_speed.Text +", ";

                            if (checkBox_random_enable.Checked)
                            {
                                Output += textBox_min_base.Text + " + math.random(" + textBox_random_low.Text + ", " + textBox_random_high.Text +"))"; 
                            } else {
                                Output += textBox_default_time.Text + ")";
                            }


                            Output += System.Environment.NewLine;
                        }
                    }
                }
            }

            if (checkBox_loop.Checked) // If loop is checked, go back through but skip last (first) line.
            {
                string[] loop = Output.Split('\n');

                for(int i=loop.Count()-3; i>=0; i--)
                {
                    Output += loop[i];
                }
            }
            richTextBox_log_input.Text = Output;
            checkBox_random_enable.Checked = false;
        }

        private void checkBox_random_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_random_enable.Checked)
            {
                textBox_min_base.Enabled = true;
                textBox_random_low.Enabled = true;
                textBox_random_high.Enabled = true;
                textBox_default_time.Enabled = false;
            }
            else {
                textBox_min_base.Enabled = false;
                textBox_random_low.Enabled = false;
                textBox_random_high.Enabled = false;
                textBox_default_time.Enabled = true;
            }
        }

        private void textBox_random_high_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_loop_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
