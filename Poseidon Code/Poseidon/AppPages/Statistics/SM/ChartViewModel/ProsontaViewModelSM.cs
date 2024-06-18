using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using Telerik.Charting;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Poseidon.Model;
using Poseidon.Utilities;

namespace Poseidon.AppPages.Statistics.SM.ChartViewModel
{
    class ProsontaViewModelSM : ViewModelBase
    {
        PoseidonDataContext db = new PoseidonDataContext();

        private List<ProsontaDataSM> _c1;
        private List<ProsontaDataSM> _c2;
        private List<ProsontaDataSM> _c3;

        private ChartPalette _palette;
        private List<ChartPalette> _palettes;
        private String _name;
        public List<String> _names;

        private ChartSeriesCombineMode _barCombineMode = ChartSeriesCombineMode.Cluster;
        private Orientation _chartOrientation = Orientation.Vertical;
        private bool _isShowLabelsEnabled = true;
        private bool _showLabels = false;

        private double _gapLength = 0.4d;
        private double _axisMaxValue = 10000d;
        private double _axisStep = 500d;
        private string _axisTitle = "";
        private string _axisLabelFormat = "N0";
        private GridLineVisibility _majorLinesVisibility = GridLineVisibility.Y;

        private static int _syear = 0;
        private static bool _msc = false;
        private static bool _phd = false;
        private static string _klados = null;
        private static double _count = 0;
        public ProsontaDataSM pd;

        public ProsontaViewModelSM()
        {
            this.InitializePalettePresets();
            this.InitializeComboNames();
            pd = new ProsontaDataSM(_syear, _msc, _phd, _klados, _count);
        }

        // Βασικό πτυχίο μόνο
        private void LoadData1()
        {

            if (pd.SchoolYear == 0)
            {
                var qry = from t in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                          where t.ΜΕΤΑΠΤΥΧΙΑΚΟ == false && t.ΔΙΔΑΚΤΟΡΙΚΟ == false
                          select t;

                var data_source = qry.ToList();

                this._c1 = new List<ProsontaDataSM>();
                foreach (var row in data_source)
                {
                    this._c1.Add(new ProsontaDataSM(0, (bool)row.ΜΕΤΑΠΤΥΧΙΑΚΟ, (bool)row.ΔΙΔΑΚΤΟΡΙΚΟ, row.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ, (double)row.ΠΛΗΘΟΣ));
                };
            }
            else if (pd.SchoolYear > 0)
            {
                var qry = from t in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                          where (t.ΜΕΤΑΠΤΥΧΙΑΚΟ == false && t.ΔΙΔΑΚΤΟΡΙΚΟ == false) && t.ΣΧΟΛΙΚΟ_ΕΤΟΣ == pd.SchoolYear
                          select t;

                var data_source = qry.ToList();

                this._c1 = new List<ProsontaDataSM>();
                foreach (var row in data_source)
                {
                    this._c1.Add(new ProsontaDataSM((int)row.ΣΧΟΛΙΚΟ_ΕΤΟΣ, (bool)row.ΜΕΤΑΠΤΥΧΙΑΚΟ, (bool)row.ΔΙΔΑΚΤΟΡΙΚΟ, 
                                 row.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ, (double)row.ΠΛΗΘΟΣ));
                };
            }
            C1 = _c1;
            AxisMaxScale();
        }   // LoadData1

        private void LoadData2()
        {
            if (pd.SchoolYear == 0)
            {
                var qry = from t in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                          where t.ΜΕΤΑΠΤΥΧΙΑΚΟ == true
                          select t;

                var data_source = qry.ToList();

                this._c2 = new List<ProsontaDataSM>();
                foreach (var row in data_source)
                {
                    this._c2.Add(new ProsontaDataSM(0, (bool)row.ΜΕΤΑΠΤΥΧΙΑΚΟ, (bool)row.ΔΙΔΑΚΤΟΡΙΚΟ, row.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ, (double)row.ΠΛΗΘΟΣ));
                };
            }
            else if (pd.SchoolYear > 0)
            {
                var qry = from t in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                          where (t.ΜΕΤΑΠΤΥΧΙΑΚΟ == true) && t.ΣΧΟΛΙΚΟ_ΕΤΟΣ == pd.SchoolYear
                          select t;

                var data_source = qry.ToList();

                this._c2 = new List<ProsontaDataSM>();
                foreach (var row in data_source)
                {
                    this._c2.Add(new ProsontaDataSM((int)row.ΣΧΟΛΙΚΟ_ΕΤΟΣ, (bool)row.ΜΕΤΑΠΤΥΧΙΑΚΟ, (bool)row.ΔΙΔΑΚΤΟΡΙΚΟ,
                                 row.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ, (double)row.ΠΛΗΘΟΣ));
                };
            }
            C2 = _c2;
            AxisMaxScale();
        }   // LoadData2

