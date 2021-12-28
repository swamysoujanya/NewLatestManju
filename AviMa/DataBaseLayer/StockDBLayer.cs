using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AviMa.DataBaseLayer
{
    class StockDBLayer
    {
        public bool CreateItem(PODetailsDO objPODetailsDO, bool POAlreadyExists, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            bool _check = false;
            try
            {
                MySqlTransaction objTran = null;
                MySqlCommand objCommand = null;
                try
                {
                    objCon.Open();
                    objTran = objCon.BeginTransaction();
                    string query = "";

                    Int64 i = 0;// objCommand.ExecuteNonQuery();
                    if (objPODetailsDO.IsAddWithPO)
                    {
                        if (!POAlreadyExists)
                        {
                            query = "INSERT INTO pomaster " +
                                 " (posupplierID, podate,poTotalAmount, poRefernece, poCreatedDate, poCreatedBy)" +
                                 " VALUES (" + objPODetailsDO.POSupplierID + ", '" + objPODetailsDO.PODate + "', '" + objPODetailsDO.poTotalAmount + "'," +
                                 "'" + objPODetailsDO.POReference + "', '" + objPODetailsDO.POCreatedDateTime + "','" + objPODetailsDO.POCreatedBy + "'); ";

                            query += "SELECT LAST_INSERT_ID(); ";
                            // query += " SELECT CAST(scope_identity() AS int);";

                            objCommand = new MySqlCommand(query, objCon);
                            var ii = objCommand.ExecuteScalar();
                            i = Convert.ToInt64(ii);
                        }
                        else
                        {
                            i = objPODetailsDO.POID; // Re assign existing PO number to the new items created
                        }
                    }
                    else
                    {
                        var ii = "99999"; // Fixed number for stock with out PO
                        i = Convert.ToInt64(ii);
                    }

                    if (i == 0)
                    {
                        _check = false;
                        objTran.Rollback();
                    }
                    else
                    {

                        int itemsCreatedCount = 0;
                        int poItemsCreatedCount = 0;

                        string _collectBarCodes = "";


                        foreach (ItemsDO objItemDO in objPODetailsDO.POItemsList)
                        {
                            query = "";


                            DataTable dtStockDetails = new DataTable();
                            dtStockDetails = SerahcStocks(objItemDO.Name, objItemDO.BarCode);

                            _collectBarCodes += objItemDO.BarCode + ",";


                            if (dtStockDetails != null && dtStockDetails.Rows.Count > 0)
                            {
                                foreach (DataRow dtRow in dtStockDetails.Rows)
                                {

                                    Int32 avlQnty = Convert.ToInt16(dtRow["IQnty"]) + Convert.ToInt16(objItemDO.Qnty);

                                    query = "";
                                    query = "UPDATE itemsmaster SET" +
                              " IQnty = '" + avlQnty + "'," +
                              " IModifiedDate = '" + AviMaConstants.CurrentDateTimesStamp + "'," +
                              " IModifiedBy = '" + objPODetailsDO.POCreatedBy + "'" +
                              " WHERE IID = " + Convert.ToInt16(dtRow["IID"]);

                                    objCommand = new MySqlCommand(query, objCon);

                                    int updQnty = objCommand.ExecuteNonQuery();

                                }
                            }
                            else   //  " IPriceCode = '" + objItemDO.PriceCode + "'," +
                            {
                                query = " INSERT INTO itemsmaster " +
                                      " (IBarCode, IName, IDescription, IQnty,IPONumber,IPurchasePrice, ISupID, IWCashPrice, IWCreditPrice, IWRetailPrice,IPriceCode, ICreatedDateTime, ICreatedBy) " +
                                      " VALUES('" + objItemDO.BarCode + "', '" + objItemDO.Name + "', '" + objItemDO.Description + "', '" + objItemDO.Qnty + "', " +
                                      "  " + Convert.ToUInt64(i) + ", '" + objItemDO.IPurchasePrice + "','" + objItemDO.SupID + "', '" + objItemDO.WCashPrice + "', '" + objItemDO.WCreditPrice + "', " +
                                      " '" + objItemDO.WRetailPrice + "', '" + objItemDO.PriceCode + "', '" + objItemDO.CreatedDateTime + "', '" + objItemDO.CreatedBy + "')";

                                objCommand = new MySqlCommand(query, objCon);
                                itemsCreatedCount += objCommand.ExecuteNonQuery();
                            }

                            query = "";
                            query = " INSERT INTO poitemsmaster " +
                                " (IBarCode, IName, IDescription, IQnty,IPONumber,IPurchasePrice, ISupID, IWCashPrice, IWCreditPrice, IWRetailPrice,IPriceCode, ICreatedDateTime, ICreatedBy) " +
                                " VALUES('" + objItemDO.BarCode + "', '" + objItemDO.Name + "', '" + objItemDO.Description + "', '" + objItemDO.Qnty + "', " +
                                "  " + Convert.ToUInt64(i) + ", '" + objItemDO.IPurchasePrice + "','" + objItemDO.SupID + "', '" + objItemDO.WCashPrice + "', '" + objItemDO.WCreditPrice + "', " +
                                " '" + objItemDO.WRetailPrice + "', '" + objItemDO.PriceCode + "', '" + objItemDO.CreatedDateTime + "', '" + objItemDO.CreatedBy + "')";

                            objCommand = new MySqlCommand(query, objCon);
                            poItemsCreatedCount += objCommand.ExecuteNonQuery();

                        }


                        #region Delete UIsed Barcodes from barcode master table

                        try
                        {
                            query = "";
                            _collectBarCodes = _collectBarCodes.Substring(0, _collectBarCodes.Length - 1);
                            query = "Delete from barcodemaster where  BarCode in(" + _collectBarCodes + ")";
                            objCommand = new MySqlCommand(query, objCon);
                            int barcodesDeleted = objCommand.ExecuteNonQuery();

                        }
                        catch (Exception)
                        {

                        }


                        MasterCache.SetBarCodes(ref errorInfo);


                        #endregion Delete UIsed Barcodes from barcode master table

                        if (itemsCreatedCount == objPODetailsDO.POItemsList.Count)
                        {
                            objTran.Commit();
                            MasterCache.SetBarCodes(ref errorInfo);
                            _check = true;
                        }
                        else
                        {
                            _check = false;
                            objTran.Rollback();
                        }
                    }
                }
                catch (Exception ex)
                {
                    objTran.Rollback();
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

        public DataTable SerahcStocks(string stcokNames, string barCodes)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtCustomersData = new DataTable();
            try
            {

                objCon.Open();
                string query = "SELECT IID, IBarCode, IName, IQnty  FROM itemsmaster where IName = ('" + stcokNames + "') and IBarCode = ('" + barCodes + "')";

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtCustomersData);
            }
            catch (Exception ex)
            {
                dtCustomersData = null;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return dtCustomersData;

        }

        public DataTable GetAllItems(ref string errorInfo, string reportType)
        {
            DataTable dtStkDetails = new DataTable();
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();
                string query = "";

                if (reportType == AviMaConstants.ShowStockReport)
                {
                    query = "SELECT IID, IBarCode, IName, IDescription, IQnty, IPurchasePrice, IPONumber, ISupID," +
                       " IWCashPrice, IWCreditPrice, IWRetailPrice,IPriceCode, ICreatedDateTime, ICreatedBy, IModifiedDate, IModifiedBy " +
                       " FROM itemsmaster";
                }
                else if (reportType == AviMaConstants.ReGenerateBarCode)
                {
                    query = "SELECT IID, IBarCode,IPriceCode, IName,IQnty, ISupID FROM itemsmaster";
                }

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtStkDetails);

                if (reportType == AviMaConstants.ReGenerateBarCode)
                {
                    if (dtStkDetails != null && dtStkDetails.Rows.Count > 0)
                    {
                        DataColumn clm = new DataColumn("BarCodeQnty");
                        dtStkDetails.Columns.Add(clm);

                    }
                }
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                dtStkDetails = null;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return dtStkDetails;
        }

        public bool DeleteItems(string itemBarCode, ref string errorInfo)
        {

            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {
                objCon.Open();
                string query = " delete from itemsmaster where IBarCode = '" + itemBarCode + "' ; ";
                MySqlCommand objCommand = new MySqlCommand(query, objCon);
                int i = objCommand.ExecuteNonQuery();
                if (i != 1)
                {
                    _check = false;
                }
                else
                {
                    _check = true;

                    #region insert deleted bar code in to the bar code master table

                    try
                    {
                        query = "";
                        query = " INSERT INTO barcodemaster	(ID, BarCode)	VALUES (NULL, '" + itemBarCode + "'); ";
                        objCommand = new MySqlCommand(query, objCon);
                        int barcodesDeleted = objCommand.ExecuteNonQuery();
                        MasterCache.SetBarCodes(ref errorInfo);

                    }
                    catch (Exception)
                    {

                    }
                    #endregion insert deleted bar code in to the bar code master table
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

        public bool UpdateItems(ItemsDO objItemDO, ref string errorInfo)
        {

            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);

            try
            {

                objCon.Open();

                string query = "UPDATE itemsmaster " +
                                "SET" +
                                   " IName = '" + objItemDO.Name + "'," +
                                   " IDescription = '" + objItemDO.Description + "'," +
                                   " IQnty = '" + objItemDO.Qnty + "'," +
                                   " IWCashPrice = '" + objItemDO.WCashPrice + "'," +
                                   " IPurchasePrice = '" + objItemDO.IPurchasePrice + "'," +
                                   " IWCreditPrice = '" + objItemDO.WCreditPrice + "'," +
                                   " IWRetailPrice = '" + objItemDO.WRetailPrice + "'," +
                                   " IModifiedDate = '" + objItemDO.ModifiedDate + "'," +
                                      " IPriceCode = '" + objItemDO.PriceCode + "'," +
                                   " IModifiedBy = '" + objItemDO.ModifiedBy + "'" +
                               " WHERE IBarCode = '" + objItemDO.BarCode + "'";

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

        //public int GetASupplier(string supplierName)
        //{

        //    int suplID = 0;

        //    try
        //    {

        //        MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);

        //        objCon.Open();

        //        string query = " select SupID from suppliermaster where SupName = '" + supplierName + "' ; ";

        //        MySqlCommand objCommand = new MySqlCommand(query, objCon);

        //        MySqlDataReader objrdr = objCommand.ExecuteReader();

        //        while (objrdr.Read())
        //        {
        //            suplID = Convert.ToInt16(objrdr["SupID"]);


        //        }

        //        //  int i = objCommand.ExecuteNonQuery();

        //        //if (i != 1)
        //        //    _check = false;
        //        //else
        //        //    _check = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        suplID = 0;               
        //    }

        //    return suplID;
        //}

        public string GetASupplier(Int64 supplierID, ref string errorInfo)
        {

            string supName = "";
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();
                string query = " select SupName from suppliermaster where SupID = '" + supplierID + "' ; ";
                MySqlCommand objCommand = new MySqlCommand(query, objCon);
                MySqlDataReader objrdr = objCommand.ExecuteReader();
                while (objrdr.Read())
                {
                    supName = Convert.ToString(objrdr["SupName"]);
                }

                //  int i = objCommand.ExecuteNonQuery();

                //if (i != 1)
                //    _check = false;
                //else
                //    _check = true;
            }
            catch (Exception ex)
            {
                supName = "";
                errorInfo = ex.Message;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

            return supName;
        }

        public DataTable SerahcStock(string stcokName, string barCode, int suplierID, ref string errorInfo, string reportType)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtStkDetails = new DataTable();
            try
            {

                objCon.Open();

                bool _noSearchCriteria = false;
                string query = "";

                if (reportType == AviMaConstants.ShowStockReport)
                {
                    query = "SELECT IID, IBarCode, IName, IDescription, IQnty, IPurchasePrice, IPONumber, ISupID," +
                       " IWCashPrice, IWCreditPrice, IWRetailPrice,IPriceCode, ICreatedDateTime, ICreatedBy, IModifiedDate, IModifiedBy " +
                       " FROM itemsmaster";
                }
                else if (reportType == AviMaConstants.ReGenerateBarCode)
                {
                    query = "SELECT IID, IBarCode,IPriceCode, IName,IQnty, ISupID FROM itemsmaster";
                }



                int queryLen = query.Length;
                if (!string.IsNullOrEmpty(stcokName))
                {
                    query += " where IName like '%" + stcokName + "%' ";
                    _noSearchCriteria = true;
                }
                if (!string.IsNullOrEmpty(barCode))
                {
                    query += " or IBarCode like '%" + barCode + "%'";
                    _noSearchCriteria = true;
                }
                if (suplierID != 0)
                {
                    query += " or ISupID like '" + suplierID + "' ";
                    _noSearchCriteria = true;
                }

                if (_noSearchCriteria && query.Substring(queryLen + 1, 2) == "or")
                {
                    query = query.Substring(0, queryLen) + " where " + query.Substring(queryLen + 3);
                }

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtStkDetails);
            }
            catch (Exception ex)
            {
                dtStkDetails = null;
                errorInfo = ex.Message;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

            if (reportType == AviMaConstants.ReGenerateBarCode)
            {
                if (dtStkDetails != null && dtStkDetails.Rows.Count > 0)
                {
                    DataColumn clm = new DataColumn("BarCodeQnty");
                    dtStkDetails.Columns.Add(clm);

                }
            }
            return dtStkDetails;

        }

        public bool GetItemWRTBarCode(string barCode, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtCustomersData = new DataTable();
            bool _check = false;
            try
            {

                objCon.Open();
                string query = "select * from itemsmaster where IBarCode = '" + barCode + "'";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtCustomersData);

                if (dtCustomersData != null && dtCustomersData.Rows.Count > 0)
                {
                    _check = true;
                }

            }
            catch (Exception ex)
            {
                // dtCustomersData = null;
                errorInfo = ex.Message;
                _check = false;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            // return dtCustomersData;
            return _check;

        }

        public DataTable GetPOs(ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtPODetails = new DataTable();
            try
            {

                objCon.Open();
                string query = " SELECT ponumber, posupplierID, poTotalAmount, podate, poRefernece, poCreatedDate, poCreatedBy"
                                + "	FROM pomaster";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtPODetails);
            }
            catch (Exception ex)
            {
                dtPODetails = null;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return dtPODetails;

        }

        internal bool UpdateQnty(ItemsDO objItemsDO, ref string errorInfo)
        {

            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();
                string query = "UPDATE itemsmaster " +
                    "SET" +
                       " IQnty = '" + objItemsDO.Qnty + "'," +
                       " IModifiedDate = '" + objItemsDO.ModifiedDate + "'," +
                       " IModifiedBy = '" + objItemsDO.ModifiedBy + "'" +
                       " WHERE IID = " + Convert.ToInt16(objItemsDO.ItemID);
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

        public DataTable SearchPO(int suplierID, string fromDate, string toDate, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtPODetails = new DataTable();
            try
            {
                objCon.Open();
                string query = "select ponumber, posupplierID, poTotalAmount, podate, poRefernece, poCreatedDate, poCreatedBy " +
                    " from pomaster where posupplierID =" + suplierID + " or  ( DATE_FORMAT(podate, '%Y-%m-%d') BETWEEN  '" + fromDate + "' and   '" + toDate + "')";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtPODetails);
            }
            catch (Exception ex)
            {
                dtPODetails = null;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return dtPODetails;
        }

        public bool DeletePO(string poNumbers, ref string errorInfo)
        {

            bool _check = true;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();
                string query = " delete from pomaster where ponumber in (" + poNumbers + ") ; delete from poitemsmaster where IPONumber in (" + poNumbers + ") ;  ";
                MySqlCommand objCommand = new MySqlCommand(query, objCon);
                int i = objCommand.ExecuteNonQuery();
                //if (i != 1)
                //    _check = false;
                //else
                //    _check = true;
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

        internal DataTable GetStockItemDetails(string poNumber, ref string errorInfo)
        {
            DataTable dtCustomersData = new DataTable();
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);

            try
            {
                objCon.Open();

                string query = "SELECT IID, IBarCode, IName, IDescription, IQnty, IPurchasePrice, IPONumber, ISupID, IWCashPrice, IWCreditPrice, IWRetailPrice, ICreatedDateTime, ICreatedBy " +
                                "FROM poitemsmaster where IPONumber =" + poNumber;

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
    }
}
