using SpellCaster0.SpellControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellCaster0.Spells
{
    public class SpellTheft :  ISpell
    {


        public SpellTheft(DateTime a)
        {
            CastTime = a;
        }

        public SpellTheft(int a)
        {
            Quantity = a;
        }
        public static int LineUp
        {
            get
            {
                return 4;
            }
        }

        public Windows.UI.Xaml.Controls.UserControl SpecifiedControl
        {
            get { return new SpellTheftControl(this); }
        }

         int ISpell.LineUp
        {
            get { return 4; }
        }

        public int Quantity { get; set;}

        public  string Name
        {
            get { return "Spell Theft"; }
        }

        public string Description
        {
            get { return "Wizard steals one of the spells from other person’s spell book and adds it to his own one."; }
        }

        public string Message
        {
            get { throw new NotImplementedException(); }
        }

        public Wizard OnWho { get; set; }

        public DateTime CastTime { get; set;}

        public TimeSpan DuringTime 
        {
            get { throw new NotImplementedException(); }
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
