using AviMa.DataObjectLayer;
using AviMa.UtilityLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AviMa.DataBaseLayer
{
    public class InvoiceDBLayer
    {
        // invoicemasterINSERT INTO invoicemaster
        //(InvID, InvSaleType, InvTotalItems, InvTotalAmount, InvCustID, InvCreatedBy, InvCreatedDate, InvModifiedBy, InvModifiedDate)
        //VALUES ('', '', '', '', 0, '', NOW(), '', NOW())

        //    SELECT invoiceitemsINSERT INTO invoiceitems
        //(InvID, InvBarCode, InvName, InvDescription, InvQnty, InvRate, InvTotalAmont, InvSupID, InvDateTime, InvCreatedBy, InvModifiedDate, InvModifiedBy)
        //VALUES ('', '', '', '', 0, 0, 0, 0, NOW(), '', NOW(), '')
        //FROM invoicemaster



        public bool CreateInvoice(InvoiceDO objInvoiceDO, bool isTempInvoice, bool bFoundCustomer, ref string errorIfo)
        {
            string GrandTotalAmount = "0";
            string TotalAmount = "0";

            if (ConfigValueLoader.CalculateItemPrice)
            {
                TotalAmount = Convert.ToString(Convert.ToDecimal(objInvoiceDO.TotalAmount) / 10);
                GrandTotalAmount = Convert.ToString(Convert.ToDecimal(objInvoiceDO.GrandTotalAmount) / 10);
            }
            else
            {
                GrandTotalAmount = objInvoiceDO.TotalAmount;
                TotalAmount = objInvoiceDO.GrandTotalAmount;
            }

            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            MySqlTransaction objTran = null;
            bool _setCustomer = false;
            try
            {
                objCon.Open();
                objTran = objCon.BeginTransaction();

                string query = "";
                MySqlCommand objCommand = null;

                if (!bFoundCustomer)
                {
                    DataRow[] dtR = null;

                    try
                    {
                        dtR = MasterCache.GetCustomerDataFrmCache(ref errorIfo).Select("CustName = '" + objInvoiceDO.CustomerDetails.CustName.Trim() + "'");
                    }
                    catch (Exception ex)
                    {
                        Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
                    }

                    try
                    {
                        if (dtR.Length == 0 && objInvoiceDO.CustomerDetails != null && dtR.Length == 0 && !string.IsNullOrEmpty(objInvoiceDO.CustomerDetails.CustName))
                        {
                            query = " INSERT INTO customermaster " +
                   " (CustName, CustMobile1, CustMobile2, CustTown, CustCreatedDate,CustCreatedBy) " +
                   " VALUES('" + objInvoiceDO.CustomerDetails.CustName + "', '" + objInvoiceDO.CustomerDetails.CustMobile1 + "', '" + objInvoiceDO.CustomerDetails.CustMobile2 + "', " +
                   " '" + objInvoiceDO.CustomerDetails.CustTown + "', '" + objInvoiceDO.InvCreatedDate + "', '" + objInvoiceDO.InvCreatedBy + "');";

                            query += "SELECT LAST_INSERT_ID(); ";
                            objCommand = new MySqlCommand(query, objCon);
                            var ii = objCommand.ExecuteScalar();
                            objInvoiceDO.CustomerDetails.CustID = Convert.ToInt16(ii);

                            _setCustomer = true;

                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
                    }
                }

                if (!isTempInvoice)
                {
                    query = " INSERT INTO invoicemaster " +
                " (InvID, InvSaleType,InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID, InvCreatedBy,InvGrandTotalAmount,InvDiscount, InvCreatedDate,InvNote) " +
                " VALUES('" + objInvoiceDO.InvoiceNumber + "', '" + objInvoiceDO.SaleType + "','" + objInvoiceDO.InvOrRepair + "' ,'" + objInvoiceDO.TotalItems + "', '" + TotalAmount + "', " +
                " '" + objInvoiceDO.CustomerDetails.CustID + "', '" + objInvoiceDO.UserLoginID + "', " +
                 " '" + GrandTotalAmount + "', '" + objInvoiceDO.Discuont + "', " +
                " '" + objInvoiceDO.InvCreatedDate + "', '" + objInvoiceDO.InvNote + "')";
                }
                else
                {

                    #region check invoice exsits in temp tables
                    query = "Delete  from tempinvoicemaster where InvID = '" + objInvoiceDO.InvoiceNumber + "';";
                    query += "Delete  from tempinvoiceitems where InvID = '" + objInvoiceDO.InvoiceNumber + "'";
                    objCommand = new MySqlCommand(query, objCon);
                    int check = objCommand.ExecuteNonQuery();

                    #endregion check invoice exsits in temp tables


                    query = " INSERT INTO tempinvoicemaster " +
           " (InvID, InvSaleType,InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID, InvCreatedBy,InvGrandTotalAmount,InvDiscount,InvCreatedDate,InvNote) " +
           " VALUES('" + objInvoiceDO.InvoiceNumber + "', '" + objInvoiceDO.SaleType + "','" + objInvoiceDO.InvOrRepair + "' ,'" + objInvoiceDO.TotalItems + "', '" + TotalAmount + "', " +
           " '" + objInvoiceDO.CustomerDetails.CustID + "', '" + objInvoiceDO.UserLoginID + "', " +
            " '" + GrandTotalAmount + "', '" + objInvoiceDO.Discuont + "', " +
           " '" + objInvoiceDO.InvCreatedDate + "', '" + objInvoiceDO.InvNote + "')";
                }


                objCommand = new MySqlCommand(query, objCon);

                int i = objCommand.ExecuteNonQuery();

                if (i != 1)
                {
                    _check = false;
                    objTran.Rollback();
                    if (objCon.State == ConnectionState.Open)
                        objCon.Close();

                }
                else
                {
                    int itemsUpdateCount = 0;

                    string _itemsName = "";
                    string _itemsBarCode = "";

                    foreach (InvoiceItemsDO item in objInvoiceDO.ItemsList)
                    {
                        string ItemRate = "0";
                        string Amount = "0";

                        if (ConfigValueLoader.CalculateItemPrice)
                        {
                            ItemRate = Convert.ToString(Convert.ToDecimal(item.ItemRate) / 10);
                            Amount = Convert.ToString(Convert.ToDecimal(item.Amount) / 10);
                        }
                        else
                        {
                            ItemRate = item.ItemRate;
                            Amount = item.Amount;
                        }

                        DateTime dateTemp = item.CreatedDateTime;
                        string convertedDate = Convert.ToDateTime(item.CreatedDateTime).ToString("yyyyMMddhhmmss");


                        query = "";
                        if (!isTempInvoice)
                        {

                            query = " INSERT INTO invoiceitems " +
                 " (InvID, InvBarCode, InvName,  InvQnty, InvRate, InvTotalAmont, InvSupID, InvCreatedBy,InvDateTime,InvSiNo) " +
                 " VALUES('" + objInvoiceDO.InvoiceNumber + "', '" + item.BarCode + "', '" + item.ItemName + "', '" + item.ItemQnty + "', " +
                 " '" + ItemRate + "', '" + Amount + "', '" + item.ItemSuplierID + "', '" + objInvoiceDO.UserLoginID + "', " +
                 " '" + convertedDate + "'," + item.SiNo + ")";

                            _itemsName += " '" + item.ItemName + "'";
                            _itemsBarCode += " '" + item.BarCode + "',";

                        }
                        else
                        {
                            query = " INSERT INTO tempinvoiceitems " +
                 " (InvID, InvBarCode, InvName,  InvQnty, InvRate, InvTotalAmont, InvSupID, InvCreatedBy,InvDateTime,InvSiNo) " +
                 " VALUES('" + objInvoiceDO.InvoiceNumber + "', '" + item.BarCode + "', '" + item.ItemName + "', '" + item.ItemQnty + "', " +
                 " '" + ItemRate + "', '" + Amount + "', '" + item.ItemSuplierID + "', '" + objInvoiceDO.UserLoginID + "', " +
                 " '" + convertedDate + "'," + item.SiNo + ")";
                        }

                        objCommand = new MySqlCommand(query, objCon);

                        itemsUpdateCount += objCommand.ExecuteNonQuery();
                    }

                    if (itemsUpdateCount == objInvoiceDO.ItemsList.Count)
                    {
                        #region Update stok Qnty

                        #region redundent code : to be removed once manul billing is stopped


                        Dictionary<string, int> objlatestQnty = new Dictionary<string, int>();

                        foreach (InvoiceItemsDO invItem in objInvoiceDO.ItemsList)
                        {
                            if (!objlatestQnty.ContainsKey(invItem.BarCode))
                            {
                                if (invItem.ItemQnty != "0")
                                    objlatestQnty.Add(invItem.BarCode, Convert.ToInt16(invItem.ItemQnty));
                                else if (invItem.ItemQnty == "0")
                                    objlatestQnty.Add(invItem.BarCode, 0);
                            }
                            else
                            {
                                int qnty3 = 0;

                                foreach (KeyValuePair<string, int> entryPoint in objlatestQnty)
                                {
                                    if (entryPoint.Key == invItem.BarCode)
                                    {
                                        int qnty1 = Convert.ToInt16(entryPoint.Value);
                                        int qnty2 = Convert.ToInt16(invItem.ItemQnty);
                                        qnty3 = qnty1 + qnty2;
                                    }
                                }
                                objlatestQnty.Remove(invItem.BarCode);
                                objlatestQnty.Add(invItem.BarCode, qnty3);

                            }
                        }
                        #endregion redundent code : to be removed once manul billing is stopped

                        if (!isTempInvoice && objInvoiceDO.InvOrRepair != AviMaConstants.RepairFlag)
                        {

                            _itemsBarCode = _itemsBarCode.Substring(0, _itemsBarCode.Length - 1);
                            if (_itemsBarCode != "")
                            {
                                DataTable dtStockDetails = new DataTable();
                                dtStockDetails = SerahcStocks(_itemsBarCode, ref errorIfo);

                                if (dtStockDetails != null && dtStockDetails.Rows.Count > 0)
                                {
                                    foreach (DataRow dtRow in dtStockDetails.Rows)
                                    {
                                        //foreach (InvoiceItemsDO item in objInvoiceDO.ItemsList)
                                        foreach (KeyValuePair<string, int> entryPoint in objlatestQnty)
                                        {
                                            dtStockDetails = SerahcStocks(_itemsBarCode, ref errorIfo);

                                            if (entryPoint.Key.ToLower() == Convert.ToString(dtRow["IBarCode"]).ToLower())
                                            {

                                                int avlQnty = Convert.ToInt16(dtRow["IQnty"]) - entryPoint.Value;

                                                query = "";
                                                query = "UPDATE itemsmaster " +
                                       "SET" +
                                          " IQnty = '" + avlQnty + "'," +
                                          " IModifiedDate = '" + AviMaConstants.CurrentDateTimesStamp + "'," +
                                          " IModifiedBy = '" + objInvoiceDO.UserLoginID + "'" +
                                          " WHERE IID = " + Convert.ToInt16(dtRow["IID"]);

                                                objCommand = new MySqlCommand(query, objCon);

                                                int updQnty = objCommand.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #region delete tempororay invoice
                        if (!isTempInvoice && objInvoiceDO.InvOrRepair != AviMaConstants.RepairFlag)
                        {
                            //PIP
                            try
                            {
                                query = "delete from tempinvoiceitems where InvID = '" + objInvoiceDO.InvoiceNumber + "';" +
                                                  " delete from tempinvoicemaster where InvID = '" + objInvoiceDO.InvoiceNumber + "'";
                                objCommand = new MySqlCommand(query, objCon);
                                int delQuery = objCommand.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
                            }


                            #region If inv type is whole sale credit then update customer acoounts table
                            try
                            {

                                if (objInvoiceDO.SaleType == AviMaConstants.WholeSaleCredit)
                                {
                                    AccountsDO objAccDO = new AccountsDO();
                                    objAccDO.AccountsType = AviMaConstants.CustAccType;
                                    objAccDO.CustOrSup = AviMaConstants.CustFlag;
                                    objAccDO.Debit = GrandTotalAmount; // divide by 10 to reduce value to 1: 10 ratio
                                    objAccDO.Credit = "0.00";
                                    objAccDO.Balance = "0.00";
                                    objAccDO.CreatedBy = objInvoiceDO.InvCreatedBy;
                                    objAccDO.CreatedDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    objAccDO.CustSupID = objInvoiceDO.CustomerDetails.CustID;
                                    objAccDO.Name = objInvoiceDO.CustomerDetails.CustName;
                                    string errorInfo = "";
                                    bool AccCreat = CreateAccount(objAccDO, ref errorInfo);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
                            }

                            #endregion If inv type is whole sale credit then update customer acoounts table


                        }
                        #endregion delete tempororay invoice
                        #endregion Update stok Qnty

                        objTran.Commit();
                        _check = true;


                        if (_setCustomer)
                            MasterCache.SetCustomerData(ref errorIfo);

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
                errorIfo = ex.Message;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
                _check = false;
                throw;
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;

        }

        public bool CreateAccount(AccountsDO objAccDO, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                #region manipulate customer and supplier Accounts
                string AccountTable = "";
                //string ColumnName = "";
                //string ColumnSupOrCust = "";

                if (objAccDO.CustOrSup == AviMaConstants.CustFlag)
                {
                    AccountTable = "custaccount";
                    //ColumnName = "CustID";
                    //ColumnSupOrCust
                }
                else if (objAccDO.CustOrSup == AviMaConstants.SupFlag)
                {
                    AccountTable = "supaccount";
                    //ColumnName = "SupID";
                    //ColumnSupOrCust
                }


                string qAcc1 = "SELECT * from " + AccountTable + " where ID = " + objAccDO.CustSupID + ";";
                MySqlDataAdapter dap = new MySqlDataAdapter(qAcc1, objCon);
                DataTable dtExistingAccRecords = new DataTable();
                dap.Fill(dtExistingAccRecords);

                decimal _totalAccDebits = 0;
                decimal _totalAccCredits = 0;
                decimal _totalAccBalance = 0;

                objCon.Open();

                if (dtExistingAccRecords != null && dtExistingAccRecords.Rows.Count > 0)
                {

                    foreach (DataRow accItem in dtExistingAccRecords.Rows)
                    {
                        _totalAccDebits = Convert.ToDecimal(accItem["Debit"]);
                        _totalAccCredits = Convert.ToDecimal(accItem["Credit"]);
                        _totalAccBalance = Convert.ToDecimal(accItem["Balance"]);
                        break;
                    }

                    try
                    {
                        _totalAccDebits += Convert.ToDecimal(objAccDO.Debit);
                    }
                    catch (Exception) { }

                    try
                    {
                        _totalAccCredits += Convert.ToDecimal(objAccDO.Credit);
                    }
                    catch (Exception) { }


                    _totalAccBalance = _totalAccDebits - _totalAccCredits;

                    objAccDO.ModyfiedDateTime = AviMaConstants.CurrentDateTimesStamp;

                    string qAcc3 = " UPDATE " + AccountTable + "  SET " +
                                  "  Debit = " + _totalAccDebits + "," +
                                   "  Credit = " + _totalAccCredits + "," +
                                    " Balance = " + _totalAccBalance + "," +
                                  "   DebitDate = " + objAccDO.ModyfiedDateTime + "," +
                                 "    Description = '" + objAccDO.Description + "'" +
                              "   WHERE ID = " + objAccDO.CustSupID + " ";


                    MySqlCommand objCommand1 = new MySqlCommand(qAcc3, objCon);

                    int ii = objCommand1.ExecuteNonQuery();

                    if (ii != 1)
                        _check = false;
                    else
                        _check = true;

                }
                else
                {
                    objAccDO.Balance = Convert.ToString(Convert.ToDecimal(objAccDO.Debit) - Convert.ToDecimal(objAccDO.Credit));

                    string qAcc2 = "INSERT INTO " + AccountTable + " (SiNo, AccountName, Debit, Credit, Balance, DebitDate, Description, ID) " +
                              " VALUES(NULL, '" + objAccDO.Name + "', " + objAccDO.Debit + ",  " + objAccDO.Credit + ",  " + objAccDO.Balance + ",  '" + objAccDO.CreatedDateTime + "', '" + objAccDO.Description + "', " + objAccDO.CustSupID + ") ";

                    MySqlCommand objCommand1 = new MySqlCommand(qAcc2, objCon);

                    int ii = objCommand1.ExecuteNonQuery();

                    if (ii != 1)
                        _check = false;
                    else
                        _check = true;
                }


                #endregion manipulate customer and supplier Accounts


                #region get existing records
                string query1 = "SELECT SiNo, CuSuID, AccountName, CustOrSupp, CreditAmnt, DebitAmnt, BalanceAmnt, date, User " +
                                " FROM creditdebithistory where CuSuID = " + objAccDO.CustSupID + " and CustOrSupp = '" + objAccDO.CustOrSup + "' order by SiNo desc";
                dap = new MySqlDataAdapter(query1, objCon);
                DataTable dtExistingRecords = new DataTable();
                dap.Fill(dtExistingRecords);

                if (dtExistingRecords != null && dtExistingRecords.Rows.Count > 0)
                {
                    //decimal _totalDebits = 0;
                    //decimal _totalCredits = 0;
                    decimal _totalBalance = 0;

                    foreach (DataRow item in dtExistingRecords.Rows)
                    {
                        //try
                        //{
                        //    _totalDebits = Convert.ToDecimal(item["DebitAmnt"]);
                        //}
                        //catch (Exception) { }

                        //try
                        //{
                        //    _totalCredits = Convert.ToDecimal(item["CreditAmnt"]);
                        //}
                        //catch (Exception) { }

                        try
                        {
                            _totalBalance = Convert.ToDecimal(item["BalanceAmnt"]);
                        }
                        catch (Exception) { }

                        break;
                    }
                    if (objAccDO.Credit == "0.00" && objAccDO.Balance == "0.00" && objAccDO.Debit != "0.00") // from invoice gen
                    {
                        decimal latestBalance = Convert.ToDecimal(_totalBalance) + Convert.ToDecimal(objAccDO.Debit);
                        objAccDO.Balance = Convert.ToString(latestBalance);
                    }
                    else if (objAccDO.Debit == "0.00" && objAccDO.Balance == "0.00" && objAccDO.Credit != "0.00") // credit account
                    {
                        decimal latestBalance = Convert.ToDecimal(_totalBalance) - Convert.ToDecimal(objAccDO.Credit);
                        objAccDO.Balance = Convert.ToString(latestBalance);
                    }


                }
                else if (objAccDO.Credit == "0.00" && objAccDO.Balance == "0.00" && objAccDO.Debit != "0.00") //from invoice gen
                {
                    objAccDO.Balance = objAccDO.Debit;
                }
                else if (objAccDO.Debit == "0.00" && objAccDO.Balance == "0.00" && objAccDO.Credit != "0.00") // credit account
                {
                    objAccDO.Balance = "-" + objAccDO.Debit;
                }



                #endregion get existing records


                string query3 = "INSERT INTO creditdebithistory (SiNo, CuSuID, CustOrSupp, CreditAmnt, DebitAmnt, BalanceAmnt, date, User,AccountName) " +
                               "VALUES (NULL, " + objAccDO.CustSupID + ", '" + objAccDO.CustOrSup + "', " + objAccDO.Credit + ", " + objAccDO.Debit + ", " +
                               "" + objAccDO.Balance + ",'" + objAccDO.CreatedDateTime + "', '" + objAccDO.CreatedBy + "', '" + objAccDO.Name + "')";

                MySqlCommand objCommand = new MySqlCommand(query3, objCon);

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


        public string GetInvoiceNumber(string IdType, ref string erroInfo)
        {
            Int64 invNoCurrent = 0;
            string finalInv = "";
            MySqlDataReader objrdr = null;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);


            try
            {

                objCon.Open();

                string query = " select IDNumber from idtracker where IDType = '" + IdType + "' ; ";

                MySqlCommand objCommand = new MySqlCommand(query, objCon);

                objrdr = objCommand.ExecuteReader();

                while (objrdr.Read())
                {
                    invNoCurrent = Convert.ToInt64(objrdr["IDNumber"]) + 1;
                }

                int lentgth = Convert.ToString(invNoCurrent).Length;

                if (lentgth != 5)
                {
                    int apndZero = 5 - lentgth;

                    string zeros = "";
                    for (int i = 0; i < apndZero; i++)
                    {
                        zeros += "0";
                    }

                    if (IdType == "Invoice")
                    {
                        //finalInv = "INV" + zeros + invNoCurrent;
                        finalInv = "000" + zeros + invNoCurrent;
                    }
                    else if (IdType == "Repair")
                        finalInv = "SER" + zeros + invNoCurrent;
                }

                //  int i = objCommand.ExecuteNonQuery();

                //if (i != 1)
                //    _check = false;
                //else
                //    _check = true;
            }
            catch (Exception ex)
            {
                finalInv = "";
                erroInfo = ex.Message;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (!objrdr.IsClosed)
                    objrdr.Close();

                if (objCon.State != ConnectionState.Closed)
                    objCon.Close();
            }

            return finalInv;
        }

        public bool UpdateInvoiceNumber(string IdType, ref string errorInfo)
        {

            bool _check = false;
            MySqlDataReader objrdr = null;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {
                objCon.Open();

                string query = " select IDNumber from idtracker where IDType = '" + IdType + "' ; ";

                MySqlCommand objCommand = new MySqlCommand(query, objCon);

                objrdr = objCommand.ExecuteReader();
                Int64 invNoCurrent = 0;
                while (objrdr.Read())
                {
                    invNoCurrent = Convert.ToInt64(objrdr["IDNumber"]) + 1;
                }

                if (!objrdr.IsClosed)
                    objrdr.Close();

                query = "";

                query = "UPDATE idtracker SET IDNumber = '" + invNoCurrent +
                                  "' WHERE  IDType  = '" + IdType + "'; ";

                objCommand = new MySqlCommand(query, objCon);


                int i = objCommand.ExecuteNonQuery();

                if (i != 1)
                    _check = false;
                else
                    _check = true;
            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                objrdr.Close();
                _check = false;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (!objrdr.IsClosed)
                    objrdr.Close();

                if (objCon.State != ConnectionState.Closed)
                    objCon.Close();
            }

            return _check;
        }


        public DataTable GetAllInvoices(char InvoiceType, ref string errorInfo)
        {

            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtInvoiceData = new DataTable();
            try
            {
                objCon.Open();

                string query = "";
                if (InvoiceType == AviMaConstants.TempInvoiceFlag)
                {
                    query = " select InvID, InvSaleType, InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID, " +
                       "InvNote,InvCreatedBy, InvCreatedDate, InvGrandTotalAmount, InvDiscount from tempinvoicemaster";
                }
                else
                {
                    query = " select InvID, InvSaleType, InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID, " +
                       "InvNote,InvCreatedBy, InvCreatedDate, InvGrandTotalAmount, InvDiscount from invoicemaster where InvOrRepair = '" + InvoiceType + "'";
                }
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



        public DataTable GetInvoice(string invoiceNumb, char invType, ref string erroInfo)
        {
            DataTable dtInvoiceData = new DataTable();
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {
                objCon.Open();
                string query = "";
                if (invType == AviMaConstants.TempInvoiceFlag)
                {
                    query = " select * from tempinvoiceitems where InvID = '" + invoiceNumb + "'";
                }
                else
                {
                    query = " SELECT InvSiNo, InvID, InvBarCode, InvName, InvDescription, InvQnty, InvRate, InvTotalAmont, InvSupID, " +
                    "InvDateTime, InvCreatedBy, InvModifiedDate, InvModifiedBy	FROM invoiceitems where InvID = '" + invoiceNumb + "' order by InvSiNo asc";
                }
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtInvoiceData);
            }
            catch (Exception ex)
            {
                erroInfo = ex.Message;
                dtInvoiceData = null;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State != ConnectionState.Closed)
                    objCon.Close();
            }
            return dtInvoiceData;
        }

        public DataTable SerahcStock(string stcokName, string barCode, string suplierID)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtCustomersData = new DataTable();
            try
            {

                objCon.Open();


                bool _noSearchCriteria = false;
                string query = "SELECT IID, IBarCode, IName, IDescription, IQnty, IPurchasePrice, IPONumber, ISupID," +
                    " IWCashPrice, IWCreditPrice, IWRetailPrice, ICreatedDateTime, ICreatedBy, IModifiedDate, IModifiedBy  FROM itemsmaster";
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
                if (!string.IsNullOrEmpty(suplierID))
                {
                    query += " or ISupID like '%" + suplierID + "%' ";
                    _noSearchCriteria = true;
                }

                if (_noSearchCriteria && query.Substring(queryLen + 1, 2) == "or")
                {
                    query = query.Substring(0, queryLen) + " where " + query.Substring(queryLen + 3);
                }


                /////////////////////////
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtCustomersData);
            }
            catch (Exception ex)
            {
                dtCustomersData = null;
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return dtCustomersData;

        }


        public DataTable SerahcStocks(string barCodes, ref string stcokNames)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtCustomersData = new DataTable();
            try
            {

                objCon.Open();
                string query = "SELECT IID, IBarCode, IName, IQnty  FROM itemsmaster where IBarCode in (" + barCodes + ")";

                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtCustomersData);
            }
            catch (Exception ex)
            {
                dtCustomersData = null;
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return dtCustomersData;

        }


        public DataTable GetAllPO(ref string erroInfo)
        {
            DataTable dtPODetails = new DataTable();
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {

                objCon.Open();
                string query = "";
                query = "SELECT ponumber, posupplierID, poTotalAmount, podate, poRefernece, poCreatedDate, poCreatedBy FROM pomaster";
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);
                objAdapter.Fill(dtPODetails);

            }
            catch (Exception ex)
            {
                dtPODetails = null;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

            return dtPODetails;

        }


        public DataTable SearchInvoice(int customerID, string fromDate, string toDate, string invNumber, string saleType, bool CallfromRpt, char InvoiceOrRepair, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtInvoiceDetails = new DataTable();
            try
            {
                string customerIDManipulated = "''";
                if (customerID != 0)
                    customerIDManipulated = Convert.ToString(customerID);

                objCon.Open();
                string query = "";

                if (CallfromRpt && InvoiceOrRepair != AviMaConstants.TempInvoiceFlag)
                {
                    query = "SELECT InvID, InvSaleType, InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID,InvGrandTotalAmount,InvDiscount,InvNote, InvCreatedBy,InvCreatedDate" +
                       " from invoicemaster where InvCustID =" + customerIDManipulated + " or  (DATE_FORMAT(InvCreatedDate, '%Y-%m-%d') BETWEEN  '" + fromDate + "' and   '" + toDate + "' and InvOrRepair = '" + InvoiceOrRepair + "')";
                }
                else if (InvoiceOrRepair != AviMaConstants.TempInvoiceFlag && InvoiceOrRepair != AviMaConstants.LedgerFlag)
                {
                    query = "SELECT InvID, InvSaleType, InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID,InvGrandTotalAmount,InvDiscount,InvNote, InvCreatedBy,InvCreatedDate" +
                      " from invoicemaster where InvID ='" + invNumber + "' or  InvSaleType ='" + saleType + "' or  (DATE_FORMAT(InvCreatedDate, '%Y-%m-%d') BETWEEN  '" + fromDate + "' and   '" + toDate + "' and InvOrRepair = '" + InvoiceOrRepair + "')";
                }
                else if (InvoiceOrRepair == AviMaConstants.TempInvoiceFlag)
                {
                    query = "SELECT InvID, InvSaleType, InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID,InvGrandTotalAmount,InvDiscount,InvNote, InvCreatedBy,InvCreatedDate" +
                                      " from tempinvoicemaster where InvID ='" + invNumber + "' or  InvSaleType ='" + saleType + "' or  (DATE_FORMAT(InvCreatedDate, '%Y-%m-%d') BETWEEN  '" + fromDate + "' and   '" + toDate + "')";
                }
                else if (InvoiceOrRepair == AviMaConstants.LedgerFlag)
                {
                    query = "SELECT InvID, InvSaleType, InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID,InvGrandTotalAmount,InvDiscount,InvNote, InvCreatedBy,InvCreatedDate" +
                      " from invoicemaster where  InvSaleType ='" + saleType + "' and  (DATE_FORMAT(InvCreatedDate, '%Y-%m-%d') BETWEEN  '" + fromDate + "' and   '" + toDate + "' and InvOrRepair = 'I')";
                }
                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtInvoiceDetails);
            }
            catch (Exception ex)
            {
                dtInvoiceDetails = null;
                errorInfo = ex.Message;
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

            return dtInvoiceDetails;
        }


        public DataTable SearchInvoice(int customerID, string saleTyppe, char InvoiceOrRepair, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtInvoiceDetails = new DataTable();
            try
            {

                objCon.Open();
                string query = "";


                query = "SELECT InvID, InvSaleType, InvOrRepair, InvTotalItems, InvTotalAmount, InvCustID,InvGrandTotalAmount,InvDiscount, InvCreatedBy,InvCreatedDate" +
                  " from invoicemaster where  InvSaleType ='" + saleTyppe + "' and InvOrRepair = '" + InvoiceOrRepair + "'  and InvCustID = '" + customerID + "' ";


                MySqlDataAdapter objAdapter = new MySqlDataAdapter(query, objCon);

                objAdapter.Fill(dtInvoiceDetails);
            }
            catch (Exception ex)
            {
                dtInvoiceDetails = null;
                errorInfo = ex.Message;
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }

            return dtInvoiceDetails;
        }



        public bool DeleteInvoice(string invNubers, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {
                objCon.Open();
                string query = " delete from invoiceitems where InvID in (" + invNubers + ") ; ";
                query += " delete from invoicemaster where InvID in (" + invNubers + ") ; ";
                MySqlCommand objCommand = new MySqlCommand(query, objCon);
                int i = objCommand.ExecuteNonQuery();

                //if (i != 1)
                //    _check = false;
                //else
                _check = true;
            }
            catch (Exception ex)
            {
                _check = false;
                errorInfo = ex.Message;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;
        }

        public bool DeleteTempInvoice(string invNubers, ref string errorInfo)
        {
            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {
                objCon.Open();
                string query = " delete from tempinvoicemaster where InvID in (" + invNubers + ") ; ";
                query += " delete from tempinvoiceitems where InvID in (" + invNubers + ") ; ";
                MySqlCommand objCommand = new MySqlCommand(query, objCon);
                int i = objCommand.ExecuteNonQuery();

                //if (i != 1)
                //    _check = false;
                //else
                _check = true;
            }
            catch (Exception ex)
            {
                _check = false;
                errorInfo = ex.Message;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State == ConnectionState.Open)
                    objCon.Close();
            }
            return _check;
        }

        public string GetInvoiceNumber(int invNumber, ref string erroInfo)
        {
            Int64 invNoCurrent = 0;
            string finalInv = "";
            try
            {
                invNoCurrent = invNumber;// + 1;
                int lentgth = Convert.ToString(invNoCurrent).Length;
                if (lentgth != 5)
                {
                    int apndZero = 5 - lentgth;

                    string zeros = "";
                    for (int i = 0; i < apndZero; i++)
                    {
                        zeros += "0";
                    }
                    //finalInv = "INV" + zeros + invNoCurrent;
                    finalInv = "000" + zeros + invNoCurrent;
                }
            }
            catch (Exception ex)
            {
                finalInv = "";
                erroInfo = ex.Message;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
            }
            return finalInv;
        }
        public DataTable GetInvoicesBetween(string fromInvNo, ref string errorInfo)
        {
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            DataTable dtInvoiceData = new DataTable();
            try
            {
                objCon.Open();
                string query = "";
                query = " SELECT InvID,InvCreatedDate   from invoicemaster where  InvOrRepair = 'I' and  InvID > '" + fromInvNo + "'";
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
        public bool RefreshInvIDS(Dictionary<string, string> invNumbMapping, bool invStartfromBegining, ref string errorInfo)
        {

            bool _check = false;
            MySqlConnection objCon = new MySqlConnection(ConfigValueLoader.ConnectionString);
            try
            {
                objCon.Open();

                MySqlCommand objCommand;

                string lastInvStr = "";

                if (!invStartfromBegining)
                {
                    foreach (KeyValuePair<string, string> entry in invNumbMapping)
                    {
                        string query = "UPDATE invoicemaster	SET InvID='" + entry.Value + "' WHERE InvOrRepair = 'I' and  InvID='" + entry.Key + "' ; " +
                                        "UPDATE invoiceitems	SET		InvID='" + entry.Value + "'	WHERE InvID='" + entry.Key + "' ;";

                        objCommand = new MySqlCommand(query, objCon);

                        int i = objCommand.ExecuteNonQuery();

                        if (i == 0)
                            _check = false;
                        else
                            _check = true;

                        lastInvStr = entry.Value;
                    }
                }
                else
                {
                    lastInvStr = "00000000"; // start inv from 00000001
                }

                if (lastInvStr != "")
                {
                    try
                    {
                        lastInvStr = lastInvStr.Substring(3, lastInvStr.Length - 3);

                        int finalNumb = Convert.ToInt16(lastInvStr);

                        string query1 = "UPDATE idtracker SET  IDNumber = '" + finalNumb + "' WHERE IDType = 'Invoice'";
                        objCommand = new MySqlCommand(query1, objCon);

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
                        Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
                    }
                }


            }
            catch (Exception ex)
            {
                errorInfo = ex.Message;
                _check = false;
                Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Invoice DB layer");
            }
            finally
            {
                if (objCon.State != ConnectionState.Closed)
                    objCon.Close();
            }

            return _check;
        }

    }
}
