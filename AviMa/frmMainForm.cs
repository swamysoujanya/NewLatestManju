using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AviMa.AviMaUserControls;
using AviMa.UtilityLayer;
using AviMa.AviMaForms;

namespace AviMa
{
    public partial class frmMainForm : Form
    {
        public string UserID { get; set; }

        public frmMainForm(string userID)
        {
            InitializeComponent();

            UserID = userID;
        }
        private void frmMainForm_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
        }




        private void billingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void vendorReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        Stock objStock = null;
        AdminCustomer objAdminCustomer = null;
        AdminUserControl objAdminUserControl = null;
        AdminSupplier objAdminSupplier = null;
        InvRprtUsrCntrl objInvRprtUsrCntrl = null;
        StkRprtUsrCntrl objStkRprtUsrCntrl = null;
        PORprtUsrCntrl objPORprtUsrCntrl = null;
        stkReturnUserControl objstkReturnUserControl = null;
        StkRprtUsrCntrl objBarStkRprtUsrCntrl = null;
        FrmUserPerConfig objFrmUserPerConfig = null;

        frmInvoice objfrmInvoice = null;
        frmInvoice objfrmRepair = null;
        frmLedger objfrmLedger = null;
        frmCustomerAccounts objfrmCustomerAccounts = null;
        frmCustomerAccounts objfrmSuppplierAcc = null;
        FormAppAdmin objFormAppAdmin = null;
        frmLedgerReprot objfrmLedgerReprot = null;
        frmLedgerReport objfrmLedgerReport = null;

        bool bStock = false;
        bool bAdminCustomer = false;
        bool bAdminUserControl = false;
        bool bAdminSupplier = false;
        bool bInvRprtUsrCntrl = false;
        bool bStkRprtUsrCntrl = false;
        bool bPORprtUsrCntrl = false;
        bool bstkReturnUserControl = false;
        bool boBarStkRprtUsrCntrl = false;


        private void productManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //RemoveControl();

            //bAdminCustomer = false;
            //bAdminUserControl = false;
            //bAdminSupplier = false;
            //bfrmInvoice = false;

            //if (objStock == null)
            //objStock = new Stock(UserID);
            //pnlMainPanel.Controls.Add(objStock);
            //bStock = true;
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();


            bStock = false;
            bAdminCustomer = false;
            bAdminSupplier = false;
            bStkRprtUsrCntrl = false;
            bInvRprtUsrCntrl = false;
            bPORprtUsrCntrl = false;
            bstkReturnUserControl = false;
            boBarStkRprtUsrCntrl = false;

            if (objAdminCustomer == null)
                objAdminCustomer = new AdminCustomer(UserID);
            pnlMainPanel.Controls.Add(objAdminCustomer);
            bAdminCustomer = true;
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RemoveControl();


            bStock = false;
            bAdminCustomer = false;
            bAdminUserControl = false;
            bStkRprtUsrCntrl = false;
            bInvRprtUsrCntrl = false;
            bPORprtUsrCntrl = false;
            bstkReturnUserControl = false;
            boBarStkRprtUsrCntrl = false;

