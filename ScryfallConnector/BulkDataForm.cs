using Newtonsoft.Json;
using ScryfallConnector.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScryfallConnector
{
    public partial class BulkDataForm : Form
    {
        ScryfallEngine engine;
        BackgroundWorker backgroundWorker;
        SqliteDB db;
        public BulkDataForm(SqliteDB odb)
        {
            InitializeComponent();
            engine = new ScryfallEngine(odb);
            this.db = odb;
            backgroundWorker = new BackgroundWorker();
        }
        // prevent blank instantiation (TODO should probably do this everywhere if it works)
        private BulkDataForm()
        {
            // :(
        }

        private void UpdateControlStates()
        {
            this.btnLoadBulkData.Enabled = (this.txtFilePath.Text != string.Empty);
        }

        private void BeginBulkDataLoad(string filepath, bool top100Only)
        {
            int counter = 0;
            int added = 0;
            ScryfallCard card = new ScryfallCard();
            DateTime started;
            DateTime finished;
            List<string> ids = null;
            try
            {
                if (File.Exists(filepath) && filepath.EndsWith(".json"))
                {
                    started = DateTime.Now;

                    ids = this.engine.GetListOfExistingCardIDs();

                    Console.WriteLine("Started bulk load at " + started.ToLongTimeString());
                    txtOutput.AppendText("Started bulk load at " + started.ToLongTimeString() + "..." + Environment.NewLine + Environment.NewLine);

                    JsonSerializer serializer = new JsonSerializer();
                    using (FileStream s = File.Open(filepath, FileMode.Open))
                    using (StreamReader sr = new StreamReader(s))
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        while (reader.Read())
                        {
                            // deserialize only when there's "{" character in the stream
                            if (reader.TokenType == JsonToken.StartObject)
                            {
                                card = new ScryfallCard();
                                card = serializer.Deserialize<ScryfallCard>(reader);
                                //used to use CardExistsInDb
                                if (!ids.Contains(card.id))
                                {
                                    card.SaveToDB(this.db);
                                    //txtOutput.AppendText(String.Format("Card {0} added to DB.", card.Name) + Environment.NewLine);
                                    ids.Add(card.id);
                                    added++;
                                }
                                else
                                {
                                    //txtOutput.AppendText(String.Format("Card {0} already in DB.", card.Name) + Environment.NewLine);
                                }
                                counter++;
                                if (top100Only && counter > 99)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    finished = DateTime.Now;

                    Console.WriteLine(String.Format("Finished processing {0} cards at {1}.", counter, finished));
                    txtOutput.AppendText(Environment.NewLine + String.Format("Finished processing {0} cards at {1}.", counter, finished));

                    Console.WriteLine(String.Format("{0} new cards added.", added));
                    txtOutput.AppendText(Environment.NewLine + String.Format("{0} new cards added.", added));

                    Console.WriteLine(String.Format("Total time elapsed: {0}", finished.Subtract(started).ToString()));
                    txtOutput.AppendText(Environment.NewLine + String.Format("Total time elapsed: {0}", finished.Subtract(started).ToString()));
                    

                } else
                {
                    MessageBox.Show("File path is invalid!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Failed at {0}", card.Name));
            }

        }

        private void btnLoadBulkData_Click(object sender, EventArgs e)
        {
            this.btnLoadBulkData.Enabled = false;
            BeginBulkDataLoad(this.txtFilePath.Text, this.chkTop100.Checked);
        }

        private void btnClearOutput_Click(object sender, EventArgs e)
        {
            this.txtOutput.Text = string.Empty;
            UpdateControlStates();
        }

        private void btnUpdateDB_Click(object sender, EventArgs e)
        {
            this.engine.GetLatestCards();
        }
    }
}
