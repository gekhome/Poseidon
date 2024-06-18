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

namespace Poseidon.AppPages.Statistics.ALL.Pages
{
    /// <summary>
    /// Interaction logic for TeacherKladosCount.xaml
    /// </summary>
    public partial class TeacherKladosCount : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        int PECount, ENCount, TECount, TotalCount;

        public TeacherKladosCount()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {

            // Load school year combo
            var syears = from sy in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         orderby sy.ΣΧΟΛ_ΕΤΟΣ descending
                         select sy;

            cboSchoolYearSelection.ItemsSource = syears.ToList();

            // Load datagrid
            //LoadGrid();
            // Load Gauge data
            LoadGauge1Data();
            LoadGauge2Data();

        } // LoadData

        public void LoadGauge1Data()
        {
            var qryPE = (from pe in db.statΑΝΑΘΕΣΕΙΣ_ΣΠ_ΣΜs
                        where pe.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ"
                        select new { pe.TOTAL }).FirstOrDefault();

            var qryEN = (from en in db.statΑΝΑΘΕΣΕΙΣ_ΣΠ_ΣΜs
                        where en.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ"
                         select new { en.TOTAL }).FirstOrDefault();

            var qryTE = (from te in db.statΑΝΑΘΕΣΕΙΣ_ΣΠ_ΣΜs
                        where te.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΤΕ"
                        select new { te.TOTAL }).FirstOrDefault();

            // absolute values of data
            if (qryPE == null) PECount = 0;
            else PECount = (int)qryPE.TOTAL;

            if (qryEN == null) ENCount = 0;
            else ENCount = (int)qryEN.TOTAL;

            if (qryTE == null) TECount = 0;
            else TECount = (int)qryTE.TOTAL;
            TotalCount = PECount + ENCount + TECount;

            // percentage values of data
            double peN = (double)PECount;
            double enN = (double)ENCount;
            double teN = (double)TECount;
            double N = (double)TotalCount;

            double p = (100.0 * peN) / N;
            double e = (100.0 * enN) / N;
            double t = (100.0 * teN) / N;

            // set needle values
            needlePE.Value = p; // (double)PECount;
            needleEN.Value = e; // (double)ENCount;
            needleTE.Value = t; // (double)TECount;


            radialScalePE.Min = 0;
            radialScalePE.Max = 100;    // TotalCount;

            radialScaleEN.Min = 0;
            radialScaleEN.Max = 100;    // TotalCount;

            radialScaleTE.Min = 0;
            radialScaleTE.Max = 100;    // TotalCount;

            radialGaugePE.ToolTip = String.Format("Πλήθος {0}, από {1}", PECount, TotalCount);
            radialGaugeEN.ToolTip = String.Format("Πλήθος {0}, από {1}", ENCount, TotalCount);
            radialGaugeTE.ToolTip = String.Format("Πλήθος {0}, από {1}", TECount, TotalCount);
        } // LoadGaugeData

