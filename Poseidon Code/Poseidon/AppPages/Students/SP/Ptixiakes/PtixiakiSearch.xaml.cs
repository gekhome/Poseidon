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
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;
using Poseidon.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Data.DataForm;
using System.ComponentModel;

namespace Poseidon.AppPages.Students.SP
{
    /// <summary>
    /// Interaction logic for PtixiakiSearch.xaml
    /// </summary>
    public partial class PtixiakiSearch : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public PtixiakiSearch()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var ptixiakes = from p in db.ViewΠΤΥΧΙΑΚΕΣ_ΣΠs
                            orderby p.ΣΧΟΛ_ΕΤΟΣ descending, p.ΤΜΗΜΑ_ΟΝΟΜΑΣΙΑ, p.ΟΝΟΜΑΤΕΠΩΝΥΜΟ1, p.ΟΝΟΜΑΤΕΠΩΝΥΜΟ2, p.ΟΝΟΜΑΤΕΠΩΝΥΜΟ3
                            select p;

            dataGrid.ItemsSource = ptixiakes.ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

    }
}
