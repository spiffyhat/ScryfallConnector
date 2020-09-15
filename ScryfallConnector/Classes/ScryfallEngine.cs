﻿using System;
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
            System.Threading.Thread.Sleep(100);
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
            System.Threading.Thread.Sleep(100);
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

        public List<ScryfallCard> FetchPrintsByUrl(string text)
        {
            System.Threading.Thread.Sleep(100);
            List<ScryfallCard> retval = new List<ScryfallCard>();
            HttpResponseMessage response;
            string responseContent = string.Empty;

            response = client.GetAsync(text).Result;
            response.EnsureSuccessStatusCode();
            responseContent = response.Content.ReadAsStringAsync().Result;

            PrintsSearchResult result = Newtonsoft.Json.JsonConvert.DeserializeObject<PrintsSearchResult>(responseContent);

            foreach (ScryfallCard card in result.data)
            {
                retval.Add(card);
            }
            return retval;
        }

        public ScryfallCard FetchCardByID(string text)
        {
            System.Threading.Thread.Sleep(100);
            ScryfallCard retval = new ScryfallCard();
            HttpResponseMessage response;
            string responseContent = string.Empty;

            response = client.GetAsync("cards/" + text).Result;
            response.EnsureSuccessStatusCode();
            responseContent = response.Content.ReadAsStringAsync().Result;

            retval = Newtonsoft.Json.JsonConvert.DeserializeObject<ScryfallCard>(responseContent);

            return retval;
        }

        private class AutoCompleteResult
        {
            public string _object { get; set; }
            public int total_values { get; set; }
            public string[] data { get; set; }
        }

        #region Set Search Result


        public class PrintsSearchResult
        {
            public string _object { get; set; }
            public int total_cards { get; set; }
            public bool has_more { get; set; }
            public ScryfallCard[] data { get; set; }
        }

        #endregion

        public ScryfallCard GetNamedCard(string text)
        {
            ScryfallCard card;
            try
            {
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
            System.Threading.Thread.Sleep(100);
            string responseContent = string.Empty;
            HttpResponseMessage response = client.GetAsync("cards/named?exact=" + text).Result;
            response.EnsureSuccessStatusCode();
            responseContent = response.Content.ReadAsStringAsync().Result;

            ScryfallCard card = Newtonsoft.Json.JsonConvert.DeserializeObject<ScryfallCard>(responseContent);

            return card;

        }

    }
}
