using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Poseidon.Utilities;
using Poseidon.SqlConnections;
using System.ComponentModel;


namespace Poseidon.Shell
{
    /// <summary>
    /// Interaction logic for ConnectPage.xaml
    /// </summary>
    public partial class ConnectPage : INotifyPropertyChanged
    {
        private SqlConnectionString _connstring;
        public string _appPath;
        public string _dbPath;
        public string _dbfile;

        public ConnectPage()
        {
            InitializeComponent();
            _appPath = Directory.GetCurrentDirectory();
            _dbPath = _appPath + "\\DB\\";
            _dbfile = _dbPath + "PoseidonDB.mdf";
            //txtAppPath.Text +=_dbfile;

        }

        public SqlConnectionString ConnString
        {
            get { return _connstring; }
            set
            {
                _connstring = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ConnString"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            LoginPage _loginPage;
            _loginPage = new LoginPage();

            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(_loginPage);

            //UserFunctions.RadWindowAlert("Go to Login Page");
        }

        private void btnAttach_Click(object sender, RoutedEventArgs e)
        {
            AttachDB();
        }

        private void AttachDB()
        {
            txtProgress.Text = "Γίνεται σύνδεση ...";
            SqlConnection conn = new SqlConnection(ConnString);
            conn = new SqlConnection("Data Source=(local)\\SQLSERVER;Integrated Security=SSPI;User Instance=False"); // works
            //conn = new SqlConnection("Server=(local)\\SQLSERVER;Data Source=;Integrated Security=SSPI;User Instance=False"); // does not work
            SqlCommand cmd = new SqlCommand("", conn);

            cmd.CommandText = "exec sys.sp_attach_db    PoseidonDB, '" + _dbfile +  "'";
            //cmd.CommandText =
            //                "CREATE DATABASE 'PoseidonDB' ON " +
            //                "PRIMARY ( FILENAME =  '" + _dbfile + "' ) " +
            //                "FOR ATTACH";
            //UserFunctions.RadWindowAlert(cmd.CommandText);

            //conn = new SqlConnection("Data Source=GEK-PC\\SQLSERVER;Initial Catalog=PoseidonDB;Integrated Security=True");

            // check if already attached
            if (DatabaseExists("PoseidonDB"))
            {
                txtProgress.Text = "Η σύνδεση ακυρώθηκε διότι υπάρχει ήδη.";
                UserFunctions.ShowAdminMessage("Η σύνδεση της βάσης δεδομένων, στον κεντρικό διακομιστή, έχει ήδη γίνει.", "#FF000000");
                return;
            }
            conn.Open();

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Dispose();

            txtProgress.Text = "Η σύνδεση ολοκληρώθηκε.";

        }

        private static bool DatabaseExists(string databaseName)
        {
            string sqlCreateDBQuery;
            bool result = false;
            SqlConnection tmpConn;
        
            try
            {
                tmpConn = new SqlConnection("Data Source=(local)\\SQLSERVER;Integrated Security=SSPI;User Instance=False");
                sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);

                using (tmpConn)
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, tmpConn))
                    {
                        tmpConn.Open();
                        int databaseID = (int)sqlCmd.ExecuteScalar();    
                        tmpConn.Close();
                        result = (databaseID > 0);
                    }
                }
            } 
            catch (Exception)
            { 
                result = false;
            }

            return result;
        }

    } // class ConnectPage
}
