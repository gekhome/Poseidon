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
    /// Interaction logic for Egrafes.xaml
    /// </summary>
    public partial class Egrafes : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();
        StudentModel sm = new StudentModel();

        Kerberos k = new Kerberos();
        private bool isNewRec;
        private int syear;
        private int dioikitis;
        private string amka;
        private int sxoli = 2;

        public Egrafes()
        {
            InitializeComponent();
            LoadData();
            isNewRec = false;
        }

        private void LoadData()
        {
            var school_years = from sy in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                               orderby sy.ΣΧΟΛ_ΕΤΟΣ descending
                               select sy;
            cboSchoolYear.ItemsSource = school_years.ToList();

            var admin = from a in db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs
                        orderby a.ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select a;
            cboDioikitis.ItemsSource = admin.ToList();

            // Load grid and form data
            var egrafes = from s in db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs
                          where s.ΣΧΟΛΗ == 2
                          orderby s.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ descending, s.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ descending
                          select s;

            var students = from t in db.ΣΠΟΥΔΑΣΤΗΣs
                           where t.ΣΧΟΛΗ == 2
                           orderby t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ
                           select t;
            
            studentGrid.ItemsSource = students.ToList();
            DataForm.ItemsSource = egrafes.ToList();

            EgrafesSchool.School = 2;
        }

        #region Combos Data Sources

        private List<ΣΧΟΛΙΚΟ_ΕΤΟΣ> GetSchoolYears()
        {
            List<ΣΧΟΛΙΚΟ_ΕΤΟΣ> school_yearList = new List<ΣΧΟΛΙΚΟ_ΕΤΟΣ>();

            var school_years = from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                               where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == syear
                               orderby s.ΣΧΟΛ_ΕΤΟΣ descending
                               select s;

            return school_yearList = school_years.ToList();
        }

        private List<ΣΧΟΛΕΣ> GetSxoli()
        {
            List<ΣΧΟΛΕΣ> sxoliList = new List<ΣΧΟΛΕΣ>();

            var sxoles = from s in db.ΣΧΟΛΕΣs
                          where s.ΣΧΟΛΗ_ΚΩΔ == sxoli
                          select s;

            return sxoliList = sxoles.ToList();
        }


        private List<ΤΜΗΜΑΤΑ_ΣΜ> GetTmimataSM()
        {
            List<ΤΜΗΜΑΤΑ_ΣΜ> tmimaList = new List<ΤΜΗΜΑΤΑ_ΣΜ>();

            var tmimata = from s in db.ΤΜΗΜΑΤΑ_ΣΜs
                          where s.ΔΙΔ_ΕΤΟΣ == syear
                          orderby s.ΔΙΔ_ΕΤΟΣ descending
                          select s;

            return tmimaList = tmimata.ToList();
        }

        private List<ΦΟΙΤΗΣΗ_ΕΙΔΗ> GetFitisi()
        {
            List<ΦΟΙΤΗΣΗ_ΕΙΔΗ> fitisiList = new List<ΦΟΙΤΗΣΗ_ΕΙΔΗ>();

            var fitiseis = from s in db.ΦΟΙΤΗΣΗ_ΕΙΔΗs
                          select s;

            return fitisiList = fitiseis.ToList();
        }

        private List<ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ> GetDioikisi()
        {
            List<ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ> dioikisiList = new List<ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ>();

            var admin = from a in db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs
                        orderby a.ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select a;
            return dioikisiList = admin.ToList();
        }

        #endregion


        #region Left Panel Events

        private void btnFilterOn_Click(object sender, RoutedEventArgs e)
        {
            if (!(txtSearch.Text == null || txtSearch.Text == ""))
            {
                var students = from t in db.ΣΠΟΥΔΑΣΤΗΣs
                               where t.ΕΠΩΝΥΜΟ.Contains(txtSearch.Text) || t.ΑΜΚΑ.Contains(txtSearch.Text)
                               select t;
                if (students.Count() == 0) UserFunctions.ShowAdminMessage("Δεν βρέθηκε καταχώρηση.");
                studentGrid.ItemsSource = students.ToList();
            }
        }

        private void btnFilterOff_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = null;

            var students = from t in db.ΣΠΟΥΔΑΣΤΗΣs
                           where t.ΣΧΟΛΗ == 2
                           orderby t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ
                           select t;

            studentGrid.ItemsSource = students.ToList();

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnFilterOn_Click(sender, e);
            }
        }

        private void studentGrid_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            ΣΠΟΥΔΑΣΤΗΣ sel_student = studentGrid.SelectedItem as ΣΠΟΥΔΑΣΤΗΣ;

            if (sel_student == null)
            {
                UserFunctions.ShowAdminMessage("Δεν έχει επιλεγεί σπουδαστής");
                return;
            }

            amka = sel_student.ΑΜΚΑ;
            string lastname = sel_student.ΕΠΩΝΥΜΟ;
            string firstname = sel_student.ΟΝΟΜΑ;

            var students_reg = from s in db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs
                               where s.ΑΜΚΑ == amka && s.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == syear && s.ΣΧΟΛΗ == sxoli
                               select s;

            DataForm.ItemsSource = students_reg.ToList();
            ShowStudentInfo(studentGrid.SelectedItem as ΣΠΟΥΔΑΣΤΗΣ);
            //RefreshFormData();


        }

        private void cboSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ΣΧΟΛΙΚΟ_ΕΤΟΣ _syear = cboSchoolYear.SelectedItem as ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            if (_syear == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε και σχολικό έτος.");
                return;
            }
            syear = Convert.ToInt32(_syear.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ);

            if (syear == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε και σχολικό έτος.");
                return;
            }

            //EgrafesSchoolYear.SchoolYear = school_year;

            if (studentGrid.SelectedItem == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε σπουδαστή");
                return;
            }
            else
            {
                var students_reg = from s in db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs
                                   where s.ΑΜΚΑ == amka && s.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == syear && s.ΣΧΟΛΗ == sxoli
                                   select s;

                DataForm.ItemsSource = students_reg.ToList();
                ShowStudentInfo(studentGrid.SelectedItem as ΣΠΟΥΔΑΣΤΗΣ);
                //RefreshFormData();
            }
        }

        private void cboDioikitis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ admin = cboDioikitis.SelectedItem as ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ;
            dioikitis = admin.ΚΩΔΙΚΟΣ;
        }

        private void studentGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (studentGrid.ItemsSource == null) return;
        }

        #endregion

        #region DataForm Events

        private void DataForm_AutoGeneratingField(object sender, AutoGeneratingFieldEventArgs e)
        {
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΜΚΑ", "Α.Μ.Κ.Α.");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΣΧΟΛΗ", "Σχολή");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ", "Διδακτικό έτος");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΤΜΗΜΑ", "Τμήμα");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΗΜΝΙΑ_ΕΓΓΡΑΦΗ", "Ημερομηνία εγγραφής");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΦΟΙΤΗΣΗ", "Φοίτηση");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΟΙΚΗΤΗΣ", "Διοικητής");


            if (e.PropertyName == "ΚΩΔΙΚΟΣ")
            {
                e.DataField.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyName == "ΑΜΚΑ")
            {
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
            }

            if (e.PropertyName == "ΦΟΙΤΗΣΗ")
            {
                DataFormComboBoxField schoolYearField = new DataFormComboBoxField();
                schoolYearField.ItemsSource = GetFitisi();
                schoolYearField.Label = "Φοίτηση";

                schoolYearField.DisplayMemberPath = "ΦΟΙΤΗΣΗ";
                schoolYearField.SelectedValuePath = "ΦΟΙΤΗΣΗ_ΚΩΔ";
                schoolYearField.DataMemberBinding = new Binding("ΦΟΙΤΗΣΗ");
                e.DataField = schoolYearField;
            }

            if (e.PropertyName == "ΔΙΟΙΚΗΤΗΣ")
            {
                DataFormComboBoxField adminField = new DataFormComboBoxField();
                adminField.ItemsSource = GetDioikisi();
                adminField.Label = "Διοικητής";

                adminField.DisplayMemberPath = "ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ";
                adminField.SelectedValuePath = "ΚΩΔΙΚΟΣ";
                adminField.DataMemberBinding = new Binding("ΔΙΟΙΚΗΤΗΣ");
                e.DataField = adminField;
            }

        }

        private void DataForm_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            isNewRec = true;
        }

        private void DataForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            ΣΠΟΥΔΑΣΤΗΣ sel_student = studentGrid.SelectedItem as ΣΠΟΥΔΑΣΤΗΣ;
            if (sel_student == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει πρώτα να επιλέξετε σπουδαστή από τον πίνακα.");
                e.Cancel = true;
                return;
            }

            var egrafi = DataForm.CurrentItem as ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ;
            egrafi.ΑΜΚΑ = amka;
            egrafi.ΣΧΟΛΗ = sxoli;
            egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
            egrafi.ΔΙΟΙΚΗΤΗΣ = dioikitis;

        }

        private void DataForm_EditEnding(object sender, EditEndingEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            if (e.EditAction == EditAction.Commit)
            {
                var egrafi = DataForm.CurrentItem as ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ;

                egrafi.ΑΜΚΑ = amka;
                egrafi.ΣΧΟΛΗ = sxoli;
                egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
                egrafi.ΔΙΟΙΚΗΤΗΣ = dioikitis;

                if (isNewRec == true)
                {
                    db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs.InsertOnSubmit(egrafi);
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

        private void DataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var sel_egrafi = DataForm.CurrentItem as ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ;

            e.Cancel = false;
            if ((MessageBox.Show("Είστε σίγουροι ότι θέλετε διαγραφή αυτής της εγγραφής;", Title,
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                var del_egrafi = db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs.SingleOrDefault(p => p.ΚΩΔΙΚΟΣ == sel_egrafi.ΚΩΔΙΚΟΣ);
                db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs.DeleteOnSubmit(sel_egrafi);
                cm.CommitData(db);
                //RefreshFormData();
            }
        }

        private void DataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var eggrafi = this.DataForm.CurrentItem as ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ;
            if (eggrafi == null)
            {
                e.Cancel = false;
                return;
            }

            string _dateEggrafi = Convert.ToString(eggrafi.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ); //null value converted to ""
            int _syear = Convert.ToInt32(eggrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ);
            int _sxoli = Convert.ToInt32(eggrafi.ΣΧΟΛΗ);
            int _tmima = Convert.ToInt32(eggrafi.ΤΜΗΜΑ);
            int _typeEggrafi = Convert.ToInt32(eggrafi.ΦΟΙΤΗΣΗ);

            if (String.IsNullOrEmpty(_dateEggrafi))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί η ημ/νια εγγραφής.");
            }

            if (!InSchoolYear(_dateEggrafi, syear))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Η ημ/νια εγγραφής είναι εκτός του επιλεγμένου διδακτικού έτους.");
            }

            if (_syear == 0 || _syear == -1)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί το διδακτικό έτος.");
            }

            if (dioikitis == 0 || dioikitis == -1)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί ο Διοικητής.");
            }

            if (_sxoli == 0 || _sxoli == -1)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί η Σχολή (Μηχανικοί ή Πλοίαρχοι.");
            }

            if (_tmima == 0 || _tmima == -1)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί το Τμήμα.");
            }

            if (_typeEggrafi == 0 || _typeEggrafi == -1)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί το είδος φοίτησης.");
            }
        }

        #endregion

        private void ShowStudentInfo(ΣΠΟΥΔΑΣΤΗΣ selectedStudent)
        {
            var student = (from t in db.ΣΠΟΥΔΑΣΤΗΣs
                           where t.ΑΜΚΑ == selectedStudent.ΑΜΚΑ
                           select new { t.ΑΜΚΑ, t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ }).FirstOrDefault();

            txtStudentInfo.Text = String.Format("AMKA: {0} - Ονοματεπώνυμο: {1} {2}", student.ΑΜΚΑ, student.ΕΠΩΝΥΜΟ, student.ΟΝΟΜΑ);
        }

        private void RefreshFormData()
        {
            string amka = SelectedStudent.AMKA;
            int school_year = EgrafesSchoolYear.SchoolYear;
            DataForm.ItemsSource = sm.LoadEgrafesData(amka, school_year);
        }

        private bool ValidatePrimaryFields()
        {
            if (syear == 0 || dioikitis == 0 || String.IsNullOrEmpty(amka))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή έτους, διοικητή και σπουδαστή.");
                return false;
            }
            else if (syear > 0 && dioikitis > 0 && !String.IsNullOrEmpty(amka))
            {
                return true;
            }
            else return false;
        }

        private bool InSchoolYear(string the_date, int syear)
        {
            bool result = true;

            var SchoolYear = (from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                              where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == syear
                              select new { s.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ, s.ΣΧ_ΕΤΟΣ_ΛΗΞΗ }).FirstOrDefault();

            DateTime _date = Convert.ToDateTime(the_date);
            DateTime syear_start = (DateTime)SchoolYear.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ;
            DateTime syear_end = (DateTime)SchoolYear.ΣΧ_ΕΤΟΣ_ΛΗΞΗ;

            if (_date < syear_start || _date > syear_end)
                result = false;
            else result = true;

            return result;
        }

    }
}
