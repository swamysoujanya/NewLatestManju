using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Avima.UtilityLayer;
using AviMa.DataBaseLayer;
using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using System.Xml;
using System.Configuration;

namespace AviMa.AviMaForms
{
    public partial class frmPrinterConfigure : Form
    {
        public frmPrinterConfigure()
        {
            InitializeComponent();
        }

        private void frmPrinterConfigure_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (string printname in PrinterSettings.InstalledPrinters)
                {
                    comboBox1.Items.Add(printname);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
               if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    MessageBox.Show("Please select printername");
                    return;
                }
                else
                {
                    string selectPrinterName = comboBox1.Text;

                   
                    string filepath = @"AvimaConfigurables.xml";
                    XmlTextWriter xmlwriter = new XmlTextWriter(filepath, Encoding.UTF8);

                    xmlwriter.Formatting = Formatting.Indented;

                    xmlwriter.WriteStartDocument();

                    xmlwriter.WriteStartElement("Printer");



                    xmlwriter.WriteElementString("Name", selectPrinterName);


                    xmlwriter.WriteEndElement();
                    xmlwriter.WriteEndDocument();

                    xmlwriter.Flush();

                    xmlwriter.Close();

                    MessageBox.Show("Printer Configured Successfully...Now app will close...Please start again");

                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), this.FindForm().Name);
            }
        }
    }
}
