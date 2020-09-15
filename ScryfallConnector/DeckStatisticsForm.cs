﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScryfallConnector.Classes
{
    public partial class DeckStatisticsForm : Form
    {
        ScryfallEngine engine;
        ScryfallCard currentCard = new ScryfallCard();
        Deck deck = new Deck();
        bool validAutocomplete = false;
        BindingSource bs;

        public DeckStatisticsForm(SqliteDB db, bool loadTestDeck)
        {
            InitializeComponent();
            timer2.Interval = 1000;
            engine = new ScryfallEngine(db);
            bs = new BindingSource();
            bs.DataSource = this.deck.cards;
            lstDeckList.DataSource = bs;
            lstDeckList.DisplayMember = "Name";
            lstDeckList.ValueMember = "id";
            lstDeckList.Sorted = false;
            if (loadTestDeck)
            {
                this.LoadTestDeck();
            }
        }

        private void ShowCurrentCard()
        {
            if (this.currentCard != null )
            {
                this.picCard.Image = engine.GetCardImage(currentCard);
            }
        }

        private List<string> SuggestStrings(string text, bool include_extras)
        {
            List<string> suggestions = new List<string>();
            try
            {
                suggestions = this.engine.AutoComplete(text, include_extras);
            }
            catch (Exception)
            {
                suggestions = null;
            }
            return suggestions;
        }

        #region Test Deck

        private void LoadTestDeck()
        {
            this.currentCard = this.engine.GetNamedCard("Shadowborn Apostle");
            for (int i = 0; i < 66; i++)
            {
                this.deck.cards.Add(currentCard);
            }
            this.currentCard = this.engine.GetNamedCard("Swamp");
            for (int i = 0; i < 33; i++)
            {
                this.deck.cards.Add(currentCard);
            }
            
            this.currentCard = this.engine.GetNamedCard("Krrik, Son of Yawgmoth");
            this.deck.commander = this.currentCard;

            // this triggers Update Control States
            // I believe it triggers the selected index changed event for the list box
            bs.ResetBindings(false);
        }

        #endregion

        #region Removing cards

        private void RemoveCard(int index)
        {
            this.deck.cards.RemoveAt(index);
        }

        private void ReplaceCardWithAlternatePrint(int index, string replacementID)
        {
            this.deck.cards[index] = this.engine.FetchCardByID(replacementID);
            this.currentCard = this.deck.cards[index];
        }

        #endregion

        #region Context Menus

        private void SetDeckContextMenu(MouseEventArgs e)
        {
            lstDeckList.SelectedIndex = lstDeckList.IndexFromPoint(e.X, e.Y);
            ctxDeckList.Items.Clear();
            if (this.lstDeckList.SelectedIndex != -1)
            {
                string cardName = this.deck.cards[this.lstDeckList.SelectedIndex].Name;
                ctxDeckList = new ContextMenuStrip();

                string remove = string.Format("Remove {0}", cardName);
                ctxDeckList.Items.Add(remove).Click += ctsRemove_Click;

                string viewOnline = string.Format("View {0} on Scryfall.com", cardName);
                ctxDeckList.Items.Add(viewOnline).Click += ctxViewOnline_Click;

                string chooseSetVersion = "Choose specific print...";
                ctxDeckList.Items.Add(chooseSetVersion, null, null);
                SetPrintsContextSubmenu(ctxDeckList.Items[2] as ToolStripMenuItem);

                lstDeckList.ContextMenuStrip = ctxDeckList;
            }
            
        }

        private void SetPrintsContextSubmenu(ToolStripMenuItem menuItem)
        {
            List<ScryfallCard> prints = this.engine.FetchPrintsByUrl(this.deck.cards[this.lstDeckList.SelectedIndex].prints_search_uri);
            if (prints.Count > 0)
            {
                foreach (ScryfallCard card in prints)
                {
                    menuItem.DropDownItems.Add(String.Format("{1} ({0})", card.set.ToUpper(), card.set_name), null, ctxChangePrint_Click).Tag = card.id;
                }

            }
            else
            {
                menuItem.DropDownItems.Add("<empty>", null, null);
            }
        }

        #endregion

        #region View online

        private void ViewCardOnline(ScryfallCard card)
        {
            Process.Start(card.scryfall_uri);
        }

        #endregion

        #region Probability testing

        private List<ScryfallCard> GetOpeningHand(Deck deck)
        {
            List<ScryfallCard> retval = new List<ScryfallCard>();
            try
            {
                deck.ShuffleCards();
                for (int i = 0; i < 7; i++)
                {
                    retval.Add(deck.cards[i]);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return retval;
        }

        private int CountStartingLands(List<ScryfallCard> list)
        {
            int retval = 0;
            try
            {
                foreach (ScryfallCard card in list)
                {
                    if (card.type_line.Contains("Land"))
                    {
                        retval++;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return retval;
        }

        private int CountStartingNonLands(List<ScryfallCard> list)
        {
            int retval = 0;
            try
            {
                foreach (ScryfallCard card in list)
                {
                    if (!card.type_line.Contains("Land"))
                    {
                        retval++;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return retval;
        }

        private string RunProbabilityTest(Deck deck, int times, bool verboseLog)
        {
            string retval = string.Empty;
            string handResult = string.Empty;
            string handResultTemplate = "Opening hand {0}: {1} lands, {2} nonlands.";
            string endResultTemplate = "Total hands: {0}. {1} lands, {2} nonlands, {3} total cards drawn.";
            string averageTemplate = "Average lands per hand: {0}";
            string landHandsTemplate = "Hands by land distribution: {0} zero, {1} one, {2} two, {3} three, {4} four, {5} five, {6} six, {7} seven.";
            int counter = 0;
            int lands = 0;
            int nonlands = 0;
            int totalLands = 0;
            int totalNonLands = 0;
            int totalCards = 0;

            // Hands by land distribution
            int zeroLand = 0;
            int oneLand = 0;
            int twoLand = 0;
            int threeLand = 0;
            int fourLand = 0;
            int fiveLand = 0;
            int sixLand = 0;
            int sevenLand = 0;

            decimal averageLandsPerHand = 0;

            List<ScryfallCard> hand = new List<ScryfallCard>();
            try
            {
                for (int i = 0; i < times; i++)
                {
                    counter++;
                    hand = GetOpeningHand(deck);
                    lands = CountStartingLands(hand);
                    nonlands = CountStartingNonLands(hand);
                    handResult = String.Format(handResultTemplate, counter, lands, nonlands);
                    if (verboseLog)
                    {
                        retval += handResult + Environment.NewLine;
                    }
                    totalLands += lands;
                    totalNonLands += nonlands;
                    totalCards += hand.Count;
                    switch (lands)
                    {
                        case 0:
                            zeroLand++;
                            break;
                        case 1:
                            oneLand++;
                            break;
                        case 2:
                            twoLand++;
                            break;
                        case 3:
                            threeLand++;
                            break;
                        case 4:
                            fourLand++;
                            break;
                        case 5:
                            fiveLand++;
                            break;
                        case 6:
                            sixLand++;
                            break;
                        case 7:
                            sevenLand++;
                            break;
                    }
                }
                averageLandsPerHand = (decimal)totalLands / (decimal)counter;
                averageLandsPerHand = Math.Round(averageLandsPerHand, 2);
                if (!verboseLog)
                {
                    retval = string.Format(endResultTemplate, counter, totalLands, totalNonLands, totalCards);
                    retval += Environment.NewLine;
                    retval += string.Format(averageTemplate, averageLandsPerHand);
                    retval += Environment.NewLine;
                    retval += string.Format(landHandsTemplate, zeroLand, oneLand, twoLand, threeLand, fourLand, fiveLand, sixLand, sevenLand);
                }
                UpdateControlStates();
            }
            catch (Exception)
            {

                throw;
            }
            return retval;
        }

        private void HandleTestButton()
        {
            this.btnTestStartingHands.Enabled = false;
            if (int.Parse(this.txtTimes.Text) >= 5)
            {
                this.txtTestOutput.Text = RunProbabilityTest(this.deck, int.Parse(this.txtTimes.Text), false);
            } else
            {
                this.txtTestOutput.Text = RunProbabilityTest(this.deck, int.Parse(this.txtTimes.Text), true);
            }
            this.btnTestStartingHands.Enabled = true;
        }

        #endregion

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
            if (this.validAutocomplete)
            {
                currentCard = engine.GetNamedCard(combobox1.Text);
                UpdateControlStates();
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
        private void UpdateControlStates()
        {
            this.btnSetCommander.Enabled = (this.currentCard != null && this.deck.commander == null);
            this.btnAddCard.Enabled = (this.currentCard != null && this.deck.commander != null && this.txtCopies.Text != string.Empty);
            if (this.deck.commander != null) this.txtCommander.Text = this.deck.commander.Name;
            if (this.currentCard != null) ShowCurrentCard();
            this.lblDecklist.Text = string.Format("Decklist ({0})", this.deck.cards.Count);
        }

        private void btnSetCommander_Click(object sender, EventArgs e)
        {
            this.deck.commander = currentCard;
            UpdateControlStates();
        }



        private void btnAddCard_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < int.Parse(txtCopies.Text); i++)
            {
                this.deck.cards.Add(currentCard);
            }
            bs.ResetBindings(false);
            this.txtCopies.Text = string.Empty;
            UpdateControlStates();
                
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            this.deck.ShuffleCards();
            bs.ResetBindings(false);
        }

        private void btnTestStartingHands_Click(object sender, EventArgs e)
        {
            HandleTestButton();
        }

        private void txtCopies_TextChanged(object sender, EventArgs e)
        {
            UpdateControlStates();
        }

        private void lstDeckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstDeckList.SelectedIndex != -1) this.currentCard = this.deck.cards[this.lstDeckList.SelectedIndex];
            UpdateControlStates();
        }

        private void txtCommander_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.deck.commander != null) this.currentCard = this.deck.commander;
            UpdateControlStates();
        }

        private void lstDeckList_MouseDown(object sender, MouseEventArgs e)
        {
            SetDeckContextMenu(e);
        }

        private void ctxViewOnline_Click(object sender, EventArgs e)
        {
            ViewCardOnline(this.deck.cards[this.lstDeckList.SelectedIndex]);
        }

        private void ctsRemove_Click(object sender, EventArgs e)
        {
            RemoveCard(this.lstDeckList.SelectedIndex);
            UpdateControlStates();
        }

        private void ctxChangePrint_Click(object sender, EventArgs e)
        {
            string replacementID= (sender as ToolStripMenuItem).Tag.ToString();
            ReplaceCardWithAlternatePrint(this.lstDeckList.SelectedIndex, replacementID);
            UpdateControlStates();
        }
    }
}
