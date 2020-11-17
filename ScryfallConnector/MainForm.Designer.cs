namespace ScryfallConnector
{
    partial class MainForm
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
            this.txtCardName = new System.Windows.Forms.TextBox();
            this.btnRandom = new System.Windows.Forms.Button();
            this.picCard = new System.Windows.Forms.PictureBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.combobox1 = new System.Windows.Forms.ComboBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.chkIncludeExtras = new System.Windows.Forms.CheckBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.chkPopulateJson = new System.Windows.Forms.CheckBox();
            this.btnOpenDeckBuilder = new System.Windows.Forms.Button();
            this.btnFlipCard = new System.Windows.Forms.Button();
            this.btnOpenBulkData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCard)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCardName
            // 
            this.txtCardName.Location = new System.Drawing.Point(333, 15);
            this.txtCardName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCardName.Multiline = true;
            this.txtCardName.Name = "txtCardName";
            this.txtCardName.ReadOnly = true;
            this.txtCardName.Size = new System.Drawing.Size(193, 41);
            this.txtCardName.TabIndex = 0;
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(16, 15);
            this.btnRandom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(309, 42);
            this.btnRandom.TabIndex = 1;
            this.btnRandom.Text = "Test API (get random card)";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.button1_Click);
            // 
            // picCard
            // 
            this.picCard.Location = new System.Drawing.Point(537, 16);
            this.picCard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picCard.Name = "picCard";
            this.picCard.Size = new System.Drawing.Size(319, 382);
            this.picCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCard.TabIndex = 2;
            this.picCard.TabStop = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(16, 84);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(53, 17);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Search";
            // 
            // combobox1
            // 
            this.combobox1.FormattingEnabled = true;
            this.combobox1.Location = new System.Drawing.Point(80, 84);
            this.combobox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.combobox1.Name = "combobox1";
            this.combobox1.Size = new System.Drawing.Size(244, 24);
            this.combobox1.TabIndex = 5;
            this.combobox1.SelectedIndexChanged += new System.EventHandler(this.combobox1_SelectedIndexChanged);
            this.combobox1.TextUpdate += new System.EventHandler(this.combobox1_TextUpdate);
            this.combobox1.TextChanged += new System.EventHandler(this.combobox1_TextChanged);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // chkIncludeExtras
            // 
            this.chkIncludeExtras.AutoSize = true;
            this.chkIncludeExtras.Location = new System.Drawing.Point(333, 86);
            this.chkIncludeExtras.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkIncludeExtras.Name = "chkIncludeExtras";
            this.chkIncludeExtras.Size = new System.Drawing.Size(118, 21);
            this.chkIncludeExtras.TabIndex = 6;
            this.chkIncludeExtras.Text = "Include Extras";
            this.chkIncludeExtras.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(80, 162);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(447, 230);
            this.treeView1.TabIndex = 7;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // chkPopulateJson
            // 
            this.chkPopulateJson.AutoSize = true;
            this.chkPopulateJson.Checked = true;
            this.chkPopulateJson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPopulateJson.Location = new System.Drawing.Point(80, 134);
            this.chkPopulateJson.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkPopulateJson.Name = "chkPopulateJson";
            this.chkPopulateJson.Size = new System.Drawing.Size(120, 21);
            this.chkPopulateJson.TabIndex = 8;
            this.chkPopulateJson.Text = "Populate Json";
            this.chkPopulateJson.UseVisualStyleBackColor = true;
            this.chkPopulateJson.CheckedChanged += new System.EventHandler(this.chkPopulateJson_CheckedChanged);
            // 
            // btnOpenDeckBuilder
            // 
            this.btnOpenDeckBuilder.Location = new System.Drawing.Point(20, 414);
            this.btnOpenDeckBuilder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenDeckBuilder.Name = "btnOpenDeckBuilder";
            this.btnOpenDeckBuilder.Size = new System.Drawing.Size(184, 28);
            this.btnOpenDeckBuilder.TabIndex = 9;
            this.btnOpenDeckBuilder.Text = "Open Deck Builder";
            this.btnOpenDeckBuilder.UseVisualStyleBackColor = true;
            this.btnOpenDeckBuilder.Click += new System.EventHandler(this.btnOpenDeckBuilder_Click);
            // 
            // btnFlipCard
            // 
            this.btnFlipCard.Enabled = false;
            this.btnFlipCard.Location = new System.Drawing.Point(644, 405);
            this.btnFlipCard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFlipCard.Name = "btnFlipCard";
            this.btnFlipCard.Size = new System.Drawing.Size(100, 28);
            this.btnFlipCard.TabIndex = 10;
            this.btnFlipCard.Text = "Flip Card";
            this.btnFlipCard.UseVisualStyleBackColor = true;
            this.btnFlipCard.Click += new System.EventHandler(this.btnFlipCard_Click);
            // 
            // btnOpenBulkData
            // 
            this.btnOpenBulkData.Location = new System.Drawing.Point(224, 414);
            this.btnOpenBulkData.Name = "btnOpenBulkData";
            this.btnOpenBulkData.Size = new System.Drawing.Size(206, 28);
            this.btnOpenBulkData.TabIndex = 11;
            this.btnOpenBulkData.Text = "Open Bulk Data Form";
            this.btnOpenBulkData.UseVisualStyleBackColor = true;
            this.btnOpenBulkData.Click += new System.EventHandler(this.btnOpenBulkData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 462);
            this.Controls.Add(this.btnOpenBulkData);
            this.Controls.Add(this.btnFlipCard);
            this.Controls.Add(this.btnOpenDeckBuilder);
            this.Controls.Add(this.chkPopulateJson);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.chkIncludeExtras);
            this.Controls.Add(this.combobox1);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.picCard);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.txtCardName);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Scryfall Connector";
            ((System.ComponentModel.ISupportInitialize)(this.picCard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCardName;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.PictureBox picCard;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox combobox1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.CheckBox chkIncludeExtras;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckBox chkPopulateJson;
        private System.Windows.Forms.Button btnOpenDeckBuilder;
        private System.Windows.Forms.Button btnFlipCard;
        private System.Windows.Forms.Button btnOpenBulkData;
    }
}

