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
    public sealed partial class CompelControl : UserControl
    {
        public CompelControl(ISpell compel)
        {
            this.InitializeComponent();
            bookGrid.DataContext = this;

            ObservableCollection<ISpell> SpellList = new ObservableCollection<ISpell>();
            SpellList.Add(new Transfix(compel.OnWho.SpellList[Transfix.LineUp]));
            SpellList.Add(new Dispel(compel.OnWho.SpellList[Dispel.LineUp]));
            SpellList.Add(new Compel(compel.OnWho.SpellList[Compel.LineUp]));
            SpellList.Add(new BookRecognition(compel.OnWho.SpellList[BookRecognition.LineUp]));
            SpellList.Add(new SpellTheft(compel.OnWho.SpellList[SpellTheft.LineUp]));

            MySpellList = SpellList as ObservableCollection<ISpell>;
            this.spell = compel;
        }

        ISpell spell; // this spell
        ISpell clicked; //spell chosen to be cast to another player



        public List<Wizard> MyWizardList
        {
            get { return (List<Wizard>)GetValue(MyWizardListProperty); }
            set { SetValue(MyWizardListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyWizardList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyWizardListProperty =
            DependencyProperty.Register("MyWizardList", typeof(List<Wizard>), typeof(CompelControl), new PropertyMetadata(0));

        
        
        public ObservableCollection<ISpell> MySpellList
        {
            get { return (ObservableCollection<ISpell>)GetValue(MySpellListProperty); }
            set { SetValue(MySpellListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MySpellList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MySpellListProperty =
            DependencyProperty.Register("MySpellList", typeof(ObservableCollection<ISpell>), typeof(CompelControl), new PropertyMetadata(""));


        public async void compelView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var txtbox = new TextBlock();
            clicked = e.ClickedItem as ISpell;
            var tempWiz = spell.OnWho;
            if (tempWiz.SpellList[clicked.LineUp] != 0)
            {
                tempWiz.SpellList[clicked.LineUp]--;
                List<Wizard> lstWiz = new List<Wizard>();
                lstWiz = await WizardServices.httpRead();

                lstWiz[lstWiz.FindIndex((w => w.Name == tempWiz.Name))] = tempWiz;

                MyWizardList = lstWiz;

                ListView newView = new ListView { ItemTemplate = (DataTemplate)Resources["ListViewDataTemplate"] };

                foreach (var  w in MyWizardList)
                {
                     newView.Items.Add(w);
                }
                newView.IsItemClickEnabled = true;
                newView.ItemClick += newView_ItemClick;
           
                bookGrid.Children.Clear();
                bookGrid.Children.Add(newView);

                txtbox.Text = "You stole a spell\n" + clicked.Name + "\nfrom player\n" + tempWiz.Name;
            }

            else
            {
                txtbox.Text = "Player "+ tempWiz.Name + "\ndoesn't have spell " + clicked.Name + ".\n You failed in use your spell.";
            }

           
        }

        async void newView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Wizard clickedWiz = e.ClickedItem as Wizard;
            clickedWiz.CastedTime[clicked.LineUp] = DateTime.Now;
            MyWizardList[MyWizardList.FindIndex((w => w.Name == clickedWiz.Name))] = clickedWiz;
            await WizardServices.httpPut(MyWizardList);
        }

      
    }
}
