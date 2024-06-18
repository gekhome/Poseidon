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

namespace Poseidon.AppPages.Statistics.SM.Pages
{
    /// <summary>
    /// Interaction logic for TeacherPostGradStudies.xaml
    /// </summary>
    public partial class TeacherPostGradStudies : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        int PE1Count, PE2Count, PE3Count, PECount;
        int EN1Count, EN2Count, EN3Count, ENCount;


        public TeacherPostGradStudies()
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

            // Load Gauge data
            LoadGauge1Data();
            LoadGauge2Data();

        } // LoadData

        #region Gauge1 set

        public void LoadGauge1Data()
        {
            var qryPE1 = (from pe1 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe1.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ" && (pe1.ΜΕΤΑΠΤΥΧΙΑΚΟ == false && pe1.ΔΙΔΑΚΤΟΡΙΚΟ == false)
                         select new { pe1.ΠΛΗΘΟΣ }).FirstOrDefault();

            var qryPE2 = (from pe2 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe2.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ" && (pe2.ΜΕΤΑΠΤΥΧΙΑΚΟ == true)
                         select new { pe2.ΠΛΗΘΟΣ }).FirstOrDefault();

            var qryPE3 = (from pe3 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe3.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ" && (pe3.ΔΙΔΑΚΤΟΡΙΚΟ == true)
                         select new { pe3.ΠΛΗΘΟΣ }).FirstOrDefault();

            // absolute values of data
            if (qryPE1 == null) PE1Count = 0;
            else PE1Count = (int)qryPE1.ΠΛΗΘΟΣ;

            if (qryPE2 == null) PE2Count = 0;
            else PE2Count = (int)qryPE2.ΠΛΗΘΟΣ;

            if (qryPE3 == null) PE3Count = 0;
            else PE3Count = (int)qryPE3.ΠΛΗΘΟΣ;

            PECount = PE1Count + PE2Count + PE3Count;

            SetGauge1Data();

        } // LoadGauge1Data

        public void LoadGauge1Data(int school_year)
        {
            var qryPE1 = (from pe1 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe1.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ" && (pe1.ΜΕΤΑΠΤΥΧΙΑΚΟ == false && pe1.ΔΙΔΑΚΤΟΡΙΚΟ == false)
                         && pe1.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                         select new {pe1.ΠΛΗΘΟΣ}).FirstOrDefault();

            var qryPE2 = (from pe2 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe2.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ" && (pe2.ΜΕΤΑΠΤΥΧΙΑΚΟ == true)
                         && pe2.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                         select new {pe2.ΠΛΗΘΟΣ}).FirstOrDefault();

            var qryPE3 = (from pe3 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe3.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΠΕ" && (pe3.ΔΙΔΑΚΤΟΡΙΚΟ == true)
                         && pe3.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                         select new {pe3.ΠΛΗΘΟΣ}).FirstOrDefault();

            // absolute values of data
            // absolute values of data
            if (qryPE1 == null) PE1Count = 0;
            else PE1Count = (int)qryPE1.ΠΛΗΘΟΣ;

            if (qryPE2 == null) PE2Count = 0;
            else PE2Count = (int)qryPE2.ΠΛΗΘΟΣ;

            if (qryPE3 == null) PE3Count = 0;
            else PE3Count = (int)qryPE3.ΠΛΗΘΟΣ;
            
            PECount = PE1Count + PE2Count + PE3Count;

            SetGauge1Data();

        }

        public void SetGauge1Data()
        {
            // percentage values of data
            double pe1N = (double)PE1Count;
            double pe2N = (double)PE2Count;
            double pe3N = (double)PE3Count;
            double N = (double)PECount;

            double p1 = (100.0 * pe1N) / N;
            double p2 = (100.0 * pe2N) / N;
            double p3 = (100.0 * pe3N) / N;

            // set the values of the gauges
            N = (double)PECount;

            p1 = (100.0 * pe1N) / N;
            p2 = (100.0 * pe2N) / N;
            p3 = (100.0 * pe3N) / N;

            SetGauge1Values(p1, p2, p3);     // set needle values

        } // SetGaugeAndPanelData

        public void SetGauge1Values(double p1, double p2, double p3)
        {
            needlePE1.Value = p1;
            needlePE2.Value = p2;
            needlePE3.Value = p3;
            radGaugePE1.ToolTip = String.Format("Πλήθος {0}, από {1}", PE1Count, PECount);
            radGaugePE2.ToolTip = String.Format("Πλήθος {0}, από {1}", PE2Count, PECount);
            radGaugePE3.ToolTip = String.Format("Πλήθος {0}, από {1}", PE3Count, PECount);
        }

        public void ClearGauge1Values()
        {
            needlePE1.Value = 0;
            needlePE2.Value = 0;
            needlePE3.Value = 0;
            radGaugePE1.ToolTip = String.Format("Πλήθος {0}, από {1}", 0, 0);
            radGaugePE2.ToolTip = String.Format("Πλήθος {0}, από {1}", 0, 0);
            radGaugePE3.ToolTip = String.Format("Πλήθος {0}, από {1}", 0, 0);
        }

        #endregion

        #region Gauge2 set

        public void LoadGauge2Data()
        {
            var qryEN1 = (from pe1 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe1.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ" && (pe1.ΜΕΤΑΠΤΥΧΙΑΚΟ == false && pe1.ΔΙΔΑΚΤΟΡΙΚΟ == false)
                         select new { pe1.ΠΛΗΘΟΣ }).FirstOrDefault();

            var qryEN2 = (from pe2 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe2.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ" && (pe2.ΜΕΤΑΠΤΥΧΙΑΚΟ == true)
                          select new { pe2.ΠΛΗΘΟΣ }).FirstOrDefault();

            var qryEN3 = (from pe3 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe3.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ" && (pe3.ΔΙΔΑΚΤΟΡΙΚΟ == true)
                         select new { pe3.ΠΛΗΘΟΣ }).FirstOrDefault();

            // absolute values of data
            if (qryEN1 == null) EN1Count = 0;
            else EN1Count = (int)qryEN1.ΠΛΗΘΟΣ;

            if (qryEN2 == null) EN2Count = 0;
            else EN2Count = (int)qryEN2.ΠΛΗΘΟΣ;

            if (qryEN3 == null) PE3Count = 0;
            else EN3Count = (int)qryEN3.ΠΛΗΘΟΣ;
            ENCount = EN1Count + EN2Count + EN3Count;

            SetGauge2Data();

        } // LoadGauge1Data

        public void LoadGauge2Data(int school_year)
        {
            var qryEN1 = (from pe1 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe1.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ" && (pe1.ΜΕΤΑΠΤΥΧΙΑΚΟ == false && pe1.ΔΙΔΑΚΤΟΡΙΚΟ == false)
                         && pe1.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                         select new { pe1.ΠΛΗΘΟΣ }).FirstOrDefault();

            var qryEN2 = (from pe2 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe2.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ" && (pe2.ΜΕΤΑΠΤΥΧΙΑΚΟ == true)
                         && pe2.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                         select new { pe2.ΠΛΗΘΟΣ }).FirstOrDefault();

            var qryEN3 = (from pe3 in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                         where pe3.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ == "ΕΝ" && (pe3.ΔΙΔΑΚΤΟΡΙΚΟ == true)
                         && pe3.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                         select new { pe3.ΠΛΗΘΟΣ }).FirstOrDefault();

            // absolute values of data
            // absolute values of data
            if (qryEN1 == null) EN1Count = 0;
            else EN1Count = (int)qryEN1.ΠΛΗΘΟΣ;

            if (qryEN2 == null) EN2Count = 0;
            else EN2Count = (int)qryEN2.ΠΛΗΘΟΣ;

            if (qryEN3 == null) PE3Count = 0;
            else EN3Count = (int)qryEN3.ΠΛΗΘΟΣ;

            ENCount = EN1Count + EN2Count + EN3Count;

            SetGauge2Data();

        }

        public void SetGauge2Data()
        {
            // percentage values of data
            double pe1N = (double)EN1Count;
            double pe2N = (double)EN2Count;
            double pe3N = (double)EN3Count;
            double N = (double)ENCount;

            double p1 = (100.0 * pe1N) / N;
            double p2 = (100.0 * pe2N) / N;
            double p3 = (100.0 * pe3N) / N;

            // set the values of the gauges
            N = (double)ENCount;

            p1 = (100.0 * pe1N) / N;
            p2 = (100.0 * pe2N) / N;
            p3 = (100.0 * pe3N) / N;

            SetGauge2Values(p1, p2, p3);     // set needle values

        } // SetGaugeAndPanelData

        public void SetGauge2Values(double p1, double p2, double p3)
        {
            needleEN1.Value = p1;
            needleEN2.Value = p2;
            needleEN3.Value = p3;
            radGaugeEN1.ToolTip = String.Format("Πλήθος {0}, από {1}", EN1Count, ENCount);
            radGaugeEN2.ToolTip = String.Format("Πλήθος {0}, από {1}", EN2Count, ENCount);
            radGaugeEN3.ToolTip = String.Format("Πλήθος {0}, από {1}", EN3Count, ENCount);
        }

        public void ClearGauge2Values()
        {
            needleEN1.Value = 0;
            needleEN2.Value = 0;
            needleEN3.Value = 0;
            radGaugeEN1.ToolTip = String.Format("Πλήθος {0}, από {1}", 0, 0);
            radGaugeEN2.ToolTip = String.Format("Πλήθος {0}, από {1}", 0, 0);
            radGaugeEN3.ToolTip = String.Format("Πλήθος {0}, από {1}", 0, 0);
        }

        #endregion

        #region Button Filter events

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            ΣΧΟΛΙΚΟ_ΕΤΟΣ syear = cboSchoolYearSelection.SelectedItem as ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            if (syear != null)
            {
                int selected_syear = syear.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ;
                LoadGauge1Data(selected_syear);
                LoadGauge2Data(selected_syear);
            }
            else if (syear == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε και προκήρυξη.");
                return;
            }

        }   //btnView_Click

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cboSchoolYearSelection.SelectedIndex = -1;

            LoadData();
        }

        #endregion


    }   // class TeacherPostGradStudies
}
