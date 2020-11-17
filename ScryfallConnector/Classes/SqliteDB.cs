using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ScryfallConnector.Classes
{
    public class SqliteDB
    {
        private string filePath;
        public SqlCeConnection connection;
        public SqlCeEngine engine;
        public SqliteDB()
        {
            filePath = "DataSource=\"appDB.sdf\"; Password=\"d7a7a247-51bd-4244-81c7-2b406a23cc69\"; Max Database Size=4000;";
            engine = new SqlCeEngine(filePath);
            if (!System.IO.Directory.Exists("images"))
            {
                System.IO.Directory.CreateDirectory("images");
            }
            if (!System.IO.File.Exists(".\\appDB.sdf"))
            {
                Console.WriteLine("DB not found. Creating DB...");
                engine.CreateDatabase();
                OpenConnection();
                GenerateTables();
            } else
            {
                OpenConnection();
                Console.WriteLine("DB found and connected.");
            }
        }

        private void OpenConnection()
        {
            connection = new SqlCeConnection(this.filePath);
            connection.Open();
        }

        private void GenerateTables()
        {
            GenerateCardImageTable();
            GeneratePrintsListTable();
            GenerateCardTable();
        }

        private void GenerateCardImageTable()
        {
            Console.WriteLine("Creating CardImage table...");
            string createImageTable = @"create table CardImage
                                        (
                                        CardImage_PK int primary key identity(1,1),
                                        Card_ID nvarchar(100) not null,
                                        Image_URL nvarchar(250) not null,
                                        Local_Filepath nvarchar(250) not null
                                        )";
            SqlCeCommand cmd = new SqlCeCommand(createImageTable, connection);
            cmd.ExecuteNonQuery();
            
        }

        private void GeneratePrintsListTable()
        {
            Console.WriteLine("Creating PrintsList table...");
            string createPrintsListTable = @"create table PrintsList
                                        (
                                        PrintsList_PK int primary key identity(1,1),
                                        Card_Name nvarchar(100) not null,
                                        Prints ntext not null
                                        )";
            SqlCeCommand cmd = new SqlCeCommand(createPrintsListTable, connection);
            cmd.ExecuteNonQuery();
        }

        private void GenerateCardTable()
        {
            Console.WriteLine("Creating Card table...");
            string createCardsTable = string.Empty;
            // this is gonna be the big one
            try
            {
                createCardsTable = @"create table Card";
                //temp version:
                //createCardsTable += "(";
                //createCardsTable += "Card_PK int primary key identity(1,1),";
                //createCardsTable += "id nvarchar(100) not null,";
                //createCardsTable += "card_name nvarchar(100) not null,";
                //createCardsTable += "set_name nvarchar(100) not null,";
                //createCardsTable += "set_abbr nvarchar(10) not null,";
                //createCardsTable += "type_line nvarchar(100),";
                //createCardsTable += "prints_search_uri ntext,";
                //createCardsTable += "image_uris ntext,";
                //createCardsTable += "json_string ntext not null";
                //createCardsTable += ")";

                // Organized according to API document cardSchema.docx
                createCardsTable += "(";

                // Critical to see first:
                createCardsTable += "card_PK int primary key identity(1,1),";
                createCardsTable += "name nvarchar(150) not null,"; //that market research meme card has 141 characters. :(

                //Internal (not from Scryfall)
                createCardsTable += "dateAdded datetime not null,";
                createCardsTable += "dateModified datetime not null,";
                createCardsTable += "modifiedMethod int not null,";

                // Core Card Fields
                // Cards have the following core properties:
                createCardsTable += "arena_id int,";
                createCardsTable += "id ntext not null,";
                createCardsTable += "lang nvarchar(100) not null,";
                createCardsTable += "mtgo_id int,";
                createCardsTable += "mtgo_foil_id int,";
                createCardsTable += "multiverse_ids ntext,";
                createCardsTable += "tcgplayer_id int,";
                createCardsTable += "object nvarchar(50) not null,";
                createCardsTable += "oracle_id ntext not null,";
                createCardsTable += "prints_search_uri ntext not null,";
                createCardsTable += "rulings_uri ntext not null,";
                createCardsTable += "scryfall_uri ntext not null,";
                createCardsTable += "uri ntext not null,";

                // Gameplay Fields
                // Cards have the following properties relevant to the game rules:
                createCardsTable += "all_parts ntext,";
                createCardsTable += "card_faces ntext,";
                createCardsTable += "cmc numeric(19,4) not null,";
                createCardsTable += "color_identity nvarchar(100) not null,";
                createCardsTable += "color_indicator nvarchar(100),";
                createCardsTable += "colors nvarchar(100),";
                createCardsTable += "edhrec_rank int,";
                createCardsTable += "foil bit not null,";
                createCardsTable += "hand_modifier nvarchar(100),";
                createCardsTable += "keywords nvarchar(150) not null,"; // card Old Fogey has 111 char in this property
                createCardsTable += "layout nvarchar(50) not null,";
                createCardsTable += "legalities ntext not null,";
                createCardsTable += "life_modifier nvarchar(10),";
                createCardsTable += "loyalty nvarchar(10),";
                createCardsTable += "mana_cost nvarchar(50),"; // B.F.M. has 45 characters in this property
                //createCardsTable += "name ,"; critical field, moved up
                createCardsTable += "nonfoil bit not null,";
                createCardsTable += "oracle_text ntext,";
                createCardsTable += "oversized bit not null,";
                createCardsTable += "power nvarchar(10),";
                createCardsTable += "produced_mana nvarchar(50),";
                createCardsTable += "reserved bit not null,";
                createCardsTable += "toughness nvarchar(10),";
                createCardsTable += "type_line nvarchar(100) not null,"; //Fucking GRIMLOCK of all cards broke this, the transformers promo. He has 79 total chars on both sides. 

                // Print fields
                createCardsTable += "artist nvarchar(100),";
                createCardsTable += "booster bit not null,";
                createCardsTable += "border_color nvarchar(25) not null,";
                createCardsTable += "card_back_id ntext not null,";
                createCardsTable += "collector_number nvarchar(50) not null,";
                createCardsTable += "content_warning bit,";
                createCardsTable += "digital bit not null,";
                createCardsTable += "flavor_name nvarchar(50),";
                createCardsTable += "flavor_text nvarchar(450),"; // Infinity elemental broke this, has 403 characters. Literally 3 more than the initial limit...
                createCardsTable += "frame_effects nvarchar(25),";
                createCardsTable += "frame nvarchar(10) not null,";
                createCardsTable += "full_art bit not null,";
                createCardsTable += "games nvarchar(100) not null,";
                createCardsTable += "highres_image bit not null,";
                createCardsTable += "illustration_id ntext,";
                createCardsTable += "image_uris ntext,";
                createCardsTable += "prices ntext not null,";
                createCardsTable += "printed_name nvarchar(150),";
                createCardsTable += "printed_text ntext not null,";
                createCardsTable += "printed_type_line nvarchar(75),";
                createCardsTable += "promo bit not null,";
                createCardsTable += "promo_types ntext,";
                createCardsTable += "purchase_uris ntext not null,";
                createCardsTable += "rarity nvarchar(25) not null,";
                createCardsTable += "related_uris ntext not null,";
                createCardsTable += "released_at datetime not null,";
                createCardsTable += "reprint bit not null,";
                createCardsTable += "scryfall_set_uri ntext not null,";
                createCardsTable += "set_name nvarchar(100) not null,";
                createCardsTable += "set_search_uri ntext not null,";
                createCardsTable += "set_type ntext not null,";
                createCardsTable += "set_uri ntext not null,";
                createCardsTable += "[set] nvarchar(25) not null,"; //THIS CAN'T JUST BE "SET", BANNED IN SQL
                createCardsTable += "story_spotlight bit not null,";
                createCardsTable += "textless bit not null,";
                createCardsTable += "variation bit not null,";
                createCardsTable += "variation_of ntext,";
                createCardsTable += "watermark ntext,";
                createCardsTable += "preview ntext"; // this field has the below three properties, don't need them separated, the program can deserialize
                //createCardsTable += "preview_previewed_at datetime,"; 
                //createCardsTable += "preview_source_uri ntext,";
                //createCardsTable += "preview_source ntext";

                createCardsTable += ")";

                SqlCeCommand cmd = new SqlCeCommand(createCardsTable, connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }
    }
}
