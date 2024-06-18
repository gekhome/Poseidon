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

namespace Poseidon.AppPages.Statistics.SP.Charts
{
    /// <summary>
    /// Interaction logic for StudentGenderPieChart.xaml
    /// </summary>
    public partial class StudentGenderPieChart : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public StudentGenderPieChart()
        {
            InitializeComponent();
            SetPieSeries();
            LoadData();
        }

        private void LoadData()
        {
            var students = from s in db.statΣΠΟΥΔΑΣΤΗΣ_ΦΥΛΟ_ΣΠs
                           select s;
            RadChart1.ItemsSource = students.ToList();
        }

        private void SetPieSeries()
        {
            RadChart1.SeriesMappings.Clear();
            SeriesMapping seriesMapping = new SeriesMapping();
            seriesMapping.SeriesDefinition = new PieSeriesDefinition();
            ItemMapping itemMapping = new ItemMapping();
            itemMapping.DataPointMember = DataPointMember.YValue;
            itemMapping.FieldName = "ΠΛΗΘΟΣ";
            seriesMapping.ItemMappings.Add(itemMapping);

            itemMapping = new ItemMapping();
            itemMapping.DataPointMember = DataPointMember.LegendLabel;
            itemMapping.FieldName = "ΦΥΛΟ_ΛΕΚΤΙΚΟ";
            seriesMapping.ItemMappings.Add(itemMapping);

            RadChart1.SeriesMappings.Add(seriesMapping);

        }
    }
}
