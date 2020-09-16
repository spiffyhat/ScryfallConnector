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
            //this is gonna be the big one
            string createCardsTable = @"create table Card";
            createCardsTable += "(";
            createCardsTable += "Card_PK int primary key identity(1,1),";
            createCardsTable += "id nvarchar(100) not null,";
            createCardsTable += "card_name nvarchar(100) not null,";
            createCardsTable += "set_name nvarchar(100) not null,";
            createCardsTable += "set_abbr nvarchar(10) not null,";
            createCardsTable += "type_line nvarchar(100),";
            createCardsTable += "image_uris ntext,";
            createCardsTable += "json_string ntext not null";
            createCardsTable += ")";

            SqlCeCommand cmd = new SqlCeCommand(createCardsTable, connection);
            cmd.ExecuteNonQuery();

        }
    }
}
