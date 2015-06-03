using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SpellCaster0.Spells;


namespace SpellCaster0
{
    public abstract class Spell
    {


        public static ISpell SpellFromCast(int whichLineUp, DateTime dt)
        {
            switch (whichLineUp)
            {
                case 0:
                    return new Transfix(dt);
                case 1:
                    return new Dispel(dt);
                case 2:
                    return new Compel(dt);
                case 3:
                    return new BookRecognition(dt);
                case 4:
                    return new SpellTheft(dt);
                default:
                    return null;
            }
        }


        public int Quantity { get; set; }

        public void QuantityIncrement()
        {
            Quantity++;
        }

        public void QuantityDecrement()
        {
            Quantity--;
        }

    }
}
