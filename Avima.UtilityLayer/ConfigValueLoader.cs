using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AviMa.UtilityLayer
{
   public static class ConfigValueLoader
    {
        //public const string ConnectionString = "Server = localhost; UserId = root; Pwd =1bm00ee039; Initial Catalog = avimadb"; // office Dev
        public const string ConnectionString = "Server = localhost; UserId = root; Pwd =avima; initial Catalog = avimadb"; // Manju , Home Dev
        //public static string ConnectionString = ConfigurationManager.AppSettings["DBConnection"];// "Server = localhost; UserId = root; Pwd =avima; Initial Catalog = avimadb";
        //public static string ConnectionString =  Convert.ToString(Properties.Settings.Default["SqlConnection"]);
        public static string PrinterName = GetPrinterName();// ConfigurationManager.AppSettings["PrinterName"]; //"CutePDF Writer";
        public static bool CalculateItemPrice = Convert.ToBoolean(ConfigurationManager.AppSettings["CalculateItemPrice"]); //"CutePDF Writer";
        public static string LogFilePath = ConfigurationManager.AppSettings["LogFilePath"]; //"CutePDF Writer";

        public static string GetPrinterName()
        {
            string printerName = "";
            try
            {
                
              //  XmlTextReader textReader = new XmlTextReader("AvimaConfigurables.xml");

                XmlDataDocument xmldoc = new XmlDataDocument();
                XmlNodeList xmlnode;

                if (File.Exists("AvimaConfigurables.xml"))
                {

                    FileStream fs = new FileStream("AvimaConfigurables.xml", FileMode.Open, FileAccess.Read);
                    xmldoc.Load(fs);
                    xmlnode = xmldoc.GetElementsByTagName("Name");
                    printerName = xmlnode[0].ChildNodes.Item(0).InnerText.Trim();
                }
                else
                {
                    return "Printer Not Configured";
                }
                

                return printerName;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogFile(ex.Message, "getting printer name", "", ex.LineNumber(), "get printer name");
            }
            finally
            {
               
            }
            return printerName;
        }



    }
}
