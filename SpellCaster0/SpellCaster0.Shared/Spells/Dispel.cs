using System;
using System.Collections.Generic;
using System.Text;

namespace SpellCaster0.Spells
{
    public class Dispel : ISpell
    {
        public Dispel(int a) 
        {
            Quantity = a;
        }


        public Dispel(DateTime a)
        {
            CastTime = a;

        }


        public static int LineUp
        {
            get { return 1; }
        }

        public int Quantity { get; set; }

        public string Name
        {
            get { return "Dispel"; }
        }

        public string Description
        {
            get { return "Lets you avoid consequences of an enchantment"; }
        }

        public string Message
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime CastTime { get; set; }

        public TimeSpan DuringTime
        {
            get { return new TimeSpan(0, 0, 0); }
        }

        public Windows.UI.Xaml.Controls.UserControl SpecifiedControl
        {
            get { throw new NotImplementedException(); }
        }


        public Wizard OnWho { get; set; }




        int ISpell.LineUp
        {
            get { return 1; }
        }


        public bool IsActive
        {
            get
            {
                if (DateTime.Now.Subtract(CastTime) < DuringTime)
                {
                    return true;
                }
                else return false;
            }
        }
    }
}