        private void LoadData3()
        {
            if (pd.SchoolYear == 0)
            {
                var qry = from t in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                          where t.ΔΙΔΑΚΤΟΡΙΚΟ == true
                          select t;

                var data_source = qry.ToList();

                this._c3 = new List<ProsontaDataSM>();
                foreach (var row in data_source)
                {
                    this._c3.Add(new ProsontaDataSM(0, (bool)row.ΜΕΤΑΠΤΥΧΙΑΚΟ, (bool)row.ΔΙΔΑΚΤΟΡΙΚΟ, row.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ, (double)row.ΠΛΗΘΟΣ));
                };
            }
            else if (pd.SchoolYear > 0)
            {
                var qry = from t in db.statΠΡΟΣΟΝΤΑ_ΣΜs
                          where (t.ΔΙΔΑΚΤΟΡΙΚΟ == true) && t.ΣΧΟΛΙΚΟ_ΕΤΟΣ == pd.SchoolYear
                          select t;

                var data_source = qry.ToList();

                this._c3 = new List<ProsontaDataSM>();
                foreach (var row in data_source)
                {
                    this._c3.Add(new ProsontaDataSM((int)row.ΣΧΟΛΙΚΟ_ΕΤΟΣ, (bool)row.ΜΕΤΑΠΤΥΧΙΑΚΟ, (bool)row.ΔΙΔΑΚΤΟΡΙΚΟ,
                                 row.ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ, (double)row.ΠΛΗΘΟΣ));
                };
            }
            C3 = _c3;
            AxisMaxScale();
        }   // LoadData3

        public void RefreshViewModel()
        {
            LoadData1();
            LoadData2();
            LoadData3();
        }

        private void AxisMaxScale()
        {
            double c1_max;
            double c2_max;
            double c3_max;

            if (C1.Count > 0) c1_max = Convert.ToDouble(C1.Max(x => x.Count));
            else c1_max = 0;

            if (C2.Count > 0) c2_max = Convert.ToDouble(C2.Max(x => x.Count));
            else c2_max = 0;

            if (C3.Count > 0) c3_max = Convert.ToDouble(C3.Max(x => x.Count));
            else c3_max = 0;


            double c_max = c1_max + c2_max + c3_max;
            int maxCount = 0;

            if (_barCombineMode == ChartSeriesCombineMode.Cluster)  // cluster
            {
                maxCount = UserFunctions.Max((int)c1_max, (int)c2_max, (int)c3_max);
                AxisMaxValue = (int)(maxCount + 0.25 * maxCount);

            }
            else if (_barCombineMode == ChartSeriesCombineMode.Stack) // stack
            {
                maxCount = (int)c_max;
                AxisMaxValue = (int)(maxCount + 0.25 * maxCount);
            }
            else if (_barCombineMode == ChartSeriesCombineMode.Stack100)
            {
                AxisMaxValue = 1d;

            }
        }

        #region Prosonta type Data

        public List<ProsontaDataSM> C1
        {
            get
            {
                if (this._c1 == null)
                {
                    LoadData1();
                }
                return this._c1;
            }
            set
            {
                this._c1 = value;
                OnPropertyChanged("C1");
            }
        }

        public List<ProsontaDataSM> C2
        {
            get
            {
                if (this._c2 == null)
                {
                    LoadData2();
                }
                return this._c2;
            }
            set
            {
                this._c2 = value;
                OnPropertyChanged("C2");
            }
        }

