﻿using Newtonsoft.Json.Linq;
using ScryfallConnector.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ScryfallConnector
{
    public partial class MainForm : Form
    {
        SqliteDB dB = new SqliteDB();
        ScryfallEngine engine;
        ScryfallCard currentCard = new ScryfallCard();
        bool frontFace = true;
        bool validAutocomplete = false;

        public MainForm()
        {
            InitializeComponent();
            timer2.Interval = 1000;
            engine = new ScryfallEngine(dB);
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            this.currentCard = engine.GetRandomCard();
            this.validAutocomplete = false;
            this.combobox1.Text = string.Empty;
            UpdateAutocompleteData();
            ShowCurrentCard();
        }

        private void ShowCurrentCard()
        {
            this.treeView1.Nodes.Clear();
            if (this.currentCard != null)
            {
                this.picCard.Image = engine.GetCardImage(currentCard, true);
                this.frontFace = true;
                this.btnFlipCard.Enabled = (this.currentCard != null
                                            && this.currentCard.card_faces != null
                                            && this.currentCard.card_faces.Length == 2
                                            && this.currentCard.card_faces[1].image_uris != null);
                this.txtCardName.Text = currentCard.Name;
                if (this.chkPopulateJson.Checked)
                {
                    PopulateTreeView(this.currentCard.ToJson());
                    // this.treeView1.ExpandAll();
                }
            }
            else
            {
                this.picCard.Image = null;
                this.txtCardName.Text = string.Empty;
                this.treeView1.Nodes.Clear();
            }
           
        }

        private void FlipCard()
        {
            if (frontFace)
            {
                // get the back image
                this.picCard.Image = engine.GetCardImage(currentCard, false);
                frontFace = false;
            }
            else
            {
                // get the front image
                this.picCard.Image = engine.GetCardImage(currentCard, true);
                frontFace = true;
            }
        }

        private List<string> SuggestStrings(string text, bool include_extras)
        {
            List<string> suggestions = new List<string>();
            try
            {
                suggestions = this.engine.AutoComplete(text, include_extras);

                //switch (text)
                //{
                //    case "Pla":
                //        suggestions.Add("Plains");
                //        break;
                //    case "Isl":
                //        suggestions.Add("Island");
                //        break;
                //    case "Swa":
                //        suggestions.Add("Swamp");
                //        break;
                //    case "Mou":
                //        suggestions.Add("Mountain");
                //        break;
                //    case "For":
                //        suggestions.Add("Forest");
                //        suggestions.Add("Force of Will");
                //        break;
                //    default:
                //        //suggestions.Add("Plains");
                //        //suggestions.Add("Island");
                //        //suggestions.Add("Swamp");
                //        //suggestions.Add("Mountain");
                //        //suggestions.Add("Forest");
                //        break;
                //}
            }
            catch (Exception)
            {
                suggestions = null;
            }
            return suggestions;
        }

        #region AutoComplete

        private bool _canUpdate = true;

        private bool _needUpdate = false;

        //If text has been changed then start timer
        //If the user doesn't change text while the timer runs then start search
        private void combobox1_TextChanged(object sender, EventArgs e) 
        {
            if (_needUpdate)
            {
                if (_canUpdate)
                {
                    _canUpdate = false;
                    UpdateAutocompleteData();
                }
                else
                {
                    RestartAutocompleteTimer();
                }
            }
        }

        private void UpdateAutocompleteData()
        {
            if (combobox1.Text.Length > 2)
            {
                List<string> searchData = SuggestStrings(combobox1.Text, chkIncludeExtras.Checked);
                HandleAutocompleteTextChanged(searchData);
            }
            else if (combobox1.Text.Length == 0)
            {
                HandleAutocompleteTextChanged(null);
                //have to do this garbage to reset the size of the dropdown
                combobox1.Items.Clear();
                combobox1.Items.Add("");
            }
        }

        //If an item was selected don't start new search, and get named card
        private void combobox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _needUpdate = false;
            if (this.validAutocomplete && (this.currentCard == null || this.currentCard.Name != combobox1.Text))
            {
                currentCard = engine.GetNamedCardExact(combobox1.Text);
                ShowCurrentCard();
            }
        }

        //Update data only when the user (not program) change something
        private void combobox1_TextUpdate(object sender, EventArgs e)
        {
            _needUpdate = true;
        }

        //While timer is running don't start search
        //timer1.Interval = 1500;
        private void RestartAutocompleteTimer()
        {
            timer2.Stop();
            _canUpdate = false;
            timer2.Start();
        }

        //Update data when timer stops
        private void timer2_Tick(object sender, EventArgs e)
        {
            _canUpdate = true;
            this.timer2.Stop();
            UpdateAutocompleteData();
        }

        //Update combobox with new data
        private void HandleAutocompleteTextChanged(List<string> dataSource)
        {
            var text = combobox1.Text;
            combobox1.DataSource = dataSource;

            if (!(dataSource is null) && dataSource.Count() > 0)
            {
                

                var sText = combobox1.Items[0].ToString();
                combobox1.SelectionStart = text.Length;
                combobox1.SelectionLength = sText.Length - text.Length;
                this.validAutocomplete = true;
                combobox1.DroppedDown = true;
                Cursor.Current = Cursors.Default;


                return;
            }
            else
            {
                this.validAutocomplete = false;
                combobox1.DroppedDown = false;
                //combobox1.SelectionStart = text.Length;
            }
        }

        #endregion

        #region TreeView Population

        private void PopulateTreeView(string json)
        {
            JObject jobj;
            try
            {
                jobj = JObject.Parse(json);
                this.treeView1.Nodes.Clear();
                TreeNode parent = Json2Tree(jobj);
                parent.Text = "Root object";
                treeView1.Nodes.Add(parent);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private TreeNode Json2Tree(JObject obj)
        {
            TreeNode parent;
            try
            {
                //create the parent node
                parent = new TreeNode();
                //loop through the obj. all token should be pair<key, value>
                foreach (var token in obj)
                {
                    //change the display Content of the parent
                    parent.Text = token.Key.ToString();
                    //create the child node
                    TreeNode child = new TreeNode();
                    child.Text = token.Key.ToString();
                    //check if the value is of type obj recall the method
                    if (token.Value.Type.ToString() == "Object")
                    {
                        // child.Text = token.Key.ToString();
                        //create a new JObject using the the Token.value
                        JObject o = (JObject)token.Value;
                        //recall the method
                        child = Json2Tree(o);
                        //add the child to the parentNode
                        parent.Nodes.Add(child);
                    }
                    //if type is of array
                    else if (token.Value.Type.ToString() == "Array")
                    {
                        int ix = -1;
                        //  child.Text = token.Key.ToString();
                        //loop though the array
                        foreach (var itm in token.Value)
                        {
                            //check if value is an Array of objects
                            if (itm.Type.ToString() == "Object")
                            {
                                TreeNode objTN = new TreeNode();
                                //child.Text = token.Key.ToString();
                                //call back the method
                                ix++;

                                JObject o = (JObject)itm;
                                objTN = Json2Tree(o);
                                objTN.Text = token.Key.ToString() + "[" + ix + "]";
                                child.Nodes.Add(objTN);
                                //parent.Nodes.Add(child);
                            }
                            //regular array string, int, etc
                            else if (itm.Type.ToString() == "Array")
                            {
                                ix++;
                                TreeNode dataArray = new TreeNode();
                                foreach (var data in itm)
                                {
                                    dataArray.Text = token.Key.ToString() + "[" + ix + "]";
                                    dataArray.Nodes.Add(data.ToString());
                                }
                                child.Nodes.Add(dataArray);
                            }

                            else
                            {
                                child.Nodes.Add(itm.ToString());
                            }
                        }
                        parent.Nodes.Add(child);
                    }
                    else
                    {
                        //if token.Value is not nested
                        // child.Text = token.Key.ToString();
                        //change the value into N/A if value == null or an empty string 
                        if (token.Value.ToString() == "")
                            child.Nodes.Add("N/A");
                        else
                            child.Nodes.Add(token.Value.ToString());
                        parent.Nodes.Add(child);
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }

            return parent;
        }

        #endregion

        private void chkPopulateJson_CheckedChanged(object sender, EventArgs e)
        {
            this.treeView1.Visible = this.chkPopulateJson.Checked;
        }

        private void btnOpenDeckBuilder_Click(object sender, EventArgs e)
        {
            bool loadTestDeck = (MessageBox.Show("Use test deck?", "Test Deck", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
            DeckStatisticsForm form = new DeckStatisticsForm(this.dB, loadTestDeck);
            form.ShowDialog();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null) Clipboard.SetText(treeView1.SelectedNode.Text);
        }

        private void btnFlipCard_Click(object sender, EventArgs e)
        {
            FlipCard();
        }

        private void btnOpenBulkData_Click(object sender, EventArgs e)
        {
            BulkDataForm form = new BulkDataForm(this.dB);
            form.ShowDialog();
        }
    }
}
