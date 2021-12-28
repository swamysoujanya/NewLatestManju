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
    public partial class FormNumberOfPrints : Form
    {
        public FormNumberOfPrints()
        {
            InitializeComponent();
        }

        public decimal NumberOfCopies { get; set; }

        private void btnContinuePrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNumbOfCopies.Text))
                {
                    NumberOfCopies = Convert.ToDecimal(txtNumbOfCopies.Text);
                }
                else
                    NumberOfCopies = 1;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to get invoice details. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void FormNumberOfPrints_Load(object sender, EventArgs e)
        {
            txtNumbOfCopies.Focus();
        }
    }
}
