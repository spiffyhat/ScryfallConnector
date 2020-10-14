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

        public Image GetCardImage(ScryfallCard card, bool frontFace)
        {
            return FetchCardImage(card, frontFace);
        }

        private Image FetchCardImage(ScryfallCard card, bool frontFace)
        {
            Image retval = null;
            string cardID = card.id;
            string imageURL;
            string imagePath;

            SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM CardImage WHERE Card_ID = \'" + cardID + "\'", db.connection);
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            DataTable result = new DataTable();
            da.Fill(result);


            switch (result.Rows.Count)
            {
                
                case 0: // no rows returned
                    // we want the front face
                    if (frontFace)
                    {
                        // if the card is single faced and the normal image url isn't misssing
                        if (card.image_uris != null && card.image_uris.normal != null)
                        {
                            imageURL = card.image_uris.normal;

                            System.Net.WebRequest request = System.Net.WebRequest.Create(imageURL);
                            System.Net.WebResponse response = request.GetResponse();
                            System.IO.Stream responseStream =
                                response.GetResponseStream();
                            retval = new Bitmap(responseStream);

                            string filePath = cardID + ".jpg";
                            retval.Save(".\\images\\" + filePath);
                            cmd = new SqlCeCommand("INSERT INTO CardImage (Card_ID, Image_URL, Local_Filepath)" +
                                                    "VALUES (@Card_ID, @Image_URL, @Local_Filepath)", db.connection);
                            cmd.Parameters.AddWithValue("@Card_ID", cardID);
                            cmd.Parameters.AddWithValue("@Image_URL", imageURL);
                            cmd.Parameters.AddWithValue("@Local_Filepath", filePath);
                            cmd.ExecuteNonQuery();
                        }
                        // if the card is multifaced
                        else if (card.card_faces != null)
                        {
                            // if the front face image url is present
                            if (card.card_faces[0].image_uris != null && card.card_faces[0].image_uris.normal != null)
                            {
                                imageURL = card.card_faces[0].image_uris.normal;

                                System.Net.WebRequest request = System.Net.WebRequest.Create(imageURL);
                                System.Net.WebResponse response = request.GetResponse();
                                System.IO.Stream responseStream =
                                    response.GetResponseStream();
                                retval = new Bitmap(responseStream);

                                string filePath = cardID + "_0" + ".jpg";

                                retval.Save(".\\images\\" + filePath);
                                cmd = new SqlCeCommand("INSERT INTO CardImage (Card_ID, Image_URL, Local_Filepath)" +
                                                        "VALUES (@Card_ID, @Image_URL, @Local_Filepath)", db.connection);
                                cmd.Parameters.AddWithValue("@Card_ID", cardID);
                                cmd.Parameters.AddWithValue("@Image_URL", imageURL);
                                cmd.Parameters.AddWithValue("@Local_Filepath", filePath);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            throw new Exception("wtf?");
                        }
                    }
                    else // we want the back face (it must be multifaced)
                    {
                        // if the back face url is present
                        if (card.card_faces[1].image_uris != null && card.card_faces[1].image_uris.normal != null)
                        {
                            imageURL = card.card_faces[1].image_uris.normal;

                            System.Net.WebRequest request = System.Net.WebRequest.Create(imageURL);
                            System.Net.WebResponse response = request.GetResponse();
                            System.IO.Stream responseStream =
                                response.GetResponseStream();
                            retval = new Bitmap(responseStream);

                            string filePath = cardID + "_1" + ".jpg";

                            retval.Save(".\\images\\" + filePath);
                            cmd = new SqlCeCommand("INSERT INTO CardImage (Card_ID, Image_URL, Local_Filepath)" +
                                                    "VALUES (@Card_ID, @Image_URL, @Local_Filepath)", db.connection);
                            cmd.Parameters.AddWithValue("@Card_ID", cardID);
                            cmd.Parameters.AddWithValue("@Image_URL", imageURL);
                            cmd.Parameters.AddWithValue("@Local_Filepath", filePath);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            throw new NotImplementedException("Image url missing!");
                        }
                    }
                    break;

                     
                case 1: // only one row returned
                    if (frontFace)
                    {
                        // if the card is NOT multifaced or the card is multifaced but one-sided
                        if (card.card_faces == null || card.card_faces[1].image_uris == null)
                        {
                            imagePath = result.Rows[0]["Local_Filepath"].ToString();
                            retval = Bitmap.FromFile(".\\images\\" + imagePath);
                        }
                        else
                        {
                            throw new Exception("wtf");
                        }
                    }
                    else // the card is multifaced and we want the back face
                    {
                        if (result.Rows[0]["Local_Filepath"].ToString().EndsWith("_1.jpg")) // if somehow the only row returned is the back face image
                        {
                            imagePath = result.Rows[0]["Local_Filepath"].ToString();
                            retval = Bitmap.FromFile(".\\images\\" + imagePath);
                        }
                        else if (card.card_faces[1].image_uris != null && card.card_faces[1].image_uris.normal != null) // if the back face image url is present
                        {
                            imageURL = card.card_faces[1].image_uris.normal;

                            System.Net.WebRequest request = System.Net.WebRequest.Create(imageURL);
                            System.Net.WebResponse response = request.GetResponse();
                            System.IO.Stream responseStream =
                                response.GetResponseStream();
                            retval = new Bitmap(responseStream);

                            string filePath = cardID + "_1" + ".jpg";

                            retval.Save(".\\images\\" + filePath);
                            cmd = new SqlCeCommand("INSERT INTO CardImage (Card_ID, Image_URL, Local_Filepath)" +
                                                    "VALUES (@Card_ID, @Image_URL, @Local_Filepath)", db.connection);
                            cmd.Parameters.AddWithValue("@Card_ID", cardID);
                            cmd.Parameters.AddWithValue("@Image_URL", imageURL);
                            cmd.Parameters.AddWithValue("@Local_Filepath", filePath);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            throw new NotImplementedException("Second face image url missing!");
                        }
                    }
                    break;


                default: // more than 1 row returned
                    if (frontFace) // we want the front face
                    {
                        if (result.Rows[0]["Local_Filepath"].ToString().EndsWith("_0.jpg")) // if row 1 is the front face
                        {
                            imagePath = result.Rows[0]["Local_Filepath"].ToString();
                            retval = Bitmap.FromFile(".\\images\\" + imagePath);
                        }
                        else if (result.Rows[1]["Local_Filepath"].ToString().EndsWith("_0.jpg")) // if row 2 is the front face
                        {
                            imagePath = result.Rows[1]["Local_Filepath"].ToString();
                            retval = Bitmap.FromFile(".\\images\\" + imagePath);
                        }
                        else
                        {
                            throw new Exception("wtf");
                        }
                    }
                    else // we want the back face
                    {
                        if (result.Rows[0]["Local_Filepath"].ToString().EndsWith("_1.jpg")) // if row 1 is the back face
                        {
                            imagePath = result.Rows[0]["Local_Filepath"].ToString();
                            retval = Bitmap.FromFile(".\\images\\" + imagePath);
                        }
                        else if (result.Rows[1]["Local_Filepath"].ToString().EndsWith("_1.jpg")) // if row 2 is the back face
                        {
                            imagePath = result.Rows[1]["Local_Filepath"].ToString();
                            retval = Bitmap.FromFile(".\\images\\" + imagePath);
                        }
                        else
                        {
                            throw new Exception("wtf");
                        }
                    }
                    break;
            }


            // OLD VERSION
            //if (frontFace && result.Rows.Count == 1)
            //{
            //    imageURL = result.Rows[0]["Local_Filepath"].ToString();
            //    retval = Bitmap.FromFile(".\\images\\" + imageURL);
            //} else
            //{
            //    if (frontFace && card.image_uris != null && card.image_uris.normal != null)
            //    {
            //        imageURL = card.image_uris.normal;

            //        System.Net.WebRequest request = System.Net.WebRequest.Create(imageURL);
            //        System.Net.WebResponse response = request.GetResponse();
            //        System.IO.Stream responseStream =
            //            response.GetResponseStream();
            //        retval = new Bitmap(responseStream);

            //        //string filePath = (".\\images\\" + cardID + ".jpg");
            //        string filePath = cardID + ".jpg";
            //        retval.Save(".\\images\\" + filePath);
            //        cmd = new SqlCeCommand("INSERT INTO CardImage (Card_ID, Image_URL, Local_Filepath)" +
            //                                "VALUES (@Card_ID, @Image_URL, @Local_Filepath)", db.connection);
            //        cmd.Parameters.AddWithValue("@Card_ID", cardID);
            //        cmd.Parameters.AddWithValue("@Image_URL", imageURL);
            //        cmd.Parameters.AddWithValue("@Local_Filepath", filePath);
            //        cmd.ExecuteNonQuery();
            //    }
            //    else if (card.card_faces != null && card.card_faces[0].image_uris != null && card.card_faces[0].image_uris.normal != null)
            //    {
            //        if (frontFace)
            //        {
            //            imageURL = card.card_faces[0].image_uris.normal;
            //        }
            //        else
            //        {
            //            if (card.card_faces[1] != null
            //                && card.card_faces[1].image_uris != null
            //                && card.card_faces[1].image_uris.normal != null)
            //                {
            //                    imageURL = card.card_faces[1].image_uris.normal;
            //                }
            //            else
            //            {
            //                throw new NotImplementedException("Second face has no image!");
            //            }
            //        }

            //        System.Net.WebRequest request = System.Net.WebRequest.Create(imageURL);
            //        System.Net.WebResponse response = request.GetResponse();
            //        System.IO.Stream responseStream =
            //            response.GetResponseStream();
            //        retval = new Bitmap(responseStream);

            //        //string filePath = (".\\images\\" + cardID + ".jpg");
            //        string filePath = cardID + ".jpg";
            //        retval.Save(".\\images\\" + filePath);
            //        cmd = new SqlCeCommand("INSERT INTO CardImage (Card_ID, Image_URL, Local_Filepath)" +
            //                                "VALUES (@Card_ID, @Image_URL, @Local_Filepath)", db.connection);
            //        cmd.Parameters.AddWithValue("@Card_ID", cardID);
            //        cmd.Parameters.AddWithValue("@Image_URL", imageURL);
            //        cmd.Parameters.AddWithValue("@Local_Filepath", filePath);
            //        cmd.ExecuteNonQuery();
            //    }
                
            //}

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
            ScryfallCard card;
            try
            {
                System.Threading.Thread.Sleep(100);
                string responseContent = string.Empty;
                HttpResponseMessage response = client.GetAsync("cards/random").Result;
                response.EnsureSuccessStatusCode();
                responseContent = response.Content.ReadAsStringAsync().Result;

                card = ScryfallCard.FromJson(responseContent);

                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM CARD WHERE [id] like \'" + card.id + "\'", db.connection);
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                DataTable result = new DataTable();
                da.Fill(result);

                if (result.Rows.Count == 0)
                {
                    card.SaveToDB(db);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            

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

            SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM PRINTSLIST WHERE Card_Name = \'" + card.Name.Replace("\'", "\'\'") + "\'", db.connection);
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            DataTable result = new DataTable();
            da.Fill(result);

            if (result.Rows.Count != 0)
            {
                Console.WriteLine(String.Format("prints list exists in DB for {0}", card.Name));
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

                Console.WriteLine(String.Format("prints added to DB for {0}", card.Name));
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

            retval = ScryfallCard.FromJson(responseContent);

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

        public ScryfallCard GetNamedCardExact(string text)
        {
            ScryfallCard card;
            try
            {
                card = FetchNamedCardExact(text);
            }
            catch (Exception)
            {
                card = null;
            }
            return card;
        }

        private ScryfallCard FetchNamedCardExact(string text)
        {
            ScryfallCard retval = null;
            try
            {
                System.Threading.Thread.Sleep(100);
                string responseContent = string.Empty;
                HttpResponseMessage response;

                SqlCeCommand cmd = new SqlCeCommand("SELECT * FROM CARD WHERE name = \'" + text.Replace("\'","\'\'") + "\'", db.connection);
                SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
                DataTable result = new DataTable();
                da.Fill(result);
                if (result.Rows.Count > 1)
                {
                    Console.WriteLine(String.Format("Card {0} has more than one record!!!", text));
                }
                else if (result.Rows.Count != 0)
                {
                    Console.WriteLine(String.Format("card {0} exists in DB", text));
                    retval = ScryfallCard.LoadFromDB(result.Rows[0]);
                }
                else
                {
                    
                    response = client.GetAsync("cards/named?exact=" + text).Result;
                    response.EnsureSuccessStatusCode();
                    responseContent = response.Content.ReadAsStringAsync().Result;

                    retval = ScryfallCard.FromJson(responseContent);

                    retval.SaveToDB(db);
                }
            }
            catch (Exception ex)
            {
                retval = null;
                throw;
            }
           

            return retval;

        }

    }
}
