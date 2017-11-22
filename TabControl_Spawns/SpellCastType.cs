using System;
using System.Collections.Generic;
using System.Text;

namespace TabControl_Spawns {
    public class SpellCastType {
        public int id;
        public string cast_type;

        public SpellCastType(int id, string cast_type) {
            this.id = id;
            this.cast_type = cast_type;
        }
    }
}
