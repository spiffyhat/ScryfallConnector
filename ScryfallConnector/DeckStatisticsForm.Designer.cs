namespace ScryfallConnector.Classes
{
    partial class DeckStatisticsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chkIncludeExtras = new System.Windows.Forms.CheckBox();
            this.combobox1 = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.picCard = new System.Windows.Forms.PictureBox();
            this.lstDeckList = new System.Windows.Forms.ListBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnAddCard = new System.Windows.Forms.Button();
            this.txtCommander = new System.Windows.Forms.TextBox();
            this.lblCommander = new System.Windows.Forms.Label();
            this.btnSetCommander = new System.Windows.Forms.Button();
            this.txtCopies = new System.Windows.Forms.TextBox();
            this.lblCopies = new System.Windows.Forms.Label();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.btnTestStartingHands = new System.Windows.Forms.Button();
            this.lblTimes = new System.Windows.Forms.Label();
            this.txtTimes = new System.Windows.Forms.TextBox();
            this.txtTestOutput = new System.Windows.Forms.TextBox();
            this.lblDecklist = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCard)).BeginInit();
            this.SuspendLayout();
            // 
            // chkIncludeExtras
            // 
            this.chkIncludeExtras.AutoSize = true;
            this.chkIncludeExtras.Location = new System.Drawing.Point(248, 14);
            this.chkIncludeExtras.Name = "chkIncludeExtras";
            this.chkIncludeExtras.Size = new System.Drawing.Size(93, 17);
            this.chkIncludeExtras.TabIndex = 9;
            this.chkIncludeExtras.Text = "Include Extras";
            this.chkIncludeExtras.UseVisualStyleBackColor = true;
            // 
            // combobox1
            // 
            this.combobox1.FormattingEnabled = true;
            this.combobox1.Location = new System.Drawing.Point(58, 9);
            this.combobox1.Name = "combobox1";
            this.combobox1.Size = new System.Drawing.Size(184, 21);
            this.combobox1.TabIndex = 8;
            this.combobox1.SelectedIndexChanged += new System.EventHandler(this.combobox1_SelectedIndexChanged);
            this.combobox1.TextUpdate += new System.EventHandler(this.combobox1_TextUpdate);
            this.combobox1.TextChanged += new System.EventHandler(this.combobox1_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(10, 12);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 7;
            this.lblSearch.Text = "Search";
            // 
            // picCard
            // 
            this.picCard.Location = new System.Drawing.Point(347, 12);
            this.picCard.Name = "picCard";
            this.picCard.Size = new System.Drawing.Size(208, 278);
            this.picCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCard.TabIndex = 10;
            this.picCard.TabStop = false;
            // 
            // lstDeckList
            // 
            this.lstDeckList.FormattingEnabled = true;
            this.lstDeckList.Location = new System.Drawing.Point(562, 53);
            this.lstDeckList.Name = "lstDeckList";
            this.lstDeckList.Size = new System.Drawing.Size(222, 238);
            this.lstDeckList.TabIndex = 11;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnAddCard
            // 
            this.btnAddCard.Enabled = false;
            this.btnAddCard.Location = new System.Drawing.Point(167, 36);
            this.btnAddCard.Name = "btnAddCard";
            this.btnAddCard.Size = new System.Drawing.Size(75, 23);
            this.btnAddCard.TabIndex = 12;
            this.btnAddCard.Text = "Add Card";
            this.btnAddCard.UseVisualStyleBackColor = true;
            this.btnAddCard.Click += new System.EventHandler(this.btnAddCard_Click);
            // 
            // txtCommander
            // 
            this.txtCommander.Location = new System.Drawing.Point(627, 12);
            this.txtCommander.Name = "txtCommander";
            this.txtCommander.ReadOnly = true;
            this.txtCommander.Size = new System.Drawing.Size(157, 20);
            this.txtCommander.TabIndex = 13;
            // 
            // lblCommander
            // 
            this.lblCommander.AutoSize = true;
            this.lblCommander.Location = new System.Drawing.Point(561, 15);
            this.lblCommander.Name = "lblCommander";
            this.lblCommander.Size = new System.Drawing.Size(66, 13);
            this.lblCommander.TabIndex = 7;
            this.lblCommander.Text = "Commander:";
            // 
            // btnSetCommander
            // 
            this.btnSetCommander.Location = new System.Drawing.Point(58, 36);
            this.btnSetCommander.Name = "btnSetCommander";
            this.btnSetCommander.Size = new System.Drawing.Size(99, 23);
            this.btnSetCommander.TabIndex = 12;
            this.btnSetCommander.Text = "Set Commander";
            this.btnSetCommander.UseVisualStyleBackColor = true;
            this.btnSetCommander.Click += new System.EventHandler(this.btnSetCommander_Click);
            // 
            // txtCopies
            // 
            this.txtCopies.Location = new System.Drawing.Point(248, 38);
            this.txtCopies.MaxLength = 3;
            this.txtCopies.Name = "txtCopies";
            this.txtCopies.Size = new System.Drawing.Size(28, 20);
            this.txtCopies.TabIndex = 14;
            this.txtCopies.TextChanged += new System.EventHandler(this.txtCopies_TextChanged);
            // 
            // lblCopies
            // 
            this.lblCopies.AutoSize = true;
            this.lblCopies.Location = new System.Drawing.Point(282, 41);
            this.lblCopies.Name = "lblCopies";
            this.lblCopies.Size = new System.Drawing.Size(38, 13);
            this.lblCopies.TabIndex = 15;
            this.lblCopies.Text = "copies";
            // 
            // btnShuffle
            // 
            this.btnShuffle.Location = new System.Drawing.Point(248, 267);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(75, 23);
            this.btnShuffle.TabIndex = 16;
            this.btnShuffle.Text = "Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // btnTestStartingHands
            // 
            this.btnTestStartingHands.Location = new System.Drawing.Point(13, 93);
            this.btnTestStartingHands.Name = "btnTestStartingHands";
            this.btnTestStartingHands.Size = new System.Drawing.Size(144, 23);
            this.btnTestStartingHands.TabIndex = 17;
            this.btnTestStartingHands.Text = "Test Starting Hands";
            this.btnTestStartingHands.UseVisualStyleBackColor = true;
            this.btnTestStartingHands.Click += new System.EventHandler(this.btnTestStartingHands_Click);
            // 
            // lblTimes
            // 
            this.lblTimes.AutoSize = true;
            this.lblTimes.Location = new System.Drawing.Point(215, 99);
            this.lblTimes.Name = "lblTimes";
            this.lblTimes.Size = new System.Drawing.Size(31, 13);
            this.lblTimes.TabIndex = 18;
            this.lblTimes.Text = "times";
            // 
            // txtTimes
            // 
            this.txtTimes.Location = new System.Drawing.Point(163, 96);
            this.txtTimes.Name = "txtTimes";
            this.txtTimes.Size = new System.Drawing.Size(46, 20);
            this.txtTimes.TabIndex = 19;
            // 
            // txtTestOutput
            // 
            this.txtTestOutput.Location = new System.Drawing.Point(13, 123);
            this.txtTestOutput.Multiline = true;
            this.txtTestOutput.Name = "txtTestOutput";
            this.txtTestOutput.ReadOnly = true;
            this.txtTestOutput.Size = new System.Drawing.Size(307, 138);
            this.txtTestOutput.TabIndex = 20;
            // 
            // lblDecklist
            // 
            this.lblDecklist.AutoSize = true;
            this.lblDecklist.Location = new System.Drawing.Point(562, 36);
            this.lblDecklist.Name = "lblDecklist";
            this.lblDecklist.Size = new System.Drawing.Size(89, 13);
            this.lblDecklist.TabIndex = 21;
            this.lblDecklist.Text = "Decklist (in order)";
            // 
            // DeckStatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 312);
            this.Controls.Add(this.lblDecklist);
            this.Controls.Add(this.txtTestOutput);
            this.Controls.Add(this.txtTimes);
            this.Controls.Add(this.lblTimes);
            this.Controls.Add(this.btnTestStartingHands);
            this.Controls.Add(this.btnShuffle);
            this.Controls.Add(this.lblCopies);
            this.Controls.Add(this.txtCopies);
            this.Controls.Add(this.txtCommander);
            this.Controls.Add(this.btnSetCommander);
            this.Controls.Add(this.btnAddCard);
            this.Controls.Add(this.lstDeckList);
            this.Controls.Add(this.picCard);
            this.Controls.Add(this.chkIncludeExtras);
            this.Controls.Add(this.combobox1);
            this.Controls.Add(this.lblCommander);
            this.Controls.Add(this.lblSearch);
            this.Name = "DeckStatisticsForm";
            this.Text = "Deck Statistics";
            ((System.ComponentModel.ISupportInitialize)(this.picCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkIncludeExtras;
        private System.Windows.Forms.ComboBox combobox1;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.PictureBox picCard;
        private System.Windows.Forms.ListBox lstDeckList;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnAddCard;
        private System.Windows.Forms.TextBox txtCommander;
        private System.Windows.Forms.Label lblCommander;
        private System.Windows.Forms.Button btnSetCommander;
        private System.Windows.Forms.TextBox txtCopies;
        private System.Windows.Forms.Label lblCopies;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Button btnTestStartingHands;
        private System.Windows.Forms.Label lblTimes;
        private System.Windows.Forms.TextBox txtTimes;
        private System.Windows.Forms.TextBox txtTestOutput;
        private System.Windows.Forms.Label lblDecklist;
    }
}