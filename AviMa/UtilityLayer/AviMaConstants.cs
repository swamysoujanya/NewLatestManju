using System;
using System.Collections.Generic;
using System.Text;

namespace AviMa.UtilityLayer
{
    static class AviMaConstants
    {

        public const string Create_Stcok = "Create Stock";
        public const string Edit_Stcok = "Edit Stock";
        public const string Delete_Stcok = "Delete Stock";
        public const string Return_Stcok_By_Customer = "Return Stock By Customer";
        public const string Return_Stcok_To_Supplier = "Return Stock By Supplier";


        public const string INVOICE = "ESTIMATE/APPROVAL";
        public const string REPAIR = "FOR REPAIR";

        public const char InvoiceFlag = 'I';
        public const char TempInvoiceFlag = 'T';//TempInvoice
        public const char LedgerFlag = 'L';//TempInvoice
        public const char RepairFlag = 'R';
        public const char PurchaseOrderFlag = 'P';


        public const string SuperAdmin = "SuperAdmin";
        public const string Employee = "Employee";

        public static string CurrentDateTimesStamp = DateTime.Now.ToString("yyyyMMddhhmmss");

        public static string FullLengthDateTime = DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt");

        public static string ReGenerateBarCode = "ReGenerateBarCode";
        public static string ShowStockReport = "SStkRpt";
        /// <summary>
        /// Ledger add
        /// </summary>
        public static string LA = "LA";
        /// <summary>
        /// Ledger Delete
        /// </summary>
        public static string LD = "LD";
        /// <summary>
        /// Ledger Update
        /// </summary>
        public static string LU = "LU";

        public const string WholeSaleCash = "WholeSaleCash";
        public const string WholeSaleCredit = "WholeSaleCredit";
        public const string Retail = "Retail";


        public const string SupAccType = "SupAccType";
        public const string CustAccType = "CustAccType";

        public const char CustFlag = 'C';
        public const char SupFlag = 'S';

        public const string DefaultDiscount = "DefaultDiscount";
    }
}
