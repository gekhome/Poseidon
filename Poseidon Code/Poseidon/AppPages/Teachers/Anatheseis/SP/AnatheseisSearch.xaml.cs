using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.DataForm;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;

namespace Poseidon.AppPages.Teachers.Anatheseis.SP
{
    /// <summary>
    /// Interaction logic for AnatheseisSearch.xaml
    /// </summary>
    public partial class AnatheseisSearch : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public AnatheseisSearch()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var anatheseis = from a in db.sqlΑΝΑΘΕΣΕΙΣ_ΣΠs
                             select a;

            anathesiGrid.ItemsSource = anatheseis.ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

    }
}
