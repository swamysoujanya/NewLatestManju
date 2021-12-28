using AviMa.DataBaseLayer;
using AviMa.UtilityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AviMa.AviMaForms
{
    public partial class frmAddAccountType : Form
    {

        public string AccountType { get; set; }

        public frmAddAccountType()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAccType.Text != string.Empty)
                {
                    AccountType = txtAccType.Text.Trim();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(ValidationUtility.GetValidationString("account type."), "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
