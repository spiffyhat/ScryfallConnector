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
            filePath = "DataSource=\"appDB.sdf\"; Password=\"d7a7a247-51bd-4244-81c7-2b406a23cc69\"";
            engine = new SqlCeEngine(filePath);
            if (!System.IO.Directory.Exists("images"))
            {
                System.IO.Directory.CreateDirectory("images");
            }
            if (!System.IO.File.Exists(".\\appDB.sdf"))
            {
                engine.CreateDatabase();
                OpenConnection();
                GenerateTables();
            } else
            {
                OpenConnection();
            }
        }

        private void OpenConnection()
        {
            connection = new SqlCeConnection(this.filePath);
            connection.Open();
        }

        private void GenerateTables()
        {
            GenerateImageTable();
            GeneratePrintsListTable();
            GenerateCardsTable();
        }

        private void GenerateImageTable()
        {
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
            string createPrintsListTable = @"create table PrintsList
                                        (
                                        PrintsList_PK int primary key identity(1,1),
                                        Card_Name nvarchar(100) not null,
                                        Prints ntext not null
                                        )";
            SqlCeCommand cmd = new SqlCeCommand(createPrintsListTable, connection);
            cmd.ExecuteNonQuery();
        }

        private void GenerateCardsTable()
        {
            // this is gonna be the big one

            string createCardsTable = @"create table Card";
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

            //Internal (not in document)
            createCardsTable += "dateAdded datetime not null,";
            createCardsTable += "dateModified datetime not null";
            createCardsTable += "modifiedMethod int not null";

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
            createCardsTable += "type_line nvarchar(50) not null,"; //B.F.M. (again) has 42 characters here

            // Print fields
            createCardsTable += ",";

            createCardsTable += ")";

            SqlCeCommand cmd = new SqlCeCommand(createCardsTable, connection);
            cmd.ExecuteNonQuery();

        }
    }
}
