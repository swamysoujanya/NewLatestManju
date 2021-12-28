using AviMa.DataBaseLayer;
using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AviMa.AviMaForms
{
    public partial class formLedgerItem : Form
    {
        public string UserID { get; set; }
        public string LedgerType { get; set; }

        Regex r = new Regex(@"^-{0,1}\d+\.{0,1}\d*$"); // This is the main part, can be altered to match any desired form or limitations
        public formLedgerItem(string userID)
        {
            InitializeComponent();
            UserID = userID;
            //   Note: Deleting account will also delete transactions data that is linked to this account. Do you wish to continue?
        }

        LedgerDBLayer objLedgerDBLayer = null;

        private void formLedgerItem_Load(object sender, EventArgs e)
        {
            if (LedgerType == AviMaConstants.LA)
            {
                txtBoxSiNo.Visible = false;
                lblSiNo.Visible = false;
                this.btnAddItem.Text = "Add Item";
            }
            else if (LedgerType == AviMaConstants.LU)
            {
                txtBoxSiNo.Visible = true;
                lblSiNo.Visible = true;
                this.btnAddItem.Text = "Update Item";
            }
            else if (LedgerType == AviMaConstants.LD)
            {
                txtBoxSiNo.Visible = true;
                lblSiNo.Visible = true;
                this.btnAddItem.Text = "Delete Item";
            }

            LoadComboBox();
        }

        string errorInfo = "";
        private void LoadComboBox()
        {
            DataTable dt = new DataTable();
            if (objLedgerDBLayer == null)
                objLedgerDBLayer = new LedgerDBLayer();
            dt = objLedgerDBLayer.GetAllLedgerAccType(ref errorInfo);
            cmbAccount.DataSource = dt;
            cmbAccount.DisplayMember = "AccountType";
            cmbAccount.ValueMember = "SiNo";
            if(LedgerType == AviMaConstants.LA)
            cmbAccount.Text = "-Select-";
        }

        private void clearAllFields()
        {
            txtBoxSiNo.Text = "";
            txtAmount.Text = "";
            txtPaidTo.Text = "";
            txtTaxPaid.Text = "";
            cmbAccount.Text = "-Select-";
            dateLedger.ResetText();
        }
        private bool Validate()
        {
            bool _check = true;

            if (cmbAccount.Text == "-Select-")
            {
                MessageBox.Show("Please select account type", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _check = false;
                cmbAccount.Focus();
            }

            else if (string.IsNullOrEmpty(txtPaidTo.Text))
            {
                MessageBox.Show("Please enter paid to", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _check = false;
                txtPaidTo.Focus();
            }
            else if (string.IsNullOrEmpty(txtAmount.Text))
            {
                MessageBox.Show("Please enter amount", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _check = false;
                txtAmount.Focus();
            }
            else if (!string.IsNullOrEmpty(txtAmount.Text) && !r.Match(txtAmount.Text).Success)
            {
                MessageBox.Show(ValidationUtility.GetValidationString("only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _check = false;
                txtAmount.Focus();
            }
            else if (!string.IsNullOrEmpty(txtTaxPaid.Text) && !r.Match(txtTaxPaid.Text).Success)
            {
                MessageBox.Show(ValidationUtility.GetValidationString("only decimals."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _check = false;
                txtAmount.Focus();
            }
            return _check;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

            if (LedgerType == AviMaConstants.LA)
            {
                #region Add
                if (MessageBox.Show("Create ledger entry.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (Validate())
                    {
                        try
                        {
                            string erroInfo = "";
                            LedgerDO objLedgerDO = new LedgerDO();
                            objLedgerDO.Amount = txtAmount.Text.Trim();
                            objLedgerDO.PaidTo = txtPaidTo.Text;
                            objLedgerDO.AccountType = cmbAccount.Text;
                            objLedgerDO.Date = Convert.ToDateTime(dateLedger.Text).ToString("yyyyMMdd");
                            objLedgerDO.CreatedBy = UserID;
                            objLedgerDO.CreatedDateTime = AviMaConstants.CurrentDateTimesStamp;
                            objLedgerDO.Note = txtNote.Text.Trim();
                            if (string.IsNullOrEmpty(txtTaxPaid.Text))
                                txtTaxPaid.Text = "0";
                            objLedgerDO.TaxPaid = txtTaxPaid.Text;

                            if (objLedgerDBLayer == null)
                                objLedgerDBLayer = new LedgerDBLayer();
                            bool _check = objLedgerDBLayer.CreateLedgerItem(objLedgerDO, ref erroInfo);

                            if (_check)
                            {
                                clearAllFields();
                                MessageBox.Show("Ledger entry created successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("Unable to create Ledger entry. " + erroInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                        }
                    }
                }
                #endregion Add
            }
            else if (LedgerType == AviMaConstants.LU)
            {
                #region Update
                if (MessageBox.Show("Update ledger entry.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (Validate())
                    {
                        try
                        {
                            string erroInfo = "";
                            LedgerDO objLedgerDO = new LedgerDO();
                            objLedgerDO.SiNo = Convert.ToInt16(txtBoxSiNo.Text.Trim());
                            objLedgerDO.Amount = txtAmount.Text.Trim();
                            objLedgerDO.PaidTo = txtPaidTo.Text;
                            objLedgerDO.AccountType = cmbAccount.Text;
                            objLedgerDO.Date = Convert.ToDateTime(dateLedger.Text).ToString("yyyyMMdd");
                            objLedgerDO.ModyfiedBy = UserID;
                            objLedgerDO.Note = txtNote.Text.Trim();
                            objLedgerDO.ModyfiedDateTime = AviMaConstants.CurrentDateTimesStamp;
                            if (string.IsNullOrEmpty(txtTaxPaid.Text))
                                txtTaxPaid.Text = "0";
                            objLedgerDO.TaxPaid = txtTaxPaid.Text;

                            if (objLedgerDBLayer == null)
                                objLedgerDBLayer = new LedgerDBLayer();
                            bool _check = objLedgerDBLayer.UpdateLedgerItem(objLedgerDO, ref erroInfo);

                            if (_check)
                            {
                                clearAllFields();
                                MessageBox.Show("Ledger entry updated successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("Unable to update Ledger entry. " + erroInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                        }
                    }
                }
                #endregion Update
            }
            else if (LedgerType == AviMaConstants.LD)
            {
                #region Delete
                if (MessageBox.Show("Delete ledger item.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (Validate())
                    {
                        try
                        {
                            string erroInfo = "";

                            if (objLedgerDBLayer == null)
                                objLedgerDBLayer = new LedgerDBLayer();
                            bool _check = objLedgerDBLayer.DeleteLedgerItem(Convert.ToInt16(txtBoxSiNo.Text.Trim()), ref erroInfo);

                            if (_check)
                            {
                                clearAllFields();
                                MessageBox.Show("Ledger entry delete successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();

                            }
                            else
                            {
                                MessageBox.Show("Unable to delete Ledger entry. " + erroInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                        }
                    }
                }
                #endregion Delete
            }
        }

        private void btnCancelItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAccntType_Click(object sender, EventArgs e)
        {
            frmAddAccountType objAcctypeFrm = new frmAddAccountType();
            this.Hide();
            objAcctypeFrm.ShowDialog();
            string AccType = objAcctypeFrm.AccountType;
            this.Show();

            if (!string.IsNullOrEmpty(AccType))
            {
                if (objLedgerDBLayer == null)
                    objLedgerDBLayer = new LedgerDBLayer();
                string erroInfo = "";
                bool _check = objLedgerDBLayer.CreateLedgerAccType(AccType, ref erroInfo);

                if (_check)
                {
                    LoadComboBox();
                    MessageBox.Show("Account type created successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Uanble to add account type.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void btnRemoveAccountType_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete account type" + cmbAccount.Text, "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {



                if (objLedgerDBLayer == null)
                    objLedgerDBLayer = new LedgerDBLayer();
                string erroInfo = "";
                bool _check = objLedgerDBLayer.DeleteLedgerAccType(Convert.ToInt16(cmbAccount.SelectedValue), ref erroInfo);

                if (_check)
                {
                    LoadComboBox();
                    MessageBox.Show("Account type deleted successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Uanble to delete Account type.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //    else
                //{
                //        MessageBox.Show("Please select an item from the acoount type to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    }

            }

        }
    }
}
