namespace AviMa.AviMaForms
{
    partial class FormAppAdmin
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
            this.txtDefaultDisc = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.grpBoxConfigDefDisc = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.grbBoxRefreshInvNo = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.grpBoxConfigDefDisc.SuspendLayout();
            this.grbBoxRefreshInvNo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Default Discount % :";
            // 
            // txtDefaultDisc
            // 
            this.txtDefaultDisc.Location = new System.Drawing.Point(118, 26);
            this.txtDefaultDisc.MaxLength = 2;
            this.txtDefaultDisc.Name = "txtDefaultDisc";
            this.txtDefaultDisc.Size = new System.Drawing.Size(47, 20);
            this.txtDefaultDisc.TabIndex = 1;
            this.txtDefaultDisc.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Set Default Discount";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "%";
            // 
            // grpBoxConfigDefDisc
            // 
            this.grpBoxConfigDefDisc.BackColor = System.Drawing.Color.Orange;
            this.grpBoxConfigDefDisc.Controls.Add(this.btnClear);
            this.grpBoxConfigDefDisc.Controls.Add(this.button1);
            this.grpBoxConfigDefDisc.Controls.Add(this.label2);
            this.grpBoxConfigDefDisc.Controls.Add(this.label1);
            this.grpBoxConfigDefDisc.Controls.Add(this.txtDefaultDisc);
            this.grpBoxConfigDefDisc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBoxConfigDefDisc.ForeColor = System.Drawing.Color.Black;
            this.grpBoxConfigDefDisc.Location = new System.Drawing.Point(28, 118);
            this.grpBoxConfigDefDisc.Name = "grpBoxConfigDefDisc";
            this.grpBoxConfigDefDisc.Size = new System.Drawing.Size(445, 68);
            this.grpBoxConfigDefDisc.TabIndex = 4;
            this.grpBoxConfigDefDisc.TabStop = false;
            this.grpBoxConfigDefDisc.Text = "Configure Default Discount";
            this.grpBoxConfigDefDisc.Visible = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(338, 27);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(85, 23);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // grbBoxRefreshInvNo
            // 
            this.grbBoxRefreshInvNo.BackColor = System.Drawing.Color.Orange;
            this.grbBoxRefreshInvNo.Controls.Add(this.button2);
            this.grbBoxRefreshInvNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbBoxRefreshInvNo.ForeColor = System.Drawing.Color.Black;
            this.grbBoxRefreshInvNo.Location = new System.Drawing.Point(28, 27);
            this.grbBoxRefreshInvNo.Name = "grbBoxRefreshInvNo";
            this.grbBoxRefreshInvNo.Size = new System.Drawing.Size(445, 68);
            this.grbBoxRefreshInvNo.TabIndex = 5;
            this.grbBoxRefreshInvNo.TabStop = false;
            this.grbBoxRefreshInvNo.Text = "Refresh Invoice Numebrs";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(51, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(311, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Refresh Invoice Numebrs";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FormAppAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 211);
            this.Controls.Add(this.grbBoxRefreshInvNo);
            this.Controls.Add(this.grpBoxConfigDefDisc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAppAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Configuration ";
            this.Load += new System.EventHandler(this.FormAppAdmin_Load);
            this.grpBoxConfigDefDisc.ResumeLayout(false);
            this.grpBoxConfigDefDisc.PerformLayout();
            this.grbBoxRefreshInvNo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDefaultDisc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpBoxConfigDefDisc;
        private System.Windows.Forms.GroupBox grbBoxRefreshInvNo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnClear;
    }
}