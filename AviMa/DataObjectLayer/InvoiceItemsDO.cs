using System;
using System.Collections.Generic;
using System.Text;

namespace AviMa.DataObjectLayer
{
   public class InvoiceItemsDO
    {
        public string SiNo { get; set; }
        public string BarCode { get; set; }
        public string ItemName { get; set; }
        public string ItemQnty { get; set; }
        public string ItemRate { get; set; }       
        public string ItemSuplierID { get; set; }
        public string Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
