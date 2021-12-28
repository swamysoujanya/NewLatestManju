using AviMa.DataBaseLayer;
using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AviMa.AviMaForms
{
    public partial class frmCustomerAccounts : Form
    {

        public string UserID { get; set; }
        public string AccountType { get; set; }
        DataTable dtAccountDetails = new DataTable();
        LedgerDBLayer objledgerDB = null;
        string erroInfo = "";
        public frmCustomerAccounts(string _userID)
        {
            InitializeComponent();
            UserID = _userID;
        }


        private void CalculateTotal(DataTable dtAccountDetails)
        {

            decimal totalCredi = 0;
            decimal totalDebit = 0;



            #region calculate totalExpences

            foreach (DataRow item in dtAccountDetails.Rows)
            {
                totalCredi += Convert.ToDecimal(item["Credit"]);
                totalDebit += Convert.ToDecimal(item["Debit"]);
            }

            #endregion calculate totalExpences

            #region calculate total sales


            #endregion calculate total sales

            lblExpences.Text = Convert.ToString(totalDebit);
            lblSales.Text = Convert.ToString(totalCredi);
            lblBalance.Text = Convert.ToString(totalDebit - totalCredi);
        }

        DataTable dtPODetails = null;

        private void frmCustomerAccounts_Load(object sender, EventArgs e)
        {
            //AccountType = AviMaConstants.CustAccType;
            ReloadEntireFormData();
        }


        private void ReloadEntireFormData()
        {
            if (AccountType == AviMaConstants.CustAccType)
            {
                lblSearchNaem.Text = "Customer Name";
                this.Text = "Customer Accounts";
                PopulateCustomerCombo();
                AccountName.HeaderText = "Customer Name";
                CuSuID.HeaderText = "Cust ID";
                grpBoxPODetails.Visible = false;
                btnDebitActual.Visible = false;

                btnDebit.Text = "Credit";
            }
            else if (AccountType == AviMaConstants.SupAccType)
            {
                lblSearchNaem.Text = "Supplier Name";
                this.Text = "Supplier Accounts";
                LoadSupplierComboBox();
                AccountName.HeaderText = "Supplier Name";
                CuSuID.HeaderText = "Sup ID";

                btnDebit.Text = "Debit";
            }

            RefreshGrid();
        }


        private void RefreshGrid()
        {
            if (objledgerDB == null)
                objledgerDB = new LedgerDBLayer();
            dtAccountDetails = objledgerDB.GetAllAccounts(AccountType, ref erroInfo);
            dataCustAcc.DataSource = dtAccountDetails;


            CalculateTotal(dtAccountDetails);
        }


        private void RefreshGrid(DataTable dtAccnts)
        {
            dtAccountDetails = dtAccnts;
            dataCustAcc.DataSource = dtAccountDetails;
        }

        DataTable dtCustomerDetails = null;
        public void PopulateCustomerCombo()
        {
            try
            {
                string erroInfo = "";

                if (dtCustomerDetails == null)
                    dtCustomerDetails = new DataTable();

                dtCustomerDetails = MasterCache.GetCustomerDataFrmCache(ref erroInfo);
                if (dtCustomerDetails != null && dtCustomerDetails.Rows.Count > 0)
                {

                    cmbSupplier.DataSource = dtCustomerDetails;
                    cmbSupplier.ValueMember = "CustID";
                    cmbSupplier.DisplayMember = "CustName";


                    //cmbSupplier.Items.Add("-Select-");
                    cmbSupplier.Text = "-Select-";
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        DataTable dtSuppliersTable = null;
        public void LoadSupplierComboBox()
        {

            try
            {
                if (dtSuppliersTable != null)
                    dtSuppliersTable = new DataTable();
                //Bind supplier data to combo box
                string erroInfo = "";

                // dtSuppliersTable = objStockDBLayer.GetSuppliers(ref erroInfo);
                dtSuppliersTable = MasterCache.GetSupplierFrmCache(ref erroInfo);

                if (dtSuppliersTable != null && dtSuppliersTable.Rows.Count > 0)
                {

                    cmbSupplier.DataSource = dtSuppliersTable.Copy();
                    cmbSupplier.ValueMember = "SupID";
                    cmbSupplier.DisplayMember = "SupName";
                    cmbSupplier.Text = "-Select-";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }



        private void btnDebit_Click(object sender, EventArgs e)
        {
            if (objAccountsDO != null && !string.IsNullOrEmpty(objAccountsDO.Name))
            {
                //if (AccountType == AviMaConstants.CustAccType)
                //{
                FormNumberOfPrints objCreditAmnt = new FormNumberOfPrints();
                this.Hide();
                if (AccountType == AviMaConstants.CustAccType)
                {
                    objCreditAmnt.label1.Text = "Credit Amount.";
                    objCreditAmnt.Text = "Credit Customer Account";
                    objCreditAmnt.lblName.Text = "Customer Account: " + objAccountsDO.Name;                    
                    objCreditAmnt.btnContinuePrint.Text = "Credit";
                }
                else
                {
                    objCreditAmnt.label1.Text = "Debit Amount.";
                    objCreditAmnt.Text = "Debit Supplier Account";
                    objCreditAmnt.lblName.Text = "Supplier Account: " + objAccountsDO.Name;
                    objCreditAmnt.btnContinuePrint.Text = "Debit";
                }

                objCreditAmnt.txtNumbOfCopies.MaxLength = 6;
                objCreditAmnt.txtNumbOfCopies.Text = "";
                objCreditAmnt.ShowDialog();

                if (!string.IsNullOrEmpty(objCreditAmnt.txtNumbOfCopies.Text) && objCreditAmnt.txtNumbOfCopies.Text != "0")
                {
                    decimal creditamount = Convert.ToDecimal(objCreditAmnt.txtNumbOfCopies.Text);


                    int SiNo = 0;
                    //decimal PrevDebit = Convert.ToDecimal(objAccountsDO.Debit);
                    //decimal prevCredit = Convert.ToDecimal(objAccountsDO.Credit);
                    SiNo = Convert.ToInt16(objAccountsDO.SiNo);


                    objAccountsDO.CustOrSup = AviMaConstants.CustFlag;
                    objAccountsDO.Credit = Convert.ToString(creditamount);                 
                    objAccountsDO.Debit = "0.00";
                    objAccountsDO.Balance = "0.00";
                    if (AccountType == AviMaConstants.SupAccType)
                    {
                        objAccountsDO.Credit = "0.00";             
                        objAccountsDO.Debit = Convert.ToString(creditamount);
                        objAccountsDO.Balance = "0.00";
                        objAccountsDO.CustOrSup = AviMaConstants.SupFlag;
                    }

                    objAccountsDO.AccountsType = AccountType;
                    //decimal latestCredit = prevCredit + creditamount;
                    //decimal latestBalance = Convert.ToDecimal(objAccountsDO.Debit) - latestCredit;



                    //objAccountsDO.Debit = "0.00";
                    //objAccountsDO.Credit = Convert.ToString(latestCredit);
                    //objAccountsDO.Balance = Convert.ToString(latestBalance);
                    //objAccountsDO.CurrentCredited = creditamount;
                    objAccountsDO.CreatedDateTime = AviMaConstants.CurrentDateTimesStamp;
                    objAccountsDO.CreatedBy = UserID;
                    //   objAccountsDO.CustID = AviMaConstants.CurrentDateTimesStamp;
                    //bool updatAcc = objledgerDB.LedgerAccoount(objAccountsDO, ref erroInfo);
               
                    bool updatAcc = objInvDb.CreateAccount(objAccountsDO, ref erroInfo);
                    RefreshGrid();
                }

                this.Show();

                //}
                //else if (AccountType == AviMaConstants.SupAccType)
                //{
                //    //lblSearchNaem.Text = "Supplier Name";
                //    //this.Text = "Supplier Accounts";
                //    //LoadSupplierComboBox();
                //    //CustName.HeaderText = "Supplier Name";
                //}
            }
            else
            {
                MessageBox.Show("Please select an account from\n above account details to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        AccountsDO objAccountsDO = null;
        private void dataCustAcc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;

                if (i != -1)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(dataCustAcc["SiNo", i].Value)))
                    {
                        objAccountsDO = new AccountsDO();

                        objAccountsDO.SiNo = Convert.ToInt16(dataCustAcc["SiNo", i].Value);
                        objAccountsDO.Balance = Convert.ToString(dataCustAcc["Balance", i].Value);
                        objAccountsDO.Credit = Convert.ToString(dataCustAcc["Credit", i].Value);
                        objAccountsDO.Debit = Convert.ToString(dataCustAcc["Debit", i].Value);
                        objAccountsDO.CustSupID = Convert.ToInt16(dataCustAcc["CuSuID", i].Value);
                        objAccountsDO.Name = Convert.ToString(dataCustAcc["AccountName", i].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string errorInfo = "";

                int customerID = Convert.ToInt16(cmbSupplier.SelectedValue);

                //temp Code
                //DataRow[] _tempdtInvoiceDetails = new DataRow();
                //_tempdtInvoiceDetails = dtInvoiceDetails.Select("where CustID = " + customerID + "  or  (InvCreatedDate BETWEEN  '" + fromDate + "' and   '" + toDate + "')");// );  //= objInvoiceDBLayer.SearchPO(suplierID, fromDate, toDate, ref errorInfo);
                if (AccountType == AviMaConstants.CustAccType)
                {
                    dtAccountDetails = new DataTable();
                    dtAccountDetails = objledgerDB.SearchAccounts(customerID, AviMaConstants.CustAccType, ref errorInfo);

                    if (errorInfo == string.Empty && dtAccountDetails != null && dtAccountDetails.Rows.Count > 0)
                    {
                        RefreshGrid(dtAccountDetails);
                    }
                    else
                    {
                        //dtAccountDetails = new DataTable();
                        //RefreshGrid(dtAccountDetails);
                        MessageBox.Show("No results found for your search criteria. " + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (AccountType == AviMaConstants.SupAccType)
                {
                    dtAccountDetails = new DataTable();
                    dtAccountDetails = objledgerDB.SearchAccounts(customerID, AviMaConstants.SupAccType, ref errorInfo);

                    if (errorInfo == string.Empty && dtAccountDetails != null && dtAccountDetails.Rows.Count > 0)
                    {
                        RefreshGrid(dtAccountDetails);
                    }
                    else
                    {
                        //dtAccountDetails = new DataTable();
                        //RefreshGrid(dtAccountDetails);
                        MessageBox.Show("No results found for your search criteria. " + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbSupplier.Text = "-Select-";
            ReloadEntireFormData();
            //RefreshGrid();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (objAccountsDO != null && !string.IsNullOrEmpty(objAccountsDO.Name))
            {
                if (MessageBox.Show("Delete selected account.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string errorInfo = "";
                    bool _check = objledgerDB.DeleteAccounts(objAccountsDO, AccountType, ref errorInfo);
                    RefreshGrid();
                    if (_check)
                    {
                        MessageBox.Show("User account deleted successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objAccountsDO = new AccountsDO();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an account from\n above account details to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        int RowIndexForReprinting = -1;
        private void dataCustAcc_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    int currentMouseOverRow = dataCustAcc.HitTest(e.X, e.Y).RowIndex;

                    if (currentMouseOverRow != -1)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dataCustAcc["SiNo", currentMouseOverRow].Value)))
                        {
                            ContextMenu m = new ContextMenu();
                            MenuItem objItem = new MenuItem("Show Ledger");
                            objItem.Click += ObjItem_Click;
                            m.MenuItems.Add(objItem);

                            MenuItem objDebit = new MenuItem("Print Ledger");
                            objDebit.Click += ObjPrint_Click;
                            m.MenuItems.Add(objDebit);

                            dataCustAcc.ClearSelection();
                            dataCustAcc.Rows[currentMouseOverRow].Selected = true;

                            if (currentMouseOverRow >= 0)
                            {
                                RowIndexForReprinting = currentMouseOverRow; ;
                            }

                            m.Show(dataCustAcc, new Point(e.X, e.Y));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private void ObjPrint_Click(object sender, EventArgs e)
        {
            try
            {

                LedgerPrintDO objLedgerPrintDO = new LedgerPrintDO();

                objLedgerPrintDO.CustOrSup = AviMaConstants.CustFlag;
                //objLedgerPrintDO.Name = "Customer payments";
                if (AccountType == AviMaConstants.SupAccType)
                {
                    objLedgerPrintDO.CustOrSup = AviMaConstants.SupFlag;
                    //objLedgerPrintDO.Name= "Supplier payments";
                }


                objLedgerPrintDO.Name = Convert.ToString(dataCustAcc["AccountName", RowIndexForReprinting].Value);
                objLedgerPrintDO.ListAccountsDO = objledgerDB.GetCredithistory(Convert.ToInt16(dataCustAcc["CuSuID", RowIndexForReprinting].Value), objLedgerPrintDO.CustOrSup,"", ref erroInfo);

                PrintLedger objPrintLedger = new PrintLedger(objLedgerPrintDO);

                objPrintLedger.PrintReport();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void ObjItem_Click(object sender, EventArgs e)
        {

            try
            {
                char accTypeFlag = AviMaConstants.CustFlag;
                string creditHistoryTitle = "Customer payments";
                if (AccountType == AviMaConstants.SupAccType)
                {
                    accTypeFlag = AviMaConstants.SupFlag;
                    creditHistoryTitle = "Supplier payments";
                }

                DataTable dtHistroy = objledgerDB.GetCredithistory(Convert.ToInt16(dataCustAcc["CuSuID", RowIndexForReprinting].Value), accTypeFlag,"",  ref erroInfo);
                InvoiceItemData objCredHhistory = new InvoiceItemData(dtHistroy);
                objCredHhistory.Text = creditHistoryTitle;

                objCredHhistory.lblCustName.Text = Convert.ToString(dataCustAcc["AccountName", RowIndexForReprinting].Value);
                this.Hide();
                objCredHhistory.ShowDialog();
                this.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void cmbComboPONumbers_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dtrPo = dtPODetails.Select("ponumber = " + cmbComboPONumbers.Text);

                foreach (DataRow item in dtrPo)
                {
                    txtSuplier.Enabled = false;
                    txtPOTotalAmnt.Enabled = false;
                    dtPODate.Enabled = false;
                    txtPOReferenceNo.Enabled = false;

                    txtSuplier.Text = Convert.ToString(item["posupplierID"]);
                    txtPOTotalAmnt.Text = Convert.ToString(item["poTotalAmount"]);
                    dtPODate.Text = Convert.ToString(item["podate"]);
                    txtPOReferenceNo.Text = Convert.ToString(item["poRefernece"]);

                    break;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

       
        InvoiceDBLayer objInvDb = new InvoiceDBLayer();

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupplier.Text != "-Select-")
                {

                    FormNumberOfPrints objCreditAmnt = new FormNumberOfPrints();
                    this.Hide();
                    if (AccountType == AviMaConstants.CustAccType)
                    {                      
                        objCreditAmnt.Text = "Credit Customer Account";
                        objCreditAmnt.lblName.Text = "Customer Account: " + cmbSupplier.Text;                     
                    }
                    else if (AccountType == AviMaConstants.SupAccType)
                    {                        
                        objCreditAmnt.Text = "Credit Supplier Account";
                        objCreditAmnt.lblName.Text = "Supplier Account: " + cmbSupplier.Text;                
                    }
                    objCreditAmnt.btnContinuePrint.Text = "Credit";
                    objCreditAmnt.label1.Text = "Advance Amount.";
                    objCreditAmnt.txtNumbOfCopies.MaxLength = 6;
                    objCreditAmnt.txtNumbOfCopies.Text = "";
                    objCreditAmnt.ShowDialog();

                    if (!string.IsNullOrEmpty(objCreditAmnt.txtNumbOfCopies.Text) && objCreditAmnt.txtNumbOfCopies.Text != "0")
                    {
                        
                        AccountsDO objAccDO = new AccountsDO();

                        //   objAccDO.Balance = "0"; // divide by 10 to reduce value to 1: 10 ratio
                        objAccDO.CreatedBy = UserID;
                        objAccDO.CreatedDateTime = AviMaConstants.CurrentDateTimesStamp;

                        try
                        {
                            objAccDO.CustSupID = Convert.ToInt16(cmbSupplier.SelectedValue);
                        }
                        catch (Exception ex)
                        {

                        }

                        objAccDO.Name = cmbSupplier.Text;
                        objAccDO.CurrentCredited = Convert.ToDecimal(objCreditAmnt.txtNumbOfCopies.Text);
                        objAccDO.Balance = "0.00";
                        objAccDO.Credit = Convert.ToString(Convert.ToDecimal(objCreditAmnt.txtNumbOfCopies.Text));
                        objAccDO.Debit = "0.00";
                        string errorInfo = "";
                        objAccDO.AccountsType = AccountType;



                        objAccDO.CustOrSup = AviMaConstants.CustFlag;
                        if (AccountType == AviMaConstants.SupAccType)
                            objAccDO.CustOrSup = AviMaConstants.SupFlag;


                        bool AccCreat = objInvDb.CreateAccount(objAccDO, ref errorInfo); // create an entry in customer account
                       // AccCreat = objledgerDB.CreateCreditHistory(objAccDO, ref errorInfo); // create an entry in creditHistory account
                        RefreshGrid();
                    }

                    this.Show();
                }
                else
                {
                    if(AccountType == AviMaConstants.CustAccType)
                    MessageBox.Show("Please select a customer from\n the above customer list.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (AccountType == AviMaConstants.SupAccType)
                        MessageBox.Show("Please select a supplier from\n the above supplier list.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + " " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnDebitActual_Click(object sender, EventArgs e)
        {
            if (objAccountsDO != null && !string.IsNullOrEmpty(objAccountsDO.Name))
            {
                //if (AccountType == AviMaConstants.CustAccType)
                //{
                FormNumberOfPrints objCreditAmnt = new FormNumberOfPrints();
                this.Hide();
                objCreditAmnt.label1.Text = "Debit Amount.";
                objCreditAmnt.Text = "Debit Supplier Account";
                objCreditAmnt.lblName.Text = "Supplier Account: " + objAccountsDO.Name;
                objCreditAmnt.txtNumbOfCopies.MaxLength = 6;
                objCreditAmnt.btnContinuePrint.Text = "Debit";
                objCreditAmnt.txtNumbOfCopies.Text = "";
                objCreditAmnt.ShowDialog();

                if (!string.IsNullOrEmpty(objCreditAmnt.txtNumbOfCopies.Text) && objCreditAmnt.txtNumbOfCopies.Text != "0")
                {
                    decimal debit = Convert.ToDecimal(objCreditAmnt.txtNumbOfCopies.Text);


                    int SiNo = 0;
                    decimal PrevDebit = Convert.ToDecimal(objAccountsDO.Debit);
                    decimal prevCredit = Convert.ToDecimal(objAccountsDO.Credit);
                    SiNo = Convert.ToInt16(objAccountsDO.SiNo);

                    decimal latestDebit = PrevDebit + debit;
                    decimal latestCredit = debit - prevCredit ;
                    decimal latestBalance = Convert.ToDecimal(objAccountsDO.Debit) - latestCredit;


                    objAccountsDO.AccountsType = AccountType;
                    objAccountsDO.Debit = Convert.ToString(latestDebit);
                    objAccountsDO.Credit = Convert.ToString(latestCredit);
                    objAccountsDO.Balance = Convert.ToString(latestBalance);
                    objAccountsDO.CurrentDebited = debit;
                    objAccountsDO.CreatedDateTime = AviMaConstants.CurrentDateTimesStamp;
                    //   objAccountsDO.CustID = AviMaConstants.CurrentDateTimesStamp;
                    bool updatAcc = objledgerDB.LedgerAccoount(objAccountsDO, ref erroInfo);
                    RefreshGrid();
                }

                this.Show();

                
            }
            else
            {
                MessageBox.Show("Please select an account from\n above account details to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
