using AviMa.DataBaseLayer;
using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AviMa.AviMaForms
{
    public partial class InvoiceData : Form
    {
        public string UserID { get; set; }
        string errorInfo = "";
        public InvoiceDO InvoiceDetails { get; set; }

        public InvoiceData(string userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        public char InvoiceType { get; set; }
        InvoiceDBLayer objInvoiceDBLayer = new InvoiceDBLayer();
        CustomerDBLayer objCustomerDBLayer = new CustomerDBLayer();
        DataTable dtCustomerInfo = null;
        private void InvoiceData_Load(object sender, EventArgs e)
        {
            if (dtCustomerInfo == null)
                dtCustomerInfo = new DataTable();

            dtCustomerInfo = MasterCache.GetCustomerDataFrmCache(ref errorInfo);
            RefreshGrid();


        }

        private void RefreshGrid()
        {

            try
            {
                DataTable dt = new DataTable();
                string errorInfo = "";
                if (InvoiceType == AviMaConstants.InvoiceFlag)
                {

                    dt = objInvoiceDBLayer.GetAllInvoices(AviMaConstants.InvoiceFlag, ref errorInfo);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No invoice data available." + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }

                    btnDelete.Visible = false; // enable only for temp invoices
                }
                else if (InvoiceType == AviMaConstants.TempInvoiceFlag)
                {
                    dt = objInvoiceDBLayer.GetAllInvoices(AviMaConstants.TempInvoiceFlag, ref errorInfo);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No invoice data available.All temp invoice data deleted." + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }

                    btnDelete.Visible = true; // enable only for temp invoices

                }
                else if (InvoiceType == AviMaConstants.RepairFlag)
                {

                    this.Text = "Repair Orders";

                    dt = objInvoiceDBLayer.GetAllInvoices(AviMaConstants.RepairFlag, ref errorInfo);
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No service request(s) available." + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }

                    btnDelete.Visible = false; // enable only for temp invoices
                }

                DataColumn objColCustName = new DataColumn("CustomerName");
                dt.Columns.Add(objColCustName);


                foreach (DataRow po in dt.Rows)
                {
                    foreach (DataRow customer in dtCustomerInfo.Rows)
                    {
                        if (Convert.ToString(po["InvCustId"]) == Convert.ToString(customer["CustID"]))
                        {
                            po["CustomerName"] = customer["CustName"];
                        }
                    }
                }


                dataGridInvDetails.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }

        }




        private void dataGridInvDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;

            if (x != -1)
            {
               // this.Hide();
                try
                {
                    String invId = Convert.ToString((dataGridInvDetails["InvID", x].Value));
                    string _custName = Convert.ToString((dataGridInvDetails["CustomerName", x].Value)); ; // To display cust name name item details page

                    if (!string.IsNullOrEmpty(invId))
                    {
                        DataTable objDtInvData = new DataTable();

                        if (InvoiceType == AviMaConstants.TempInvoiceFlag)
                        {
                            string errorInfo = "";
                            objDtInvData = objInvoiceDBLayer.GetInvoice(invId, InvoiceType, ref errorInfo);

                            if (objDtInvData != null && objDtInvData.Rows.Count > 0)
                            {
                                if (InvoiceDetails == null) ;
                                InvoiceDetails = new InvoiceDO();

                                //InvID, InvSaleType, InvTotalItems, InvTotalAmount, InvCustID, InvCreatedBy, InvCreatedDate, InvModifiedBy, InvModifiedDate

                                InvoiceDetails.InvoiceNumber = Convert.ToString((dataGridInvDetails["InvID", x].Value));
                                InvoiceDetails.SaleType = Convert.ToString((dataGridInvDetails["InvSaleType", x].Value));
                                InvoiceDetails.TotalItems = Convert.ToString((dataGridInvDetails["InvTotalItems", x].Value));
                                InvoiceDetails.TotalAmount = Convert.ToString((dataGridInvDetails["InvTotalAmount", x].Value));
                                InvoiceDetails.InvNote = Convert.ToString((dataGridInvDetails["InvNote", x].Value));
                                InvoiceDetails.GrandTotalAmount = Convert.ToString((dataGridInvDetails["InvGrandTotalAmount", x].Value));
                                InvoiceDetails.Discuont = Convert.ToString((dataGridInvDetails["InvDiscount", x].Value));
                                int customerID = Convert.ToInt16((dataGridInvDetails["InvCustID", x].Value));



                                errorInfo = "";
                                InvoiceDetails.CustomerDetails = new CustomerDO();
                                InvoiceDetails.CustomerDetails = objCustomerDBLayer.GetCustomer(customerID, ref errorInfo);
                                if (InvoiceDetails.CustomerDetails != null)
                                {
                                    InvoiceDetails.CustomerDetails.CustID = customerID;
                                }
                                InvoiceDetails.InvOrRepair = InvoiceType;
                                // InvoiceDetails.Title = Convert.ToString((dataGridInvDetails[0, x].Value));
                                InvoiceDetails.PackedBy = Convert.ToString((dataGridInvDetails["InvCreatedBy", x].Value));


                                InvoiceDetails.ItemsList = new List<InvoiceItemsDO>();

                                foreach (DataRow item in objDtInvData.Rows)
                                {
                                    InvoiceItemsDO _item = new InvoiceItemsDO();
                                    _item.SiNo = Convert.ToString(item["InvID"]);
                                    _item.BarCode = Convert.ToString(item["InvBarCode"]);
                                    _item.ItemName = Convert.ToString(item["InvName"]);
                                    _item.ItemQnty = Convert.ToString(item["InvQnty"]);
                                    _item.ItemRate = Convert.ToString(item["InvRate"]);
                                    _item.ItemSuplierID = Convert.ToString(item["InvSupID"]);
                                    _item.Amount = Convert.ToString(item["InvTotalAmont"]);
                                    _item.CreatedDateTime = Convert.ToDateTime(item["InvDateTime"]);
                                    InvoiceDetails.ItemsList.Add(_item);
                                }

                            }
                            this.Close();
                        }
                        else
                        {
                            string errorInfo = "";
                            objDtInvData = objInvoiceDBLayer.GetInvoice(invId, InvoiceType, ref errorInfo);

                            if (objDtInvData != null && objDtInvData.Rows.Count > 0)
                            {
                                InvoiceItemData objItemData = new InvoiceItemData(objDtInvData);
                                objItemData.lblCustName.Text = _custName;
                                this.Hide();
                                objItemData.ShowDialog();
                                this.Show();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to get invoice details. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
                finally
                {

                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtDetails == null)
                    dtDetails = new DataTable();


                string invoiceID = txtSearchInvNo.Text;
                string fromDate = Convert.ToDateTime(dateFromSearch.Text).ToString("yyyy-MM-dd");
                string toDate = Convert.ToDateTime(dateToSearch.Text).ToString("yyyy-MM-dd");
                string _cmbSaleType = cmbSaleType.Text;
                string errorInfo = "";
                dtDetails = objInvoiceDBLayer.SearchInvoice(0, fromDate, toDate, invoiceID, _cmbSaleType, false, InvoiceType, ref errorInfo);
                if (dtDetails == null || dtDetails.Rows.Count == 0)
                {
                    MessageBox.Show("No results found for your search criteria", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }



                DataColumn objColCustName = new DataColumn("CustomerName");
                dtDetails.Columns.Add(objColCustName);


                foreach (DataRow po in dtDetails.Rows)
                {
                    foreach (DataRow customer in dtCustomerInfo.Rows)
                    {
                        if (Convert.ToString(po["InvCustId"]) == Convert.ToString(customer["CustID"]))
                        {
                            po["CustomerName"] = customer["CustName"];
                        }
                    }
                }

                dataGridInvDetails.DataSource = dtDetails;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to search invoice details. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
        DataTable dtDetails = null;
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear search fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    txtSearchInvNo.Text = "";
                    dateFromSearch.ResetText();
                    dateToSearch.ResetText();
                    cmbSaleType.Text = "-Select-";
                    string errorInfo = "";

                    DataTable dtdetails = objInvoiceDBLayer.GetAllInvoices(InvoiceType, ref errorInfo);

                    DataColumn objColCustName = new DataColumn("CustomerName");
                    dtdetails.Columns.Add(objColCustName);


                    foreach (DataRow po in dtdetails.Rows)
                    {
                        foreach (DataRow customer in dtCustomerInfo.Rows)
                        {
                            if (Convert.ToString(po["InvCustId"]) == Convert.ToString(customer["CustID"]))
                            {
                                po["CustomerName"] = customer["CustName"];
                            }
                        }
                    }

                    dataGridInvDetails.DataSource = dtdetails;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
            }
        }


        #region Invoice Re-Print

        int RowIndexForReprinting = -1;
        private void dataGridInvDetails_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (InvoiceType == AviMaConstants.InvoiceFlag)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        int currentMouseOverRow = dataGridInvDetails.HitTest(e.X, e.Y).RowIndex;

                        if (currentMouseOverRow != -1)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dataGridInvDetails["InvID", currentMouseOverRow].Value)))
                            {

                                ContextMenu m = new ContextMenu();
                                MenuItem objItem = new MenuItem("Re-Print");
                                objItem.Click += objItem_Click;
                                m.MenuItems.Add(objItem);
                                MenuItem objDeleteItem = new MenuItem("Delete");
                                objDeleteItem.Click += ObjDeleteItem_Click; ;
                                m.MenuItems.Add(objDeleteItem);


                                dataGridInvDetails.ClearSelection();
                                dataGridInvDetails.Rows[currentMouseOverRow].Selected = true;

                                if (currentMouseOverRow >= 0)
                                {
                                    RowIndexForReprinting = currentMouseOverRow; ;
                                }

                                m.Show(dataGridInvDetails, new Point(e.X, e.Y));
                            }
                        }
                    }
                }
                else if (InvoiceType == AviMaConstants.TempInvoiceFlag)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        int currentMouseOverRow = dataGridInvDetails.HitTest(e.X, e.Y).RowIndex;

                        if (currentMouseOverRow != -1)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dataGridInvDetails["InvID", currentMouseOverRow].Value)))
                            {

                                ContextMenu m = new ContextMenu();                                
                                MenuItem objDeleteItem = new MenuItem("Delete");
                                objDeleteItem.Click += ObjDeleteItem_Click1;
                                m.MenuItems.Add(objDeleteItem);


                                dataGridInvDetails.ClearSelection();
                                dataGridInvDetails.Rows[currentMouseOverRow].Selected = true;

                                if (currentMouseOverRow >= 0)
                                {
                                    RowIndexForReprinting = currentMouseOverRow; ;
                                }

                                m.Show(dataGridInvDetails, new Point(e.X, e.Y));
                            }
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

        private void ObjDeleteItem_Click1(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Delete temporary invoice.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string invNubers = Convert.ToString(dataGridInvDetails[0, RowIndexForReprinting].Value);
                    bool _check = objInvoiceDBLayer.DeleteTempInvoice("'" + invNubers + "'", ref errorInfo);


                    if (_check)
                    {
                        MessageBox.Show("Temporary Invoice deleted successfully. " + errorInfo, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshGrid();
                    }
                    else
                    {
                        MessageBox.Show("Unable to deleted temporary Invoice." + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        void objItem_Click(object sender, EventArgs e)
        {
            try
            {
                InvoiceDO objInvoiceDO = new InvoiceDO();
                string _errorInfo = "";
                // InvID 0, InvSaleType, InvOrRepair 2, InvTotalItems, InvTotalAmount, InvCustID 5, InvCreatedBy, InvCreatedDate, InvGrandTotalAmount 8, InvDiscount

                objInvoiceDO.InvoiceNumber = Convert.ToString(dataGridInvDetails["InvID", RowIndexForReprinting].Value);
                objInvoiceDO.SaleType = Convert.ToString(dataGridInvDetails["InvSaleType", RowIndexForReprinting].Value);
                objInvoiceDO.InvOrRepair = Convert.ToChar(dataGridInvDetails["InvOrRepair", RowIndexForReprinting].Value);
                objInvoiceDO.TotalItems = Convert.ToString(dataGridInvDetails["InvTotalItems", RowIndexForReprinting].Value);

                objInvoiceDO.InvNote = Convert.ToString((dataGridInvDetails["InvNote", RowIndexForReprinting].Value));

                string TotalAmount = "0";
                string GrandTotalAmount = "0";

                if (ConfigValueLoader.CalculateItemPrice)
                {
                    TotalAmount = Convert.ToString(Convert.ToDecimal(dataGridInvDetails["InvTotalAmount", RowIndexForReprinting].Value) * 10);
                    GrandTotalAmount = Convert.ToString(Convert.ToDecimal(dataGridInvDetails["InvGrandTotalAmount", RowIndexForReprinting].Value) * 10);
                }
                else
                {
                    TotalAmount = Convert.ToString(dataGridInvDetails["InvTotalAmount", RowIndexForReprinting].Value);
                    GrandTotalAmount = Convert.ToString(dataGridInvDetails["InvGrandTotalAmount", RowIndexForReprinting].Value);
                }

                objInvoiceDO.TotalAmount = TotalAmount;
                objInvoiceDO.GrandTotalAmount = GrandTotalAmount;
                objInvoiceDO.Discuont = Convert.ToString(dataGridInvDetails["InvDiscount", RowIndexForReprinting].Value);
                objInvoiceDO.PackedBy = Convert.ToString(dataGridInvDetails["InvCreatedBy", RowIndexForReprinting].Value);
                objInvoiceDO.Title = AviMaConstants.INVOICE;
                objInvoiceDO.InvOrRepair = AviMaConstants.InvoiceFlag;

                objInvoiceDO.FoundCustomer = true;

                CustomerDO objCustomerDO = new CustomerDO();

                try  // temp code remove ex handling
                {
                    objCustomerDO.CustID = Convert.ToInt16(dataGridInvDetails["InvCustID", RowIndexForReprinting].Value);
                    DataTable dtCustomer = new DataTable();
                    dtCustomer = MasterCache.GetCustomerDataFrmCache(ref _errorInfo);

                    DataRow[] dtRow = dtCustomer.Select("CustID = " + objCustomerDO.CustID);

                    foreach (DataRow customer in dtRow)
                    {
                        objCustomerDO.CustName = Convert.ToString(customer["CustName"]);
                        objCustomerDO.CustMobile1 = Convert.ToString(customer["CustMobile1"]);
                        objCustomerDO.CustMobile2 = Convert.ToString(customer["CustMobile2"]);
                        objCustomerDO.CustTown = Convert.ToString(customer["CustTown"]);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
                objInvoiceDO.CustomerDetails = objCustomerDO;

                objInvoiceDO.UserLoginID = UserID;
                objInvoiceDO.InvCreatedBy = UserID;
                objInvoiceDO.InvCreatedDate = AviMaConstants.CurrentDateTimesStamp;


                List<InvoiceItemsDO> objItemsList = new List<InvoiceItemsDO>();

                DataTable objDtInvData = new DataTable();
                objDtInvData = objInvoiceDBLayer.GetInvoice(objInvoiceDO.InvoiceNumber, AviMaConstants.InvoiceFlag, ref _errorInfo);


                int j = 1;

                //InvID, InvBarCode, InvName, InvDescription, InvQnty, InvRate, InvTotalAmont, InvSupID, InvDateTime, InvCreatedBy, InvModifiedDate, InvModifiedBy, `Column 13`, `Column 14`

                foreach (DataRow item in objDtInvData.Rows)
                {

                    string ItemRate = "0";
                    string Amount = "0";

                    if (ConfigValueLoader.CalculateItemPrice)
                    {
                        ItemRate = Convert.ToString(Convert.ToDecimal(item["InvRate"]) * 10);
                        Amount = Convert.ToString(Convert.ToDecimal(item["InvTotalAmont"]) * 10);
                    }
                    else
                    {
                        ItemRate = Convert.ToString(item["InvRate"]);
                        Amount = Convert.ToString(item["InvTotalAmont"]);
                    }

                    InvoiceItemsDO objInvoiceItems = new InvoiceItemsDO();
                    objInvoiceItems.SiNo = Convert.ToString(j);
                    objInvoiceItems.ItemName = Convert.ToString(item["InvName"]);
                    objInvoiceItems.BarCode = Convert.ToString(item["InvBarCode"]);
                    objInvoiceItems.ItemQnty = Convert.ToString(item["InvQnty"]);
                    objInvoiceItems.CreatedDateTime = Convert.ToDateTime(item["InvDateTime"]);
                    objInvoiceItems.ItemRate = ItemRate;
                    objInvoiceItems.Amount = Amount;


                    objInvoiceItems.ItemSuplierID = Convert.ToString(item["InvSupID"]);
                    objItemsList.Add(objInvoiceItems);
                    j++;
                }

                objInvoiceDO.ItemsList = objItemsList;

                PrintInvoice objPrintInvoice = new PrintInvoice(objInvoiceDO);
                bool checkPrintSuccssfull = objPrintInvoice.PrintReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
        private void ObjDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Delete invoice.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string invNubers = Convert.ToString(dataGridInvDetails[0, RowIndexForReprinting].Value);
                    bool _check = objInvoiceDBLayer.DeleteInvoice("'" + invNubers + "'", ref errorInfo);


                    if (_check)
                    {                        
                        MessageBox.Show("Invoice deleted successfully. " + errorInfo, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshGrid();
                    }
                    else
                    {
                        MessageBox.Show("Unable to deleted Invoice." + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
        #endregion Invoice Re-Print

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string listOfSearchInvIds = "";

            try
            {
                if (MessageBox.Show("Delete paused invoice data.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    try
                    {
                        for (int i = 0; i < dataGridInvDetails.Rows.Count; i++)
                        {
                            listOfSearchInvIds += "'" + Convert.ToString(dataGridInvDetails["InvID", i].Value) + "' ,";
                        }

                        listOfSearchInvIds = listOfSearchInvIds.Substring(0, listOfSearchInvIds.Length - 1);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }

                    if (listOfSearchInvIds != "")
                    {
                        string errorInfo = "";

                        bool check = objInvoiceDBLayer.DeleteTempInvoice(listOfSearchInvIds, ref errorInfo);

                        if (check)
                        {
                            RefreshGrid();

                            MessageBox.Show("Paused Invoice(s) deleted succssfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to delete Paused invoice(s). " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    }
}
