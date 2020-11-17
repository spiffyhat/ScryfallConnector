namespace ScryfallConnector
{
    partial class BulkDataForm
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
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.btnLoadBulkData = new System.Windows.Forms.Button();
            this.chkTop100 = new System.Windows.Forms.CheckBox();
            this.btnClearOutput = new System.Windows.Forms.Button();
            this.btnUpdateDB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 96);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(595, 227);
            this.txtOutput.TabIndex = 0;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(13, 18);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(314, 22);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(346, 14);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(105, 31);
            this.btnChooseFile.TabIndex = 2;
            this.btnChooseFile.Text = "Choose File";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            // 
            // btnLoadBulkData
            // 
            this.btnLoadBulkData.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadBulkData.Location = new System.Drawing.Point(468, 59);
            this.btnLoadBulkData.Name = "btnLoadBulkData";
            this.btnLoadBulkData.Size = new System.Drawing.Size(139, 31);
            this.btnLoadBulkData.TabIndex = 3;
            this.btnLoadBulkData.Text = "Load Bullk Data";
            this.btnLoadBulkData.UseVisualStyleBackColor = true;
            this.btnLoadBulkData.Click += new System.EventHandler(this.btnLoadBulkData_Click);
            // 
            // chkTop100
            // 
            this.chkTop100.AutoSize = true;
            this.chkTop100.Location = new System.Drawing.Point(468, 20);
            this.chkTop100.Name = "chkTop100";
            this.chkTop100.Size = new System.Drawing.Size(113, 21);
            this.chkTop100.TabIndex = 4;
            this.chkTop100.Text = "Top 100 only";
            this.chkTop100.UseVisualStyleBackColor = true;
            // 
            // btnClearOutput
            // 
            this.btnClearOutput.Location = new System.Drawing.Point(327, 59);
            this.btnClearOutput.Name = "btnClearOutput";
            this.btnClearOutput.Size = new System.Drawing.Size(124, 31);
            this.btnClearOutput.TabIndex = 5;
            this.btnClearOutput.Text = "Clear Output";
            this.btnClearOutput.UseVisualStyleBackColor = true;
            this.btnClearOutput.Click += new System.EventHandler(this.btnClearOutput_Click);
            // 
            // btnUpdateDB
            // 
            this.btnUpdateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateDB.Location = new System.Drawing.Point(179, 59);
            this.btnUpdateDB.Name = "btnUpdateDB";
            this.btnUpdateDB.Size = new System.Drawing.Size(131, 31);
            this.btnUpdateDB.TabIndex = 6;
            this.btnUpdateDB.Text = "Update Cards";
            this.btnUpdateDB.UseVisualStyleBackColor = true;
            this.btnUpdateDB.Click += new System.EventHandler(this.btnUpdateDB_Click);
            // 
            // BulkDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 334);
            this.Controls.Add(this.btnUpdateDB);
            this.Controls.Add(this.btnClearOutput);
            this.Controls.Add(this.chkTop100);
            this.Controls.Add(this.btnLoadBulkData);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.txtOutput);
            this.Name = "BulkDataForm";
            this.Text = "BulkDataForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Button btnLoadBulkData;
        private System.Windows.Forms.CheckBox chkTop100;
        private System.Windows.Forms.Button btnClearOutput;
        private System.Windows.Forms.Button btnUpdateDB;
    }
}