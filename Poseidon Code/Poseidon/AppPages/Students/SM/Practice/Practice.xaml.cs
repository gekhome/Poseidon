using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Telerik.Windows.Controls.Data.DataForm;
using Telerik.Windows.Controls;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;

namespace Poseidon.AppPages.Students.SM
{
    /// <summary>
    /// Interaction logic for Practice.xaml
    /// </summary>
    public partial class Practice : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        private int syear;
        private int taxidi;
        private int tmima;
        private string amka;
        private int sxoli = 2;
        private bool isNewRec;

        public Practice()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var syears = from sy in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         orderby sy.ΣΧΟΛ_ΕΤΟΣ descending
                         select sy;
            cboSchoolYear.ItemsSource = syears.ToList();

            var taxidia = from t in db.ΤΑΞΙΔΙΑs
                          select t;
            cboTaxidi.ItemsSource = taxidia.ToList();

            var students_kep = from s in db.ΚΕΠs
                               where s.ΣΧΟΛΗ == sxoli
                               select s;

            DataForm.ItemsSource = students_kep.ToList();

        }

        #region Selection Panel Events

        private void cboSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboSchoolYear.IsLoaded) return;

            ΣΧΟΛΙΚΟ_ΕΤΟΣ school_year = cboSchoolYear.SelectedItem as ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            if (school_year == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί διδακτικό έτος.");
                return;
            }

            syear = school_year.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ;
        }

        private void cboTaxidi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboTaxidi.IsLoaded) return;

            ΤΑΞΙΔΙΑ taxidia = cboTaxidi.SelectedItem as ΤΑΞΙΔΙΑ;

            if (taxidia == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί εκπαιδευτικό ταξίδι.");
                return;
            }

            taxidi = taxidia.ΚΕΠ_ΚΩΔ;

            if (taxidi == 1)
            {
                var tmimata = from t in db.ΤΜΗΜΑΤΑ_ΣΜs
                              where t.ΕΞΑΜΗΝΟ == 1 && t.ΔΙΔ_ΕΤΟΣ == syear
                              select t;
                cboTmima.ItemsSource = tmimata.ToList();
            }
            else if (taxidi == 2)
            {
                var tmimata = from t in db.ΤΜΗΜΑΤΑ_ΣΜs
                              where t.ΕΞΑΜΗΝΟ == 3 && t.ΔΙΔ_ΕΤΟΣ == syear
                              select t;
                cboTmima.ItemsSource = tmimata.ToList();
            }
        }

        private void cboTmima_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboTmima.IsLoaded) return;

            ΤΜΗΜΑΤΑ_ΣΜ tmimata = cboTmima.SelectedItem as ΤΜΗΜΑΤΑ_ΣΜ;

            if (tmimata == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί τμήμα.");
                return;
            }

            tmima = (int)tmimata.ΤΜΗΜΑ_ΚΩΔ;

            var spoudastes = from s in db.ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΜs
                             where s.ΤΜΗΜΑ == tmima
                             orderby s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                             select s;
            cboStudent.ItemsSource = spoudastes.ToList();

        }

        private void cboStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboStudent.IsLoaded) return;

            ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΜ student = cboStudent.SelectedItem as ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΜ;

            if (student == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί σπουδαστής.");
                return;
            }

            amka = student.ΑΜΚΑ;

            var students_kep = from s in db.ΚΕΠs
                           where s.ΑΜΚΑ == amka && s.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == syear 
                           && s.ΤΜΗΜΑ == tmima && s.ΤΑΞΙΔΙ == taxidi && s.ΣΧΟΛΗ == sxoli
                           select s;

            DataForm.ItemsSource = students_kep.ToList();
        }

        #endregion

        #region Form Combos Data Sources

        private List<ΣΧΟΛΙΚΟ_ΕΤΟΣ> GetSchoolYears()
        {
            List<ΣΧΟΛΙΚΟ_ΕΤΟΣ> school_yearList = new List<ΣΧΟΛΙΚΟ_ΕΤΟΣ>();

            var school_years = from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                               //where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == syear
                               orderby s.ΣΧΟΛ_ΕΤΟΣ descending
                               select s;

            return school_yearList = school_years.ToList();
        }

        private List<ΣΧΟΛΕΣ> GetSxoli()
        {
            List<ΣΧΟΛΕΣ> sxoliList = new List<ΣΧΟΛΕΣ>();

            var sxoles = from s in db.ΣΧΟΛΕΣs
                         //where s.ΣΧΟΛΗ_ΚΩΔ == sxoli
                         select s;

            return sxoliList = sxoles.ToList();
        }

        private List<ΤΜΗΜΑΤΑ_ΣΜ> GetTmimataSM()
        {
            List<ΤΜΗΜΑΤΑ_ΣΜ> tmimaList = new List<ΤΜΗΜΑΤΑ_ΣΜ>();

            var tmimata = from s in db.ΤΜΗΜΑΤΑ_ΣΜs
                          //where s.ΤΜΗΜΑ_ΚΩΔ == tmima
                          orderby s.ΔΙΔ_ΕΤΟΣ descending
                          select s;

            return tmimaList = tmimata.ToList();
        }

        private List<ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑ> GetShipCategories()
        {
            List<ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑ> categoryList = new List<ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑ>();

            var categories = from c in db.ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑs
                             select c;

            return categoryList = categories.ToList();
        }

        private List<ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ> GetShipSubCategories()
        {
            List<ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ> subcategoryList = new List<ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ>();

            var subcategories = from c in db.ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑs
                             select c;

            return subcategoryList = subcategories.ToList();
        }

        private List<ΤΑΞΙΔΙ_ΑΠΟΤΕΛΕΣΜΑ> GetApotelesma()
        {
            List<ΤΑΞΙΔΙ_ΑΠΟΤΕΛΕΣΜΑ> apotelesmaList = new List<ΤΑΞΙΔΙ_ΑΠΟΤΕΛΕΣΜΑ>();

            var result = from r in db.ΤΑΞΙΔΙ_ΑΠΟΤΕΛΕΣΜΑs
                         select r;

            return apotelesmaList = result.ToList();
        }

        private List<ΤΑΞΙΔΙΑ> GetTaxidi()
        {
            List<ΤΑΞΙΔΙΑ> taxidiList = new List<ΤΑΞΙΔΙΑ>();

            var result = from t in db.ΤΑΞΙΔΙΑs
                         //where t.ΚΕΠ_ΚΩΔ == taxidi
                         select t;

            return taxidiList = result.ToList();
        }


        #endregion

        #region DataForm Events

        private void DataForm_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΚΩΔΙΚΟΣ", "Κωδικός");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΜΚΑ", "Α.Μ.Κ.Α.");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΣΧΟΛΗ", "Σχολή");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ", "Διδακτικό έτος");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΤΑΞΙΔΙ", "Εκπαιδευτικό ταξίδι");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΤΜΗΜΑ", "Τμήμα");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΠΟ", "Έναρξη");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΕΩΣ", "Λήξη");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΠΟΤΕΛΕΣΜΑ", "Αποτέλεσμα εκτέλεσης");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΛΟΙΟ_ΟΝΟΜΑ", "Όνομα πλοίου");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΛΟΙΟ_ΝΗΟΛΟΓΙΟ", "Νηολόγιο");
            e.DataField.Label = ((string)e.DataField.Label).Replace("SHIP_TYPE", "Κατηγορία πλοίου");
            e.DataField.Label = ((string)e.DataField.Label).Replace("SHIP_SUBTYPE", "Υποκατηγορία πλοίου");

            if (e.PropertyName == "ΚΩΔΙΚΟΣ")
            {
                e.DataField.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyName == "ΑΜΚΑ")
            {
                e.DataField.IsReadOnly = true;
            }

            if (e.PropertyName == "ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ")
            {
                DataFormComboBoxField syearField = new DataFormComboBoxField();
                syearField.ItemsSource = GetSchoolYears();
                syearField.Label = "Διδακτικό έτος";

                syearField.DisplayMemberPath = "ΣΧΟΛ_ΕΤΟΣ";
                syearField.SelectedValuePath = "ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ";
                syearField.DataMemberBinding = new Binding("ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ");
                e.DataField = syearField;
                e.DataField.IsReadOnly = true;
            }

            if (e.PropertyName == "ΣΧΟΛΗ")
            {
                DataFormComboBoxField sxoliField = new DataFormComboBoxField();
                sxoliField.ItemsSource = GetSxoli();
                sxoliField.Label = "Σχολή";

                sxoliField.DisplayMemberPath = "ΣΧΟΛΗ";
                sxoliField.SelectedValuePath = "ΣΧΟΛΗ_ΚΩΔ";
                sxoliField.DataMemberBinding = new Binding("ΣΧΟΛΗ");
                e.DataField = sxoliField;
                e.DataField.IsReadOnly = true;
            }

            if (e.PropertyName == "ΤΑΞΙΔΙ")
            {
                DataFormComboBoxField kepField = new DataFormComboBoxField();
                kepField.ItemsSource = GetTaxidi();
                kepField.Label = "Εκπαιδευτικό ταξίδι";

                kepField.DisplayMemberPath = "ΚΕΠ";
                kepField.SelectedValuePath = "ΚΕΠ_ΚΩΔ";
                kepField.DataMemberBinding = new Binding("ΤΑΞΙΔΙ");
                e.DataField = kepField;
                e.DataField.IsReadOnly = true;
            }

            if (e.PropertyName == "ΤΜΗΜΑ")
            {
                DataFormComboBoxField tmimaField = new DataFormComboBoxField();
                tmimaField.ItemsSource = GetTmimataSM();
                tmimaField.Label = "Τμήμα";

                tmimaField.DisplayMemberPath = "ΤΜΗΜΑ_ΟΝΟΜΑΣΙΑ";
                tmimaField.SelectedValuePath = "ΤΜΗΜΑ_ΚΩΔ";
                tmimaField.DataMemberBinding = new Binding("ΤΜΗΜΑ");
                e.DataField = tmimaField;
                e.DataField.IsReadOnly = true;
            }

            if (e.PropertyName == "SHIP_TYPE")
            {
                DataFormComboBoxField shipCategoryField = new DataFormComboBoxField();
                shipCategoryField.ItemsSource = GetShipCategories();
                shipCategoryField.Label = "Κατηγορία πλοίου";

                shipCategoryField.DisplayMemberPath = "ΚΑΤΗΓΟΡΙΑ_ΛΕΚΤΙΚΟ";
                shipCategoryField.SelectedValuePath = "ΚΑΤΗΓΟΡΙΑ_ΚΩΔ";
                shipCategoryField.DataMemberBinding = new Binding("SHIP_TYPE");
                e.DataField = shipCategoryField;
            }

            if (e.PropertyName == "SHIP_SUBTYPE")
            {
                DataFormComboBoxField shipSubCategoryField = new DataFormComboBoxField();
                shipSubCategoryField.ItemsSource = GetShipSubCategories();
                shipSubCategoryField.Label = "Υποκατηγορία πλοίου";

                shipSubCategoryField.DisplayMemberPath = "ΥΠΟΚΑΤΗΓΟΡΙΑ";
                shipSubCategoryField.SelectedValuePath = "ΥΠΟΚΑΤΗΓΟΡΙΑ_ΚΩΔ";
                shipSubCategoryField.DataMemberBinding = new Binding("SHIP_SUBTYPE");
                e.DataField = shipSubCategoryField;
            }

            if (e.PropertyName == "ΑΠΟΤΕΛΕΣΜΑ")
            {
                DataFormComboBoxField resultField = new DataFormComboBoxField();
                resultField.ItemsSource = GetApotelesma();
                resultField.Label = "Αποτέλεσμα εκτέλεσης";

                resultField.DisplayMemberPath = "ΑΠΟΤΕΛΕΣΜΑ";
                resultField.SelectedValuePath = "ΑΠΟΤΕΛΕΣΜΑ_ΚΩΔ";
                resultField.DataMemberBinding = new Binding("ΑΠΟΤΕΛΕΣΜΑ");
                e.DataField = resultField;
            }


        }

        private void DataForm_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
            if (!ValidatePrimaryFields()) {  e.Cancel = true; return; }

            isNewRec = true;
        }

        private void DataForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            var egrafi = DataForm.CurrentItem as ΚΕΠ;
            egrafi.ΑΜΚΑ = amka;
            egrafi.ΣΧΟΛΗ = sxoli;
            egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
            egrafi.ΤΑΞΙΔΙ = taxidi;
            egrafi.ΤΜΗΜΑ = tmima;
            
        }

        private void DataForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            if (e.EditAction == EditAction.Commit)
            {
                var egrafi = DataForm.CurrentItem as ΚΕΠ;

                egrafi.ΑΜΚΑ = amka;
                egrafi.ΣΧΟΛΗ = 2;
                egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
                egrafi.ΤΑΞΙΔΙ = taxidi;
                egrafi.ΤΜΗΜΑ = tmima;

                if (isNewRec == true)
                {
                    db.ΚΕΠs.InsertOnSubmit(egrafi);
                    cm.CommitData(db);
                    //RefreshFormData();
                    // need to reset it otherwise InsertOnSubmit throws an exception if we edit immediately the new record
                    isNewRec = false;
                }
                else
                {
                    cm.CommitData(db);
                    //RefreshFormData();
                }
            }
            else if (e.EditAction == EditAction.Cancel)
            {
                isNewRec = false;
            }
        }

        private void DataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var taxidi = this.DataForm.CurrentItem as ΚΕΠ;

            if (taxidi == null) return;

            string _date1 = Convert.ToString(taxidi.ΑΠΟ); //null value converted to ""
            string _date2 = Convert.ToString(taxidi.ΕΩΣ); //null value converted to ""
            string _shipName = taxidi.ΠΛΟΙΟ_ΟΝΟΜΑ;
            string _shipReg = taxidi.ΠΛΟΙΟ_ΝΗΟΛΟΓΙΟ;

            int _shipType = Convert.ToInt32(taxidi.SHIP_TYPE);
            int _shipSubtype = Convert.ToInt32(taxidi.SHIP_SUBTYPE);

            if (taxidi == null)
            {
                e.Cancel = false;
                return;
            }

            if (String.IsNullOrEmpty(_date1))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί η ημ/νια έναρξης.");
            }
            if (String.IsNullOrEmpty(_date2))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί η ημ/νια περάτωσης.");
            }

            if (_shipType == 0 || _shipType == -1)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί η κατηγορία πλοίου.");
            }
            if (_shipSubtype == 0 || _shipSubtype == -1)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί η υποκατηγορία πλοίου.");
            }

        }

        private void DataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var sel_egrafi = DataForm.CurrentItem as ΚΕΠ;

            e.Cancel = false;
            if ((MessageBox.Show("Είστε σίγουροι ότι θέλετε διαγραφή αυτής της εγγραφής;", Title,
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                var del_egrafi = db.ΚΕΠs.SingleOrDefault(p => p.ΚΩΔΙΚΟΣ == sel_egrafi.ΚΩΔΙΚΟΣ);
                db.ΚΕΠs.DeleteOnSubmit(sel_egrafi);
                cm.CommitData(db);
                RefreshFormData();
            }
        }

        #endregion

        private void RefreshFormData()
        {
            //DataForm.ItemsSource = sm.LoadEgrafesData(amka, school_year);
        }

        private bool ValidatePrimaryFields()
        {
            if (syear == 0 || tmima == 0 || taxidi == 0 || String.IsNullOrEmpty(amka))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή έτους, τμήματος, ταξιδιού και σπουδαστή.");
                return false;
            }
            else if (syear != 0 && tmima != 0)
            {
                return true;
            }
            else return false;
        }

    }   // class Practice
}
