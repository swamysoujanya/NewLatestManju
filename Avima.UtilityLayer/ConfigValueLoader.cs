using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace AviMa.UtilityLayer
{
   public static class ConfigValueLoader
    {
        //public const string ConnectionString = "Server = localhost; UserId = root; Pwd =1bm00ee039; Initial Catalog = avimadb"; // office Dev
        public const string ConnectionString = "Server = localhost; UserId = root; Pwd =avima; initial Catalog = avimadb"; // Manju , Home Dev
        //public static string ConnectionString = ConfigurationManager.AppSettings["DBConnection"];// "Server = localhost; UserId = root; Pwd =avima; Initial Catalog = avimadb";
        //public static string ConnectionString =  Convert.ToString(Properties.Settings.Default["SqlConnection"]);
        public static  string PrinterName = ConfigurationManager.AppSettings["PrinterName"]; //"CutePDF Writer";
        public static bool CalculateItemPrice = Convert.ToBoolean(ConfigurationManager.AppSettings["CalculateItemPrice"]); //"CutePDF Writer";
        public static string LogFilePath = ConfigurationManager.AppSettings["LogFilePath"]; //"CutePDF Writer";
       
    }
}
