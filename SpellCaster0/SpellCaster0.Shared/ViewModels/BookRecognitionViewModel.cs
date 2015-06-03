using SpellCaster0.Spells;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpellCaster0.ViewModels
{
    class BookRecognitionViewModel : MyNotify
    {

        public BookRecognitionViewModel(ISpell spell)
        {
            this.spell = spell;
        }

        private ISpell spell;

        public List<ISpell> SpellList
        {
            get
            {
                RaisePropertyChanged();

                var ownWiz = spell.OnWho;

                var spellList = new List<ISpell>();
                spellList.Add(new Transfix(ownWiz.SpellList[0]));
                spellList.Add(new Dispel(ownWiz.SpellList[1]));
                spellList.Add(new Compel(ownWiz.SpellList[2]));
                spellList.Add(new BookRecognition(ownWiz.SpellList[3]));
                spellList.Add(new SpellTheft(ownWiz.SpellList[SpellTheft.LineUp]));
                return spellList;
            }
        }
    }
}
