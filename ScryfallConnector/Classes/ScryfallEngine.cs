using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data.SqlServerCe;
using System.Data;
using System.IO;
using System.Drawing.Imaging;

namespace ScryfallConnector.Classes
{
    class ScryfallEngine
    {

        private HttpClient client;
        SqliteDB db = null;

        public ScryfallEngine(SqliteDB db)
        {
            this.db = db;
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.BaseAddress = new Uri("https://api.scryfall.com/");
        }

        public Image GetCardImage(ScryfallCard card)
        {
            return FetchCardImage(card);
        }

        private Image FetchCardImage(ScryfallCard card)
        {
            Image retval = null;
            string cardID = card.id;
            string imageURL;
            SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM IMAGES_NORMAL WHERE Card_ID = \'" + cardID + "\'", db.connection);
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            DataTable result = new DataTable();
            da.Fill(result);
            if (result.Rows.Count != 0)
            {
                imageURL = result.Rows[0]["Local_Filepath"].ToString();
                retval = Bitmap.FromFile(".\\images\\" + imageURL);
            } else
            {
                if (card.image_uris.normal != null)
                {
                    imageURL = card.image_uris.normal;

                    System.Net.WebRequest request = System.Net.WebRequest.Create(imageURL);
                    System.Net.WebResponse response = request.GetResponse();
                    System.IO.Stream responseStream =
                        response.GetResponseStream();
                    retval = new Bitmap(responseStream);

                    //string filePath = (".\\images\\" + cardID + ".jpg");
                    string filePath = cardID + ".jpg";
                    retval.Save(".\\images\\" + filePath);
                    cmd = new SqlCeCommand("INSERT INTO Images_Normal (Card_ID, Image_URL, Local_Filepath)" +
                                            "VALUES (@Card_ID, @Image_URL, @Local_Filepath)", db.connection);
                    cmd.Parameters.AddWithValue("@Card_ID", cardID);
                    cmd.Parameters.AddWithValue("@Image_URL", imageURL);
                    cmd.Parameters.AddWithValue("@Local_Filepath", filePath);
                    cmd.ExecuteNonQuery();
                }
                
            }

            return retval;
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

        public List<Print> GetPrintsByUrl(ScryfallCard card)
        {
            return FetchPrintsByUrl(card);
        }

        public List<Print> FetchPrintsByUrl(ScryfallCard card)
        {
            System.Threading.Thread.Sleep(100);

            List<string> retval = new List<string>();
            List<ScryfallCard> cards = new List<ScryfallCard>();
            PrintsList printsList = new PrintsList();
            HttpResponseMessage response;
            string dbContent = string.Empty;
            string responseContent = string.Empty;

            SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM PRINTSLIST WHERE Card_Name = \'" + card.Name + "\'", db.connection);
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            DataTable result = new DataTable();
            da.Fill(result);

            if (result.Rows.Count != 0)
            {

                dbContent = result.Rows[0]["Prints"].ToString();
                printsList.prints = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Print>>(dbContent);

            } else
            {
                printsList.card_name = card.Name;
                printsList.prints = new List<Print>();

                response = client.GetAsync(card.prints_search_uri).Result;
                response.EnsureSuccessStatusCode();
                responseContent = response.Content.ReadAsStringAsync().Result;

                PrintsSearchResult apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<PrintsSearchResult>(responseContent);

                foreach (ScryfallCard datacard in apiResult.data)
                {
                    Print p = new Print();
                    p.card_ID = datacard.id;
                    p.set_name = datacard.set_name;
                    p.set = datacard.set;
                    printsList.prints.Add(p);
                }

                string serial = Newtonsoft.Json.JsonConvert.SerializeObject(printsList.prints);

                cmd = new SqlCeCommand("INSERT INTO PrintsList (Card_Name, Prints)" +
                                           "VALUES (@Card_Name, @Prints)", db.connection);
                cmd.Parameters.AddWithValue("@Card_Name", card.Name);
                cmd.Parameters.AddWithValue("@Prints", serial);
                cmd.ExecuteNonQuery();
            }

            return printsList.prints;
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


        private class PrintsSearchResult
        {
            public string _object { get; set; }
            public int total_cards { get; set; }
            public bool has_more { get; set; }
            public ScryfallCard[] data { get; set; }
        }

        public class PrintsList
        {
            public string id;
            public string card_name;
            public List<Print> prints;
        }

        public class Print
        {
            public string card_ID;
            public string set_name;
            public string set;
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
