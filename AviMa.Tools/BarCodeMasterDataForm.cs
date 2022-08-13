using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AviMa.Tools
{
    public partial class BarCodeMasterDataForm : Form
    {

        string connection = "";
        public BarCodeMasterDataForm(string _connection)
        {
            InitializeComponent();
            connection = _connection;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Cursor =Cursors.WaitCursor;

            // List<string> objBArcodes = new List<string>();
            try
            {

                for (int i = 1; i < 1000000; i++)// upto 9999
                {
                    string _iString = Convert.ToString(i);

                    int length = _iString.Length;

                    if (length < 6)
                    {
                        int zerosToAppend = 6 - length;
                        string _zeros = "";
                        for (int j = 0; j < zerosToAppend; j++)
                        {
                            _zeros += "0";
                        }

                        _iString = _zeros + _iString;

                    }
                    else
                    {

                    }
                    string errorInfo = "";
                    CreateBArCodeMasterData(_iString, ref errorInfo);
                    //objBArcodes.Add(_iString);
                }

                LoadBarCodesForReview();

                MessageBox.Show("Done with creating barcodes");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }


        private void LoadBarCodesForReview()
        {
            try
            {
                string query = " select * from barcodemaster; ";

                MySqlDataAdapter adp = new MySqlDataAdapter(query, connection);

                DataTable dt = new DataTable();

                adp.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        

        }

        public bool CreateBArCodeMasterData(string barcode, ref string errorInfo)
        {

            bool _check = false;

            MySqlConnection objCon = new MySqlConnection(connection);
            try
            {
                objCon.Open();

                string query = " INSERT INTO barcodemaster	(ID, BarCode)	VALUES (NULL, '" + barcode + "'); ";

                MySqlCommand objCommand = new MySqlCommand(query, objCon);

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
                MessageBox.Show(errorInfo);
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
