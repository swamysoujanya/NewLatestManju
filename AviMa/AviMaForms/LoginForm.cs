using AviMa.DataBaseLayer;
using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;


namespace AviMa.AviMaForms
{
    public partial class LoginForm : Form
    {

        DataTable objUserDetails = new DataTable();
        DataTable dtConfiguration = new DataTable();
        UserDBLayer objUserDBLayer = new UserDBLayer();
        Dictionary<int, String> objFeatureCollection = null;
        UserACLDBLayer objUserACLDBLayer = null;
        string UserRoll = "";


        //  BackgroundWorker objBackGroundWorker = new BackgroundWorker();

        public LoginForm()
        {
            InitializeComponent();
            //objUserDetails = objUserDBLayer.GetAllUsers(ref errorInfo);

            //if (objUserDetails == null && !string.IsNullOrEmpty(errorInfo))
            //{
            //    MessageBox.Show(errorInfo, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Environment.Exit(0);
            //}

            //objBackGroundWorker.DoWork += ObjBackGroundWorker_DoWork;
            //objBackGroundWorker.RunWorkerCompleted += ObjBackGroundWorker_RunWorkerCompleted;

        }

        //private void ObjBackGroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (objFlash != null)
        //    {
        //        objFlash.Show();
        //    }
        //}

        //FormFlashScree objFlash = null;
        //private void ObjBackGroundWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    if (objFlash == null)
        //    {
        //        objFlash = new FormFlashScree(txtLoginID.Text.Trim());
        //        objFlash.Show();
        //    }

