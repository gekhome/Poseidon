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
using Poseidon.AppPages.Teachers;
using Poseidon.AppPages.Teachers.Printouts;

namespace Poseidon.AppPages.Teachers
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
            string shortText = "Λίστα Εκπαιδευτικών";
            string longText = "Κατάλογος Εκπαιδευτικών με όλα τα στοιχεία \n";
            longText += "(διεύθυνση, τηλέφωνα, email)";
            SetTileContent(tile01, btn01, txtTitle01, longText, shortText);
        }

        private void tile02_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Υ.Δ. 1599";
            string longText = "Υπεύθυνη Δήλωση του Ν.1599/1986";
            SetTileContent(tile02, btn02, txtTitle02, longText, shortText);
        }

        private void tile03_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Έντυπο Αίτησης";
            string longText = "Έντυπο Αίτησης προς την Γραμματεία με προσυμπληρωμένα τα στοιχεία του εκπαιδευτικού";
            SetTileContent(tile03, btn03, txtTitle03, longText, shortText);
        }

        private void tile04_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Βεβαιώσεις Προϋπηρεσίας";
            string longText = "Βεβαιώσεις προϋπηρεσίας εκπαιδευτικών συνολικές ή ανά διδ. έτος";
            SetTileContent(tile04, btn04, txtTitle04, longText, shortText);
        }

        private void tile05_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Μηνιαίες Ώρες";
            string longText = "Αναλυτικός πίνακας υλοποιηθέντων μηνιαίων ωρών Ε.Ε.Π. \n ";
            longText += "(ανά διδ. έτος και μήνα και από τις δύο σχολές (ΣΠ, ΣΜ)";
            SetTileContent(tile05, btn05, txtTitle05, longText, shortText);
        }

        private void tile06_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Τίτλος εντύπου 6";
            string longText = "Αναλυτική περιγραφή εντύπου 6 - Λογισμικό Poseidon";
            SetTileContent(tile06, btn06, txtTitle06, longText, shortText);
        }

        private void tile07_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Τίτλος εντύπου 7";
            string longText = "Αναλυτική περιγραφή εντύπου 7 - Λογισμικό Poseidon";
            SetTileContent(tile07, btn07, txtTitle07, longText, shortText);
        }

        private void tile08_TileStateChanged(object sender, RadRoutedEventArgs e)
        {
            string shortText = "Τίτλος εντύπου 8";
            string longText = "Αναλυτική περιγραφή εντύπου 8 - Λογισμικό Poseidon";
            SetTileContent(tile08, btn08, txtTitle08, longText, shortText);
        }

        #endregion


        #region Tile Button Events

        private void btn01_Click(object sender, RoutedEventArgs e)
        {
            TeacherListWindow radwindow = new TeacherListWindow();
            radwindow.Show();
        }

        private void btn02_Click(object sender, RoutedEventArgs e)
        {
            Dilosi1599 radwindow = new Dilosi1599();
            radwindow.Show();
        }

        private void btn03_Click(object sender, RoutedEventArgs e)
        {
            TeacherAitisi radwindow = new TeacherAitisi();
            radwindow.Show();
        }

        private void btn04_Click(object sender, RoutedEventArgs e)
        {
            TeacherBebaiosi radwindow = new TeacherBebaiosi();
            radwindow.Show();
        }

        private void btn05_Click(object sender, RoutedEventArgs e)
        {
            TeacherMonthHours radwindow = new TeacherMonthHours();
            radwindow.Show();
        }

        private void btn06_Click(object sender, RoutedEventArgs e)
        {
            DemoReport radwindow = new DemoReport();
            radwindow.Show();
        }

        private void btn07_Click(object sender, RoutedEventArgs e)
        {
            DemoReport radwindow = new DemoReport();
            radwindow.Show();
        }

        private void btn08_Click(object sender, RoutedEventArgs e)
        {
            DemoReport radwindow = new DemoReport();
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
                txtBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
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
