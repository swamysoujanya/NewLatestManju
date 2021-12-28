using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AviMa.AviMaForms
{
    public partial class InvoiceItemData : Form
    {
        public InvoiceItemData(DataTable dt)
        {
            InitializeComponent();

            dataGridInvDetails.DataSource = dt;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
          //  Application.Exit();
        }

      
    }
}
