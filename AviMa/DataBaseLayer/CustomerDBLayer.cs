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
    class CustomerDBLayer
    {
        public bool CreateCustomer(CustomerDO objCustomerDO, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {             

                objCon.Open();

                string query = " INSERT INTO customermaster " +
       " (CustName, CustMobile1, CustMobile2, CustEmail, CustState, CustDist, CustTown, CustDescri, CustCreatedBy, CustCreatedDate) " +
       " VALUES('" + objCustomerDO.CustName + "', '" + objCustomerDO.CustMobile1 + "', '" + objCustomerDO.CustMobile2 + "', '" + objCustomerDO.CustEmail + "', " +
       " '" + objCustomerDO.CustState + "', '" + objCustomerDO.CustDist + "', '" + objCustomerDO.CustTown + "', " +
       " '" + objCustomerDO.CustDescri + "', '" + objCustomerDO.CustCreatedBy + "', " + objCustomerDO.CustCreatedDate + ")";

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
                if(objCon.State == ConnectionState.Open)
                objCon.Close();
            }
            return _check;

        }

        public DataTable GetAllCustomers(ref string errorInfo)
        {

            return MasterCache.GetCustomerDataFrmCache(ref errorInfo);

            //MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            //DataTable dtCustomersData = new DataTable();
            //try
            //{
            //    objCon.Open();

            //    string query = " select * from customermaster";

            //    MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

            //    objAdapter.Fill(dtCustomersData);
            //}
            //catch (Exception ex)
            //{
            //    errorInfo = ex.Message;
            //}
            //finally
            //{
            //    if (objCon.State == ConnectionState.Open)
            //        objCon.Close();
            //}


            //return dtCustomersData;

        }

        public bool DeleteCustomer(int custID, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {

                objCon.Open();
                string query = " delete from customermaster where custid = " + custID + " ; ";
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

        public bool UpdateCustomer(CustomerDO objCustomerDO, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {
                objCon.Open();

                string query = "UPDATE customermaster " +
                                "SET" +
                                   " CustName = '" + objCustomerDO.CustName + "'," +
                                   " CustMobile1 = '" + objCustomerDO.CustMobile1 + "'," +
                                   " CustMobile2 = '" + objCustomerDO.CustMobile2 + "'," +
                                   " CustEmail = '" + objCustomerDO.CustEmail + "'," +
                                   " CustState = '" + objCustomerDO.CustState + "'," +
                                   " CustDist = '" + objCustomerDO.CustDist + "'," +
                                   " CustTown = '" + objCustomerDO.CustTown + "'," +
                                   " CustDescri = '" + objCustomerDO.CustDescri + "'," +
                                   " CustModiBy = '" + objCustomerDO.CustModiBy + "'," +
                                   " CustModiDate = " + objCustomerDO.CustModiDate +
                               " WHERE custid = " + objCustomerDO.CustID;

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

        public DataTable SerahcCustomer(string CustName, string mobileNum, string emailId, ref string errorInfo)
        {
            DataTable dtCustomersData = new DataTable();
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try { 
            
                objCon.Open();
                //string query = " select * from customermaster where CustName like '%" + CustName + "%' or Custmobile1 like '%" + mobileNum + "%' " +
                //    "or Custmobile2 like '%" + mobileNum + "%' or CustEmail like '%" + emailId + "%' ";

                bool _noSearchCriteria = false;
                string query = "select * from customermaster";
                int queryLen = query.Length;
                if (!string.IsNullOrEmpty(CustName))
                {
                    query += " where CustName like '%" + CustName + "%' ";
                    _noSearchCriteria = true;
                }

                if (!string.IsNullOrEmpty(mobileNum))
                {
                    query += " or Custmobile1 like '%" + mobileNum + "%' ";
                    query += " or Custmobile2 like '%" + mobileNum + "%'";
                    _noSearchCriteria = true;
                }
                if (!string.IsNullOrEmpty(emailId))
                {
                    query += " or CustEmail like '%" + emailId + "%' ";
                    _noSearchCriteria = true;
                }

                if (_noSearchCriteria && query.Substring(queryLen + 1, 2) == "or")
                {
                    query = query.Substring(0, queryLen) + " where " + query.Substring(queryLen + 3);
                }

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

        public CustomerDO GetCustomer(int custID ,ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            CustomerDO objCustomerDO = null;
            try
            {
                objCon.Open();

                string query = "SELECT CustName, CustMobile1, CustMobile2, CustEmail, CustState, CustDist, CustTown "+
                      " FROM customermaster where CustID = "+ custID ;

                MySqlCommand objCmd = new MySqlCommand(query, objCon);
                MySqlDataReader objReader = objCmd.ExecuteReader();

                while(objReader.Read())
                {
                    objCustomerDO = new CustomerDO();

                    objCustomerDO.CustName = Convert.ToString(objReader["CustName"]);
                    objCustomerDO.CustMobile1 = Convert.ToString(objReader["CustMobile1"]);
                    objCustomerDO.CustMobile2 = Convert.ToString(objReader["CustMobile2"]);
                    objCustomerDO.CustEmail = Convert.ToString(objReader["CustEmail"]);
                    objCustomerDO.CustState = Convert.ToString(objReader["CustState"]);
                    objCustomerDO.CustDist = Convert.ToString(objReader["CustDist"]);
                    objCustomerDO.CustTown = Convert.ToString(objReader["CustTown"]);
                }
             
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                objCustomerDO = null;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return objCustomerDO;
        }
    }
}
