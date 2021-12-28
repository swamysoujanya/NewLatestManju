using Avima.UtilityLayer;
using AviMa.DataBaseLayer;
using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using BarcodeRender;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AviMa.AviMaForms
{
    public partial class POCForm : Form
    {
        public POCForm()
        {
            InitializeComponent();
        }

        private void POCForm_Load(object sender, EventArgs e)
        {

            /////////////////////////////////////////////////////////////////////////////////////////////////////

            CustomerDBLayer objCustomerDBLayer = new CustomerDBLayer();


            string errorInfo = "";
            DataTable dtCustomerDetails = new DataTable();
            dtCustomerDetails = objCustomerDBLayer.GetAllCustomers(ref errorInfo);
            DataTable dtCustomerDetails1 = new DataTable();
            dtCustomerDetails1 = objCustomerDBLayer.GetAllCustomers(ref errorInfo);
            DataTable dtCustomerDetails2 = new DataTable();
            dtCustomerDetails2 = objCustomerDBLayer.GetAllCustomers(ref errorInfo);
            if (dtCustomerDetails != null && dtCustomerDetails.Rows.Count > 0)
            {
                // dtCustomerDetails = dtCustomerDetails.Copy();
                //comboBox1.DataSource = dtCustomerDetails;
                //comboBox1.ValueMember = "CustID";
                //comboBox1.DisplayMember = "CustName";
                //dtCustomerDetails1 = dtCustomerDetails.Copy();
                //comboBox2.DataSource = dtCustomerDetails1;
                //comboBox2.ValueMember = "CustID";
                //comboBox2.DisplayMember = "CustName";
                //dtCustomerDetails2 = dtCustomerDetails.Copy();
                //comboBox3.DataSource = dtCustomerDetails2;
                //comboBox3.ValueMember = "CustID";
                //comboBox3.DisplayMember = "CustName";

                //  dtCustomerDetails = dtCustomerDetails.Copy();
                comboBox1.DataSource = dtCustomerDetails.Copy();
                comboBox1.ValueMember = "CustID";
                comboBox1.DisplayMember = "CustName";

                // dtCustomerDetails1 = dtCustomerDetails.Copy();
                comboBox2.DataSource = dtCustomerDetails.Copy();
                comboBox2.ValueMember = "CustID";
                comboBox2.DisplayMember = "CustName";

                // dtCustomerDetails2 = dtCustomerDetails.Copy();
                comboBox3.DataSource = dtCustomerDetails.Copy();
                comboBox3.ValueMember = "CustID";
                comboBox3.DisplayMember = "CustName";

            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////











            errorInfo = "";
            bool check = MasterCache.SetCache(ref errorInfo);






            //PODetailsDO objPODetailsDO = new PODetailsDO();
            //StockDBLayer objStockDBLayer = new StockDBLayer ();
            //string errorInfo = "";

            //objPODetailsDO.PODate =  AviMaConstants.CurrentDateTimesStamp;
            //objPODetailsDO.POSupplierID = 3;
            //objPODetailsDO.POReference = "sdfsdfsdf";
            //objPODetailsDO.POCreatedBy = "werwer";
            //objPODetailsDO.POCreatedDateTime =  AviMaConstants.CurrentDateTimesStamp;

            //objPODetailsDO.POItemsList = new List<ItemsDO>();

            //bool test = objStockDBLayer.CreateItem(objPODetailsDO, ref  errorInfo);










            ///////////////////////////////////////////////////////////
            //DataTable dt = new DataTable();
            //dt.Columns.Add("Name");
            //dt.Columns.Add("Country");

            //dt.Rows.Add("Name1", "Country1");
            //dt.Rows.Add("Name2", "Country2");

            //BindingSource bindingSource = new BindingSource();
            //bindingSource.DataSource = dt;
            //movieListBox.DataSource = bindingSource;
            //movieListBox.ValueMember = "Name";

            //textBoxName.DataBindings.Add(new Binding("Text", movieListBox.DataSource, "Name", true, DataSourceUpdateMode.OnPropertyChanged));





            #region Add column to grid at run time

            DataTable dt = new DataTable();
            InvoiceDBLayer objIOnv = new InvoiceDBLayer ();
            dt = objIOnv.GetInvoice("INV00005", AviMaConstants.InvoiceFlag, ref  errorInfo);
            InvoiceItemData obj = new InvoiceItemData(dt);
          //  obj.ShowDialog();

            #endregion Add column to grid at run time





        }
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            //string test = textBoxName.Text;
            //if (test.Length == 3)
            //{
            //    CustomerDBLayer objCustomerDBLayer = new CustomerDBLayer();
            //    string errorInfo = "";
            //    try
            //    {
            //        DataTable dtSearchResult = objCustomerDBLayer.SerahcCustomer("Man", "", "", ref errorInfo);

            //        listCustomerNames.DataSource = dtSearchResult;
            //        listCustomerNames.DisplayMember = "CustName";
            //        listCustomerNames.ValueMember = "CustID";

            //        listCustomerNames.Visible = true;

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Unable to populate user. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }



            //}
        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {
            string test = txtCustName.Text;
            if (test.Length == 3)
            {
                CustomerDBLayer objCustomerDBLayer = new CustomerDBLayer();
                string errorInfo = "";
                try
                {
                    DataTable dtSearchResult = objCustomerDBLayer.SerahcCustomer("Man", "", "", ref errorInfo);

                    txtCustName.DataSource = dtSearchResult;
                    txtCustName.DisplayMember = "CustName";
                    txtCustName.ValueMember = "CustID";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to populate user. " + ex.Message, "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
        }


        private void movieListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private int fontcount;
        private int fontposition = 1;
        private float ypos = 1;
        private PrintPreviewDialog previewDlg = null;

        List<string> testList = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {

            //Create a PrintPreviewDialog object
            // previewDlg = new PrintPreviewDialog();
            //Create a PrintDocument object
            PrintDocument pd = new PrintDocument();
            //Add print-page event handler



            testList.Add("test 1");
            testList.Add("test 2");
            testList.Add("test 3");
            testList.Add("test 4");
            testList.Add("test 5");
            testList.Add("test 6");
            testList.Add("test 7");
            testList.Add("test 8");
            testList.Add("test 9");
            testList.Add("test 10");
            testList.Add("test 11");
            testList.Add("test 12");
            testList.Add("test 13");
            testList.Add("test 14");
            testList.Add("test 15");
            testList.Add("test 16");
            testList.Add("test 17");
            testList.Add("test 18");
            testList.Add("test 19");
            testList.Add("test 20");
            testList.Add("test 21");
            testList.Add("test 22");
            testList.Add("test 23");
            testList.Add("test 24");
            testList.Add("test 25");
            testList.Add("test 26");
            testList.Add("test 27");
            testList.Add("test 28");
            testList.Add("test 29");
            testList.Add("test 30");
            testList.Add("test 31");
            testList.Add("test 32");
            testList.Add("test 33");

            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            //Print
            pd.Print();

        }



        public void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            ypos = 1;
            float pageheight = ev.MarginBounds.Height;
            //Create a Graphics object
            Graphics g = ev.Graphics;
            //Get installed fonts
            InstalledFontCollection ifc = new InstalledFontCollection();
            //Get font families
            //FontFamily[] ffs = ifc.Families;




            //Draw string on the paper
            // while (ypos + 60 < pageheight && fontposition < ffs.GetLength(0))


            //while (ypos + 60 < pageheight && fontposition < testList.Count)
            //{

            try
            {
                Font f = null;

                int breakLoop = 0;



                for (int i = 0; i < testList.Count; i++)
                {
                    //Get the font name 

                    f = new Font(testList[i], 25);

                    //Draw string
                    g.DrawString(testList[i], f, new SolidBrush(Color.Black), 1, ypos);
                    //fontposition = fontposition + 1;
                    ypos = ypos + 60;
                    //testList.RemoveAt(0);

                    if (breakLoop == 10)
                        break;
                    else
                        breakLoop++;
                }

            }
            catch (Exception)
            {
            }


            for (int rem = 0; rem < 10; rem++)
            {
                try
                {
                    testList.RemoveAt(0);
                }
                catch (Exception)
                {
                    break;
                }
            }

            if (testList.Count != 0)
                testList.RemoveAt(0);

            //   int i = 0;

            //foreach (string item in testList)
            //{
            //    //Get the font name 
            //    Font f =
            //    new Font(item, 25);
            //    //Draw string
            //    g.DrawString(item, f, new SolidBrush(Color.Black), 1, ypos);
            //    fontposition = fontposition + 1;
            //    ypos = ypos + 60;
            //    if (i == 10)
            //        break;
            //    else
            //        i++;
            //}

            //}

            //if (fontposition < testList.Count)
            //{
            //Has more pages??
            if (testList.Count != 0)
                ev.HasMorePages = true;
            //}
        }




        private void button2_Click(object sender, EventArgs e)
        {
            //ReportPrintDO OBJPODetailsDO = new ReportPrintDO();

            //AviMa.UtilityLayer.GenaralReport OBJpRINTpo = new UtilityLayer.GenaralReport(OBJPODetailsDO);
            //OBJpRINTpo.PrintReport();


            //InvoiceDBLayer objIOnv = new InvoiceDBLayer ();
            //string errorInfo="";
            //bool check = objIOnv.UpdateInvoiceNumber( "Invoice", ref errorInfo);



            try
            {

                int i = 0;
                int j = 5;

                int yield = j / i;

            }

            catch (Exception exe)
            {
                //call LogFile method and pass argument as Exception message, event name, control         name, error line number, current form name

                Logger.LogFile(exe.Message, e.ToString(), ((Control)sender).Name, exe.LineNumber(), this.FindForm().Name);

            }


            //PrintReport();

        }

        DataTable dtCustomerDetails = null;
        DataTable dtInvoiceDetails = null;
        InvoiceDBLayer objInvoiceDBLayer = new InvoiceDBLayer();
        CustomerDBLayer objCustomerDBLayer = new CustomerDBLayer();

        private bool PrintReport()
        {

            DataTable dtInvoiceDetails = new DataTable();


            dtCustomerDetails = new DataTable();
            string errorInfo = "";
            dtCustomerDetails = objCustomerDBLayer.GetAllCustomers(ref errorInfo);


            // dtInvoiceDetails = objInvoiceDBLayer.GetAllInvoices(AviMaConstants.InvoiceFlag, ref errorInfo);

            RefreshGrid(dtInvoiceDetails);

            ReportPrintDO _ObjReportPrintDO = new ReportPrintDO();



            _ObjReportPrintDO.RptDetails = dtInvoiceDetails;
            _ObjReportPrintDO.RptTotal = "1000";
            _ObjReportPrintDO.RPTName1 = "Sales Report";

            _ObjReportPrintDO.RPTName2 = "Supplier Name";
            _ObjReportPrintDO.RptDateTime = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");

            GenaralReport objGenaralReport = new GenaralReport(_ObjReportPrintDO);

            return objGenaralReport.PrintReport();
        }



        private void RefreshGrid(DataTable dtInvoiceDetails)
        {
            try
            {
                //   DataColumn objColSupName = new DataColumn("CustName");
                DataColumn objColSupName1 = new DataColumn("ponumber");
                DataColumn objColSupName2 = new DataColumn("SupplierName");
                DataColumn objColSupName3 = new DataColumn("podate");
                DataColumn objColSupName4 = new DataColumn("poTotalAmount");


                dtInvoiceDetails.Columns.Add(objColSupName1);
                dtInvoiceDetails.Columns.Add(objColSupName2);
                dtInvoiceDetails.Columns.Add(objColSupName3);
                dtInvoiceDetails.Columns.Add(objColSupName4);

                Int32 invTotalAmount = 0; //calculate total Amount


                for (int i = 0; i < 28; i++)
                {
                    //DataTableRow objRow ;

                    //objRow["ponumber"] = "sjfdhsdjh";
                    //objRow["SupplierName"] = "sjfdhsdjh";
                    //objRow["podate"] ="sjfdhsdjh";
                    //objRow["poTotalAmount"] = "sjfdhsdjh";

                    dtInvoiceDetails.Rows.Add("sjfdhsdjh", "sjfdhsdjh", "28/25/2015 00 00", "1");
                }

                //foreach (DataRow invoice in dtInvoiceDetails.Rows)
                //{
                //    foreach (DataRow cutomer in dtCustomerDetails.Rows)
                //    {
                //        //if (Convert.ToString(invoice["InvCustID"]) == Convert.ToString(cutomer["CustID"]))
                //        //{
                //        invoice["ponumber"] = "342342342";
                //       // }
                //    }

                //    invTotalAmount += Convert.ToInt32(invoice["InvTotalAmount"]); //calculate total Amount

                //}
                //dataGridPOReport.DataSource = dtInvoiceDetails;
                //txtGrnadTotal.Text = Convert.ToString(invTotalAmount); // calculate total Amount
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCacheTest_Click(object sender, EventArgs e)
        {
            string errorInfo = "";
            //bool check = MasterCache.SetCache(ref errorInfo);

            DataTable dtCustomersData = MasterCache.GetCustomerDataFrmCache(ref errorInfo);
            DataTable dtSupplierData = MasterCache.GetSupplierFrmCache(ref errorInfo);
            DataTable dtUserData = MasterCache.GetSupplierFrmCache(ref errorInfo);

            DataRow[] dtR = null;


            dtR = MasterCache.GetCustomerDataFrmCache(ref errorInfo).Select("CustName = '4555'");
        }

        private void textBoxName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (check)
                    button3_Click(sender, e);
                check = true;
            }
        }

        bool check = false;

        private void button3_Click(object sender, EventArgs e)
        {
            if (check)
            {
                check = false;
            }
            else
                check = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string errorInfo = "";
            BarcodeTestForm objFrm = new BarcodeTestForm(false);
            objFrm.barCodeDataList = objBarCodeDataList;
            objFrm.PrintBarCodes(ref  errorInfo);
            //this.Hide();
            //objFrm.ShowDialog();
            //this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string errorInfo = "";
            GetBarCodeDetails(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, ref  errorInfo);
        }

        #region barcode POC

        BarCodeDataList objBarCodeDataList = null;

        private void GetBarCodeDetails(string barcode, string pricecode, string itemname, string qnty, ref string errorInfo)
        {
            try
            {
                #region For Bar Code Printing

               
                    objBarCodeDataList = new BarCodeDataList();

                BarcodeData _objBarcodeData = new BarcodeData();
                _objBarcodeData.BarCode = barcode;
                _objBarcodeData.ItemName = itemname;
                _objBarcodeData.PriceCode = pricecode;
                _objBarcodeData.Qntity = qnty;
                if (objBarCodeDataList.ListOfBarcodeData == null)
                    objBarCodeDataList.ListOfBarcodeData = new List<BarcodeData>();

                objBarCodeDataList.ListOfBarcodeData.Add(_objBarcodeData);

                #endregion For Bar Code Printing
            }
            catch (Exception ex)
            {

            }
        }

        #endregion barcode POC



        FormNumberOfPrints objNumPrint = null;
        private void btnInvoiceTest_Click(object sender, EventArgs e)
        {


            try
            {
                if (objNumPrint == null)
                    objNumPrint = new FormNumberOfPrints();


                this.Hide();
                objNumPrint.ShowDialog();
                this.Show();

                decimal noOfPrints = objNumPrint.NumberOfCopies;

                for (int i = 0; i < noOfPrints; i++)
                {
                    InvoiceDO objInvoiceDO = new InvoiceDO();

                    objInvoiceDO.InvoiceNumber = "TestInv";
                    objInvoiceDO.GrandTotalAmount = "1000";
                    objInvoiceDO.InvCreatedBy = "TestUser";
                    objInvoiceDO.Title = "Test";
                    objInvoiceDO.Discuont = "1000";

                    objInvoiceDO.ItemsList = new List<InvoiceItemsDO>();
                    InvoiceItemsDO objInvoiceItemsDO = new InvoiceItemsDO();
                    objInvoiceItemsDO.SiNo = "1";
                    objInvoiceItemsDO.ItemName = "hgfghffgfhgffgthbnyjuiklo3568975856";
                    objInvoiceItemsDO.ItemQnty = "2";
                    objInvoiceItemsDO.BarCode = "1234";
                    objInvoiceItemsDO.ItemRate = "20";
                    objInvoiceItemsDO.Amount = "40";
                    objInvoiceDO.ItemsList.Add(objInvoiceItemsDO);

                    objInvoiceDO.CustomerDetails = new CustomerDO();
                    objInvoiceDO.CustomerDetails.CustName = "Test Customer";

                    PrintInvoice objPrintInvoice = new PrintInvoice(objInvoiceDO);
                    bool checkPrintSuccssfull = objPrintInvoice.PrintReport();
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            try
            {
                string errorInfo = "";

                int customerID = 0;
                string fromDate = "";
                string toDate = "";


                //temp Code
                //DataRow[] _tempdtInvoiceDetails = new DataRow();
                //_tempdtInvoiceDetails = dtInvoiceDetails.Select("where CustID = " + customerID + "  or  (InvCreatedDate BETWEEN  '" + fromDate + "' and   '" + toDate + "')");// );  //= objInvoiceDBLayer.SearchPO(suplierID, fromDate, toDate, ref errorInfo);

                dtCustomerDetails = objCustomerDBLayer.GetAllCustomers(ref errorInfo);

                dtInvoiceDetails = new DataTable();
                dtInvoiceDetails = objInvoiceDBLayer.GetAllInvoices(AviMaConstants.InvoiceFlag, ref errorInfo);

                if (errorInfo == string.Empty && dtInvoiceDetails != null && dtInvoiceDetails.Rows.Count > 0)
                {
                    // RefreshGrid(dtInvoiceDetails);

                    DataColumn objColSupName = new DataColumn("CustName");
                    dtInvoiceDetails.Columns.Add(objColSupName);

                    Int32 invTotalAmount = 0; //calculate total Amount

                    foreach (DataRow invoice in dtInvoiceDetails.Rows)
                    {
                        foreach (DataRow cutomer in dtCustomerDetails.Rows)
                        {
                            if (Convert.ToString(invoice["InvCustID"]) == Convert.ToString(cutomer["CustID"]))
                            {
                                invoice["CustName"] = cutomer["CustName"];
                            }
                        }

                    }


                }
                else
                {
                    dtInvoiceDetails = new DataTable();
                    RefreshGrid(dtInvoiceDetails);
                    MessageBox.Show("No results found for your search criteria. " + errorInfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                dataGridPOReport.DataSource = dtInvoiceDetails;
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



        int RowIndexForReprinting = -1;
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                MenuItem objItem = new MenuItem("Re-Print");
                objItem.Click += objItem_Click;
                m.MenuItems.Add(objItem);

                int currentMouseOverRow = dataGridPOReport.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    RowIndexForReprinting = currentMouseOverRow; ;
                }

                m.Show(dataGridPOReport, new Point(e.X, e.Y));
            }
        }


        string UserID = "tESTuSER";
        void objItem_Click(object sender, EventArgs e)
        {
            try
            {
                InvoiceDO objInvoiceDO = new InvoiceDO();
                string _errorInfo = "";
                // InvID, InvSaleType, InvOrRepair, InvTotalItems 3, InvTotalAmount, InvCustID 5, InvCreatedBy, InvCreatedDate, InvModifiedBy 8, InvModifiedDate, InvGrandTotalAmount, InvDiscount 11, `Column 11`, `Column 12`

                objInvoiceDO.InvoiceNumber = Convert.ToString(dataGridPOReport[0, RowIndexForReprinting].Value);
                objInvoiceDO.SaleType = Convert.ToString(dataGridPOReport[1, RowIndexForReprinting].Value);
                objInvoiceDO.InvOrRepair = Convert.ToChar(dataGridPOReport[2, RowIndexForReprinting].Value);
                objInvoiceDO.TotalItems = Convert.ToString(dataGridPOReport[3, RowIndexForReprinting].Value);
                objInvoiceDO.TotalAmount = Convert.ToString(dataGridPOReport[4, RowIndexForReprinting].Value);
                objInvoiceDO.Discuont = Convert.ToString(dataGridPOReport[11, RowIndexForReprinting].Value);
                objInvoiceDO.GrandTotalAmount = Convert.ToString(dataGridPOReport[10, RowIndexForReprinting].Value);
                objInvoiceDO.PackedBy = Convert.ToString(dataGridPOReport[6, RowIndexForReprinting].Value);

                objInvoiceDO.Title = AviMaConstants.INVOICE;
                objInvoiceDO.InvOrRepair = AviMaConstants.InvoiceFlag;

                objInvoiceDO.FoundCustomer = true;

                CustomerDO objCustomerDO = new CustomerDO();

                try  // temp code remove ex handling
                {
                    objCustomerDO.CustID = Convert.ToInt16(dataGridPOReport[5, RowIndexForReprinting].Value);
                    DataTable dtCustomer = new DataTable();
                    dtCustomer = MasterCache.GetCustomerDataFrmCache(ref _errorInfo);

                    DataRow[] dtRow = dtCustomer.Select("CustID = " + objCustomerDO.CustID);

                    foreach (DataRow customer in dtRow)
                    {
                        objCustomerDO.CustName = Convert.ToString(customer["CustName"]);
                        objCustomerDO.CustMobile1 = Convert.ToString(customer["CustMobile1"]);
                        objCustomerDO.CustMobile2 = Convert.ToString(customer["CustMobile2"]);
                        objCustomerDO.CustTown = Convert.ToString(customer["CustTown"]);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
                }
                objInvoiceDO.CustomerDetails = objCustomerDO;

                objInvoiceDO.UserLoginID = UserID;
                objInvoiceDO.InvCreatedBy = UserID;
                objInvoiceDO.InvCreatedDate = AviMaConstants.CurrentDateTimesStamp;


                List<InvoiceItemsDO> objItemsList = new List<InvoiceItemsDO>();

                DataTable objDtInvData = new DataTable();
                objDtInvData = objInvoiceDBLayer.GetInvoice(objInvoiceDO.InvoiceNumber, AviMaConstants.InvoiceFlag, ref _errorInfo);


                int j = 1;

                //InvID, InvBarCode, InvName, InvDescription, InvQnty, InvRate, InvTotalAmont, InvSupID, InvDateTime, InvCreatedBy, InvModifiedDate, InvModifiedBy, `Column 13`, `Column 14`

                foreach (DataRow item in objDtInvData.Rows)
                {
                    InvoiceItemsDO objInvoiceItems = new InvoiceItemsDO();
                    objInvoiceItems.SiNo = Convert.ToString(j);
                    objInvoiceItems.ItemName = Convert.ToString(item["InvName"]);
                    objInvoiceItems.BarCode = Convert.ToString(item["InvBarCode"]);
                    objInvoiceItems.ItemQnty = Convert.ToString(item["InvQnty"]);
                    objInvoiceItems.ItemRate = Convert.ToString(item["InvRate"]);
                    objInvoiceItems.Amount = Convert.ToString(item["InvTotalAmont"]);

                    objInvoiceItems.ItemSuplierID = "4";
                    objItemsList.Add(objInvoiceItems);
                    j++;
                }

                objInvoiceDO.ItemsList = objItemsList;

                PrintInvoice objPrintInvoice = new PrintInvoice(objInvoiceDO);
                bool checkPrintSuccssfull = objPrintInvoice.PrintReport();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Logger.LogFile(ex.Message, e.ToString(), ((Control)sender).Name, ex.LineNumber(), this.FindForm().Name);
            }


        }

        private void dataGridPOReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        private void btnValidateRegEx_Click(object sender, EventArgs e)
        {
            try
            {
                Regex r = new Regex(@"^-{0,1}\d+\.{0,1}\d*$"); // This is the main part, can be altered to match any desired form or limitations
                Match m = r.Match(textBox5.Text);
                if (!m.Success)
                {
                    MessageBox.Show("Enter only decimal numbers");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void button7_Click(object sender, EventArgs e)//DailyLedgerReportLayOutSetupAndPrint
        {
            DailyLedgerDO objDailyLedgerDO = new DailyLedgerDO();
            objDailyLedgerDO.OpeningBalance = "1000.00";
            objDailyLedgerDO.RowCount = 100;


        //    public const string SalesCash = "SalesCash";
        //public const string SalesCredit = "SalesCredit";
        //public const string Receipt = "Receipt";
        //public const string Payments = "Payments";



            objDailyLedgerDO.Sales = new List<Sale>();


            Sale objsale = new Sale();
            objsale.Amount = "1000";
            objsale.Type = AviMaConstants.SalesCash;
            objsale.Name = AviMaConstants.SalesCash;
            objDailyLedgerDO.Sales.Add(objsale);


            objsale = new Sale();
            objsale.Amount = "2000";
            objsale.Type = AviMaConstants.SalesCredit;
            objsale.Name = AviMaConstants.SalesCredit;
            objDailyLedgerDO.Sales.Add(objsale);


            objsale = new Sale();
            objsale.Amount = "3000";
            objsale.Type = AviMaConstants.Receipt;
            objsale.Name = AviMaConstants.Receipt;
            objDailyLedgerDO.Sales.Add(objsale);


            objsale = new Sale();
            objsale.Amount = "4000";
            objsale.Type = AviMaConstants.Payments;
            objsale.Name = AviMaConstants.Payments;
            objDailyLedgerDO.Sales.Add(objsale);

 
            PrntDailyLedRpt objPrntDailyLedRpt = new PrntDailyLedRpt(objDailyLedgerDO);

            objPrntDailyLedRpt.PrintReport();
        }
    }
}
