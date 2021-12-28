namespace AviMa.AviMaForms
{
    partial class frmSearchCustomer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridCustomer = new System.Windows.Forms.DataGridView();
            this.CustID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustMobile1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustMobile2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustDist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustTown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCustomer)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridCustomer
            // 
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridCustomer.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCustomer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustID,
            this.CustName,
            this.CustMobile1,
            this.CustMobile2,
            this.CustState,
            this.CustDist,
            this.CustTown});
            this.dataGridCustomer.Location = new System.Drawing.Point(0, 3);
            this.dataGridCustomer.Name = "dataGridCustomer";
            this.dataGridCustomer.ReadOnly = true;
            this.dataGridCustomer.Size = new System.Drawing.Size(350, 367);
            this.dataGridCustomer.TabIndex = 7;
            // 
            // CustID
            // 
            this.CustID.DataPropertyName = "CustID";
            this.CustID.HeaderText = "Customer ID";
            this.CustID.Name = "CustID";
            this.CustID.ReadOnly = true;
            // 
            // CustName
            // 
            this.CustName.DataPropertyName = "CustName";
            this.CustName.HeaderText = "Cust Name";
            this.CustName.Name = "CustName";
            this.CustName.ReadOnly = true;
            // 
            // CustMobile1
            // 
            this.CustMobile1.DataPropertyName = "CustMobile1";
            this.CustMobile1.HeaderText = "CustMobile 1";
            this.CustMobile1.Name = "CustMobile1";
            this.CustMobile1.ReadOnly = true;
            // 
            // CustMobile2
            // 
            this.CustMobile2.DataPropertyName = "CustMobile2";
            this.CustMobile2.HeaderText = "CustMobile 2";
            this.CustMobile2.Name = "CustMobile2";
            this.CustMobile2.ReadOnly = true;
            // 
            // CustState
            // 
            this.CustState.DataPropertyName = "CustState";
            this.CustState.HeaderText = "Cust State";
            this.CustState.Name = "CustState";
            this.CustState.ReadOnly = true;
            // 
            // CustDist
            // 
            this.CustDist.DataPropertyName = "CustDist";
            this.CustDist.HeaderText = "Cust Dist";
            this.CustDist.Name = "CustDist";
            this.CustDist.ReadOnly = true;
            // 
            // CustTown
            // 
            this.CustTown.DataPropertyName = "CustTown";
            this.CustTown.HeaderText = "Cust Town";
            this.CustTown.Name = "CustTown";
            this.CustTown.ReadOnly = true;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(669, 25);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 8;
            this.btnSelect.Text = "Select Customer";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridCustomer);
            this.panel1.Location = new System.Drawing.Point(239, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 385);
            this.panel1.TabIndex = 9;
            // 
            // frmSearchCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 422);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSelect);
            this.Name = "frmSearchCustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Search Result";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCustomer)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridCustomer;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustMobile1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustMobile2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustState;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustDist;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustTown;
        private System.Windows.Forms.Panel panel1;
    }
}