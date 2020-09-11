namespace ScryfallConnector
{
    partial class Form1
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
            ((System.ComponentModel.ISupportInitialize)(this.picCard)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCardName
            // 
            this.txtCardName.Location = new System.Drawing.Point(250, 12);
            this.txtCardName.Multiline = true;
            this.txtCardName.Name = "txtCardName";
            this.txtCardName.Size = new System.Drawing.Size(146, 34);
            this.txtCardName.TabIndex = 0;
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(12, 12);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(232, 34);
            this.btnRandom.TabIndex = 1;
            this.btnRandom.Text = "Test API (get random card)";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.button1_Click);
            // 
            // picCard
            // 
            this.picCard.Location = new System.Drawing.Point(403, 13);
            this.picCard.Name = "picCard";
            this.picCard.Size = new System.Drawing.Size(239, 310);
            this.picCard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCard.TabIndex = 2;
            this.picCard.TabStop = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(12, 68);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 4;
            this.lblSearch.Text = "Search";
            // 
            // combobox1
            // 
            this.combobox1.FormattingEnabled = true;
            this.combobox1.Location = new System.Drawing.Point(60, 68);
            this.combobox1.Name = "combobox1";
            this.combobox1.Size = new System.Drawing.Size(184, 21);
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
            this.chkIncludeExtras.Location = new System.Drawing.Point(250, 70);
            this.chkIncludeExtras.Name = "chkIncludeExtras";
            this.chkIncludeExtras.Size = new System.Drawing.Size(93, 17);
            this.chkIncludeExtras.TabIndex = 6;
            this.chkIncludeExtras.Text = "Include Extras";
            this.chkIncludeExtras.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(60, 132);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(336, 188);
            this.treeView1.TabIndex = 7;
            // 
            // chkPopulateJson
            // 
            this.chkPopulateJson.AutoSize = true;
            this.chkPopulateJson.Checked = true;
            this.chkPopulateJson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPopulateJson.Location = new System.Drawing.Point(60, 109);
            this.chkPopulateJson.Name = "chkPopulateJson";
            this.chkPopulateJson.Size = new System.Drawing.Size(93, 17);
            this.chkPopulateJson.TabIndex = 8;
            this.chkPopulateJson.Text = "Populate Json";
            this.chkPopulateJson.UseVisualStyleBackColor = true;
            this.chkPopulateJson.CheckedChanged += new System.EventHandler(this.chkPopulateJson_CheckedChanged);
            // 
            // btnOpenDeckBuilder
            // 
            this.btnOpenDeckBuilder.Location = new System.Drawing.Point(15, 336);
            this.btnOpenDeckBuilder.Name = "btnOpenDeckBuilder";
            this.btnOpenDeckBuilder.Size = new System.Drawing.Size(138, 23);
            this.btnOpenDeckBuilder.TabIndex = 9;
            this.btnOpenDeckBuilder.Text = "Open Deck Builder";
            this.btnOpenDeckBuilder.UseVisualStyleBackColor = true;
            this.btnOpenDeckBuilder.Click += new System.EventHandler(this.btnOpenDeckBuilder_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 375);
            this.Controls.Add(this.btnOpenDeckBuilder);
            this.Controls.Add(this.chkPopulateJson);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.chkIncludeExtras);
            this.Controls.Add(this.combobox1);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.picCard);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.txtCardName);
            this.Name = "Form1";
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
    }
}

