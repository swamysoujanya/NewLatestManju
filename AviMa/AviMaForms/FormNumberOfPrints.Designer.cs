namespace AviMa.AviMaForms
{
    partial class FormNumberOfPrints
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnContinuePrint = new System.Windows.Forms.Button();
            this.txtNumbOfCopies = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number Of Copies :";
            // 
            // btnContinuePrint
            // 
            this.btnContinuePrint.Location = new System.Drawing.Point(55, 108);
            this.btnContinuePrint.Name = "btnContinuePrint";
            this.btnContinuePrint.Size = new System.Drawing.Size(147, 23);
            this.btnContinuePrint.TabIndex = 1;
            this.btnContinuePrint.Text = "Save and Print";
            this.btnContinuePrint.UseVisualStyleBackColor = true;
            this.btnContinuePrint.Click += new System.EventHandler(this.btnContinuePrint_Click);
            // 
            // txtNumbOfCopies
            // 
            this.txtNumbOfCopies.Location = new System.Drawing.Point(152, 66);
            this.txtNumbOfCopies.MaxLength = 1;
            this.txtNumbOfCopies.Name = "txtNumbOfCopies";
            this.txtNumbOfCopies.Size = new System.Drawing.Size(71, 20);
            this.txtNumbOfCopies.TabIndex = 2;
            this.txtNumbOfCopies.Text = "1";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(52, 39);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 13);
            this.lblName.TabIndex = 3;
            // 
            // FormNumberOfPrints
            // 
            this.AcceptButton = this.btnContinuePrint;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 191);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtNumbOfCopies);
            this.Controls.Add(this.btnContinuePrint);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNumberOfPrints";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Number Of Invoice Copies";
            this.Load += new System.EventHandler(this.FormNumberOfPrints_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnContinuePrint;
        public System.Windows.Forms.TextBox txtNumbOfCopies;
        public System.Windows.Forms.Label lblName;
    }
}