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
    public partial class frmDisplayDetailsInGrid : Form
    {
        public frmDisplayDetailsInGrid(DataTable dtData, string typeOfData)
        {
            InitializeComponent();

            if (dtData != null)
                dataGridDetails.DataSource = dtData;
            else if (dtData == null)
                return;
        }

        private void frmDisplayDetailsInGrid_Load(object sender, EventArgs e)
        {

        }
    }
}
