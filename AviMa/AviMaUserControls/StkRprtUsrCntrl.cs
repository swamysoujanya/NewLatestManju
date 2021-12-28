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
using BarcodeRender;

namespace AviMa.AviMaUserControls
{
    public partial class StkRprtUsrCntrl : UserControl
    {
        public StkRprtUsrCntrl(string UserID)
        {
            InitializeComponent();
        }
        public string ReportType { get; set; }


        BarCodeDataList objBarCodeDataList = null;

        DataTable dtSupplierInfo = new DataTable();
        StockDBLayer objStockDBLayer = new StockDBLayer();
        DataTable dtStockDetails = new DataTable();
        BarcodeTestForm objFrm = null;

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchName.Text) || !string.IsNullOrEmpty(txtSearchBarCode.Text) || cmbSupplier.Text != "-Select-" || !string.IsNullOrEmpty(cmbSupplier.Text))
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    string errorInfo = "";
                    int suplierID = 0;
                    if (cmbSupplier.Text != "-Select-" && cmbSupplier.Text != "")
                        suplierID = Convert.ToInt16(cmbSupplier.SelectedValue);
                    dtStockDetails = new DataTable();
                    dtStockDetails = objStockDBLayer.SerahcStock(txtSearchName.Text, txtSearchBarCode.Text, suplierID, ref errorInfo, ReportType);

                    if (dtStockDetails != null && dtStockDetails.Rows.Count > 0 && errorInfo == string.Empty)
                    {
                        RefreshGrid(dtStockDetails);
                    }
                    else
                    {
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
            else
            {
                MessageBox.Show("Please select or enter at least one search field", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSearchName.Focus();
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear search fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                cmbSupplier.Text = "-Select-";
                txtSearchName.Text = "";
                txtSearchBarCode.Text = "";
                dateFrom.ResetText();
                dateTo.ResetText();
                string errorInfo = "";

                if (ReportType == AviMaConstants.ShowStockReport)
                {
                    // string errorInfo = "";
                    if (dtStockDetails == null)
                        dtStockDetails = new DataTable();
                    dtStockDetails = objStockDBLayer.GetAllItems(ref errorInfo, AviMaConstants.ShowStockReport);
                    RefreshGrid(dtStockDetails);
                }
                else if (ReportType == AviMaConstants.ReGenerateBarCode)
                {
                    //  string errorInfo = "";
                    if (dtStockDetails == null)
                        dtStockDetails = new DataTable();
                    dtStockDetails = objStockDBLayer.GetAllItems(ref errorInfo, AviMaConstants.ReGenerateBarCode);
                    RefreshGrid(dtStockDetails);
                }
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

            string errorInfo = "";
            bool validationFiled = true;
            try
            {
                if (dataGridPOReport != null && dataGridPOReport.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridPOReport.Rows.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(dataGridPOReport["IBarCode", i].Value).Trim()))
                        {
                            if (!String.IsNullOrEmpty(Convert.ToString(dataGridPOReport["BarCodeQnty", i].Value).Trim()))
                            {
                                string barcode = Convert.ToString(dataGridPOReport["IBarCode", i].Value);
                                string pricecode = Convert.ToString(dataGridPOReport["IPriceCode", i].Value);
                                string itemname = Convert.ToString(dataGridPOReport["IName", i].Value);
                                string qnty = Convert.ToString(dataGridPOReport["BarCodeQnty", i].Value);
                                GetBarCodeDetails(barcode, pricecode, itemname, qnty, ref errorInfo);
                                validationFiled = true;
                            }
                        }
                    }


                    #region BarCode Printing
                    if (validationFiled)
                    {
                        errorInfo = "";
                        objFrm = new BarcodeTestForm(false);
                        objFrm.barCodeDataList = objBarCodeDataList;
                        objFrm.PrintBarCodes(ref errorInfo);
                        objBarCodeDataList = new BarCodeDataList();
                    }
                    else
                    {
                        MessageBox.Show("Please enter at least required barcode qnty for one item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        validationFiled = false;
                    }
                    #endregion BarCode Printing
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }



        private void GetBarCodeDetails(string barcode, string pricecode, string itemname, string qnty, ref string errorInfo)
        {
            try
            {
                #region For Bar Code Printing

                if (objBarCodeDataList == null)
                    objBarCodeDataList = new BarCodeDataList();

                BarcodeData _objBarcodeData = new BarcodeData();
                _objBarcodeData.BarCode = barcode;
                _objBarcodeData.ItemName = itemname;
                _objBarcodeData.PriceCode = pricecode;
                _objBarcodeData.Qntity = qnty;

                if (objBarCodeDataList.ListOfBarcodeData == null)
                    objBarCodeDataList.ListOfBarcodeData = new List<BarcodeData>();

                objBarCodeDataList.ListOfBarcodeData.Add(_objBarcodeData);

                #endregion For Bar Code Printing
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to get barcode details . " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private bool PrintReport()
        {
            bool check = false;

            try
            {
                if (dtStockDetails != null && dtStockDetails.Rows.Count > 0)
                {
                    ReportPrintDO _ObjReportPrintDO = new ReportPrintDO();
                    _ObjReportPrintDO.RptDetails = dtStockDetails;
                    _ObjReportPrintDO.RptTotal = txtGrnadTotal.Text;
                    _ObjReportPrintDO.RPTName1 = "Stock Report";
                    if (cmbSupplier.Text != "-Select-")
                        _ObjReportPrintDO.RPTName2 = cmbSupplier.Text;
                    _ObjReportPrintDO.RptDateTime = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");

                    PrintStkReport objGenaralReport = new PrintStkReport(_ObjReportPrintDO);
                    check = objGenaralReport.PrintReport();

                    txtGrnadTotal.Text = "";
                    txtTotalItems.Text = "";
                }
            }
            catch (Exception ex)
            {
                check = false;
                MessageBox.Show("Unable to print report. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
            return check;
        }

        private void StkRprtUsrCntrl_Load(object sender, EventArgs e)
        {

            //Bind supplier data to combo box
            string erroInfo = "";
            if (dtSupplierInfo == null)
                dtSupplierInfo = new DataTable();

            // dtSupplierInfo = objStockDBLayer.GetSuppliers(ref erroInfo);
            dtSupplierInfo = MasterCache.GetSupplierFrmCache(ref erroInfo);


            cmbSupplier.Items.Add("-Select-");
            if (dtSupplierInfo != null && dtSupplierInfo.Rows.Count > 0)
            {

                cmbSupplier.DataSource = dtSupplierInfo;
                cmbSupplier.ValueMember = "SupID";
                cmbSupplier.DisplayMember = "SupName";


                cmbSupplier.Text = "-Select-";
            }

            if (ReportType == AviMaConstants.ShowStockReport)
            {
                pnlStkRptSumary.Visible = true;
                btnPrint.Visible = true;
                btnRegenBarCode.Visible = false;
                string errorInfo = "";
                if (dtStockDetails == null)
                    dtStockDetails = new DataTable();
                dtStockDetails = objStockDBLayer.GetAllItems(ref errorInfo, AviMaConstants.ShowStockReport);
                RefreshGrid(dtStockDetails);
            }
            else if (ReportType == AviMaConstants.ReGenerateBarCode)
            {
                pnlStkRptSumary.Visible = false;
                btnPrint.Visible = false;
                btnRegenBarCode.Visible = true;
                string errorInfo = "";
                if (dtStockDetails == null)
                    dtStockDetails = new DataTable();
                dtStockDetails = objStockDBLayer.GetAllItems(ref errorInfo, AviMaConstants.ReGenerateBarCode);
                RefreshGrid(dtStockDetails);
            }

        }

        private void RefreshGrid(DataTable dtStockDetails)
        {
            try
            {
                DataColumn objColSupName = new DataColumn("SupplierName");
                dtStockDetails.Columns.Add(objColSupName);

                Int64 stkTotalAmount = 0; //calculate total Amount
                Int64 stkTotalItems = 0; //calculate total number of items

                foreach (DataRow po in dtStockDetails.Rows)
                {
                    foreach (DataRow supplier in dtSupplierInfo.Rows)
                    {
                        if (Convert.ToString(po["ISupID"]) == Convert.ToString(supplier["SupID"]))
                        {
                            po["SupplierName"] = supplier["SupName"];
                        }
                    }

                    if (ReportType == AviMaConstants.ShowStockReport)
                    {
                        stkTotalAmount += Convert.ToInt32(po["IPurchasePrice"]) * Convert.ToInt32(po["IQnty"]); //calculate total Amount
                        stkTotalItems += Convert.ToInt32(po["IQnty"]); //calculate total number of items
                    }

                }
                dataGridPOReport.DataSource = dtStockDetails;
                if (ReportType == AviMaConstants.ShowStockReport)
                {
                    txtGrnadTotal.Text = Convert.ToString(stkTotalAmount); // display total Amount
                    txtTotalItems.Text = Convert.ToString(stkTotalItems); // display total  number of items
                    dataGridPOReport.ReadOnly = true;
                }
                else
                {
                    dataGridPOReport.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to refresh stock details data. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridPOReport_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (ReportType == AviMaConstants.ReGenerateBarCode)
            {
                try
                {
                    e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                    if (dataGridPOReport.CurrentCell.ColumnIndex == 6) //Desired Column
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
                    MessageBox.Show("Erro occured in dataGridPOReport_EditingControlShowing. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
            }
        }

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
