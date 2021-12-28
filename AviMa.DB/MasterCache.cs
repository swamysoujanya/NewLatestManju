using AviMa.UtilityLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

using System.Text;

namespace AviMa.DataBaseLayer
{
    public static class MasterCache
    {
        //static DataSet dsMasterData = null;
        static DataTable dtCustomersData = new DataTable("customermaster");
        static DataTable dtSupplierData = new DataTable("suppliermaster");
        static DataTable dtUserData = new DataTable("usermaster");
        static DataTable dtStockDataForInv = new DataTable("stockMasterForInv");
        static DataTable dtAvailableBarCode = new DataTable("barCodes");
        static DataTable dtAppConfigData = new DataTable("AppConfigData");

        public static bool SetCache(ref string errorInfo)
        {
            bool _check = true;

            try
            {
                _check = SetSupplier(ref errorInfo);
                _check = SetUserData(ref  errorInfo);
                _check = SetCustomerData(ref  errorInfo);
                _check = SetStockForInvoice(ref errorInfo);
                _check = SetBarCodes(ref errorInfo);
                _check = SetAppConfigData(ref errorInfo);
            }
            catch (Exception ex)
            {
                _check = false;
                errorInfo = ex.Message;
            }

            return _check;
        }

        public static bool SetSupplier(ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = true;
            try
            {
                string query = "";
                query = "SELECT SupID, SupName, SupMobile1, SupMobile2, SupEmail, SupState, SupDist, SupTown, SupDescri," +
                       "  SupCreatedBy, SupCreatedDate, SupModiBy, SupModiDate FROM suppliermaster";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                dtSupplierData = new DataTable();
                objAdapter.Fill(dtSupplierData);
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
        public static bool SetUserData(ref string errorInfo)
        {
            bool _check = true;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {
                string query = "";
                query = " SELECT UserID, UserName, UserLoginID, UserPassword, Role, UserMobile1, UserMobile2, UserEmail, UserState, UserDist, UserTown, UserDescri," +
                       "   UserCreatedBy, UserCreatedDate, UserModiBy, UserModiDate FROM usermaster";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                dtUserData = new DataTable();
                objAdapter.Fill(dtUserData);
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
        public static bool SetCustomerData(ref string errorInfo)
        {

            bool _check = true;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {
                objCon.Open();
                string query = "SELECT CustID, CustName, CustMobile1, CustMobile2, CustEmail, CustState, CustDist, CustTown," +
                        " CustDescri, CustCreatedBy, CustCreatedDate, CustModiBy, CustModiDate FROM customermaster order by CustName asc ";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                dtCustomersData = new DataTable();
                objAdapter.Fill(dtCustomersData);
                // dsMasterData.Tables.Add(dtCustomersData);

                objCon.Close();
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
        public static bool SetStockForInvoice(ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = true;
            try
            {
                string query = "";
                query = "SELECT IID, IBarCode, IName, IQnty, IPurchasePrice, IWCashPrice, IWCreditPrice, IWRetailPrice FROM itemsmaster";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                dtStockDataForInv = new DataTable();
                objAdapter.Fill(dtStockDataForInv);
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
        public static bool SetBarCodes(ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = true;
            try
            {
                string query = "";
                query = "select * from barcodemaster order by BarCode asc;";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                dtAvailableBarCode = new DataTable();
                objAdapter.Fill(dtAvailableBarCode);
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
        public static bool SetAppConfigData(ref string errorInfo)
        {

            bool _check = true;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {
                objCon.Open();
                string query = "        SELECT SiNo, ConfigKey, ConfigValue, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate FROM appconfiguration";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                dtAppConfigData = new DataTable();
                objAdapter.Fill(dtAppConfigData);
                // dsMasterData.Tables.Add(dtCustomersData);

                objCon.Close();
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

        public static DataTable GetSupplierFrmCache(ref string errorInfo)
        {
            return dtSupplierData;
        }
        public static DataTable GetUserDataFrmCache(ref string errorInfo)
        {
            return dtUserData;
        }
        public static DataTable GetCustomerDataFrmCache(ref string errorInfo)
        {
            return dtCustomersData;
        }
        public static DataTable GetStockDataForInvFrmCache(ref string errorInfo)
        {
            return dtStockDataForInv;
        }
        public static DataTable GetAvialableBarCodes(ref string errorInfo)
        {
            return dtAvailableBarCode;
        }
        public static DataTable GetAppConfigData(ref string errorInfo)
        {
            return dtAppConfigData;
        }





    }
}
