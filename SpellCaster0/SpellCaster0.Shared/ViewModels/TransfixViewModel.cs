using SpellCaster0.Spells;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellCaster0.ViewModels
{
    class TransfixViewModel : MyNotify
    {
        public TransfixViewModel(ISpell spell)
        {
            MyText = "You cast transfix on " + spell.OnWho.Name;
        }



        private string _myText;

        public string MyText
        {
            get { return _myText; }
            set
            {
                _myText = value;
                RaisePropertyChanged();
            }
        }
        


    }
}
