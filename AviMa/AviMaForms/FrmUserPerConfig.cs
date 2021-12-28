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
using System.Windows.Forms;

namespace AviMa.AviMaForms
{
    public partial class FrmUserPerConfig : Form
    {


        //  ConfigID, UserRole, `File`, Stock, Billing, Invoice, ShowInvoice, Report, Maintanance, `Repair`, ShowRepair, User, Supplier, Customer, FC1, FC2
        // configuration
        Dictionary<int, String> objFeatureCollection = null;
        string errorInfo = "";
        UserACLDBLayer objUserACLDBLayer = null;

        public FrmUserPerConfig()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtID.Text.Trim()))
            {

                try
                {
                    int UserID = Convert.ToInt16(txtID.Text.Trim());
                    UserACLDO objUserACLDO = new UserACLDO();
                    objUserACLDO.UserID = UserID;
                    objUserACLDO.EnabledFeat = new List<int>();

                    if (chkStock.Checked)
                    {
                        var myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Stock").Key;
                        objUserACLDO.EnabledFeat.Add(myValue);

                        if (chkPurchaseEntry.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "PurchaseEntry").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }
                        if (chkStockReturn.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "StockReturn").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }
                        if (chkReGenBarCode.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "ReGenerateBarcode").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }
                    }

                    if (chkBilling.Checked)
                    {

                        var myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Billing").Key;
                        objUserACLDO.EnabledFeat.Add(myValue);

                        if (chkInvoice.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Invoice").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }
                        if (chkShowInvoice.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "ShowInvoice").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }
                    }

                    if (chkReport.Checked)
                    {
                        // Allow all reports if main root has got permission
                        var myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Report").Key;
                        objUserACLDO.EnabledFeat.Add(myValue);
                    }

                    if (chkMaintenance.Checked)
                    {

                        var myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Maintanance").Key;
                        objUserACLDO.EnabledFeat.Add(myValue);

                        if (chkRepair.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Repair").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }

                        if (chkShowRepair.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "ShowRepair").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }
                    }

                    if (chkAdministartion.Checked)
                    {

                        var myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Administartion").Key;
                        objUserACLDO.EnabledFeat.Add(myValue);

                        if (chkUserEmp.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "User").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }

                        if (chkCustomer.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Customer").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }

                        if (chkSupplier.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Supplier").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }

                        if (chkAppConfig.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "ApplicationConfig").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }
                    }

                    if (chkAccounts.Checked)
                    {
                        var myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "Accounts").Key;
                        objUserACLDO.EnabledFeat.Add(myValue);

                        if (chkExpense.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "ExpenceLedger").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }

                        if (chkCustomerAccount.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "CustomerAccounts").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }
                        if (chkSupplierAccount.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "SupplierAccounts").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }


                        if (chkBoxLedgerSum.Checked)
                        {
                            myValue = objFeatureCollection.FirstOrDefault(x => x.Value == "LedgerSummary").Key;
                            objUserACLDO.EnabledFeat.Add(myValue);
                        }
                        
                    }


                    bool check = objUserACLDBLayer.MapUsersAndFeatures(objUserACLDO, ref errorInfo);
                    if (check)
                    {
                        MessageBox.Show("Configured user access permision successfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtID.Text = "";
                        txtLoginID.Text = "";
                        txtUserName.Text = "";
                        txtUserRole.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Logger.LogFile(errorInfo, e.ToString(), ((Control)sender).Name, 143, this.FindForm().Name);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
            }
            else
            {
                MessageBox.Show("Please select user from the list to set access permision ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }


        private void FrmUserPerConfig_Load(object sender, EventArgs e)
        {
            try
            {
                if (objFeatureCollection == null)
                    objFeatureCollection = new Dictionary<int, string>();

                if (objUserACLDBLayer == null)
                    objUserACLDBLayer = new UserACLDBLayer();

                errorInfo = "";
                objFeatureCollection = objUserACLDBLayer.GetAllFeatures(ref errorInfo);

                dataGridUser.DataSource = MasterCache.GetUserDataFrmCache(ref errorInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
        }

        private void dataGridUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;

                if (i != -1)
                {

                    txtID.Text = dataGridUser[0, i].Value.ToString();
                    txtUserName.Text = dataGridUser[1, i].Value.ToString();
                    txtLoginID.Text = dataGridUser[2, i].Value.ToString();
                    txtUserRole.Text = dataGridUser[4, i].Value.ToString();


                    List<int> enabledFeatures = new List<int>();
                    UserACLDBLayer objUserACLDBLayer = new UserACLDBLayer();
                    enabledFeatures = objUserACLDBLayer.GetFeatureUserMap(Convert.ToUInt16(txtID.Text.Trim()), ref errorInfo);



                    objFeatureCollection = new Dictionary<int, string>();

                    if (objUserACLDBLayer == null)
                        objUserACLDBLayer = new UserACLDBLayer();

                    errorInfo = "";
                    objFeatureCollection = objUserACLDBLayer.GetAllFeatures(ref errorInfo);

                    List<string> listFeatures = new List<string>();

                    foreach (int featureID in enabledFeatures)
                    {
                        listFeatures.Add(objFeatureCollection.FirstOrDefault(x => x.Key == featureID).Value);
                    }

                    chkStock.Checked = false;
                    chkPurchaseEntry.Checked = false;
                    chkStockReturn.Checked = false;
                    chkReGenBarCode.Checked = false;
                    chkShowInvoice.Checked = false;
                    chkInvoice.Checked = false;
                    chkBilling.Checked = false;
                    chkReport.Checked = false;
                    chkRepair.Checked = false;
                    chkMaintenance.Checked = false;
                    chkShowRepair.Checked = false;
                    chkRepair.Checked = false;
                    chkCustomer.Checked = false;
                    chkSupplier.Checked = false;
                    chkAdministartion.Checked = false;
                    chkUserEmp.Checked = false;
                    chkBoxLedgerSum.Checked = false;

                    //Stock
                    if (listFeatures.Contains("Stock"))
                    {
                        chkStock.Checked = true;

                        if (listFeatures.Contains("PurchaseEntry"))
                        {
                            chkPurchaseEntry.Checked = true;
                        }

                        if (listFeatures.Contains("StockReturn"))
                        {
                            chkStockReturn.Checked = true;
                        }

                        if (listFeatures.Contains("ReGenerateBarcode"))
                        {
                            chkReGenBarCode.Checked = true;
                        }

                    }
                    //  else
                    //chkStock.Checked = false;

                    /// Billing
                    if (listFeatures.Contains("Billing"))
                    {
                        chkBilling.Checked = true;
                        if (listFeatures.Contains("Invoice"))
                        {
                            chkInvoice.Checked = true;
                        }
                        if (listFeatures.Contains("ShowInvoice"))
                        {
                            chkShowInvoice.Checked = true;
                        }

                    }


                    //Report
                    if (listFeatures.Contains("Report"))
                    {
                        chkReport.Checked = true;
                    }


                    // Maintanance
                    if (listFeatures.Contains("Maintanance"))
                    {
                        chkMaintenance.Checked = true;
                        if (listFeatures.Contains("Repair"))
                        {
                            chkRepair.Checked = true;
                        }

                        if (listFeatures.Contains("ShowRepair"))
                        {
                            chkShowRepair.Checked = true;
                        }

                    }


                    ///Administartion
                    if (listFeatures.Contains("Administartion"))
                    {
                        chkAdministartion.Checked = true;

                        if (listFeatures.Contains("User"))
                        {
                            chkUserEmp.Checked = true;
                        }

                        if (listFeatures.Contains("Customer"))
                        {
                            chkCustomer.Checked = true;
                        }

                        if (listFeatures.Contains("Supplier"))
                        {
                            chkSupplier.Checked = true;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
