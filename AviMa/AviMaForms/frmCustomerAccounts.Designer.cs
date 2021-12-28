namespace AviMa.AviMaForms
{
    partial class frmCustomerAccounts
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
            this.dataCustAcc = new System.Windows.Forms.DataGridView();
            this.SiNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuSuID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Debit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Credit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSales = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblExpences = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdvance = new System.Windows.Forms.Button();
            this.btnDebit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblSearchNaem = new System.Windows.Forms.Label();
            this.grpBoxPODetails = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPOTotalAmnt = new System.Windows.Forms.TextBox();
            this.txtSuplier = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.cmbComboPONumbers = new System.Windows.Forms.ComboBox();
            this.dtPODate = new System.Windows.Forms.DateTimePicker();
            this.label55 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.txtPOReferenceNo = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.btnDebitActual = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataCustAcc)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpBoxPODetails.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataCustAcc
            // 
            this.dataCustAcc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataCustAcc.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SiNo,
            this.AccountName,
            this.CuSuID,
            this.Debit,
            this.Credit,
            this.Balance});
            this.dataCustAcc.Location = new System.Drawing.Point(-3, 87);
            this.dataCustAcc.Name = "dataCustAcc";
            this.dataCustAcc.Size = new System.Drawing.Size(647, 275);
            this.dataCustAcc.TabIndex = 0;
            this.dataCustAcc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataCustAcc_CellClick);
            this.dataCustAcc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataCustAcc_MouseDown);
            // 
            // SiNo
            // 
            this.SiNo.DataPropertyName = "SiNo";
            this.SiNo.HeaderText = "Si.No";
            this.SiNo.Name = "SiNo";
            this.SiNo.ReadOnly = true;
            // 
            // AccountName
            // 
            this.AccountName.DataPropertyName = "AccountName";
            this.AccountName.HeaderText = "Customer Name";
            this.AccountName.Name = "AccountName";
            this.AccountName.ReadOnly = true;
            // 
            // CuSuID
            // 
            this.CuSuID.DataPropertyName = "ID";
            this.CuSuID.HeaderText = "Cust ID";
            this.CuSuID.Name = "CuSuID";
            this.CuSuID.ReadOnly = true;
            // 
            // Debit
            // 
            this.Debit.DataPropertyName = "Debit";
            this.Debit.HeaderText = "Debit";
            this.Debit.Name = "Debit";
            this.Debit.ReadOnly = true;
            // 
            // Credit
            // 
            this.Credit.DataPropertyName = "Credit";
            this.Credit.HeaderText = "Credit";
            this.Credit.Name = "Credit";
            this.Credit.ReadOnly = true;
            // 
            // Balance
            // 
            this.Balance.DataPropertyName = "Balance";
            this.Balance.HeaderText = "Balance";
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBalance);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblSales);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblExpences);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 438);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(622, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Summary";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.ForeColor = System.Drawing.Color.Blue;
            this.lblBalance.Location = new System.Drawing.Point(386, 58);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(66, 24);
            this.lblBalance.TabIndex = 5;
            this.lblBalance.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(386, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 24);
            this.label5.TabIndex = 4;
            this.label5.Text = "Balance:";
            // 
            // lblSales
            // 
            this.lblSales.AutoSize = true;
            this.lblSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSales.ForeColor = System.Drawing.Color.Red;
            this.lblSales.Location = new System.Drawing.Point(6, 58);
            this.lblSales.Name = "lblSales";
            this.lblSales.Size = new System.Drawing.Size(66, 24);
            this.lblSales.TabIndex = 3;
            this.lblSales.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(195, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total Debits:";
            // 
            // lblExpences
            // 
            this.lblExpences.AutoSize = true;
            this.lblExpences.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpences.ForeColor = System.Drawing.Color.Green;
            this.lblExpences.Location = new System.Drawing.Point(195, 58);
            this.lblExpences.Name = "lblExpences";
            this.lblExpences.Size = new System.Drawing.Size(66, 24);
            this.lblExpences.TabIndex = 1;
            this.lblExpences.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Credits:";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(545, 409);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(99, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete Selected";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdvance
            // 
            this.btnAdvance.Location = new System.Drawing.Point(439, 409);
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Size = new System.Drawing.Size(100, 23);
            this.btnAdvance.TabIndex = 6;
            this.btnAdvance.Text = "Advance";
            this.btnAdvance.UseVisualStyleBackColor = true;
            this.btnAdvance.Click += new System.EventHandler(this.btnAdvance_Click);
            // 
            // btnDebit
            // 
            this.btnDebit.Location = new System.Drawing.Point(358, 409);
            this.btnDebit.Name = "btnDebit";
            this.btnDebit.Size = new System.Drawing.Size(75, 23);
            this.btnDebit.TabIndex = 5;
            this.btnDebit.Text = "Credit";
            this.btnDebit.UseVisualStyleBackColor = true;
            this.btnDebit.Click += new System.EventHandler(this.btnDebit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(572, 53);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 69;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(491, 53);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 64;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier.FormattingEnabled = true;
            this.cmbSupplier.Location = new System.Drawing.Point(127, 21);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(521, 21);
            this.cmbSupplier.TabIndex = 70;
            // 
            // lblSearchNaem
            // 
            this.lblSearchNaem.AutoSize = true;
            this.lblSearchNaem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchNaem.Location = new System.Drawing.Point(8, 25);
            this.lblSearchNaem.Name = "lblSearchNaem";
            this.lblSearchNaem.Size = new System.Drawing.Size(113, 17);
            this.lblSearchNaem.TabIndex = 71;
            this.lblSearchNaem.Text = "Customer Name:";
            // 
            // grpBoxPODetails
            // 
            this.grpBoxPODetails.Controls.Add(this.btnUpdate);
            this.grpBoxPODetails.Controls.Add(this.textBox1);
            this.grpBoxPODetails.Controls.Add(this.label2);
            this.grpBoxPODetails.Controls.Add(this.txtPOTotalAmnt);
            this.grpBoxPODetails.Controls.Add(this.txtSuplier);
            this.grpBoxPODetails.Controls.Add(this.label53);
            this.grpBoxPODetails.Controls.Add(this.cmbComboPONumbers);
            this.grpBoxPODetails.Controls.Add(this.dtPODate);
            this.grpBoxPODetails.Controls.Add(this.label55);
            this.grpBoxPODetails.Controls.Add(this.label54);
            this.grpBoxPODetails.Controls.Add(this.txtPOReferenceNo);
            this.grpBoxPODetails.Controls.Add(this.label52);
            this.grpBoxPODetails.Location = new System.Drawing.Point(650, 87);
            this.grpBoxPODetails.Name = "grpBoxPODetails";
            this.grpBoxPODetails.Size = new System.Drawing.Size(219, 246);
            this.grpBoxPODetails.TabIndex = 72;
            this.grpBoxPODetails.TabStop = false;
            this.grpBoxPODetails.Text = "PO Details";
            this.grpBoxPODetails.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(135, 203);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 73;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(86, 177);
            this.textBox1.MaxLength = 45;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(124, 20);
            this.textBox1.TabIndex = 83;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 82;
            this.label2.Text = "Payable:";
            // 
            // txtPOTotalAmnt
            // 
            this.txtPOTotalAmnt.Location = new System.Drawing.Point(90, 142);
            this.txtPOTotalAmnt.MaxLength = 45;
            this.txtPOTotalAmnt.Name = "txtPOTotalAmnt";
            this.txtPOTotalAmnt.ReadOnly = true;
            this.txtPOTotalAmnt.Size = new System.Drawing.Size(82, 20);
            this.txtPOTotalAmnt.TabIndex = 77;
            // 
            // txtSuplier
            // 
            this.txtSuplier.Location = new System.Drawing.Point(86, 59);
            this.txtSuplier.MaxLength = 45;
            this.txtSuplier.Name = "txtSuplier";
            this.txtSuplier.ReadOnly = true;
            this.txtSuplier.Size = new System.Drawing.Size(124, 20);
            this.txtSuplier.TabIndex = 81;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label53.Location = new System.Drawing.Point(18, 143);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(68, 17);
            this.label53.TabIndex = 80;
            this.label53.Text = "P.O.Amnt";
            // 
            // cmbComboPONumbers
            // 
            this.cmbComboPONumbers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComboPONumbers.FormattingEnabled = true;
            this.cmbComboPONumbers.Location = new System.Drawing.Point(19, 19);
            this.cmbComboPONumbers.Name = "cmbComboPONumbers";
            this.cmbComboPONumbers.Size = new System.Drawing.Size(56, 21);
            this.cmbComboPONumbers.TabIndex = 57;
            this.cmbComboPONumbers.SelectedValueChanged += new System.EventHandler(this.cmbComboPONumbers_SelectedValueChanged);
            // 
            // dtPODate
            // 
            this.dtPODate.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtPODate.CustomFormat = "";
            this.dtPODate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPODate.Location = new System.Drawing.Point(90, 112);
            this.dtPODate.Name = "dtPODate";
            this.dtPODate.Size = new System.Drawing.Size(82, 20);
            this.dtPODate.TabIndex = 74;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(16, 59);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(64, 17);
            this.label55.TabIndex = 78;
            this.label55.Text = "Supplier:";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label54.Location = new System.Drawing.Point(18, 113);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(66, 17);
            this.label54.TabIndex = 75;
            this.label54.Text = "PO Date:";
            // 
            // txtPOReferenceNo
            // 
            this.txtPOReferenceNo.Location = new System.Drawing.Point(86, 84);
            this.txtPOReferenceNo.MaxLength = 45;
            this.txtPOReferenceNo.Name = "txtPOReferenceNo";
            this.txtPOReferenceNo.ReadOnly = true;
            this.txtPOReferenceNo.Size = new System.Drawing.Size(124, 20);
            this.txtPOReferenceNo.TabIndex = 76;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label52.Location = new System.Drawing.Point(16, 85);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(64, 17);
            this.label52.TabIndex = 79;
            this.label52.Text = "Ref. No.:";
            // 
            // btnDebitActual
            // 
            this.btnDebitActual.Location = new System.Drawing.Point(244, 409);
            this.btnDebitActual.Name = "btnDebitActual";
            this.btnDebitActual.Size = new System.Drawing.Size(108, 23);
            this.btnDebitActual.TabIndex = 73;
            this.btnDebitActual.Text = "Credit Supplier";
            this.btnDebitActual.UseVisualStyleBackColor = true;
            this.btnDebitActual.Click += new System.EventHandler(this.btnAdvance_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Orange;
            this.groupBox2.Controls.Add(this.lblSearchNaem);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.cmbSupplier);
            this.groupBox2.Location = new System.Drawing.Point(-3, -2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(658, 83);
            this.groupBox2.TabIndex = 74;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Customer Accounts";
            // 
            // frmCustomerAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 545);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnDebitActual);
            this.Controls.Add(this.grpBoxPODetails);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdvance);
            this.Controls.Add(this.btnDebit);
            this.Controls.Add(this.dataCustAcc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCustomerAccounts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accounts";
            this.Load += new System.EventHandler(this.frmCustomerAccounts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataCustAcc)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpBoxPODetails.ResumeLayout(false);
            this.grpBoxPODetails.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataCustAcc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSales;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblExpences;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdvance;
        private System.Windows.Forms.Button btnDebit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblSearchNaem;
        private System.Windows.Forms.GroupBox grpBoxPODetails;
        private System.Windows.Forms.ComboBox cmbComboPONumbers;
        public System.Windows.Forms.TextBox txtPOTotalAmnt;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.DateTimePicker dtPODate;
        private System.Windows.Forms.Label label55;
        public System.Windows.Forms.TextBox txtPOReferenceNo;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label54;
        public System.Windows.Forms.TextBox txtSuplier;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDebitActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn SiNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CuSuID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Debit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Credit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}