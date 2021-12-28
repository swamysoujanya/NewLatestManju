using System;
using System.Collections.Generic;
using System.Text;

namespace AviMa.DataObjectLayer
{
  public  class InvoiceDO
    {
        public string InvoiceNumber { get; set; }
        public string Title { get; set; }
        public string TotalItems { get; set; }
        public string TotalAmount { get; set; }
        public string GrandTotalAmount { get; set; }
        public string Discuont { get; set; }
        public string PackedBy { get; set; }
        public string SaleType { get; set; }
        public char InvOrRepair { get; set; }
        public string UserLoginID { get; set; }
        public CustomerDO CustomerDetails { get; set; }
        public List<InvoiceItemsDO> ItemsList { get; set; }
        public string InvCreatedBy { get; set; }
        public string InvCreatedDate { get; set; }
        public string InvModiDate { get; set; }
        public string InvModiBy { get; set; }
        public bool FoundCustomer{ get; set; }
        public string InvNote { get; set; }
        public string OldBalance { get; set; }
        public bool PrintOldBalance { get; set; }
    }
}
