using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AviMa.DataObjectLayer
{
  public  class AccountsDO
    {
        /// <summary>
        /// Customer orSupplier
        /// </summary>
        public string AccountsType { get; set; }
        public int SiNo { get; set; }
        public string Name { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Balance { get; set; }
        public string Description { get; set; }
        public int CustSupID { get; set; }
        public string ModyfiedBy { get; set; }
        public string ModyfiedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDateTime { get; set; }
        public decimal CurrentDebited { get; set; }
        public decimal CurrentCredited { get; set; }
        public char CustOrSup { get; set; }
    }

    public class LedgerPrintDO
    {
        public DataTable ListAccountsDO { get; set; }
        public string Name { get; set; }
        public char CustOrSup { get; set; }
    }

    //public class PrintAccountsDO
    //{        
    //    public int SiNo { get; set; }        
    //    public string Credit { get; set; }
    //    public string Debit { get; set; }
    //    public string Balance { get; set; }
    //    public string Description { get; set; }
    //    public int CustSupID { get; set; }
    //    public string ModyfiedBy { get; set; }
    //    public string ModyfiedDateTime { get; set; }
    //    public string CreatedBy { get; set; }
    //    public string CreatedDateTime { get; set; }
    //}


}
