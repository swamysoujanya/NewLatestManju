using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using AviMa.DataBaseLayer;
using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;

namespace AviMa.AviMaUserControls
{
    public partial class stkReturnUserControl : UserControl
    {
        public string UserID { get; set; }
        public stkReturnUserControl(string userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtSearchName.Text) || !string.IsNullOrEmpty(txtSearchBarCode.Text) || txtSearchSupplier.Text != "-Select-" )
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    string errorInfo = "";
                    int suplierID = 0;
                    if(txtSearchSupplier.Text != "-Select-")
                    suplierID = Convert.ToInt16(txtSearchSupplier.SelectedValue);
                    

                    //string fromDate = Convert.ToDateTime(dateFrom.Text).ToString("yyyyMMdd");
                    //string toDate = Convert.ToDateTime(dateTo.Text).ToString("yyyyMMdd");


                    //txtSearchBarCode
                    //txtSearchName
                    //txtSearchSupplier


                    stStockDetails = new DataTable();
                    stStockDetails = objStockDBLayer.SerahcStock(txtSearchName.Text, txtSearchBarCode.Text, suplierID, ref errorInfo , AviMaConstants.ShowStockReport);

                    if (stStockDetails != null && stStockDetails.Rows.Count > 0 && errorInfo == string.Empty)
                    {
                        RefreshGrid(stStockDetails);
                    }
                    else
                    {
                        stStockDetails = new DataTable();
                        RefreshGrid(stStockDetails);
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

        DataTable dtSuppliersTable = null;
        DataTable stStockDetails = null;
        StockDBLayer objStockDBLayer = new StockDBLayer();

        private void stkReturnUserControl_Load(object sender, EventArgs e)
        {
            try
            {
                //Bind supplier data to combo box
                string erroInfo = "";

                if (dtSuppliersTable != null)
                    dtSuppliersTable = new DataTable();

                //dtSuppliersTable = objStockDBLayer.GetSuppliers(ref erroInfo);
                dtSuppliersTable = MasterCache.GetSupplierFrmCache(ref erroInfo);

                if (dtSuppliersTable != null && dtSuppliersTable.Rows.Count > 0)
                {

                    txtSearchSupplier.DataSource = dtSuppliersTable;
                    txtSearchSupplier.ValueMember = "SupID";
                    txtSearchSupplier.DisplayMember = "SupName";

                    txtSearchSupplier.Text = "-Select-";
                }

                RefreshGrid();
               
                ClearAllEditFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void ClearAllEditFields()
        {
            txtBarCodeEdit.Text = "";
            txtNameEdit.Text = "";
            txtDescripEdit.Text = "";
            txtQntyEdit.Text = "0";
            cmbSupplierEdit.Text = "-Select-";
            txtWSCashPriceEdit.Text = "0";
            txtWSCreditPriceEdit.Text = "0";
            txtRetailPriceEdit.Text = "0";
            txtPurchasePriceEdit.Text = "0";

        }
        DataTable dtSupplierInfo = null;
        private void RefreshGrid()
        {
            string errorInfo = "";
            try
            {
                stStockDetails = objStockDBLayer.GetAllItems(ref errorInfo , AviMaConstants.ShowStockReport);

                if (dtSupplierInfo == null)
                {
                    dtSupplierInfo = new DataTable();
                    dtSupplierInfo = MasterCache.GetSupplierFrmCache(ref errorInfo);
                }

                DataColumn objColSupName = new DataColumn("SupplierName");
                stStockDetails.Columns.Add(objColSupName);

                Int32 stkTotalAmount = 0; //calculate total Amount
                Int32 stkTotalItems = 0; //calculate total number of items

                foreach (DataRow po in stStockDetails.Rows)
                {
                    foreach (DataRow supplier in dtSupplierInfo.Rows)
                    {
                        if (Convert.ToString(po["ISupID"]) == Convert.ToString(supplier["SupID"]))
                        {
                            po["SupplierName"] = supplier["SupName"];
                        }
                    }
                    stkTotalAmount += Convert.ToInt32(po["IPurchasePrice"]); //calculate total Amount
                    stkTotalItems += Convert.ToInt32(po["IQnty"]); //calculate total number of items

                }
                dtGridStock.DataSource = stStockDetails;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to refresh stock details data. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }








            //string errorInfo = "";
            //try
            //{

            //    stStockDetails = new DataTable();
            //    dtGridStock.DataSource = stStockDetails;
            //    stStockDetails = objStockDBLayer.GetAllItems(ref errorInfo);

            //    if (stStockDetails != null && stStockDetails.Rows.Count > 0)
            //    {
            //        dtGridStock.DataSource = stStockDetails;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message + " " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error); dtGridStock.DataSource = stStockDetails;
            //    Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            //}
        }

        private void RefreshGrid(DataTable stStockDetails)
        {
            string errorInfo = "";
            try
            {
                //stStockDetails = objStockDBLayer.GetAllItems(ref errorInfo);

                if (dtSupplierInfo == null)
                {
                    dtSupplierInfo = new DataTable();
                    dtSupplierInfo = MasterCache.GetSupplierFrmCache(ref errorInfo);
                }

                DataColumn objColSupName = new DataColumn("SupplierName");
                stStockDetails.Columns.Add(objColSupName);

                Int32 stkTotalAmount = 0; //calculate total Amount
                Int32 stkTotalItems = 0; //calculate total number of items

                foreach (DataRow po in stStockDetails.Rows)
                {
                    foreach (DataRow supplier in dtSupplierInfo.Rows)
                    {
                        if (Convert.ToString(po["ISupID"]) == Convert.ToString(supplier["SupID"]))
                        {
                            po["SupplierName"] = supplier["SupName"];
                        }
                    }
                    stkTotalAmount += Convert.ToInt32(po["IPurchasePrice"]); //calculate total Amount
                    stkTotalItems += Convert.ToInt32(po["IQnty"]); //calculate total number of items

                }
                dtGridStock.DataSource = stStockDetails;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to refresh stock details data. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear search fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                txtSearchName.Text = "";
                txtSearchBarCode.Text = "";
                txtSearchSupplier.Text = "-Select-";
                RefreshGrid();
            }
        }

        int rowclicked = -1;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidatEditStock())
            {
                if (MessageBox.Show("Update stock.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        ItemsDO objItemsDO = new ItemsDO();

                        //objItemsDO.BarCode = txtBarCodeEdit.Text.Trim();
                        //objItemsDO.Name = txtNameEdit.Text.Trim();
                        //objItemsDO.Description = txtDescripEdit.Text.Trim();
                        if (rowclicked != -1)
                        {
                            objItemsDO.Qnty = Convert.ToInt16(txtQntyEdit.Text.Trim()) + Convert.ToInt16(dtGridStock[4, rowclicked].Value.ToString());
                            objItemsDO.ItemID = Convert.ToInt16(dtGridStock[0, rowclicked].Value.ToString());
                        }
                        else
                            objItemsDO.Qnty = Convert.ToInt16(txtQntyEdit.Text.Trim());

                        //objItemsDO.SupID = Convert.ToInt32(cmbSupplierEdit.SelectedValue);
                        //objItemsDO.WCashPrice = Convert.ToDecimal(txtWSCashPriceEdit.Text.Trim());
                        //objItemsDO.WCreditPrice = Convert.ToDecimal(txtWSCreditPriceEdit.Text.Trim());
                        //objItemsDO.WRetailPrice = Convert.ToDecimal(txtRetailPriceEdit.Text.Trim());
                        objItemsDO.ModifiedDate = AviMaConstants.CurrentDateTimesStamp;
                        objItemsDO.ModifiedBy = UserID;

                        string errorInfo = "";
                        bool _check = objStockDBLayer.UpdateQnty(objItemsDO, ref errorInfo);

                        if (_check)
                        {
                            ClearAllEditFields();
                            RefreshGrid();
                            MessageBox.Show("Item qnty updated successfully", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Unable to update item qnty. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }
                }
            }
        }

        private bool ValidatEditStock()
        {
            bool _check = true;

            try
            {
                if (string.IsNullOrEmpty(txtBarCodeEdit.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("item bar code."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtBarCodeEdit.Focus();
                }
                else if (string.IsNullOrEmpty(txtNameEdit.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("item name."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtNameEdit.Focus();
                }
                else if (string.IsNullOrEmpty(txtQntyEdit.Text) || txtQntyEdit.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("item qnty."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtQntyEdit.Focus();
                }
                else if (string.IsNullOrEmpty(txtWSCashPriceEdit.Text) || txtWSCashPriceEdit.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("whole sale cash price."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCashPriceEdit.Focus();
                }
                else if (string.IsNullOrEmpty(txtWSCreditPriceEdit.Text) || txtWSCreditPriceEdit.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("whole sale credit price."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCreditPriceEdit.Focus();
                }
                else if (string.IsNullOrEmpty(txtRetailPriceEdit.Text) || txtRetailPriceEdit.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("retail price."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtRetailPriceEdit.Focus();
                }
                else if (string.IsNullOrEmpty(cmbSupplierEdit.Text) || cmbSupplierEdit.Text.Trim() == "-Select-")
                {
                    MessageBox.Show("Please select supplier name", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    cmbSupplierEdit.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name); ;
            }

            return _check;

        }

        private void btnUpdCancel_Click(object sender, EventArgs e)
        {
            ClearAllEditFields();
        }

        private void dtGridStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;
                string errorInfo = "";
                if (i != -1)
                {
                    rowclicked = i;
                    //  IID, IBarCode, IName, IDescription, IQnty, ISupID, IWCashPrice, IWCreditPrice, IWRetailPrice, ICreatedDateTime, ICreatedBy, IModifiedDate, IModifiedBy

                    txtBarCodeEdit.Text = dtGridStock[1, i].Value.ToString();
                    txtNameEdit.Text = dtGridStock[2, i].Value.ToString();
                    txtDescripEdit.Text = dtGridStock[3, i].Value.ToString();
                    //  txtQntyEdit.Text = dtGridStock[4, i].Value.ToString();

                    string supName = objStockDBLayer.GetASupplier(Convert.ToInt64(dtGridStock[7, i].Value.ToString()), ref errorInfo);
                    cmbSupplierEdit.Text = supName;


                    txtPurchasePriceEdit.Text = dtGridStock[5, i].Value.ToString();
                    txtWSCashPriceEdit.Text = dtGridStock[6, i].Value.ToString();
                    txtWSCreditPriceEdit.Text = dtGridStock[7, i].Value.ToString();
                    txtRetailPriceEdit.Text = dtGridStock[8, i].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        #region Main_144

        private void txtQntyEdit_KeyPress(object sender, KeyPressEventArgs e)
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

        #endregion Main_144
    }
}
