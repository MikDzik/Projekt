using SpellCaster0.SpellControls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellCaster0.Spells
{
    public class BookRecognition : ISpell
    {
        public BookRecognition(int a)
        {
            Quantity = a;

        }

        public BookRecognition(DateTime a)
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
            get { return "BookRecognition"; }
        }

        public string Description
        {
            get { return "See, what spells choosen player can cast"; }
        }

        public string Message
        {
            get { throw new NotImplementedException(); }
        }

        public DateTime CastTime { get; set; }

        public TimeSpan DuringTime
        {
            get { throw new NotImplementedException(); }
        }

        Windows.UI.Xaml.Controls.UserControl ISpell.SpecifiedControl
        {
            get { return new BookRecognitionControl(this); }
        }

        public Wizard OnWho { get; set; }



        int ISpell.LineUp
        {
            get { return 3; }
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
