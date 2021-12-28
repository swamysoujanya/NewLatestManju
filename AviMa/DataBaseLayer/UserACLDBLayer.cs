using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AviMa.DataBaseLayer
{
    class UserACLDBLayer
    {


        public Dictionary<int, String> GetAllFeatures(ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable objDtFeatures = new DataTable();
            Dictionary<int, String> objFeatureCollection = new Dictionary<int, string>();
            try
            {
                string query = "Select * from features";

                MySqlDataAdapter objDap = new MySqlDataAdapter(query, objCon);
                objDap.Fill(objDtFeatures);

                if (objDtFeatures != null && objDtFeatures.Rows.Count > 0)
                {
                    foreach (DataRow item in objDtFeatures.Rows)
                    {
                        objFeatureCollection.Add(Convert.ToInt16(item["featureID"]), Convert.ToString(item["featureName"]));
                    }
                }
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                objDtFeatures = null;
            }
            finally
            {
            }
            return objFeatureCollection;
        }

        public List<int> GetFeatureUserMap(int userID, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable objFeatureUserMap = new DataTable();
            List<int> enabledFeatures = new List<int>();
            try
            {
                objCon.Open();

                string query = "Select * from userfeaturemapping where userID =" + userID;

                MySqlDataAdapter objDap = new MySqlDataAdapter(query, objCon);
                objDap.Fill(objFeatureUserMap);

                foreach (DataRow item in objFeatureUserMap.Rows)
                {
                    enabledFeatures.Add(Convert.ToInt16(item["featureID"]));
                }

            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                enabledFeatures = null;
            }
            finally
            {

            }
            return enabledFeatures;
        }

        public bool MapUsersAndFeatures(UserACLDO objUserACLDO, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = true;
            try
            {
                objCon.Open();

                string query = "";
                query = "Delete from userfeaturemapping where userID =" + objUserACLDO.UserID + ";";
                string featireIDBulkInsert = "";
                foreach (int item in objUserACLDO.EnabledFeat)
                {
                    featireIDBulkInsert += "(" + objUserACLDO.UserID + "," + item + "),";
                }
                featireIDBulkInsert = featireIDBulkInsert.Substring(0, featireIDBulkInsert.Length - 1);

                query += "insert into userfeaturemapping (userID,featureID) values " + featireIDBulkInsert + ";";

                MySqlCommand myCmnd = new MySqlCommand(query, objCon);

                int i = myCmnd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                _check = false;
            }
            finally
            {
                objCon.Close();
            }
            return _check;
        }

    }
}
