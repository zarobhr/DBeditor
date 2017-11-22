using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TabControl_Spawns
{
    public class ControlEffectType
    {
    public int id;
        public string control_effect_type;

        public ControlEffectType(int id, string control_effect_type)
        {
            this.id = id;
            this.control_effect_type = control_effect_type;
        }
    }
}