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

namespace Poseidon.AppPages.Teachers.Simbaseis
{
    /// <summary>
    /// Interaction logic for SimbaseisSearch.xaml
    /// </summary>
    public partial class SimbaseisSearch : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public SimbaseisSearch()
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

            var simbaseis = from s in db.ViewΕΚΠ_ΣΥΜΒΑΣΗs
                            orderby s.ΣΧΟΛ_ΕΤΟΣ descending, s.ΗΜΕΡΟΜΗΝΙΑ descending, s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                            select s;
            dataGrid.ItemsSource = simbaseis.ToList();

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            var simbaseis = from s in db.ViewΕΚΠ_ΣΥΜΒΑΣΗs
                            orderby s.ΣΧΟΛ_ΕΤΟΣ descending, s.ΗΜΕΡΟΜΗΝΙΑ descending, s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                            select s;
            dataGrid.ItemsSource = simbaseis.ToList();
        }


    }   // class SimbaseisSearch
}
