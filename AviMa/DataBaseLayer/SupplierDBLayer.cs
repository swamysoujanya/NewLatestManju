using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AviMa.DataBaseLayer
{
    class SupplierDBLayer
    {       

        public bool CreateSupplier(CustomerDO objCustomerDO , ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);

            try
            {

                objCon.Open();

                string query = " INSERT INTO suppliermaster " +
       " (SupName, SupMobile1, SupMobile2, SupEmail, SupState, SupDist, SupTown, SupDescri, SupCreatedBy, SupCreatedDate) " +
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


        //Eliminated below method after creating master cache layer
        //public DataTable GetAllSuppliers( ref string errorInfo)
        //{
        //    DataTable dtCustomersData = new DataTable();
        //    try
        //    {

        //        MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);

        //        objCon.Open();

        //        string query = "SELECT SupID, SupName, SupMobile1, SupMobile2, SupEmail, SupState, SupDist, SupTown, "+
        //            "SupDescri, SupCreatedBy, SupCreatedDate, SupModiBy, SupModiDate FROM suppliermaster";

        //        MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

        //        objAdapter.Fill(dtCustomersData);

        //    }
        //    catch (Exception ex)
        //    {
        //        errorInfo = ex.Message;
        //        dtCustomersData = null;
        //    }

        //    return dtCustomersData;
        //}

        public bool DeleteSupplier(int suplierID, ref string errorInfo)
        {

            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();
                string query = " delete from suppliermaster where SupID = " + suplierID + " ; ";
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

        public bool UpdateSupplier(CustomerDO objCustomerDO, ref string errorInfo)
        {

            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();
                string query = "UPDATE suppliermaster " +
                                "SET" +
                                   " SupName = '" + objCustomerDO.CustName + "'," +
                                   " SupMobile1 = '" + objCustomerDO.CustMobile1 + "'," +
                                   " SupMobile2 = '" + objCustomerDO.CustMobile2 + "'," +
                                   " SupEmail = '" + objCustomerDO.CustEmail + "'," +
                                   " SupState = '" + objCustomerDO.CustState + "'," +
                                   " SupDist = '" + objCustomerDO.CustDist + "'," +
                                   " SupTown = '" + objCustomerDO.CustTown + "'," +
                                   " SupDescri = '" + objCustomerDO.CustDescri + "'," +
                                   " SupModiBy = '" + objCustomerDO.CustModiBy + "'," +
                                   " SupModiDate = " + objCustomerDO.CustModiDate +
                               " WHERE SupID = " + objCustomerDO.CustID;

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

        public DataTable SerahcSupplier(string suprName, string mobileNum, string emailId, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtCustomersData = new DataTable();
            try
            {            

                objCon.Open();

                //string query = " select * from suppliermaster where SupName like '%" + suprName + "%' or SupMobile1 like '%" + mobileNum + "%' " +
                //                    "or SupMobile2 like '%" + mobileNum + "%' or SupEmail like '%" + emailId + "%' ";


                bool _noSearchCriteria = false;
                string query = "SELECT SupID, SupName, SupMobile1, SupMobile2, SupEmail, SupState, SupDist, SupTown,"+
                    " SupDescri, SupCreatedBy, SupCreatedDate, SupModiBy, SupModiDate FROM suppliermaster";

                int queryLen = query.Length;
                if (!string.IsNullOrEmpty(suprName))
                {
                    query += " where SupName like '%" + suprName + "%' ";
                    _noSearchCriteria = true;
                }
               
                if (!string.IsNullOrEmpty(mobileNum))
                {
                    query += " or SupMobile1 like '%" + mobileNum + "%' ";
                    query += " or SupMobile2 like '%" + mobileNum + "%'";
                    _noSearchCriteria = true;
                }
                if (!string.IsNullOrEmpty(emailId))
                {
                    query += " or SupEmail like '%" + emailId + "%' ";
                    _noSearchCriteria = true;
                }


                if (_noSearchCriteria && query.Substring(queryLen+1, 2) == "or")
                {
                    query = query.Substring(0, queryLen) + " where " + query.Substring(queryLen+3);
                }


                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtCustomersData);
            }
            catch (Exception ex)
            {
                dtCustomersData = null;
                errorInfo = ex.Message;
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }



            return dtCustomersData;

        }      

    }
}
