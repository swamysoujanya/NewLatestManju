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

namespace AviMa.AviMaUserControls
{
    public partial class AdminSupplier : UserControl
    {
        public string UserID { get; set; }
        public AdminSupplier(string userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        SupplierDBLayer objSupplierDBLayer = new SupplierDBLayer();

        DataTable dtSuppliersDetails = null;

        private void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatCreateSup())
                {
                    if (MessageBox.Show("Create supplier.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        CustomerDO objSupplierDO = new CustomerDO();

                        objSupplierDO.CustName = txtCustName.Text.Trim();
                        objSupplierDO.CustMobile1 = txtMobileOne.Text.Trim();
                        objSupplierDO.CustMobile2 = txtMobileTwo.Text.Trim();
                        objSupplierDO.CustEmail = txtEmail.Text.Trim();
                        objSupplierDO.CustState = txtState.Text.Trim();
                        objSupplierDO.CustDist = txtDistrict.Text.Trim();
                        objSupplierDO.CustTown = txtAddr.Text.Trim();
                        objSupplierDO.CustDescri = txtDescription.Text.Trim();
                        objSupplierDO.CustCreatedBy = UserID;
                        objSupplierDO.CustCreatedDate = AviMaConstants.CurrentDateTimesStamp;

                        string errorInfo = "";
                        bool _check = objSupplierDBLayer.CreateSupplier(objSupplierDO, ref errorInfo);

                        if (_check)
                        {
                            ClearAllFields();
                            MasterCache.SetSupplier(ref errorInfo);
                            RefreshGrid();

                            MessageBox.Show("Suppplier created successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("Unable to create supplier. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ClearAllFields()
        {
            txtCustName.Text = "";
            txtMobileOne.Text = "";
            txtMobileTwo.Text = "";
            txtEmail.Text = "";
            txtState.Text = "";
            txtDistrict.Text = "";
            txtAddr.Text = "";
            txtDescription.Text = "";

        }

        private void ClearAllDelFields()
        {
            txtCustIdDel.Text = "";
            txtNameDel.Text = "";
            txtMobOneDel.Text = "";
            txtMobiTwoDel.Text = "";
            txtEmailDel.Text = "";
            txtStateDel.Text = "";
            txtDestrictDel.Text = "";
            txtAddrsDel.Text = "";
            txtDescrDel.Text = "";
        }

        private void ClearAllEditFields()
        {
            txtCustID.Text = "";
            txtNameEdit.Text = "";
            txtMobileOneEdit.Text = "";
            txtMobileTwoEdit.Text = "";
            txtEmailEdit.Text = "";
            txtStateEdit.Text = "";
            txtDescriptionEdit.Text = "";
            txtAddressEdit.Text = "";
            txtDistrictEdit.Text = "";
        }

        private void RefreshGrid()
        {
            try
            {
                string errorInfo = "";
                dtSuppliersDetails = new DataTable();
                dataGridSupplier.DataSource = dtSuppliersDetails;

                //dtSuppliersDetails = objSupplierDBLayer.GetAllSuppliers(ref errorInfo);
                MasterCache.SetSupplier(ref errorInfo);
                dtSuppliersDetails = MasterCache.GetSupplierFrmCache(ref errorInfo);


                if (dtSuppliersDetails != null && dtSuppliersDetails.Rows.Count > 0)
                {
                    dataGridSupplier.DataSource = dtSuppliersDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;

                if (i != -1)
                {
                    txtCustIdDel.Text = txtCustID.Text = dataGridSupplier[0, i].Value.ToString();
                    txtNameDel.Text = txtNameEdit.Text = dataGridSupplier[1, i].Value.ToString();
                    txtMobOneDel.Text = txtMobileOneEdit.Text = dataGridSupplier[2, i].Value.ToString();
                    txtMobiTwoDel.Text = txtMobileTwoEdit.Text = dataGridSupplier[3, i].Value.ToString();
                    txtEmailDel.Text = txtEmailEdit.Text = dataGridSupplier[4, i].Value.ToString();
                    txtStateDel.Text = txtStateEdit.Text = dataGridSupplier[5, i].Value.ToString();
                    txtDestrictDel.Text = txtDistrictEdit.Text = dataGridSupplier[6, i].Value.ToString();
                    txtAddrsDel.Text = txtAddressEdit.Text = dataGridSupplier[7, i].Value.ToString();
                    txtDescrDel.Text = txtDescriptionEdit.Text = dataGridSupplier[8, i].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidatEditSup())
                {
                    if (MessageBox.Show("Update supplier.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(txtCustIdDel.Text))
                        {

                            Int32 custID = 0;
                            custID = Convert.ToInt32(txtCustIdDel.Text);

                            CustomerDO objSupplierDO = new CustomerDO();
                            objSupplierDO.CustID = custID;
                            objSupplierDO.CustName = txtNameEdit.Text.Trim();
                            objSupplierDO.CustMobile1 = txtMobileOneEdit.Text.Trim();
                            objSupplierDO.CustMobile2 = txtMobileTwoEdit.Text.Trim();
                            objSupplierDO.CustEmail = txtEmailEdit.Text.Trim();
                            objSupplierDO.CustState = txtStateEdit.Text.Trim();
                            objSupplierDO.CustDist = txtDistrictEdit.Text.Trim();
                            objSupplierDO.CustTown = txtAddressEdit.Text.Trim();
                            objSupplierDO.CustDescri = txtDescriptionEdit.Text.Trim();
                            objSupplierDO.CustModiBy = UserID;
                            objSupplierDO.CustModiDate = AviMaConstants.CurrentDateTimesStamp;

                            string errorInfo = "";
                            bool _check = objSupplierDBLayer.UpdateSupplier(objSupplierDO, ref errorInfo);

                            if (_check)
                            {
                                ClearAllEditFields();
                                ClearAllDelFields();
                                MasterCache.SetSupplier(ref errorInfo);
                                RefreshGrid();
                                MessageBox.Show("Suppplier updated successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Unable to update supplier. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clera edit fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ClearAllEditFields();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clera delete fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ClearAllDelFields();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustIdDel.Text))
                {
                    if (MessageBox.Show("Delete supplier.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Int32 custID = 0;
                        custID = Convert.ToInt32(txtCustIdDel.Text);
                        string errorInfo = "";
                        bool _check = objSupplierDBLayer.DeleteSupplier(custID, ref errorInfo);
                        if (_check)
                        {
                            ClearAllDelFields();
                            ClearAllEditFields();
                            MasterCache.SetSupplier(ref errorInfo);
                            RefreshGrid();
                            MessageBox.Show("Suppplier deleted successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to delete supplier. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a supplier from search grid to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void AdminSupplier_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = sender as TextBox;
                string erroInfo = "";
                if (txtBox.Text.Trim().Length > 4)
                {
                    DataTable dtSearchResult = new DataTable();
                    dataGridSupplier.DataSource = dtSearchResult;
                    dtSearchResult = objSupplierDBLayer.SerahcSupplier(txtSearchName.Text.Trim(), txtSearchMobile.Text.Trim(), txtSearchEmail.Text.Trim(), ref erroInfo);
                    RefreshGrid(dtSearchResult);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void RefreshGrid(DataTable dtSearchResult)
        {
            if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
            {
                dataGridSupplier.DataSource = dtSearchResult;
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchName.Text) || !string.IsNullOrEmpty(txtSearchMobile.Text) || !string.IsNullOrEmpty(txtSearchEmail.Text))
            {
                string erroInfo = "";
                this.Cursor = Cursors.WaitCursor;
                try
                {

                    DataTable dtSearchResult = new DataTable();
                    dataGridSupplier.DataSource = dtSearchResult;
                    dtSearchResult = objSupplierDBLayer.SerahcSupplier(txtSearchName.Text.Trim(), txtSearchMobile.Text.Trim(), txtSearchEmail.Text.Trim(), ref erroInfo);

                    if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                    {
                        RefreshGrid(dtSearchResult);
                    }
                    else
                    {
                        MessageBox.Show("No results found for your search criteria", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtSearchName.Text = "";
                txtSearchMobile.Text = "";
                txtSearchEmail.Text = "";
                RefreshGrid();
            }
        }

        private bool ValidatCreateSup()
        {
            bool _check = true;

            try
            {
                if (string.IsNullOrEmpty(txtCustName.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("supplier name."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtCustName.Focus();
                }
                else if (!ValidateSupName())
                {
                    _check = false;
                    txtCustName.Focus();
                }
                else if (string.IsNullOrEmpty(txtMobileOne.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("mobile number."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtMobileOne.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
            return _check;
        }
        private bool ValidateSupName()
        {

            bool _check = true;
            try
            {

                if (!string.IsNullOrEmpty(txtCustName.Text))
                {
                    if (dtSuppliersDetails != null && dtSuppliersDetails.Rows.Count > 0)
                    {
                        DataRow[] dtRow = dtSuppliersDetails.Select("SupName = '" + txtCustName.Text.Trim() + "'");
                        if (dtRow.Length > 0)
                        {
                            MessageBox.Show("Supplier with name " + txtCustName.Text + " already exists. \nPlease enter different name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCustName.Focus();
                            _check = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _check = false;
                Logger.LogFile(ex.Message, "Test", "Test", ex.LineNumber(), this.FindForm().Name);
            }
            return _check;
        }

        private bool ValidatEditSup()
        {
            bool _check = true;
            try
            {

                if (string.IsNullOrEmpty(txtNameEdit.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("supplier name."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtNameEdit.Focus();
                }
                //else if (!ValidateSupNameEdit())
                //{
                //    _check = false;
                //    txtNameEdit.Focus();
                //}
                else if (string.IsNullOrEmpty(txtMobileOneEdit.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("mobile number."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtMobileOneEdit.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
            return _check;

        }

        private void txtSearchMobile_KeyPress(object sender, KeyPressEventArgs e)
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
        private bool ValidateSupNameEdit()
        {

            bool _check = true;
            try
            {

                if (!string.IsNullOrEmpty(txtNameEdit.Text))
                {
                    if (dtSuppliersDetails != null && dtSuppliersDetails.Rows.Count > 0)
                    {
                        DataRow[] dtRow = dtSuppliersDetails.Select("SupName = '" + txtNameEdit.Text.Trim() + "'");
                        if (dtRow.Length > 0)
                        {
                            MessageBox.Show("Supplier with name " + txtNameEdit.Text + " already exists. \nPlease enter different name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtNameEdit.Focus();
                            _check = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _check = false;
                Logger.LogFile(ex.Message, "Test", "Test", ex.LineNumber(), this.FindForm().Name);
            }
            return _check;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllFields();
        }

        private void txtCustName_Leave(object sender, EventArgs e)
        {
            try
            {

                TextBox txtBox = sender as TextBox;

                if (!string.IsNullOrEmpty(txtBox.Text))
                {
                    if (dtSuppliersDetails != null && dtSuppliersDetails.Rows.Count > 0)
                    {
                        DataRow[] dtRow = dtSuppliersDetails.Select("SupName = '" + txtBox.Text.Trim() + "'");
                        if (dtRow.Length > 0)
                        {
                            MessageBox.Show("Supplier with name " + txtBox.Text + " already exists. \nPlease enter different name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCustName.Focus();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
