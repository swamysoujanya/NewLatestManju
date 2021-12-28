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
    public partial class frmLedger : Form
    {
        public string UserID { get; set; }
        LedgerDBLayer objLedgerDBLayer = null;
        string errorinfo = "";
        formLedgerItem objItemForUD = null;
        public frmLedger(string userID)
        {
            InitializeComponent();
            UserID = userID;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            formLedgerItem objItem = new formLedgerItem(UserID);
            this.Hide();
            objItem.LedgerType = AviMaConstants.LA;
            objItem.ShowDialog();
            RefresGrid();
            this.Show();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (objItemForUD == null)
                objItemForUD = new formLedgerItem(UserID);

            if (objItemForUD.txtBoxSiNo.Text != "")
            {
                this.Hide();
                objItemForUD.LedgerType = AviMaConstants.LU;
                objItemForUD.ShowDialog();
                RefresGrid();
                this.Show();
            }

            else
            {
                MessageBox.Show("Please select an item from the ledger grid to edit.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Delete selected ledger item.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (objItemForUD == null)
                    objItemForUD = new formLedgerItem(UserID);
                if (objItemForUD.txtBoxSiNo.Text != "")
                {
                    this.Hide();
                    objItemForUD.LedgerType = AviMaConstants.LD;
                    objItemForUD.ShowDialog();
                    RefresGrid();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Please select an item from the ledger grid to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void frmLedger_Load(object sender, EventArgs e)
        {
            if (objLedgerDBLayer == null)
                objLedgerDBLayer = new LedgerDBLayer();
            RefresGrid();
        }

        private void RefresGrid()
        {
            DataTable dtLedgerInfo = new DataTable ();
            dtLedgerInfo= objLedgerDBLayer.GetAllLedger(ref errorinfo);
            dataGridLedger.DataSource = dtLedgerInfo;
            CalculateTotal( dtLedgerInfo);
        }

        private void dataGridLedger_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;

                if (i != -1)
                {


                    if (objItemForUD == null)
                        objItemForUD = new formLedgerItem(UserID);

                    objItemForUD.txtBoxSiNo.Text = Convert.ToString(dataGridLedger["SiNo", i].Value);
                    objItemForUD.txtPaidTo.Text = Convert.ToString(dataGridLedger["PaidTo", i].Value);
                    objItemForUD.txtTaxPaid.Text = Convert.ToString(dataGridLedger["TaxPaid", i].Value);
                    objItemForUD.txtAmount.Text = Convert.ToString(dataGridLedger["Amount", i].Value);
                    objItemForUD.dateLedger.Text = Convert.ToString(dataGridLedger["Date", i].Value);
                    objItemForUD.cmbAccount.Text = Convert.ToString(dataGridLedger["Account", i].Value);
                    objItemForUD.txtNote.Text = Convert.ToString(dataGridLedger["Note", i].Value);

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
            string erroInfo = "";
            this.Cursor = Cursors.WaitCursor;
            try
            {

                string fromDate = Convert.ToDateTime(dateFrom.Text).ToString("yyyy-MM-dd");
                string toDate = Convert.ToDateTime(dateTo.Text).ToString("yyyy-MM-dd");

                DataTable dtSearchResult = new DataTable();

                dtSearchResult = objLedgerDBLayer.SerahcLedger(fromDate, toDate, ref erroInfo);

                if (dtSearchResult != null && dtSearchResult.Rows.Count > 0)
                {
                    dataGridLedger.DataSource = dtSearchResult;
                    CalculateTotal(dtSearchResult);
                }
                else
                {
                    dataGridLedger.DataSource = dtSearchResult;
                    CalculateTotal(dtSearchResult);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            dateFrom.ResetText();
            dateTo.ResetText();
            RefresGrid();
        }


        private void CalculateTotal(DataTable dtLedgerInfo)
        {

            decimal totalExpences = 0;
            decimal totalSales = 0;

            #region calculate totalExpences

            foreach (DataRow item in dtLedgerInfo.Rows)
            {
                totalExpences += Convert.ToDecimal(item["Amount"]);
            }

            #endregion calculate totalExpences

            #region calculate total sales

            string fromDate = Convert.ToDateTime(dateFrom.Text).ToString("yyyy-MM-dd");
            string toDate = Convert.ToDateTime(dateTo.Text).ToString("yyyy-MM-dd");
            DataTable dtInvoiceData = objLedgerDBLayer.GetAllInvoiceAmount(0, fromDate, toDate, AviMaConstants.InvoiceFlag, ref errorinfo);

            foreach (DataRow _item in dtInvoiceData.Rows)
            {
                totalSales += Convert.ToDecimal(_item["InvGrandTotalAmount"]);
            }


            #endregion calculate total sales

            lblExpences.Text = Convert.ToString(totalExpences);
            lblSales.Text = Convert.ToString(totalSales);
            lblBalance.Text = Convert.ToString(totalSales - totalExpences);
        }

    }
}
