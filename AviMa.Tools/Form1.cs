using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AviMa.Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static string ConnectionString = ConfigurationManager.AppSettings["DBConnection"];

        private void button1_Click(object sender, EventArgs e)
        {
            BarCodeMasterDataForm obj = new BarCodeMasterDataForm(ConnectionString);
            this.Hide();
            obj.ShowDialog();
            this.Show();     
        }

        private void btnConvertTaxData_Click(object sender, EventArgs e)
        {
            LoadBarCodesForReview();
        }

        private void LoadBarCodesForReview()
        {
            try
            {
                Dictionary<string, double> objDictionary = new Dictionary<string, double>();

                string query = " select InvID,InvGrandTotalAmount,InvTax1 from invoicemaster; ";

                MySqlDataAdapter adp = new MySqlDataAdapter(query, ConnectionString);

                DataTable dt = new DataTable();   
                dtGrdTaxDtMigration.DataSource = dt;

                foreach (DataRow item in dt.Rows)
                {
                    double actualTax = Convert.ToDouble(item["InvGrandTotalAmount"]) - ((Convert.ToDouble(item["InvGrandTotalAmount"]) * 100) / 105.5);
                    item["InvTax1"] = actualTax +1;
                    objDictionary.Add(Convert.ToString(item["InvID"]), actualTax);

                }
                adp.Fill(dt);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
