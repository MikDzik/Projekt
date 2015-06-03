using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using Windows.Storage;
using System.Net;


namespace SpellCaster0
{
    public static class WizardServices
    {
        public static async Task<List<Wizard>> httpRead()
        {
            var client = new HttpClient();
            List<Wizard> lstWiz = new List<Wizard>();


            try
            {
                string result = await client.GetStringAsync("http://mojaproba.c0.pl/" + Player.Game + ".json");

                lstWiz = (JsonConvert.DeserializeObject<List<Wizard>>(result));
                return lstWiz;

            }

            catch (Exception ex)
            {
                Wizard exc = new Wizard() { Id = 1234, Name = "Bład pobrania danych", Status = true };
                lstWiz.Add(exc);
                return lstWiz;
            }
        }

        public static async Task<String> httpPut(List<Wizard> list)
        {

            var client = new HttpClient();
            try
            {
                await client.PostAsJsonAsync<List<Wizard>>("http://mojaproba.c0.pl/" + Player.Game + ".php", list);
                return "Lista zostala pomyslnie wczytana.";
            }
            catch (Exception ex)
            {
                return "Lista nie została wczytana.";
            }
        }
    }


}
