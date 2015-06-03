using SpellCaster0.Spells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace SpellCaster0
{
    public sealed partial class SpellControl : UserControl
    {
        public SpellControl(ISpell spell, List<Wizard> lw)
        {
            this.spell = spell;
            SpellQuantity = spell.Quantity;
            SpellDescription = spell.Description;
            SpellName = spell.Name;
            SpellIndex = spell.LineUp;

            this.InitializeComponent();
            Pnl.DataContext = this;
            temp = lw;
        }

        public event SpellCaster0.GamePage.NewEventHandler StatusUpdated;
        private ISpell spell;
        private List<Wizard> temp;
        private int SpellIndex { get; set; }


        public List<Wizard> toBind
        {
            get { return (List<Wizard>)GetValue(toBindProperty); }
            set { SetValue(toBindProperty, value); }
        }

        public static readonly DependencyProperty toBindProperty =
            DependencyProperty.Register("toBind", typeof(List<Wizard>), typeof(SpellControl), new PropertyMetadata(WizardServices.httpRead()));




        public int SpellQuantity
        {
            get { return (int)GetValue(SpellQuantityProperty); }
            set { SetValue(SpellQuantityProperty, value); }
        }

        public static readonly DependencyProperty SpellQuantityProperty =
            DependencyProperty.Register("SpellQuantity", typeof(int), typeof(SpellControl), new PropertyMetadata(""));




        public string SpellDescription
        {
            get { return (string)GetValue(SpellDescriptionProperty); }
            set { SetValue(SpellDescriptionProperty, value); }
        }

        public static readonly DependencyProperty SpellDescriptionProperty =
            DependencyProperty.Register("SpellDescription", typeof(string), typeof(SpellControl), new PropertyMetadata(""));




        public string SpellName
        {
            get { return (string)GetValue(SpellNameProperty); }
            set { SetValue(SpellNameProperty, value); }
        }

        public static readonly DependencyProperty SpellNameProperty =
            DependencyProperty.Register("SpellName", typeof(string), typeof(SpellControl), new PropertyMetadata(""));

        private async void showPlayers_Click(object sender, RoutedEventArgs e)
        {
            if ((string)showPlayers.Content != "Hide list")
            {
                toBind = await WizardServices.httpRead();

                listView.Items.Clear();


                foreach (var a in toBind)
                {
                    listView.Items.Add(a.Name);
                }

                showPlayers.Content = "Hide list";
                Grid.SetRowSpan(board, 4);

                Grid.SetColumn(listView, 1);
                Grid.SetRow(listView, 3);
                Pnl.RowDefinitions[3].Height = new GridLength(1, GridUnitType.Auto);
            }
            else
            {
                showPlayers.Content = "Cast Spell";
                Pnl.RowDefinitions[3].Height = new GridLength(0);
            }
        }

        private async void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListView lsl = sender as ListView;


            spell.OnWho = temp.Find(w => w.Name == e.ClickedItem.ToString());
            await WizardServices.httpPut(temp);
            if (this.StatusUpdated != null)
                this.StatusUpdated(spell); //add to delegate
        }

    }
}
