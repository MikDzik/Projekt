using SpellCaster0.Spells;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // w razie
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


namespace SpellCaster0
{
    /// <summary>
    /// Main Page of the game.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        public List<Wizard> listWiz = null;
        Wizard ownWiz = null;
        List<ISpell> spellList = null;
        public DateTime lastCheckTime { get; set; }
        public delegate void MyEventHandler(object d, EventArgs q, ISpell a);
        public delegate void NewEventHandler(ISpell a);

        public ObservableCollection<ISpell> MyActionList
        {
            get { return (ObservableCollection<ISpell>)GetValue(MyActionListProperty); }
            set { SetValue(MyActionListProperty, value); }
        }

        public static readonly DependencyProperty MyActionListProperty =
            DependencyProperty.Register("MyActionList", typeof(ObservableCollection<Spell>), typeof(GamePage), new PropertyMetadata(""));



        private ObservableCollection<ISpell> _actionList = new ObservableCollection<ISpell>();

        public ObservableCollection<ISpell> ActionList
        {
            get
            {
                for (int i = 0; i < ownWiz.CastedTime.Count; i++)
                {
                    if (ownWiz.CastedTime[i] > lastCheckTime)
                    {
                        _actionList.Insert(0, Spell.SpellFromCast(i, ownWiz.CastedTime[i]));
                    }
                }
                lastCheckTime = DateTime.Now;

                return _actionList;
            }
        }

        public bool IsTransfixed
        {
            get
            {
                if (ownWiz != null)
                {
                    Transfix trf = new Transfix(ownWiz.CastedTime[Transfix.LineUp]);


                    if (trf.IsActive)
                    {
                        return true;
                    }
                    else return false;

                }
                else return false;

            }
        }
      
        public GamePage()
        {
            this.InitializeComponent();
            mainGrid.DataContext = this;
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            listWiz = await WizardServices.httpRead();
            int index = listWiz.FindIndex(t => t.Name == Player.Name);
            ownWiz = listWiz[index];

            spellList = new List<ISpell>();
            spellList.Add(new Transfix(ownWiz.SpellList[0]));
            spellList.Add(new Compel(ownWiz.SpellList[2]));
            spellList.Add(new BookRecognition(ownWiz.SpellList[3]));
            spellList.Add(new SpellTheft(ownWiz.SpellList[SpellTheft.LineUp]));
            stcPnl.Children.Clear();

            foreach (var s in spellList)
            {
                SpellControl nowa = new SpellControl(s, listWiz);
                nowa.StatusUpdated += new NewEventHandler(SpellControl_StatusUpdated);

                stcPnl.Children.Add(nowa);

            }

            mainGrid_Tapped(sender, e as TappedRoutedEventArgs);

            MyActionList = ActionList;
        }

        private async void SpellControl_StatusUpdated(ISpell spell)
        {
            if (ownWiz.SpellList[spell.LineUp] > 0)
            {
               
                ownWiz.SpellList[spell.LineUp]--;
                var tempList = await WizardServices.httpRead();
                tempList[tempList.FindIndex((w => w.Name == ownWiz.Name))] = ownWiz;
                await WizardServices.httpPut(tempList);

                tempList = await WizardServices.httpRead();
                var tempWiz = tempList[tempList.FindIndex(w => w.Name == spell.OnWho.Name)];
                tempWiz.CastedTime[spell.LineUp] = DateTime.Now;
                DateTime now = DateTime.Now;
                spell.CastTime = now;

                tempList[tempList.FindIndex((w => w.Name == tempWiz.Name))] = tempWiz;

                await WizardServices.httpPut(tempList);

                var toShow = new TemplateSpellControl(spell);
                Refresh_Click(new object(), new RoutedEventArgs());
                toShow.hostPopup.IsOpen = true;
                toShow.hostPopup.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                toShow.hostPopup.VerticalOffset = 30;
                toShow.hostPopup.HorizontalOffset = 230;
                mainGrid.Children.Add(toShow.hostPopup);
            }
        }


        void mainGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (IsTransfixed)
            {
                FlyoutBase.ShowAttachedFlyout((FrameworkElement)mainGrid);
                mainGrid.IsTapEnabled = true;
                scroll.IsEnabled = false;
            }

            else
            {
                mainGrid.IsTapEnabled = false;
                scroll.IsEnabled = true;
            }
        }



    }
}
