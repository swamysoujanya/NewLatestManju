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

namespace AviMa.AviMaUserControls
{
    public partial class AdminCustomer : UserControl
    {
        public string UserID { get; set; }
        public AdminCustomer(string userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        CustomerDBLayer objCustomerDBLayer = new CustomerDBLayer();
        DataTable dtCustomersDetails = null;

        private void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustName.Text) && ValidateUserNameCreate())
                {
                    if (MessageBox.Show("Create customer.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        CustomerDO objCustomerDO = new CustomerDO();
                        objCustomerDO.CustName = txtCustName.Text.Trim();
                        objCustomerDO.CustMobile1 = txtMobileOne.Text.Trim();
                        objCustomerDO.CustMobile2 = txtMobileTwo.Text.Trim();
                        objCustomerDO.CustEmail = txtEmail.Text.Trim();
                        objCustomerDO.CustState = txtState.Text.Trim();
                        objCustomerDO.CustDist = txtDistrict.Text.Trim();
                        objCustomerDO.CustTown = txtAddr.Text.Trim();
                        objCustomerDO.CustDescri = txtDescription.Text.Trim();
                        objCustomerDO.CustCreatedBy = UserID;
                        objCustomerDO.CustCreatedDate = AviMaConstants.CurrentDateTimesStamp;

                        string errorInfo = "";
                        bool _check = objCustomerDBLayer.CreateCustomer(objCustomerDO, ref errorInfo);

                        if (_check)
                        {
                            MasterCache.SetCustomerData(ref errorInfo);
                            ClearAllFields();                            
                            RefreshGrid();
                            MessageBox.Show("Customer created successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to create customer. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please enter customer name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCustName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }


        private bool ValidateUserNameCreate()
        {
            bool _check = true;
            try
            {
                if (!string.IsNullOrEmpty(txtCustName.Text))
                {
                    if (dtCustomersDetails != null && dtCustomersDetails.Rows.Count > 0)
                    {
                        DataRow[] dtRow = dtCustomersDetails.Select("CustName = '" + txtCustName.Text.Trim() + "'");
                        if (dtRow.Length > 0)
                        {
                            MessageBox.Show("Customer with name " + txtCustName.Text + " already exists. \nPlease enter different name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCustName.Focus();
                            _check = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _check = false;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }

            return _check;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all the fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ClearAllFields();
            }
        }

        private void AdminCustomer_Load(object sender, EventArgs e)
        {

            RefreshGrid();

            txtSearchName.Focus();

        }

        private void RefreshGrid()
        {
            try
            {
                //if(dataGridCustomer.Rows.Count > 0)
                //    dataGridCustomer.Rows.Clear();


                ClearGrid();
                string errorInfo = "";
                dtCustomersDetails = objCustomerDBLayer.GetAllCustomers(ref errorInfo);

                if (dtCustomersDetails != null && dtCustomersDetails.Rows.Count > 0)
                {
                    dataGridCustomer.DataSource = dtCustomersDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "","", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void RefreshGrid(DataTable dtCustomersDetails)
        {
            try
            {
                ClearGrid();

                if (dtCustomersDetails != null && dtCustomersDetails.Rows.Count > 0)
                {
                    dataGridCustomer.DataSource = dtCustomersDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "","", ex.LineNumber(), this.FindForm().Name);
            }

        }

        private void dataGridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;

                if (i != -1)
                {
                    //  CustName, CustMobile1, CustMobile2, CustEmail, CustState, CustDist, CustTown, CustDescri, CustCreatedBy, CustCreatedDate

                    txtCustIdDel.Text = txtCustID.Text = dataGridCustomer[0, i].Value.ToString();
                    txtNameDel.Text = txtNameEdit.Text = dataGridCustomer[1, i].Value.ToString();
                    txtMobOneDel.Text = txtMobileOneEdit.Text = dataGridCustomer[2, i].Value.ToString();
                    txtMobiTwoDel.Text = txtMobileTwoEdit.Text = dataGridCustomer[3, i].Value.ToString();
                    txtEmailDel.Text = txtEmailEdit.Text = dataGridCustomer[4, i].Value.ToString();
                    txtStateDel.Text = txtStateEdit.Text = dataGridCustomer[5, i].Value.ToString();
                    txtDestrictDel.Text = txtDistrictEdit.Text = dataGridCustomer[6, i].Value.ToString();
                    txtAddrsDel.Text = txtAddressEdit.Text = dataGridCustomer[7, i].Value.ToString();
                    txtDescrDel.Text = txtDescriptionEdit.Text = dataGridCustomer[8, i].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustIdDel.Text))
                {
                    if (MessageBox.Show("Delete customer.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Int32 custID = 0;
                        custID = Convert.ToInt32(txtCustIdDel.Text);
                        string errorInfo = "";
                        bool _check = objCustomerDBLayer.DeleteCustomer(custID, ref errorInfo);
                        if (_check)
                        {
                            MasterCache.SetCustomerData(ref errorInfo);
                            RefreshGrid();
                            ClearAllDelFields();
                            ClearAllEditFields();
                            MessageBox.Show("Customer deleted successfully. " + errorInfo, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to deleted customer." + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a customer from search grid to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void ClearGrid()
        {
            dtCustomersDetails = new DataTable();
            dataGridCustomer.DataSource = dtCustomersDetails;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustIdDel.Text) && ValidateUserNameEdit())
                {
                    if (MessageBox.Show("Update customer.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {



                        Int32 custID = 0;
                        custID = Convert.ToInt32(txtCustIdDel.Text);

                        CustomerDO objCustomerDO = new CustomerDO();
                        objCustomerDO.CustID = custID;
                        objCustomerDO.CustName = txtNameEdit.Text.Trim();
                        objCustomerDO.CustMobile1 = txtMobileOneEdit.Text.Trim();
                        objCustomerDO.CustMobile2 = txtMobileTwoEdit.Text.Trim();
                        objCustomerDO.CustEmail = txtEmailEdit.Text.Trim();
                        objCustomerDO.CustState = txtStateEdit.Text.Trim();
                        objCustomerDO.CustDist = txtDistrictEdit.Text.Trim();
                        objCustomerDO.CustTown = txtAddressEdit.Text.Trim();
                        objCustomerDO.CustDescri = txtDescriptionEdit.Text.Trim();
                        objCustomerDO.CustModiBy = UserID;
                        objCustomerDO.CustModiDate = AviMaConstants.CurrentDateTimesStamp;

                        string errorInfo = "";
                        bool _check = objCustomerDBLayer.UpdateCustomer(objCustomerDO, ref errorInfo);

                        if (_check)
                        {                            
                            MasterCache.SetCustomerData(ref errorInfo);
                            RefreshGrid();
                            ClearAllEditFields();
                            ClearAllDelFields();
                            MessageBox.Show("Customer updated successfully" + errorInfo, "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to update customer ", "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearAllEditFields();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Please select a customer from search grid to update.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private bool ValidateUserNameEdit()
        {
            bool _check = true;
            //try
            //{
            //    if (!string.IsNullOrEmpty(txtNameEdit.Text))
            //    {
            //        if (dtCustomersDetails != null && dtCustomersDetails.Rows.Count > 0)
            //        {
            //            DataRow[] dtRow = dtCustomersDetails.Select("CustName = '" + txtNameEdit.Text.Trim() + "'");
            //            if (dtRow.Length > 0)
            //            {
            //                MessageBox.Show("Customer with name " + txtNameEdit.Text + " already exists. \nPlease enter different name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                txtNameEdit.Focus();
            //                _check = false;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _check = false;
            //    Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            //}

            return _check;
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = sender as TextBox;

                if (txtBox.Text.Trim().Length > 4)
                {
                    string errorInfo = "";
                    DataTable dtSearchResult = objCustomerDBLayer.SerahcCustomer(txtSearchName.Text.Trim(), txtSearchMobile.Text.Trim(), txtSearchEmail.Text.Trim(), ref errorInfo);
                    RefreshGrid(dtSearchResult);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchName.Text) || !string.IsNullOrEmpty(txtSearchMobile.Text) || !string.IsNullOrEmpty(txtSearchEmail.Text))
            {

                this.Cursor = Cursors.WaitCursor;
                try
                {
                    string errorInfo = "";
                    DataTable dtSearchResult = objCustomerDBLayer.SerahcCustomer(txtSearchName.Text.Trim(), txtSearchMobile.Text.Trim(), txtSearchEmail.Text.Trim(), ref errorInfo);

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
                    MessageBox.Show(ex.Message, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
                finally
                { this.Cursor = Cursors.Default; }
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


        private bool Validation()
        {
            bool Check = true;

            try
            {
                if (string.IsNullOrEmpty(txtCustName.Text.Trim()))
                {
                    //MessageBox.Show("Please enter customer name.", "Information");
                    MessageBox.Show(ValidationUtility.GetValidationString("customer name."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCustName.Focus();
                    Check = false;
                }
                else if (string.IsNullOrEmpty(txtMobileOne.Text.Trim()))
                {
                    //MessageBox.Show("Please enter mobile number.", "Information");
                    MessageBox.Show(ValidationUtility.GetValidationString("mobile number."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMobileOne.Focus();
                    Check = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "","", ex.LineNumber(), this.FindForm().Name);
            }
            return Check;
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


        private void tabCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabCustomer.SelectedTab == tabSearch)
                {
                    txtSearchName.Focus();
                }
                else if (tabCustomer.SelectedTab == tabCreate)
                {
                    txtCustName.Focus();
                }
                else if (tabCustomer.SelectedTab == tabEdit)
                {
                    txtNameEdit.Focus();
                }
                else if (tabCustomer.SelectedTab == tabDelete)
                {
                    button4.Focus();
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
            if (MessageBox.Show("Clear all the fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ClearAllEditFields();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear all the fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ClearAllDelFields();
            }
        }

        private void txtCustName_Leave(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = sender as TextBox;

                if (!string.IsNullOrEmpty(txtBox.Text))
                {
                    if (dtCustomersDetails != null && dtCustomersDetails.Rows.Count > 0)
                    {
                        DataRow[] dtRow = dtCustomersDetails.Select("CustName = '" + txtBox.Text.Trim() + "'");
                        if (dtRow.Length > 0)
                        {
                            MessageBox.Show("Customer with name " + txtBox.Text + " already exists. \nPlease enter different name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
