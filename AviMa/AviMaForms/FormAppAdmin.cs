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
    public partial class FormAppAdmin : Form
    {
        public string UserID { get; set; }


        public FormAppAdmin(string userID)
        {
            InitializeComponent();
            UserID = userID;
        }
        string errorInfo = "";
        AppConfigDB objAppConfigDB = new AppConfigDB();
        private void button1_Click(object sender, EventArgs e) // Set Default Discount
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDefaultDisc.Text))
                {
                    if (MessageBox.Show("Set default discount.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        AppConfigDO objAppConfigDO = new AppConfigDO();

                        objAppConfigDO.ConfigKey = AviMaConstants.DefaultDiscount;
                        int discValue = 0;
                        try
                        {
                            discValue = Convert.ToInt16(txtDefaultDisc.Text);
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        objAppConfigDO.ConfigValue = txtDefaultDisc.Text;
                        objAppConfigDO.CreatedBy = UserID;
                        objAppConfigDO.CreatedDate = AviMaConstants.CurrentDateTimesStamp;

                        bool _check = objAppConfigDB.CreateConfig(objAppConfigDO, ref errorInfo);


                        if (_check)
                        {
                            MasterCache.SetAppConfigData(ref errorInfo);
                            MessageBox.Show("Default discount set successfully.", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to set default discount. " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            #region default discount

            try
            {

                txtDefaultDisc.Text = objAppConfigDB.GetDeafaultDiscount(ref errorInfo);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
            #endregion default discount
        }

        private void FormAppAdmin_Load(object sender, EventArgs e)
        {
            #region default discount

            try
            {

                txtDefaultDisc.Text = objAppConfigDB.GetDeafaultDiscount(ref errorInfo);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }
            #endregion default discount
        }

        private void button2_Click(object sender, EventArgs e) //Refresh Invoice Numebrs
        {
            try
            {
                List<string> MissingInvNo = new List<string>();
                string missingInvoiceNum = "";
                DataTable dtInvoices = new DataTable();
                InvoiceDBLayer objInvoiceDBLayer = new InvoiceDBLayer();
                string error = "";
                dtInvoices = objInvoiceDBLayer.GetAllInvoices(AviMaConstants.InvoiceFlag, ref error);
                Dictionary<string, string> objInvNumMapping = new Dictionary<string, string>();

                if (dtInvoices != null && dtInvoices.Rows.Count > 0)
                {
                    string invLatestNumb = objInvoiceDBLayer.GetInvoiceNumber("Invoice", ref error);

                    invLatestNumb = invLatestNumb.Remove(0, 3);

                    int countofInvoices = dtInvoices.Rows.Count;

                    if (Convert.ToInt16(invLatestNumb) - 1 != countofInvoices)
                    {
                        int invNoCurrent = 1;


                        foreach (DataRow item in dtInvoices.Rows)
                        {
                            bool checkValuFound = false;
                            while (!checkValuFound)
                            {
                                string finalInv = "";
                                int lentgth = Convert.ToString(invNoCurrent).Length;

                                if (lentgth != 5)
                                {
                                    int apndZero = 5 - lentgth;

                                    string zeros = "";
                                    for (int i = 0; i < apndZero; i++)
                                    {
                                        zeros += "0";
                                    }
                                    //finalInv = "INV" + zeros + invNoCurrent;
                                    finalInv = "000" + zeros + invNoCurrent;
                                }

                                if (Convert.ToString(item["InvID"]) != finalInv)
                                {
                                    MissingInvNo.Add(finalInv);
                                    missingInvoiceNum += ", " + finalInv;
                                }
                                else
                                {
                                    checkValuFound = true;
                                }
                                invNoCurrent++;
                            }
                        }
                     
                        if (MissingInvNo.Count != 0 && missingInvoiceNum != "")
                        {
                            if (MessageBox.Show("Missing invoice number(s). :\n Totat missing inovoice number(s) : " + MissingInvNo.Count + "\n\n" + missingInvoiceNum + "\n\nAdjust all the above invoice numbers", "Missing invoice number(s).", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                DataTable dtInvNoForRefresh = new DataTable();
                                dtInvNoForRefresh = objInvoiceDBLayer.GetInvoicesBetween(MissingInvNo[0], ref errorInfo);

                                int currentInvNumber = Convert.ToInt16(MissingInvNo[0].Remove(0, 3));                              

                                foreach (DataRow item in dtInvNoForRefresh.Rows)
                                {
                                    string newInvNumber = objInvoiceDBLayer.GetInvoiceNumber(currentInvNumber, ref errorInfo);
                                    objInvNumMapping.Add(Convert.ToString(item["InvID"]), newInvNumber);// oldinv number , new inv number
                                    item["InvID"] = newInvNumber;
                                    currentInvNumber++;

                                }

                                bool check = objInvoiceDBLayer.RefreshInvIDS(objInvNumMapping,false, ref errorInfo);

                                if (check)
                                {
                                    MasterCache.SetAppConfigData(ref errorInfo);
                                    MessageBox.Show("Done with refreshing invoice number(s).", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Unable to refresh  invoice number(s). " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }
                        else
                        {
                            

                            MessageBox.Show("No invoice numbers to refresh");
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("Set start invoice number to 00000001", "Missing invoice number(s).", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        bool check = objInvoiceDBLayer.RefreshInvIDS(objInvNumMapping, true, ref errorInfo);

                        if (check)
                        {
                            MasterCache.SetAppConfigData(ref errorInfo);
                            MessageBox.Show("Done with refreshing invoice number(s).", "Success Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Unable to refresh  invoice number(s). " + errorInfo, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
