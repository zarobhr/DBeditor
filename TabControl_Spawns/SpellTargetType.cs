using System;
using System.Collections.Generic;
using System.Text;

namespace TabControl_Spawns {
    public class SpellTargetType {
        public int id;
        public string target_type;

        public SpellTargetType(int id, string target_type) {
            this.id = id;
            this.target_type = target_type;
        }
    }
}
