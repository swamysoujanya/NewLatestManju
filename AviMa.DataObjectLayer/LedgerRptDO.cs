using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviMa.DataObjectLayer
{
   public class LedgerRptDO
    {
        public int SiNo { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public string LedgerDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string Column6 { get; set; }
        public string Column7 { get; set; }
        public string Column8 { get; set; }
        public string Column9 { get; set; }
        public string Column10 { get; set; }
        public string Column11 { get; set; }
        public string  Column12 { get; set; }
    }
}
