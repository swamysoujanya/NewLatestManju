using MySql.Data.MySqlClient; 
using System;

namespace AviMa
{
    internal class DBConnection
    {
        private DBConnection()
        {
        }

        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MySqlConnection Connection { get; set; }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            try
            {
                if (Connection == null)
                {
                    if (String.IsNullOrEmpty(DatabaseName))
                        return false;
                    //string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
                    string connstring = string.Format($"datasource={Server}; port=3306; username={UserName}; password={Password}; database={DatabaseName};");
                    Connection = new MySqlConnection(connstring);
                    Connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }
}