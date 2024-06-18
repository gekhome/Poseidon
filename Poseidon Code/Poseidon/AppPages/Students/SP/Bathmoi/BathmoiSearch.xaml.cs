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
    /// Interaction logic for BathmoiSearch.xaml
    /// </summary>
    public partial class BathmoiSearch : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public BathmoiSearch()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var records = from r in db.ViewΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ_ΣΠs
                          orderby r.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ descending, r.ΟΝΟΜΑΤΕΠΩΝΥΜΟ, r.ΜΑΘΗΜΑ, r.ΤΜΗΜΑ_ΟΝΟΜΑΣΙΑ
                          select r;

            dataGrid.ItemsSource = records.ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

    }
}
