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

namespace Poseidon.AppPages.Teachers.Simbaseis
{
    /// <summary>
    /// Interaction logic for Simbaseis.xaml
    /// </summary>
    public partial class Simbaseis : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        SimbasiModel sm = new SimbasiModel();
        private CommitModel cm = new CommitModel();
        Kerberos k = new Kerberos();
        private bool isNewRec;
        private string afm;
        private int school_year;

        public Simbaseis()
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

            // Load grid and form data
            var simbaseis = from s in db.ΕΚΠ_ΣΥΜΒΑΣΗs
                             orderby s.ΣΧΟΛΙΚΟ_ΕΤΟΣ, s.ΗΜΕΡΟΜΗΝΙΑ descending, s.ΑΦΜ
                             select s;

            var trainers = from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           orderby t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ
                           select t;

            teacherGrid.ItemsSource = trainers.ToList();
            DataForm.ItemsSource = null;    // simbaseis.ToList();
        }

        #region Left Panel Events

        private void teacherGrid_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            ΕΚΠΑΙΔΕΥΤΙΚΟΣ sel_teacher = teacherGrid.SelectedItem as ΕΚΠΑΙΔΕΥΤΙΚΟΣ;

            if (sel_teacher == null)
            {
                UserFunctions.ShowAdminMessage("Δεν έχει επιλεγεί εκπαιδευτικός");
                return;
            }

            afm = sel_teacher.ΑΦΜ;
            string lastname = sel_teacher.ΕΠΩΝΥΜΟ;
            string firstname = sel_teacher.ΟΝΟΜΑ;

            // set the property to the AFM of the current teacher
            SimbasiTeacher.AFM = afm;
            SimbasiTeacher.LastName = lastname;
            SimbasiTeacher.FirstName = firstname;

            if (cboSchoolYear.SelectedIndex > 0)
            {
                DataForm.ItemsSource = sm.LoadSimbasiData(afm);
                ShowTeacherInfo(sel_teacher);
            }
            else
            {
                DataForm.ItemsSource = null;    // sm.LoadSimbasiData(afm);
                //UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε και διδακτικό έτος.");
                return;
            }

        }

        private void teacherGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (teacherGrid.ItemsSource == null) return;

            //teacherGrid.SelectedItem = teacherGrid.Items[0];
        }

        private void cboSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RadComboBoxItem selectedItem = cboSchoolYear.SelectedItem as RadComboBoxItem;
            ΣΧΟΛΙΚΟ_ΕΤΟΣ syear = cboSchoolYear.SelectedItem as ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            if (syear == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε και σχολικό έτος.");
                return;
            }
            school_year = Convert.ToInt32(syear.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ);

            if (school_year == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε και σχολικό έτος.");
                return;
            }

            SimbasiSchoolYear.SYear = school_year;

            if (teacherGrid.SelectedItem == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε καθηγητή");
                return;
            }
            else
            {
                DataForm.ItemsSource = sm.LoadSimbasiData(SimbasiTeacher.AFM);
                ShowTeacherInfo(teacherGrid.SelectedItem as ΕΚΠΑΙΔΕΥΤΙΚΟΣ);
            }
        }

        private void btnFilterOn_Click(object sender, RoutedEventArgs e)
        {
            if (!(txtSearch.Text == null || txtSearch.Text == ""))
            {
                var trainers = from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                               where t.ΕΠΩΝΥΜΟ.Contains(txtSearch.Text) || t.ΑΦΜ.Contains(txtSearch.Text)
                               select t;
                if (trainers.Count() == 0) UserFunctions.ShowAdminMessage("Δεν βρέθηκε καταχώρηση.");
                teacherGrid.ItemsSource = trainers.ToList();
            }
        }

        private void btnFilterOff_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = null;
            teacherGrid.ItemsSource = null; // trainers.ToList();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnFilterOn_Click(sender, e);
            }
        }

        #endregion

        #region Dataform Events

        private void DataForm_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            // set Teacher PersistentData
            afm = SimbasiTeacher.AFM;
            school_year = SimbasiSchoolYear.SYear;

            if (SimbasiPersistentData.SimbaseisExist(school_year) == false)
            {
                UserFunctions.ShowAdminMessage("Δεν βρέθηκαν αντίστοιχες συμβάσεις για μεταφορά κοινών στοιχείων.");
            }
            SimbasiPersistentData.SetDefaultModelFields(school_year);
            isNewRec = true;
            SimbasiPersistentData.NewRecord = true;
        }

        private void DataForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            ΕΚΠΑΙΔΕΥΤΙΚΟΣ sel_teacher = teacherGrid.SelectedItem as ΕΚΠΑΙΔΕΥΤΙΚΟΣ;
            if (sel_teacher == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει πρώτα να επιλέξετε εκπαιδευτικό από τον πίνακα.");
                e.Cancel = true;
                return;
            }
        }

        private void DataForm_EditEnding(object sender, EditEndingEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            if (e.EditAction == EditAction.Commit)
            {
                var simbasi = DataForm.CurrentItem as ΕΚΠ_ΣΥΜΒΑΣΗ;

                if (isNewRec == true)
                {
                    SimbasiPersistentData.AddEntityFields(db, simbasi);
                    simbasi.ΑΦΜ = SimbasiTeacher.AFM;
                    simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ = SimbasiSchoolYear.SYear;
                    //simbasi.ΔΙΟΙΚΗΤΗΣ = SimbasiPersistentData.Commander;
                    cm.CommitData(db);
                    RefreshFormData();
                    // need to reset it otherwise InsertOnSubmit throws an exception if we edit immediately the new record
                    isNewRec = false;
                    SimbasiPersistentData.NewRecord = false;
                }
                else
                {
                    SimbasiPersistentData.EditEntityFields(db, simbasi);
                    cm.CommitData(db);
                    RefreshFormData();
                }
            }
            else if (e.EditAction == EditAction.Cancel)
            {
                isNewRec = false;
                SimbasiPersistentData.NewRecord = false;
            }
        }

        private void DataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var sel_simbasi = DataForm.CurrentItem as ΕΚΠ_ΣΥΜΒΑΣΗ;

            e.Cancel = false;
            if ((MessageBox.Show("Είστε σίγουροι ότι θέλετε διαγραφή αυτής της εγγραφής;", Title,
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                db.ΕΚΠ_ΣΥΜΒΑΣΗs.DeleteOnSubmit(sel_simbasi);
                cm.CommitData(db);
                RefreshFormData();
            }
        }

        private void DataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var simbasi = DataForm.CurrentItem as ΕΚΠ_ΣΥΜΒΑΣΗ;
            if (simbasi == null)
            {
                e.Cancel = false;
                return;
            }
            e.Cancel = false;   // it is set to true in case of errors
            // Kerberos validations
            if (k.EmptyFieldsError(simbasi) == true) { e.Cancel = true; }
            else if (k.SimbasiDateInSchoolYear(simbasi) == false) { e.Cancel = true; }    //you started with false
            else { e.Cancel = false; }
        }

        #endregion

        private void ShowTeacherInfo(ΕΚΠΑΙΔΕΥΤΙΚΟΣ selectedTeacher)
        {
            var teacher = (from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           where t.ΑΦΜ == selectedTeacher.ΑΦΜ
                           select new { t.ΑΦΜ, t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ }).FirstOrDefault();

            txtTeacherInfo.Text = String.Format("ΑΦΜ: {0} - Ονοματεπώνυμο: {1} {2}", teacher.ΑΦΜ, teacher.ΕΠΩΝΥΜΟ, teacher.ΟΝΟΜΑ);
        }

        private void RefreshFormData()
        {
            string afm = SimbasiTeacher.AFM;
            DataForm.ItemsSource = sm.LoadSimbasiData(afm);
        }

        private bool ValidatePrimaryFields()
        {
            if (school_year == 0 || String.IsNullOrEmpty(afm))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή έτους και καθηγητή.");
                return false;
            }
            else if (school_year != 0 && !String.IsNullOrEmpty(afm))
            {
                return true;
            }
            else return false;
        }

    }   // class Simbaseis
}
