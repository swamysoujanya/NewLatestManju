namespace AviMa.Tools
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnConvertTaxData = new System.Windows.Forms.Button();
            this.dtGrdTaxDtMigration = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdTaxDtMigration)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(48, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create Bar Code Master Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConvertTaxData
            // 
            this.btnConvertTaxData.Location = new System.Drawing.Point(48, 113);
            this.btnConvertTaxData.Name = "btnConvertTaxData";
            this.btnConvertTaxData.Size = new System.Drawing.Size(182, 23);
            this.btnConvertTaxData.TabIndex = 1;
            this.btnConvertTaxData.Text = "ConvertTaxData";
            this.btnConvertTaxData.UseVisualStyleBackColor = true;
            this.btnConvertTaxData.Click += new System.EventHandler(this.btnConvertTaxData_Click);
            // 
            // dtGrdTaxDtMigration
            // 
            this.dtGrdTaxDtMigration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtGrdTaxDtMigration.Location = new System.Drawing.Point(48, 156);
            this.dtGrdTaxDtMigration.Name = "dtGrdTaxDtMigration";
            this.dtGrdTaxDtMigration.Size = new System.Drawing.Size(828, 363);
            this.dtGrdTaxDtMigration.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 575);
            this.Controls.Add(this.dtGrdTaxDtMigration);
            this.Controls.Add(this.btnConvertTaxData);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdTaxDtMigration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnConvertTaxData;
        private System.Windows.Forms.DataGridView dtGrdTaxDtMigration;
    }
}

