using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SpellCaster0
{
    public class Player
    {
        public static string Game { get; set; }
        public static string Name { get; set; }

        [DefaultValue(false)]
        public static bool IsCreator { get; set; }
    }
}
