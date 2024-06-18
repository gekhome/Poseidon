using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Controls.Chart;
using Telerik.Windows.Controls.Charting;
using Telerik.Windows.Controls.ChartView;
using Poseidon.AppPages.Statistics.SP.ChartViewModel;
using Poseidon.Controls;
using Poseidon.Model;
using Poseidon.Utilities;

namespace Poseidon.AppPages.Statistics.SP.Charts
{
    /// <summary>
    /// Interaction logic for TeacherTypeBarChart.xaml
    /// </summary>
    public partial class TeacherTypeBarChart : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private static int _syear = 0;
        private static string _work = null;
        private static string _klados = null;
        private static double _count = 0;

        private WorkDataSP wd = new WorkDataSP(_syear, _work, _klados, _count);
        public ObservableCollection<string> names = new ObservableCollection<string>();

        public TeacherTypeBarChart()
        {
            InitializeComponent();
            LoadComboData();
        }

        public void LoadComboData()
        {
            var syears = from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         orderby s.ΣΧΟΛ_ΕΤΟΣ descending
                         select s;

            cboSchoolYearSelection.ItemsSource = syears.ToList();
        }

        #region Selection button events

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            ΣΧΟΛΙΚΟ_ΕΤΟΣ syear = cboSchoolYearSelection.SelectedItem as ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            WorkViewModelSP wvm = DataContext as WorkViewModelSP;

            if (syear == null)
            {
                wvm.wd.SchoolYear = 0;
                wvm.RefreshViewModel();
            }
            else if (syear != null)
            {
                int selected_syear = syear.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ;
                wvm.wd.SchoolYear = selected_syear;
                wvm.RefreshViewModel();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cboSchoolYearSelection.SelectedIndex = -1;

            WorkViewModelSP wvm = DataContext as WorkViewModelSP;
            wvm.wd.SchoolYear = 0;
            wvm.RefreshViewModel();
        }

        private void cboCombineMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WorkViewModelSP wvm = DataContext as WorkViewModelSP;

            if (cboCombineMode.SelectedValue != null)
            {
                int index = cboCombineMode.SelectedIndex;

                if (index == 0)
                {
                    cboCombineMode.SelectedValue = "Συστοιχία";
                    wvm.BarCombineMode = Telerik.Charting.ChartSeriesCombineMode.Cluster;
                }
                if (index == 1)
                {
                    cboCombineMode.SelectedValue = "Στοίβα";
                    wvm.BarCombineMode = Telerik.Charting.ChartSeriesCombineMode.Stack;
                }
                if (index == 2)
                {
                    cboCombineMode.SelectedValue = "Στοίβα (%)";
                    wvm.BarCombineMode = Telerik.Charting.ChartSeriesCombineMode.Stack100;
                }
            }
        }

        #endregion

    }
}
