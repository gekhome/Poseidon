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
using Poseidon.AppPages.Statistics.ALL.ChartViewModel;
using Poseidon.Controls;
using Poseidon.Model;
using Poseidon.Utilities;

namespace Poseidon.AppPages.Statistics.ALL.Charts
{
    /// <summary>
    /// Interaction logic for TeacherGenderBarChart.xaml
    /// </summary>
    public partial class TeacherGenderBarChart : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private static int _syear = 0;
        private static string _gender = null;
        private static string _klados = null;
        private static double _count = 0;

        private GenderDataALL gd = new GenderDataALL(_syear, _gender, _klados, _count);
        public ObservableCollection<string> names = new ObservableCollection<string>();

        public TeacherGenderBarChart()
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
            GenderViewModelALL gvm = DataContext as GenderViewModelALL;

            if (syear == null)
            {
                gvm.gd.SchoolYear = 0;
                gvm.RefreshViewModel();
            }
            else if (syear != null)
            {
                int selected_syear = syear.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ;
                gvm.gd.SchoolYear = selected_syear;
                gvm.RefreshViewModel();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cboSchoolYearSelection.SelectedIndex = -1;

            GenderViewModelALL gvm = DataContext as GenderViewModelALL;
            gvm.gd.SchoolYear = 0;
            gvm.RefreshViewModel();
        }

        private void cboCombineMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenderViewModelALL gvm = DataContext as GenderViewModelALL;

            if (cboCombineMode.SelectedValue != null)
            {
                int index = cboCombineMode.SelectedIndex;

                if (index == 0)
                {
                    cboCombineMode.SelectedValue = "Συστοιχία";
                    gvm.BarCombineMode = Telerik.Charting.ChartSeriesCombineMode.Cluster;
                }
                if (index == 1)
                {
                    cboCombineMode.SelectedValue = "Στοίβα";
                    gvm.BarCombineMode = Telerik.Charting.ChartSeriesCombineMode.Stack;
                }
                if (index == 2)
                {
                    cboCombineMode.SelectedValue = "Στοίβα (%)";
                    gvm.BarCombineMode = Telerik.Charting.ChartSeriesCombineMode.Stack100;
                }
            }
        }

        #endregion

    }   // class TeacherGenderBarChart
}
