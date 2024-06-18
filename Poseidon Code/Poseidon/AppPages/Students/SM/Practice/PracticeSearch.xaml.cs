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

namespace Poseidon.AppPages.Students.SM
{
    /// <summary>
    /// Interaction logic for PracticeSearch.xaml
    /// </summary>
    public partial class PracticeSearch : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public PracticeSearch()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var keps = from k in db.ViewΚΕΠ_ΣΜs
                       orderby k.ΣΧΟΛ_ΕΤΟΣ descending, k.ΤΜΗΜΑ_ΟΝΟΜΑΣΙΑ, k.ΚΕΠ, k.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                       select k;

            dataGrid.ItemsSource = keps.ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
