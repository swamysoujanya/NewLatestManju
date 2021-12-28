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
    public class LedgerRptDBLayer
    {
        public bool CreateLedgerRprtItem(LedgerRptDO objLedgerRptDO, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();

                string query = "INSERT INTO dailyledgerreport  (SiNo, OpeningBalance, LedgerDate, CreatedBy,CreatedDate,  `Column 6`, `Column 7`, `Column 8`, `Column 9`, `Column 10`, `Column 11`, `Column 12`) " +
                   " VALUES (NULL, " + objLedgerRptDO.OpeningBalance + ", '" + objLedgerRptDO.LedgerDate + "', '" + objLedgerRptDO.CreatedBy + "', '" + objLedgerRptDO.CreatedDate + "', " +
              "  '" + objLedgerRptDO.Column6 + "', '" + objLedgerRptDO.Column7 + "',  '" + objLedgerRptDO.Column8 + "', '" + objLedgerRptDO.Column9 + "', '" + objLedgerRptDO.Column9 + "', '" + objLedgerRptDO.Column9 + "', '" + objLedgerRptDO.Column9 + "') ";

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

        public DataTable GetAllLedgerRprtItem(ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtledgerRprtDtls = new DataTable();
            try
            {
                objCon.Open();

                string query = "SELECT SiNo, OpeningBalance, Date, CreatedBy, CreatedDate, `Column 6`, `Column 7`, `Column 8`, `Column 9`, "+
                    " `Column 10`, `Column 11`, `Column 12` FROM dailyledgerreport";

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtledgerRprtDtls);
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

            return dtledgerRprtDtls;
        }

        public bool UpdateLedgerRprtItem(LedgerRptDO objLedgerRptDO, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {
                objCon.Open();

                string query = " UPDATE dailyledgerreport  SET " +
        //" SiNo='" + objLedgerRptDO.SiNo + "', " +
        " OpeningBalance= '" + objLedgerRptDO.OpeningBalance + "'," +
         " ClosingBalance= '" + objLedgerRptDO.ClosingBalance + "'" +
        //" Date='" + objLedgerRptDO.LedgerDate + "'," +
        //" CreatedBy='" + objLedgerRptDO.CreatedBy + "'," +
        //" CreatedDate='" + objLedgerRptDO.CreatedDate + "'," +        
        " WHERE SiNo=" + objLedgerRptDO.SiNo + " ";

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

        //public bool UpdateOpeningBalance(LedgerRptDO objLedgerRptDO, ref string errorInfo)
        //{

        //    MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
        //    bool _check = false;
        //    try
        //    {
        //        objCon.Open();

        //        string query = " UPDATE dailyledgerreport  SET " +
        // //" SiNo='" + objLedgerRptDO.SiNo + "', " +
        // //" OpeningBalance= '" + objLedgerRptDO.OpeningBalance + "'," +
        // " OpeningBalance= '" + objLedgerRptDO.OpeningBalance + "'" +
        ////" Date='" + objLedgerRptDO.LedgerDate + "'," +
        ////" CreatedBy='" + objLedgerRptDO.CreatedBy + "'," +
        ////" CreatedDate='" + objLedgerRptDO.CreatedDate + "'," +        
        //" WHERE SiNo=" + objLedgerRptDO.SiNo + " ";

        //        MySqlCommand objCommand = new MySqlCommand(query, objCon);

        //        int i = objCommand.ExecuteNonQuery();

        //        if (i != 1)
        //            _check = false;
        //        else
        //            _check = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _check = false;
        //        errorInfo = ex.Message;
        //    }
        //    finally
        //    {
        //        if (objCon.State == ConnectionState.Open)
        //            objCon.Close();
        //    }
        //    return _check;
        //}

        //   DELETE FROM dailyledgerreport WHERE SiNo=NULLavimadbretailinvoicemaster
    }
}
