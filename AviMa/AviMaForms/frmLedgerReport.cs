using Avima.UtilityLayer;
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
    public partial class frmLedgerReport : Form
    {
        public string UserID { get; set; }
        public frmLedgerReport(string userID)
        {
            InitializeComponent();
        }

        private void frmLedgerReport_Load(object sender, EventArgs e)
        {
            try
            {
                dateOpeningBalDate.ResetText();

                btnPrint.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
                throw;
            }
        }

        DataTable CreateTableStructure()
        {
            DataTable ledgerDetails = new DataTable();
            try
            {


                #region JUst display to the user
                DataColumn dtColumnSINo = new DataColumn();
                dtColumnSINo.ColumnName = "Si.No.";
                DataColumn dtColumnName = new DataColumn();
                dtColumnName.ColumnName = "Name";
                DataColumn dtColumnSalesCash = new DataColumn();
                dtColumnSalesCash.ColumnName = "SalesCash";
                DataColumn dtColumnSalesCredit = new DataColumn();
                dtColumnSalesCredit.ColumnName = "SalesCredit";
                DataColumn dtColumnReceipt = new DataColumn();
                dtColumnReceipt.ColumnName = "Receipt";
                DataColumn dtColumnPayments = new DataColumn();
                dtColumnPayments.ColumnName = "Payments";
                DataColumn dtColumnBalance = new DataColumn();
                dtColumnBalance.ColumnName = "Balance";



                ledgerDetails = new DataTable();
                ledgerDetails.Columns.Add(dtColumnSINo);
                ledgerDetails.Columns.Add(dtColumnName);
                ledgerDetails.Columns.Add(dtColumnSalesCash);
                ledgerDetails.Columns.Add(dtColumnSalesCredit);
                ledgerDetails.Columns.Add(dtColumnReceipt);
                ledgerDetails.Columns.Add(dtColumnPayments);
                ledgerDetails.Columns.Add(dtColumnBalance);


                //ledgerDetails.Rows.Add(bulkSiNo++, objItemsDO.BarCode, objItemsDO.Name, objItemsDO.Qnty, objItemsDO.Description);
                //dataBulcItem.DataSource = dtBulkItemsCreation;

                #endregion JUst display to the user
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
            return ledgerDetails;
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

        public bool LoadForm()
        {
            bool check = true;

            dtCustomerAccounts = new DataTable();
            dtSupAccount = new DataTable();

            try
            {
                ClearAllControls();

                string rptDate = Convert.ToDateTime(dateOpeningBalDate.Text).ToString("yyyy-MM-dd");


                if (objLedgerDBLayer == null)
                    objLedgerDBLayer = new LedgerDBLayer();


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

                txtOpeningBalance.Text = Convert.ToString(openingBalance);

                if (txtOpeningBalance.Text == "0")
                {
                    MessageBox.Show("Opening balance is Zero so could not load the report. ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    check = false;
                    return check;
                }

                #endregion openingbalance


                if (objInvoiceDBLayer == null)
                    objInvoiceDBLayer = new InvoiceDBLayer();


                int SINO = 1;

                #region Cash sales

                DataTable dtCash = new DataTable();
                dtCash = objInvoiceDBLayer.SearchInvoice(0, rptDate, rptDate, "", AviMaConstants.WholeSaleCash, false, AviMaConstants.LedgerFlag, ref erroInfo);

                DataTable dtRetail = new DataTable();
                dtRetail = objInvoiceDBLayer.SearchInvoice(0, rptDate, rptDate, "", AviMaConstants.Retail, false, AviMaConstants.LedgerFlag, ref erroInfo);


                foreach (DataRow itemCash in dtRetail.Rows)
                {
                    if (Convert.ToString(itemCash["InvGrandTotalAmount"]) != "" && Convert.ToString(itemCash["InvGrandTotalAmount"]) != "0")
                    {

                        //If there is no customer number associated with invoice then show inv number
                        if (Convert.ToString(itemCash["InvCustID"]) != "0")
                            dtLedgerDetails.Rows.Add(SINO, itemCash["InvCustID"], Convert.ToString(itemCash["InvGrandTotalAmount"]), "----", "----", "----", "");
                        else
                            dtLedgerDetails.Rows.Add(SINO, itemCash["InvID"], Convert.ToString(itemCash["InvGrandTotalAmount"]), "----", "----", "----", "");
                    }

                }

                foreach (DataRow itemCash in dtCash.Rows)
                {
                    if (Convert.ToString(itemCash["InvGrandTotalAmount"]) != "" && Convert.ToString(itemCash["InvGrandTotalAmount"]) != "0")
                    {

                        //If there is no customer number associated with invoice then show inv number
                        if (Convert.ToString(itemCash["InvCustID"]) != "0")
                            dtLedgerDetails.Rows.Add(SINO, itemCash["InvCustID"], Convert.ToString(itemCash["InvGrandTotalAmount"]), "----", "----", "----", "");
                        else
                            dtLedgerDetails.Rows.Add(SINO, itemCash["InvID"], Convert.ToString(itemCash["InvGrandTotalAmount"]), "----", "----", "----", "");
                    }
                }
                #endregion  Cash sales

                #region Credit sales
                //DataRow[] dtResultsCreditSales = dtAllInvoices.Select("InvSaleType = '" + AviMaConstants.WholeSaleCredit + "'");

                DataTable dtCreditgInvoices = new DataTable();
                dtCreditgInvoices = objInvoiceDBLayer.SearchInvoice(0, rptDate, rptDate, "", AviMaConstants.WholeSaleCredit, false, AviMaConstants.LedgerFlag, ref erroInfo);


                foreach (DataRow itemCredit in dtCreditgInvoices.Rows)
                {
                    if (Convert.ToString(itemCredit["InvGrandTotalAmount"]) != "" && Convert.ToString(itemCredit["InvGrandTotalAmount"]) != "0")
                        dtLedgerDetails.Rows.Add(SINO, itemCredit["InvCustID"], "----", Convert.ToString(itemCredit["InvGrandTotalAmount"]), "----", "----", "");
                    //totalCreditSales += Convert.ToDouble(itemCredit["InvGrandTotalAmount"]);
                }

                //  dataGridExpenseDetails.DataSource = dtResultsCreditSales;

                #endregion  Credit sales

                #region expences

                DataTable dtSearchResultForExpence = new DataTable();

                dtSearchResultForExpence = objLedgerDBLayer.SerahcLedger(rptDate, rptDate, ref erroInfo);

                foreach (DataRow itemExpences in dtSearchResultForExpence.Rows)
                {
                    if (Convert.ToString(itemExpences["Amount"]) != "" && Convert.ToString(itemExpences["Amount"]) != "0")
                        dtLedgerDetails.Rows.Add(SINO, itemExpences["PaidTo"], "----", "----", "----", Convert.ToDouble(itemExpences["Amount"]), "");
                    //totalExpense += Convert.ToDouble(itemExpences["Amount"]);
                }
                #endregion expences

                #region Balance recived Amount recived from customer : Customer Accounts

                erroInfo = "";
                dtCustomerAccounts = objLedgerDBLayer.GetCredithistory(0, AviMaConstants.CustFlag, rptDate, ref erroInfo);

                foreach (DataRow itemCustCreditAmount in dtCustomerAccounts.Rows)
                {
                    if (Convert.ToString(itemCustCreditAmount["CreditAmnt"]) != "" && Convert.ToString(itemCustCreditAmount["CreditAmnt"]) != "0.00")
                        dtLedgerDetails.Rows.Add(SINO, itemCustCreditAmount["AccountName"], "----", "----", Convert.ToDouble(itemCustCreditAmount["CreditAmnt"]), "----", "");
                    // balanceRecived += Convert.ToDouble(itemCustCreditAmount["CreditAmnt"]);
                }

                #endregion Balance recived Amount recived from customer : Customer Accounts

                #region Balance  Amount paid to supplier : Supplier Accounts


                erroInfo = "";
                dtSupAccount = objLedgerDBLayer.GetCredithistory(0, AviMaConstants.SupFlag, rptDate, ref erroInfo);

                foreach (DataRow itemCustCreditAmount in dtSupAccount.Rows)
                {
                    if (Convert.ToString(itemCustCreditAmount["CreditAmnt"]) != "" && Convert.ToString(itemCustCreditAmount["CreditAmnt"]) != "0.00")
                        dtLedgerDetails.Rows.Add(SINO, itemCustCreditAmount["AccountName"], "----", "----", "----", Convert.ToDouble(itemCustCreditAmount["CreditAmnt"]), "");
                    //totalBalancePiadToSup += Convert.ToDouble(itemCustCreditAmount["CreditAmnt"]);
                }

                #endregion Balance  Amount paid to supplier : Supplier Accounts


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
                check = false;
            }


            return check;
        }

        private void ClearAllControls()
        {
            //txtOpeningBalance.Text = "0";
            //txtSalesCash.Text = "0";
            ////txtSalesCredit.Text = "0";
            //txtBalanceRecived.Text = "0";
            //txtExpences.Text = "0";
            //txtCashInHand.Text = "0";

            //totalCashSales = 0;
            ////totalCreditSales = 0;
            //totalExpense = 0;
            //totalBalancePiadToSup = 0;
            //balanceRecived = 0;

            //erroInfo = "";
        }
        DataTable dtLedgerDetails = new DataTable();
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(dateOpeningBalDate.Text) > DateTime.Now)
                {
                    MessageBox.Show("Selected date is greater than today's date", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dateOpeningBalDate.ResetText();
                }

                else
                {

                    dtLedgerDetails = CreateTableStructure();
                    bool check = LoadForm();
                    if (check)
                    {
                        ReplaceCustIdsWithCustNames(ref dtLedgerDetails);
                        CalculateBalance();
                        UpdateClosingBalance();
                        btnPrint.Enabled = true;
                    }
                    dtGridgLedgerDetails.DataSource = dtLedgerDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        DataTable dtCustomerDetails = null;
        CustomerDBLayer objCustomerDBLayer = null;

        void ReplaceCustIdsWithCustNames(ref DataTable dtLedgerDetails)
        {
            try
            {
                #region load cust info to map

                string erroInfo = "";

                if (dtCustomerDetails == null)
                {
                    dtCustomerDetails = new DataTable();

                    if (objCustomerDBLayer == null)
                        objCustomerDBLayer = new CustomerDBLayer();

                    dtCustomerDetails = objCustomerDBLayer.GetAllCustomers(ref erroInfo);

                }

                #endregion load cust info to map


                //DataColumn objColSupName = new DataColumn("Name");
                //dtLedgerDetails.Columns.Add(objColSupName);


                foreach (DataRow item in dtLedgerDetails.Rows)
                {


                    if (!string.IsNullOrEmpty(Convert.ToString(item["SalesCash"])) || !string.IsNullOrEmpty(Convert.ToString(item["SalesCredit"])))
                    {
                        if (Convert.ToString(item["SalesCash"]) != "----" && Convert.ToString(item["SalesCash"]) != "")
                        {
                            foreach (DataRow cutomer in dtCustomerDetails.Rows)
                            {
                                if (Convert.ToString(item["Name"]) == Convert.ToString(cutomer["CustID"]))
                                {
                                    item["Name"] = cutomer["CustName"];
                                }
                            }
                        }

                        if (Convert.ToString(item["SalesCredit"]) != "----" && Convert.ToString(item["SalesCredit"]) != "")
                        {
                            foreach (DataRow cutomer in dtCustomerDetails.Rows)
                            {
                                if (Convert.ToString(item["Name"]) == Convert.ToString(cutomer["CustID"]))
                                {
                                    item["Name"] = cutomer["CustName"];
                                }
                            }
                        }

                    }





                    //if (!string.IsNullOrEmpty(Convert.ToString(item["Receipt"])))
                    //{
                    //    if (Convert.ToString(item["Receipt"]) != "----" && Convert.ToString(item["Receipt"]) != "")
                    //    {



                    //        foreach (DataRow cutomer in dtCustomerDetails.Rows)
                    //        {
                    //            if (Convert.ToString(item["InvCustID"]) == Convert.ToString(cutomer["CustID"]))
                    //            {
                    //                invoice["CustName"] = cutomer["CustName"];
                    //            }
                    //        }



                    //    }


                    //}
                    //if (!string.IsNullOrEmpty(Convert.ToString(item["Payments"])))
                    //{
                    //    if (openingBala != 0 && Convert.ToString(item["Payments"]) != "----")
                    //        openingBala -= Convert.ToDecimal(item["Payments"]);
                    //    else if (Convert.ToString(item["Payments"]) != "----")
                    //        openingBala = Convert.ToDecimal(item["Payments"]);
                    //}


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }

        }


        DailyLedgerDO objDailyLedgerDO = new DailyLedgerDO();

        void CalculateBalance()
        {

            try
            {
                decimal openingBala = 0;

                if (txtOpeningBalance.Text != "")
                {
                    openingBala = Convert.ToInt16(txtOpeningBalance.Text);
                }

                objDailyLedgerDO.Sales = new List<Sale>(); // for print

                foreach (DataRow item in dtLedgerDetails.Rows)
                {
                    Sale objsale = new Sale(); // for print

                    if (!string.IsNullOrEmpty(Convert.ToString(item["SalesCash"])) && Convert.ToString(item["SalesCash"]) != "----")
                    {
                        if (openingBala != 0)
                            openingBala += Convert.ToDecimal(item["SalesCash"]);
                        else if (Convert.ToString(item["SalesCash"]) != "----")
                            openingBala = Convert.ToDecimal(item["SalesCash"]);

                        objsale.Amount = Convert.ToString(item["SalesCash"]);
                        objsale.Type = AviMaConstants.SalesCash;


                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(item["Receipt"])) && Convert.ToString(item["Receipt"]) != "----")
                    {
                        if (openingBala != 0)
                            openingBala += Convert.ToDecimal(item["Receipt"]);
                        else if (Convert.ToString(item["Receipt"]) != "----")
                            openingBala = Convert.ToDecimal(item["Receipt"]);


                        objsale.Amount = Convert.ToString(item["Receipt"]);
                        objsale.Type = AviMaConstants.Receipt;
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(item["Payments"])) && Convert.ToString(item["Payments"]) != "----")
                    {
                        if (openingBala != 0)
                            openingBala -= Convert.ToDecimal(item["Payments"]);
                        else if (Convert.ToString(item["Payments"]) != "----")
                            openingBala = Convert.ToDecimal(item["Payments"]);

                        objsale.Amount = Convert.ToString(item["Payments"]);
                        objsale.Type = AviMaConstants.Payments;
                    }
                    else if (!string.IsNullOrEmpty(Convert.ToString(item["SalesCredit"])) && Convert.ToString(item["SalesCredit"]) != "----")
                    {
                        objsale.Amount = Convert.ToString(item["SalesCredit"]);
                        objsale.Type = AviMaConstants.SalesCredit;
                    }



                    objsale.Name = Convert.ToString(item["Name"]);


                    //logic to make sure name should not be too lengthy in printed report
                    if (objsale.Name.Length > 16)
                        objsale.Name = objsale.Name.Substring(0, 16);

                    objDailyLedgerDO.Sales.Add(objsale);


                    item["Balance"] = openingBala;
                }

                lblPrintCashInHand.Text = Convert.ToString(openingBala);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintLedgerReport();
        }

        bool UpdateClosingBalance()
        {

            bool checkClosingBalanceUpda = true;
            try
            {

                #region update coresponding closing balance(cash in hand ) in db
                if (txtOpeningBalance.Text != "0" && !string.IsNullOrEmpty(lblPrintCashInHand.Text) && openingBalanaceSINO != 0)
                {
                    LedgerRptDBLayer objLedgerRptDBLayer = new LedgerRptDBLayer();
                    LedgerRptDO objLedgerRptDO = new LedgerRptDO();
                    objLedgerRptDO.ClosingBalance = Convert.ToDecimal(lblPrintCashInHand.Text);
                    objLedgerRptDO.OpeningBalance = Convert.ToDecimal(txtOpeningBalance.Text);
                    objLedgerRptDO.SiNo = openingBalanaceSINO;
                    erroInfo = "";
                    checkClosingBalanceUpda = objLedgerRptDBLayer.UpdateLedgerRprtItem(objLedgerRptDO, ref erroInfo);
                }
                #endregion update coresponding closing balance(cash in hand ) in db

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
            return checkClosingBalanceUpda;
        }

        void PrintLedgerReport()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtOpeningBalance.Text) && txtOpeningBalance.Text != "0" && dtLedgerDetails.Rows.Count > 0)
                {
                    objDailyLedgerDO.OpeningBalance = txtOpeningBalance.Text;
                    objDailyLedgerDO.RowCount = dtLedgerDetails.Rows.Count;
                    PrntDailyLedRpt objPrntDailyLedRpt = new PrntDailyLedRpt(objDailyLedgerDO);
                    objPrntDailyLedRpt.PrintReport();
                }
                else
                {
                    MessageBox.Show("There are no trasaction for the day.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