        public void LoadGauge1Data(int school_year)
        {
            //&& pe.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year

            var qryPE = (from pe in db.statΑΝΑΘΕΣΕΙΣ_ΣΠ_ΣΜs
                         where pe.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ" && pe.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                         select new { pe.TOTAL }).FirstOrDefault();

            var qryEN = (from en in db.statΑΝΑΘΕΣΕΙΣ_ΣΠ_ΣΜs
                         where en.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ" && en.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                         select new { en.TOTAL }).FirstOrDefault();

            var qryTE = (from te in db.statΑΝΑΘΕΣΕΙΣ_ΣΠ_ΣΜs
                         where te.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΤΕ" && te.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                         select new { te.TOTAL }).FirstOrDefault();

            // absolute values of data
            if(qryPE == null) PECount = 0;
            else PECount = (int)qryPE.TOTAL;

            if (qryEN == null) ENCount = 0;
            else ENCount = (int)qryEN.TOTAL;

            if (qryTE == null) TECount = 0;
            else TECount = (int)qryTE.TOTAL;

            TotalCount = PECount + ENCount + TECount;

            // percentage values of data
            double peN = (double)PECount;
            double enN = (double)ENCount;
            double teN = (double)TECount;
            double N = (double)TotalCount;

            double p = (100.0 * peN) / N;
            double e = (100.0 * enN) / N;
            double t = (100.0 * teN) / N;

            // set needle values
            needlePE.Value = p; // (double)PECount;
            needleEN.Value = e; // (double)TECount;
            needleTE.Value = t; // (double)DECount;


            radialScalePE.Min = 0;
            radialScalePE.Max = 100;    // TotalCount;

            radialScaleEN.Min = 0;
            radialScaleTE.Max = 100;    // TotalCount;

            radialScaleTE.Min = 0;
            radialScaleTE.Max = 100;    // TotalCount;

            radialGaugePE.ToolTip = String.Format("Πλήθος {0}, από {1}", PECount, TotalCount);
            radialGaugeEN.ToolTip = String.Format("Πλήθος {0}, από {1}", ENCount, TotalCount);
            radialGaugeTE.ToolTip = String.Format("Πλήθος {0}, από {1}", TECount, TotalCount);
        }

        public void LoadGauge2Data()
        {
            var qryPE_hours = (from pe in db.statΑΝΑΘΕΣΕΙΣ_ΩΡΕΣ_ΣΠ_ΣΜs
                               where pe.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ"
                               group pe by pe.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ into g
                               select new { SumHours = g.Sum(x => x.ΩΡΕΣ_ΣΥΝΟΛΟ) }).FirstOrDefault();

            var qryEN_hours = (from en in db.statΑΝΑΘΕΣΕΙΣ_ΩΡΕΣ_ΣΠ_ΣΜs
                               where en.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ"
                               group en by en.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ into g
                               select new { SumHours = g.Sum(x => x.ΩΡΕΣ_ΣΥΝΟΛΟ) }).FirstOrDefault();

            var qryTE_hours = (from te in db.statΑΝΑΘΕΣΕΙΣ_ΩΡΕΣ_ΣΠ_ΣΜs
                               where te.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΤΕ"
                               group te by te.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ into g
                               select new { SumHours = g.Sum(x => x.ΩΡΕΣ_ΣΥΝΟΛΟ) }).FirstOrDefault();


            // initialize local variables that hold query results
            int pe_total_hours = 0;
            int en_total_hours = 0;
            int te_total_hours = 0;
            int TotalHours = 0;

            // percentage values of data
            double peN = 0;
            double enN = 0;
            double teN = 0;
            double N = 0;

            double p = 0;
            double e = 0;
            double t = 0;

            // handle null values

            // κατηγορία ΠΕ
            if (qryPE_hours != null) pe_total_hours = Convert.ToInt32(qryPE_hours.SumHours); 

            // κατηγορία EN
            if (qryEN_hours != null) en_total_hours = Convert.ToInt32(qryEN_hours.SumHours);

            // κατηγορία TΕ
            if (qryTE_hours != null) te_total_hours = Convert.ToInt32(qryTE_hours.SumHours);

            // set the values of the gauges
            TotalHours = pe_total_hours + en_total_hours + te_total_hours;

            peN = (double)pe_total_hours;
            N = (double)TotalHours;
            p = (100.0 * peN) / N;

            enN = (double)en_total_hours;
            N = (double)TotalHours;
            e = (100.0 * enN) / N;

            teN = (double)te_total_hours;
            N = (double)TotalHours;
            t = (100.0 * teN) / N;

            gauge1_marker.Value = p;
            gauge2_marker.Value = e;
            gauge3_marker.Value = t;

            radGaugePE.ToolTip = String.Format("Πλήθος {0}, από {1}", pe_total_hours, TotalHours);
            radGaugeEN.ToolTip = String.Format("Πλήθος {0}, από {1}", en_total_hours, TotalHours);
            radGaugeTE.ToolTip = String.Format("Πλήθος {0}, από {1}", te_total_hours, TotalHours);

        } // LoadGauge2Data

        public void LoadGauge2Data(int school_year)
        {
            var qryPE_hours = (from pe in db.statΑΝΑΘΕΣΕΙΣ_ΩΡΕΣ_ΣΠ_ΣΜs
                               where pe.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ" && pe.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                               group pe by pe.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ into g
                               select new { SumHours = g.Sum(x => x.ΩΡΕΣ_ΣΥΝΟΛΟ) }).FirstOrDefault();

            var qryEN_hours = (from en in db.statΑΝΑΘΕΣΕΙΣ_ΩΡΕΣ_ΣΠ_ΣΜs
                               where en.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ" && en.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                               group en by en.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ into g
                               select new { SumHours = g.Sum(x => x.ΩΡΕΣ_ΣΥΝΟΛΟ) }).FirstOrDefault();

            var qryTE_hours = (from te in db.statΑΝΑΘΕΣΕΙΣ_ΩΡΕΣ_ΣΠ_ΣΜs
                               where te.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΤΕ" && te.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                               group te by te.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ into g
                               select new { SumHours = g.Sum(x => x.ΩΡΕΣ_ΣΥΝΟΛΟ) }).FirstOrDefault();


            // initialize local variables that hold query results
            int pe_total_hours = 0;
            int en_total_hours = 0;
            int te_total_hours = 0;
            int TotalHours = 0;

            // percentage values of data
            double peN = 0;
            double enN = 0;
            double teN = 0;
            double N = 0;

            double p = 0;
            double e = 0;
            double t = 0;

            // handle null values

            // κατηγορία ΠΕ
            if (qryPE_hours != null) pe_total_hours = Convert.ToInt32(qryPE_hours.SumHours);

            // κατηγορία EN
            if (qryEN_hours != null) en_total_hours = Convert.ToInt32(qryEN_hours.SumHours);

            // κατηγορία TΕ
            if (qryTE_hours != null) te_total_hours = Convert.ToInt32(qryTE_hours.SumHours);

            // set the values of the gauges
            TotalHours = pe_total_hours + en_total_hours + te_total_hours;

            peN = (double)pe_total_hours;
            N = (double)TotalHours;
            p = (100.0 * peN) / N;

            enN = (double)en_total_hours;
            N = (double)TotalHours;
            e = (100.0 * enN) / N;

            teN = (double)te_total_hours;
            N = (double)TotalHours;
            t = (100.0 * teN) / N;

            gauge1_marker.Value = p;
            gauge2_marker.Value = e;
            gauge3_marker.Value = t;

            radGaugePE.ToolTip = String.Format("Πλήθος {0}, από {1}", pe_total_hours, TotalHours);
            radGaugeEN.ToolTip = String.Format("Πλήθος {0}, από {1}", en_total_hours, TotalHours);
            radGaugeTE.ToolTip = String.Format("Πλήθος {0}, από {1}", te_total_hours, TotalHours);

        } // LoadGauge2Data


        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            ΣΧΟΛΙΚΟ_ΕΤΟΣ school_year = cboSchoolYearSelection.SelectedItem as ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            if (school_year == null) return;

            int selected_school_year = school_year.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ;
            LoadGauge1Data(selected_school_year);
            LoadGauge2Data(selected_school_year);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cboSchoolYearSelection.SelectedValue = -1;
            LoadData();
        }


    }   // class TeacherKladosCount
}
