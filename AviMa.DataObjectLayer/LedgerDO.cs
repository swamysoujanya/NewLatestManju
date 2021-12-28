using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviMa.DataObjectLayer
{
   public class LedgerDO
    {
        public int SiNo { get; set; }
        public string Date { get; set; }
        public string AccountType { get; set; }
        public string PaidTo { get; set; }
        public string Amount { get; set; }
        public string Note { get; set; }
        public string TaxPaid { get; set; }
        public string ModyfiedBy { get; set; }
        public string ModyfiedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDateTime { get; set; }
    }
}
