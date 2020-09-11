﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScryfallConnector.Classes
{
    public partial class DeckBuilderForm : Form
    {
        ScryfallEngine engine = new ScryfallEngine();
        ScryfallCard currentCard = new ScryfallCard();
        Deck deck = new Deck();
        bool validAutocomplete = false;
        BindingSource bs;

        public DeckBuilderForm()
        {
            InitializeComponent();
            timer2.Interval = 1000;
            bs = new BindingSource();
            bs.DataSource = this.deck.cards;
            lstDeckList.DataSource = bs;
            lstDeckList.DisplayMember = "Name";
            lstDeckList.ValueMember = "id";
            lstDeckList.Sorted = false;
        }

        private void ShowCurrentCard()
        {
            if (this.currentCard != null )
            {
                this.picCard.Load(currentCard.image_uris.normal);
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

        private void btnSetCommander_Click(object sender, EventArgs e)
        {
            this.deck.commander = currentCard;
            UpdateControlStates();
        }

        private void UpdateControlStates()
        {
            this.btnSetCommander.Enabled = (this.currentCard != null && this.deck.commander == null);
            this.btnAddCard.Enabled = (this.currentCard != null && this.deck.commander != null);
             if (this.deck.commander != null) this.txtCommander.Text = this.deck.commander.Name;
            ShowCurrentCard();
        }

        private void btnAddCard_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < int.Parse(txtCopies.Text); i++)
            {
                this.deck.cards.Add(currentCard);
                bs.ResetBindings(false);
            }

            UpdateControlStates();
                
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            this.deck.ShuffleCards();
            bs.ResetBindings(false);
        }
    }
}
