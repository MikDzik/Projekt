using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

    public sealed partial class ChoicePage : Page
    {
        List<Wizard> wizList = null;
        public ChoicePage()
        {
            this.InitializeComponent();
        }

        private void btnAdd()
        {
            stkPnl.Children.Clear();
            foreach (var e in wizList)
            {
                Button btn = new Button();
                btn.Content = e.Name;
                btn.Click += btn_Click;
                stkPnl.Children.Add(btn);
            }
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            Button tempBtn = sender as Button;
            Player.Name = tempBtn.Content.ToString();
            this.Frame.Navigate(typeof(GamePage));
        }

  

        private async void actBtn_Click(object sender, RoutedEventArgs e)
        {
            wizList = await WizardServices.httpRead();
            btnAdd();
        }

    }
}
