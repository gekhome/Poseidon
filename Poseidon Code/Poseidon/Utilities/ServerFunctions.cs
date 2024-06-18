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
using System.ComponentModel;
using System.Configuration;
using Telerik.Windows.Controls;
using Poseidon.Model;
using Poseidon.DataModel;
using Poseidon.Utilities;


namespace Poseidon.Utilities
{

    class ServerFunctions
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private static string _connString = "";

        public static string ReadConnString()
        {
            _connString = ConfigurationManager.ConnectionStrings["Poseidon.Properties.Settings.PoseidonDBConnectionString"].ConnectionString;

            //UserFunctions.RadWindowAlert(_connString);

            return _connString;
        }

        public static void WriteConnString()
        {

        }

        public static void DeleteData()
        {

            // delete student data
            DeleteTableData("ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ");
            DeleteTableData("ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ");
            DeleteTableData("ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ");
            DeleteTableData("ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ");
            DeleteTableData("ΚΕΠ");
            DeleteTableData("ΣΠΟΥΔΑΣΤΗΣ");

            // delete teacher data
            DeleteTableData("ΑΝΑΘΕΣΕΙΣ_ΣΜ");
            DeleteTableData("ΑΝΑΘΕΣΕΙΣ_ΣΠ");
            DeleteTableData("ΕΚΠ_ΒΕΒΑΙΩΣΗ");
            DeleteTableData("ΕΚΠ_ΣΥΜΒΑΣΗ");
            DeleteTableData("ΠΡΟΓΡΑΜΜΑ_ΣΜ");
            DeleteTableData("ΠΡΟΓΡΑΜΜΑ_ΣΠ");
            DeleteTableData("ΕΚΠΑΙΔΕΥΤΙΚΟΣ");

            // delete general data
            DeleteTableData("ΑΕΝ");
            DeleteTableData("ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ");
            DeleteTableData("ΤΜΗΜΑΤΑ_ΣΜ");
            DeleteTableData("ΤΜΗΜΑΤΑ_ΣΠ");

        }

        public static void DeleteTableData(string table_name)
        {
            string conn_string = ReadConnString();

            SqlConnection conn = new SqlConnection(conn_string);
            SqlCommand cmd = new SqlCommand("", conn);

            cmd.CommandText = "DELETE FROM " + table_name;

            conn.Open();
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            conn.Dispose();
        }




    }   // class ServerFunctions
}
