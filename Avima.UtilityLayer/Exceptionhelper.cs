using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace AviMa.UtilityLayer
{
  public static class Exceptionhelper
    {


        public static  int LineNumber(this Exception e)
        {

            int linenum = 0;

            try
            {

                linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":line") + 5));

            }

            catch
            {

                //Stack trace is not available!

            }

            return linenum;

        }


    }

}
