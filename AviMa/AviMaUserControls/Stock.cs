using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AviMa.DataObjectLayer;
using AviMa.DataBaseLayer;
using AviMa.UtilityLayer;
using BarcodeRender;
using System.Text.RegularExpressions;

namespace AviMa.AviMaUserControls
{
    public partial class Stock : UserControl
    {
        public string UserID { get; set; }

        public Stock(string userID)
        {
            InitializeComponent();

            UserID = userID;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        StockDBLayer objStockDBLayer = new StockDBLayer();
        DataTable stStockDetails = null;
        BarCodeDataList objBarCodeDataList = null;
        BarcodeTestForm objFrm = null;
        List<ItemsDO> ItemsList = null;
        DataTable dtBulkItemsCreation = null;
        int bulkSiNo = 1;
        int nextBarCode = 0;
        bool isValidStockAdded = false;
        Regex r = new Regex(@"^-{0,1}\d+\.{0,1}\d*$"); // This is the main part, can be altered to match any desired form or limitations
        DataTable dtPODetails = null;
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (isValidStockAdded || ValidatCreateStock())
            {
                if (MessageBox.Show("Create stock.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {

                        string createdDateTime = AviMaConstants.CurrentDateTimesStamp;

                        PODetailsDO objPODetailsDO = new PODetailsDO();

                        objPODetailsDO.IsAddWithPO = chkBoxAddWithPO.Checked;

                        if (objPODetailsDO.IsAddWithPO)
                        {
                            if (cmbBoxPOSupplier.Text != "-Select-")
                                objPODetailsDO.POSupplierID = Convert.ToInt16(cmbBoxPOSupplier.SelectedValue);
                            objPODetailsDO.POReference = txtPOReferenceNo.Text;
                            objPODetailsDO.PODate = Convert.ToDateTime(dtPODate.Text).ToString("yyyyMMdd");

                        }

                        objPODetailsDO.POItemsList = new List<ItemsDO>();
                        objPODetailsDO.POCreatedDateTime = createdDateTime;
                        objPODetailsDO.POCreatedBy = UserID;

                        try
                        {
                            if (!string.IsNullOrEmpty(txtPOTotalAmnt.Text))
                                objPODetailsDO.poTotalAmount = Convert.ToInt64(txtPOTotalAmnt.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show(ValidationUtility.GetValidationString("valid PO total amount."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtPOTotalAmnt.Focus();
                            return;
                        }


                        if (ItemsList != null)
                        {
                            objPODetailsDO.POItemsList = ItemsList;
                        }
                        else
                        {

                            ItemsDO objItemsDO = new ItemsDO();

                            objItemsDO.BarCode = txtBarCodeCrea.Text.Trim();
                            objItemsDO.Name = txtNameCrea.Text.Trim();
                            objItemsDO.Description = txtDescripCrea.Text.Trim();
                            objItemsDO.Qnty = Convert.ToInt16(txtQntyCrea.Text.Trim());

                            if (!string.IsNullOrEmpty(txtPurchasePrice.Text))
                                objItemsDO.IPurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);

                            objItemsDO.SupID = Convert.ToInt64(cmbSupplier.SelectedValue);
                            objItemsDO.WCashPrice = Convert.ToDecimal(txtWSCashPriceCrea.Text.Trim());
                            objItemsDO.WCreditPrice = Convert.ToDecimal(txtWSCreditPriceCrea.Text.Trim());
                            objItemsDO.WRetailPrice = Convert.ToDecimal(txtRetailPriceCrea.Text.Trim());
                            objItemsDO.CreatedDateTime = AviMaConstants.CurrentDateTimesStamp;
                            objItemsDO.CreatedBy = UserID;

                            string _errorInfo = "";
                            GetBarCodeDetails(txtBarCodeCrea.Text.Trim(), txtPriceCodeCreate.Text.Trim(), txtNameCrea.Text.Trim(), txtQntyCrea.Text.Trim(), ref _errorInfo);

                            objPODetailsDO.POItemsList.Add(objItemsDO);
                        }

                        string errorInfo = "";
                        #region to update items against exiting PO 
                        bool _reUsePO = chkUseExistingPO.Checked;

                        if (_reUsePO)
                        {
                            objPODetailsDO.POID = Convert.ToInt16(cmbComboPONumbers.Text);
                        }

                        #endregion to update items against exiting PO

                        bool _check = objStockDBLayer.CreateItem(objPODetailsDO, _reUsePO, ref errorInfo);

                        if (_check)
                        {
                            ClearAllCreateFields(false);
                            RefreshGrid();

                            ItemsList = new List<ItemsDO>();
                            #region display bulk items creation to the user
                            dtBulkItemsCreation = null;
                            dataBulcItem.DataSource = dtBulkItemsCreation;
                            bulkSiNo = 1;
                            isValidStockAdded = false;
                            #endregion display bulk items creation to the user

                            MasterCache.SetStockForInvoice(ref errorInfo); // refresh stocklist ofr invoice

                            MessageBox.Show("Item created successfully", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);


                            #region BarCode Printing
                            //BarcodeTestForm objForm = new BarcodeTestForm();
                            //this.Hide();
                            //objForm.barCodeDataList = objBarCodeDataList;
                            //objForm.ShowDialog();
                            //this.Show();
                            errorInfo = "";
                            objFrm = new BarcodeTestForm(false);
                            objFrm.barCodeDataList = objBarCodeDataList;
                            objFrm.PrintBarCodes(ref errorInfo);
                            objBarCodeDataList = new BarCodeDataList();
                            #endregion BarCode Printing

                        }
                        else
                        {
                            MessageBox.Show("Unable to create item. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to create item. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }
                }
            }
        }

        private void ClearAllCreateFields(bool isFrmAdd)
        {

            //txtBarCodeCrea.Text = "";
            txtNameCrea.Text = "";
            txtDescripCrea.Text = "";
            txtQntyCrea.Text = "0";
            cmbSupplier.Text = "-Select-";
            txtWSCashPriceCrea.Text = "0";
            txtWSCreditPriceCrea.Text = "0";
            txtRetailPriceCrea.Text = "0";
            txtPurchasePrice.Text = "0";
            txtPriceCodeCreate.Text = "";


            if (!isFrmAdd)
            {
                txtPOTotalAmnt.Text = "";
                txtPOReferenceNo.Text = "";
                dtPODate.ResetText();
                txtSearchSupplier.Text = "-Select-";
                cmbBoxPOSupplier.Text = "-Select-";

                //on cancel reset barcode values
                nextBarCode = 0;
                AssignNewBarCodeID(nextBarCode);

            }
        }

        private bool ValidatCreateStock()
        {
            bool _check = true;

            try
            {


                if (string.IsNullOrEmpty(txtBarCodeCrea.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("item bar code."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtBarCodeCrea.Focus();
                }

                else if (txtBarCodeCrea.Text.Length != 4)
                {
                    MessageBox.Show("Bar code should be 4 digits.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtBarCodeCrea.Focus();
                }
                else if (string.IsNullOrEmpty(txtNameCrea.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("item name."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtNameCrea.Focus();
                }
                else if (string.IsNullOrEmpty(txtQntyCrea.Text) || txtQntyCrea.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("item qnty."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtQntyCrea.Focus();
                }
                else if (!string.IsNullOrEmpty(txtPurchasePrice.Text) && !r.Match(txtPurchasePrice.Text).Success)
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtPurchasePrice.Focus();
                }
                else if (string.IsNullOrEmpty(txtWSCashPriceCrea.Text) || txtWSCashPriceCrea.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("whole sale cash price."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCashPriceCrea.Focus();
                }
                else if (!r.Match(txtWSCashPriceCrea.Text).Success)
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("  only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCashPriceCrea.Focus();
                }
                else if (string.IsNullOrEmpty(txtWSCreditPriceCrea.Text) || txtWSCreditPriceCrea.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("whole sale credit price."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCreditPriceCrea.Focus();
                }
                else if (!r.Match(txtWSCreditPriceCrea.Text).Success)
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("  only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCreditPriceCrea.Focus();
                }
                else if (string.IsNullOrEmpty(txtRetailPriceCrea.Text) || txtRetailPriceCrea.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("retail price."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtRetailPriceCrea.Focus();
                }
                else if (!r.Match(txtRetailPriceCrea.Text).Success)
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("  only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtRetailPriceCrea.Focus();
                }
                else if (string.IsNullOrEmpty(cmbSupplier.Text) || cmbSupplier.Text.Trim() == "-Select-")
                {
                    MessageBox.Show("Please select supplier name", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    cmbSupplier.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create item. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
            return _check;

        }

        private void ClearAllDelFields()
        {
            txtBarCodeDel.Text = "";
            txtNameDel.Text = "";
            txtDescripDel.Text = "";
            txtQntyDel.Text = "0";
            //cmbSupplier.Text = "-Select-";
            //cmbBoxPOSupplier.Text = "-Select-";
            txtWSCashPriceDel.Text = "0";
            txtWSCreditPriceDel.Text = "0";
            txtRetailPriceDel.Text = "0";
            txtPurchPriceDele.Text = "0";
            txtSuppplierDel.Text = "";
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
            txtPriceCodeEdit.Text = "0";

        }


        private bool ValidateSearchFields()
        {

            bool _check = true;

            try
            {
                if (string.IsNullOrEmpty(txtSearchName.Text) && string.IsNullOrEmpty(txtSearchBarCode.Text) && txtSearchSupplier.Text.Trim() == "-Select-")
                {
                    MessageBox.Show("Please enter atleast one search criteria", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtBarCodeCrea.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create item. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }

            return _check;
        }
        private bool ValidatEditStock()
        {
            bool _check = true;

            try
            {
                if (string.IsNullOrEmpty(txtBarCodeEdit.Text))
                {
                    MessageBox.Show("Please select at least one item from the left side grid", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtNameEdit.Focus();
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
                else if (!string.IsNullOrEmpty(txtPurchasePriceEdit.Text) && !r.Match(txtPurchasePriceEdit.Text).Success)
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtPurchasePriceEdit.Focus();
                }
                else if (string.IsNullOrEmpty(txtWSCashPriceEdit.Text) || txtWSCashPriceEdit.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("whole sale cash price."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCashPriceEdit.Focus();
                }
                else if (!r.Match(txtWSCashPriceEdit.Text).Success)
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("  only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCashPriceEdit.Focus();
                }
                else if (string.IsNullOrEmpty(txtWSCreditPriceEdit.Text) || txtWSCreditPriceEdit.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("whole sale credit price."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCreditPriceEdit.Focus();
                }
                else if (!r.Match(txtWSCreditPriceEdit.Text).Success)
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("  only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtWSCreditPriceEdit.Focus();
                }
                else if (string.IsNullOrEmpty(txtRetailPriceEdit.Text) || txtRetailPriceEdit.Text.Trim() == "0")
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("retail price."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtRetailPriceEdit.Focus();
                }
                else if (!r.Match(txtRetailPriceEdit.Text).Success)
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("  only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Unable to create item. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }

            return _check;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllCreateFields(false);
            ItemsList = new List<ItemsDO>();
            #region display bulk items creation to the user
            dtBulkItemsCreation = null;
            dataBulcItem.DataSource = dtBulkItemsCreation;
            bulkSiNo = 1;
            #endregion display bulk items creation to the user

            //on cancel reset barcode values
            nextBarCode = 0;
            AssignNewBarCodeID(nextBarCode);
            nextBarCode++;

            chkUseExistingPO.Checked = false;
        }

        private void RefreshGrid()
        {
            string errorInfo = "";
            try
            {

                stStockDetails = new DataTable();
                dtGridStock.DataSource = stStockDetails;
                stStockDetails = objStockDBLayer.GetAllItems(ref errorInfo, AviMaConstants.ShowStockReport);
                RefreshGrid(stStockDetails);
                //if (stStockDetails != null && stStockDetails.Rows.Count > 0)
                //{
                //    dtGridStock.DataSource = stStockDetails;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error); dtGridStock.DataSource = stStockDetails;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        DataTable dtSuppliersTable = null;

        private void Stock_Load(object sender, EventArgs e)
        {
            LoadSupplierComboBox();
            RefreshGrid();
            ClearAllCreateFields(false);
            ClearAllEditFields();
            AssignNewBarCodeID(nextBarCode);
            nextBarCode++;
        }


        private void AssignNewBarCodeID(int barCodeUtilised)
        {
            try
            {
                string errorInfo = "";
                DataTable dtBarCodes = new DataTable();
                dtBarCodes = MasterCache.GetAvialableBarCodes(ref errorInfo);
                if (dtBarCodes != null && dtBarCodes.Rows.Count > 0)
                {
                    txtBarCodeCrea.Text = Convert.ToString(dtBarCodes.Rows[barCodeUtilised]["BarCode"]);
                }
                if (string.IsNullOrEmpty(txtBarCodeCrea.Text))
                {
                    MessageBox.Show("Please comntact admin bar codes are not available", "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //dtGridStock.DataSource = stStockDetails;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

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

                    cmbBoxPOSupplier.DataSource = dtSuppliersTable.Copy();
                    cmbBoxPOSupplier.ValueMember = "SupID";
                    cmbBoxPOSupplier.DisplayMember = "SupName";

                    cmbSupplier.DataSource = dtSuppliersTable.Copy();
                    cmbSupplier.ValueMember = "SupID";
                    cmbSupplier.DisplayMember = "SupName";


                    cmbSupplierEdit.DataSource = dtSuppliersTable.Copy();
                    cmbSupplierEdit.ValueMember = "SupID";
                    cmbSupplierEdit.DisplayMember = "SupName";


                    txtSearchSupplier.DataSource = dtSuppliersTable.Copy();
                    txtSearchSupplier.ValueMember = "SupID";
                    txtSearchSupplier.DisplayMember = "SupName";


                    cmbSupplier.Text = "-Select-";
                    cmbBoxPOSupplier.Text = "-Select-";
                    cmbSupplierEdit.Text = "-Select-";
                    txtSearchSupplier.Text = "-Select-";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error); dtGridStock.DataSource = stStockDetails;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dtGridStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;
                string errorInfo = "";
                if (i != -1)
                {
                    //IID, IBarCode, IName, IDescription, IQnty, IPurchasePrice, IPONumber, ISupID, IWCashPrice, IWCreditPrice, IWRetailPrice, ICreatedDateTime, ICreatedBy


                    txtBarCodeEdit.Text = txtBarCodeDel.Text = dtGridStock[1, i].Value.ToString();
                    txtNameEdit.Text = txtNameDel.Text = dtGridStock[2, i].Value.ToString();
                    txtDescripEdit.Text = txtDescripDel.Text = dtGridStock[3, i].Value.ToString();
                    txtQntyEdit.Text = txtQntyDel.Text = dtGridStock[4, i].Value.ToString();
                    //string supName = objStockDBLayer.GetASupplier(Convert.ToInt64(dtGridStock[7, i].Value.ToString()), ref errorInfo);
                    //cmbSupplierEdit.Text = txtSuppplierDel.Text = supName;
                    cmbSupplierEdit.Text = txtSuppplierDel.Text = dtGridStock[16, i].Value.ToString();
                    txtPurchasePriceEdit.Text = txtPurchPriceDele.Text = dtGridStock[5, i].Value.ToString();
                    txtWSCashPriceEdit.Text = txtWSCashPriceDel.Text = dtGridStock[8, i].Value.ToString();
                    txtWSCreditPriceEdit.Text = txtWSCreditPriceDel.Text = dtGridStock[9, i].Value.ToString();
                    txtRetailPriceEdit.Text = txtRetailPriceDel.Text = dtGridStock[10, i].Value.ToString();
                    txtPriceCodeEdit.Text = txtRetailPriceDel.Text = dtGridStock[11, i].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to edit/delet stock data. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

        }

        private void btnDElCan_Click(object sender, EventArgs e)
        {
            ClearAllDelFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtBarCodeDel.Text.Trim()))
            {
                if (MessageBox.Show("Delete stock.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        string errorInfo = "";
                        bool _check = objStockDBLayer.DeleteItems(txtBarCodeDel.Text.Trim(), ref errorInfo);

                        if (_check)
                        {
                            ClearAllEditFields();
                            ClearAllDelFields();
                            RefreshGrid();
                            MasterCache.SetBarCodes(ref errorInfo);
                            MessageBox.Show("Item deleted successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to delete item. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to delete item. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a stock from search grid to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdCancel_Click(object sender, EventArgs e)
        {
            ClearAllEditFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidatEditStock())
            {
                if (MessageBox.Show("Update stock.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        ItemsDO objItemsDO = new ItemsDO();


                        objItemsDO.BarCode = txtBarCodeEdit.Text.Trim();
                        objItemsDO.Name = txtNameEdit.Text.Trim();
                        objItemsDO.Description = txtDescripEdit.Text.Trim();
                        objItemsDO.Qnty = Convert.ToInt16(txtQntyEdit.Text.Trim());

                        if (!string.IsNullOrEmpty(txtPurchasePriceEdit.Text))
                            objItemsDO.IPurchasePrice = Convert.ToDecimal(txtPurchasePriceEdit.Text);

                        //string supplierName = cmbSupplier.Text.Trim();
                        //int suplID = objStockDBLayer.GetASupplier(supplierName);
                        //objItemsDO.SupID = Convert.ToInt16(suplID);
                        objItemsDO.SupID = Convert.ToInt32(cmbSupplierEdit.SelectedValue);

                        objItemsDO.WCashPrice = Convert.ToDecimal(txtWSCashPriceEdit.Text.Trim());
                        objItemsDO.WCreditPrice = Convert.ToDecimal(txtWSCreditPriceEdit.Text.Trim());
                        objItemsDO.WRetailPrice = Convert.ToDecimal(txtRetailPriceEdit.Text.Trim());
                        objItemsDO.ModifiedDate = AviMaConstants.CurrentDateTimesStamp;
                        objItemsDO.PriceCode = txtPriceCodeEdit.Text.Trim();
                        objItemsDO.ModifiedBy = UserID;

                        string errorInfo = "";
                        bool _check = objStockDBLayer.UpdateItems(objItemsDO, ref errorInfo);

                        if (_check)
                        {
                            ClearAllEditFields();
                            RefreshGrid();
                            MessageBox.Show("Item updated successfully", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Unable to update item. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to update item.  " + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (ValidateSearchFields())
            {
                this.Cursor = Cursors.WaitCursor;
                string errorInfo = "";

                try
                {
                    int supID = 0;
                    if (txtSearchSupplier.Text.Trim() != "-Select-")
                        supID = Convert.ToInt16(txtSearchSupplier.SelectedValue);

                    DataTable dtSearchResult = objStockDBLayer.SerahcStock(txtSearchName.Text.Trim(), txtSearchBarCode.Text.Trim(), supID, ref errorInfo, AviMaConstants.ShowStockReport);

                    if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                    {
                        RefreshGrid(dtSearchResult);
                    }
                    else
                    {
                        MessageBox.Show("No results found for your search criteria. " + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        private void RefreshGrid(DataTable dtSearchResult)
        {

            try
            {
                DataColumn objColSupName = new DataColumn("SupplierName");
                dtSearchResult.Columns.Add(objColSupName);


                foreach (DataRow po in dtSearchResult.Rows)
                {
                    foreach (DataRow supplier in dtSuppliersTable.Rows)
                    {
                        if (Convert.ToString(po["ISupID"]) == Convert.ToString(supplier["SupID"]))
                        {
                            po["SupplierName"] = supplier["SupName"];
                        }
                    }
                    //    posTotalAmount += Convert.ToInt32(po["poTotalAmount"]); //calculate total Amount
                }
                if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                {
                    dtGridStock.DataSource = dtSearchResult;
                }
            }
            catch (Exception ex)
            {
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

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string errorInfo = "";
                TextBox txtBox = sender as TextBox;

                if (txtBox.Text.Trim().Length > 4)
                {
                    DataTable dtSearchResult = objStockDBLayer.SerahcStock(txtSearchName.Text.Trim(), txtSearchBarCode.Text.Trim(), Convert.ToInt16(txtSearchSupplier.SelectedValue), ref errorInfo, AviMaConstants.ShowStockReport);
                    RefreshGrid(dtSearchResult);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidatCreateStock())
            {
                try
                {
                    if (ItemsList == null)
                        ItemsList = new List<ItemsDO>();

                    if (ValidatCreateStock())
                    {
                        ItemsDO _item = new ItemsDO();

                        ItemsDO objItemsDO = new ItemsDO();


                        objItemsDO.BarCode = txtBarCodeCrea.Text.Trim();
                        objItemsDO.Name = txtNameCrea.Text.Trim();
                        objItemsDO.Description = txtDescripCrea.Text.Trim();
                        objItemsDO.Qnty = Convert.ToInt16(txtQntyCrea.Text.Trim());


                        if (!string.IsNullOrEmpty(txtPurchasePrice.Text))
                            objItemsDO.IPurchasePrice = Convert.ToDecimal(txtPurchasePrice.Text);

                        objItemsDO.SupID = Convert.ToInt32(cmbSupplier.SelectedValue);
                        objItemsDO.WCashPrice = Convert.ToDecimal(txtWSCashPriceCrea.Text.Trim());
                        objItemsDO.WCreditPrice = Convert.ToDecimal(txtWSCreditPriceCrea.Text.Trim());
                        objItemsDO.WRetailPrice = Convert.ToDecimal(txtRetailPriceCrea.Text.Trim());
                        objItemsDO.CreatedDateTime = AviMaConstants.CurrentDateTimesStamp;
                        objItemsDO.PriceCode = txtPriceCodeCreate.Text.Trim();
                        objItemsDO.CreatedBy = UserID;

                        ItemsList.Add(objItemsDO);


                        #region For Bar Code Printing
                        string errorInfo = "";
                        GetBarCodeDetails(txtBarCodeCrea.Text.Trim(), txtPriceCodeCreate.Text.Trim(), txtNameCrea.Text.Trim(), txtQntyCrea.Text.Trim(), ref errorInfo);

                        #endregion For Bar Code Printing

                        #region JUst display to the user
                        DataColumn dtColumnSINo = new DataColumn();
                        dtColumnSINo.ColumnName = "Si.No.";
                        DataColumn dtColumnBar = new DataColumn();
                        dtColumnBar.ColumnName = "Bar Code";
                        DataColumn dtColumnName = new DataColumn();
                        dtColumnName.ColumnName = "ItemName";
                        DataColumn dtColumnQnty = new DataColumn();
                        dtColumnQnty.ColumnName = "Qnty";
                        DataColumn dtColumnDesc = new DataColumn();
                        dtColumnDesc.ColumnName = "Description";



                        if (dtBulkItemsCreation == null)
                        {
                            dtBulkItemsCreation = new DataTable();
                            dtBulkItemsCreation.Columns.Add(dtColumnSINo);
                            dtBulkItemsCreation.Columns.Add(dtColumnBar);
                            dtBulkItemsCreation.Columns.Add(dtColumnName);
                            dtBulkItemsCreation.Columns.Add(dtColumnQnty);
                            dtBulkItemsCreation.Columns.Add(dtColumnDesc);
                        }


                        dtBulkItemsCreation.Rows.Add(bulkSiNo++, objItemsDO.BarCode, objItemsDO.Name, objItemsDO.Qnty, objItemsDO.Description);
                        dataBulcItem.DataSource = dtBulkItemsCreation;
                        #endregion JUst display to the user

                        MessageBox.Show("Item " + objItemsDO.Name + " addes to the list successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearAllCreateFields(true);
                        isValidStockAdded = true;

                        // get new bar code for next ITEM 
                        AssignNewBarCodeID(nextBarCode);
                        nextBarCode++;
                    }
                }
                catch (Exception ex)
                {
                    isValidStockAdded = false;
                    MessageBox.Show("Unable to add items to the list. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
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

        private void txtPOTotalAmnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void txtBarCodeCrea_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = sender as TextBox;

                if (!string.IsNullOrEmpty(txtBox.Text))
                {
                    string errorInfo = "";
                    bool _check = objStockDBLayer.GetItemWRTBarCode(txtBox.Text.Trim(), ref errorInfo);

                    if (ItemsList != null && ItemsList.Count > 0)
                    {
                        foreach (ItemsDO item in ItemsList)
                        {
                            if (txtBox.Text.Trim() == item.BarCode)
                            {
                                _check = true;
                                break;
                            }
                        }
                    }

                    if (_check)
                    {
                        MessageBox.Show("Item bar code already exists please provide other bar code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }

        }

        private void dataBulcItem_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
                if (dataBulcItem.CurrentCell.ColumnIndex == 3 || dataBulcItem.CurrentCell.ColumnIndex == 4) //Desired Column
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

        private void tabStockMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabStockMain.SelectedTab == tabStockMain.TabPages["tabCreate"])
                {
                    nextBarCode = 0;
                    AssignNewBarCodeID(nextBarCode);
                    nextBarCode++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtQntyCrea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPriceCodeCreate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void chkUseExistingPO_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkUseExistingPO.Checked)
                {
                    cmbComboPONumbers.Visible = true;

                    #region  update exiting PO with new items list
                    //  cmbComboPONumbers.Items.Clear();

                    string errorPO = "";
                    if (dtPODetails == null)
                        dtPODetails = new DataTable();
                    dtPODetails = objStockDBLayer.GetPOs(ref errorPO);
                    cmbComboPONumbers.DataSource = dtPODetails;
                    cmbComboPONumbers.DisplayMember = "ponumber";

                    chkBoxAddWithPO.Checked = true;

                    #endregion  update exiting PO with new items list
                }
                else
                {
                    cmbComboPONumbers.Visible = false;

                    cmbBoxPOSupplier.Enabled = true;
                    txtPOTotalAmnt.Enabled = true;
                    dtPODate.Enabled = true;
                    txtPOReferenceNo.Enabled = true;

                    cmbBoxPOSupplier.Text = "-Select-";
                    txtPOTotalAmnt.Text = "";
                    dtPODate.ResetText();
                    txtPOReferenceNo.Text = "";

                    chkBoxAddWithPO.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        bool checkFirstTimeHit = true; // Toavoid :Cannot find column [System.Data.DataRowView]. error at the first time
        private void cmbComboPONumbers_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!checkFirstTimeHit)
                {
                    DataRow[] dtrPo = dtPODetails.Select("ponumber = " + cmbComboPONumbers.Text);

                    foreach (DataRow item in dtrPo)
                    {
                        cmbBoxPOSupplier.Enabled = false;
                        txtPOTotalAmnt.Enabled = false;
                        dtPODate.Enabled = false;
                        txtPOReferenceNo.Enabled = false;

                        cmbBoxPOSupplier.SelectedValue = Convert.ToString(item["posupplierID"]);
                        txtPOTotalAmnt.Text = Convert.ToString(item["poTotalAmount"]);
                        dtPODate.Text = Convert.ToString(item["podate"]);
                        txtPOReferenceNo.Text = Convert.ToString(item["poRefernece"]);

                        break;
                    }
                }
                checkFirstTimeHit = false;
            }
            catch (Exception ex)
            {
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
