using SpellCaster0.Spells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SpellCaster0.SpellControls
{
    public sealed partial class SpellTheftControl : UserControl
    {
        public SpellTheftControl(ISpell spellTheft)
        {
            this.InitializeComponent();
            bookGrid.DataContext = this;
            ObservableCollection<ISpell> SpellList = new ObservableCollection<ISpell>();
            SpellList.Add(new Transfix(spellTheft.OnWho.SpellList[Transfix.LineUp]));
            SpellList.Add(new Dispel(spellTheft.OnWho.SpellList[Dispel.LineUp]));
            SpellList.Add(new Compel(spellTheft.OnWho.SpellList[Compel.LineUp]));
            SpellList.Add(new BookRecognition(spellTheft.OnWho.SpellList[BookRecognition.LineUp]));
            SpellList.Add(new SpellTheft(spellTheft.OnWho.SpellList[SpellTheft.LineUp]));
            MySpellList = SpellList as ObservableCollection<ISpell>;
            this.spell = spellTheft;
        }

        ISpell spell;

        public ObservableCollection<ISpell> MySpellList
        {
            get { return (ObservableCollection<ISpell>)GetValue(MySpellListProperty); }
            set { SetValue(MySpellListProperty, value); }
        }

        public static readonly DependencyProperty MySpellListProperty =
            DependencyProperty.Register("MySpellList", typeof(ObservableCollection<ISpell>), typeof(SpellTheftControl), new PropertyMetadata(""));


        public async void compelView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var txtbox = new TextBlock();
            ISpell clicked = e.ClickedItem as ISpell;
            var tempWiz = spell.OnWho;
            if (tempWiz.SpellList[clicked.LineUp] != 0)
            {
                tempWiz.SpellList[clicked.LineUp]--;
                List<Wizard> lstWiz = new List<Wizard>();
                lstWiz = await WizardServices.httpRead();

                lstWiz[lstWiz.FindIndex((w => w.Name == tempWiz.Name))] = tempWiz;
                lstWiz[lstWiz.FindIndex((w => w.Name == Player.Name))].SpellList[clicked.LineUp]++;

                await WizardServices.httpPut(lstWiz);

                txtbox.Text = "You stole a spell\n" + clicked.Name + "\nfrom player\n" + tempWiz.Name;
            }

            else
            {
                txtbox.Text = "Player " + tempWiz.Name + "\ndoesn't have spell " + clicked.Name + ".\n You failed in use your spell.";
            }

            bookGrid.Children.Clear();
            bookGrid.Children.Add(txtbox);

        }


    }
}
