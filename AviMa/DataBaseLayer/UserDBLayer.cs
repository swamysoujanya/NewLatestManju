using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AviMa.DataBaseLayer
{
    class UserDBLayer
    {
        public bool CreateUser(UserDo objUserDO, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);

            try
            {

                objCon.Open();

                string query = " INSERT INTO usermaster " +
       " (UserName, UserLoginID, UserPassword,Role, UserMobile1, UserMobile2,UserEmail, UserState, UserDist, UserTown, UserDescri, UserCreatedBy, UserCreatedDate) " +
       " VALUES('" + objUserDO.UserName + "', '" + objUserDO.UserLoginID + "', '" + objUserDO.UserPassword + "', '" + objUserDO.UserRole + "', '" + objUserDO.UserMobile1 + "', " +
       " '" + objUserDO.UserMobile2 + "', '" + objUserDO.UserEmail + "', '" + objUserDO.UserState + "', " +
       " '" + objUserDO.UserDist + "', '" + objUserDO.UserTown + "', '" + objUserDO.UserDescri + "', '" + objUserDO.UserCreatedBy + "', " + objUserDO.UserCreatedDate + ")";

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

        public DataTable GetAllUsers(ref string errorInfo)
        {

            return MasterCache.GetUserDataFrmCache(ref errorInfo);

            //MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            //DataTable objDbTbl = new DataTable();
            //try
            //{
            //    string query = "select UserID, UserName, UserLoginID, UserPassword, Role, UserMobile1, UserMobile2, UserEmail, UserState,"+
            //                   " UserDist, UserTown, UserDescri, UserCreatedBy, UserCreatedDate from usermaster;";
            //    MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
            //    objAdapter.Fill(objDbTbl);
            //}
            //catch (Exception ex)
            //{
            //    errorInfo = ex.Message;
            //    objDbTbl = null;
            //}
            //finally
            //{
            //    if (objCon.State == ConnectionState.Open)
            //        objCon.Close();
            //}
            //return objDbTbl;
        }

        public bool DeleteUser(string userLoginID,ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {
                objCon.Open();
                string query = " delete from usermaster where UserLoginID = '" + userLoginID + "' ; ";
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

        public bool UpdateUser(UserDo objUserDO, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {
                objCon.Open();
                string query = "UPDATE usermaster " +
                                "SET" +
                                   " UserName = '" + objUserDO.UserName + "'," +
                                    " Role = '" + objUserDO.UserRole + "'," +
                                   " UserPassword = '" + objUserDO.UserPassword + "'," +
                                   " UserMobile1 = '" + objUserDO.UserMobile1 + "'," +
                                   " UserMobile2 = '" + objUserDO.UserMobile2 + "'," +
                                   " UserEmail = '" + objUserDO.UserEmail + "'," +
                                   " UserState = '" + objUserDO.UserState + "'," +
                                   " UserDist = '" + objUserDO.UserDist + "'," +
                                   " UserTown = '" + objUserDO.UserTown + "'," +
                                   " UserDescri = '" + objUserDO.UserDescri + "'," +
                                   " UserModiBy = '" + objUserDO.UserModiBy + "'," +
                                   " UserModiDate = '" + objUserDO.UserModiDate + "' " +
                               " WHERE UserLoginID = '" + objUserDO.UserLoginID + "';";

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

        public DataTable SerahcUser(string userName, string loginId, string mobileNum, string emailId, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtUsersData = new DataTable();
            try
            {

                objCon.Open();

                bool _noSearchCriteria = false;
                string query = "select UserID, UserName, UserLoginID, UserPassword, Role, UserMobile1, UserMobile2, UserEmail,"+
                    " UserState, UserDist, UserTown, UserDescri, UserCreatedBy, UserCreatedDate from usermaster";

                int queryLen = query.Length;
                if (!string.IsNullOrEmpty(userName))
                {
                    query += " where UserName like '%" + userName + "%' ";
                    _noSearchCriteria = true;
                }
                if (!string.IsNullOrEmpty(loginId))
                {
                    query += " or UserLoginID like '%" + loginId + "%'";
                    _noSearchCriteria = true;
                }
                if (!string.IsNullOrEmpty(mobileNum))
                {
                    query += " or UserMobile1 like '%" + mobileNum + "%' ";
                    query += " or UserMobile2 like '%" + mobileNum + "%'";
                    _noSearchCriteria = true;
                }
                if (!string.IsNullOrEmpty(emailId))
                {
                    query += " or UserEmail like '%" + emailId + "%' ";
                    _noSearchCriteria = true;
                }


                if (_noSearchCriteria && query.Substring(queryLen+1, 2) == "or")
                {
                    query = query.Substring(0, queryLen) + " where " + query.Substring(queryLen+3);
                }

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtUsersData);
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                dtUsersData = null;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

            return dtUsersData;
        }

        public bool SearchForAUser(string userLoginID, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);

            try
            {

                objCon.Open();

                string query = " select UserName from usermaster where  UserLoginID = '" + userLoginID + "'";

                MySqlCommand objCommand = new MySqlCommand(query, objCon);

                MySqlDataReader objRdr = objCommand.ExecuteReader();

                while (objRdr.Read())
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

        public DataTable GetConfiguration(string userRole, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable objDbTbl = new DataTable();
            try
            {
                string query = "SELECT ConfigID, UserRole, `File`, Stock, Billing, Invoice, ShowInvoice, Report,"+
                    " Maintanance, `Repair`, ShowRepair, User, Supplier, Customer from configuration where UserRole = '" + userRole+"'";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(objDbTbl);
            }
            catch (Exception ex)
            {
                errorInfo =ex.Message;
                objDbTbl = null;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return objDbTbl;       
        }
    }
}