        public List<ProsontaDataSM> C3
        {
            get
            {
                if (this._c3 == null)
                {
                    LoadData3();
                }
                return this._c3;
            }
            set
            {
                this._c3 = value;
                OnPropertyChanged("C3");
            }
        }

        #endregion


        #region Chart Series Properties

        private void InitializePalettePresets()
        {
            List<ChartPalette> palettes = new List<ChartPalette>();
            palettes.Add(ChartPalettes.Arctic);
            palettes.Add(ChartPalettes.Autumn);
            palettes.Add(ChartPalettes.Cold);
            palettes.Add(ChartPalettes.Flower);
            palettes.Add(ChartPalettes.Forest);
            palettes.Add(ChartPalettes.Grayscale);
            palettes.Add(ChartPalettes.Ground);
            palettes.Add(ChartPalettes.Lilac);
            palettes.Add(ChartPalettes.Natural);
            palettes.Add(ChartPalettes.Pastel);
            palettes.Add(ChartPalettes.Rainbow);
            palettes.Add(ChartPalettes.Spring);
            palettes.Add(ChartPalettes.Summer);
            palettes.Add(ChartPalettes.Warm);
            palettes.Add(ChartPalettes.Windows8);

            this.Palettes = palettes;
            this.Palette = ChartPalettes.Windows8;
        }

        public void InitializeComboNames()
        {
            List<String> names = new List<String>();
            names.Add("Συστοιχία");
            names.Add("Στοίβα");
            names.Add("Στοίβα (%)");

            this.CombineModeNames = names;
            this.CombineModeName = "Συστοιχία";
        }


        public String CombineModeName
        {
            get
            {
                return this._name;
            }
            set
            {
                if (this._name != value)
                {
                    this._name = value;
                    this.OnPropertyChanged("CombineModeName");
                }
            }
        }

        public List<String> CombineModeNames
        {
            get
            {
                return this._names;
            }
            set
            {
                if (this._names != value)
                {
                    this._names = value;
                    this.OnPropertyChanged("CombineModeNames");
                }
            }
        }

        public ChartSeriesCombineMode ConvertNameToMode()
        {
            if (CombineModeName == "Συστοιχία")
            {
                _barCombineMode = ChartSeriesCombineMode.Cluster;
            }
            if (CombineModeName == "Στοίβα")
            {
                _barCombineMode = ChartSeriesCombineMode.Stack;
            }
            if (CombineModeName == "Στοίβα (%)")
            {
                _barCombineMode = ChartSeriesCombineMode.Stack100;
            }
            return _barCombineMode;
        }


        public ChartPalette Palette
        {
            get
            {
                return this._palette;
            }
            set
            {
                if (this._palette != value)
                {
                    this._palette = value;
                    this.OnPropertyChanged("Palette");
                }
            }
        }

        public List<ChartPalette> Palettes
        {
            get
            {
                return this._palettes;
            }
            set
            {
                if (this._palettes != value)
                {
                    this._palettes = value;
                    this.OnPropertyChanged("Palettes");
                }
            }
        }

        public ChartSeriesCombineMode BarCombineMode
        {
            get
            {
                this._barCombineMode = ConvertNameToMode();
                return this._barCombineMode;
            }
            set
            {
                if (this._barCombineMode != value)
                {
                    value = ConvertNameToMode();
                    this._barCombineMode = value;
                    this.OnPropertyChanged("BarCombineMode");

                    this.UpdateLabelsConfiguration(this._barCombineMode);
                    this.UpdateAxisConfiguration(this._barCombineMode);
                }
            }
        }

        public bool ShowLabels
        {
            get
            {
                return this._showLabels;
            }
            set
            {
                if (this._showLabels != value)
                {
                    this._showLabels = value;
                    this.OnPropertyChanged("ShowLabels");
                }
            }
        }

        public bool IsShowLabelsEnabled
        {
            get
            {
                return this._isShowLabelsEnabled;
            }
            set
            {
                if (this._isShowLabelsEnabled != value)
                {
                    this._isShowLabelsEnabled = value;
                    this.OnPropertyChanged("IsShowLabelsEnabled");
                }
            }
        }

