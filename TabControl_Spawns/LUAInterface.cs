using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.DirectoryServices;
using System.Net;

namespace TabControl_Spawns {
    public class LUAInterface {
        static string[] EQ2EmuFunctions = { "AddConversationOption", "StartConversation", "CreateConversation", "FaceTarget", "PlayFlavor", "Say", "AddQuestStepChat", "AddQuestStepKill", "AddQuestStepLocation", "AddQuestStepSpell", "AddQuestStep", "AddQuestStepCompleteAction", "HasQuest", "HasCompletedQuest", "QuestIsComplete", "UpdateQuestStepDescription", "UpdateQuestTaskGroupDescription", "RegisterQuest", "AddQuestRewardCoin", "AddQuestRewardItem", "AddQuestSelectableRewardItem", "SetQuestPrereqLevel", "AddQuestPrereqQuest", "AddQuestPrereqRace", "SetCompletedDescription", "QuestReturnNPC", "UpdateQuestDescription", "GiveQuestReward", "AddQuestRewardFaction", "SetQuestRewardExp", "OfferQuest", "SetStepComplete", "GetLevel", "GetRace", "ProvidesQuest" };
        static char[] delims = {'\t', ' ', '(', ')', '\n', ',', '\r', '.'};
        static string[] keywords = {"function", "end", "if", "then", "else", "elseif", "and", "local", "true", "false", "not", "do", "while", "for", "return", "repeat", "until", "break", "in"};
        static string[] lua_libraries = {"math", "string", "io", "os", "debug"};
        private int string_start;

        public LUAInterface() {
            string_start = 0;
        }

        /* Options:
         *  0: normal
         *  1: in a comment
         *  2: in a string */
        public int CheckLUASyntax(ref RichTextBox textbox, int index, int option) {
            int return_val = 0;
            int start_index = GetWordStartIndex(textbox.Text, index);
            int end_index = GetWordEndIndex(textbox.Text, index);
            if (start_index < 0 || end_index < 0 /*|| start_index >= textbox.Text.Length || end_index >= textbox.Text.Length*/)
                return option;
            /*if (option == 1) {
            }
            else if (option == 2) {
                if (textbox.Text[index - 1] == '"') {
                    int length = index - string_start;
                    if (length > 0) {
                        textbox.SelectionStart = string_start;
                        textbox.SelectionLength = index - string_start;
                        textbox.SelectionColor = Color.Teal;
                        textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);

                        textbox.SelectionStart = index;
                        textbox.SelectionLength = 0;
                        textbox.SelectionColor = Color.White;
                        textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
                        return_val = 0;
                    }
                    else
                        return_val = 2;
                }
                else
                    return_val = 2;
            }
            else {
                if (textbox.Text[index - 1] == '"') {
                    string_start = index - 1;
                    return_val = 2;
                }
                else {*/
                    string word = textbox.Text.Substring(start_index, (end_index - start_index));
                    textbox.SelectionStart = start_index;
                    textbox.SelectionLength = end_index - start_index;
                    if (WordIsLUAKeyword(word)) {
                        textbox.SelectionColor = Color.LightBlue;
                        textbox.SelectionFont = new Font(textbox.Font, FontStyle.Bold);
                    }
                    else if (WordIsEQ2EmuFunction(word)) {
                        textbox.SelectionColor = Color.White;
                        textbox.SelectionFont = new Font(textbox.Font, FontStyle.Bold);
                    }
                    else if (WordIsImmediate(word)) {
                        textbox.SelectionColor = Color.Tomato;
                        textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
                    }
                    else if (WordIsLUALibrary(word)) {
                        textbox.SelectionColor = Color.MediumPurple;
                        textbox.SelectionFont = new Font(textbox.Font, FontStyle.Bold);
                    }
                    else {
                        textbox.SelectionColor = Color.White;
                        textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
                    }
                    return_val = 0;
                //}
                textbox.SelectionStart = index;
                textbox.SelectionLength = 0;
                textbox.SelectionColor = Color.White;
                textbox.SelectionFont = new Font(textbox.Font, FontStyle.Regular);
            //}
            return return_val;
        }

