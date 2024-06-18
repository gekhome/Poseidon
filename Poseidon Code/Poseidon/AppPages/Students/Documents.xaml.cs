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
using Telerik.Windows;
using Telerik.Windows.Controls;
using Poseidon.Utilities;
using Poseidon.AppPages.Students;
using Poseidon.AppPages.Students.Printouts;


namespace Poseidon.AppPages.Students
{
    /// <summary>
    /// Interaction logic for Documents.xaml
    /// </summary>
    public partial class Documents : Page
    {
        public Documents()
        {
            InitializeComponent();
        }

        #region Tile Events

        private void tile01_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Υπεύθυνη Δήλωση";
            string longText = "Υπεύθυνη Δήλωση Ν.1599/1986";
            SetTileContent(tile01, btn01, txtTitle01, longText, shortText);
        }

        private void tile02_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Κατάλογος Σπουδαστών";
            string longText = "Κατάλογος ατομικών στοιχείων σπουδαστών.";
            SetTileContent(tile02, btn02, txtTitle02, longText, shortText);
            
        }

        private void tile03_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Αιτήσεις Βεβ./Πιστοπ.";
            string longText = "Αίτηση σπουδαστή για έκδοση βεβαιώσεων/πιστοποιητικών.";
            SetTileContent(tile03, btn03, txtTitle03, longText, shortText);
        }

        private void tile04_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Αιτήσεις Εγγραφών";
            string longText = "Αιτήσεις εγγραφής σπουδαστών στα εξάμηνα σπουδών.";
            SetTileContent(tile04, btn04, txtTitle04, longText, shortText);
        }

        private void tile05_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Αιτήσεις κατάθ. Π.Ε.";
            string longText = "Αιτήσεις κατάθεσης Πτυχιακών Εργασιών";
            SetTileContent(tile05, btn05, txtTitle05, longText, shortText);
        }

        private void tile06_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Αιτήσεις κ.π. Κ.Ε.";
            string longText = "Αιτήσεις εγγραφής κατά παρέκκλιση του Κ.Ε.";
            SetTileContent(tile06, btn06, txtTitle06, longText, shortText);
        }

        private void tile07_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Συγκ.Βαθμ. ΣΜ";
            string longText = "Συγκεντρωτική έκθεση βαθμολογιών σπουδαστών ΣΜ";
            SetTileContent(tile07, btn07, txtTitle07, longText, shortText);
        }

        private void tile08_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Συγκ.Βαθμ. ΣΠ";
            string longText = "Συγκεντρωτική έκθεση βαθμολογιών σπουδαστών ΣΠ";
            SetTileContent(tile08, btn08, txtTitle08, longText, shortText);
        }

        private void tile09_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Πιστοποιητικό Αποφ. ΣΜ";
            string longText = "Πιστοποιητικό αποφοίτησης με αναλυτική βαθμολογία ΣΜ";
            SetTileContent(tile09, btn09, txtTitle09, longText, shortText);
        }

        private void tile10_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Πιστοποιητικό Αποφ. ΣΠ";
            string longText = "Πιστοποιητικό αποφοίτησης με αναλυτική βαθμολογία ΣΠ";
            SetTileContent(tile10, btn10, txtTitle10, longText, shortText);
        }

        private void tile11_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Πιστοποιητικό Σπουδών ΣΠ";
            string longText = "Πιστοποιητικό σπουδών για κάθε νόμιμη χρήση (ΣΠ)";
            SetTileContent(tile11, btn11, txtTitle11, longText, shortText);
        }

        private void tile12_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Πιστοποιητικό Σπουδών ΣΜ";
            string longText = "Πιστοποιητικό σπουδών για κάθε νόμιμη χρήση (ΣΜ)";
            SetTileContent(tile12, btn12, txtTitle12, longText, shortText);
        }



        #endregion


        #region Tile Button Events

        private void btn01_Click(object sender, RoutedEventArgs e)
        {
            YD1599 radwindow = new YD1599();
            radwindow.Show();
        }

        private void btn02_Click(object sender, RoutedEventArgs e)
        {
            StudentDataWindow radwindow = new StudentDataWindow();
            radwindow.Show();
        }

        private void btn03_Click(object sender, RoutedEventArgs e)
        {
            StudentAitisi1 radwindow = new StudentAitisi1();
            radwindow.Show();
        }

        private void btn04_Click(object sender, RoutedEventArgs e)
        {
            StudentAitisi2 radwindow = new StudentAitisi2();
            radwindow.Show();
        }

        private void btn05_Click(object sender, RoutedEventArgs e)
        {
            StudentAitisi3 radwindow = new StudentAitisi3();
            radwindow.Show();
        }

        private void btn06_Click(object sender, RoutedEventArgs e)
        {
            StudentAitisi4 radwindow = new StudentAitisi4();
            radwindow.Show();
        }

        private void btn07_Click(object sender, RoutedEventArgs e)
        {
            StudentGradesReportSM radwindow = new StudentGradesReportSM();
            radwindow.Show();
        }

        private void btn08_Click(object sender, RoutedEventArgs e)
        {
            StudentGradesReportSP radwindow = new StudentGradesReportSP();
            radwindow.Show();
        }

        private void btn09_Click(object sender, RoutedEventArgs e)
        {
            StudentCertificate1SM radwindow = new StudentCertificate1SM();
            radwindow.Show();
        }

        private void btn10_Click(object sender, RoutedEventArgs e)
        {
            StudentCertificate1SP radwindow = new StudentCertificate1SP();
            radwindow.Show();
        }

        private void btn11_Click(object sender, RoutedEventArgs e)
        {
            BebeoseisPrintSP radwindow = new BebeoseisPrintSP();
            radwindow.Show();
        }

        private void btn12_Click(object sender, RoutedEventArgs e)
        {
            BebeoseisPrintSM radwindow = new BebeoseisPrintSM();
            radwindow.Show();
        }

        #endregion

        private void SetTileContent(RadTileViewItem tile, RadButton btn, TextBlock txtBlock, string longText, string shortText)
        {
            string tname = tile.Name;

            if (tile.TileState == TileViewItemState.Maximized)
            {
                btn.Visibility = Visibility.Visible;
                txtBlock.Text = longText;
                txtBlock.FontSize = 24;
            }
            else
            {
                txtBlock.Text = shortText;
                txtBlock.FontSize = 14;
                btn.Visibility = Visibility.Collapsed;
            }

        }


    }
}
