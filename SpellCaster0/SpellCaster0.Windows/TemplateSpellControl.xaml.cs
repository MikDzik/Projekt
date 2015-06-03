using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SpellCaster0.Spells
{
    public sealed partial class TemplateSpellControl : UserControl
    {
        public Popup hostPopup;
        private ISpell spell;
        public TemplateSpellControl(ISpell spell)
        {
            hostPopup = new Popup();
            hostPopup.Child = this;
            BindText = spell.Name;


            this.InitializeComponent();
            controlGrid.DataContext = this;

            this.spell = spell;

            UserControl gridChild = spell.SpecifiedControl;

            Grid.SetRow(gridChild, 1);
            controlGrid.Children.Add(gridChild);
        }




        public string BindText
        {
            get { return (string)GetValue(BindTextProperty); }
            set { SetValue(BindTextProperty, value); }
        }

        public static readonly DependencyProperty BindTextProperty =
            DependencyProperty.Register("BindText", typeof(string), typeof(TemplateSpellControl), new PropertyMetadata(""));




        public void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            hostPopup.IsOpen = false;
        }
    }
}
