using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScryfallConnector.Classes
{
    class ScryfallEngine
    {

        private HttpClient client;

        public ScryfallEngine()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.BaseAddress = new Uri("https://api.scryfall.com/");
        }

        public ScryfallCard GetRandomCard()
        {
            ScryfallCard response;
            try
            {
                System.Threading.Thread.Sleep(100);
                response = FetchRandomCard();
            }
            catch (Exception)
            {

                throw;
            }

            return response;
        }

        private ScryfallCard FetchRandomCard()
        {
            string responseContent = string.Empty;
            HttpResponseMessage response = client.GetAsync("cards/random").Result;
            response.EnsureSuccessStatusCode();
            responseContent = response.Content.ReadAsStringAsync().Result;

            ScryfallCard card = Newtonsoft.Json.JsonConvert.DeserializeObject<ScryfallCard>(responseContent);

            return card;

        }

        public List<string> AutoComplete(string text, bool include_extras)
        {
            List<string> results = new List<string>();
            try
            {
                results = FetchAutoCompleteResults(text, include_extras);
            }
            catch (Exception)
            {
                results = null;
            }
            return results;
        }

        private List<string> FetchAutoCompleteResults(string text, bool include_extras)
        {
            List<string> retval = new List<string>();
            HttpResponseMessage response;
            string responseContent = string.Empty;

            response = client.GetAsync("cards/autocomplete?q=" + text + "&include_extras=" + include_extras).Result;
            response.EnsureSuccessStatusCode();
            responseContent = response.Content.ReadAsStringAsync().Result;

            AutoCompleteResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<AutoCompleteResult>(responseContent);

            foreach (string card in result.data)
            {
                retval.Add(card);
            }
            return retval;

        }


        private class AutoCompleteResult
        {
            public string _object { get; set; }
            public int total_values { get; set; }
            public string[] data { get; set; }
        }

        public ScryfallCard GetNamedCard(string text)
        {
            ScryfallCard card;
            try
            {
                System.Threading.Thread.Sleep(100);
                card = FetchNamedCard(text);
            }
            catch (Exception)
            {
                card = null;
            }
            return card;
        }

        private ScryfallCard FetchNamedCard(string text)
        {
            string responseContent = string.Empty;
            HttpResponseMessage response = client.GetAsync("cards/named?exact=" + text).Result;
            response.EnsureSuccessStatusCode();
            responseContent = response.Content.ReadAsStringAsync().Result;

            ScryfallCard card = Newtonsoft.Json.JsonConvert.DeserializeObject<ScryfallCard>(responseContent);

            return card;

        }

    }
}
