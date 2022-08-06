using AviMa.AviMaForms;
using AviMa.DataBaseLayer;
using AviMa.UtilityLayer;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AviMa
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            try
            {
                #region Load Cache
                //string errorInfo = "";
                //bool check = true;
                //try
                //{
                //    check = MasterCache.SetCache(ref errorInfo);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Unabled to load cache data  " + ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    Logger.LogFile(ex.Message, errorInfo, " static void Main()", ex.LineNumber(), "Login form");
                //}

                //if (!check)
                //{
                //    MessageBox.Show("Unabled to load cache data " + errorInfo, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Error); // CH_23_11_2015
                //}
                //else
                //{
                //    //Application.Run(new LoginForm());
                //    //Application.Run(new frmLedgerReprot("TestUser"));
                //    //Application.Run(new frmLedger("TestUser"));
                //    //Application.Run(new POCForm());
                //    //Application.Run(new FrmUserPerConfig(1));
                //    //Application.Run(new frmMainForm("SuperAdmin"));
                //    //Application.Run(new frmCustomerAccounts("TestUser"));

                Application.Run(new DBIntialiser());

                //}
                #endregion Load Cache
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Program");
            }


        }
    }
}