        public void CheckLUASyntaxWholeFile(ref RichTextBox textbox) {
            int option = 0;
            for (int i = 0; i < textbox.Text.Length; i++)
                option = CheckLUASyntax(ref textbox, i, option);
        }

        private int GetWordStartIndex(string text, int index) {
            int start_index = index - 1;
            while (start_index > 0 && start_index < text.Length) {
                char letter = text[start_index];
                if (CharIsDelim(letter)) {
                    start_index = start_index + 1;
                    break;
                }
                start_index--;
            }
            return start_index;
        }

        private int GetWordEndIndex(string text, int index) {
            int end_index = index;
            while (end_index > 0 && end_index < text.Length) {
                char letter = text[end_index];
                if (CharIsDelim(letter)) {
                    break;
                }
                end_index++;
            }
            return end_index;
        }

        private bool CharIsDelim(char c) {
            for (int i = 0; i < delims.Length; i++)
                if (delims[i] == c)
                    return true;
            return false;
        }

        private bool WordIsLUAKeyword(string word) {
            for (int i = 0; i < keywords.Length; i++)
                if (keywords[i] == word)
                    return true;
            return false;
        }

        private bool WordIsImmediate(string word) {
            try {
                int num = Convert.ToInt32(word);
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        private bool WordIsEQ2EmuFunction(string word) {
            for (int i = 0; i < EQ2EmuFunctions.Length; i++)
                if (EQ2EmuFunctions[i] == word)
                    return true;
            return false;
        }

        private bool WordIsLUALibrary(string word) {
            for (int i = 0; i < lua_libraries.Length; i++)
                if (lua_libraries[i] == word)
                    return true;
            return false;
        }

        private string GetDate() {
            DateTime today = DateTime.Now;
            string month = today.Month.ToString();
            string day = today.Day.ToString();
            if (month.Length == 1)
                month = "0" + month;
            if (day.Length == 1)
                day = "0" + day;
            return today.Year + "." + month + "." + day;
        }

        public bool LoadLUAFile(ref RichTextBox textbox, string file_name) {
            WebClient client = new WebClient();
            try {
                string text = client.DownloadString(Properties.Settings.Default.ScriptLocation.ToString().Replace("/","") + "/" + file_name);
                textbox.Text = text;
                return true;
            }
            catch (Exception ex) {
            }
            return false;
        }

        public bool SaveLUAFile(ref RichTextBox textbox, string file_name) {
            WebClient client = new WebClient();
            string text = textbox.Text;
            try {
                client.UploadString(Properties.Settings.Default.ScriptLocation.ToString().Replace("/", "") + "/" + file_name, text);
                return true;
            }
            catch (Exception ex)  {
                MessageBox.Show(ex.Message + "\n\nFile not saved!", "Save Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public string GetSpawnScriptDefaultHeader(string script_name, int spawn_id, int spawn_group_id, int spawn_entry_id) {
            return "--[[\r\n" +
                   "\tScript Name: " + script_name + "\r\n" +
                   "\tSpawn ID: " + spawn_id + "\r\n" + 
                   "\tSpawn Group ID: " + spawn_group_id + "\r\n" +
                   "\tSpawn Entry ID: " + spawn_entry_id + "\r\n" + 
                   "\tAuthor:\r\n" +
                   "\tDate: " + GetDate() + "\r\n" +
                   "--]]\r\n\r\n" +
                   "function spawn(NPC)\r\n" +
                   "end\r\n\r\n" +
                   "function respawn(NPC)\r\n" +
                   "\tspawn(NPC)\r\n" +
                   "end\r\n\r\n" +
                   "function hailed(NPC, Spawn)\r\n" +
                   "\tFaceTarget(NPC, Spawn)\r\n" +
                   "end";
        }
    }
}
