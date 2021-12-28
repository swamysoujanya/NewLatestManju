using System;
using System.Collections.Generic;
using System.Text;

namespace AviMa.DataBaseLayer
{
    public class DummyDBLayer
    {


        #region public string GetASupplier(Int64 supplierID)
        //public string GetASupplier(Int64 supplierID)
        //{

        //    string supName = "";

        //    try
        //    {

        //        MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);

        //        objCon.Open();

        //        string query = " select SupName from suppliermaster where SupID = '" + supplierID + "' ; ";

        //        MySqlCommand objCommand = new MySqlCommand(query, objCon);

        //        MySqlDataReader objrdr = objCommand.ExecuteReader();

        //        while (objrdr.Read())
        //        {
        //            supName = Convert.ToString(objrdr["SupName"]);

        //        }

        //        //  int i = objCommand.ExecuteNonQuery();

        //        //if (i != 1)
        //        //    _check = false;
        //        //else
        //        //    _check = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        supName = "";
        //    }

        //    return supName;
        //}


        #endregion public string GetASupplier(Int64 supplierID)



    }
}
