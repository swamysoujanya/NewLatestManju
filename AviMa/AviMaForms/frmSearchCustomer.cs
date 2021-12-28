using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AviMa.AviMaForms
{
    public partial class frmSearchCustomer : Form
    {
        public frmSearchCustomer(DataTable dt)
        {
            InitializeComponent();
            dataGridCustomer.DataSource = dt;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public string CustIDD { get; set; }
        public string CustNamee { get; set; }
        public string  Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Address { get; set; }
        private void btnSelect_Click(object sender, EventArgs e)
        {

            try
            {
                int j = 1;
                for (int i = 0; i < dataGridCustomer.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(dataGridCustomer[1, i].Value).Trim()))
                    {
                       // CustIDD = Convert.ToString(dataGridCustomer[0, i].Value);
                        CustNamee = Convert.ToString(dataGridCustomer[1, i].Value);
                        Mobile1 = Convert.ToString(dataGridCustomer[2, i].Value);
                        Mobile2 = Convert.ToString(dataGridCustomer[3, i].Value);
                        Address = Convert.ToString(dataGridCustomer[4, i].Value) + "," + Convert.ToString(dataGridCustomer[5, i].Value) + "," + Convert.ToString(dataGridCustomer[6, i].Value);
                    }
                }             

            }
            catch (Exception ex)
            {

                this.Close();
                throw;
            }
        }
    }
}
