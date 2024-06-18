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

namespace Poseidon.AppPages.Statistics.ALL.ChartViewModel
{
    class StudentNationalityVM_ALL : ViewModelBase
    {
        PoseidonDataContext db = new PoseidonDataContext();

        private List<StudentNationalityALL> _n1;
        private List<StudentNationalityALL> _n2;
        private List<StudentNationalityALL> _n3;

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

        private static string _nationality = null;
        private static double _count = 0;
        public StudentNationalityALL nd;

        public StudentNationalityVM_ALL()
        {
            this.InitializePalettePresets();
            this.InitializeComboNames();
            nd = new StudentNationalityALL(_nationality, _count);
        }

        public void LoadData1()
        {
            var qry = from t in db.statΣΠΟΥΔΑΣΤΗΣ_ΥΠΗΚΟΟΤΗΤΑs
                      where t.ΥΠΗΚΟΟΤΗΤΑ == "ΕΛΛΗΝΙΚΗ"
                      select t;

            var data_source = qry.ToList();

            this._n1 = new List<StudentNationalityALL>();
            foreach (var row in data_source)
            {
                this._n1.Add(new StudentNationalityALL(row.ΥΠΗΚΟΟΤΗΤΑ, (double)row.ΠΛΗΘΟΣ));
            };

            N1 = _n1;
            AxisMaxScale();
        }

        public void LoadData2()
        {
            var qry = from t in db.statΣΠΟΥΔΑΣΤΗΣ_ΥΠΗΚΟΟΤΗΤΑs
                      where t.ΥΠΗΚΟΟΤΗΤΑ == "ΕΥΡΩΠΑΪΚΗ ΕΝΩΣΗ"
                      select t;

            var data_source = qry.ToList();

            this._n2 = new List<StudentNationalityALL>();
            foreach (var row in data_source)
            {
                this._n2.Add(new StudentNationalityALL(row.ΥΠΗΚΟΟΤΗΤΑ, (double)row.ΠΛΗΘΟΣ));
            };

            N2 = _n2;
            AxisMaxScale();
        }

        public void LoadData3()
        {
            var qry = from t in db.statΣΠΟΥΔΑΣΤΗΣ_ΥΠΗΚΟΟΤΗΤΑs
                      where t.ΥΠΗΚΟΟΤΗΤΑ == "ΑΛΛΗ"
                      select t;

            var data_source = qry.ToList();

            this._n3 = new List<StudentNationalityALL>();
            foreach (var row in data_source)
            {
                this._n3.Add(new StudentNationalityALL(row.ΥΠΗΚΟΟΤΗΤΑ, (double)row.ΠΛΗΘΟΣ));
            };

            N3 = _n3;
            AxisMaxScale();
        }

        public void RefreshViewModel()
        {
            LoadData1();
            LoadData2();
            LoadData3();
        }

        #region Category type Data

        public List<StudentNationalityALL> N1
        {
            get
            {
                if (this._n1 == null)
                {
                    LoadData1();
                }
                return this._n1;
            }
            set
            {
                this._n1 = value;
                OnPropertyChanged("N1");
            }
        }

        public List<StudentNationalityALL> N2
        {
            get
            {
                if (this._n2 == null)
                {
                    LoadData2();
                }
                return this._n2;
            }
            set
            {
                this._n2 = value;
                OnPropertyChanged("N2");
            }
        }

        public List<StudentNationalityALL> N3
        {
            get
            {
                if (this._n3 == null)
                {
                    LoadData3();
                }
                return this._n3;
            }
            set
            {
                this._n3 = value;
                OnPropertyChanged("N3");
            }
        }

        #endregion

        private void AxisMaxScale()
        {
            double n1_max;
            double n2_max;
            double n3_max;

            if (N1.Count > 0) n1_max = Convert.ToDouble(N1.Max(x => x.Count));
            else n1_max = 0;

            if (N2.Count > 0) n2_max = Convert.ToDouble(N2.Max(x => x.Count));
            else n2_max = 0;

            if (N3.Count > 0) n3_max = Convert.ToDouble(N3.Max(x => x.Count));
            else n3_max = 0;


            double c_max = n1_max + n2_max + n3_max;
            int maxCount = 0;

            if (_barCombineMode == ChartSeriesCombineMode.Cluster)  // cluster
            {
                maxCount = UserFunctions.Max((int)n1_max, (int)n2_max, (int)n3_max);
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
                this.AxisTitle = "ΠΛΗΘΟΣ ΑΝΑ ΥΠΗΚΟΟΤΗΤΑ";
                this.AxisLabelFormat = "N0";
            }
            else if (mode == ChartSeriesCombineMode.Stack)
            {
                this.GapLength = 0.5d;
                AxisMaxScale();
                this.AxisTitle = "ΠΛΗΘΟΣ ΑΝΑ ΥΠΗΚΟΟΤΗΤΑ";
                this.AxisLabelFormat = "N0";
            }
            else if (mode == ChartSeriesCombineMode.Stack100)
            {
                this.GapLength = 0.5d;
                AxisMaxScale();
                this.AxisTitle = "ΠΛΗΘΟΣ ΑΝΑ ΥΠΗΚΟΟΤΗΤΑ (%)";
                this.AxisLabelFormat = "P0";
            }
        }

        #endregion

        #endregion


    }   // class StudentNationalityVM_ALL
}