        //}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateLoginFormn())
                {


                    //this.Hide();
                    //objBackGroundWorker.RunWorkerAsync();

                    //Thread.Sleep(3000);

                    frmMainForm objfrmMainForm = new frmMainForm(txtLoginID.Text.Trim());

                    #region User Administration/Configuration
                    string errorInfo = "";


                    if (objFeatureCollection == null)
                        objFeatureCollection = new Dictionary<int, string>();

                    if (objUserACLDBLayer == null)
                        objUserACLDBLayer = new UserACLDBLayer();

                    #region new enhancement for user roll access

                    if (txtLoginID.Text.Trim().ToLower() != "superadmin")
                    {
                        DataRow[] dtUserRow = MasterCache.GetUserDataFrmCache(ref errorInfo).Select("UserLoginID = '" + txtLoginID.Text.Trim() + "'");
                        DataTable dtUserPermission = new DataTable();
                        List<int> enabledFeatures = new List<int>();
                        foreach (DataRow item in dtUserRow)
                        {
                            enabledFeatures = objUserACLDBLayer.GetFeatureUserMap(Convert.ToUInt16(item["UserID"]), ref errorInfo);
                            break;
                        }


                        errorInfo = "";
                        objFeatureCollection = objUserACLDBLayer.GetAllFeatures(ref errorInfo);

                        List<string> listFeatures = new List<string>();

                        foreach (int featureID in enabledFeatures)
                        {
                            listFeatures.Add(objFeatureCollection.FirstOrDefault(x => x.Key == featureID).Value);
                        }



                        //  objfrmMainForm.fileToolStripMenuItem.Enabled = false;
                        objfrmMainForm.productManagementToolStripMenuItem.Enabled = false;
                        objfrmMainForm.maintananceToolStripMenuItem.Enabled = false;
                        objfrmMainForm.purchaseEntryToolStripMenuItem.Enabled = false;
                        objfrmMainForm.stockReturnToolStripMenuItem.Enabled = false;
                        objfrmMainForm.reGenarateToolStripMenuItem.Enabled = false;
                        // objfrmMainForm.fileToolStripMenuItem.Enabled = false;
                        objfrmMainForm.invoiceToolStripMenuItem.Enabled = false;
                        objfrmMainForm.showInvoiceToolStripMenuItem.Enabled = false;
                        objfrmMainForm.billingToolStripMenuItem.Enabled = false;
                        objfrmMainForm.reportToolStripMenuItem.Enabled = false;
                        objfrmMainForm.configurationToolStripMenuItem.Enabled = false;
                        objfrmMainForm.userToolStripMenuItem.Enabled = false;
                        objfrmMainForm.userConfigurationToolStripMenuItem.Enabled = false;
                        objfrmMainForm.supplierToolStripMenuItem.Enabled = false;
                        objfrmMainForm.customerToolStripMenuItem.Enabled = false;
                        objfrmMainForm.repairToolStripMenuItem.Enabled = false;
                        objfrmMainForm.viewToolStripMenuItem.Enabled = false;

                        objfrmMainForm.fileToolStripMenuItem.Visible = false;
                        objfrmMainForm.productManagementToolStripMenuItem.Visible = false;
                        objfrmMainForm.maintananceToolStripMenuItem.Visible = false;
                        objfrmMainForm.purchaseEntryToolStripMenuItem.Visible = false;
                        objfrmMainForm.stockReturnToolStripMenuItem.Visible = false;
                        objfrmMainForm.reGenarateToolStripMenuItem.Visible = false;
                        objfrmMainForm.invoiceToolStripMenuItem.Visible = false;
                        objfrmMainForm.showInvoiceToolStripMenuItem.Visible = false;
                        objfrmMainForm.billingToolStripMenuItem.Visible = false;
                        objfrmMainForm.reportToolStripMenuItem.Visible = false;
                        objfrmMainForm.configurationToolStripMenuItem.Visible = false;
                        objfrmMainForm.userToolStripMenuItem.Visible = false;
                        objfrmMainForm.userConfigurationToolStripMenuItem.Visible = false;
                        objfrmMainForm.supplierToolStripMenuItem.Visible = false;
                        objfrmMainForm.customerToolStripMenuItem.Visible = false;
                        objfrmMainForm.repairToolStripMenuItem.Visible = false;
                        objfrmMainForm.viewToolStripMenuItem.Visible = false;
                        objfrmMainForm.applicationConfigurationToolStripMenuItem.Visible = false;

                        objfrmMainForm.accountsToolStripMenuItem.Visible = false;
                        objfrmMainForm.ledgerToolStripMenuItem.Visible = false;
                        objfrmMainForm.expencesLedgerToolStripMenuItem.Visible = false;
                        objfrmMainForm.customerAccountsToolStripMenuItem.Visible = false;
                        objfrmMainForm.supplierAccountsToolStripMenuItem.Visible = false;
                        ///////////////////////////////
                        //////////////////////////////////


                        ///File
                        if (listFeatures.Contains("File"))
                        {
                            objfrmMainForm.fileToolStripMenuItem.Enabled = true;
                        }

                        ////STOCK
                        if (listFeatures.Contains("Stock"))
                        {
                            objfrmMainForm.productManagementToolStripMenuItem.Enabled = true;
                            objfrmMainForm.productManagementToolStripMenuItem.Visible = true;

                            if (listFeatures.Contains("PurchaseEntry"))
                                objfrmMainForm.purchaseEntryToolStripMenuItem.Enabled = true;
                            if (listFeatures.Contains("StockReturn"))
                                objfrmMainForm.stockReturnToolStripMenuItem.Enabled = true;
                            if (listFeatures.Contains("ReGenerateBarcode"))
                                objfrmMainForm.reGenarateToolStripMenuItem.Enabled = true;

                            if (listFeatures.Contains("PurchaseEntry"))
                                objfrmMainForm.purchaseEntryToolStripMenuItem.Visible = true;
                            if (listFeatures.Contains("StockReturn"))
                                objfrmMainForm.stockReturnToolStripMenuItem.Visible = true;
                            if (listFeatures.Contains("ReGenerateBarcode"))
                                objfrmMainForm.reGenarateToolStripMenuItem.Visible = true;

                        }


                        ////Billing
                        if (listFeatures.Contains("Billing"))
                        {
                            objfrmMainForm.billingToolStripMenuItem.Enabled = true;

                            if (listFeatures.Contains("Invoice"))
                                objfrmMainForm.invoiceToolStripMenuItem.Enabled = true;
                            if (listFeatures.Contains("ShowInvoice"))
                                objfrmMainForm.showInvoiceToolStripMenuItem.Enabled = true;

                            objfrmMainForm.billingToolStripMenuItem.Visible = true;

                            if (listFeatures.Contains("Invoice"))
                                objfrmMainForm.invoiceToolStripMenuItem.Visible = true;
                            if (listFeatures.Contains("ShowInvoice"))
                                objfrmMainForm.showInvoiceToolStripMenuItem.Visible = true;
                        }

                        //Report
                        if (listFeatures.Contains("Report"))
                            objfrmMainForm.reportToolStripMenuItem.Enabled = true;

                        if (listFeatures.Contains("Report"))
                            objfrmMainForm.reportToolStripMenuItem.Visible = true;

                        //Maintanance
                        if (listFeatures.Contains("Maintanance"))
                        {
                            objfrmMainForm.maintananceToolStripMenuItem.Enabled = true;
                            if (listFeatures.Contains("Repair"))
                                objfrmMainForm.repairToolStripMenuItem.Enabled = true;
                            if (listFeatures.Contains("ShowRepair"))
                                objfrmMainForm.viewToolStripMenuItem.Enabled = true;

                            objfrmMainForm.maintananceToolStripMenuItem.Visible = true;
                            if (listFeatures.Contains("Repair"))
                                objfrmMainForm.repairToolStripMenuItem.Visible = true;
                            if (listFeatures.Contains("ShowRepair"))
                                objfrmMainForm.viewToolStripMenuItem.Visible = true;
                        }


                        ///Administartion              
                        if (listFeatures.Contains("Administartion"))
                        {
                            objfrmMainForm.configurationToolStripMenuItem.Enabled = true;

                            if (listFeatures.Contains("User"))
                            {
                                objfrmMainForm.userToolStripMenuItem.Enabled = true;
                              //  objfrmMainForm.userConfigurationToolStripMenuItem.Enabled = true;
                            }


                            if (listFeatures.Contains("Supplier"))
                                objfrmMainForm.supplierToolStripMenuItem.Enabled = true;

                            if (listFeatures.Contains("Customer"))
                                objfrmMainForm.customerToolStripMenuItem.Enabled = true;



                            objfrmMainForm.configurationToolStripMenuItem.Visible = true;

                            if (listFeatures.Contains("User"))
                            {
                                objfrmMainForm.userToolStripMenuItem.Visible = true;
                                objfrmMainForm.userConfigurationToolStripMenuItem.Visible = true;
                            }


                            if (listFeatures.Contains("Supplier"))
                                objfrmMainForm.supplierToolStripMenuItem.Visible = true;

                            if (listFeatures.Contains("Customer"))
                                objfrmMainForm.customerToolStripMenuItem.Visible = true;

                            if (listFeatures.Contains("ApplicationConfig"))
                                objfrmMainForm.applicationConfigurationToolStripMenuItem.Visible = true;
                        }

                        //Accounts
                        if (listFeatures.Contains("Accounts"))
                        {

                            //objfrmMainForm.accountsToolStripMenuItem.Visible = false;
                            //objfrmMainForm.expencesLedgerToolStripMenuItem.Visible = false;
                            //objfrmMainForm.customerAccountsToolStripMenuItem.Visible = false;
                            //objfrmMainForm.supplierAccountsToolStripMenuItem.Visible = false;

                            objfrmMainForm.accountsToolStripMenuItem.Visible = true;

                            if (listFeatures.Contains("LedgerSummary"))
                                objfrmMainForm.ledgerToolStripMenuItem.Visible = true;
                            if (listFeatures.Contains("ExpenceLedger"))
                                objfrmMainForm.expencesLedgerToolStripMenuItem.Visible = true;
                            if (listFeatures.Contains("CustomerAccounts"))
                                objfrmMainForm.customerAccountsToolStripMenuItem.Visible = true;
                            if (listFeatures.Contains("SupplierAccounts"))
                                objfrmMainForm.supplierAccountsToolStripMenuItem.Visible = true;                           
                        }
                    }


                    #endregion new enhancement for user roll access
                    //dtConfiguration = objUserDBLayer.GetConfiguration(UserRoll, ref errorInfo);

                    //foreach (DataRow dtRow in dtConfiguration.Rows)
                    //{
                    //    //UserRole, `File`, Stock, Billing, Invoice, ShowInvoice, Report, Maintanance, `Repair`, ShowRepair, User, Supplier, Customer

                    //    objfrmMainForm.fileToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["File"]);
                    //    objfrmMainForm.productManagementToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["Stock"]);
                    //    objfrmMainForm.billingToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["Billing"]);
                    //    objfrmMainForm.invoiceToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["Invoice"]);
                    //    objfrmMainForm.showInvoiceToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["ShowInvoice"]);
                    //    objfrmMainForm.reportToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["Report"]);
                    //    objfrmMainForm.maintananceToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["Maintanance"]);
                    //    objfrmMainForm.repairToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["Repair"]);
                    //    objfrmMainForm.viewToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["ShowRepair"]);
                    //    objfrmMainForm.userToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["User"]);
                    //    objfrmMainForm.customerToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["Customer"]);
                    //    objfrmMainForm.supplierToolStripMenuItem.Enabled = Convert.ToBoolean(dtRow["Supplier"]);

                    //    //  if (Convert.ToString(dtRow["File"]) == "0")
                    //}

                    #endregion User Administration/Configuration


                    //objBackGroundWorker.WorkerSupportsCancellation = true;

                    //objBackGroundWorker.CancelAsync();

                    this.Hide();
                    objfrmMainForm.lblUserLoggedIN.Text = "User: " + txtLoginID.Text.Trim();
                    objfrmMainForm.lblLoggedInDateTime.Text = "Logged in at: " + DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");
                    objfrmMainForm.ShowDialog();
                    Application.Exit();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to login user. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private bool ValidateLoginFormn()
        {
            bool Check = true;

            try
            {
                if (string.IsNullOrEmpty(txtLoginID.Text.Trim()))
                {
                    MessageBox.Show("Please enter login ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Check = false;
                }
                else if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("Please enter password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Check = false;
                }
                else if (objUserDetails == null || objUserDetails.Rows.Count <= 0)
                {
                    MessageBox.Show("No users in database. Please contact administrator.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Check = false;
                }
                else if (objUserDetails != null || objUserDetails.Rows.Count > 0)
                {
                    bool _validUser = false;

                    foreach (DataRow item in objUserDetails.Rows)
                    {
                        if (txtLoginID.Text.Trim() == Convert.ToString(item["UserLoginID"]) && txtPassword.Text.Trim() == Convert.ToString(item["UserPassword"]))
                        {
                            UserRoll = Convert.ToString(item["Role"]);
                            _validUser = true;
                            break;
                        }
                    }
                    if (!_validUser)
                    {
                        MessageBox.Show("Invalid login ID or password, please retry", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Check = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
            return Check;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit AviMa application.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLoginID.Text = "";
            txtPassword.Text = "";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

            string errorInfo = "";
            this.objUserDetails = MasterCache.GetUserDataFrmCache(ref errorInfo);

            

        }
    }
}
