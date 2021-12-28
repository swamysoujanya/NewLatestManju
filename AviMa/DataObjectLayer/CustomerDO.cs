using System;
using System.Collections.Generic;
using System.Text;

namespace AviMa.DataObjectLayer
{
   public class CustomerDO
    {
        public int CustID { get; set; }
        public string CustName { get; set; }
        public string CustMobile1 { get; set; }
        public string CustMobile2 { get; set; }
        public string CustEmail { get; set; }
        public string CustState { get; set; }
        public string CustDist { get; set; }
        public string CustTown { get; set; }
        public string CustDescri { get; set; }
        public string CustCreatedBy { get; set; }
        public string CustCreatedDate { get; set; }       
        public string CustModiDate { get; set; }
        public string CustModiBy { get; set; }


    }
}
