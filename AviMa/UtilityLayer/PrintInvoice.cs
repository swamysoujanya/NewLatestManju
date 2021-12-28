using AviMa.DataObjectLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AviMa.UtilityLayer
{
    class PrintInvoice
    {
        InvoiceDO objGeneralDeatils = null;

        public PrintInvoice(InvoiceDO _obGeneralDeatils)
        {
            objGeneralDeatils = _obGeneralDeatils;
            this.prnDocument = new System.Drawing.Printing.PrintDocument();
            // The Event of 'PrintPage'


            if (objGeneralDeatils.ItemsList.Count > 0)
            {
                try
                {
                    int batchjCount25 = objGeneralDeatils.ItemsList.Count / 25;
                    int itrator = 0; // batch of 25
                    for (int i = 0; i < batchjCount25; i++)
                    {
                        datesToPrint.Add(objGeneralDeatils.ItemsList[itrator].CreatedDateTime);
                        itrator += 25;
                    }

                    try
                    {
                        datesToPrint.Add(objGeneralDeatils.ItemsList[itrator].CreatedDateTime);
                    }
                    catch (Exception)
                    {
                        // dont do anything just continue execution
                    }


                    int test = 0;
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[0].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[25].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[50].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[75].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[100].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[125].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[150].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[175].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[200].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[225].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[250].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[275].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[300].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[325].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[350].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[375].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[400].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[425].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[450].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[475].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[500].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[525].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[550].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[575].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[600].CreatedDateTime);
                    //datesToPrint.Add(objGeneralDeatils.ItemsList[625].CreatedDateTime);
                }
                catch
                {
                    // dont do anything just continue execution
                }
            }



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
        private Font InvoiceFont = new Font("Arial", 8, FontStyle.Regular);
        // Invoice Font height
        private int InvoiceFontHeight;
        // Blue Color
        // private SolidBrush BlackBrush = new SolidBrush(Color.Blue);
        // Red Color
        private SolidBrush RedBrush = new SolidBrush(Color.Red);
        // Black Color
        private SolidBrush BlackBrush = new SolidBrush(Color.Black);

        private Font bold10font = new Font("Arial", 9, FontStyle.Bold);
        private Font grandTotalFont = new Font("Arial", 12, FontStyle.Bold);


        public bool PrintReport()
        {
            try
            {
                prnDocument.PrinterSettings.PrinterName = ConfigValueLoader.PrinterName;
                prnDocument.PrinterSettings.PrintFileName = AviMaConstants.CurrentDateTimesStamp;
                prnDocument.Print();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        int sino = 1;  // multi page printing
        int pageNo = 1;  // multi page printing
        Decimal grandTotal = 0;
        Decimal totalQntity = 0;



        List<DateTime> datesToPrint = new List<DateTime>();
        // Result of the Event 'PrintPage'
        private void prnDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {

                if (objGeneralDeatils.ItemsList.Count <= 25) // there is only one page for printing
                    firstPage = false;

                leftMargin = 20;// (int)e.MarginBounds.Left;
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


                // Check number of items and then set parameter accordingly
                //if(objGeneralDeatils != null && objGeneralDeatils.ItemsList != null && objGeneralDeatils.ItemsList.Count != 0)
                //e.HasMorePages = true;



                if (objGeneralDeatils.ItemsList.Count != 0)  // multi page printing
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

            foreach (DateTime item in datesToPrint)
            {
                InvTitle1 = objGeneralDeatils.Title + "                                       " + "Date:" + Convert.ToDateTime(item).ToString("dd/MM/yyy hh:mm:ss");
                break;
            }
            datesToPrint.RemoveAt(0);


            //InvTitle1 = objGeneralDeatils.Title + "                                       " + "Date:" + DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");
            InvTitle = "M/s. " + objGeneralDeatils.CustomerDetails.CustName;// "M/s. WWWWWWWWWWWWWWWWWWWWWWW"; Max limit 35
            try
            {

                // if (!string.IsNullOrEmpty(objGeneralDeatils.CustomerDetails.CustMobile1))
                InvSubTitle1 = "Mobile 1:" + objGeneralDeatils.CustomerDetails.CustMobile1 + "  Mobile 2:" + objGeneralDeatils.CustomerDetails.CustMobile2; // max limit 40
                                                                                                                                                            //else
                                                                                                                                                            //    InvSubTitle1 = "";

                //  if (!string.IsNullOrEmpty(objGeneralDeatils.CustomerDetails.CustTown))
                InvSubTitle2 = "Address:" + objGeneralDeatils.CustomerDetails.CustTown;// max limit 40
                //else
                //    InvSubTitle2 = "";
            }
            catch (Exception ex)
            {
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "PrintInvoice.cs");
            }

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
            int lenInvSubTitle1 = (int)g.MeasureString(InvSubTitle1, InvSubTitleFont).Width;
            int lenInvSubTitle2 = (int)g.MeasureString(InvSubTitle2, InvSubTitleFont).Width;
            int lenInvSubTitle3 = (int)g.MeasureString(InvSubTitle3, InvSubTitleFont).Width;
            int lenInvSubTitle4 = (int)g.MeasureString(InvSubTitle4, InvSubTitleFont).Width;

            // Set Titles Left:
            int xInvTitle1 = CurrentX;// CurrentX + (InvoiceWidth - lenInvTitle1) / 2;
            int xInvTitle = CurrentX;//CurrentX + (InvoiceWidth - lenInvTitle) / 2;
            int xInvSubTitle1 = CurrentX;// CurrentX;// CurrentX + (InvoiceWidth - lenInvSubTitle1) / 2;
            int xInvSubTitle2 = CurrentX;//CurrentX + (InvoiceWidth - lenInvSubTitle2) / 2;
            int xInvSubTitle3 = CurrentX + (InvoiceWidth - lenInvSubTitle3) / 2;
            int xInvSubTitle4 = CurrentX + (InvoiceWidth - lenInvSubTitle4) / 2;

            // Draw Invoice Head:    
            //  g.DrawRectangle(new Pen(Brushes.Black, 2), CurrentX - 15, CurrentY + 15, rightMargin, 1100);
            g.DrawRectangle(new Pen(Brushes.Black, 2), leftMargin + 105, CurrentY, rightMargin + 83, 760 + 25);

            int i = 15;

            if (InvTitle1 != "")
            {
                CurrentY = CurrentY + ImageHeight + 20;
                g.DrawString(InvTitle1, InvSubTitleFont, BlackBrush, xInvTitle1 + 15 + 120, CurrentY - i);
            }

            if (InvTitle != "")
            {
                CurrentY = CurrentY + InvTitleHeight;
                g.DrawString(InvTitle, InvTitleFont, BlackBrush, xInvTitle + 10 + 120, CurrentY - i);
            }
            if (InvSubTitle1 != "")
            {
                CurrentY = CurrentY + InvTitleHeight;
                g.DrawString(InvSubTitle1, InvSubTitleFont, BlackBrush, xInvSubTitle1 + 10 + 120, CurrentY - i);
                g.DrawString(" Estimate No.:" + objGeneralDeatils.InvoiceNumber, InvSubTitleFont, BlackBrush, 400 + 120, CurrentY - i);
            }
            if (InvSubTitle2 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight;
                g.DrawString(InvSubTitle2, InvSubTitleFont, BlackBrush, xInvSubTitle2 + 10 + 120, CurrentY - i);
            }


            if (InvSubTitle3 != "")
            {
                CurrentY = CurrentY + InvSubTitleHeight;
                g.DrawString(InvSubTitle3, InvSubTitleFont, BlackBrush, xInvSubTitle3 + 10 + 120, CurrentY - i);
            }

            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight;
            g.DrawLine(new Pen(Brushes.Black, 1), CurrentX - 14 + 120, CurrentY - 25, rightMargin + 85 + 120, CurrentY - 25);

            if (InvSubTitle4 != "")
            {
                CurrentY = CurrentY + 2;
                g.DrawString(InvSubTitle4, InvSubTitleFont, BlackBrush, xInvSubTitle4 + 20 + 120, CurrentY - i);
            }

            // Draw line:
            CurrentY = CurrentY + InvSubTitleHeight - i;


        }

        bool firstPage = true; // measurements are different for first page

        private void SetInvoiceData(Graphics g, System.Drawing.Printing.PrintPageEventArgs e)
        {  // Set Invoice Table:
            //string FieldValue = "";
            //int CurrentRecord = 0;
            //int RecordsPerPage = 20; // twenty items in a page
            //decimal Amount = 0;
            //bool StopReading = false;

            // Set Table Head:
            int xProductID = leftMargin;
            //CurrentY = CurrentY + InvoiceFontHeight + 20;
            // CurrentY = CurrentY;
            //  g.DrawString("SI.No.", InvoiceFont, BlackBrush, xProductID, CurrentY);
            g.DrawString("SI.No.", InvoiceFont, BlackBrush, leftMargin + 120, CurrentY - 20);

            //g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin , CurrentY);
            //g.DrawLine(new Pen(Brushes.Black), leftMargin, CurrentY, rightMargin, -20);
            // e.Graphics.DrawLine(Pens.Blue, 10, 10, 10, 200);


            if (firstPage) // Alignments only for the first page
            {
                e.Graphics.DrawLine(Pens.Black, leftMargin + 40 + 120, 75, leftMargin + 40 + 120, 605); // top x position -- top y  -- bottom x  --Total vertical lenght 
                e.Graphics.DrawLine(Pens.Black, 300 - 30 + 120, 75, 300 - 30 + 120, 625);
                e.Graphics.DrawLine(Pens.Black, 373 - 30 + 120, 75, 373 - 30 + 120, 625);
                e.Graphics.DrawLine(Pens.Black, 415 - 30 + 120, 75, 415 - 30 + 120, 625);
                e.Graphics.DrawLine(Pens.Black, 460 + 120, 75, 460 + 120, 625);

            }
            else
            {
                e.Graphics.DrawLine(Pens.Black, leftMargin + 40 + 120, 75, leftMargin + 40 + 120, 625); // top x position -- top y  -- bottom x  --Total vertical lenght 
                e.Graphics.DrawLine(Pens.Black, 300 - 30 + 120, 75, 300 - 30 + 120, 645);
                e.Graphics.DrawLine(Pens.Black, 373 - 30 + 120, 75, 373 - 30 + 120, 680);
                e.Graphics.DrawLine(Pens.Black, 415 - 30 + 120, 75, 415 - 30 + 120, 680);
                e.Graphics.DrawLine(Pens.Black, 460 + 120, 75, 460 + 120, 680);
            }




            int nleftMargin = leftMargin - 15 + 120;
            int nCuurentY = CurrentY + 20;
            int nrightMargin = rightMargin + 86;




            //for (int i = 0; i < 26; i++)
            //{
            //    g.DrawLine(new Pen(Brushes.Black), nleftMargin, nCuurentY - 15, nrightMargin + 120, nCuurentY - 15);
            //    nCuurentY += 20;
            //}


            #region print only required number of rows

            if (objGeneralDeatils.ItemsList.Count < 26)
            {
                int numbOfLines = objGeneralDeatils.ItemsList.Count + 1;

                for (int i = 0; i < numbOfLines; i++)
                {
                    g.DrawLine(new Pen(Brushes.Black), nleftMargin, nCuurentY - 15, nrightMargin + 120, nCuurentY - 15);
                    nCuurentY += 20;
                }

                int actuallines = 26 - numbOfLines;

                for (int i = 0; i < actuallines; i++)
                {
                    nCuurentY += 20;
                }

                g.DrawLine(new Pen(Brushes.Black), nleftMargin, nCuurentY - 15, nrightMargin + 120, nCuurentY - 15);
            }
            else
            {
                for (int i = 0; i < 26; i++)
                {
                    g.DrawLine(new Pen(Brushes.Black), nleftMargin, nCuurentY - 15, nrightMargin + 120, nCuurentY - 15);
                    nCuurentY += 20;
                }
            }
            nCuurentY += 20;

            #endregion print only required number of rows

            if (!firstPage)
            {
                g.DrawLine(new Pen(Brushes.Black), 270 + 120, nCuurentY - 15, nrightMargin + 120, nCuurentY - 15);
                g.DrawLine(new Pen(Brushes.Black), 343 + 120, nCuurentY, nrightMargin + 120, nCuurentY);
            }

            if (firstPage)
            {
                g.DrawString("Qnty:", InvSubTitleFont, BlackBrush, 420, nCuurentY - 48);
                g.DrawString("Total:", InvSubTitleFont, BlackBrush, 535, nCuurentY - 48);
            }
            else
            {
                g.DrawString("Qnty:", InvSubTitleFont, BlackBrush, 300 + 120, nCuurentY - 30);
                g.DrawString("Total:", InvSubTitleFont, BlackBrush, 415 + 120, nCuurentY - 30);
            }


            g.DrawString("Packed By:" + objGeneralDeatils.PackedBy, InvSubTitleFont, BlackBrush, leftMargin + 120, nCuurentY - 20);

            int xProductName = xProductID + (int)g.MeasureString("SI.No.", InvoiceFont).Width + 15;
            g.DrawString("Description", InvoiceFont, BlackBrush, xProductName + 120, CurrentY - 20);



            // int xUnitPrice = xProductName + (int)g.MeasureString("Product Name", InvoiceFont).Width + 72;
            int xUnitPrice = 300;
            g.DrawString("Bar Code", InvoiceFont, BlackBrush, xUnitPrice - 30 + 120, CurrentY - 20);

            //int xQuantity = xUnitPrice + (int)g.MeasureString("Unit Price", InvoiceFont).Width + 4;
            int xQuantity = 375;// xUnitPrice + (int)g.MeasureString("Unit Price", InvoiceFont).Width + 4;
            g.DrawString("Qnty", InvoiceFont, BlackBrush, xQuantity - 30 + 120, CurrentY - 20);

            //int xDiscount = xQuantity + (int)g.MeasureString("Quantity", InvoiceFont).Width + 4;
            int xDiscount = 425;//xQuantity + (int)g.MeasureString("Quantity", InvoiceFont).Width + 4;
            g.DrawString("Rate", InvoiceFont, BlackBrush, xDiscount - 30 + 120, CurrentY - 20);
            //   g.DrawString("in Rs", InvSubTitleFont, BlackBrush, xDiscount-30, CurrentY - 7);

            AmountPosition = 475;// xDiscount + (int)g.MeasureString("Discount", InvoiceFont).Width + 4;
            g.DrawString("Amount", InvoiceFont, BlackBrush, AmountPosition + 120, CurrentY - 20);
            // g.DrawString("in Rs", InvSubTitleFont, BlackBrush, AmountPosition, CurrentY - 7);


            int yPosition = 148;

            //foreach (InvoiceItemsDO item in objGeneralDeatils.ItemsList)
            //{

            Decimal totalAmntPerPage = 0;  // multi page printing
            Decimal totalQntyPerPage = 0;  // multi page printing
            for (int i = 0; i < 25; i++)      // multi page printing
            {


                if (objGeneralDeatils.ItemsList.Count > 0)
                {
                    InvoiceItemsDO item = objGeneralDeatils.ItemsList[0];

                    g.DrawString(item.SiNo, InvoiceFont, BlackBrush, leftMargin + 120, yPosition - 38);
                    g.DrawString(item.ItemName, InvoiceFont, BlackBrush, leftMargin + 40 + 120, yPosition - 38);
                    g.DrawString(item.BarCode, InvoiceFont, BlackBrush, 300 - 15 + 120, yPosition - 38);
                    g.DrawString(item.ItemQnty, InvoiceFont, BlackBrush, 371 - 15 + 115, yPosition - 38);
                    g.DrawString(item.ItemRate, InvoiceFont, BlackBrush, 413 - 20 + 120, yPosition - 38);
                    g.DrawString(item.Amount, InvoiceFont, BlackBrush, 475 + 120, yPosition - 38);
                    yPosition += 20;
                    //}

                    totalAmntPerPage += Convert.ToDecimal(item.Amount);
                    totalQntyPerPage += Convert.ToDecimal(item.ItemQnty);

                    objGeneralDeatils.ItemsList.RemoveAt(0);

                }
            }

            grandTotal += totalAmntPerPage;  // multi page printing
            totalQntity += totalQntyPerPage;

            if (firstPage)
            {
                g.DrawString(Convert.ToString(totalAmntPerPage), bold10font, BlackBrush, 463 + 120, nCuurentY - 50);
                g.DrawString(Convert.ToString(totalQntyPerPage), bold10font, BlackBrush, 350 + 115, nCuurentY - 50);
            }
            else
            {
                g.DrawString(Convert.ToString(totalAmntPerPage), bold10font, BlackBrush, 463 + 120, nCuurentY - 30);
                g.DrawString(Convert.ToString(totalQntyPerPage), bold10font, BlackBrush, 350 + 115, nCuurentY - 30);
            }

            if (objGeneralDeatils.ItemsList.Count == 0)
            {
                g.DrawString("Discount:", InvSubTitleFont, BlackBrush, 400 + 120, nCuurentY - 14);
                g.DrawString("Grand Total:", InvSubTitleFont, BlackBrush, 390 + 120, nCuurentY + 3);
                g.DrawString("Total Qnty:", InvSubTitleFont, BlackBrush, 250 + 145, nCuurentY + 5);
                g.DrawString(Convert.ToString(objGeneralDeatils.Discuont), bold10font, BlackBrush, 463 + 120, nCuurentY - 14);
                g.DrawString(Convert.ToString(objGeneralDeatils.GrandTotalAmount), grandTotalFont, BlackBrush, 463 + 120, nCuurentY + 3);
                g.DrawString(Convert.ToString(totalQntity), bold10font, BlackBrush, 350 + 115, nCuurentY + 5);

                if (objGeneralDeatils.PrintOldBalance)
                    g.DrawString("Old Balance: " + objGeneralDeatils.OldBalance, InvSubTitleFont, BlackBrush, 510, nCuurentY + 40);

                #region print note for last page


                string[] noteList = objGeneralDeatils.InvNote.Split('\n');
                g.DrawString("Note: ", bold10font, BlackBrush, leftMargin + 120, nCuurentY + 20);
                try
                {

                    g.DrawString(noteList[0], InvoiceFont, BlackBrush, leftMargin + 120, nCuurentY + 40);
                    g.DrawString(noteList[1], InvoiceFont, BlackBrush, leftMargin + 120, nCuurentY + 55);
                    g.DrawString(noteList[2], InvoiceFont, BlackBrush, leftMargin + 120, nCuurentY + 70);
                    g.DrawString(noteList[3], InvoiceFont, BlackBrush, leftMargin + 120, nCuurentY + 85);
                }
                catch
                {
                    // don't do anything it's just index out of range exception 
                }

                #endregion print note for last page


            }

            #region print note first page

            if (firstPage)
            {
                string[] noteList = objGeneralDeatils.InvNote.Split('\n');
                g.DrawString("Note: ", bold10font, BlackBrush, leftMargin + 120, nCuurentY + 20);
                try
                {

                    g.DrawString(noteList[0], InvoiceFont, BlackBrush, leftMargin + 120, nCuurentY + 40);
                    g.DrawString(noteList[1], InvoiceFont, BlackBrush, leftMargin + 120, nCuurentY + 55);
                    g.DrawString(noteList[2], InvoiceFont, BlackBrush, leftMargin + 120, nCuurentY + 70);
                    g.DrawString(noteList[3], InvoiceFont, BlackBrush, leftMargin + 120, nCuurentY + 85);
                }
                catch
                {
                    // don't do anything it's just index out of range exception 
                }
            }
            #endregion print note first page

            g.DrawString(" NO GUARANETEE  FOR IMPORTED TOYS ", bold10font, BlackBrush, leftMargin + 225, nCuurentY + 105);

            if (firstPage)
            {
                g.DrawLine(new Pen(Brushes.Black), 390, 625, nrightMargin + 120, 625);
            }
            else
            {
                g.DrawLine(new Pen(Brushes.Black), 463, nCuurentY + 20, nrightMargin + 120, nCuurentY + 20);
            }

            g.DrawString("Page No. -" + pageNo + "-", bold10font, BlackBrush, 458 + 120, nCuurentY + 105);


            firstPage = false; // on successfull completion of first page printing

            int ytotalPosition = 918;

            g.Dispose();
        }

    }
}
