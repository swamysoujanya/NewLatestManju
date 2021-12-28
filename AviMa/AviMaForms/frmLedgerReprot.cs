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
    public partial class frmLedgerReprot : Form
    {

        public string UserID { get; set; }

        public frmLedgerReprot(string userID)
        {
            InitializeComponent();

            UserID = userID;
        }

        private void frmLedgerReprot_Load(object sender, EventArgs e)
        {
            // when ever ui re appers reset the date to todays date

            dateOpeningBalDate.ResetText();

            LoadForm();
        }

        private void btnBalanceDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtCustomerAccounts != null && dtCustomerAccounts.Rows.Count > 0)
                {
                    frmDisplayDetailsInGrid objfrmDisplayDetailsInGrid = new frmDisplayDetailsInGrid(dtCustomerAccounts, "");
                    this.Hide();
                    objfrmDisplayDetailsInGrid.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("No accounts to show", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }
        LedgerDBLayer objLedgerDBLayer = null;
        LedgerRptDBLayer objLedgerRptDBLayer = null;
        InvoiceDBLayer objInvoiceDBLayer = null;
        double totalCashSales = 0;
      //  double totalCreditSales = 0;
        double totalExpense = 0;
        double totalBalancePiadToSup = 0;
        double balanceRecived = 0; //balance recieved from the customer
        string erroInfo = "";

        double openingBalance = 0;

        int openingBalanaceSINO = 0;

        DataTable dtCustomerAccounts = null;
        DataTable dtSupAccount = null;

        public void LoadForm()
        {

            #region allow user to edit opening balance for today's and tommorows not yesterdays and other day's

           

            if (dateOpeningBalDate.Text != DateTime.Now.ToShortDateString())
            {
                txtOpeningBalance.Enabled = false;
                btnRefreshLegerDetails.Enabled = false;
            }
            else if(dateOpeningBalDate.Text == DateTime.Now.ToShortDateString())
            {
                txtOpeningBalance.Enabled = true;
                btnRefreshLegerDetails.Enabled = true;
            }

            #endregion allow user to edit opening balance for today's and tommorows not yesterdays and other day's

            
            dtCustomerAccounts = new DataTable();
            dtSupAccount = new DataTable();

            try
            {
                ClearAllControls();

                string rptDate = Convert.ToDateTime(dateOpeningBalDate.Text).ToString("yyyy-MM-dd");


                if (objInvoiceDBLayer == null)
                    objInvoiceDBLayer = new InvoiceDBLayer();

                //DataTable dtAllInvoices = new DataTable();
                //dtAllInvoices = objInvoiceDBLayer.SearchInvoice(0, date, date, "",AviMaConstants.WholeSaleCredit, false, AviMaConstants.InvoiceFlag, ref erroInfo);

                //DataTable dtCreditgInvoices = new DataTable();
                //dtCreditgInvoices = objInvoiceDBLayer.SearchInvoice(0, rptDate, rptDate, "", AviMaConstants.WholeSaleCredit, false, AviMaConstants.LedgerFlag, ref erroInfo);

                DataTable dtCash = new DataTable();
                dtCash = objInvoiceDBLayer.SearchInvoice(0, rptDate, rptDate, "", AviMaConstants.WholeSaleCash, false, AviMaConstants.LedgerFlag, ref erroInfo);

                DataTable dtRetail = new DataTable();
                dtRetail = objInvoiceDBLayer.SearchInvoice(0, rptDate, rptDate, "", AviMaConstants.Retail, false, AviMaConstants.LedgerFlag, ref erroInfo);

                //if (dtAllInvoices != null && dtAllInvoices.Rows.Count > 0)
                //{
                #region Cash sales

                //if (objLedgerDBLayer == null)
                //    objLedgerDBLayer = new LedgerDBLayer();

                //  DataRow[] dtResultsCashSales = dtAllInvoices.Select("InvSaleType = '" + AviMaConstants.WholeSaleCash + "' or InvSaleType = '" + AviMaConstants.Retail + "'");

                foreach (DataRow itemCash in dtRetail.Rows)
                {
                    if (Convert.ToString(itemCash["InvGrandTotalAmount"]) != "" && Convert.ToString(itemCash["InvGrandTotalAmount"]) != "0")
                        totalCashSales += Convert.ToDouble(itemCash["InvGrandTotalAmount"]);
                }

                foreach (DataRow itemCash in dtCash.Rows)
                {
                    if (Convert.ToString(itemCash["InvGrandTotalAmount"]) != "" && Convert.ToString(itemCash["InvGrandTotalAmount"]) != "0")
                        totalCashSales += Convert.ToDouble(itemCash["InvGrandTotalAmount"]);
                }

                // dataGridExpenseDetails.DataSource = dtResultsCashSales;

                #endregion  Cash sales

                #region Credit sales
                // DataRow[] dtResultsCreditSales = dtAllInvoices.Select("InvSaleType = '" + AviMaConstants.WholeSaleCredit + "'");

                //foreach (DataRow itemCredit in dtCreditgInvoices.Rows)
                //{
                //    if (Convert.ToString(itemCredit["InvGrandTotalAmount"]) != "" && Convert.ToString(itemCredit["InvGrandTotalAmount"]) != "0")
                //        totalCreditSales += Convert.ToDouble(itemCredit["InvGrandTotalAmount"]);
                //}

                //   dataGridExpenseDetails.DataSource = dtResultsCreditSales;

                #endregion  Credit sales

                //   }
                #region expences

                DataTable dtSearchResultForExpence = new DataTable();
                if (objLedgerDBLayer == null)
                    objLedgerDBLayer = new LedgerDBLayer();

                dtSearchResultForExpence = objLedgerDBLayer.SerahcLedger(rptDate, rptDate, ref erroInfo);

                foreach (DataRow itemExpences in dtSearchResultForExpence.Rows)
                {
                    if (Convert.ToString(itemExpences["Amount"]) != "" && Convert.ToString(itemExpences["Amount"]) != "0")
                        totalExpense += Convert.ToDouble(itemExpences["Amount"]);
                }

                dataGridExpenseDetails.DataSource = dtSearchResultForExpence;

                #endregion expences

                #region openingbalance

                DataTable dtLedgerRpt = new DataTable();
                erroInfo = "";

                bool isOpeningBalanceExists = true;
                int siNoOfCreatedOpeningBal = 0;


                dtLedgerRpt = objLedgerDBLayer.GetLedgerRprtItemByDate(rptDate, ref isOpeningBalanceExists, ref siNoOfCreatedOpeningBal, ref erroInfo);
                decimal openingBalance = 0;
                // bool chkOpeningBalanaceExists = false;
                foreach (DataRow ldrRptItem in dtLedgerRpt.Rows)
                {
                    openingBalance = Math.Round(Convert.ToDecimal(ldrRptItem["OpeningBalance"]), 2);
                    openingBalanaceSINO = Convert.ToInt16(ldrRptItem["SiNo"]);
                    //chkOpeningBalanaceExists = true;
                }

                //if(openingBalance == 0) //opening balance is not updated
                //{
                //    //get previous day closing balance
                //    string[] days = rptDate.Split('-');
                //    if (days[2] != null && days[2] != "0")
                //    {
                //        int day = Convert.ToInt16(days[2]) -1;

                //        dtLedgerRpt = objLedgerDBLayer.GetLedgerRprtItemByDate(rptDate, ref erroInfo);
                //    }
                //}


                #endregion openingbalance

                #region Balance recived Amount recived from customer : Customer Accounts

                erroInfo = "";
                dtCustomerAccounts = objLedgerDBLayer.GetCredithistory(0, AviMaConstants.CustFlag, rptDate, ref erroInfo);

                foreach (DataRow itemCustCreditAmount in dtCustomerAccounts.Rows)
                {
                    if (Convert.ToString(itemCustCreditAmount["CreditAmnt"]) != "" && Convert.ToString(itemCustCreditAmount["CreditAmnt"]) != "0")
                        balanceRecived += Convert.ToDouble(itemCustCreditAmount["CreditAmnt"]);
                }

                #endregion Balance recived Amount recived from customer : Customer Accounts

                #region Balance  Amount paid to supplier : Supplier Accounts


                erroInfo = "";
                dtSupAccount = objLedgerDBLayer.GetCredithistory(0, AviMaConstants.SupFlag, rptDate, ref erroInfo);

                foreach (DataRow itemCustCreditAmount in dtSupAccount.Rows)
                {
                    if (Convert.ToString(itemCustCreditAmount["CreditAmnt"]) != "" && Convert.ToString(itemCustCreditAmount["CreditAmnt"]) != "0")
                        totalBalancePiadToSup += Convert.ToDouble(itemCustCreditAmount["CreditAmnt"]);
                }

                #endregion Balance  Amount paid to supplier : Supplier Accounts

                #region Assign values

                txtOpeningBalance.Text = Convert.ToString(openingBalance);
                txtSalesCash.Text = Convert.ToString(totalCashSales);
                //txtSalesCredit.Text = Convert.ToString(totalCreditSales);
                txtExpences.Text = Convert.ToString(totalExpense);
                txtBalanceRecived.Text = Convert.ToString(balanceRecived);
                txtBalancePaidToSup.Text = Convert.ToString(totalBalancePiadToSup);
                #endregion  Assign values

                #region calculate Cash In Hand
                CalculateCashInHand();

                #endregion calculate Cash In Hand

                #region update coresponding closing balance(cash in hand ) in db
                if (txtCashInHand.Text != "0" && !string.IsNullOrEmpty(txtCashInHand.Text) && openingBalanaceSINO != 0)
                {
                    LedgerRptDBLayer objLedgerRptDBLayer = new LedgerRptDBLayer();
                    LedgerRptDO objLedgerRptDO = new LedgerRptDO();
                    objLedgerRptDO.ClosingBalance = Convert.ToDecimal(txtCashInHand.Text);
                    objLedgerRptDO.OpeningBalance = Convert.ToDecimal(txtOpeningBalance.Text);
                    objLedgerRptDO.SiNo = openingBalanaceSINO;
                    erroInfo = "";
                    bool checkClosingBalanceUpda = objLedgerRptDBLayer.UpdateLedgerRprtItem(objLedgerRptDO, ref erroInfo);
                }
                #endregion update coresponding closing balance(cash in hand ) in db

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }


        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dateOpeningBalDate.Text) > DateTime.Now)
            {
                MessageBox.Show("Selected date is greater than today's date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateOpeningBalDate.ResetText();
                
                return;
            }

            LoadForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefreshLegerDetails_Click(object sender, EventArgs e)
        {
            try
            {


                if (!string.IsNullOrEmpty(txtOpeningBalance.Text) && txtOpeningBalance.Text != "0")
                {
                    if (MessageBox.Show("Update opening balance.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {


                        LedgerRptDO objLedgerRptDO = new LedgerRptDO();

                        if (openingBalanaceSINO != 0)
                        {
                            objLedgerRptDO.SiNo = openingBalanaceSINO;

                        }
                        else
                        {
                            objLedgerRptDO.LedgerDate = Convert.ToDateTime(dateOpeningBalDate.Text).ToString("yyyy-MM-dd");  //dateOpeningBalDate.Text;                         
                            objLedgerRptDO.CreatedBy = UserID;
                            objLedgerRptDO.CreatedDate = AviMaConstants.CurrentDateTimesStamp;
                        }
                        objLedgerRptDO.OpeningBalance = Convert.ToDecimal(txtOpeningBalance.Text);
                        objLedgerRptDO.ClosingBalance = Convert.ToDecimal(txtCashInHand.Text);

                        if (objLedgerRptDBLayer == null)
                            objLedgerRptDBLayer = new LedgerRptDBLayer();

                        string errorInfo = "";
                        bool _check = false;
                        if (openingBalanaceSINO == 0)
                        {
                            _check = objLedgerRptDBLayer.CreateLedgerRprtItem(objLedgerRptDO, ref errorInfo);
                        }
                        else
                        {
                            _check = objLedgerRptDBLayer.UpdateLedgerRprtItem(objLedgerRptDO, ref errorInfo);
                        }

                        if (_check)
                        {
                            MessageBox.Show("Ledger item created successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to create ledger item. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter opening balance.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOpeningBalance.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

            CalculateCashInHand();
        }


        private void CalculateCashInHand()
        {
            try
            {
                openingBalance = Convert.ToDouble(txtOpeningBalance.Text);
               // txtCashInHand.Text = Convert.ToString(openingBalance + totalCashSales + balanceRecived - (totalCreditSales + totalExpense + totalBalancePiadToSup));
                txtCashInHand.Text = Convert.ToString(openingBalance + totalCashSales + balanceRecived - ( totalExpense + totalBalancePiadToSup));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "CalculateCashInHand", "CalculateCashInHand()", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void ClearAllControls()
        {
            txtOpeningBalance.Text = "0";
            txtSalesCash.Text = "0";
            //txtSalesCredit.Text = "0";
            txtBalanceRecived.Text = "0";
            txtExpences.Text = "0";
            txtCashInHand.Text = "0";

            totalCashSales = 0;
            //totalCreditSales = 0;
            totalExpense = 0;
            totalBalancePiadToSup = 0;
            balanceRecived = 0;

            erroInfo = "";
        }

        private void btnSupBalanceDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtSupAccount != null && dtSupAccount.Rows.Count > 0)
                {
                    frmDisplayDetailsInGrid objfrmDisplayDetailsInGrid = new frmDisplayDetailsInGrid(dtSupAccount, "");
                    this.Hide();
                    objfrmDisplayDetailsInGrid.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("No supplier accounts to show", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

       
     
    }
}
