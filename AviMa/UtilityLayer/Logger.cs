using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AviMa.UtilityLayer
{
    public static class Logger
    {


        public static void LogFile(string sExceptionName, string sEventName, string sControlName, int nErrorLineNo, string sFormName)
        {
            StreamWriter log = null;

            try
            {           

                if (!File.Exists("logfile.txt"))
                {
                    log = new StreamWriter("logfile.txt");
                }

                else
                {
                    log = File.AppendText("logfile.txt");
                }

                // Write to the file:

                log.WriteLine("Data Time:" + DateTime.Now);
                log.WriteLine("Exception Name:" + sExceptionName);
                if(!string.IsNullOrEmpty(sEventName))
                log.WriteLine("Event Name:" + sEventName);
                if (!string.IsNullOrEmpty(sControlName))
                log.WriteLine("Control Name:" + sControlName);
                log.WriteLine("Error Line No.:" + nErrorLineNo);
                log.WriteLine("Form Name:" + sFormName);

                // Close the stream:

                log.Close();

            }
            catch (Exception ex)
            {
                 log.Close();
            }
        }



    }

}
