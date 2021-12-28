using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AviMa.DataBaseLayer;
using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using AviMa.AviMaForms;

namespace AviMa.AviMaUserControls
{
    public partial class PORprtUsrCntrl : UserControl
    {
        StockDBLayer objStockDBLayer = new StockDBLayer();

        public PORprtUsrCntrl(string UserID)
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            try
            {
                string errorInfo = "";
                int suplierID = 0;
                if (cmbSupplier.Text != "-Select-")
                    suplierID = Convert.ToInt16(cmbSupplier.SelectedValue);


                string fromDate = Convert.ToDateTime(dateFrom.Text).ToString("yyyy-MM-dd");
                string toDate = Convert.ToDateTime(dateTo.Text).ToString("yyyy-MM-dd");


                dtPODetails = new DataTable();
                dtPODetails = objStockDBLayer.SearchPO(suplierID, fromDate, toDate, ref errorInfo);

                if (dtPODetails != null && dtPODetails.Rows.Count > 0 && errorInfo == string.Empty)
                {
                    RefreshGrid(dtPODetails);
                }
                else
                {
                    dtPODetails = new DataTable();
                    RefreshGrid(dtPODetails);
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

        private void RefreshGrid(DataTable dtPODetails)
        {
            try
            {
                txtGrnadTotal.Text = "";

                if (dtPODetails != null && dtPODetails.Rows.Count != 0)
                {
                    DataColumn objColSupName = new DataColumn("SupplierName");
                    dtPODetails.Columns.Add(objColSupName);

                    Int32 posTotalAmount = 0; //calculate total Amount

                    foreach (DataRow po in dtPODetails.Rows)
                    {
                        foreach (DataRow supplier in dtSupplierInfo.Rows)
                        {
                            if (Convert.ToString(po["posupplierID"]) == Convert.ToString(supplier["SupID"]) && Convert.ToString(supplier["SupName"]) != "-Select-")
                            {
                                po["SupplierName"] = supplier["SupName"];
                            }
                        }
                        posTotalAmount += Convert.ToInt32(po["poTotalAmount"]); //calculate total Amount

                    }
                    txtGrnadTotal.Text = Convert.ToString(posTotalAmount); // calculate total Amount
                }

                dataGridPOReport.DataSource = dtPODetails;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to refresh PO details data. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        DataTable dtSupplierInfo = null;
        DataTable dtPODetails = null;
        private void PORprtUsrCntrl_Load(object sender, EventArgs e)
        {

            try
            {

                //Bind supplier data to combo box
                string erroInfo = "";
                if (dtSupplierInfo == null)
                    dtSupplierInfo = new DataTable();

                // dtSupplierInfo = objStockDBLayer.GetSuppliers(ref erroInfo);
                dtSupplierInfo = MasterCache.GetSupplierFrmCache(ref erroInfo);




                if (dtSupplierInfo != null && dtSupplierInfo.Rows.Count > 0)
                {

                    cmbSupplier.DataSource = dtSupplierInfo;
                    cmbSupplier.ValueMember = "SupID";
                    cmbSupplier.DisplayMember = "SupName";


                    cmbSupplier.Text = "-Select-";
                }

                string errorInfo = "";
                if (dtPODetails == null)
                    dtPODetails = new DataTable();
                dtPODetails = objStockDBLayer.GetPOs(ref errorInfo);
                RefreshGrid(dtPODetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to refresh invoice details data. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Print report.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                PrintReport();
            }
        }

        private void btnPrintAndDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Print and delete data.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                string poNumbers = "";
                if (dtPODetails != null && dtPODetails.Rows.Count > 0)
                {
                    foreach (DataRow po in dtPODetails.Rows)
                    {
                        poNumbers += Convert.ToString(po["ponumber"]) + ",";
                    }

                    if (PrintReport())
                    {
                        try
                        {

                            poNumbers = poNumbers.Substring(0, poNumbers.Length - 1);

                            string errorInfo = "";

                            bool check = objStockDBLayer.DeletePO(poNumbers, ref errorInfo);

                            if (check)
                            {
                                MessageBox.Show("Purchase orders deleted succssfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Unable to delete Purchase orders. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Unable to delete Purchase orders. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                        }

                    }
                }
            }
        }

        private bool PrintReport()
        {
            bool check = false;

            try
            {
                if (dtPODetails != null && dtPODetails.Rows.Count > 0)
                {
                    ReportPrintDO _ObjReportPrintDO = new ReportPrintDO();
                    _ObjReportPrintDO.RptDetails = dtPODetails;
                    _ObjReportPrintDO.RptTotal = txtGrnadTotal.Text;
                    _ObjReportPrintDO.RPTName1 = "Purchase Order Report";
                    if (cmbSupplier.Text != "-Select-")
                        _ObjReportPrintDO.RPTName2 = cmbSupplier.Text;
                    _ObjReportPrintDO.RptDateTime = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");

                    GenaralReport objGenaralReport = new GenaralReport(_ObjReportPrintDO);
                    check = objGenaralReport.PrintReport();
                }
            }
            catch (Exception ex)
            {
                check = false;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
            return check;
        }

        private void dataGridPOReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = e.RowIndex;

            if (x != -1)
            {

                try
                {
                    String poNumber = Convert.ToString((dataGridPOReport["ponumber", x].Value));
                    if (!string.IsNullOrEmpty(poNumber))
                    {
                        DataTable objDtInvData = new DataTable();

                        string errorInfo = "";
                        objDtInvData = objStockDBLayer.GetStockItemDetails(poNumber, ref errorInfo);

                        if (objDtInvData != null && objDtInvData.Rows.Count > 0)
                        {
                            InvoiceItemData OBJInvoiceItemData = new InvoiceItemData(objDtInvData);
                            this.Hide();
                            OBJInvoiceItemData.label1.Text = "Supplier Name:";
                            OBJInvoiceItemData.lblCustName.Text = Convert.ToString((dataGridPOReport["SupplierName", x].Value));
                            OBJInvoiceItemData.ShowDialog();                           
                            this.Show();

                        }
                        else
                        {
                            MessageBox.Show("No items found for selected PO. " + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear search fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                cmbSupplier.Text = "-Select-";
                dateFrom.ResetText();
                dateTo.ResetText();
                string errorInfo = "";
                if (dtPODetails == null)
                    dtPODetails = new DataTable();
                dtPODetails = objStockDBLayer.GetPOs(ref errorInfo);
                RefreshGrid(dtPODetails);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete PO details.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                string poNumbers = "";
                if (dtPODetails != null && dtPODetails.Rows.Count > 0)
                {
                    foreach (DataRow po in dtPODetails.Rows)
                    {
                        poNumbers += Convert.ToString(po["ponumber"]) + ",";
                    }

                    try
                    {

                        poNumbers = poNumbers.Substring(0, poNumbers.Length - 1);

                        string errorInfo = "";

                        bool check = objStockDBLayer.DeletePO(poNumbers, ref errorInfo);

                        if (check)
                        {
                            if (dtPODetails == null)
                                dtPODetails = new DataTable();
                            dtPODetails = objStockDBLayer.GetPOs(ref errorInfo);
                            RefreshGrid(dtPODetails);


                            MessageBox.Show("Purchase orders deleted succssfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to delete Purchase orders. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to delete Purchase orders. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }


                }
            }
        }
    }
}
