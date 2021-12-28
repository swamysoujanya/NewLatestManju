using AviMa.DataObjectLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AviMa.UtilityLayer
{
    class PrintInvoiceRprt
    {
          ReportPrintDO ObjReportPrintDO = null;

        public PrintInvoiceRprt(ReportPrintDO _ObjReportPrintDO)
        {
            ObjReportPrintDO = _ObjReportPrintDO;
            this.prnDocument = new System.Drawing.Printing.PrintDocument();
            // The Event of 'PrintPage'
            prnDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(prnDocument_PrintPage);
            

        }

        private System.Drawing.Printing.PrintDocument prnDocument;


        // for Invoice Head:
        private string InvTitle1;
        private string InvTitle;
        private string InvSubTitle1;
        private string InvSubTitle2;
        private string InvSubTitle3;
        private string InvSubTitle4;
        private string InvImage;

        // for Report:
        private int CurrentY;
        private int CurrentX;
        private int leftMargin;
        private int rightMargin;
        private int topMargin;
        private int bottomMargin;
        private int InvoiceWidth;
        private int InvoiceHeight;
        private string CustomerName;
        private string CustomerCity;
        private string SellerName;
        private string SaleID;
        private string SaleDate;
        private decimal SaleFreight;
        private decimal SubTotal;
        private decimal InvoiceTotal;
        private bool ReadInvoice;
        private int AmountPosition;

        // Font and Color:------------------
        // Title Font
        private Font InvTitleFont = new Font("Arial", 12, FontStyle.Bold);
        // Title Font height
        private int InvTitleHeight;
        // SubTitle Font
        private Font InvSubTitleFont = new Font("Arial", 8, FontStyle.Regular);
        // SubTitle Font height
        private int InvSubTitleHeight;
        // Invoice Font
        private Font InvoiceFont = new Font("Arial", 10, FontStyle.Regular);
        // Invoice Font height
        private int InvoiceFontHeight;
        // Blue Color
        // private SolidBrush BlackBrush = new SolidBrush(Color.Blue);
        // Red Color
        private SolidBrush RedBrush = new SolidBrush(Color.Red);
        // Black Color
        private SolidBrush BlackBrush = new SolidBrush(Color.Black);


        string _listOfSearchInvIds = "";

        public bool PrintReport(ref string listOfSearchInvIds)
        {
            try
            {
                _listOfSearchInvIds = "";
                prnDocument.PrinterSettings.PrinterName = ConfigValueLoader.PrinterName;
                prnDocument.PrinterSettings.PrintFileName =  AviMaConstants.CurrentDateTimesStamp;
                prnDocument.Print();

                listOfSearchInvIds = _listOfSearchInvIds;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Result of the Event 'PrintPage'
        private void prnDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
            //    leftMargin = (int)e.MarginBounds.Left;
            //    rightMargin = 450;// (int)e.MarginBounds.Right;
            //    topMargin = 10;//(int)e.MarginBounds.Top;
            //    bottomMargin = (int)e.MarginBounds.Bottom;
            //    InvoiceWidth = (int)e.MarginBounds.Width;
            //    InvoiceHeight = (int)e.MarginBounds.Height;


             leftMargin = 20;//  (int)e.MarginBounds.Left;
            rightMargin = 485;// (int)e.MarginBounds.Right;
            topMargin = 0;//(int)e.MarginBounds.Top;
            bottomMargin = 0;// (int)e.MarginBounds.Bottom;
            InvoiceWidth = (int)e.MarginBounds.Width;
            InvoiceHeight = (int)e.MarginBounds.Height;

            //if (!ReadInvoice)
            //    ReadInvoiceData();

            SetInvoiceHead(e.Graphics); // Draw Invoice Head
               // SetOrderData(e.Graphics); // Draw Order Data
                SetInvoiceData(e.Graphics, e); // Draw Invoice Data

                ReadInvoice = true;

                if (ObjReportPrintDO.RptDetails.Rows.Count !=0)  // multi page printing
                {
                    e.HasMorePages = true;
                    pageNo++;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }


        private void ReadInvoiceHead()
        {
            //Titles and Image of invoice:
            InvTitle1 = ObjReportPrintDO.RPTName1 + "                      " + "Date:" + DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");
            InvTitle =ObjReportPrintDO.RPTName2;// Max limit 35
          
        }

        private void SetInvoiceHead(Graphics g)
        {
            ReadInvoiceHead();

            CurrentY = topMargin;
            CurrentX = leftMargin;
            int ImageHeight = 0;


            InvTitleHeight = (int)(InvTitleFont.GetHeight(g));
            InvSubTitleHeight = (int)(InvSubTitleFont.GetHeight(g)) + 2;

            // Get Titles Length:
            int lenInvTitle1 = (int)g.MeasureString(InvTitle1, InvSubTitleFont).Width;
            int lenInvTitle = (int)g.MeasureString(InvTitle, InvTitleFont).Width;
           
            // Set Titles Left:
            int xInvTitle1 = CurrentX ;
            int xInvTitle = CurrentX;

            //g.DrawRectangle(new Pen(Brushes.Black, 2), CurrentX - 15, CurrentY, rightMargin + 83, 760);
            g.DrawRectangle(new Pen(Brushes.Black, 2), leftMargin + 105, CurrentY, rightMargin + 83, 760);


            if (InvTitle1 != "")
            {
                CurrentY = CurrentY + ImageHeight + 20;
                g.DrawString(InvTitle1, InvSubTitleFont, BlackBrush, xInvTitle1 + 15+120, CurrentY);
            }
            if (InvTitle != "")
            {
                CurrentY = CurrentY + InvTitleHeight;
                g.DrawString(InvTitle, InvTitleFont, BlackBrush, xInvTitle + 20 + 120, CurrentY -7);
            }
          
            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight;
            g.DrawLine(new Pen(Brushes.Black, 1), CurrentX - 14 + 120, CurrentY, rightMargin + 85 + 120, CurrentY);


            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight;

            

        }

        int sino = 1;  // multi page printing
        int pageNo = 1;  // multi page printing
        Int64 grandTotal = 0;

        private void SetInvoiceData(Graphics g, System.Drawing.Printing.PrintPageEventArgs e)
        {  
            // Set Table Head:
            int xProductID = leftMargin + 120;
            g.DrawString("SI.No.", InvoiceFont, BlackBrush, leftMargin + 120, CurrentY-11);

            int xStart = leftMargin +40 + 120;
            e.Graphics.DrawLine(Pens.Black, xStart, 53, xStart, 688); // top x position -- top y  -- bottom x  --Total vertical lenght 
            xStart += 78;
            e.Graphics.DrawLine(Pens.Black, xStart, 53, xStart, 688);
            xStart += 130;
            e.Graphics.DrawLine(Pens.Black, xStart+30, 53, xStart + 30, 688);

            g.DrawString("Sale Type", InvoiceFont, BlackBrush, xStart+5 + 30, CurrentY - 11);

            xStart += 83;
            e.Graphics.DrawLine(Pens.Black, xStart+30, 53, xStart + 30, 708);
            xStart += 83;
            e.Graphics.DrawLine(Pens.Black, xStart+30, 53, xStart + 30, 708);


            int nleftMargin = leftMargin - 15;
            int nCuurentY = CurrentY + 20;
            int nrightMargin = rightMargin + 86;




            for (int i = 0; i < 31; i++)
            {
                g.DrawLine(new Pen(Brushes.Black), nleftMargin + 120, nCuurentY, nrightMargin + 120, nCuurentY);
                nCuurentY += 20;
            }

            g.DrawLine(new Pen(Brushes.Black), 350 + 120+30, nCuurentY, nrightMargin + 120, nCuurentY);

           

            int xProductName = xProductID + (int)g.MeasureString("SI.No.", InvoiceFont).Width + 15;


            string number = "";
            string SupOrCust = "";
            string DateType = "";

           
                number = "Inv No.";
                SupOrCust = "Customer";
                DateType = "Inv Date";
           

            g.DrawString(number, InvoiceFont, BlackBrush, xProductName - 5 , CurrentY - 11);
            int xUnitPrice = xProductName + 150;
            g.DrawString(SupOrCust, InvoiceFont, BlackBrush, xUnitPrice - 50 , CurrentY - 11);

          

            int xQuantity = 375;
            g.DrawString(DateType, InvoiceFont, BlackBrush, xQuantity - 5 + 120 + 30, CurrentY - 11);



            int xDiscount = 425;
            g.DrawString("Amount", InvoiceFont, BlackBrush, xDiscount +25 + 120 + 30, CurrentY - 11);

            int yPosition = 100 ;

            
                //foreach (DataRow item in ObjReportPrintDO.RptDetails.Rows)
                //{
                    // ponumber, posupplierID, poTotalAmount, podate, poRefernece, poCreatedDate, poCreatedBy ,SupplierName

            Int64 totalAmntPerPage = 0;  // multi page printing

            for (int i = 0; i < 30; i++)      // multi page printing
            {

                if (ObjReportPrintDO.RptDetails.Rows.Count > 0)
                {
                    DataRow item = ObjReportPrintDO.RptDetails.Rows[0];

                    _listOfSearchInvIds += "'"+ Convert.ToString(item["InvID"]) + "' ," ;

                    g.DrawString(Convert.ToString(sino), InvoiceFont, BlackBrush, leftMargin + 120, yPosition-10);
                    g.DrawString(Convert.ToString(item["InvID"]), InvoiceFont, BlackBrush, leftMargin + 43 + 120, yPosition-10);
                    g.DrawString(Convert.ToString(item["CustName"]), InvoiceFont, BlackBrush, leftMargin + 43 +80 + 120, yPosition-10);

                    string saleType = "";

                    if (Convert.ToString(item["InvSaleType"]) == AviMaConstants.WholeSaleCash)
                        saleType = "Cash";
                    else if(Convert.ToString(item["InvSaleType"]) == AviMaConstants.WholeSaleCredit)
                        saleType = "Credit";
                    else if (Convert.ToString(item["InvSaleType"]) == AviMaConstants.Retail)
                        saleType = "Retail";

                    g.DrawString(saleType, InvoiceFont, BlackBrush, leftMargin + 43 + 230 + 120 + 30, yPosition - 10);
                    if (Convert.ToString(item["InvCreatedDate"]).Length > 10)
                        g.DrawString(Convert.ToString(item["InvCreatedDate"]).Substring(0, 10), InvoiceFont, BlackBrush, 355 + 120 +30, yPosition-10);
                    g.DrawString(Convert.ToString(item["InvGrandTotalAmount"]), InvoiceFont, BlackBrush, 436 + 120 +30, yPosition-10);

                   

                    totalAmntPerPage += Convert.ToInt64(item["InvGrandTotalAmount"]);

                  

                    yPosition += 20;
                    sino++;

                    ObjReportPrintDO.RptDetails.Rows.Remove(item);
                }

            }

            grandTotal += totalAmntPerPage;  // multi page printing
                   
                //}    

            g.DrawString("Total", InvoiceFont, BlackBrush, 358 + 120+30, 688);
            g.DrawString(Convert.ToString(totalAmntPerPage), InvoiceFont, BlackBrush, 436 + 120+30, 688);  // multi page printing
            g.DrawString("Page No.:", InvoiceFont, BlackBrush, 358 + 120+30, 730);    // multi page printing
            g.DrawString(Convert.ToString(pageNo), InvoiceFont, BlackBrush, 436 + 120+30, 730);   // multi page printing


            if(ObjReportPrintDO.RptDetails.Rows.Count == 0)
            {
                g.DrawString("Grand Total:", InvoiceFont, BlackBrush, leftMargin + 120+30, 730);    // multi page printing
                g.DrawString("Rs."+String.Format("{0:n}", grandTotal), InvoiceFont, BlackBrush, leftMargin +80 + 120+30, 730);   // multi page printing
                
            }

            g.Dispose();
        }
    }
}
