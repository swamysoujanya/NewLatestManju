namespace AviMa.AviMaForms
{
    partial class frmDisplayDetailsInGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplayDetailsInGrid));
            this.dataGridDetails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridDetails
            // 
            this.dataGridDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDetails.Location = new System.Drawing.Point(12, 12);
            this.dataGridDetails.Name = "dataGridDetails";
            this.dataGridDetails.ReadOnly = true;
            this.dataGridDetails.Size = new System.Drawing.Size(943, 476);
            this.dataGridDetails.TabIndex = 7;
            // 
            // frmDisplayDetailsInGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 500);
            this.Controls.Add(this.dataGridDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDisplayDetailsInGrid";
            this.Text = "Details";
            this.Load += new System.EventHandler(this.frmDisplayDetailsInGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridDetails;
    }
}