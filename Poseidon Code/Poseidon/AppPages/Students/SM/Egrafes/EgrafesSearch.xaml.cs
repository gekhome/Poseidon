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
    /// Interaction logic for EgrafesSearch.xaml
    /// </summary>
    public partial class EgrafesSearch : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public EgrafesSearch()
        {
            InitializeComponent();
            LoadData();
        }

    private void LoadData()
    {
        var eggrafes = from e in db.ViewΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ_ΣΜs
                       orderby e.ΣΧΟΛ_ΕΤΟΣ descending, e.ΤΜΗΜΑ_ΟΝΟΜΑΣΙΑ, e.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                       select e;

        dataGrid.ItemsSource = eggrafes.ToList();
    }

    private void btnRefresh_Click(object sender, RoutedEventArgs e)
    {
        LoadData();
    }

    }   // class EgrafesSearch
}
