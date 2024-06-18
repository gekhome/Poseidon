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

namespace Poseidon.AppPages.Statistics.SM.Pages
{
    /// <summary>
    /// InOTraction logic for StudEUtNationalityCount.xaml
    /// </summary>
    public partial class StudentNationalityCount : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        int GRCount, EUCount, OTCount, TotalCount;

        public StudentNationalityCount()
        {
            InitializeComponent();
            LoadGauge1Data();
        }

        public void LoadGauge1Data()
        {
            var qryGR = (from GR in db.statΣΠΟΥΔΑΣΤΗΣ_ΥΠΗΚΟΟΤΗΤΑ_ΣΜs
                         where GR.ΥΠΗΚΟΟΤΗΤΑ == "ΕΛΛΗΝΙΚΗ"
                         select new { GR.ΠΛΗΘΟΣ }).FirstOrDefault();

            var qryEU = (from EU in db.statΣΠΟΥΔΑΣΤΗΣ_ΥΠΗΚΟΟΤΗΤΑ_ΣΜs
                         where EU.ΥΠΗΚΟΟΤΗΤΑ == "ΕΥΡΩΠΑΪΚΗ ΕΝΩΣΗ"
                         select new { EU.ΠΛΗΘΟΣ }).FirstOrDefault();

            var qryOTHER = (from OT in db.statΣΠΟΥΔΑΣΤΗΣ_ΥΠΗΚΟΟΤΗΤΑ_ΣΜs
                         where OT.ΥΠΗΚΟΟΤΗΤΑ == "ΑΛΛΗ"
                         select new { OT.ΠΛΗΘΟΣ }).FirstOrDefault();

            // absoluOT values of data
            if (qryGR == null) GRCount = 0;
            else GRCount = (int)qryGR.ΠΛΗΘΟΣ;

            if (qryEU == null) EUCount = 0;
            else EUCount = (int)qryEU.ΠΛΗΘΟΣ;

            if (qryOTHER == null) OTCount = 0;
            else OTCount = (int)qryOTHER.ΠΛΗΘΟΣ;
            TotalCount = GRCount + EUCount + OTCount;

            // GRrcEUtage values of data
            double GRN = (double)GRCount;
            double EUN = (double)EUCount;
            double OTU = (double)OTCount;
            double N = (double)TotalCount;

            double p = (100.0 * GRN) / N;
            double e = (100.0 * EUN) / N;
            double t = (100.0 * OTU) / N;

            // set needle values
            needleGR.Value = p; // (double)GRCount;
            needleEU.Value = e; // (double)EUCount;
            needleOT.Value = t; // (double)OTCount;


            radialScaleGR.Min = 0;
            radialScaleGR.Max = 100;    // TotalCount;

            radialScaleEU.Min = 0;
            radialScaleEU.Max = 100;    // TotalCount;

            radialScaleOT.Min = 0;
            radialScaleOT.Max = 100;    // TotalCount;

            radialGaugeGR.ToolTip = String.Format("Πλήθος {0}, από {1}", GRCount, TotalCount);
            radialGaugeEU.ToolTip = String.Format("Πλήθος {0}, από {1}", EUCount, TotalCount);
            radialGaugeOT.ToolTip = String.Format("Πλήθος {0}, από {1}", OTCount, TotalCount);
        } // LoadGaugeData



    }
}
