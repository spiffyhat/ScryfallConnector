﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScryfallConnector.Classes
{

    public class ScryfallCard
    {
        #region Properties
        // Critical to see first:
        public int card_PK { get; set; }
        public string Name { get; set; } //has to be capitalized for some reason

        // Internal 
        public DateTime dateAdded { get; private set; }
        public DateTime dateModified { get; private set; }
        public int modifiedMethod { get; private set; } // TODO change to enum

        // Core card fields
        public int arena_id { get; set; }
        public string id { get; set; }
        public string lang { get; set; }
        public int mtgo_id { get; set; }
        public int mtgo_foil_id { get; set; }
        public object[] multiverse_ids { get; set; }
        public int tcgplayer_id { get; set; }
        public string @object { get; set; } // should always be "card"
        public string oracle_id { get; set; }
        public string prints_search_uri { get; set; }
        public string rulings_uri { get; set; }
        public string scryfall_uri { get; set; }
        public string Uri { get; set; }

        // Gameplay fields
        public string[] all_parts { get; set; }
        public string[] card_faces { get; set; }
        public float cmc { get; set; }
        public string[] color_identity { get; set; }
        public string[] color_indicator { get; set; }
        public string[] colors { get; set; }
        public int edhrec_rank { get; set; }
        public bool foil { get; set; }
        public string hand_modifier { get; set; }
        public string[] keywords { get; set; } // not sure if this should be an array or not
        public string layout { get; set; }
        public Legalities legalities { get; set; }
        public string life_modifier { get; set; }
        public string loyalty { get; set; }
        public string mana_cost { get; set; }
        public bool nonfoil { get; set; }
        public string oracle_text { get; set; }
        public bool oversized { get; set; }
        public string power { get; set; }
        public string[] produced_mana { get; set; }
        public bool reserved { get; set; }
        public string toughness { get; set; }
        public string type_line { get; set; }

        // Print fields
        public string artist { get; set; }
        public bool booster { get; set; }
        public string border_color { get; set; }
        public string card_back_id { get; set; }
        public string collector_number { get; set; }
        public bool content_warning { get; set; }
        public bool digital { get; set; }
        public string flavor_name { get; set; }
        public string flavor_text { get; set; }
        //public string frame_effects { get; set; } // need array here and custom class
        public string frame { get; set; }
        public bool full_art { get; set; }
        public string[] games { get; set; }
        public bool highres_image { get; set; }
        public string illustration_id { get; set; }
        public Image_Uris image_uris { get; set; }
        public Prices prices { get; set; }
        public string printed_name { get; set; }
        public string printed_text { get; set; }
        public string printed_type_line { get; set; }
        public bool promo { get; set; }
        public string promo_types { get; set; }
        public Purchase_Uris purchase_uris { get; set; }
        public string rarity { get; set; }
        public Related_Uris related_uris { get; set; }
        public DateTime released_at { get; set; }
        public bool reprint { get; set; }
        public string scryfall_set_uri { get; set; }
        public string set_name { get; set; }
        public string set_search_uri { get; set; }
        public string set_type { get; set; }
        public string set_uri {get; set;}
        public string @set { get; set; }
        public bool story_spotlight { get; set; }
        public bool textless { get; set; }
        public bool variation { get; set; }
        public string variation_of { get; set; }
        public string watermark { get; set; }
        public Preview preview { get; set; }

        #endregion
        #region Subclasses
        public class Preview
        {
            // TODO add preview properties
        }

        public class Image_Uris
        {
            public string small { get; set; }
            public string normal { get; set; }
            public string large { get; set; }
            public string png { get; set; }
            public string art_crop { get; set; }
            public string border_crop { get; set; }
        }

        public class Legalities
        {
            public string standard { get; set; }
            public string future { get; set; }
            public string historic { get; set; }
            public string pioneer { get; set; }
            public string modern { get; set; }
            public string legacy { get; set; }
            public string pauper { get; set; }
            public string vintage { get; set; }
            public string penny { get; set; }
            public string commander { get; set; }
            public string brawl { get; set; }
            public string duel { get; set; }
            public string oldschool { get; set; }
        }

        public class Prices
        {
            public object usd { get; set; }
            public object usd_foil { get; set; }
            public object eur { get; set; }
            public object tix { get; set; }
        }

        public class Related_Uris
        {
            public string tcgplayer_decks { get; set; }
            public string edhrec { get; set; }
            public string mtgtop8 { get; set; }
        }

        public class Purchase_Uris
        {
            public string tcgplayer { get; set; }
            public string cardmarket { get; set; }
            public string cardhoarder { get; set; }
        }
        #endregion
        public String ToJson()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            //this.json_string = json;
            return json;
        }

        public static ScryfallCard FromJson(string json)
        {
            //DEBUG
            using (StreamWriter file = File.CreateText(@".\testcard.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, json);
            }

            ScryfallCard card = Newtonsoft.Json.JsonConvert.DeserializeObject<ScryfallCard>(json);
            return card;
        }

        //public string json_string;

        public void SaveToDB(SqliteDB dB)
        {
            try
            {

                SqlCeCommand testCmd = GenerateSaveSqlCmd(dB);
                LoadCardIntoSqlCmd(this, ref testCmd);

                testCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        private SqlCeCommand GenerateSaveSqlCmd(SqliteDB dB)
        {

            SqlCeCommand cmd = new SqlCeCommand();
            bool needsComma = false;
            try
            {
                

                string sqlInsert = "INSERT INTO Card ";
                // TODO use reflection to build this string
                string demoSqlFields = "(id, name, set_name, set_abbr, type_line, prints_search_uri, image_uris)";

                // TODO use reflection to build this string
                string demoSqlValues = "VALUES (@id, @name, @set_name, @set_abbr, @type_line, @prints_search_uri, @image_uris)";

                string sqlFields = string.Empty;
                sqlFields += "(";

                string sqlValues = string.Empty;
                sqlValues += "VALUES (";

                // loop through all properties for a ScryfallCard and build the string
                PropertyInfo[] pi = typeof(ScryfallCard).GetProperties();
                foreach (PropertyInfo info in pi)
                {
                    //DEBUG
                    //if (info.Name == "color_identity")
                    //{
                    //    Console.WriteLine("here");
                    //}
                    switch (info.Name)
                    {
                        case "card_PK":
                        //case "dateAdded":
                        //case "dateModified":
                        //case "modifiedMethod":
                            continue;
                    }
                    if (needsComma)
                    {
                        sqlFields += ", ";
                        sqlValues += ", ";
                    }
                    needsComma = true;
                    sqlFields += "[" + info.Name + "]";
                    sqlValues += "@" + info.Name;
                }

                sqlFields += ") ";
                sqlValues += ") ";



                cmd = new SqlCeCommand(sqlInsert + sqlFields + sqlValues, dB.connection);

            }
            catch (Exception)
            {

                throw;
            }

            return cmd;
        }

        private void LoadCardIntoSqlCmd(ScryfallCard card, ref SqlCeCommand cmd)
        {
            try
            {
                PropertyInfo[] pi = typeof(ScryfallCard).GetProperties();
                foreach (PropertyInfo info in pi)
                {
                    //DEBUG
                    if (info.Name == "color_identity")
                    {
                        Console.WriteLine("here");
                    }
                    switch (info.Name)
                    {
                        case "card_PK":
                        case "dateAdded":
                        case "dateModified":
                        case "modifiedMethod":
                            continue;
                    }
                    TypeCode typeCode = Type.GetTypeCode(info.PropertyType);
                    switch (typeCode)
                    {
                        case TypeCode.Object:
                            HandleSerializedValue(ref cmd, info, card);
                            continue;
                    }
                    if (info.GetValue(card) is null)
                    {
                        cmd.Parameters.AddWithValue("@" + info.Name, "NULL");
                    } 
                    else
                    {
                        // get the property and add it as the value for the cmd
                        cmd.Parameters.AddWithValue("@" + info.Name, info.GetValue(card));
                    }
                }

                cmd.Parameters.AddWithValue("@dateAdded", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@dateModified", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@modifiedMethod", 1);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void HandleSerializedValue(ref SqlCeCommand cmd, PropertyInfo info, ScryfallCard card)
        {
            try
            {
                string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(info.GetValue(card));
                cmd.Parameters.AddWithValue("@" + info.Name, serialized);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static ScryfallCard LoadFromDB(DataRow row)
        {
            ScryfallCard retval = new ScryfallCard();

            retval.id = row["id"].ToString();
            retval.Name = row["card_name"].ToString();
            retval.set_name = row["set_name"].ToString();
            retval.set = row["set_abbr"].ToString();
            retval.type_line = row["type_line"].ToString();
            retval.prints_search_uri = row["prints_search_uri"].ToString();

            retval.image_uris = Newtonsoft.Json.JsonConvert.DeserializeObject<Image_Uris>(row["image_uris"].ToString());

            //retval.json_string = row["json_string"].ToString();

            return retval;
        }

    }
}
