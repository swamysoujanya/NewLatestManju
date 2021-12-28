using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviMa.DataObjectLayer
{
    public class DailyLedgerDO
    {
        public string OpeningBalance { get; set; }
        public List<Sale> Sales { get; set; }
        public string CashInHand { get; set; }
        public int RowCount { get; set; }
    }

    public class Sale
    {
        public string Name { get; set; }
        public string Amount { get; set; }
        /// <summary>
        /// SalesCash, SalesCredit, Receipt,Payments
        /// </summary>
        public string Type { get; set; }
    }



}