        public Orientation ChartOrientation
        {
            get
            {
                return this._chartOrientation;
            }
            set
            {
                if (this._chartOrientation != value)
                {
                    this._chartOrientation = value;
                    this.OnPropertyChanged("ChartOrientation");

                    this.UpdateMajorLinesVisibility(this._chartOrientation);
                }
            }
        }

        public double GapLength
        {
            get
            {
                return this._gapLength;
            }
            set
            {
                if (this._gapLength != value)
                {
                    this._gapLength = value;
                    this.OnPropertyChanged("GapLength");
                }
            }
        }

        public double AxisMaxValue
        {
            get
            {
                return this._axisMaxValue;
            }
            set
            {
                if (this._axisMaxValue != value)
                {
                    this._axisMaxValue = value;
                    this.OnPropertyChanged("AxisMaxValue");
                }
            }
        }

        public double AxisStep
        {
            get
            {
                return this._axisStep;
            }
            set
            {
                if (this._axisStep != value)
                {
                    this._axisStep = value;
                    this.OnPropertyChanged("AxisStep");
                }
            }
        }

        public string AxisTitle
        {
            get
            {
                return this._axisTitle;
            }
            set
            {
                if (this._axisTitle != value)
                {
                    this._axisTitle = value;
                    this.OnPropertyChanged("AxisTitle");
                }
            }
        }

        public string AxisLabelFormat
        {
            get
            {
                return this._axisLabelFormat;
            }
            set
            {
                if (this._axisLabelFormat != value)
                {
                    this._axisLabelFormat = value;
                    this.OnPropertyChanged("AxisLabelFormat");
                }
            }
        }

        #region Chart configuration

        public GridLineVisibility MajorLinesVisibility
        {
            get
            {
                return this._majorLinesVisibility;
            }
            set
            {
                if (this._majorLinesVisibility != value)
                {
                    this._majorLinesVisibility = value;
                    this.OnPropertyChanged("MajorLinesVisibility");
                }
            }
        }

        private void UpdateMajorLinesVisibility(Orientation chartOrientation)
        {
            if (chartOrientation == Orientation.Horizontal)
                this.MajorLinesVisibility = GridLineVisibility.X;
            else
                this.MajorLinesVisibility = GridLineVisibility.Y;
        }

        private void UpdateLabelsConfiguration(ChartSeriesCombineMode mode)
        {
            this.ShowLabels = false;
            this.IsShowLabelsEnabled = mode == ChartSeriesCombineMode.Cluster;
        }

        private void UpdateAxisConfiguration(ChartSeriesCombineMode mode)
        {
            if (mode == ChartSeriesCombineMode.Cluster)
            {
                this.GapLength = 0.4d;
                AxisMaxScale();
                //this.AxisMaxValue = 20000d;
                //this.AxisStep = 5000d;

                this.AxisTitle = "ΠΛΗΘΟΣ ΑΝΑ ΠΤΥΧΙΟ ΚΑΙ ΚΛΑΔΟ";
                this.AxisLabelFormat = "N0";
            }
            else if (mode == ChartSeriesCombineMode.Stack)
            {
                this.GapLength = 0.5d;
                AxisMaxScale();
                //this.AxisMaxValue = 70000d;
                //this.AxisStep = 16500d;

                this.AxisTitle = "ΠΛΗΘΟΣ ΑΝΑ ΠΤΥΧΙΟ ΚΑΙ ΚΛΑΔΟ";
                this.AxisLabelFormat = "N0";
            }
            else if (mode == ChartSeriesCombineMode.Stack100)
            {
                this.GapLength = 0.5d;
                AxisMaxScale();

                //this.AxisMaxValue = 1d;
                //this.AxisStep = 0.25d;

                this.AxisTitle = "ΠΛΗΘΟΣ ΑΝΑ ΠΤΥΧΙΟ ΚΑΙ ΚΛΑΔΟ (%)";
                this.AxisLabelFormat = "P0";
            }
        }

        #endregion

        #endregion


    }   // class ProsontaViewModelSM
}
