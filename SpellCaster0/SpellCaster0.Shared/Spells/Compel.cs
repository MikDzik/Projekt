using SpellCaster0.SpellControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellCaster0.Spells
{
    public class Compel : ISpell
    {
        public Compel(int a)
        {
            Quantity = a;
        }
        public Compel(DateTime a)
        {
            CastTime = a;
        }


        public static int LineUp
        {
            get { return 3; }
        }

        public int Quantity { get; set; }


        public string Name
        {
            get { return "Compel"; }
        }

        public string Description
        {
            get { return "You can cast an other player spell"; }
        }

        public string Message
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime CastTime { get; set; }

        public TimeSpan DuringTime
        {
            get { return new TimeSpan(); }
        }

        public Windows.UI.Xaml.Controls.UserControl SpecifiedControl
        {
            get { return new CompelControl(this); }
        }

        public Wizard OnWho { get; set; }


        int ISpell.LineUp
        {
            get { return 2; }
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
