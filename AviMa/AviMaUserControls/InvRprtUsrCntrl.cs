using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AviMa.DataBaseLayer;
using AviMa.UtilityLayer;
using AviMa.DataObjectLayer;
using AviMa.AviMaForms;

namespace AviMa.AviMaUserControls
{
    public partial class InvRprtUsrCntrl : UserControl
    {
        public InvRprtUsrCntrl(string UserID)
        {
            InitializeComponent();
        }

        DataTable dtCustomerDetails = null;
        DataTable dtInvoiceDetails = null;
        InvoiceDBLayer objInvoiceDBLayer = new InvoiceDBLayer();
        CustomerDBLayer objCustomerDBLayer = new CustomerDBLayer();
        private void InvRprtUsrCntrl_Load(object sender, EventArgs e)
        {
            try
            {
                //Bind supplier data to combo box
                //string erroInfo = "";
                //if (dtCustomerDetails == null)
                //    dtCustomerDetails = new DataTable();

                //dtCustomerDetails = objCustomerDBLayer.GetAllCustomers(ref erroInfo);
                //if (dtCustomerDetails != null && dtCustomerDetails.Rows.Count > 0)
                //{

                //    cmbSupplier.DataSource = dtCustomerDetails;
                //    cmbSupplier.ValueMember = "CustID";
                //    cmbSupplier.DisplayMember = "CustName";


                //    //cmbSupplier.Items.Add("-Select-");
                //    cmbSupplier.Text = "-Select-";
                //}
                PopulateCustomerCombo(); 

               // LoadData();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to refresh invoice details data. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        #region Main_90
        public void  LoadData()  
        {
            string errorInfo = "";
            if (dtInvoiceDetails == null)
                dtInvoiceDetails = new DataTable();
            dtInvoiceDetails = objInvoiceDBLayer.GetAllInvoices(AviMaConstants.InvoiceFlag, ref errorInfo);
            RefreshGrid(dtInvoiceDetails);
        }

        #endregion Main_90

        public void PopulateCustomerCombo()
        {
            try
            {
                string erroInfo = "";

                if (dtCustomerDetails == null)
                    dtCustomerDetails = new DataTable();

                dtCustomerDetails = objCustomerDBLayer.GetAllCustomers(ref erroInfo);
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

        private void RefreshGrid(DataTable dtInvoiceDetails)
        {
            try
            {
                DataColumn objColSupName = new DataColumn("CustName");
                dtInvoiceDetails.Columns.Add(objColSupName);

                Int32 invTotalAmount = 0; //calculate total Amount

                foreach (DataRow invoice in dtInvoiceDetails.Rows)
                {
                    foreach (DataRow cutomer in dtCustomerDetails.Rows)
                    {
                        if (Convert.ToString(invoice["InvCustID"]) == Convert.ToString(cutomer["CustID"]))
                        {
                            invoice["CustName"] = cutomer["CustName"];
                        }
                    }

                    invTotalAmount += Convert.ToInt32(invoice["InvTotalAmount"]); //calculate total Amount
                   
                }
                dataGridPOReport.DataSource = dtInvoiceDetails;
                txtGrnadTotal.Text = Convert.ToString(invTotalAmount); // calculate total Amount
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to refresh invoice details data. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

       
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string errorInfo = "";

                int customerID = Convert.ToInt16(cmbSupplier.SelectedValue);
                string fromDate = Convert.ToDateTime(dateFrom.Text).ToString("yyyy-MM-dd");
                string toDate = Convert.ToDateTime(dateTo.Text).ToString("yyyy-MM-dd");


                //temp Code
                //DataRow[] _tempdtInvoiceDetails = new DataRow();
                //_tempdtInvoiceDetails = dtInvoiceDetails.Select("where CustID = " + customerID + "  or  (InvCreatedDate BETWEEN  '" + fromDate + "' and   '" + toDate + "')");// );  //= objInvoiceDBLayer.SearchPO(suplierID, fromDate, toDate, ref errorInfo);

                dtInvoiceDetails = new DataTable();
                dtInvoiceDetails = objInvoiceDBLayer.SearchInvoice(customerID, fromDate, toDate, "", "", true,AviMaConstants.InvoiceFlag, ref errorInfo);

                if (errorInfo == string.Empty && dtInvoiceDetails != null && dtInvoiceDetails.Rows.Count > 0 )
                {
                    RefreshGrid(dtInvoiceDetails);
                }
                else
                {
                    dtInvoiceDetails = new DataTable();
                    RefreshGrid(dtInvoiceDetails);
                    MessageBox.Show("No results found for your search criteria. " + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private bool PrintReport( ref string listOfSearchInvIds)
        {
            bool check = false;

            try
            {

                ReportPrintDO _ObjReportPrintDO = new ReportPrintDO();
                _ObjReportPrintDO.RptDetails = dtInvoiceDetails;
                _ObjReportPrintDO.RptTotal = txtGrnadTotal.Text;
                _ObjReportPrintDO.RPTName1 = "Sales Report";
                if (cmbSupplier.Text != "-Select-")
                    _ObjReportPrintDO.RPTName2 = cmbSupplier.Text;
                _ObjReportPrintDO.RptDateTime = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");

                PrintInvoiceRprt objGenaralReport = new PrintInvoiceRprt(_ObjReportPrintDO);

                check = objGenaralReport.PrintReport(ref listOfSearchInvIds);

                txtGrnadTotal.Text = ""; // clear total amount after generating invoice
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
                check = false;
            }
            return check;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear search fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                cmbSupplier.Text = "-Select-";
                dateFrom.ResetText();
                dateTo.ResetText();
                string erroInfo = "";
                dtInvoiceDetails = objInvoiceDBLayer.GetAllInvoices(AviMaConstants.InvoiceFlag, ref erroInfo);
                RefreshGrid(dtInvoiceDetails);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Print report.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                PrintReport(ref listOfSearchInvIds);
            }
        }

        string listOfSearchInvIds = "";

        private void btnPrintAndDelete_Click(object sender, EventArgs e)
        {
    
            listOfSearchInvIds = "";

            try
            {
                if (MessageBox.Show("Print and delete data.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (PrintReport(ref listOfSearchInvIds))
                    {
                        try
                        {
                            listOfSearchInvIds = listOfSearchInvIds.Substring(0, listOfSearchInvIds.Length - 1);
                        }
                        catch (Exception ex)
                        {
                            Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                        }

                        if (listOfSearchInvIds != "")
                        {
                            string errorInfo = "";

                            bool check = objInvoiceDBLayer.DeleteInvoice(listOfSearchInvIds, ref errorInfo);

                            if (check)
                            {
                                MessageBox.Show("Invoice(s) deleted succssfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Unable to delete invoice(s). " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dataGridPOReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int x = e.RowIndex;

                if (x != -1)
                {
                    this.Hide();
                    try
                    {
                        String invId = Convert.ToString((dataGridPOReport["InvID", x].Value));
                        if (!string.IsNullOrEmpty(invId))
                        {
                            DataTable objDtInvData = new DataTable();
                            string errorInfo = "";
                            objDtInvData = objInvoiceDBLayer.GetInvoice(invId, AviMaConstants.InvoiceFlag, ref errorInfo);

                            if (objDtInvData != null && objDtInvData.Rows.Count > 0)
                            {
                                InvoiceItemData OBJInvoiceItemData = new InvoiceItemData(objDtInvData);
                                OBJInvoiceItemData.lblCustName.Text = Convert.ToString((dataGridPOReport["CustName", x].Value));
                                OBJInvoiceItemData.ShowDialog();   
                            }
                            else
                            {
                                MessageBox.Show("No item details associated with this invoice. " , "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        this.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to get invoice details. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {

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
