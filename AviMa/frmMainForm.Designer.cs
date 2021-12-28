namespace AviMa
{
    partial class frmMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainForm));
            this.mnStrpMainForm = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productManagementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaseEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockReturnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reGenarateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dailySalesReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purchaceOrderReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ledgerReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintananceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.repairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supplierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ledgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expencesLedgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supplierAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMainPanel = new System.Windows.Forms.Panel();
            this.lblUserLoggedIN = new System.Windows.Forms.Label();
            this.lblLoggedInDateTime = new System.Windows.Forms.Label();
            this.mnStrpMainForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnStrpMainForm
            // 
            this.mnStrpMainForm.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.mnStrpMainForm.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.mnStrpMainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.productManagementToolStripMenuItem,
            this.billingToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.maintananceToolStripMenuItem,
            this.configurationToolStripMenuItem,
            this.accountsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnStrpMainForm.Location = new System.Drawing.Point(0, 0);
            this.mnStrpMainForm.Name = "mnStrpMainForm";
            this.mnStrpMainForm.Size = new System.Drawing.Size(1276, 27);
            this.mnStrpMainForm.TabIndex = 0;
            this.mnStrpMainForm.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // productManagementToolStripMenuItem
            // 
            this.productManagementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.purchaseEntryToolStripMenuItem,
            this.stockReturnToolStripMenuItem,
            this.reGenarateToolStripMenuItem});
            this.productManagementToolStripMenuItem.Name = "productManagementToolStripMenuItem";
            this.productManagementToolStripMenuItem.Size = new System.Drawing.Size(57, 23);
            this.productManagementToolStripMenuItem.Text = "Stock";
            this.productManagementToolStripMenuItem.Click += new System.EventHandler(this.productManagementToolStripMenuItem_Click);
            // 
            // purchaseEntryToolStripMenuItem
            // 
            this.purchaseEntryToolStripMenuItem.Name = "purchaseEntryToolStripMenuItem";
            this.purchaseEntryToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.purchaseEntryToolStripMenuItem.Text = "Purchase Entry";
            this.purchaseEntryToolStripMenuItem.Click += new System.EventHandler(this.purchaseEntryToolStripMenuItem_Click);
            // 
            // stockReturnToolStripMenuItem
            // 
            this.stockReturnToolStripMenuItem.Name = "stockReturnToolStripMenuItem";
            this.stockReturnToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.stockReturnToolStripMenuItem.Text = "Stock Return";
            this.stockReturnToolStripMenuItem.Click += new System.EventHandler(this.stockReturnToolStripMenuItem_Click);
            // 
            // reGenarateToolStripMenuItem
            // 
            this.reGenarateToolStripMenuItem.Name = "reGenarateToolStripMenuItem";
            this.reGenarateToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.reGenarateToolStripMenuItem.Text = "Re-Generate Barcode ";
            this.reGenarateToolStripMenuItem.Click += new System.EventHandler(this.reGenarateToolStripMenuItem_Click);
            // 
            // billingToolStripMenuItem
            // 
            this.billingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.invoiceToolStripMenuItem,
            this.showInvoiceToolStripMenuItem});
            this.billingToolStripMenuItem.Name = "billingToolStripMenuItem";
            this.billingToolStripMenuItem.Size = new System.Drawing.Size(57, 23);
            this.billingToolStripMenuItem.Text = "Billing";
            this.billingToolStripMenuItem.Click += new System.EventHandler(this.billingToolStripMenuItem_Click);
            // 
            // invoiceToolStripMenuItem
            // 
            this.invoiceToolStripMenuItem.Name = "invoiceToolStripMenuItem";
            this.invoiceToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.invoiceToolStripMenuItem.Text = "Invoice";
            this.invoiceToolStripMenuItem.Click += new System.EventHandler(this.invoiceToolStripMenuItem_Click);
            // 
            // showInvoiceToolStripMenuItem
            // 
            this.showInvoiceToolStripMenuItem.Name = "showInvoiceToolStripMenuItem";
            this.showInvoiceToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.showInvoiceToolStripMenuItem.Text = "Show Invoice";
            this.showInvoiceToolStripMenuItem.Click += new System.EventHandler(this.showInvoiceToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stockReportToolStripMenuItem,
            this.dailySalesReportToolStripMenuItem,
            this.purchaceOrderReportToolStripMenuItem,
            this.ledgerReportToolStripMenuItem});
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(63, 23);
            this.reportToolStripMenuItem.Text = "Report";
            // 
            // stockReportToolStripMenuItem
            // 
            this.stockReportToolStripMenuItem.Name = "stockReportToolStripMenuItem";
            this.stockReportToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.stockReportToolStripMenuItem.Text = "Stock Report";
            this.stockReportToolStripMenuItem.Click += new System.EventHandler(this.stockReportToolStripMenuItem_Click);
            // 
            // dailySalesReportToolStripMenuItem
            // 
            this.dailySalesReportToolStripMenuItem.Name = "dailySalesReportToolStripMenuItem";
            this.dailySalesReportToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.dailySalesReportToolStripMenuItem.Text = "Daily Sales Report";
            this.dailySalesReportToolStripMenuItem.Click += new System.EventHandler(this.dailySalesReportToolStripMenuItem_Click);
            // 
            // purchaceOrderReportToolStripMenuItem
            // 
            this.purchaceOrderReportToolStripMenuItem.Name = "purchaceOrderReportToolStripMenuItem";
            this.purchaceOrderReportToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.purchaceOrderReportToolStripMenuItem.Text = "Purchace Order Report";
            this.purchaceOrderReportToolStripMenuItem.Click += new System.EventHandler(this.purchaceOrderReportToolStripMenuItem_Click);
            // 
            // ledgerReportToolStripMenuItem
            // 
            this.ledgerReportToolStripMenuItem.Name = "ledgerReportToolStripMenuItem";
            this.ledgerReportToolStripMenuItem.Size = new System.Drawing.Size(221, 24);
            this.ledgerReportToolStripMenuItem.Text = "Ledger Report";
            this.ledgerReportToolStripMenuItem.Click += new System.EventHandler(this.ledgerReportToolStripMenuItem_Click);
            // 
            // maintananceToolStripMenuItem
            // 
            this.maintananceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.repairToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.maintananceToolStripMenuItem.Name = "maintananceToolStripMenuItem";
            this.maintananceToolStripMenuItem.Size = new System.Drawing.Size(102, 23);
            this.maintananceToolStripMenuItem.Text = "Maintenance ";
            // 
            // repairToolStripMenuItem
            // 
            this.repairToolStripMenuItem.Name = "repairToolStripMenuItem";
            this.repairToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.repairToolStripMenuItem.Text = "Repair";
            this.repairToolStripMenuItem.Click += new System.EventHandler(this.repairToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(157, 24);
            this.viewToolStripMenuItem.Text = "Show Repair";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem,
            this.userConfigurationToolStripMenuItem,
            this.customerToolStripMenuItem,
            this.supplierToolStripMenuItem,
            this.applicationConfigurationToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(108, 23);
            this.configurationToolStripMenuItem.Text = "Administartion";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(232, 24);
            this.userToolStripMenuItem.Text = "User/Employee";
            this.userToolStripMenuItem.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
            // 
            // userConfigurationToolStripMenuItem
            // 
            this.userConfigurationToolStripMenuItem.Name = "userConfigurationToolStripMenuItem";
            this.userConfigurationToolStripMenuItem.Size = new System.Drawing.Size(232, 24);
            this.userConfigurationToolStripMenuItem.Text = "User Permission (ACL)";
            this.userConfigurationToolStripMenuItem.Click += new System.EventHandler(this.userConfigurationToolStripMenuItem_Click);
            // 
            // customerToolStripMenuItem
            // 
            this.customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            this.customerToolStripMenuItem.Size = new System.Drawing.Size(232, 24);
            this.customerToolStripMenuItem.Text = "Customer";
            this.customerToolStripMenuItem.Click += new System.EventHandler(this.customerToolStripMenuItem_Click);
            // 
            // supplierToolStripMenuItem
            // 
            this.supplierToolStripMenuItem.Name = "supplierToolStripMenuItem";
            this.supplierToolStripMenuItem.Size = new System.Drawing.Size(232, 24);
            this.supplierToolStripMenuItem.Text = "Supplier";
            this.supplierToolStripMenuItem.Click += new System.EventHandler(this.supplierToolStripMenuItem_Click);
            // 
            // applicationConfigurationToolStripMenuItem
            // 
            this.applicationConfigurationToolStripMenuItem.Name = "applicationConfigurationToolStripMenuItem";
            this.applicationConfigurationToolStripMenuItem.Size = new System.Drawing.Size(232, 24);
            this.applicationConfigurationToolStripMenuItem.Text = "Application Configuration";
            this.applicationConfigurationToolStripMenuItem.Click += new System.EventHandler(this.applicationConfigurationToolStripMenuItem_Click);
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ledgerToolStripMenuItem,
            this.expencesLedgerToolStripMenuItem,
            this.customerAccountsToolStripMenuItem,
            this.supplierAccountsToolStripMenuItem});
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(78, 23);
            this.accountsToolStripMenuItem.Text = "Accounts";
            // 
            // ledgerToolStripMenuItem
            // 
            this.ledgerToolStripMenuItem.Name = "ledgerToolStripMenuItem";
            this.ledgerToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.ledgerToolStripMenuItem.Text = "Ledger Summary";
            this.ledgerToolStripMenuItem.Click += new System.EventHandler(this.ledgerToolStripMenuItem_Click);
            // 
            // expencesLedgerToolStripMenuItem
            // 
            this.expencesLedgerToolStripMenuItem.Name = "expencesLedgerToolStripMenuItem";
            this.expencesLedgerToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.expencesLedgerToolStripMenuItem.Text = "Expences";
            this.expencesLedgerToolStripMenuItem.Click += new System.EventHandler(this.expencesLedgerToolStripMenuItem_Click);
            // 
            // customerAccountsToolStripMenuItem
            // 
            this.customerAccountsToolStripMenuItem.Name = "customerAccountsToolStripMenuItem";
            this.customerAccountsToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.customerAccountsToolStripMenuItem.Text = "Customer Accounts";
            this.customerAccountsToolStripMenuItem.Click += new System.EventHandler(this.customerAccountsToolStripMenuItem_Click);
            // 
            // supplierAccountsToolStripMenuItem
            // 
            this.supplierAccountsToolStripMenuItem.Name = "supplierAccountsToolStripMenuItem";
            this.supplierAccountsToolStripMenuItem.Size = new System.Drawing.Size(197, 24);
            this.supplierAccountsToolStripMenuItem.Text = "Supplier Accounts";
            this.supplierAccountsToolStripMenuItem.Click += new System.EventHandler(this.supplierAccountsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(50, 23);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // pnlMainPanel
            // 
            this.pnlMainPanel.AutoScroll = true;
            this.pnlMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainPanel.Location = new System.Drawing.Point(0, 27);
            this.pnlMainPanel.Name = "pnlMainPanel";
            this.pnlMainPanel.Size = new System.Drawing.Size(1276, 706);
            this.pnlMainPanel.TabIndex = 1;
            // 
            // lblUserLoggedIN
            // 
            this.lblUserLoggedIN.AutoSize = true;
            this.lblUserLoggedIN.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblUserLoggedIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserLoggedIN.ForeColor = System.Drawing.Color.Black;
            this.lblUserLoggedIN.Location = new System.Drawing.Point(855, 7);
            this.lblUserLoggedIN.Name = "lblUserLoggedIN";
            this.lblUserLoggedIN.Size = new System.Drawing.Size(52, 17);
            this.lblUserLoggedIN.TabIndex = 2;
            this.lblUserLoggedIN.Text = "label1";
            // 
            // lblLoggedInDateTime
            // 
            this.lblLoggedInDateTime.AutoSize = true;
            this.lblLoggedInDateTime.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblLoggedInDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoggedInDateTime.ForeColor = System.Drawing.Color.Black;
            this.lblLoggedInDateTime.Location = new System.Drawing.Point(1024, 7);
            this.lblLoggedInDateTime.Name = "lblLoggedInDateTime";
            this.lblLoggedInDateTime.Size = new System.Drawing.Size(52, 17);
            this.lblLoggedInDateTime.TabIndex = 3;
            this.lblLoggedInDateTime.Text = "label1";
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1276, 733);
            this.Controls.Add(this.lblLoggedInDateTime);
            this.Controls.Add(this.lblUserLoggedIN);
            this.Controls.Add(this.pnlMainPanel);
            this.Controls.Add(this.mnStrpMainForm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnStrpMainForm;
            this.Name = "frmMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Avi Ma - Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMainForm_Load);
            this.mnStrpMainForm.ResumeLayout(false);
            this.mnStrpMainForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnStrpMainForm;
        private System.Windows.Forms.Panel pnlMainPanel;
        public System.Windows.Forms.Label lblUserLoggedIN;
        public System.Windows.Forms.Label lblLoggedInDateTime;
        public System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem productManagementToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem billingToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem stockReportToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem dailySalesReportToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem invoiceToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem maintananceToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem repairToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem showInvoiceToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem customerToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem supplierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purchaceOrderReportToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem purchaseEntryToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem stockReturnToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem reGenarateToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem userConfigurationToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem expencesLedgerToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem customerAccountsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem supplierAccountsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem applicationConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ledgerReportToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ledgerToolStripMenuItem;
    }
}

