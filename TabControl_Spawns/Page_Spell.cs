using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace TabControl_Spawns {
    public partial class Page_Spell : UserControl {
        private MySqlEngine db;
        private TabPage owner;
        private LUAInterface lua_interface;
        private ArrayList classes;
        private ArrayList cast_types;
        private ArrayList target_types;
        private ArrayList spellbook_types;
        private ArrayList skills;
        private ArrayList spell_tiers;
        private ArrayList det_types;
        private ArrayList control_effect_types;
        private ArrayList is_given_by;

        public Page_Spell(MySqlConnection connection, ref TabPage owner) {
            InitializeComponent();
            this.db = new MySqlEngine(connection);
            this.owner = owner;
            lua_interface = new LUAInterface();
            classes = new ArrayList();
            cast_types = new ArrayList();
            target_types = new ArrayList();
            spellbook_types = new ArrayList();
            skills = new ArrayList();
            spell_tiers = new ArrayList();
            det_types = new ArrayList();
            control_effect_types = new ArrayList();
            is_given_by = new ArrayList();

            InitializeClasses();
            InitializeCastTypes();
            InitializeTargetTypes();
            InitializeSpellBookTypes();
            InitializeSkills();
            InitializeSpellTiers();
            InitializeDetType();
            InitializeControlEffectType();
            InitializeGivenBy();
        }

        private void comboBox_select_class_SelectedIndexChanged(object sender, EventArgs e) {
            if (PopulateSpellsComboBox()) {
                label_select_spell.Visible = true;
                comboBox_select_spell.Visible = true;
            }
            owner.Text = "Spell: <none>";
            tabControl_main.Visible = false;

            Properties.Settings.Default.LastSpellClass = (string)comboBox_select_class.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private void comboBox_select_spell_SelectedIndexChanged(object sender, EventArgs e) {
            int spell_id = GetSelectedSpellID();
            if (spell_id == -1)
                return;

            ResetSpell();
            ResetSpellTiers(true);
            ResetSpellClasses(true);
            ResetSpellData(true);
            ResetSpellDisplayEffects(true);
            ResetSpellScript();

            LoadSpell(spell_id);
            LoadSpellTiers(spell_id);
            LoadSpellClasses(spell_id);
            LoadSpellData(spell_id);
            LoadSpellDisplayEffects(spell_id);
            LoadSpellScript(spell_id);

            tabControl_main.Visible = true;

            Properties.Settings.Default.LastSpell = (string)comboBox_select_spell.SelectedItem;
            Properties.Settings.Default.Save();
        }

        private bool PopulateSpellsComboBox() {
            if (comboBox_select_class.SelectedItem == null)
                return false;
            int class_id = GetClassID((string)comboBox_select_class.SelectedItem);
            if (class_id == -1)
                return false;

            string SQL = "";

            if (class_id == 999)
            {
                SQL += "SELECT spells.`id`, spells.`name` " +
                        "FROM spells LEFT OUTER JOIN spell_classes " +
                        "ON spells.`id`= spell_classes.`spell_id` " +
                        "WHERE spell_classes.`adventure_class_id` IS NULL";
            } else {
                SQL += "SELECT spells.`id`, spells.`name` " +
                        "FROM spells INNER JOIN spell_classes " +
                        "ON spells.`id`=spell_classes.`spell_id` " +
                        "WHERE spell_classes.`adventure_class_id`=" + class_id;

                if (Settings.ShowInactiveSpells == 0)
                    SQL +=" AND is_active=1";

            }

            MySqlDataReader reader = db.RunSelectQuery(SQL);
 
            if (reader != null) {
                comboBox_select_spell.Items.Clear();
                while (reader.Read())
                    comboBox_select_spell.Items.Add(reader.GetString(1) + "   (" + reader.GetString(0) + ")");
                reader.Close();
                return true;
            }
            return false;

        }

        private int GetSelectedSpellID() {
            if (comboBox_select_spell.SelectedItem == null)
                return -1;

            string search_for = "   (";
            string spell_name = (string)comboBox_select_spell.SelectedItem;
            spell_name = spell_name.Substring(spell_name.IndexOf(search_for) + search_for.Length);
            spell_name = spell_name.Remove(spell_name.Length - 1);
            return Convert.ToInt32(spell_name);
        }

        private void InitializeClasses() {
            classes.Add(new Class(0, "Commoner"));
            classes.Add(new Class(1, "Fighter"));
            classes.Add(new Class(2, "Warrior"));
            classes.Add(new Class(3, "Guardian"));
            classes.Add(new Class(4, "Berserker"));
            classes.Add(new Class(5, "Brawler"));
            classes.Add(new Class(6, "Monk"));
            classes.Add(new Class(7, "Bruiser"));
            classes.Add(new Class(8, "Crusader"));
            classes.Add(new Class(9, "Shadowknight"));
            classes.Add(new Class(10, "Paladin"));
            classes.Add(new Class(11, "Priest"));
            classes.Add(new Class(12, "Cleric"));
            classes.Add(new Class(13, "Templar"));
            classes.Add(new Class(14, "Inquisitor"));
            classes.Add(new Class(15, "Druid"));
            classes.Add(new Class(16, "Warden"));
            classes.Add(new Class(17, "Fury"));
            classes.Add(new Class(18, "Shaman"));
            classes.Add(new Class(19, "Mystic"));
            classes.Add(new Class(20, "Defiler"));
            classes.Add(new Class(21, "Mage"));
            classes.Add(new Class(22, "Sorcerer"));
            classes.Add(new Class(23, "Wizard"));
            classes.Add(new Class(24, "Warlock"));
            classes.Add(new Class(25, "Enchanter"));
            classes.Add(new Class(26, "Illusionist"));
            classes.Add(new Class(27, "Coercer"));
            classes.Add(new Class(28, "Summoner"));
            classes.Add(new Class(29, "Conjuror"));
            classes.Add(new Class(30, "Necromancer"));
            classes.Add(new Class(31, "Scout"));
            classes.Add(new Class(32, "Rogue"));
            classes.Add(new Class(33, "Swashbuckler"));
            classes.Add(new Class(34, "Brigand"));
            classes.Add(new Class(35, "Bard"));
            classes.Add(new Class(36, "Troubador"));
            classes.Add(new Class(37, "Dirge"));
            classes.Add(new Class(38, "Predator"));
            classes.Add(new Class(39, "Ranger"));
            classes.Add(new Class(40, "Assasin"));
            classes.Add(new Class(999, "Other"));
            for (int i = 0; i < classes.Count; i++) {
                comboBox_select_class.Items.Add(((Class)classes[i]).class_name);
                comboBox_spellclasses_classid.Items.Add(((Class)classes[i]).class_name);
            }
        }

        private int GetClassID(string class_name) {
            for (int i = 0; i < classes.Count; i++) {
                Class class_ = (Class)classes[i];
                if (class_name == class_.class_name)
                    return class_.id;
            }
            return -1;
        }

        private string GetClassName(int class_id) {
            for (int i = 0; i < classes.Count; i++) {
                Class class_ = (Class)classes[i];
                if (class_id == class_.id)
                    return class_.class_name;
            }
            return null;
        }

        private void InitializeCastTypes() {
            cast_types.Add(new SpellCastType(0, "Normal"));
            cast_types.Add(new SpellCastType(1, "Toggle"));
            for (int i = 0; i < cast_types.Count; i++)
                comboBox_spell_casttype.Items.Add(((SpellCastType)cast_types[i]).cast_type);
        }

        private int GetCastTypeID(string cast_type) {
            for (int i = 0; i < cast_types.Count; i++) {
                SpellCastType type = (SpellCastType)cast_types[i];
                if (cast_type == type.cast_type)
                    return type.id;
            }
            return -1;
        }

        private string GetCastTypeName(int id) {
            for (int i = 0; i < cast_types.Count; i++) {
                SpellCastType type = (SpellCastType)cast_types[i];
                if (id == type.id)
                    return type.cast_type;
            }
            return null;
        }

        private void InitializeTargetTypes() {
            target_types.Add(new SpellTargetType(0, "Self"));
            target_types.Add(new SpellTargetType(1, "Enemy"));
            target_types.Add(new SpellTargetType(2, "Group AE"));
            target_types.Add(new SpellTargetType(3, "Caster Pet"));
            target_types.Add(new SpellTargetType(4, "Enemy Pet"));
            target_types.Add(new SpellTargetType(5, "Enemy Corpse"));
            target_types.Add(new SpellTargetType(6, "Group Corpse"));
            target_types.Add(new SpellTargetType(7, "None"));
            target_types.Add(new SpellTargetType(8, "Raid AE"));
            target_types.Add(new SpellTargetType(9, "Other group AE"));

            for (int i = 0; i < target_types.Count; i++)
                comboBox_spell_targettype.Items.Add(((SpellTargetType)target_types[i]).target_type);
        }

        private int GetTargetTypeID(string target_type) {
            for (int i = 0; i < target_types.Count; i++) {
                SpellTargetType type = (SpellTargetType)target_types[i];
                if (target_type == type.target_type)
                    return type.id;
            }
            return -1;
        }

        private string GetTargetTypeName(int id) {
            for (int i = 0; i < target_types.Count; i++) {
                SpellTargetType type = (SpellTargetType)target_types[i];
                if (id == type.id)
                    return type.target_type;
            }
            return null;
        }

        private void InitializeSpellBookTypes() {
            spellbook_types.Add(new SpellBookType(0, "Spell"));
            spellbook_types.Add(new SpellBookType(1, "Combat Art"));
            spellbook_types.Add(new SpellBookType(2, "Ability"));
            spellbook_types.Add(new SpellBookType(3, "Tradeskill"));
            spellbook_types.Add(new SpellBookType(4, "Not Shown"));
            for (int i = 0; i < spellbook_types.Count; i++)
                comboBox_spell_spellbooktype.Items.Add(((SpellBookType)spellbook_types[i]).spellbook_type);
        }

        private int GetSpellBookTypeID(string type_name) {
            for (int i = 0; i < spellbook_types.Count; i++) {
                SpellBookType type = (SpellBookType)spellbook_types[i];
                if (type_name == type.spellbook_type)
                    return type.id;
            }
            return -1;
        }

        private string GetSpellBookTypeName(int id) {
            for (int i = 0; i < spellbook_types.Count; i++) {
                SpellBookType type = (SpellBookType)spellbook_types[i];
                if (id == type.id)
                    return type.spellbook_type;
            }
            return null;
        }

        private void InitializeSkills() {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, name " +
                                                       "FROM skills");
            if (reader != null) {
                skills.Clear();
                comboBox_spell_classskill.Items.Clear();
                comboBox_spell_masteryskill.Items.Clear();
                comboBox_spell_classskill.Items.Add("");
                comboBox_spell_masteryskill.Items.Add("");
                while (reader.Read()) {
                    Skill skill = new Skill(reader.GetInt64(0), reader.GetString(1));
                    skills.Add(skill);
                    comboBox_spell_classskill.Items.Add(skill.skill);
                    comboBox_spell_masteryskill.Items.Add(skill.skill);
                }
                reader.Close();
            }
        }

        private long GetSkillID(string skill) {
            for (int i = 0; i < skills.Count; i++) {
                Skill s = (Skill)skills[i];
                if (skill == s.skill)
                    return s.id;
            }
            return -1;
        }

        private string GetSkillName(long id) {
            for (int i = 0; i < skills.Count; i++) {
                Skill s = (Skill)skills[i];
                if (id == s.id)
                    return s.skill;
            }
            return null;
        }

        private void InitializeSpellTiers() {
            spell_tiers.Add(new SpellTier(0, " "));
            spell_tiers.Add(new SpellTier(1, "Apprentice I"));
            spell_tiers.Add(new SpellTier(2, "Apprentice II"));
            spell_tiers.Add(new SpellTier(3, "Apprentice III"));
            spell_tiers.Add(new SpellTier(4, "Apprentice IV"));
            spell_tiers.Add(new SpellTier(5, "Adept I"));
            spell_tiers.Add(new SpellTier(6, "Adept II"));
            spell_tiers.Add(new SpellTier(7, "Adept III"));
            spell_tiers.Add(new SpellTier(9, "Master I"));
            spell_tiers.Add(new SpellTier(10, "Master II"));
            for (int i = 0; i < spell_tiers.Count; i++) {
                comboBox_spelltiers_tier.Items.Add(((SpellTier)spell_tiers[i]).tier);
                comboBox_spelldata_tier.Items.Add(((SpellTier)spell_tiers[i]).tier);
                comboBox_spelldisplayeffects_tier.Items.Add(((SpellTier)spell_tiers[i]).tier);
            }
        }

        private int GetSpellTierID(string tier) {
            for (int i = 0; i < spell_tiers.Count; i++) {
                SpellTier type = (SpellTier)spell_tiers[i];
                if (tier == type.tier)
                    return type.id;
            }
            return -1;
        }

        private string GetSpellTierName(int id) {
            for (int i = 0; i < spell_tiers.Count; i++) {
                SpellTier type = (SpellTier)spell_tiers[i];
                if (id == type.id)
                    return type.tier;
            }
            return null;
        }

        private void InitializeDetType()
        {
            det_types.Add(new DetType(0, " "));
            det_types.Add(new DetType(1, "Trauma"));
            det_types.Add(new DetType(2, "Arcane"));
            det_types.Add(new DetType(3, "Noxious"));
            det_types.Add(new DetType(4, "Elemental"));
            det_types.Add(new DetType(5, "Curse"));

            for (int i = 0; i < det_types.Count; i++)
                comboBox_spell_dettype.Items.Add(((DetType)det_types[i]).det_type);
        }

        private int GetDetTypeID(string det_type)
        {
            for (int i = 0; i < det_types.Count; i++)
            {
                DetType type = (DetType)det_types[i];
                if (det_type == type.det_type)
                    return type.id;
            }
            return -1;
        }

        private string GetDetTypeName(int id)
        {
            for (int i = 0; i < det_types.Count; i++)
            {
                DetType type = (DetType)det_types[i];
                if (id == type.id)
                    return type.det_type;
            }
            return null;
        }

        private void InitializeControlEffectType()
        {
            control_effect_types.Add(new ControlEffectType(0, " "));
            control_effect_types.Add(new ControlEffectType(1, "Mez"));
            control_effect_types.Add(new ControlEffectType(2, "Stifle"));
            control_effect_types.Add(new ControlEffectType(3, "Daze"));
            control_effect_types.Add(new ControlEffectType(4, "Stun"));

            for (int i = 0; i < control_effect_types.Count; i++)
                comboBox_spell_controleffecttype.Items.Add(((ControlEffectType)control_effect_types[i]).control_effect_type);
        }

        private int GetControlEffectTypeID(string control_effect_type)
        {
            for (int i = 0; i < control_effect_types.Count; i++)
            {
                ControlEffectType type = (ControlEffectType)control_effect_types[i];
                if (control_effect_type == type.control_effect_type)
                    return type.id;
            }
            return -1;
        }

        private string GetControlEffectTypeName(int id)
        {
            for (int i = 0; i < control_effect_types.Count; i++)
            {
                ControlEffectType type = (ControlEffectType)control_effect_types[i];
                if (id == type.id)
                    return type.control_effect_type;
            }
            return null;
        }

        private void InitializeGivenBy()
        {
            comboBox_spelltiers_givenby.DataSource = new[]
            {
                " ",
                "Alternate Advancement",
                "Character Trait",
                "Class",
                "Class Training",
                "Race",
                "Racial Innate",
                "Racial Tradition",
                "Spell Scroll",
                "Tradeskill Class",
                "Warder Spell",
                "all"
            };
            /*
            is_given_by.Add(new SpellGivenBy(0, " "));
            is_given_by.Add(new SpellGivenBy(1, "Alternate Advancement"));
            is_given_by.Add(new SpellGivenBy(2, "Character Trait"));
            is_given_by.Add(new SpellGivenBy(3, "class"));
            is_given_by.Add(new SpellGivenBy(4, "Class Training"));
            is_given_by.Add(new SpellGivenBy(5, "Race"));
            is_given_by.Add(new SpellGivenBy(6, "Racial Innate"));
            is_given_by.Add(new SpellGivenBy(7, "Racial Tradition"));
            is_given_by.Add(new SpellGivenBy(8, "Spell Scroll"));
            is_given_by.Add(new SpellGivenBy(9, "Tradeskill Class"));
            is_given_by.Add(new SpellGivenBy(10, "Warder Spell"));
            is_given_by.Add(new SpellGivenBy(11, "all"));

            for (int i = 0; i < is_given_by.Count; i++)
                comboBox_spelltiers_givenby.Items.Add(((SpellGivenBy)is_given_by[i]).given_by);
            **/
        }

        private int GetGivenByID(string given_by)
        {
            for (int i = 0; i < is_given_by.Count; i++)
            {
                SpellGivenBy type = (SpellGivenBy)is_given_by[i];
                if (given_by == type.given_by)
                    return type.id;
            }
            return -1;
        }

        private string GetGivenByName(int id)
        {
            for (int i = 0; i < is_given_by.Count; i++)
            {
                SpellGivenBy type = (SpellGivenBy)is_given_by[i];
                if (id == type.id)
                    return type.given_by;
            }
            return null;
        }

        /*************************************************************************************************************************
         *                                             SPELL
         *************************************************************************************************************************/

        private void LoadSpell(int spell_id)
        {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, soe_spell_crc, type, cast_type, name, description, icon, icon_heroic_op, icon_backdrop, class_skill, mastery_skill, min_class_skill_req, duration_until_cancel, target_type, success_message, fade_message, interruptable, cast_while_moving, lua_script, spell_visual, effect_message, spell_book_type, can_effect_raid, affect_only_group_members, display_spell_tier, friendly_spell, group_spell, det_type, control_effect_type, incurable, linked_timer_id, not_maintained, casting_flags, persist_through_death, savage_bar, savage_bar_slot, is_active, is_aa, is_deity, deity, last_auto_update, soe_last_update " +
                                                       "FROM spells " +
                                                       "WHERE id=" + spell_id);
            if (reader != null) {
                if (reader.Read()) {
                    textBox_spell_id.Text = reader.GetString(0);
                    textBox_spell_soespellcrc.Text = reader.GetString(1);
                    textBox_spell_type.Text = reader.GetString(2);
                    comboBox_spell_casttype.SelectedItem = GetCastTypeName(reader.GetInt32(3));
                    textBox_spell_name.Text = reader.GetString(4);
                    textBox_spell_description.Text = reader.GetString(5);
                    textBox_spell_icon.Text = reader.GetString(6);
                    textBox_spell_iconheroicop.Text = reader.GetString(7);
                    textBox_spell_iconbackdrop.Text = reader.GetString(8);
                    comboBox_spell_classskill.Text = GetSkillName(reader.GetInt64(9));
                    comboBox_spell_masteryskill.Text = GetSkillName(reader.GetInt64(10));
                    textBox_spell_minclassskillreq.Text = reader.GetString(11);
                    checkBox_spell_durationuntilcancel.Checked = Convert.ToBoolean(reader.GetInt32(12));
                    comboBox_spell_targettype.SelectedItem = GetTargetTypeName(reader.GetInt32(13));
                    try {textBox_spell_successmessage.Text = reader.GetString(14);} catch (Exception ex) {textBox_spell_successmessage.Text = "";}
                    try {textBox_spell_fademessage.Text = reader.GetString(15);} catch (Exception ex) {textBox_spell_fademessage.Text = "";}
                    checkBox_spell_interruptable.Checked = Convert.ToBoolean(reader.GetInt32(16));
                    checkBox_spellcastwhilemoving.Checked = Convert.ToBoolean(reader.GetInt32(17));
                    try {textBox_spell_luascript.Text = reader.GetString(18);} catch (Exception ex) {textBox_spell_luascript.Text = "";}
                    textBox_spell_spellvisual.Text = reader.GetString(19);
                    try {textBox_spell_effectmessage.Text = reader.GetString(20);} catch (Exception ex) {textBox_spell_effectmessage.Text = "";};
                    comboBox_spell_spellbooktype.SelectedItem = GetSpellBookTypeName(reader.GetInt32(21));
                    checkBox_spell_caneffectraid.Checked = Convert.ToBoolean(reader.GetInt32(22));
                    checkBox_spell_affectonlygroupmembers.Checked = Convert.ToBoolean(reader.GetInt32(23));
                    checkBox_spell_displayspelltier.Checked = Convert.ToBoolean(reader.GetInt32(24));
                    checkBox_spell_friendlyspell.Checked = Convert.ToBoolean(reader.GetInt32(25));
                    checkBox_spell_groupspell.Checked = Convert.ToBoolean(reader.GetInt32(26));
                    comboBox_spell_dettype.SelectedItem = GetCastTypeName(reader.GetInt32(27));
                    comboBox_spell_controleffecttype.SelectedItem = GetCastTypeName(reader.GetInt32(28));
                    checkBox_spell_incurable.Checked = Convert.ToBoolean(reader.GetInt32(29));
                    textBox_spell_linkedtimerid.Text = reader.GetString(30);
                    checkBox_spell_notmaintained.Checked = Convert.ToBoolean(reader.GetInt32(31));
                    textBox_spell_castingflags.Text = reader.GetString(32);
                    checkBox_spell_persistthroughdeath.Checked = Convert.ToBoolean(reader.GetInt32(33));
                    textBox_spell_savagebar.Text = reader.GetString(34);
                    textBox_spell_savagebarslot.Text = reader.GetString(35);
                    checkBox_spell_isactive.Checked = Convert.ToBoolean(reader.GetInt32(36));
                    checkBox_spell_isaa.Checked = Convert.ToBoolean(reader.GetInt32(37));
                    checkBox_spell_isdeity.Checked = Convert.ToBoolean(reader.GetInt32(38));
                    textBox_spell_deity.Text = reader.GetString(39);
                    textBox_spell_lastautoupdate.Text = reader.GetString(40);
                    textBox_spell_soelastupdate.Text = reader.GetString(41);

                    owner.Text = "Spell: " + textBox_spell_name.Text;
                }
                reader.Close();
            }
        }

        private void button_spell_update_Click(object sender, EventArgs e)
        {
            string id = textBox_spell_id.Text;
            string soe_spell_crc = textBox_spell_soespellcrc.Text;
            string type = textBox_spell_type.Text;
            int cast_type = GetCastTypeID((string)comboBox_spell_casttype.SelectedItem) == -1 ? 0 : GetCastTypeID((string)comboBox_spell_casttype.SelectedItem);
            string name = textBox_spell_name.Text;
            string description = db.RemoveEscapeCharacters(textBox_spell_description.Text);
            string icon = textBox_spell_icon.Text;
            string icon_heroic = textBox_spell_iconheroicop.Text;
            string icon_backdrop = textBox_spell_iconbackdrop.Text;
            long class_skill = GetSkillID((string)comboBox_spell_classskill.SelectedItem) == -1 ? 0 : GetSkillID((string)comboBox_spell_classskill.SelectedItem);
            long mastery_skill = GetSkillID((string)comboBox_spell_masteryskill.SelectedItem) == -1 ? 0 : GetSkillID((string)comboBox_spell_masteryskill.SelectedItem);
            string min_class_skill_req = textBox_spell_minclassskillreq.Text;
            int duration_until_cancel = Convert.ToInt32(checkBox_spell_durationuntilcancel.Checked);
            int target_type = GetTargetTypeID((string)comboBox_spell_targettype.SelectedItem) == -1 ? 0 : GetTargetTypeID((string)comboBox_spell_targettype.SelectedItem);
            string success_message = db.RemoveEscapeCharacters(textBox_spell_successmessage.Text);
            string fade_message = db.RemoveEscapeCharacters(textBox_spell_fademessage.Text);
            int interruptable = Convert.ToInt32(checkBox_spell_interruptable.Checked);
            int cast_while_moving = Convert.ToInt32(checkBox_spellcastwhilemoving.Checked);
            string luascript = db.RemoveEscapeCharacters(textBox_spell_luascript.Text);
            string spell_visual = textBox_spell_spellvisual.Text;
            string effect_message = db.RemoveEscapeCharacters(textBox_spell_effectmessage.Text);
            int spell_book_type = GetSpellBookTypeID((string)comboBox_spell_spellbooktype.SelectedItem);
            int can_effect_raid = Convert.ToInt32(checkBox_spell_caneffectraid.Checked);
            int affect_only_group_members = Convert.ToInt32(checkBox_spell_affectonlygroupmembers.Checked);
            int display_spell_tier = Convert.ToInt32(checkBox_spell_displayspelltier.Checked);
            int friendly_spell = Convert.ToInt32(checkBox_spell_friendlyspell.Checked);
            int group_spell = Convert.ToInt32(checkBox_spell_groupspell.Checked);
            int det_type = GetDetTypeID((string)comboBox_spell_dettype.Text) == -1 ? 0 : GetDetTypeID((string)comboBox_spell_dettype.Text);
            int control_effect_type = GetControlEffectTypeID((string)comboBox_spell_controleffecttype.Text) == -1 ? 0 :  GetControlEffectTypeID((string)comboBox_spell_controleffecttype.Text);
            int incurable = Convert.ToInt32(checkBox_spell_incurable.Checked);
            string linked_timer_id = textBox_spell_linkedtimerid.Text;
            int not_maintained = Convert.ToInt32(checkBox_spell_notmaintained.Checked);
            string casting_flags = textBox_spell_castingflags.Text;
            int persist_through_death = Convert.ToInt32(checkBox_spell_persistthroughdeath.Checked);
            string savage_bar = textBox_spell_savagebar.Text;
            string savage_bar_slot = textBox_spell_savagebarslot.Text;
            int is_active = Convert.ToInt32(checkBox_spell_isactive.Checked);
            int is_aa = Convert.ToInt32(checkBox_spell_isaa.Checked);
            int is_deity = Convert.ToInt32(checkBox_spell_isdeity.Checked);
            string deity = textBox_spell_deity.Text;
            string last_auto_update = textBox_spell_lastautoupdate.Text;
            string soe_last_update = textBox_spell_soelastupdate.Text;

            int rows = db.RunQuery("UPDATE spells SET type=" + type + ", cast_type=" + cast_type + ", name='" + name + "', description='" + description + "', icon=" + icon + ", icon_heroic_op=" + icon_heroic + ", icon_backdrop=" + icon_backdrop + ", class_skill= " + class_skill + ", mastery_skill=" + mastery_skill + ", min_class_skill_req=" + min_class_skill_req + ", duration_until_cancel=" + duration_until_cancel + ", target_type=" + target_type + ", success_message='" + success_message + "', fade_message='" + fade_message + "', interruptable=" + interruptable + ", cast_while_moving=" + cast_while_moving + ", lua_script='" + luascript + "', spell_visual=" + spell_visual + ", effect_message='" + effect_message + "', spell_book_type=" + spell_book_type + ", can_effect_raid=" + can_effect_raid + ", affect_only_group_members=" + affect_only_group_members + ", display_spell_tier=" + display_spell_tier + ", friendly_spell=" + friendly_spell + ", group_spell=" + group_spell + ", det_type=" + det_type + ", control_effect_type=" + control_effect_type + ", incurable=" + incurable + ", linked_timer_id=" + linked_timer_id + ", not_maintained=" + not_maintained + ", casting_flags=" + casting_flags + ", persist_through_death=" + persist_through_death + ", savage_bar=" + savage_bar + ", savage_bar_slot=" + savage_bar_slot + ", is_active=" + is_active + ", is_aa=" + is_aa + ", is_deity=" + is_deity + ", deity=" + deity + " WHERE id=" + id);

        }

        private void ResetSpell() {
        }

        /*************************************************************************************************************************
         *                                             SPELL TIERS
         *************************************************************************************************************************/

        private void LoadSpellTiers(int spell_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT  id, spell_id, tier, hp_req, hp_req_percent, hp_upkeep, power_req, power_req_percent, power_upkeep, savagery_req, savagery_req_percent, savagery_upkeep, dissonance_req, dissonance_req_percent, dissonance_upkeep, req_concentration, cast_time, recovery, recast, radius, max_aoe_targets, min_range, `range`, duration1, duration2, resistibility, hit_bonus, call_frequency, unknown9, given_by " +
                                                       "FROM spell_tiers " +
                                                       "WHERE spell_id=" + spell_id);
            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(5)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(6)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(7)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(8)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(9)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(10)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(11)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(12)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(13)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(14)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(15)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(16)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(17)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(18)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(19)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(20)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(21)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(22)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(23)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(24)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(25)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(26)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(27)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(28)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(29)));
                    listView_spelltiers.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_spelltiers_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_spelltiers.SelectedIndices.Count == 0 || listView_spelltiers.SelectedIndices[0] == -1) {
                ResetSpellTiers(false);
                return;
            }

            ListViewItem item = listView_spelltiers.Items[listView_spelltiers.SelectedIndices[0]];
            textBox_spelltiers_id.Text = item.Text;
            textBox_spelltiers_spellid.Text = item.SubItems[1].Text;
            comboBox_spelltiers_tier.SelectedItem = GetSpellTierName(Convert.ToInt32(item.SubItems[2].Text));
            textBox_spelltiers_hpreq.Text = item.SubItems[3].Text;
            textBox_spelltiers_hpreqpercent.Text = item.SubItems[4].Text;
            textBox_spelltiers_hpupkeep.Text = item.SubItems[5].Text;
            textBox_spelltiers_powerreq.Text = item.SubItems[6].Text;
            textBox_spelltiers_powerreqpercent.Text = item.SubItems[7].Text;
            textBox_spelltiers_powerupkeep.Text = item.SubItems[8].Text;
            textBox_spelltiers_savageryreq.Text = item.SubItems[9].Text;
            textBox_spelltiers_savageryreqpercent.Text = item.SubItems[10].Text;
            textBox_spelltiers_savageryupkeep.Text = item.SubItems[11].Text;
            textBox_spelltiers_dissonancereq.Text = item.SubItems[12].Text;
            textBox_spelltiers_dissonancereqpercent.Text = item.SubItems[13].Text;
            textBox_spelltiers_dissonanceupkeep.Text = item.SubItems[14].Text;
            textBox_spelltiers_reqconcentration.Text = item.SubItems[15].Text;
            textBox_spelltiers_casttime.Text = item.SubItems[16].Text;
            textBox_spelltiers_recovery.Text = item.SubItems[17].Text;
            textBox_spelltiers_recast.Text = item.SubItems[18].Text;
            textBox_spelltiers_radius.Text = item.SubItems[19].Text;
            textBox_spelltiers_maxaoetargets.Text = item.SubItems[20].Text;
            textBox_spelltiers_minrange.Text = item.SubItems[21].Text;
            textBox_spelltiers_range.Text = item.SubItems[22].Text;
            textBox_spelltiers_duration1.Text = item.SubItems[23].Text;
            textBox_spelltiers_duration2.Text = item.SubItems[24].Text;
            textBox_spelltiers_resistability.Text = item.SubItems[25].Text;
            textBox_spelltiers_hitbonus.Text = item.SubItems[26].Text;
            textBox_spelltiers_callfrequency.Text = item.SubItems[27].Text;
            textBox_spelltiers_unknown9.Text = item.SubItems[28].Text;


            button_spelltiers_insert.Enabled = true;
            button_spelltiers_update.Enabled = true;
            button_spelltiers_remove.Enabled = true;
        }

        private void button_spelltiers_insert_Click(object sender, EventArgs e) {
            string spell_id = GetSelectedSpellID().ToString();
            int tier = GetSpellTierID((string)comboBox_spelltiers_tier.SelectedItem);
            string hp_req = textBox_spelltiers_hpreq.Text;
            string hp_req_percent = textBox_spelltiers_hpreqpercent.Text;
            string hp_upkeep = textBox_spelltiers_hpupkeep.Text;
            string power_req = textBox_spelltiers_powerreq.Text;
            string power_req_percent = textBox_spelltiers_powerreqpercent.Text;
            string power_upkeep = textBox_spelltiers_powerupkeep.Text;
            string req_concentration = textBox_spelltiers_reqconcentration.Text;
            string cast_time = textBox_spelltiers_casttime.Text;
            string recovery = textBox_spelltiers_recovery.Text;
            string recast = textBox_spelltiers_recast.Text;
            string radius = textBox_spelltiers_radius.Text;
            string max_aoe_targets = textBox_spelltiers_maxaoetargets.Text;
            string min_range = textBox_spelltiers_minrange.Text;
            string range = textBox_spelltiers_range.Text;
            string duration1 = textBox_spelltiers_duration1.Text;
            string duration2 = textBox_spelltiers_duration2.Text;
            string resitibility = textBox_spelltiers_resistability.Text;
            string hit_bonus = textBox_spelltiers_hitbonus.Text;
            string call_frequency = textBox_spelltiers_callfrequency.Text;
            string unknown9 = textBox_spelltiers_unknown9.Text;

            int rows = db.RunQuery("INSERT INTO spell_tiers (spell_id, tier, hp_req, hp_req_percent, hp_upkeep, power_req, power_req_percent, power_upkeep, req_concentration, cast_time, recovery, recast, radius, max_aoe_targets, min_range, `range`, duration1, duration2, resistibility, hit_bonus, call_frequency, unknown9) " +
                                   "VALUES (" + spell_id + ", " + tier + ", " + hp_req + ", " + hp_req_percent + ", " + hp_upkeep + ", " + power_req + ", " + power_req_percent + ", " + power_upkeep + ", " + req_concentration + ", " + cast_time + ", " + recovery + ", " + recast + ", " + radius + ", " + max_aoe_targets + ", " + min_range + ", " + range + ", " + duration1 + ", " + duration2 + ", " + resitibility + ", " + hit_bonus + ", " + call_frequency + ", " + unknown9 + ")");
            if (rows > 0) {
                ResetSpellTiers(true);
                LoadSpellTiers(Convert.ToInt32(spell_id));
            }
        }

        private void button_spelltiers_update_Click(object sender, EventArgs e) {
            string id = textBox_spelltiers_id.Text;
            string spell_id = textBox_spelltiers_spellid.Text;
            int tier = GetSpellTierID((string)comboBox_spelltiers_tier.SelectedItem);
            string hp_req = textBox_spelltiers_hpreq.Text;
            string hp_req_percent = textBox_spelltiers_hpreqpercent.Text;
            string hp_upkeep = textBox_spelltiers_hpupkeep.Text;
            string power_req = textBox_spelltiers_powerreq.Text;
            string power_req_percent = textBox_spelltiers_powerreqpercent.Text;
            string power_upkeep = textBox_spelltiers_powerupkeep.Text;
            string req_concentration = textBox_spelltiers_reqconcentration.Text;
            string cast_time = textBox_spelltiers_casttime.Text;
            string recovery = textBox_spelltiers_recovery.Text;
            string recast = textBox_spelltiers_recast.Text;
            string radius = textBox_spelltiers_radius.Text;
            string max_aoe_targets = textBox_spelltiers_maxaoetargets.Text;
            string min_range = textBox_spelltiers_minrange.Text;
            string range = textBox_spelltiers_range.Text;
            string duration1 = textBox_spelltiers_duration1.Text;
            string duration2 = textBox_spelltiers_duration2.Text;
            string resitibility = textBox_spelltiers_resistability.Text;
            string hit_bonus = textBox_spelltiers_hitbonus.Text;
            string call_frequency = textBox_spelltiers_callfrequency.Text;
            string unknown9 = textBox_spelltiers_unknown9.Text;

            int rows = db.RunQuery("UPDATE spell_tiers " +
                                   "SET spell_id=" + spell_id + ", tier=" + tier + ", hp_req=" + hp_req + ", hp_req_percent=" + hp_req_percent + ", hp_upkeep=" + hp_upkeep + ", power_req=" + power_req + ", power_req_percent=" + power_req_percent + ", power_upkeep=" + power_upkeep + ", req_concentration=" + req_concentration + ", cast_time=" + cast_time + ", recovery=" + recovery + ", recast=" + recast + ", radius=" + radius + ", max_aoe_targets=" + max_aoe_targets + ", min_range=" + min_range + ", `range`=" + range + ", duration1=" + duration1 + ", duration2=" + duration2 + ", resistibility=" + resitibility + ", hit_bonus=" + hit_bonus + ", call_frequency=" + call_frequency + ", unknown9=" + unknown9 + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetSpellTiers(true);
                LoadSpellTiers(Convert.ToInt32(spell_id));
            }
        }

        private void button_spelltiers_remove_Click(object sender, EventArgs e) {
            string id = textBox_spelltiers_id.Text;

            int rows = db.RunQuery("DELETE FROM spell_tiers " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetSpellTiers(true);
                LoadSpellTiers(GetSelectedSpellID());
            }
        }

        private void ResetSpellTiers(bool include_listview) {
            if (include_listview)
                listView_spelltiers.Items.Clear();

            textBox_spelltiers_id.Clear();
            textBox_spelltiers_spellid.Clear();
            comboBox_spelltiers_tier.SelectedIndex = 0;
            textBox_spelltiers_hpreq.Clear();
            textBox_spelltiers_hpreqpercent.Clear();
            textBox_spelltiers_hpupkeep.Clear();
            textBox_spelltiers_powerreq.Clear();
            textBox_spelltiers_powerreqpercent.Clear();
            textBox_spelltiers_powerupkeep.Clear();
            textBox_spelltiers_reqconcentration.Clear();
            textBox_spelltiers_casttime.Clear();
            textBox_spelltiers_recovery.Clear();
            textBox_spelltiers_recast.Clear();
            textBox_spelltiers_radius.Clear();
            textBox_spelltiers_maxaoetargets.Clear();
            textBox_spelltiers_minrange.Clear();
            textBox_spelltiers_range.Clear();
            textBox_spelltiers_duration1.Clear();
            textBox_spelltiers_duration2.Clear();
            textBox_spelltiers_resistability.Clear();
            textBox_spelltiers_hitbonus.Clear();
            textBox_spelltiers_callfrequency.Clear();
            textBox_spelltiers_unknown9.Clear();

            button_spelltiers_update.Enabled = false;
            button_spelltiers_remove.Enabled = false;
        }

        /*************************************************************************************************************************
         *                                             SPELL CLASSES
         *************************************************************************************************************************/

        private void LoadSpellClasses(int spell_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, spell_id, adventure_class_id, tradeskill_class_id, level " +
                                                       "FROM spell_classes " +
                                                       "WHERE spell_id=" + spell_id);
            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    listView_spellclasses.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_spellclasses_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_spellclasses.SelectedIndices.Count == 0 || listView_spellclasses.SelectedIndices[0] == -1) {
                ResetSpellClasses(false);
                return;
            }

            ListViewItem item = listView_spellclasses.Items[listView_spellclasses.SelectedIndices[0]];
            textBox_spellclasses_id.Text = item.Text;
            textBox_spellclasses_spellid.Text = item.SubItems[1].Text;
            comboBox_spellclasses_classid.SelectedItem = GetClassName(Convert.ToInt32(item.SubItems[2].Text));
            textBox_spellclasses_tradeskillclassid.Text = item.SubItems[3].Text;
            textBox_spellclasses_level.Text = item.SubItems[4].Text;

            button_spellclasses_insert.Enabled = true;
            button_spellclasses_update.Enabled = true;
            button_spellclasses_remove.Enabled = true;
        }

        private void button_spellclasses_insert_Click(object sender, EventArgs e) {
            string spell_id = GetSelectedSpellID().ToString();
            int adventure_class_id = GetClassID((string)comboBox_spellclasses_classid.SelectedItem);
            string tradeskill_class_id = textBox_spellclasses_tradeskillclassid.Text;
            string level = textBox_spellclasses_level.Text;

            int rows = db.RunQuery("INSERT INTO spell_classes (spell_id, adventure_class_id, tradeskill_class_id, level) " +
                                   "VALUES (" + spell_id + ", " + adventure_class_id + ", " + tradeskill_class_id + ", " + level + ")");
            if (rows > 0) {
                ResetSpellClasses(true);
                LoadSpellClasses(Convert.ToInt32(spell_id));
            }
        }

        private void button_spellclasses_update_Click(object sender, EventArgs e) {
            string id = textBox_spellclasses_id.Text;
            string spell_id = textBox_spellclasses_spellid.Text;
            int adventure_class_id = GetClassID((string)comboBox_spellclasses_classid.SelectedItem);
            string tradeskill_class_id = textBox_spellclasses_tradeskillclassid.Text;
            string level = textBox_spellclasses_level.Text;

            int rows = db.RunQuery("UPDATE spell_classes " +
                                   "SET spell_id=" + spell_id + ", adventure_class_id=" + adventure_class_id + ", tradeskill_class_id=" + tradeskill_class_id + ", level=" + level + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetSpellClasses(true);
                LoadSpellClasses(Convert.ToInt32(spell_id));
            }
        }

        private void button_spellclasses_remove_Click(object sender, EventArgs e) {
            string id = textBox_spellclasses_id.Text;

            int rows = db.RunQuery("DELETE FROM spell_classes " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetSpellClasses(true);
                LoadSpellClasses(GetSelectedSpellID());
            }
        }

        private void ResetSpellClasses(bool include_listview) {
            if (include_listview)
                listView_spellclasses.Items.Clear();

            textBox_spellclasses_id.Clear();
            textBox_spellclasses_spellid.Clear();
            comboBox_spellclasses_classid.SelectedItem = "All";
            textBox_spellclasses_tradeskillclassid.Clear();
            textBox_spellclasses_level.Clear();

            button_spellclasses_update.Enabled = false;
            button_spellclasses_remove.Enabled = false;
        }

        /*************************************************************************************************************************
         *                                             SPELL DATA
         *************************************************************************************************************************/

        private void LoadSpellData(int spell_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, spell_id, tier, index_field, value_type, value, value2 " +
                                                       "FROM spell_data " +
                                                       "WHERE spell_id=" + spell_id);
            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(5)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(6)));
                    listView_spelldata.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_spelldata_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_spelldata.SelectedIndices.Count == 0 || listView_spelldata.SelectedIndices[0] == -1) {
                ResetSpellData(false);
                return;
            }

            ListViewItem item = listView_spelldata.Items[listView_spelldata.SelectedIndices[0]];
            textBox_spelldata_id.Text = item.Text;
            textBox_spelldata_spellid.Text = item.SubItems[1].Text;
            comboBox_spelldata_tier.SelectedItem = GetSpellTierName(Convert.ToInt32(item.SubItems[2].Text));
            textBox_spelldata_indexfield.Text = item.SubItems[3].Text;
            comboBox_spelldata_valuetype.SelectedItem = item.SubItems[4].Text;
            textBox_spelldata_value.Text = item.SubItems[5].Text;
            textBox_spelldata_value2.Text = item.SubItems[6].Text;

            button_spelldata_insert.Enabled = true;
            button_spelldata_update.Enabled = true;
            button_spelldata_remove.Enabled = true;
        }

        private void button_spelldata_insert_Click(object sender, EventArgs e) {
            string spell_id = GetSelectedSpellID().ToString();
            int tier = GetSpellTierID((string)comboBox_spelldata_tier.SelectedItem);
            string index_field = string.IsNullOrEmpty(textBox_spelldata_indexfield.Text) ? "0" : textBox_spelldata_indexfield.Text;
            string value_type =  string.IsNullOrEmpty((string)comboBox_spelldata_valuetype.SelectedItem) ? "3" : (string)comboBox_spelldata_valuetype.SelectedItem;
            string value = string.IsNullOrEmpty(textBox_spelldata_value.Text) ? "0" : textBox_spelldata_value.Text;
            string value2 =  string.IsNullOrEmpty(textBox_spelldata_value2.Text) ? "0" : textBox_spelldata_value2.Text;

            int rows = db.RunQuery("INSERT INTO spell_data (spell_id, tier, index_field, value_type, value, value2) " +
                                   "VALUES (" + spell_id + ", " + tier + ", " + index_field + ", '" + value_type + "', '" + value + "', '" + value2 + "')");
            if (rows > 0) {
                ResetSpellData(true);
                LoadSpellData(Convert.ToInt32(spell_id));
            }
        }

        private void button_spelldata_update_Click(object sender, EventArgs e) {
            string id = textBox_spelldata_id.Text;
            string spell_id = textBox_spelldata_spellid.Text;
            int tier = GetSpellTierID((string)comboBox_spelldata_tier.SelectedItem);
            string index_field = textBox_spelldata_indexfield.Text;
            string value_type = (string)comboBox_spelldata_valuetype.SelectedItem;
            string value = textBox_spelldata_value.Text;
            string value2 = textBox_spelldata_value2.Text;

            int rows = db.RunQuery("UPDATE spell_data " +
                                   "SET spell_id=" + spell_id + ", tier=" + tier + ", index_field=" + index_field + ", value_type='" + value_type + "', value='" + value + "', value2='" + value2 + "' " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetSpellData(true);
                LoadSpellData(Convert.ToInt32(spell_id));
            }
        }

        private void button_spelldata_remove_Click(object sender, EventArgs e) {
            string id = textBox_spelldata_id.Text;

            int rows = db.RunQuery("DELETE FROM spell_data " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetSpellData(true);
                LoadSpellData(GetSelectedSpellID());
            }
        }

        private void ResetSpellData(bool include_listview) {
            if (include_listview)
                listView_spelldata.Items.Clear();

            textBox_spelldata_id.Clear();
            textBox_spelldata_spellid.Clear();
            textBox_spelldata_indexfield.Clear();
            textBox_spelldata_value.Clear();
            textBox_spelldata_value2.Clear();
            comboBox_spelldata_tier.SelectedItem = "Apprentice I";
            comboBox_spelldata_valuetype.SelectedItem = "INT";
        }

        /*************************************************************************************************************************
         *                                             SPELL DISPLAY EFFECTS
         *************************************************************************************************************************/

        private void LoadSpellDisplayEffects(int spell_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT id, spell_id, tier, percentage, description, bullet, `index` " +
                                                       "FROM spell_display_effects " +
                                                       "WHERE spell_id=" + spell_id);
            if (reader != null) {
                while (reader.Read()) {
                    ListViewItem item = new ListViewItem(reader.GetString(0));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(1)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(2)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(3)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(4)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(5)));
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, reader.GetString(6)));
                    listView_spelldisplayeffects.Items.Add(item);
                }
                reader.Close();
            }
        }

        private void listView_spelldisplayeffects_SelectedIndexChanged(object sender, EventArgs e) {
            if (listView_spelldisplayeffects.SelectedIndices.Count == 0 || listView_spelldisplayeffects.SelectedIndices[0] == -1) {
                ResetSpellDisplayEffects(false);
                return;
            }

            ListViewItem item = listView_spelldisplayeffects.Items[listView_spelldisplayeffects.SelectedIndices[0]];
            textBox_spelldisplayeffects_id.Text = item.Text;
            textBox_spelldisplayeffects_spellid.Text = item.SubItems[1].Text;
            comboBox_spelldisplayeffects_tier.SelectedItem = GetSpellTierName(Convert.ToInt32(item.SubItems[2].Text));
            textBox_spelldisplayeffects_percentage.Text = item.SubItems[3].Text;
            textBox_spelldisplayeffects_description.Text = item.SubItems[4].Text;
            textBox_spelldisplayeffects_bullet.Text = item.SubItems[5].Text;
            textBox_spelldisplayeffects_index.Text = item.SubItems[6].Text;

            button_spelldisplayeffects_insert.Enabled = true;
            button_spelldisplayeffects_update.Enabled = true;
            button_spelldisplayeffects_remove.Enabled = true;
        }

        private void button_spelldisplayeffects_insert_Click(object sender, EventArgs e) {
            string spell_id = GetSelectedSpellID().ToString();
            int tier = GetSpellTierID((string)comboBox_spelldisplayeffects_tier.SelectedItem);
            string percentage = textBox_spelldisplayeffects_percentage.Text;
            string description = db.RemoveEscapeCharacters(textBox_spelldisplayeffects_description.Text);
            string bullet = textBox_spelldisplayeffects_bullet.Text;
            string index = textBox_spelldisplayeffects_index.Text;

            int rows = db.RunQuery("INSERT INTO spell_display_effects (spell_id, tier, percentage, description, bullet, `index`) " +
                                   "VALUES (" + spell_id + ", " + tier + ", " + percentage + ", '" + description + "', " + bullet + ", " + index + ")");
            if (rows > 0) {
                ResetSpellDisplayEffects(true);
                LoadSpellDisplayEffects(Convert.ToInt32(spell_id));
            }
        }

        private void button_spelldisplayeffects_update_Click(object sender, EventArgs e) {
            string id = textBox_spelldisplayeffects_id.Text;
            string spell_id = GetSelectedSpellID().ToString();
            int tier = GetSpellTierID((string)comboBox_spelldisplayeffects_tier.SelectedItem);
            string percentage = textBox_spelldisplayeffects_percentage.Text;
            string description = db.RemoveEscapeCharacters(textBox_spelldisplayeffects_description.Text);
            string bullet = textBox_spelldisplayeffects_bullet.Text;
            string index = textBox_spelldisplayeffects_index.Text;

            int rows = db.RunQuery("UPDATE spell_display_effects " +
                                   "SET spell_id=" + spell_id + ", tier=" + tier + ", percentage=" + percentage + ", description='" + description + "', bullet=" + bullet + ", `index`=" + index + " " +
                                   "WHERE id=" + id);
            if (rows > 0) {
                ResetSpellDisplayEffects(true);
                LoadSpellDisplayEffects(Convert.ToInt32(spell_id));
            }
        }

        private void button_spelldisplayeffects_remove_Click(object sender, EventArgs e) {
            string id = textBox_spelldisplayeffects_id.Text;

            int rows = db.RunQuery("DELETE FROM spell_display_effects " +
                                   "WHERE id=" + id);

            if (rows > 0) {
                ResetSpellDisplayEffects(true);
                LoadSpellDisplayEffects(GetSelectedSpellID());
            }
        }

        private void ResetSpellDisplayEffects(bool include_listview) {
            if (include_listview)
                listView_spelldisplayeffects.Items.Clear();

            textBox_spelldisplayeffects_id.Clear();
            textBox_spelldisplayeffects_spellid.Clear();
            comboBox_spelldisplayeffects_tier.SelectedItem = "Apprentice I";
            textBox_spelldisplayeffects_percentage.Clear();
            textBox_spelldisplayeffects_description.Clear();
            textBox_spelldisplayeffects_bullet.Clear();
            textBox_spelldisplayeffects_index.Clear();
        }

        /*************************************************************************************************************************
         *                                             SPELL SCRIPT
         *************************************************************************************************************************/

        private void LoadSpellScript(int spell_id) {
            MySqlDataReader reader = db.RunSelectQuery("SELECT lua_script " +
                                                       "FROM spells " +
                                                       "WHERE id=" + spell_id);
            if (reader != null) {
                if (reader.Read()) {
                    string spell_script = "";
                    try { spell_script = reader.GetString(0); } catch (Exception ex) { spell_script = "<none>"; }
                    label_spellscript_scriptname.Text = spell_script;
                    lua_interface.LoadLUAFile(ref textBox_spellscript_script, spell_script);
                }
                reader.Close();
            }
        }


        private void textBox_spellscript_script_TextChanged(object sender, EventArgs e) {
            //lua_interface.CheckLUASyntax(ref textBox_spellscript_script);
        }

        private void ResetSpellScript() {
            label_spellscript_scriptname.Text = "<none>";
            textBox_spellscript_script.Clear();
        }


        /*************************************************************************************************************************
         *                                             Other
         *************************************************************************************************************************/

        private void button_close_Click(object sender, EventArgs e) {
            owner.Dispose();
        }

        #region "Tool Tip"

        private void button_newspell_MouseHover(object sender, EventArgs e)
        {
            toolTip_spell.SetToolTip(button_newspell, "Insert");
        }

        private void button_close_MouseHover(object sender, EventArgs e)
        {
            toolTip_spell.SetToolTip(button_close, "Close");
        }

        #endregion

        private void button_Add_Script_Click(object sender, EventArgs e)
        {
            if (textBox_spell_luascript.Text == "")
            {
                textBox_spell_luascript.Text = "Spells/" + comboBox_select_class.SelectedItem.ToString() + "/" + textBox_spell_name.Text.Replace(" ","") + ".lua";
            }
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            comboBox_select_spell_SelectedIndexChanged(null, null);
        }

        private void Page_Spell_Load(object sender, EventArgs e)
        {
            LoadLastSettings();
        }

        private void LoadLastSettings()
        {
            comboBox_select_class.SelectedItem = Properties.Settings.Default.LastSpellClass;
            comboBox_select_spell.SelectedItem = Properties.Settings.Default.LastSpell;
        }

        private void tabPage_spell_Click(object sender, EventArgs e)
        {

        }
    }
}
