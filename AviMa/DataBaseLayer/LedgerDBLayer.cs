using AviMa.DataObjectLayer;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using AviMa.UtilityLayer;
using MySql.Data;
using System.Data;
namespace AviMa.DataBaseLayer
{
    public class LedgerDBLayer
    {
        public bool CreateLedgerItem(LedgerDO objLedgerDO, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();

                string query = "INSERT INTO ledgermaster  (SiNo, Date, AccountType, PaidTo, Amount, CreatedBy, CreatedDate, TaxPaid ,Note) " +
                   " VALUES (NULL, '" + objLedgerDO.Date + "', '" + objLedgerDO.AccountType + "', '" + objLedgerDO.PaidTo + "', " + objLedgerDO.Amount + ", " +
              "  '" + objLedgerDO.CreatedBy + "', '" + objLedgerDO.CreatedDateTime + "',  " + objLedgerDO.TaxPaid + ", '" + objLedgerDO.Note + "') ";

                MySqlCommand objCommand = new MySqlCommand(query, objCon);


                int i = objCommand.ExecuteNonQuery();

                if (i != 1)
                    _check = false;
                else
                    _check = true;

            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                _check = false;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;

        }

        public DataTable GetAllLedger(ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtledgerDetails = new DataTable();
            try
            {
                objCon.Open();

                string query = "SELECT SiNo, Date, AccountType, PaidTo, Amount,Note, CreatedBy, CreatedDate, MofidifedBy, ModifiedDate, TaxPaid  FROM ledgermaster";

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtledgerDetails);
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }


            return dtledgerDetails;

        }

        public bool DeleteLedgerItem(int SiNo, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {

                objCon.Open();
                string query = "DELETE FROM ledgermaster WHERE SiNo = " + SiNo + " ; ";
                MySqlCommand objCommand = new MySqlCommand(query, objCon);
                int i = objCommand.ExecuteNonQuery();

                if (i != 1)
                    _check = false;
                else
                    _check = true;
            }
            catch (Exception ex)
            {
                _check = false;
                errorInfo = ex.Message;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;
        }

        public bool UpdateLedgerItem(LedgerDO objLedgerDO, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {
                objCon.Open();

                string query = " UPDATE ledgermaster  SET " +
        " SiNo='" + objLedgerDO.SiNo + "', " +
        " Date= '" + objLedgerDO.Date + "'," +
        " AccountType='" + objLedgerDO.AccountType + "'," +
        " PaidTo='" + objLedgerDO.PaidTo + "'," +
        " Amount='" + objLedgerDO.Amount + "'," +
        " Note='" + objLedgerDO.Note + "'," +
        " MofidifedBy='" + objLedgerDO.ModyfiedBy + "'," +
        " ModifiedDate='" + objLedgerDO.ModyfiedDateTime + "'," +
        " TaxPaid='" + objLedgerDO.TaxPaid + "' " +
        " WHERE SiNo=" + objLedgerDO.SiNo + " ";

                MySqlCommand objCommand = new MySqlCommand(query, objCon);

                int i = objCommand.ExecuteNonQuery();

                if (i != 1)
                    _check = false;
                else
                    _check = true;
            }
            catch (Exception ex)
            {
                _check = false;
                errorInfo = ex.Message;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;
        }

        public DataTable SerahcLedger(string fromDate, string toDate, ref string errorInfo)
        {
            DataTable dtCustomersData = new DataTable();
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();

                string query = "SELECT SiNo, Date, AccountType, PaidTo, Amount,Note,CreatedBy, CreatedDate, MofidifedBy, ModifiedDate, TaxPaid  FROM ledgermaster where  ( DATE_FORMAT(CreatedDate, '%Y-%m-%d') BETWEEN  '" + fromDate + "' and   '" + toDate + "')"; ;

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtCustomersData);

            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;

                dtCustomersData = null;

            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

            return dtCustomersData;
        }

        public DataTable GetAllInvoiceAmount(int customerID, string fromDate, string toDate, char InvoiceOrRepair, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtInvoiceData = new DataTable();
            try
            {
                objCon.Open();
                string query = "";
                query = "SELECT InvGrandTotalAmount" +
                        " from invoicemaster where InvCustID =" + customerID + " or  (DATE_FORMAT(InvCreatedDate, '%Y-%m-%d') BETWEEN  '" + fromDate + "' and   '" + toDate + "' and InvOrRepair = '" + InvoiceOrRepair + "')";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtInvoiceData);

            }
            catch (Exception ex)
            {
                dtInvoiceData = null;
                errorInfo = ex.Message;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State != ConnectionState.Closed)
                    objCon.Close();
            }

