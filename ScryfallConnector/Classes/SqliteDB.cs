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
            filePath = "DataSource=\"test.sdf\"; Password=\"test\"";
            engine = new SqlCeEngine(filePath);
            if (!System.IO.Directory.Exists("images"))
            {
                System.IO.Directory.CreateDirectory("images");
            }
            if (!System.IO.File.Exists(".\\test.sdf"))
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
        }

        private void GenerateImageTable()
        {
            string createImageTable = @"create table Images_Normal
                                        (
                                        Images_Normal_ID int primary key identity(1,1),
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
                                        PrintsList_ID int primary key identity(1,1),
                                        Card_Name nvarchar(100) not null,
                                        Prints ntext not null
                                        )";
            SqlCeCommand cmd = new SqlCeCommand(createPrintsListTable, connection);
            cmd.ExecuteNonQuery();
        }

    }
}
