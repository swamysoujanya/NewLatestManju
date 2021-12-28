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
    public class AppConfigDB
    {
        public bool CreateConfig(AppConfigDO objAppConfigDO, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();

                string query = "Delete from appconfiguration where ConfigKey = '" + objAppConfigDO.ConfigKey + "';";

                 query += "INSERT INTO appconfiguration (SiNo, ConfigKey, ConfigValue, CreatedBy, CreatedDate) " +
                              " VALUES(NULL, '" + objAppConfigDO.ConfigKey + "', '" + objAppConfigDO.ConfigValue + "', '" + objAppConfigDO.CreatedBy + "', " + objAppConfigDO.CreatedDate + ")";

                MySqlCommand objCommand = new MySqlCommand(query, objCon);


                int i = objCommand.ExecuteNonQuery();

                if (i == 0)
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

        public string GetDeafaultDiscount(ref string errorInfo)
        {
            string defaultDisc = "0";
            try
            {                
                DataRow[] dtDiscout = MasterCache.GetAppConfigData(ref errorInfo).Select("ConfigKey = '" + AviMaConstants.DefaultDiscount + "'");
                defaultDisc = Convert.ToString(dtDiscout[0]["ConfigValue"]);

            }
            catch (Exception ex)
            {
                defaultDisc = "0";
                errorInfo = ex.Message;
            }

            return defaultDisc;
        }

        public bool UpdateCustomer(AppConfigDO objAppConfigDO, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {

                objCon.Open();

                string query = "UPDATE appconfiguration " +
                                "SET" +
                                   " ConfigValue = '" + objAppConfigDO.ConfigValue + "'," +
                                   " ModifiedBy = '" + objAppConfigDO.ModiBy + "'," +
                                   " ModifiedDate = " + objAppConfigDO.ModiDate +
                               " WHERE ConfigKey = " + objAppConfigDO.ConfigKey;

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
    }
}
