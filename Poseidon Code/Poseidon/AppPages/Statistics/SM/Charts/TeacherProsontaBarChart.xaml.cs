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
using Poseidon.AppPages.Statistics.SM.ChartViewModel;
using Poseidon.Controls;
using Poseidon.Model;
using Poseidon.Utilities;

namespace Poseidon.AppPages.Statistics.SM.Charts
{
    /// <summary>
    /// Interaction logic for TeacherProsontaBarChart.xaml
    /// </summary>
    public partial class TeacherProsontaBarChart : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private static int _syear = 0;
        private static bool _msc = false;
        private static bool _phd = false;
        private static string _klados = null;
        private static double _count = 0;

        private ProsontaDataSM pd = new ProsontaDataSM(_syear, _msc, _phd,_klados, _count);

        public TeacherProsontaBarChart()
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

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            ΣΧΟΛΙΚΟ_ΕΤΟΣ syear = cboSchoolYearSelection.SelectedItem as ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            ProsontaViewModelSM pvm = DataContext as ProsontaViewModelSM;

            if (syear == null)
            {
                pvm.pd.SchoolYear = 0;
                pvm.RefreshViewModel();
            }
            else if (syear != null)
            {
                int selected_syear = syear.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ;
                pvm.pd.SchoolYear = selected_syear;
                pvm.RefreshViewModel();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cboSchoolYearSelection.SelectedIndex = -1;

            ProsontaViewModelSM pvm = DataContext as ProsontaViewModelSM;
            pvm.pd.SchoolYear = 0;
            pvm.RefreshViewModel();
        }

        private void cboCombineMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProsontaViewModelSM cvm = DataContext as ProsontaViewModelSM;

            if (cboCombineMode.SelectedValue != null)
            {
                int index = cboCombineMode.SelectedIndex;

                if (index == 0)
                {
                    cboCombineMode.SelectedValue = "Συστοιχία";
                    cvm.BarCombineMode = Telerik.Charting.ChartSeriesCombineMode.Cluster;
                }
                if (index == 1)
                {
                    cboCombineMode.SelectedValue = "Στοίβα";
                    cvm.BarCombineMode = Telerik.Charting.ChartSeriesCombineMode.Stack;
                }
                if (index == 2)
                {
                    cboCombineMode.SelectedValue = "Στοίβα (%)";
                    cvm.BarCombineMode = Telerik.Charting.ChartSeriesCombineMode.Stack100;
                }
            }
        }


    }   // class TeacherProsontaBarChart
}
