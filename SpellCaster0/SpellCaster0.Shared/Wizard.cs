using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCaster0
{
    public class Wizard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int [] SpellList { get; set; }

        [DefaultValue(null)]
        public List<DateTime> CastedTime { get; set; }
    }
}
