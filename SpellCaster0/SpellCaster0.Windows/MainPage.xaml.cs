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
using Windows.Storage;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using Windows.ApplicationModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;


namespace SpellCaster0
{
    /// <summary>
    /// A start page.
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }



        private async void joinBtn(object sender, RoutedEventArgs e)
        {
            using (HttpClient http = new HttpClient())
            {
                try
                {
                    await http.GetStringAsync("http://mojaproba.c0.pl/otwarteGry.php");
                    String mojeGry = await http.GetStringAsync("http://mojaproba.c0.pl/otwarteGry.txt");
                    if (mojeGry.Contains(txtBox.Text))
                    {
                        Player.Game = txtBox.Text;
                        this.Frame.Navigate(typeof(ChoicePage));
                    }
                    else
                    {
                        display.Text = "Nie ma takiej gry!";
                    }

                }
                catch (Exception ex)
                {
                    display.Text = "Wystapił błąd";
                }
            }


        }

        private async void createBtn(object sender, RoutedEventArgs e)
        {
            HttpContent httpContent = new StringContent(txtBox.Text);

            using (HttpClient http = new HttpClient())
            {

                try
                {
                    await http.PostAsync("http://mojaproba.c0.pl/zapis.php", httpContent);
                    Player.Game = txtBox.Text;
                    Player.IsCreator = true;
                    this.Frame.Navigate(typeof(ChoicePage));
                }
                catch (Exception ex)
                {
                    txtBox.Text = ex.ToString();
                }

            }

        }

        private async void showBtn(object sender, RoutedEventArgs e)
        {
            using (HttpClient http = new HttpClient())
            {
                try
                {
                    await http.GetStringAsync("http://mojaproba.c0.pl/otwarteGry.php");
                    String mojeGry = await http.GetStringAsync("http://mojaproba.c0.pl/otwarteGry.txt");
                    mojeGry = mojeGry.Replace(".json", "");
                    display.Text = mojeGry.ToString();

                }
                catch (Exception ex)
                {
                    txtBox.Text = ex.ToString();
                }
            }
        }

    }
}