            return dtInvoiceData;

        }

        public DataTable GetAllCreditInvoices(ref string errorInfo)
        {
            Char InvoiceType = AviMaConstants.InvoiceFlag;

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtInvoiceData = new DataTable();
            try
            {
                objCon.Open();

                string query = "";

                query = " select InvID, InvSaleType, InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID, " +
                   "InvCreatedBy, InvCreatedDate, InvGrandTotalAmount, InvDiscount from invoicemaster where InvOrRepair = '" + InvoiceType + "' and InvSaleType = '" + AviMaConstants.WholeSaleCredit + "'";

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtInvoiceData);

            }
            catch (Exception ex)
            {
                dtInvoiceData = null;
                errorInfo = ex.Message;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State != ConnectionState.Closed)
                    objCon.Close();

            }

            return dtInvoiceData;
        }

        public bool CreateLedgerAccType(string accType, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();

                string query = " INSERT INTO ledgeraccounttype (SiNo, AccountType) VALUES(NULL, '" + accType + "') ";
                MySqlCommand objCommand = new MySqlCommand(query, objCon);
                int i = objCommand.ExecuteNonQuery();
                if (i != 1)
                    _check = false;
                else
                    _check = true;

            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                _check = false;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;

        }

        public bool DeleteLedgerAccType(int accID, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {

                objCon.Open();
                string query = "DELETE FROM ledgeraccounttype WHERE SiNo= " + accID + "";
                MySqlCommand objCommand = new MySqlCommand(query, objCon);
                int i = objCommand.ExecuteNonQuery();

                if (i != 1)
                    _check = false;
                else
                    _check = true;
            }
            catch (Exception ex)
            {
                _check = false;
                errorInfo = ex.Message;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;
        }

        public DataTable GetAllLedgerAccType(ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtledgerDetails = new DataTable();
            try
            {
                objCon.Open();

                string query = "SELECT SiNo, AccountType	FROM ledgeraccounttype";

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtledgerDetails);
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }


            return dtledgerDetails;

        }

        public bool LedgerAccoount(AccountsDO objAccountsDO, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {
                objCon.Open();


                string AccountTable = "";
                char AccountTypeFlag = 'C';
                //   string SearchColumn = "";
                if (objAccountsDO.AccountsType == AviMaConstants.CustAccType)
                {
                    AccountTable = "custaccount";
                    //   SearchColumn = "CustID";
                    // AccountTypeFlag = 'C';
                }
                else if (objAccountsDO.AccountsType == AviMaConstants.SupAccType)
                {
                    AccountTable = "supaccount";
                    //    SearchColumn = "SupID";
                    AccountTypeFlag = 'S';
                }

                string query2 = "  UPDATE " + AccountTable + " SET " +
                               "  Credit = " + objAccountsDO.Credit + "," +
                                 "  Debit = " + objAccountsDO.Debit + "," +
                               " Balance =" + objAccountsDO.Balance + "" +
                               " WHERE SiNo = " + objAccountsDO.SiNo + "";

                MySqlCommand objCommand = new MySqlCommand(query2, objCon);

                int i = objCommand.ExecuteNonQuery();

                if (i != 1)
                    _check = false;
                else
                {
                    _check = true;

                    string query3 = "INSERT INTO creditdebithistory (SiNo, CuSuID, CustOrSupp, CreditAmnt, DebitAmnt, BalanceAmnt, date, User,AccountName) " +
                                 "VALUES (NULL, " + objAccountsDO.CustSupID + ", '" + objAccountsDO.CustOrSup + "', " + objAccountsDO.CurrentCredited + ", " + objAccountsDO.CurrentDebited + ", " +
                                 "" + objAccountsDO.Balance + ",'" + objAccountsDO.CreatedDateTime + "', '" + objAccountsDO.CreatedBy + "', '" + objAccountsDO.Name + "')";

                    objCommand = new MySqlCommand(query3, objCon);
                    i = objCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _check = false;
                errorInfo = ex.Message;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;
        }

        public bool CreateCreditHistory(AccountsDO objAccountsDO, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {
                objCon.Open();

                char AccTypeTracker = 'C';

                if (objAccountsDO.AccountsType == AviMaConstants.CustAccType)
                    AccTypeTracker = 'C';
                else if (objAccountsDO.AccountsType == AviMaConstants.CustAccType)
                    AccTypeTracker = 'S';

                string query3 = " INSERT INTO credithistory (SiNo, CuSuID, CustOrSupp, CreditAmnt, Creditdate)" +
                                   " VALUES (NULL, " + objAccountsDO.CustSupID + ", " + AccTypeTracker + ", " + objAccountsDO.CurrentCredited + "," +
                                   "'" + objAccountsDO.CreatedDateTime + "') ";

                MySqlCommand objCommand = new MySqlCommand(query3, objCon);
                int i = objCommand.ExecuteNonQuery();

                if (i != 1)
                    _check = false;
                else
                {
                    _check = true;
                }
            }
            catch (Exception ex)
            {
                _check = false;
                errorInfo = ex.Message;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;
        }

        public DataTable GetCredithistory(int CuSuID, char CustOrSupp, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtCreditHist = new DataTable();
            try
            {
                objCon.Open();
                
                string query = " SELECT SiNo, CuSuID, AccountName, CustOrSupp,DebitAmnt, CreditAmnt,BalanceAmnt, date, User " +
                               " FROM creditdebithistory where CuSuID = " + CuSuID + " and CustOrSupp = '"+ CustOrSupp + "' ";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtCreditHist);

            }
            catch (Exception ex)
            {
                dtCreditHist = null;
                errorInfo = ex.Message;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State != ConnectionState.Closed)
                    objCon.Close();
            }

            return dtCreditHist;
        }

        public DataTable GetAllAccounts(string AccountsType, ref string errorInfo)
        {

            string AccountTable = "";
            //string ColumnName = "";
            //string ColumnSupOrCust = "";

            if (AccountsType == AviMaConstants.CustAccType)
            {
                AccountTable = "custaccount";
                //ColumnName = "CustID";
                //ColumnSupOrCust
            }
            else if (AccountsType == AviMaConstants.SupAccType)
            {
                AccountTable = "supaccount";
                //ColumnName = "SupID";
                //ColumnSupOrCust
            }


            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtledgerDetails = new DataTable();
            try
            {
                objCon.Open();

                string query = "SELECT * FROM " + AccountTable + "";

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtledgerDetails);
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                dtledgerDetails = null;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Ledger DB layer line number 390");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }


            return dtledgerDetails;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Might be sup or cust id</param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public DataTable SearchAccounts(int id, string AccountsType, ref string errorInfo)
        {

            string AccountTable = "";
            //string SearchColumnName = "";
            //string ColumnSupOrCust = "";

            if (AccountsType == AviMaConstants.CustAccType)
            {
                AccountTable = "custaccount";
                //SearchColumnName = "CustID";
                //ColumnSupOrCust
            }
            else if (AccountsType == AviMaConstants.SupAccType)
            {
                AccountTable = "supaccount";
                //SearchColumnName = "SupID";
                //ColumnSupOrCust
            }


            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtAccountDetails = new DataTable();
            try
            {
                objCon.Open();

                string query = "SELECT * FROM " + AccountTable + " where ID = " + id + "";

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtAccountDetails);
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                dtAccountDetails = null;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Ledger DB layer line number 390");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }


            return dtAccountDetails;

        }

        public bool DeleteAccounts(AccountsDO objAccountsDO, string AccountsType, ref string errorInfo)
        {
            bool _check = true;
            string AccountTable = "";
            if (AccountsType == AviMaConstants.CustAccType)
            {
                AccountTable = "custaccount";
            }
            else if (AccountsType == AviMaConstants.SupAccType)
            {
                AccountTable = "supaccount";
            }


            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);

            try
            {
                objCon.Open();

                string query = "DELETE FROM " + AccountTable + " WHERE SiNo= " + objAccountsDO.SiNo + ";" +
                 "delete from creditdebithistory where CuSuID in (" + objAccountsDO.CustSupID + ");";

                MySqlCommand cmd = new MySqlCommand(query, objCon);

                int i = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                _check = false;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Ledger DB layer line number 530");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }


            return _check;

        }

    }
}
