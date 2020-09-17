using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScryfallConnector.Classes
{

    public class ScryfallCard
    {
        public string _object { get; set; }
        public string id { get; set; }
        public string oracle_id { get; set; }
        public object[] multiverse_ids { get; set; }
        public int tcgplayer_id { get; set; }
        public string Name { get; set; }
        public string lang { get; set; }
        public string released_at { get; set; }
        public string Uri { get; set; }
        public string scryfall_uri { get; set; }
        public string layout { get; set; }
        public bool highres_image { get; set; }
        public Image_Uris image_uris { get; set; }
        public string mana_cost { get; set; }
        public float cmc { get; set; }
        public string type_line { get; set; }
        public string oracle_text { get; set; }
        public string power { get; set; }
        public string toughness { get; set; }
        public string[] colors { get; set; }
        public string[] color_identity { get; set; }
        public string[] keywords { get; set; }
        public Legalities legalities { get; set; }
        public string[] games { get; set; }
        public bool reserved { get; set; }
        public bool foil { get; set; }
        public bool nonfoil { get; set; }
        public bool oversized { get; set; }
        public bool promo { get; set; }
        public bool reprint { get; set; }
        public bool variation { get; set; }
        public string set { get; set; }
        public string set_name { get; set; }
        public string set_type { get; set; }
        public string set_uri { get; set; }
        public string set_search_uri { get; set; }
        public string scryfall_set_uri { get; set; }
        public string rulings_uri { get; set; }
        public string prints_search_uri { get; set; }
        public string collector_number { get; set; }
        public bool digital { get; set; }
        public string rarity { get; set; }
        public string flavor_text { get; set; }
        public string card_back_id { get; set; }
        public string artist { get; set; }
        public string[] artist_ids { get; set; }
        public string illustration_id { get; set; }
        public string border_color { get; set; }
        public string frame { get; set; }
        public bool full_art { get; set; }
        public bool textless { get; set; }
        public bool booster { get; set; }
        public bool story_spotlight { get; set; }
        public int edhrec_rank { get; set; }
        public Prices prices { get; set; }
        public Related_Uris related_uris { get; set; }
        public Purchase_Uris purchase_uris { get; set; }

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

        public String ToJson()
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            this.json_string = json;
            return json;
        }

        public string json_string;

        public void SaveToDB(SqliteDB dB)
        {
            try
            {
                SqlCeCommand cmd = new SqlCeCommand();

                string sqlInsert = "INSERT INTO Card";
                string sqlFields = "(id, card_name, set_name, set_abbr, type_line, prints_search_uri, image_uris, json_string)";
                string sqlValues = "VALUES (@id, @card_name, @set_name, @set_abbr, @type_line, @prints_search_uri, @image_uris, @json_string)";

                cmd = new SqlCeCommand(sqlInsert + sqlFields + sqlValues, dB.connection);

                cmd.Parameters.AddWithValue("@id", this.id);
                cmd.Parameters.AddWithValue("@card_name", this.Name);
                cmd.Parameters.AddWithValue("@set_name", this.set_name);
                cmd.Parameters.AddWithValue("@set_abbr", this.set);
                cmd.Parameters.AddWithValue("@type_line", this.type_line);
                cmd.Parameters.AddWithValue("@prints_search_uri", this.prints_search_uri);

                string images = Newtonsoft.Json.JsonConvert.SerializeObject(this.image_uris);
                cmd.Parameters.AddWithValue("@image_uris", images);

                cmd.Parameters.AddWithValue("@json_string", this.ToJson());

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
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

            retval.json_string = row["json_string"].ToString();

            return retval;
        }

    }
}
