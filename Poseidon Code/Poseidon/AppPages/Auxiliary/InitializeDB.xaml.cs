using System;
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
using Telerik.Windows.Controls;
using Poseidon.Model;
using Poseidon.DataModel;
using Poseidon.Utilities;

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for InitializeDB.xaml
    /// </summary>
    public partial class InitializeDB : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public InitializeDB()
        {
            InitializeComponent();
        }



        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string verify_string = "Θέλετε σίγουρα διαγραφή των δεδομένων;";
            if (MessageBox.Show(verify_string, "Διαγραφή", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            { return; }

            Delay(100000000) ;
            txtMessage.Text = "Έναρξη διαγραφής δεδομένων ...";
            progressBar.Visibility = Visibility.Visible;

            if (cm.UserCanUpdate() == false)
            {
                txtMessage.Text = "Η διαγραφή ακυρώθηκε.";
                progressBar.Visibility = Visibility.Hidden;
                return;
            }

            ServerFunctions.DeleteData();

            txtMessage.Text = "Η διαγραφή ολοκληρώθηκε.";
            progressBar.Visibility = Visibility.Hidden;

        }

        private void Delay(int count)
        {
            for (int i = 0; i <= count; i++) ;

        }
    }   // class InitializeDB
}
