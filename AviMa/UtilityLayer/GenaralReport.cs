using AviMa.DataObjectLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AviMa.UtilityLayer
{
    class GenaralReport
    {

        ReportPrintDO ObjReportPrintDO = null;


        public GenaralReport(ReportPrintDO _ObjReportPrintDO)
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



        public bool PrintReport()
        {
            try
            {                
                prnDocument.PrinterSettings.PrinterName = ConfigValueLoader.PrinterName;
                prnDocument.PrinterSettings.PrintFileName =  AviMaConstants.CurrentDateTimesStamp;
                prnDocument.Print();
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
                leftMargin = (int)e.MarginBounds.Left;
                rightMargin = 450;// (int)e.MarginBounds.Right;
                topMargin = 10;//(int)e.MarginBounds.Top;
                bottomMargin = (int)e.MarginBounds.Bottom;
                InvoiceWidth = (int)e.MarginBounds.Width;
                InvoiceHeight = (int)e.MarginBounds.Height;

                //if (!ReadInvoice)
                //    ReadInvoiceData();

                SetInvoiceHead(e.Graphics); // Draw Invoice Head
               // SetOrderData(e.Graphics); // Draw Order Data
                SetInvoiceData(e.Graphics, e); // Draw Invoice Data

                ReadInvoice = true;

                if (ObjReportPrintDO.RptDetails.Rows.Count != 0)  // multi page printing
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
            
            g.DrawRectangle(new Pen(Brushes.Black, 2), CurrentX - 15, CurrentY + 15, rightMargin, 550);


            if (InvTitle1 != "")
            {
                CurrentY = CurrentY + ImageHeight + 20;
                g.DrawString(InvTitle1, InvSubTitleFont, BlackBrush, xInvTitle1 + 15, CurrentY);
            }
            if (InvTitle != "")
            {
                CurrentY = CurrentY + InvTitleHeight;
                g.DrawString(InvTitle, InvTitleFont, BlackBrush, xInvTitle + 20, CurrentY -7);
            }
          
            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight;
            g.DrawLine(new Pen(Brushes.Black, 1), CurrentX - 14, CurrentY, rightMargin + 85, CurrentY);


            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight;

            

        }
        int sino = 1;  // multi page printing
        int pageNo = 1;  // multi page printing
        Int64 grandTotal = 0;      

        private void SetInvoiceData(Graphics g, System.Drawing.Printing.PrintPageEventArgs e)
        {  
            // Set Table Head:
            int xProductID = leftMargin;
            g.DrawString("SI.No.", InvoiceFont, BlackBrush, xProductID, CurrentY-11);

           
            e.Graphics.DrawLine(Pens.Black, 142, 63, 142, 536); // top x position -- top y  -- bottom x  --Total vertical lenght 
            e.Graphics.DrawLine(Pens.Black, 220, 63, 220, 536);
            e.Graphics.DrawLine(Pens.Black, 350, 63, 350, 556);
            e.Graphics.DrawLine(Pens.Black, 433, 63, 433, 556);
           


            int nleftMargin = leftMargin - 15;
            int nCuurentY = CurrentY + 20;
            int nrightMargin = rightMargin + 86;




            for (int i = 0; i < 23; i++)
            {
                g.DrawLine(new Pen(Brushes.Black), nleftMargin, nCuurentY, nrightMargin, nCuurentY);
                nCuurentY += 20;
            }

            g.DrawLine(new Pen(Brushes.Black), 350, nCuurentY, nrightMargin, nCuurentY);

           

            int xProductName = xProductID + (int)g.MeasureString("SI.No.", InvoiceFont).Width + 15;


            string number = "";
            string SupOrCust = "";
            string DateType = "";

           
                number = "PO No.";
                SupOrCust = "Supplier";
                DateType = "PO Date";

            

            g.DrawString(number, InvoiceFont, BlackBrush, xProductName - 5, CurrentY - 11);
            int xUnitPrice = xProductName + 150;
            g.DrawString(SupOrCust, InvoiceFont, BlackBrush, xUnitPrice - 50, CurrentY - 11);
            int xQuantity = 375;
            g.DrawString(DateType, InvoiceFont, BlackBrush, xQuantity - 5, CurrentY - 11);



            int xDiscount = 425;
            g.DrawString("Amount", InvoiceFont, BlackBrush, xDiscount +25, CurrentY - 11);

            int yPosition = 100 ;

            Int64 totalAmntPerPage = 0;  // multi page printing
            
                //foreach (DataRow item in ObjReportPrintDO.RptDetails.Rows)
                //{
                    // ponumber, posupplierID, poTotalAmount, podate, poRefernece, poCreatedDate, poCreatedBy ,SupplierName
              for (int i = 0; i < 22; i++)      // multi page printing
            {

                if (ObjReportPrintDO.RptDetails.Rows.Count > 0)
                {
                    DataRow item = ObjReportPrintDO.RptDetails.Rows[0];

                    g.DrawString(Convert.ToString(sino), InvoiceFont, BlackBrush, 110, yPosition);
                    g.DrawString(Convert.ToString(item["ponumber"]), InvoiceFont, BlackBrush, 150, yPosition);
                    g.DrawString(Convert.ToString(item["SupplierName"]), InvoiceFont, BlackBrush, 222, yPosition);
                    g.DrawString(Convert.ToString(item["podate"]).Substring(0, 10), InvoiceFont, BlackBrush, 355, yPosition);
                    g.DrawString(Convert.ToString(item["poTotalAmount"]), InvoiceFont, BlackBrush, 436, yPosition);
                    yPosition += 20;
                    sino++;
                    totalAmntPerPage += Convert.ToInt64(item["poTotalAmount"]);     
                    ObjReportPrintDO.RptDetails.Rows.Remove(item);
                }

            }
                //}


                grandTotal += totalAmntPerPage;  // multi page printing

            g.DrawString("Total", InvoiceFont, BlackBrush, 358, 540);
            g.DrawString(Convert.ToString(totalAmntPerPage), InvoiceFont, BlackBrush, 436, 540);  // multi page printing
            g.DrawString("Page No.:", InvoiceFont, BlackBrush, 358, 560);    // multi page printing
            g.DrawString(Convert.ToString(pageNo), InvoiceFont, BlackBrush, 436, 560);   // multi page printing


            g.Dispose();
        }

    }
}
