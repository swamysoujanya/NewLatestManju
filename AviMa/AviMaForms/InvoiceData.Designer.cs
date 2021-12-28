namespace AviMa.AviMaForms
{
    partial class InvoiceData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InvoiceData));
            this.dataGridInvDetails = new System.Windows.Forms.DataGridView();
            this.grpBoxSearchItem = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cmbSaleType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dateToSearch = new System.Windows.Forms.DateTimePicker();
            this.dateFromSearch = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchInvNo = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.RePrintInv = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInvDetails)).BeginInit();
            this.grpBoxSearchItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridInvDetails
            // 
            this.dataGridInvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridInvDetails.Location = new System.Drawing.Point(8, 110);
            this.dataGridInvDetails.Name = "dataGridInvDetails";
            this.dataGridInvDetails.ReadOnly = true;
            this.dataGridInvDetails.Size = new System.Drawing.Size(1212, 425);
            this.dataGridInvDetails.TabIndex = 5;
            this.dataGridInvDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridInvDetails_CellClick);
            this.dataGridInvDetails.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridInvDetails_MouseDown);
            // 
            // grpBoxSearchItem
            // 
            this.grpBoxSearchItem.BackColor = System.Drawing.Color.SandyBrown;
            this.grpBoxSearchItem.Controls.Add(this.btnDelete);
            this.grpBoxSearchItem.Controls.Add(this.cmbSaleType);
            this.grpBoxSearchItem.Controls.Add(this.label3);
            this.grpBoxSearchItem.Controls.Add(this.btnClear);
            this.grpBoxSearchItem.Controls.Add(this.btnSearch);
            this.grpBoxSearchItem.Controls.Add(this.dateToSearch);
            this.grpBoxSearchItem.Controls.Add(this.dateFromSearch);
            this.grpBoxSearchItem.Controls.Add(this.label2);
            this.grpBoxSearchItem.Controls.Add(this.label1);
            this.grpBoxSearchItem.Controls.Add(this.txtSearchInvNo);
            this.grpBoxSearchItem.Controls.Add(this.label26);
            this.grpBoxSearchItem.ForeColor = System.Drawing.SystemColors.MenuText;
            this.grpBoxSearchItem.Location = new System.Drawing.Point(8, 17);
            this.grpBoxSearchItem.Name = "grpBoxSearchItem";
            this.grpBoxSearchItem.Size = new System.Drawing.Size(1201, 87);
            this.grpBoxSearchItem.TabIndex = 6;
            this.grpBoxSearchItem.TabStop = false;
            this.grpBoxSearchItem.Text = "Search Invoice";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(688, 22);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(82, 23);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete All";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // cmbSaleType
            // 
            this.cmbSaleType.FormattingEnabled = true;
            this.cmbSaleType.Items.AddRange(new object[] {
            "-Select-",
            "WholeSaleCash",
            "WholeSaleCredit",
            "Retail"});
            this.cmbSaleType.Location = new System.Drawing.Point(83, 60);
            this.cmbSaleType.Name = "cmbSaleType";
            this.cmbSaleType.Size = new System.Drawing.Size(116, 21);
            this.cmbSaleType.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 31;
            this.label3.Text = "Sale Type:";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClear.Location = new System.Drawing.Point(600, 51);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(82, 24);
            this.btnClear.TabIndex = 30;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearch.Location = new System.Drawing.Point(600, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 25);
            this.btnSearch.TabIndex = 29;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dateToSearch
            // 
            this.dateToSearch.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateToSearch.Location = new System.Drawing.Point(467, 34);
            this.dateToSearch.Name = "dateToSearch";
            this.dateToSearch.Size = new System.Drawing.Size(114, 20);
            this.dateToSearch.TabIndex = 28;
            // 
            // dateFromSearch
            // 
            this.dateFromSearch.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFromSearch.Location = new System.Drawing.Point(278, 31);
            this.dateFromSearch.Name = "dateFromSearch";
            this.dateFromSearch.Size = new System.Drawing.Size(114, 20);
            this.dateFromSearch.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(398, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 17);
            this.label2.TabIndex = 26;
            this.label2.Text = "To Date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(205, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "From Date:";
            // 
            // txtSearchInvNo
            // 
            this.txtSearchInvNo.Location = new System.Drawing.Point(83, 31);
            this.txtSearchInvNo.Name = "txtSearchInvNo";
            this.txtSearchInvNo.Size = new System.Drawing.Size(116, 20);
            this.txtSearchInvNo.TabIndex = 22;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label26.Location = new System.Drawing.Point(6, 34);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(82, 17);
            this.label26.TabIndex = 24;
            this.label26.Text = "Invoice No.:";
            // 
            // RePrintInv
            // 
            this.RePrintInv.Name = "contextMenuStrip1";
            this.RePrintInv.Size = new System.Drawing.Size(61, 4);
            this.RePrintInv.Text = "Re-Print Invoice";
            // 
            // InvoiceData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 551);
            this.Controls.Add(this.grpBoxSearchItem);
            this.Controls.Add(this.dataGridInvDetails);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InvoiceData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Details";
            this.Load += new System.EventHandler(this.InvoiceData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridInvDetails)).EndInit();
            this.grpBoxSearchItem.ResumeLayout(false);
            this.grpBoxSearchItem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridInvDetails;
        private System.Windows.Forms.GroupBox grpBoxSearchItem;
        public System.Windows.Forms.TextBox txtSearchInvNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSaleType;
        private System.Windows.Forms.ContextMenuStrip RePrintInv;
        public System.Windows.Forms.Label label26;
        public System.Windows.Forms.DateTimePicker dateFromSearch;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.DateTimePicker dateToSearch;
        public System.Windows.Forms.Button btnClear;
        public System.Windows.Forms.Button btnSearch;
        public System.Windows.Forms.Button btnDelete;
    }
}