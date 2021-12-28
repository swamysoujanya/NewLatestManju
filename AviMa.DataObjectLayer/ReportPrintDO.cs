using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AviMa.DataObjectLayer
{
    public class ReportPrintDO
    {
        
            public DataTable RptDetails { set; get; }
            public string RPTName1 { get; set; }
            public string RPTName2 { get; set; }
            public string RptDateTime { get; set; }
            public string RptTotal { get; set; }

    }
}
