namespace AviMa.AviMaForms
{
    partial class formLedgerItem
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
            this.lblSiNo = new System.Windows.Forms.Label();
            this.txtBoxSiNo = new System.Windows.Forms.TextBox();
            this.dateLedger = new System.Windows.Forms.DateTimePicker();
            this.Account = new System.Windows.Forms.Label();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPaidTo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancelItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.txtTaxPaid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddAccntType = new System.Windows.Forms.Button();
            this.btnRemoveAccountType = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSiNo
            // 
            this.lblSiNo.AutoSize = true;
            this.lblSiNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblSiNo.Location = new System.Drawing.Point(46, 32);
            this.lblSiNo.Name = "lblSiNo";
            this.lblSiNo.Size = new System.Drawing.Size(46, 17);
            this.lblSiNo.TabIndex = 0;
            this.lblSiNo.Text = "Si.No:";
            // 
            // txtBoxSiNo
            // 
            this.txtBoxSiNo.Enabled = false;
            this.txtBoxSiNo.Location = new System.Drawing.Point(104, 29);
            this.txtBoxSiNo.Name = "txtBoxSiNo";
            this.txtBoxSiNo.ReadOnly = true;
            this.txtBoxSiNo.Size = new System.Drawing.Size(100, 20);
            this.txtBoxSiNo.TabIndex = 0;
            // 
            // dateLedger
            // 
            this.dateLedger.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateLedger.CustomFormat = "";
            this.dateLedger.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateLedger.Location = new System.Drawing.Point(104, 56);
            this.dateLedger.Name = "dateLedger";
            this.dateLedger.Size = new System.Drawing.Size(95, 20);
            this.dateLedger.TabIndex = 1;
            // 
            // Account
            // 
            this.Account.AutoSize = true;
            this.Account.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Account.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Account.Location = new System.Drawing.Point(29, 88);
            this.Account.Name = "Account";
            this.Account.Size = new System.Drawing.Size(63, 17);
            this.Account.TabIndex = 63;
            this.Account.Text = "Account:";
            // 
            // cmbAccount
            // 
            this.cmbAccount.FormattingEnabled = true;
            this.cmbAccount.Location = new System.Drawing.Point(103, 87);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(186, 21);
            this.cmbAccount.TabIndex = 2;
            this.cmbAccount.Text = "-Select-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(50, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 66;
            this.label3.Text = "Date:";
            // 
            // txtPaidTo
            // 
            this.txtPaidTo.Location = new System.Drawing.Point(103, 114);
            this.txtPaidTo.Name = "txtPaidTo";
            this.txtPaidTo.Size = new System.Drawing.Size(186, 20);
            this.txtPaidTo.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(28, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 17);
            this.label5.TabIndex = 69;
            this.label5.Text = "Paid To:";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(104, 148);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(185, 20);
            this.txtAmount.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(31, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 71;
            this.label6.Text = "Amount:";
            // 
            // btnCancelItem
            // 
            this.btnCancelItem.Location = new System.Drawing.Point(214, 339);
            this.btnCancelItem.Name = "btnCancelItem";
            this.btnCancelItem.Size = new System.Drawing.Size(75, 23);
            this.btnCancelItem.TabIndex = 7;
            this.btnCancelItem.Text = "Cancel";
            this.btnCancelItem.UseVisualStyleBackColor = true;
            this.btnCancelItem.Click += new System.EventHandler(this.btnCancelItem_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(129, 339);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.TabIndex = 6;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // txtTaxPaid
            // 
            this.txtTaxPaid.Location = new System.Drawing.Point(103, 178);
            this.txtTaxPaid.Name = "txtTaxPaid";
            this.txtTaxPaid.Size = new System.Drawing.Size(185, 20);
            this.txtTaxPaid.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(24, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 17);
            this.label1.TabIndex = 75;
            this.label1.Text = "Tax Paid:";
            // 
            // btnAddAccntType
            // 
            this.btnAddAccntType.Location = new System.Drawing.Point(295, 87);
            this.btnAddAccntType.Name = "btnAddAccntType";
            this.btnAddAccntType.Size = new System.Drawing.Size(36, 23);
            this.btnAddAccntType.TabIndex = 77;
            this.btnAddAccntType.Text = "+";
            this.btnAddAccntType.UseVisualStyleBackColor = true;
            this.btnAddAccntType.Click += new System.EventHandler(this.btnAddAccntType_Click);
            // 
            // btnRemoveAccountType
            // 
            this.btnRemoveAccountType.Location = new System.Drawing.Point(337, 85);
            this.btnRemoveAccountType.Name = "btnRemoveAccountType";
            this.btnRemoveAccountType.Size = new System.Drawing.Size(36, 23);
            this.btnRemoveAccountType.TabIndex = 78;
            this.btnRemoveAccountType.Text = "-";
            this.btnRemoveAccountType.UseVisualStyleBackColor = true;
            this.btnRemoveAccountType.Click += new System.EventHandler(this.btnRemoveAccountType_Click);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(104, 218);
            this.txtNote.MaxLength = 300;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(185, 90);
            this.txtNote.TabIndex = 79;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(46, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 80;
            this.label2.Text = "Note:";
            // 
            // formLedgerItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 385);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRemoveAccountType);
            this.Controls.Add(this.btnAddAccntType);
            this.Controls.Add(this.txtTaxPaid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.btnCancelItem);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPaidTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbAccount);
            this.Controls.Add(this.dateLedger);
            this.Controls.Add(this.Account);
            this.Controls.Add(this.txtBoxSiNo);
            this.Controls.Add(this.lblSiNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formLedgerItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ledger Item";
            this.Load += new System.EventHandler(this.formLedgerItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSiNo;
        private System.Windows.Forms.Label Account;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancelItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddAccntType;
        private System.Windows.Forms.Button btnRemoveAccountType;
        public System.Windows.Forms.DateTimePicker dateLedger;
        public System.Windows.Forms.ComboBox cmbAccount;
        public System.Windows.Forms.TextBox txtPaidTo;
        public System.Windows.Forms.TextBox txtAmount;
        public System.Windows.Forms.TextBox txtTaxPaid;
        public System.Windows.Forms.TextBox txtBoxSiNo;
        public System.Windows.Forms.Button btnAddItem;
        public System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label2;
    }
}