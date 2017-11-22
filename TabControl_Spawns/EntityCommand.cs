using System;
using System.Collections.Generic;
using System.Text;

namespace TabControl_Spawns {
    public class EntityCommand {
        public int id;
        public string entity_command;

        public EntityCommand(int id, string entity_command) {
            this.id = id;
            this.entity_command = entity_command;
        }
    }
}
