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
    public partial class AdminUserControl : UserControl
    {
        UserDBLayer objUserDBLayer = new UserDBLayer();
        DataTable dtUserDetails = null;
        public string UserID { get; set; }
        public AdminUserControl(string userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private bool ValidateUserCreation()
        {
            bool _check = true;

            try
            {
                if (string.IsNullOrEmpty(txtUsrName.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Name"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtUsrName.Focus();
                }
                else if (cmbBoxRole.Text == "-Select-")
                {
                    MessageBox.Show("Please select user role", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    cmbBoxRole.Focus();
                }
                else if (string.IsNullOrEmpty(txtUsrLoginID.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("User Login ID"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtUsrLoginID.Focus();
                }
                else if (ValidateDuplicateUID())
                {
                    _check = false;
                    txtUsrLoginID.Focus();
                }
                else if (string.IsNullOrEmpty(txtPaswrd.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Password"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtPaswrd.Focus();
                }
                else if (string.IsNullOrEmpty(txtReEntrPaswd.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Re-Enter Password"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtReEntrPaswd.Focus();
                }
                else if (!string.IsNullOrEmpty(txtPaswrd.Text) && !string.IsNullOrEmpty(txtReEntrPaswd.Text) && (txtPaswrd.Text.Trim() != txtReEntrPaswd.Text.Trim()))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Re-Entered Password same as Password"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtReEntrPaswd.Focus();
                }
                else if (string.IsNullOrEmpty(txtMobileOne.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Mobile1"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private bool ValidateDuplicateUID()
        {
            bool _check = false;
            try
            {
                if (!string.IsNullOrEmpty(txtUsrLoginID.Text))
                {
                    string erroInfo = "";
                    _check = objUserDBLayer.SearchForAUser(txtUsrLoginID.Text, ref erroInfo);
                    if (_check)
                    {
                        MessageBox.Show("User with login ID " + txtUsrLoginID.Text + " already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsrLoginID.Focus();
                        _check = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _check = false;
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "Test", "Test", ex.LineNumber(), this.FindForm().Name);
            }
            return _check;
        }
        private bool ValidateUserEdit()
        {
            bool _check = true;

            try
            {
                if (string.IsNullOrEmpty(txtEdName.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Name"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtEdName.Focus();
                }
                else if (cmbBoxEditRole.Text == "-Select-")
                {
                    MessageBox.Show("Please select user role", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    cmbBoxEditRole.Focus();
                }
                else if (string.IsNullOrEmpty(txtEdPassword.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Password"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtEdPassword.Focus();
                }
                else if (string.IsNullOrEmpty(txtReTypPasswrd.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Re-Enter Password"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtReTypPasswrd.Focus();
                }
                else if ((!string.IsNullOrEmpty(txtEdPassword.Text) && !string.IsNullOrEmpty(txtReTypPasswrd.Text)) && (txtEdPassword.Text.Trim() != txtReTypPasswrd.Text.Trim()))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Re-Entered Password should be same as Password"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtReTypPasswrd.Focus();
                }
                else if (string.IsNullOrEmpty(txtEditMobileOne.Text))
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("Mobile1"), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _check = false;
                    txtEditMobileOne.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
            return _check;
        }

        private void ClearAllCreateFields()
        {
            txtUsrLoginID.Text = "";
            txtPaswrd.Text = "";
            txtReEntrPaswd.Text = "";
            txtUsrName.Text = "";
            txtMobileOne.Text = "";
            txtMobileTwo.Text = "";
            txtEmail.Text = "";
            txtState.Text = "";
            txtDistrict.Text = "";
            txtAddr.Text = "";
            txtDescription.Text = "";
            cmbBoxRole.Text = "-Select-";

        }

        private void ClearAllEditFields()
        {
            txtEditLoginId.Text = "";
            txtEdPassword.Text = "";
            txtReTypPasswrd.Text = "";
            txtEdName.Text = "";
            txtEditMobileOne.Text = "";
            txtedMobileTwo.Text = "";
            txtEdEmail.Text = "";
            txtEdState.Text = "";
            txtEdDistrict.Text = "";
            txtEdAddrs.Text = "";
            txtEdDescription.Text = "";
            cmbBoxEditRole.Text = "-Select-";

        }

        private void ClearAllDelFields()
        {
            txtCustIdDel.Text = "";
            txtDelCustName.Text = "";
            txtDelMobileOne.Text = "";
            txtDelMobileTwo.Text = "";
            txtDelEmail.Text = "";
            txtDelState.Text = "";
            txtdelDistrict.Text = "";
            txtDelAddrs.Text = "";
            txtDelDescription.Text = "";

        }

        private void RefresUserhGrid()
        {

            try
            {
                dtUserDetails = new DataTable();
                dataGridUser.DataSource = dtUserDetails;

                string _errorInfo = "";
                dtUserDetails = objUserDBLayer.GetAllUsers(ref _errorInfo);

                if (dtUserDetails != null && dtUserDetails.Rows.Count > 0)
                {
                    dataGridUser.DataSource = dtUserDetails;
                }
                else if (dtUserDetails == null && !string.IsNullOrEmpty(_errorInfo))
                {
                    MessageBox.Show(_errorInfo, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void AdminUserControl_Load(object sender, EventArgs e)
        {
            RefresUserhGrid();
        }

        private void btnCreateUsr_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUserCreation())
                {
                    if (MessageBox.Show("Create user.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        UserDo objUserDO = new UserDo();
                        objUserDO.UserName = txtUsrName.Text.Trim();
                        objUserDO.UserRole = cmbBoxRole.Text.Trim();
                        objUserDO.UserLoginID = txtUsrLoginID.Text.Trim();
                        objUserDO.UserPassword = txtPaswrd.Text.Trim();
                        objUserDO.UserMobile1 = txtMobileOne.Text.Trim();
                        objUserDO.UserMobile2 = txtMobileTwo.Text.Trim();
                        objUserDO.UserEmail = txtEmail.Text.Trim();
                        objUserDO.UserState = txtState.Text.Trim();
                        objUserDO.UserDist = txtDistrict.Text.Trim();
                        objUserDO.UserTown = txtAddr.Text.Trim();
                        objUserDO.UserDescri = txtDescription.Text.Trim();
                        objUserDO.UserCreatedBy = UserID;
                        objUserDO.UserCreatedDate = AviMaConstants.CurrentDateTimesStamp;

                        string _errorInof = "";
                        bool _check = objUserDBLayer.CreateUser(objUserDO, ref _errorInof);

                        if (_check)
                        {
                            ClearAllCreateFields();
                            MasterCache.SetUserData(ref _errorInof);
                            RefresUserhGrid();
                            MessageBox.Show("User created successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to create user. " + _errorInof, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ClearAllCreateFields();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateUserEdit())
                {
                    if (MessageBox.Show("Update user.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {


                        UserDo objUserDO = new UserDo();

                        objUserDO.UserLoginID = txtEditLoginId.Text.Trim();
                        objUserDO.UserName = txtEdName.Text.Trim();
                        objUserDO.UserRole = cmbBoxEditRole.Text.Trim();
                        objUserDO.UserPassword = txtEdPassword.Text.Trim();
                        objUserDO.UserMobile1 = txtEditMobileOne.Text.Trim();
                        objUserDO.UserMobile2 = txtedMobileTwo.Text.Trim();
                        objUserDO.UserEmail = txtEdEmail.Text.Trim();
                        objUserDO.UserState = txtEdState.Text.Trim();
                        objUserDO.UserDist = txtEdDistrict.Text.Trim();
                        objUserDO.UserTown = txtEdAddrs.Text.Trim();
                        objUserDO.UserDescri = txtEdDescription.Text.Trim();
                        objUserDO.UserModiBy = UserID;
                        objUserDO.UserModiDate = AviMaConstants.CurrentDateTimesStamp;

                        string errorInfo = "";
                        bool _check = objUserDBLayer.UpdateUser(objUserDO, ref errorInfo);

                        if (_check)
                        {
                            ClearAllEditFields();
                            ClearAllDelFields();
                            MasterCache.SetUserData(ref errorInfo);
                            RefresUserhGrid();
                            MessageBox.Show("User update successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to update user. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnEdCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear edit fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ClearAllEditFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string errorInfo = "";

                if (!string.IsNullOrEmpty(txtCustIdDel.Text))
                {
                    if (MessageBox.Show("Delete user.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        string custID = "";
                        custID = txtCustIdDel.Text;
                        bool _check = objUserDBLayer.DeleteUser(custID, ref errorInfo);
                        if (_check)
                        {
                            ClearAllDelFields();
                            ClearAllEditFields();
                            MasterCache.SetUserData(ref errorInfo);
                            RefresUserhGrid();
                            MessageBox.Show("User deleted successfully");
                        }
                        else
                        {
                            MessageBox.Show("Unable to deleted user");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a user from search grid to delete." + errorInfo, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void btnDelCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear delete fields.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ClearAllDelFields();
            }
        }

        private void tabUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabUser.SelectedTab.Name == "tabCreate")
            {
                txtUsrName.Focus();
            }
            else if (tabUser.SelectedTab.Name == "tabEdit")
            {
                txtEdName.Focus();
            }
            //else if (tabUser.SelectedTab.Name == "tabDelete")
            //{
            //    txtEdName.Focus();
            //}
        }

        private void dataGridUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;

                if (i != -1)
                {

                    txtDelCustName.Text = txtEdName.Text = dataGridUser[1, i].Value.ToString();
                    txtCustIdDel.Text = txtEditLoginId.Text = dataGridUser[2, i].Value.ToString();
                    txtEdPassword.Text = dataGridUser[3, i].Value.ToString();
                    cmbBoxEditRole.Text = dataGridUser[4, i].Value.ToString();
                    txtDelMobileOne.Text = txtEditMobileOne.Text = dataGridUser[5, i].Value.ToString();
                    txtDelMobileTwo.Text = txtedMobileTwo.Text = dataGridUser[6, i].Value.ToString();
                    txtDelEmail.Text = txtEdEmail.Text = dataGridUser[7, i].Value.ToString();
                    txtDelState.Text = txtEdState.Text = dataGridUser[8, i].Value.ToString();
                    txtdelDistrict.Text = txtEdDistrict.Text = dataGridUser[9, i].Value.ToString();
                    txtDelAddrs.Text = txtEdAddrs.Text = dataGridUser[10, i].Value.ToString();
                    txtDelDescription.Text = txtEdDescription.Text = dataGridUser[11, i].Value.ToString();

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
            if (!string.IsNullOrEmpty(txtSearchName.Text) || !string.IsNullOrEmpty(txtLoginID.Text) || !string.IsNullOrEmpty(txtSearchMobile.Text) || !string.IsNullOrEmpty(txtSearchEmail.Text))
            {
                this.Cursor = Cursors.WaitCursor;
                string errorInfo = "";
                try
                {
                    DataTable dtSearchResult = new DataTable();
                    dataGridUser.DataSource = dtSearchResult;
                    dtSearchResult = objUserDBLayer.SerahcUser(txtSearchName.Text.Trim(), txtLoginID.Text.Trim(), txtSearchMobile.Text.Trim(), txtSearchEmail.Text.Trim(), ref errorInfo);

                    if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                    {
                        RefreshGrid(dtSearchResult);
                    }
                    else
                    {
                        MessageBox.Show("No results found for your search criteria " + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtLoginID.Text = "";
                RefresUserhGrid();
            }

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
                    dataGridUser.DataSource = dtSearchResult;
                    dtSearchResult = objUserDBLayer.SerahcUser(txtSearchName.Text.Trim(), txtLoginID.Text.Trim(), txtSearchMobile.Text.Trim(), txtSearchEmail.Text.Trim(), ref erroInfo);
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
                dataGridUser.DataSource = dtSearchResult;
            }

        }

        private void txtUsrLoginID_Leave(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtUsrLoginID.Text))
                {
                    string erroInfo = "";
                    bool _check = objUserDBLayer.SearchForAUser(txtUsrLoginID.Text, ref erroInfo);
                    if (_check)
                    {
                        MessageBox.Show("User with login ID " + txtUsrLoginID.Text + " already exists.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsrLoginID.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void txtSearchMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
