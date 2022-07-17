using AviMa.DataBaseLayer;
using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AviMa.AviMaForms
{
    public partial class frmInvoice : Form
    {
        public string InvoiceType { get; set; }
        public string UserID { get; set; }
        DataTable dtStockDtlsForInv = null;

        public frmInvoice(string userID)
        {
            InitializeComponent();

            UserID = userID;
        }

        InvoiceDBLayer objInvoiceDBLayer = new InvoiceDBLayer();
        FormNumberOfPrints objNumPrint = null;
        char invoiceType;

        private void dtGridInvoice_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    int rowNumber = e.RowIndex;

            //    if (rowNumber != -1 && String.IsNullOrEmpty(Convert.ToString(dtGridInvoice[0, rowNumber].Value)))
            //    {
            //        int intSiNo = 1 + rowNumber;
            //        dtGridInvoice[0, rowNumber].Value = intSiNo;
            //        dtGridInvoice[6, rowNumber].Value = "Delete";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            //}
        }

        private void ClearControls()
        {
            try
            {
                txtCustName.Text = "";
                txtAddress.Text = string.Empty;
                txtMobile1.Text = string.Empty;
                txtMobile2.Text = string.Empty;
                txtAddress.Text = string.Empty;
                dtGridInvoice.Rows.Clear();
                txtGrnadTotal2.Text = string.Empty;
                txtGrnadTotal.Text = string.Empty;
                txtDiscount.Text = string.Empty;
                lblGrandTotal.Text = string.Empty;
                txtTotalItems.Text = string.Empty;
                //txtPackedBy.Text = string.Empty;
                lblOldBalance.Text = "0";
                bFoundCustomer = false;

                txtNote1.Text = "";
                txtNote2.Text = "";
                txtNote3.Text = "";
                txtNote4.Text = "";


                dtGridViewItemsList.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }

        }

        private void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            if (dtGridInvoice.RowCount > 1)
            {
                if (MessageBox.Show("Generate and print invoice", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        callFromBtnSave = true;

                        this.Cursor = Cursors.WaitCursor;

                        InvoiceDO objInvoiceDO = new InvoiceDO();
                        objInvoiceDO.Title = InvoiceType;
                        objInvoiceDO.InvOrRepair = invoiceType;
                        objInvoiceDO.FoundCustomer = bFoundCustomer;
                        // Collect customer details
                        CustomerDO objCustomerDO = new CustomerDO();
                        objCustomerDO.CustName = txtCustName.Text.Trim().TrimStart();
                        objCustomerDO.CustMobile1 = txtMobile1.Text.Trim().TrimStart();
                        objCustomerDO.CustMobile2 = txtMobile2.Text.Trim().TrimStart();
                        objCustomerDO.CustTown = txtAddress.Text.Trim().TrimStart();
                        objInvoiceDO.InvNote = txtNote1.Text.Trim().TrimStart() + "\n" + txtNote2.Text.Trim().TrimStart() + "\n" + txtNote3.Text.Trim().TrimStart() + "\n" + txtNote4.Text.Trim().TrimStart();
                        if (chkPrintBalance.Checked)
                            objInvoiceDO.OldBalance = lblOldBalance.Text;
                        objInvoiceDO.PrintOldBalance = chkPrintBalance.Checked;
                        try  // temp code remove ex handling
                        {
                            objCustomerDO.CustID = Convert.ToInt16(listCustomerNames.SelectedValue);
                        }
                        catch (Exception)
                        {

                        }

                        objInvoiceDO.CustomerDetails = objCustomerDO;
                        objInvoiceDO.SaleType = GetSaleType();


                        if (objInvoiceDO.SaleType == AviMaConstants.WholeSaleCredit && string.IsNullOrEmpty(txtCustName.Text))
                        {
                            MessageBox.Show("Customer name is mandatory to generate credit invoice", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCustName.Focus();
                            return;
                        }

                        objInvoiceDO.InvoiceNumber = txtInvoiceNu.Text.Trim();


                        objInvoiceDO.UserLoginID = UserID;
                        objInvoiceDO.InvCreatedBy = UserID;
                        objInvoiceDO.InvCreatedDate = DateTime.Now.ToString("yyyyMMddHHMMss");


                        List<InvoiceItemsDO> objItemsList = new List<InvoiceItemsDO>();

                        objInvoiceDO.TotalAmount = txtGrnadTotal.Text.Trim();

                        objInvoiceDO.Discuont = txtDiscount.Text.Trim();
                        objInvoiceDO.GrandTotalAmount = txtGrnadTotal2.Text.Trim();


                        objInvoiceDO.TotalItems = txtTotalItems.Text.Trim();
                        objInvoiceDO.PackedBy = txtPackedBy.Text.Trim();

                        objInvoiceDO.ItemsList = GetItemsList();


                        #region for printing multiple copies
                        if (objNumPrint == null)
                            objNumPrint = new FormNumberOfPrints();
                        this.Hide();
                        objNumPrint.ShowDialog();
                        this.Show();
                        decimal noOfPrints = 1; // Default 1 copy
                        noOfPrints = objNumPrint.NumberOfCopies; // Override Default 1 copy
                        #endregion for printing multiple copies

                        //Save invoice to DB
                        bool _checkDBHit = false;
                        string erroInfo = "";
                        try
                        {
                            _checkDBHit = objInvoiceDBLayer.CreateInvoice(objInvoiceDO, false, bFoundCustomer, ref erroInfo);
                        }
                        catch (Exception ex)
                        {
                            #region Hack to resolve issue with dupplicate invoice number : After finding exact cause and fix please remove this logic and comments
                            if (ex.Message.Contains("#23000Duplicate entry"))
                            {
                                objInvoiceDO.InvoiceNumber = objInvoiceDBLayer.GetInvoiceNumber("Invoice", ref erroInfo);
                                _checkDBHit = objInvoiceDBLayer.CreateInvoice(objInvoiceDO, false, bFoundCustomer, ref erroInfo);
                                invDataFromTempInv = false; // reset invoice number
                            }

                            #endregion Hack to resolve issue with dupplicate invoice number : After finding exact cause and fix please remove this logic and comments
                        }

                        //if Save invoice to DB is success full then print it
                        if (_checkDBHit)
                        {
                            bool checkPrintSuccssfull = false;
                            for (int i = 0; i < noOfPrints; i++)
                            {
                                try
                                {

                                    PrintInvoice objPrintInvoice = new PrintInvoice(objInvoiceDO);
                                    checkPrintSuccssfull = objPrintInvoice.PrintReport();
                                    objInvoiceDO.ItemsList = GetItemsList();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Invoice Sved successfully but Unable to print invoice. " + ex.Message + ". Please go to show invoice menu items and re-print invoice after configring the printer", "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);


                                    ClearControls(); // if print is unsuccssfull
                                }
                            }

                            if (checkPrintSuccssfull)
                            {
                                erroInfo = "";
                                //if (!objInvoiceDBLayer.UpdateInvoiceNumber("Invoice", ref erroInfo))
                                //{
                                //    MessageBox.Show("Unable to update invoice number. " + erroInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //}
                                ////else
                                ////{
                                //Done with rpinting and saving invoice so clear all the controls
                                ClearControls();
                                erroInfo = "";
                                string newInvNum = "";
                                if (InvoiceType == AviMaConstants.INVOICE)
                                {
                                    if (!invDataFromTempInv)
                                    {
                                        if (objInvoiceDBLayer.UpdateInvoiceNumber("Invoice", ref erroInfo))
                                            newInvNum = objInvoiceDBLayer.GetInvoiceNumber("Invoice", ref erroInfo);
                                        else
                                        {
                                            MessageBox.Show("Unable to update invoice number. " + erroInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        newInvNum = objInvoiceDBLayer.GetInvoiceNumber("Invoice", ref erroInfo);
                                    }


                                    invDataFromTempInv = false;

                                }
                                else if (InvoiceType == AviMaConstants.REPAIR)
                                {
                                    if (objInvoiceDBLayer.UpdateInvoiceNumber("Repair", ref erroInfo))
                                        newInvNum = objInvoiceDBLayer.GetInvoiceNumber("Repair", ref erroInfo);
                                    else
                                    {
                                        MessageBox.Show("Unable to update repair number. " + erroInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                                if (!string.IsNullOrEmpty(newInvNum))
                                {
                                    txtInvoiceNu.Text = newInvNum;
                                }
                                //}

                                #region //Issue : Eventhough custmer details are note entered, invoice master table is getting populated with previously generated invoice customer.
                                // Scenario: 1) Create an invoice with customer as B.
                                //           2) if #1 successfully completed then now with out entering custmore detials create invoice 
                                //           3) Invoice created @ #2 is updated with customer ID of invoice created at #1
                                listCustomerNames.ClearSelected();


                                #endregion //Issue : Eventhough custmer is note entered, invoice master table is getting populated with previously generated invoice customer.
                            }
                        }
                        else
                        { MessageBox.Show("Unable to generate invoice. " + erroInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to generate invoice. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter at least one item to generate invoice", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private List<InvoiceItemsDO> GetItemsList()
        {
            List<InvoiceItemsDO> objItemsList = new List<InvoiceItemsDO>();

            try
            {

                int j = 1;
                for (int i = 0; i < dtGridInvoice.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(dtGridInvoice[1, i].Value).Trim()))
                    {
                        InvoiceItemsDO objInvoiceItems = new InvoiceItemsDO();
                        objInvoiceItems.SiNo = Convert.ToString(j);

                        objInvoiceItems.ItemName = Convert.ToString(dtGridInvoice["ItemName", i].Value);
                        objInvoiceItems.BarCode = Convert.ToString(dtGridInvoice["ItemBarCode", i].Value);
                        objInvoiceItems.ItemQnty = Convert.ToString(dtGridInvoice["Itemqnty", i].Value);
                        objInvoiceItems.ItemRate = Convert.ToString(dtGridInvoice["ITEMRATE", i].Value);
                        objInvoiceItems.Amount = Convert.ToString(dtGridInvoice["AMOUNT", i].Value);

                        //objInvoiceItems.CreatedDateTime = Convert.ToString(dtGridInvoice["InvDateTime", i].Value);
                        DateTime dateTemp = Convert.ToDateTime(dtGridInvoice["InvDateTime", i].Value);
                        objInvoiceItems.CreatedDateTime = dateTemp;


                        objInvoiceItems.ItemSuplierID = "4"; // need to replace with actuall suplier ID
                        objItemsList.Add(objInvoiceItems);
                        j++;
                    }
                }

            }
            catch (Exception ex)
            {
                objItemsList = null;
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }

            return objItemsList;
        }


        private string GetSaleType()
        {
            string _saleType = "";

            try
            {
                if (rdBtnRetail.Checked)
                    _saleType = AviMaConstants.Retail;
                else if (rdbtnWholSaleCash.Checked)
                    _saleType = AviMaConstants.WholeSaleCash;
                else if (rdBtnWholesalesCredit.Checked)
                    _saleType = AviMaConstants.WholeSaleCredit;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
                _saleType = "";
            }
            return _saleType;
        }

        private void SetSaleType(string saleType)
        {
            try
            {

                if (saleType == AviMaConstants.Retail)
                    rdBtnRetail.Checked = true;
                else if (saleType == AviMaConstants.WholeSaleCash)
                    rdbtnWholSaleCash.Checked = true;
                else if (saleType == AviMaConstants.WholeSaleCredit)
                    rdBtnWholesalesCredit.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        //DataTable dtStkDetails = null;

        private void frmInvoice_Load(object sender, EventArgs e)
        {

            try
            {
                lblInvoiceType.Text = InvoiceType;
                this.Text = InvoiceType;

                txtPackedBy.Text = UserID;
                string errorInfo = "";

                //lblDate.Text += DateTime.Now.ToString("MM/dd/yyyy  HH:MM:ss tt");

                // Show radio buttons only for the invoice
                if (InvoiceType == AviMaConstants.INVOICE)
                {
                    invoiceType = AviMaConstants.InvoiceFlag;
                    //lblInvoiNumber.Text = "Invoice No.:";
                    lblInvoiNumber.Text = "Estimate No.:";
                    lblRcvdPackd.Text = "Packed By:";
                    txtInvoiceNu.Text = objInvoiceDBLayer.GetInvoiceNumber("Invoice", ref errorInfo);

                    rdBtnRetail.Visible = true;
                    rdBtnWholesalesCredit.Visible = true;
                    rdbtnWholSaleCash.Visible = true;

                    // Create the ToolTip and associate with the Form container.
                    ToolTip toolTip1 = new ToolTip();
                    toolTip1.ShowAlways = true;
                    // Set up the ToolTip text for the Button and Checkbox.
                    toolTip1.SetToolTip(this.rdBtnRetail, "Retail");
                    toolTip1.SetToolTip(this.rdBtnWholesalesCredit, "Whole Sale Credit");
                    toolTip1.SetToolTip(this.rdbtnWholSaleCash, "Whole Sale Cash");

                    lblDsicount.Visible = true;
                    lblGrndTotal.Visible = true;
                    txtDiscount.Visible = true;
                    txtGrnadTotal2.Visible = true;
                    button1.Visible = true;
                    button2.Visible = true;
                    lblRs1.Visible = true;
                    lblRs2.Visible = true;
                    grpPauseBilling.Visible = true;
                    grpBoxOldBal.Visible = true;
                }
                else if (InvoiceType == AviMaConstants.REPAIR)
                {
                    txtInvoiceNu.Text = objInvoiceDBLayer.GetInvoiceNumber("Repair", ref errorInfo);
                    invoiceType = AviMaConstants.RepairFlag;
                    lblInvoiNumber.Text = "Service No.:";
                    lblRcvdPackd.Text = "Recived By:";
                    rdBtnRetail.Visible = false;
                    rdBtnWholesalesCredit.Visible = false;
                    rdbtnWholSaleCash.Visible = false;

                    lblDsicount.Visible = false;
                    lblGrndTotal.Visible = false;
                    txtDiscount.Visible = false;
                    txtGrnadTotal2.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    lblRs1.Visible = false;
                    lblRs2.Visible = false;

                    grpPauseBilling.Visible = false;
                    grpBoxOldBal.Visible = false;
                }

                dtStockDtlsForInv = MasterCache.GetStockDataForInvFrmCache(ref errorInfo);
                txtCustName.Focus();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        bool callFromBtnSave = false;
        int invoiceGridRowNumberCurrent = -1;
        private void dtGridInvoice_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (!callFromBtnSave)
                {

                    dtGridInvoice.EndEdit();
                    int rowIndex = e.RowIndex;


                    #region autopopulate item details for Bar code

                    if (e.ColumnIndex == 2)
                    {
                        string barcode = Convert.ToString(dtGridInvoice[2, rowIndex].Value);

                        if (!string.IsNullOrEmpty(barcode))
                        {
                            if (objInvoiceDBLayer == null)
                                objInvoiceDBLayer = new InvoiceDBLayer();

                            try
                            {
                                DataTable dtSearchResult = objInvoiceDBLayer.SerahcStock("", barcode, "");

                                if (dtSearchResult != null && dtSearchResult.Rows.Count == 1)//&& dtSearchResult.Rows.Count > 0 && dtSearchResult.Rows.Count == 1)
                                {
                                    foreach (DataRow item in dtSearchResult.Rows)
                                    {
                                        //IName,IQnty,IWCashPrice,IWCreditPrice,IWRetailPrice
                                        dtGridInvoice[1, rowIndex].Value = Convert.ToString(item["IName"]); // description                            
                                        if (rdBtnRetail.Checked)
                                            dtGridInvoice[4, rowIndex].Value = Convert.ToString(item["IWRetailPrice"]); // Rate
                                        else if (rdbtnWholSaleCash.Checked)
                                            dtGridInvoice[4, rowIndex].Value = Convert.ToString(item["IWCashPrice"]); // Rate
                                        else if (rdBtnWholesalesCredit.Checked)
                                            dtGridInvoice[4, rowIndex].Value = Convert.ToString(item["IWCreditPrice"]); // Rate
                                    }

                                }
                                else
                                {
                                    dtGridViewItemsList.DataSource = dtSearchResult;
                                    invoiceGridRowNumberCurrent = rowIndex;
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    #endregion autopopulate item details for Bar code

                    #region autopopulate item details for Item Name

                    if (e.ColumnIndex == 1)
                    {
                        string itemName = Convert.ToString(dtGridInvoice[1, rowIndex].Value);
                        if (!string.IsNullOrEmpty(itemName))
                        {
                            if (objInvoiceDBLayer == null)
                                objInvoiceDBLayer = new InvoiceDBLayer();

                            try
                            {
                                DataTable dtSearchResult = objInvoiceDBLayer.SerahcStock(itemName, "", "");

                                if (dtSearchResult != null && dtSearchResult.Rows.Count == 1)//&& dtSearchResult.Rows.Count > 0 && dtSearchResult.Rows.Count == 1)
                                {
                                    foreach (DataRow item in dtSearchResult.Rows)
                                    {
                                        //IName,IQnty,IWCashPrice,IWCreditPrice,IWRetailPrice
                                        dtGridInvoice[1, rowIndex].Value = Convert.ToString(item["IName"]); // description                            
                                        if (rdBtnRetail.Checked)
                                            dtGridInvoice[4, rowIndex].Value = Convert.ToString(item["IWRetailPrice"]); // Rate
                                        else if (rdbtnWholSaleCash.Checked)
                                            dtGridInvoice[4, rowIndex].Value = Convert.ToString(item["IWCashPrice"]); // Rate
                                        else if (rdBtnWholesalesCredit.Checked)
                                            dtGridInvoice[4, rowIndex].Value = Convert.ToString(item["IWCreditPrice"]); // Rate
                                    }

                                }
                                else
                                {
                                    dtGridViewItemsList.DataSource = dtSearchResult;
                                    invoiceGridRowNumberCurrent = rowIndex;
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }

                    }

                    #endregion autopopulate item details for Item Name


                    decimal totalAmount = Convert.ToDecimal(dtGridInvoice[3, rowIndex].Value) * Convert.ToDecimal(dtGridInvoice[4, rowIndex].Value);
                    dtGridInvoice[5, rowIndex].Value = totalAmount;

                    CalculateTotalQntyAndAmount();
                }
                callFromBtnSave = false;
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Enter Correct value", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void CalculateTotalQntyAndAmount()
        {
            try
            {
                decimal objtotalWitoutVAT = 0;
                Int64 totalQnty = 0;

                for (int i = 0; i < dtGridInvoice.Rows.Count; i++)
                {
                    objtotalWitoutVAT += Convert.ToDecimal(dtGridInvoice[5, i].Value);
                    totalQnty += Convert.ToInt64(dtGridInvoice[3, i].Value);
                }

                txtGrnadTotal.Text = Convert.ToString(objtotalWitoutVAT);
                lblGrandTotal.Text = txtGrnadTotal.Text + ".00";
                txtTotalItems.Text = Convert.ToString(totalQnty);
                txtGrnadTotal2.Text = txtGrnadTotal.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear invoice details.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ClearControls();
                ClearInvItemFields();

                if (InvoiceType == AviMaConstants.INVOICE)
                {
                    //PIP store inv locally like prev current and feature so that we could avoid few DB hits
                    txtInvoiceNu.Text = "";
                    string erroInfo = "";
                    txtInvoiceNu.Text = objInvoiceDBLayer.GetInvoiceNumber("Invoice", ref erroInfo);
                    invDataFromTempInv = false;

                    //rowCountFrmAddBtn = 0;
                }

            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will close invoice form", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }



        private void btnSearchClear_Click(object sender, EventArgs e)
        {
            txtCustName.Text = "";
            txtMobile1.Text = "";
            txtMobile2.Text = "";
            txtAddress.Text = "";
        }

        private void dtGridInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                int i = e.ColumnIndex;

                if (i == 6 && e.RowIndex != -1) // Fix for Main_147
                {
                    if (MessageBox.Show("Delete item from the inovice item list", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        try
                        {
                            dtGridInvoice.Rows.RemoveAt(e.RowIndex);

                            int sino = 1;
                            for (int y = 0; y < dtGridInvoice.Rows.Count; y++)
                            {
                                dtGridInvoice[0, y].Value = sino;
                                sino++;

                                //rowCountFrmAddBtn--;
                                //siNumber--;
                            }
                            CalculateTotalQntyAndAmount();
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show("No data to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    //int j = e.RowIndex;
                    //if (j != -1)
                    //{
                    //    txtInvName.Text = Convert.ToString(dtGridInvoice[1, j].Value); // description      
                    //    txtInvBarCode.Text = Convert.ToString(dtGridInvoice[2, j].Value); // bar code
                    //    txtInvQnty.Text = Convert.ToString(dtGridInvoice[3, j].Value); //      qnty             
                    //    txtInvItemRate.Text = Convert.ToString(dtGridInvoice[4, j].Value); // Rate
                    //    txtInvTotalAmnt.Text = Convert.ToString(dtGridInvoice[5, j].Value); // total amonut = qnty * rate
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dtGridViewItemsList_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int i = e.RowIndex;
                if (i != -1 && invoiceGridRowNumberCurrent != -1)
                {
                    // check to validate selected item is not empty from the dtGridViewItemsList
                    if (!string.IsNullOrEmpty(Convert.ToString(dtGridViewItemsList[2, i].Value)) &&
                        !string.IsNullOrEmpty(Convert.ToString(dtGridViewItemsList[1, i].Value)))
                    {
                        //IID, IBarCode, IName, IDescription, IQnty, IPurchasePrice, IPONumber, ISupID,"+
                        //    " IWCashPrice, IWCreditPrice, IWRetailPrice, ICreatedDateTime, ICreatedBy, I

                        dtGridInvoice[1, invoiceGridRowNumberCurrent].Value = Convert.ToString(dtGridViewItemsList[2, i].Value); // description  
                        dtGridInvoice[2, invoiceGridRowNumberCurrent].Value = Convert.ToString(dtGridViewItemsList[1, i].Value); // Bar code  

                        if (rdBtnRetail.Checked)
                            dtGridInvoice[4, invoiceGridRowNumberCurrent].Value = Convert.ToString(dtGridViewItemsList[10, i].Value); // Rate
                        else if (rdbtnWholSaleCash.Checked)
                            dtGridInvoice[4, invoiceGridRowNumberCurrent].Value = Convert.ToString(dtGridViewItemsList[8, i].Value); // Rate
                        else if (rdBtnWholesalesCredit.Checked)
                            dtGridInvoice[4, invoiceGridRowNumberCurrent].Value = Convert.ToString(dtGridViewItemsList[9, i].Value); // Rate
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

        }


        DataTable dtSearchResult = null;
        bool bFoundCustomer = false;

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {
            if (!fromBackSpace)
            {

                string name = txtCustName.Text.Trim();
                if (name.Length > 2)
                {
                    this.Cursor = Cursors.WaitCursor;


                    CustomerDBLayer objCustomerDBLayer = new CustomerDBLayer();
                    string errorInfo = "";
                    try
                    {
                        dtSearchResult = objCustomerDBLayer.SerahcCustomer(txtCustName.Text.Trim(), txtMobile1.Text.Trim(), "", ref errorInfo);

                        if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                        {

                            listCustomerNames.DataSource = dtSearchResult;
                            listCustomerNames.DisplayMember = "CustName";
                            listCustomerNames.ValueMember = "CustID";

                            listCustomerNames.Visible = true;
                            bFoundCustomer = true;
                            listCustomerNames.Focus();
                        }
                        else
                        {
                            bFoundCustomer = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to populate user. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }

                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    listCustomerNames.Visible = false;
                }
            }
            fromBackSpace = false;

        }

        private void listCustomerNames_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtSearchResult.Rows)
                        {
                            if (Convert.ToString(item["CustID"]) == Convert.ToString(listCustomerNames.SelectedValue))
                            {
                                txtCustName.Text = Convert.ToString(item["CustName"]);
                                txtMobile1.Text = Convert.ToString(item["CustMobile1"]);
                                txtMobile2.Text = Convert.ToString(item["CustMobile2"]);
                                txtAddress.Text = Convert.ToString(item["CustTown"]) + "," + Convert.ToString(item["CustDist"]) + "," + Convert.ToString(item["CustState"]);
                                string errorInfo = "";
                                lblOldBalance.Text = Convert.ToString(GetCustOldBalance(Convert.ToInt16(item["CustID"]), ref errorInfo));
                                listCustomerNames.Visible = false;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string errorInfo = "";

            if (dtGridInvoice.RowCount > 1)
            {
                if (MessageBox.Show("This will save invoice as temporary file.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        callFromBtnSave = true;

                        this.Cursor = Cursors.WaitCursor;

                        InvoiceDO objInvoiceDO = new InvoiceDO();
                        objInvoiceDO.Title = InvoiceType;
                        objInvoiceDO.InvOrRepair = invoiceType;

                        // Collect customer details  // temp code
                        CustomerDO objCustomerDO = new CustomerDO();
                        objCustomerDO.CustName = txtCustName.Text.Trim();
                        objCustomerDO.CustMobile1 = txtMobile1.Text.Trim();
                        objCustomerDO.CustMobile2 = txtMobile2.Text.Trim();
                        objCustomerDO.CustTown = txtAddress.Text.Trim();
                        objInvoiceDO.InvNote = txtNote1.Text.Trim() + "\n" + txtNote2.Text.Trim() + "\n" + txtNote3.Text.Trim() + "\n" + txtNote4.Text.Trim();

                        try  // temp code remove ex handling
                        {
                            objCustomerDO.CustID = Convert.ToInt16(listCustomerNames.SelectedValue);
                        }
                        catch (Exception)
                        {

                        }

                        objInvoiceDO.CustomerDetails = objCustomerDO;
                        objInvoiceDO.SaleType = GetSaleType();

                        //if (invoiceType == AviMaConstants.InvoiceFlag)
                        //    objInvoiceDO.InvoiceNumber = objInvoiceDBLayer.GetInvoiceNumber("Invoice", ref errorInfo);
                        //else if (invoiceType == AviMaConstants.RepairFlag)
                        //    objInvoiceDO.InvoiceNumber = objInvoiceDBLayer.GetInvoiceNumber("Repair", ref errorInfo);
                        objInvoiceDO.InvoiceNumber = txtInvoiceNu.Text.Trim();

                        objInvoiceDO.UserLoginID = UserID;
                        objInvoiceDO.InvCreatedDate = AviMaConstants.CurrentDateTimesStamp;

                        List<InvoiceItemsDO> objItemsList = new List<InvoiceItemsDO>();
                        int j = 1;
                        for (int i = 0; i < dtGridInvoice.Rows.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(Convert.ToString(dtGridInvoice[1, i].Value).Trim()))
                            {
                                InvoiceItemsDO objInvoiceItems = new InvoiceItemsDO();
                                objInvoiceItems.SiNo = Convert.ToString(j);


                                objInvoiceItems.ItemName = Convert.ToString(dtGridInvoice["ItemName", i].Value);
                                objInvoiceItems.BarCode = Convert.ToString(dtGridInvoice["ItemBarCode", i].Value);
                                objInvoiceItems.ItemQnty = Convert.ToString(dtGridInvoice["Itemqnty", i].Value);
                                objInvoiceItems.ItemRate = Convert.ToString(dtGridInvoice["ITEMRATE", i].Value);
                                objInvoiceItems.Amount = Convert.ToString(dtGridInvoice["AMOUNT", i].Value);

                                DateTime dateTemp = Convert.ToDateTime(dtGridInvoice["InvDateTime", i].Value);
                                objInvoiceItems.CreatedDateTime = dateTemp;

                                objInvoiceItems.ItemSuplierID = "4"; // need to replace with actuall suplier ID
                                objItemsList.Add(objInvoiceItems);
                                j++;
                            }
                        }


                        objInvoiceDO.TotalAmount = txtGrnadTotal.Text.Trim();

                        objInvoiceDO.Discuont = txtDiscount.Text.Trim();
                        objInvoiceDO.GrandTotalAmount = txtGrnadTotal2.Text.Trim();


                        objInvoiceDO.TotalItems = txtTotalItems.Text.Trim();
                        objInvoiceDO.PackedBy = txtPackedBy.Text.Trim();

                        objInvoiceDO.ItemsList = objItemsList;

                        //Save invoice to DB
                        bool _checkDBHit = false;
                        string erroInfo = "";
                        _checkDBHit = objInvoiceDBLayer.CreateInvoice(objInvoiceDO, true, bFoundCustomer, ref erroInfo);

                        //if Save invoice to DB is success full then print it
                        if (_checkDBHit)
                        {
                            ClearControls();
                            erroInfo = "";

                            if (!invDataFromTempInv)
                            {
                                if (!objInvoiceDBLayer.UpdateInvoiceNumber("Invoice", ref erroInfo))
                                {
                                    MessageBox.Show("Unable to update invoice number. " + erroInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (invoiceType == AviMaConstants.InvoiceFlag)
                            {
                                string newInvNum = objInvoiceDBLayer.GetInvoiceNumber("Invoice", ref errorInfo);
                                if (!string.IsNullOrEmpty(newInvNum))
                                {
                                    txtInvoiceNu.Text = newInvNum;
                                }
                                invDataFromTempInv = false;
                            }

                            listCustomerNames.DataSource = new DataTable(); // while pausing billing it was picking previuos customer name even though customer details are not provided

                        }
                        else
                        {
                            MessageBox.Show("Unable to save temp invoice. " + erroInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to save temp invoice. " + ex.Message + " " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter at least one item to generate invoice", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        bool invDataFromTempInv = false;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("This will reload the entire Invoice page, please confirm.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    ClearControls(); // clear all invoice form controls before displaying temp invoices
                    ClearInvItemFields(); // clear all invoice form controls before displaying temp invoices


                    InvoiceData objInvoiceData = new InvoiceData(UserID);
                    this.Hide();
                    objInvoiceData.InvoiceType = AviMaConstants.TempInvoiceFlag;
                    objInvoiceData.ShowDialog();

                    InvoiceDO objInvoiceDetails = objInvoiceData.InvoiceDetails;

                    try
                    {
                        if (objInvoiceDetails != null && objInvoiceDetails.ItemsList.Count != 0)
                        {



                            txtInvoiceNu.Text = objInvoiceDetails.InvoiceNumber;

                            if (objInvoiceDetails.CustomerDetails != null)
                            {
                                txtCustName.Text = objInvoiceDetails.CustomerDetails.CustName;
                                txtMobile1.Text = objInvoiceDetails.CustomerDetails.CustMobile1;
                                txtMobile2.Text = objInvoiceDetails.CustomerDetails.CustMobile2;
                                txtAddress.Text = objInvoiceDetails.CustomerDetails.CustTown;
                            }

                            int siNo = 1;
                            int i = 0;

                            //temp code
                            //DataTable dt = new DataTable();
                            //dtGridInvoice.DataSource = dt;

                            dtGridInvoice.Rows.Add(objInvoiceDetails.ItemsList.Count);

                            foreach (InvoiceItemsDO item in objInvoiceDetails.ItemsList)
                            {




                                //objInvoiceItems.ItemName = Convert.ToString(dtGridInvoice["ItemName", i].Value);
                                //objInvoiceItems.BarCode = Convert.ToString(dtGridInvoice["ItemBarCode", i].Value);
                                //objInvoiceItems.ItemQnty = Convert.ToString(dtGridInvoice["Itemqnty", i].Value);
                                //objInvoiceItems.ItemRate = Convert.ToString(dtGridInvoice["ITEMRATE", i].Value);
                                //objInvoiceItems.Amount = Convert.ToString(dtGridInvoice["AMOUNT", i].Value);
                                //objInvoiceItems.CreatedDateTime = Convert.ToString(dtGridInvoice["InvDateTime", i].Value);



                                dtGridInvoice[0, i].Value = siNo;
                                dtGridInvoice["ItemName", i].Value = item.ItemName;
                                dtGridInvoice["ItemBarCode", i].Value = item.BarCode;
                                dtGridInvoice["Itemqnty", i].Value = item.ItemQnty;


                                if (ConfigValueLoader.CalculateItemPrice)
                                    dtGridInvoice["ITEMRATE", i].Value = Convert.ToString(Convert.ToDecimal(item.ItemRate) * 10);
                                else
                                    dtGridInvoice["ITEMRATE", i].Value = item.ItemRate;

                                if (ConfigValueLoader.CalculateItemPrice)
                                    dtGridInvoice["AMOUNT", i].Value = Convert.ToString(Convert.ToDecimal(item.Amount) * 10);
                                else
                                    dtGridInvoice["AMOUNT", i].Value = item.Amount;


                                dtGridInvoice["ItemDelete", i].Value = "Delete";

                                dtGridInvoice["InvDateTime", i].Value = item.CreatedDateTime;
                                siNo++;
                                i++;
                            }

                            dtGridInvoice[0, i].Value = siNo;

                            txtPackedBy.Text = objInvoiceDetails.PackedBy;
                            txtTotalItems.Text = objInvoiceDetails.TotalItems;
                            string[] noteList = objInvoiceDetails.InvNote.Split('\n');


                            try
                            {
                                txtNote1.Text = noteList[0];
                                txtNote2.Text = noteList[1];
                                txtNote3.Text = noteList[2];
                                txtNote4.Text = noteList[3];
                            }
                            catch
                            {
                                // don't do anything it's just index out of range exception 
                            }



                            if (ConfigValueLoader.CalculateItemPrice)
                                txtGrnadTotal.Text = Convert.ToString(Convert.ToDecimal(objInvoiceDetails.TotalAmount) * 10);
                            else
                                txtGrnadTotal.Text = objInvoiceDetails.TotalAmount;

                            if (ConfigValueLoader.CalculateItemPrice)
                                txtGrnadTotal2.Text = Convert.ToString(Convert.ToDecimal(objInvoiceDetails.GrandTotalAmount) * 10);
                            else
                                txtGrnadTotal2.Text = objInvoiceDetails.GrandTotalAmount;



                            txtDiscount.Text = objInvoiceDetails.Discuont;

                            if (ConfigValueLoader.CalculateItemPrice)
                                lblGrandTotal.Text = Convert.ToString(Convert.ToDecimal(txtGrnadTotal2.Text) * 10) + ".00";
                            else
                                lblGrandTotal.Text = txtGrnadTotal2.Text + ".00";


                            SetSaleType(objInvoiceDetails.SaleType);

                            #region Get existing balance
                            try
                            {
                                string errorInfo = "";
                                lblOldBalance.Text = Convert.ToString(GetCustOldBalance(Convert.ToInt16(objInvoiceDetails.CustomerDetails.CustID), ref errorInfo));
                            }
                            catch (Exception ex)
                            {
                                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                            }
                            #endregion Get existing balance

                            invDataFromTempInv = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }
                    // dtGridInvoice.Rows.RemoveAt(objInvoiceDetails.ItemsList.Count);

                    this.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to retrive temp invoice. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dtGridInvoice_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                if (dtGridInvoice.CurrentCell.ColumnIndex == 3) //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro occured in dtGridInvoice_EditingControlShowing. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            //}
        }

        private void txtMobile1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private decimal GetCustOldBalance(int custID, ref string errorInfo)
        {
            decimal OldBalanace = 0;

            try
            {
                LedgerDBLayer objLedgerDB = new LedgerDBLayer();

                DataTable dtAcountInfo = new DataTable();

                dtAcountInfo = objLedgerDB.SearchAccounts(custID, AviMaConstants.CustAccType, ref errorInfo);

                if (dtAcountInfo != null && dtAcountInfo.Rows.Count > 0)
                {
                    foreach (DataRow item in dtAcountInfo.Rows)
                    {
                        OldBalanace = Convert.ToDecimal(item["Balance"]);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "Calculate Balance", "Calculate Balance", ex.LineNumber(), this.FindForm().Name);
            }

            return OldBalanace;

        }

        private void listCustomerNames_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                {
                    foreach (DataRow item in dtSearchResult.Rows)
                    {
                        if (Convert.ToString(item["CustID"]) == Convert.ToString(listCustomerNames.SelectedValue))
                        {
                            txtCustName.Text = Convert.ToString(item["CustName"]);
                            txtMobile1.Text = Convert.ToString(item["CustMobile1"]);
                            txtMobile2.Text = Convert.ToString(item["CustMobile2"]);
                            txtAddress.Text = Convert.ToString(item["CustTown"]) + "," + Convert.ToString(item["CustDist"]) + "," + Convert.ToString(item["CustState"]);
                            string errorInfo = "";
                            lblOldBalance.Text = Convert.ToString(GetCustOldBalance(Convert.ToInt16(item["CustID"]), ref errorInfo));
                            listCustomerNames.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void rdbtnWholSaleCash_Click(object sender, EventArgs e)
        {
            // TO DO assign coresponding rate depending sale type
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txtGrnadTotal.Text) && !string.IsNullOrEmpty(txtDiscount.Text))
                //{
                //    txtGrnadTotal2.Text = Convert.ToString(Convert.ToDecimal(txtGrnadTotal.Text) - Convert.ToDecimal(txtDiscount.Text));
                //    lblGrandTotal.Text = txtGrnadTotal2.Text;
                //}
                //else if (string.IsNullOrEmpty(txtDiscount.Text) && !string.IsNullOrEmpty(txtGrnadTotal.Text))
                //{
                //    txtGrnadTotal2.Text = txtGrnadTotal.Text;
                //    lblGrandTotal.Text = txtGrnadTotal2.Text;
                //}

                //Above logic moved to method CalculateDiscount()
                CalculateDiscount();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        void CalculateDiscount()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtGrnadTotal2.Text))
                {
                    decimal totalAfterDiscount = 0;

                    if (!string.IsNullOrEmpty(txtGrnadTotal.Text) && txtGrnadTotal.Text != "0")
                        totalAfterDiscount += Convert.ToDecimal(txtGrnadTotal.Text);

                    if (!string.IsNullOrEmpty(txtDiscount.Text) && txtDiscount.Text != "0")
                        txtGrnadTotal2.Text = Convert.ToString(totalAfterDiscount - Convert.ToDecimal(txtDiscount.Text));
                    else
                        txtGrnadTotal2.Text = Convert.ToString(totalAfterDiscount);

                    lblGrandTotal.Text = txtGrnadTotal2.Text;
                }
                else if (string.IsNullOrEmpty(txtDiscount.Text) && !string.IsNullOrEmpty(txtGrnadTotal.Text))
                {
                    txtGrnadTotal2.Text = txtGrnadTotal.Text;
                    lblGrandTotal.Text = txtGrnadTotal2.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "CalculateDiscount()", "CalculateDiscount()", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            //}
        }


        #region new enhnacement

        bool _detectReader = false; // to avoid enter key event during scanning bar code

        private void txtInvBarCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtInvBarCode.Text.Length == 4)
                {
                    btnInvAdd.Enabled = false;

                    if (dtStockDtlsForInv != null && dtStockDtlsForInv.Rows.Count > 0)
                    {
                        DataRow[] dtItemInfo = dtStockDtlsForInv.Select("IBarCode = '" + txtInvBarCode.Text.Trim() + "'");

                        foreach (DataRow item in dtItemInfo)
                        {
                            //IName,IQnty,IWCashPrice,IWCreditPrice,IWRetailPrice
                            txtInvName.Text = Convert.ToString(item["IName"]); // description    

                            if (rdBtnRetail.Checked)
                                txtInvItemRate.Text = Convert.ToString(item["IWRetailPrice"]); // Rate
                            else if (rdbtnWholSaleCash.Checked)
                                txtInvItemRate.Text = Convert.ToString(item["IWCashPrice"]); // Rate
                            else if (rdBtnWholesalesCredit.Checked)
                                txtInvItemRate.Text = Convert.ToString(item["IWCreditPrice"]); // Rate


                            #region price multiplication factor
                            if (ConfigValueLoader.CalculateItemPrice)
                                txtInvItemRate.Text = Convert.ToString(Convert.ToDecimal(txtInvItemRate.Text) * 10);
                            else
                                txtInvItemRate.Text = Convert.ToString(Convert.ToDecimal(txtInvItemRate.Text));
                            #endregion price multiplication factor


                            txtInvQnty.Text = "1";

                            txtInvTotalAmnt.Text = txtInvItemRate.Text;

                            txtInvQnty.Focus();
                            txtInvQnty.SelectAll();
                            break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtInvQnty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvItemRate.Text.Trim()))
                {
                    if (validateForDecimal(txtInvItemRate.Text.Trim()))
                    {
                        if (!string.IsNullOrEmpty(txtInvQnty.Text) && !string.IsNullOrEmpty(txtInvItemRate.Text))
                        {
                            txtInvTotalAmnt.Text = Convert.ToString(Convert.ToInt64(txtInvQnty.Text) * Convert.ToDecimal(txtInvItemRate.Text));
                        }

                        btnInvAdd.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Please enter valida decimal number");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        Regex r = new Regex(@"^-{0,1}\d+\.{0,1}\d*$"); // This is the main part, can be altered to match any desired form or limitations
        private bool validateForDecimal(string text)
        {
            bool check = true;
            try
            {
                if (!r.Match(text).Success)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile(ex.Message, ex.StackTrace, "", ex.LineNumber(), this.FindForm().Name);
            }
            return check;
        }


        // int siNumber = 1;
        private void btnInvAdd_Click(object sender, EventArgs e)
        {
            int actualGridRows = dtGridInvoice.Rows.Count;

            if (_detectReader)
            {
                if (!string.IsNullOrEmpty(txtInvName.Text) && !string.IsNullOrEmpty(txtInvBarCode.Text) && !string.IsNullOrEmpty(txtInvQnty.Text)
                 && !string.IsNullOrEmpty(txtInvQnty.Text) && !string.IsNullOrEmpty(txtInvItemRate.Text))
                {
                    if (!validateForDecimal(txtInvItemRate.Text))
                    {
                        MessageBox.Show("Please enter valid decimal number");
                        return;
                    }


                    try
                    {

                        #region grid item update logic


                        bool bFoundItemInGrid = false;
                        int rowCountFrmAddBtn = 0;

                        int _nextSerialNum = 0;

                        for (int i = 0; i < dtGridInvoice.Rows.Count; i++)
                        {
                            if (!String.IsNullOrEmpty(Convert.ToString(dtGridInvoice[1, i].Value).Trim()))
                            {
                                try
                                {
                                    _nextSerialNum = Convert.ToInt16(dtGridInvoice[0, i].Value);
                                    rowCountFrmAddBtn += 1;
                                }
                                catch (Exception ex)
                                {
                                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                                }


                                // check item already added to the inv grid
                                //if (txtInvBarCode.Text.Trim() == Convert.ToString(dtGridInvoice[2, i].Value).Trim())
                                //{
                                //    rowCountFrmAddBtn = i;
                                //    bFoundItemInGrid = true;
                                //    break;
                                //}

                                bFoundItemInGrid = false;
                            }
                            else
                            {
                                dtGridInvoice[0, rowCountFrmAddBtn].Value = "";
                                dtGridInvoice[6, rowCountFrmAddBtn].Value = "";
                            }
                        }

                        #endregion grid item update logic

                        //IName,IQnty,IWCashPrice,IWCreditPrice,IWRetailPrice 

                        if (!bFoundItemInGrid)
                        {
                            dtGridInvoice.Rows.Add();
                            dtGridInvoice["SINO", rowCountFrmAddBtn].Value = _nextSerialNum + 1;
                            dtGridInvoice["ItemName", rowCountFrmAddBtn].Value = txtInvName.Text; // description      
                            dtGridInvoice["ItemBarCode", rowCountFrmAddBtn].Value = txtInvBarCode.Text; // 
                        }
                        dtGridInvoice["Itemqnty", rowCountFrmAddBtn].Value = txtInvQnty.Text; //                   
                        dtGridInvoice["ITEMRATE", rowCountFrmAddBtn].Value = txtInvItemRate.Text; // Rate
                        dtGridInvoice["AMOUNT", rowCountFrmAddBtn].Value = txtInvTotalAmnt.Text; // 
                        dtGridInvoice["ItemDelete", rowCountFrmAddBtn].Value = "Delete";
                        dtGridInvoice["InvDateTime", rowCountFrmAddBtn].Value = DateTime.Now;
                        //rowCountFrmAddBtn++;
                        //siNumber++;

                        ClearInvItemFields();
                        txtInvBarCode.Focus();

                        CalculateTotalQntyAndAmount(); // re calculate total qnty and amount


                        //Scenario  :1) Enter discount amount first 
                        //2) Start adding items
                        // 3) Grnad total is not getting deducted with discount amount entered first.
                        CalculateDiscount();

                        _detectReader = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }
                }
                else
                {
                    MessageBox.Show(ValidationUtility.GetValidationString(" at least one item."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //txtInvBarCode.Focus();
                }
            }
            else
                _detectReader = true;


            #region Code to show the latest row added with auto scroll

            try
            {

                //   'Scroll to the last row.
                this.dtGridInvoice.FirstDisplayedScrollingRowIndex = dtGridInvoice.RowCount - 1;

                //   'Select the last row.
                //  this.dtGridInvoice.Rows[actualGridRows - 1].Selected = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

            #endregion Code to show the latest row added with auto scroll

        }



        private void ClearInvItemFields()
        {
            txtInvBarCode.Text = "0000";
            txtInvName.Text = ""; // description   
            //  txtInvBarCode.Text = ""; //  
            txtInvQnty.Text = "";  //               
            txtInvItemRate.Text = "";  // Rate
            txtInvTotalAmnt.Text = "";  // 
        }


        private void txtInvBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnInvAdd_Click(sender, e);
            }
        }

        #endregion new enhnacement

        private void txtInvQnty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
        bool fromBackSpace = false;
        private void txtCustName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                fromBackSpace = true;

                txtAddress.Text = string.Empty;
                txtMobile1.Text = string.Empty;
                txtMobile2.Text = string.Empty;
                txtAddress.Text = string.Empty;
                lblOldBalance.Text = "0";
            }
        }
    }
}
