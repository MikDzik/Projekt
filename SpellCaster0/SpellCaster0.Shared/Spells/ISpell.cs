using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using System.ComponentModel;


namespace SpellCaster0.Spells
{
    public interface ISpell
    {
        UserControl SpecifiedControl { get; }
        int LineUp { get; }
        int Quantity { get; set; }
        string Name { get; }
        string Description { get; }
        string Message { get; }
        bool IsActive { get; }
        Wizard OnWho { get; set; }

        DateTime CastTime { get; set; }

        [DefaultValue(null)]
        TimeSpan DuringTime { get; }

    }
}
