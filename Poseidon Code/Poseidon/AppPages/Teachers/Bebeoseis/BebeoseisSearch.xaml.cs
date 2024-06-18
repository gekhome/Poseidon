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
using Telerik.Windows.Controls.GridView;
using Poseidon.Model;
using Poseidon.DataModel;
using Poseidon.Utilities;
using Poseidon.AppPages.Printouts;

namespace Poseidon.AppPages.Teachers.Bebeoseis
{
    /// <summary>
    /// Interaction logic for BebeoseisSearch.xaml
    /// </summary>
    public partial class BebeoseisSearch : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public BebeoseisSearch()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var schoolYears = from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                              orderby s.ΣΧΟΛ_ΕΤΟΣ descending
                              select s;
            cboSchoolYear.ItemsSource = schoolYears.ToList();

            var bebeoseis = from b in db.ViewΕΚΠ_ΒΕΒΑΙΩΣΗs
                            orderby b.ΣΧΟΛΙΚΟ_ΕΤΟΣ ascending, b.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, b.ΗΜΕΡΟΜΗΝΙΑ descending 
                            select b;
            dataGrid.ItemsSource = bebeoseis.ToList();

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }   // class BebeoseisSearch
}
