using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TabControl_Spawns
{
    public class SpellGivenBy
    {
        public int id;
        public string given_by;

        public SpellGivenBy(int id, string given_by)
        {
            this.id = id;
            this.given_by = given_by;
        }
    }
}
