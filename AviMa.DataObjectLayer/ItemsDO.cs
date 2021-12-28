using System;
using System.Collections.Generic;
using System.Text;

namespace AviMa.DataObjectLayer
{
   

    public class PODetailsDO
    {
        public int POID { get; set; }
        public Int32 POSupplierID { get; set; }
        public Int64 poTotalAmount { get; set; }
        public string PODate { get; set; }
        public string POReference { get; set; }
        public List<ItemsDO> POItemsList { get; set; }
        public string POCreatedDateTime { get; set; }
        public string POCreatedBy { get; set; }
        public string POModifiedDate { get; set; }
        public string POModifiedBy { get; set; }
        public bool IsAddWithPO { get; set; }
    }

    public class ItemsDO
    {
        public Int32 ItemID { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Int32 Qnty { get; set; }
        public Decimal IPurchasePrice { get; set; }
        public Int64 SupID { get; set; }
        public Decimal WCashPrice { get; set; }
        public Decimal WCreditPrice { get; set; }
        public Decimal WRetailPrice { get; set; }
        public string CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Int64 PONumber { get; set; }
        public string PriceCode { get; set; }
    }


}
