using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using SpellCaster0.SpellControls;

namespace SpellCaster0.Spells
{
    public class Transfix : Spell, ISpell
    {

        public Transfix(int a)
        {
            Quantity = a;
        }
        public Transfix(DateTime a)
        {
            CastTime = a;
        }

        public static int LineUp
        {
            get
            {
                return 0;
            }
        }
        public int Quantity { get; set; }


        public string Name
        {
            get { return "Transfix"; }
        }

        public string Description
        {
            get { return "Target is immobilised for 5 min"; }
        }

        public string Message
        {
            get { return "Actually, you are transfixed!"; }
        }

        public DateTime CastTime { get; set; }

        public TimeSpan DuringTime
        {
            get { return new TimeSpan(0, 1, 0); }
        }

        Windows.UI.Xaml.Controls.UserControl ISpell.SpecifiedControl
        {
            get { return new TransfixControl(this); }
        }


        public Wizard OnWho { get; set; }


        int ISpell.LineUp
        {
            get { return 0; }
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
