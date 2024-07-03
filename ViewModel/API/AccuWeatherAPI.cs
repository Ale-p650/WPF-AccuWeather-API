using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.ViewModel.API
{
    public class AccuWeatherAPI
    {
        public const string URL_BASE = "http://dataservice.accuweather.com";

        public const string endpointCiudad = "/locations/v1/cities/autocomplete?apikey={0}&q={1}";

        public const string endpointClima = "/currentconditions/v1/{0}?apikey={1}";

        public const string apiKey = "ymZTcPSGy65nzmaBwtjeNPjknbvTf9G8";


        public static async Task< List<Ciudad>> GetCiudad(string query)
        {
            List<Ciudad> Ciudades = new List<Ciudad>();

            using(HttpClient client = new HttpClient())
            {
                string url = URL_BASE + String.Format(endpointCiudad, apiKey, query);

                var resp = await client.GetAsync(url);

                var cont = await resp.Content.ReadAsStringAsync();

                Ciudades = JsonConvert.DeserializeObject<List<Ciudad>>(cont);
                
            }
                return Ciudades;
        }

        public static async Task<Clima> GetCLima(string key)
        {
            Clima clima;

            using (HttpClient client = new HttpClient())
            {
                string url = URL_BASE + String.Format(endpointClima,key, apiKey);

                var resp = await client.GetAsync(url);

                var cont = await resp.Content.ReadAsStringAsync();

                clima = (JsonConvert.DeserializeObject<List<Clima>>(cont)).FirstOrDefault();

            }
            return clima;
        }
    }
}