            if (objAdminSupplier == null)
                objAdminSupplier = new AdminSupplier(UserID);
            pnlMainPanel.Controls.Add(objAdminSupplier);
            bAdminSupplier = true;
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            bStock = false;
            bAdminCustomer = false;
            bAdminSupplier = false;
            bStkRprtUsrCntrl = false;
            bInvRprtUsrCntrl = false;
            bPORprtUsrCntrl = false;
            bstkReturnUserControl = false;
            boBarStkRprtUsrCntrl = false;
            if (objAdminUserControl == null)
                objAdminUserControl = new AdminUserControl(UserID);
            pnlMainPanel.Controls.Add(objAdminUserControl);
            bAdminUserControl = true;
        }

        private void RemoveControl()
        {
            if (bAdminCustomer && objAdminCustomer != null)
            { pnlMainPanel.Controls.Remove(objAdminCustomer); }
            else if (bStock && objStock != null)
            { pnlMainPanel.Controls.Remove(objStock); }
            else if (bAdminUserControl && objAdminUserControl != null)
            { pnlMainPanel.Controls.Remove(objAdminUserControl); }
            else if (bAdminSupplier && objAdminSupplier != null)
            { pnlMainPanel.Controls.Remove(objAdminSupplier); }
            else if (bStkRprtUsrCntrl && objStkRprtUsrCntrl != null)
            { pnlMainPanel.Controls.Remove(objStkRprtUsrCntrl); }
            else if (bInvRprtUsrCntrl && objInvRprtUsrCntrl != null)
            { pnlMainPanel.Controls.Remove(objInvRprtUsrCntrl); }
            else if (bPORprtUsrCntrl && objPORprtUsrCntrl != null)
            { pnlMainPanel.Controls.Remove(objPORprtUsrCntrl); }
            else if (bstkReturnUserControl && objstkReturnUserControl != null)
            { pnlMainPanel.Controls.Remove(objstkReturnUserControl); }
            else if (boBarStkRprtUsrCntrl && objBarStkRprtUsrCntrl != null)
            { pnlMainPanel.Controls.Remove(objBarStkRprtUsrCntrl); }             

        }



        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RemoveControl();

            if (objfrmInvoice == null)
                objfrmInvoice = new frmInvoice(UserID);
            this.Hide();
            objfrmInvoice.InvoiceType = AviMaConstants.INVOICE;
            objfrmInvoice.lblDate.Text = "";
            objfrmInvoice.lblDate.Text += DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");
            objfrmInvoice.ShowDialog();
            this.Show();


            //   RemoveControl();

            //   bStock = false;
            //   bAdminCustomer = false;
            //   bAdminSupplier = false;
            //   bAdminUserControl = false;


            //   if (objInvUsrCtrl == null)
            //       objInvUsrCtrl = new InvoiceUserControl();
            ////   objfrmInvoice.InvoiceType = AviMaConstants.INVOICE;
            //   pnlMainPanel.Controls.Add(objInvUsrCtrl);
            //   bfrmInvoice = true;
        }

        private void repairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objfrmRepair == null)
                objfrmRepair = new frmInvoice(UserID);
            this.Hide();
            objfrmRepair.InvoiceType = AviMaConstants.REPAIR;
            objfrmRepair.lblDate.Text = "";
            objfrmRepair.lblDate.Text += DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");


            objfrmRepair.ShowDialog();
            this.Show();

        }

        private void showInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceData objInvoiceData = new InvoiceData(UserID);
            this.Hide();
            objInvoiceData.InvoiceType = AviMaConstants.InvoiceFlag;
            objInvoiceData.ShowDialog();
            this.Show();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvoiceData objRepairData = new InvoiceData(UserID);
            this.Hide();
            objRepairData.InvoiceType = AviMaConstants.RepairFlag;
            objRepairData.ShowDialog();
            this.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpScreen objHelpScreen = new HelpScreen(UserID);
            objHelpScreen.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close the application.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void purchaseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            bAdminCustomer = false;
            bAdminUserControl = false;
            bAdminSupplier = false;
            bStkRprtUsrCntrl = false;
            bInvRprtUsrCntrl = false;
            bPORprtUsrCntrl = false;
            bstkReturnUserControl = false;
            boBarStkRprtUsrCntrl = false;

            if (objStock == null)
            {
                objStock = new Stock(UserID);
            }
            else
            {
                objStock.LoadSupplierComboBox();
            }
            pnlMainPanel.Controls.Add(objStock);
            bStock = true;
        }

        private void purchaceOrderReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            bAdminCustomer = false;
            bAdminUserControl = false;
            bAdminSupplier = false;
            bStkRprtUsrCntrl = false;
            bInvRprtUsrCntrl = false;
            bStock = false;
            bstkReturnUserControl = false;
            boBarStkRprtUsrCntrl = false;

            if (objPORprtUsrCntrl == null)
                objPORprtUsrCntrl = new PORprtUsrCntrl(UserID);
            pnlMainPanel.Controls.Add(objPORprtUsrCntrl);
            bPORprtUsrCntrl = true;


        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            bAdminCustomer = false;
            bAdminUserControl = false;
            bAdminSupplier = false;
            bPORprtUsrCntrl = false;
            bInvRprtUsrCntrl = false;
            bStock = false;
            bstkReturnUserControl = false;

            if (objStkRprtUsrCntrl == null)
                objStkRprtUsrCntrl = new StkRprtUsrCntrl(UserID);
            objStkRprtUsrCntrl.ReportType = AviMaConstants.ShowStockReport;
            pnlMainPanel.Controls.Add(objStkRprtUsrCntrl);
            bStkRprtUsrCntrl = true;

        }

        private void dailySalesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            bAdminCustomer = false;
            bAdminUserControl = false;
            bAdminSupplier = false;
            bPORprtUsrCntrl = false;
            bStkRprtUsrCntrl = false;
            bStock = false;
            bstkReturnUserControl = false;
            boBarStkRprtUsrCntrl = false;

            if (objInvRprtUsrCntrl == null)
                objInvRprtUsrCntrl = new InvRprtUsrCntrl(UserID);
            pnlMainPanel.Controls.Add(objInvRprtUsrCntrl);
            objInvRprtUsrCntrl.PopulateCustomerCombo();
            objInvRprtUsrCntrl.LoadData();
            bInvRprtUsrCntrl = true;

        }


        private void stockReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            bAdminCustomer = false;
            bAdminUserControl = false;
            bAdminSupplier = false;
            bStkRprtUsrCntrl = false;
            bInvRprtUsrCntrl = false;
            bPORprtUsrCntrl = false;
            bStock = false;
            boBarStkRprtUsrCntrl = false;

            if (objstkReturnUserControl == null)
                objstkReturnUserControl = new stkReturnUserControl(UserID);
            pnlMainPanel.Controls.Add(objstkReturnUserControl);
            bstkReturnUserControl = true;
        }

        private void reGenarateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            bAdminCustomer = false;
            bAdminUserControl = false;
            bAdminSupplier = false;
            bPORprtUsrCntrl = false;
            bInvRprtUsrCntrl = false;
            bStock = false;
            bstkReturnUserControl = false;
            bStkRprtUsrCntrl = false;

            if (objBarStkRprtUsrCntrl == null)
                objBarStkRprtUsrCntrl = new StkRprtUsrCntrl(UserID);
            objBarStkRprtUsrCntrl.ReportType = AviMaConstants.ReGenerateBarCode;
            pnlMainPanel.Controls.Add(objBarStkRprtUsrCntrl);
            boBarStkRprtUsrCntrl = true;

        }

        private void userConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            if (objFrmUserPerConfig == null)
                objFrmUserPerConfig = new FrmUserPerConfig ();
            this.Hide();            
            objFrmUserPerConfig.ShowDialog();
            this.Show();

        }

        private void expencesLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            if (objfrmLedger == null)
                objfrmLedger = new frmLedger(UserID);
            this.Hide();
            objfrmLedger.ShowDialog();
            this.Show();
        }

        private void customerAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RemoveControl();

            if (objfrmCustomerAccounts == null)
                objfrmCustomerAccounts = new frmCustomerAccounts(UserID);
            this.Hide();
            objfrmCustomerAccounts.AccountType = AviMaConstants.CustAccType;
            objfrmCustomerAccounts.ShowDialog();
            this.Show();
            
        }

        private void supplierAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RemoveControl();

            if (objfrmSuppplierAcc == null)
                objfrmSuppplierAcc = new frmCustomerAccounts(UserID);
            this.Hide();
            objfrmSuppplierAcc.AccountType = AviMaConstants.SupAccType;
            objfrmSuppplierAcc.ShowDialog();
            this.Show();

            // MessageBox.Show("Supplier Acoounts is under development \n Coming Up Soon...");

        }

        private void applicationConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RemoveControl();

            if (objFormAppAdmin == null)
                objFormAppAdmin = new FormAppAdmin(UserID);
            this.Hide();
            objFormAppAdmin.ShowDialog();
            this.Show();
        }

        private void ledgerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RemoveControl();

            if (objfrmLedgerReprot == null)
                objfrmLedgerReprot = new frmLedgerReprot(UserID);
            this.Hide();
            objfrmLedgerReprot.ShowDialog();
            this.Show();

            
        }

        private void ledgerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveControl();

            if (objfrmLedgerReport == null)
                objfrmLedgerReport = new frmLedgerReport(UserID);
            this.Hide();
            objfrmLedgerReport.ShowDialog();
            this.Show();
        }
    }
}
