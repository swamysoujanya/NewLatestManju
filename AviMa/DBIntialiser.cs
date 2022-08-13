using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Threading;
using System.Linq;

using AviMa.AviMaForms;
using AviMa.DataBaseLayer;
using AviMa.UtilityLayer;


namespace AviMa
{
    public partial class DBIntialiser : System.Windows.Forms.Form
    {
        public DBIntialiser()
        {
            InitializeComponent();
            ResetControls();
        }

        private void buttonShowBrowseDialog_Click(object sender, EventArgs e)
        {           
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                labelSelectedPath.Text = folderBrowserDialog.SelectedPath;
                buttonConnect.Enabled = true;
            }
            else
            {
                ResetControls();
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                buttonClear.Enabled = false;

                if (string.IsNullOrEmpty(labelSelectedPath.Text))
                {
                    MessageBox.Show("Select the path/drive");
                    return;
                }

                var mySQLServerWindowsServiceName = ConfigurationManager.AppSettings.Get("MySQLServerWindowsService");
                if (string.IsNullOrEmpty(mySQLServerWindowsServiceName))
                {
                    MessageBox.Show("Invalid MySQL Server Windows Service Name");
                    return;
                }

                var service = new ServiceController(mySQLServerWindowsServiceName);
                if (service != null) //To check if service is installed
                {
                    ////Stop the MySQL Windows Service if it is running
                    if (service.Status == ServiceControllerStatus.Running) //To check if the service is in 'Running' status
                    {
                        try
                        {
                            service.Stop(); //To stop the service
                            var timeout = new TimeSpan(0, 0, 10); //5 seconds
                            service.WaitForStatus(ServiceControllerStatus.Stopped, timeout); //To wait for service to stop
                        }
                        catch (InvalidOperationException ex)
                        {
                            txtErrorLogger.Text = ex.Message;
                            MessageBox.Show($"{ex.Message}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }

                    ////Include the logic to modify the (C:\ProgramData\MySQL\MySQL Server 8.0\my.ini file) to set variable datadir value
                    IniFileParser();

                    //Wait or pause for 5 seconds
                    System.Threading.Thread.Sleep(5000);

                    ////Start the MySQL Windows Service back to running status
                    if (service.Status == ServiceControllerStatus.Stopped) //To check if the service is in 'Running' status
                    {
                        try
                        {
                            service.Start(); //To stop the service
                            var timeout = new TimeSpan(0, 0, 30); //10 seconds
                            service.WaitForStatus(ServiceControllerStatus.Running, timeout); //To wait for service to start
                        }
                        catch (InvalidOperationException ex)
                        {
                            txtErrorLogger.Text = ex.Message;
                            MessageBox.Show($"{ex.Message}");
                            return;
                        }
                    }

                    ////Try connecting to MySQL database
                    try
                    {
                        TryConnection();
                        //Login logic : Start

                        try
                        {
                            #region Load Cache
                            string errorInfo = "";
                            bool check = true;
                            try
                            {
                                check = MasterCache.SetCache(ref errorInfo);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Unabled to load cache data  " + ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Logger.LogFile(ex.Message, errorInfo, " static void Main()", ex.LineNumber(), "Login form");
                            }

                            if (!check)
                            {
                                MessageBox.Show("Unabled to load cache data " + errorInfo, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Error); // CH_23_11_2015
                            }
                            else
                            {
                                this.Hide();
                                LoginForm objLoginForm = new LoginForm();
                                objLoginForm.Show();
                               
                            }
                            #endregion Load Cache
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message + "Method Name:" + ex.GetBaseException(), "Error  Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Logger.LogFile(ex.Message, "", "", ex.LineNumber(), "Program");
                        }

                        //Login Logic : End
                        MessageBox.Show($"Connection Succeeded");
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException != null)
                        {
                            txtErrorLogger.Text = ex.Message;
                            MessageBox.Show($"{ex.Message}, {ex.InnerException.Message}");
                        }
                        else
                        {
                            txtErrorLogger.Text = ex.Message;
                            MessageBox.Show($"{ex.Message}");
                        }
                    }
                }

                ResetControls(false);
            }
            catch (Exception ex)
            {
                txtErrorLogger.Text = ex.ToString();
            }
        }

        private void ResetControls(bool resetLabel = true)
        {
            if (!resetLabel)
            {
                labelSelectedPath.Text = "No path selected";
                buttonClear.Enabled = true;
            }
            buttonConnect.Enabled = false;
        }

        private void IniFileParser()
        {
            //var parser = new FileIniDataParser();
            //IniData data = parser.ReadFile(@"C:\ProgramData\MySQL\MySQL Server 8.0\My.ini");

            ////Retrieve the value for a key inside of a named section (values are always fetched as string)
            //string useFullScreenStr = data["UI"]["fullscreen"];
            //// useFullScreenStr contains "true"
            //bool useFullScreen = bool.Parse(useFullScreenStr);

            ////Modify the value in the dictionary, not the value retrieved, and saved to a new file or overwrite
            //data["UI"]["fullscreen"] = "true";
            //parser.WriteFile(@"C:\ProgramData\MySQL\MySQL Server 8.0\My.ini", data);

            var mySQLServer_DefaultConfigurationSetting = ConfigurationManager.AppSettings.Get("MySQLServer_DefaultConfigurationSetting");
            if (string.IsNullOrEmpty(mySQLServer_DefaultConfigurationSetting))
            {
                MessageBox.Show("MySQL Server Configuration Setting doesn't exists");
                return;
            }

            var lines = System.IO.File.ReadAllLines($"{mySQLServer_DefaultConfigurationSetting}").ToList();
            const string pattern = @"datadir=";

            //var matchingLine = lines.Where(text => text.StartsWith(pattern)).FirstOrDefault();
            //if (matchingLine != null)
            //{
            //    matchingLine = matchingLine.Replace(matchingLine.Substring(pattern.Length), @"C:\Data");
            //}

            var matchingLineIndex = lines.FindIndex(text => text.StartsWith(pattern));
            if (matchingLineIndex >= 0)
            {
                //lines[matchingLineIndex] = lines[matchingLineIndex].Replace(lines[matchingLineIndex].Substring(pattern.Length), @"C:\Data");
                lines[matchingLineIndex] = lines[matchingLineIndex].Replace(lines[matchingLineIndex].Substring(pattern.Length), $"{labelSelectedPath.Text}");
            }

            System.IO.File.WriteAllLines($"{mySQLServer_DefaultConfigurationSetting}", lines.ToArray());
        }

        private bool TryConnection()
        {
            bool _checkDbconnetion = false;

            var dbCon = DBConnection.Instance();
            dbCon.Server = "localhost"; //hostname (make sure to add new user along with hostname and grant the privilege to schema's)
            dbCon.DatabaseName = "avimadb"; //Schema name is database name
            dbCon.UserName = "root"; //username
            dbCon.Password = "zaserp"; //password
            try
            {
                if (dbCon.IsConnect())
                {
                    string query = "SELECT * FROM appconfiguration;";
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var someStringFromColumnZero = reader.GetInt32(0);
                        var someStringFromColumnOne = reader.GetString(1);
                        //MessageBox.Show(someStringFromColumnZero + "," + someStringFromColumnOne);
                    }
                    dbCon.Close();

                    _checkDbconnetion = true;
                }
            }
            catch (Exception ex)
            {
                _checkDbconnetion = false;
                txtErrorLogger.Text = ex.Message;
                MessageBox.Show(ex.Message);
            }

            return _checkDbconnetion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TryConnection())
            {
                this.Hide();
                LoginForm objLoginForm = new LoginForm();
                objLoginForm.Show();
            }
            else
            {
                MessageBox.Show("Please select a right drive and proceed further");
            }
        }
    }
}
