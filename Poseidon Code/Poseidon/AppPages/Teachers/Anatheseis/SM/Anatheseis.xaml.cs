using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.DataForm;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;

namespace Poseidon.AppPages.Teachers.Anatheseis.SM
{
    /// <summary>
    /// Interaction logic for Anatheseis.xaml
    /// </summary>
    public partial class Anatheseis : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        AnathesiModel am = new AnathesiModel();
        private CommitModel cm = new CommitModel();
        Kerberos k = new Kerberos();
        private bool isNewRec;
        private string afm;
        private int school_year;

        public Anatheseis()
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

            var eidikotites = from e in db.ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣs
                              orderby e.ΒΑΘΜΙΔΑ
                              select e;
            cboEidikotita.ItemsSource = eidikotites.ToList();

            var posts = from p in db.ΕΚΠ_ΚΑΤΗΓΟΡΙΕΣs
                        select p;
            cboPost.ItemsSource = posts.ToList();

            var terms = from t in db.ΕΞΑΜΗΝΑs
                        select t;
            cboterm.ItemsSource = terms.ToList();

            var lessons = from l in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                          select l;
            cboLesson.ItemsSource = lessons.ToList();

            // Load grid and form data
            var anatheseis = from a in db.ΑΝΑΘΕΣΕΙΣ_ΣΜs
                             orderby a.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                             select a;

            var trainers = from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           orderby t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ
                           select t;

            anathesiGrid.ItemsSource = null;    // anatheseis.ToList();
            teacherGrid.ItemsSource = trainers.ToList();

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
            SelectedTeacher.TeacherAFM = afm;
            SelectedTeacher.TeacherLastName = lastname;
            SelectedTeacher.TeacherFirstName = firstname;

            //ObservableCollection<ΑΙΤΗΣΗ> oca = new ObservableCollection<ΑΙΤΗΣΗ>(am.LoadAitisiData(afm));
            if (cboSchoolYear.SelectedIndex > 0)
            {
                anathesiGrid.ItemsSource = am.LoadAnathesiDataSM(afm);
                anathesiForm.ItemsSource = am.LoadAnathesiDataSM(afm);
                ShowTeacherInfo(sel_teacher);
            }
            else
            {
                anathesiGrid.ItemsSource = null;
                anathesiForm.ItemsSource = null;
                //UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε και διδακτικό έτος.");
                return;
            }
        }

        private void teacherGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (teacherGrid.ItemsSource == null) return;
        }

        private void cboSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            anathesiForm.CommandButtonsVisibility = DataFormCommandButtonsVisibility.All;

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

            SelectedSchoolYear.SchoolYear = school_year;

            if (teacherGrid.SelectedItem == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε καθηγητή");
                return;
            }
            else
            {
                anathesiGrid.ItemsSource = am.LoadAnathesiDataSM(SelectedTeacher.TeacherAFM);
                anathesiForm.ItemsSource = am.LoadAnathesiDataSM(SelectedTeacher.TeacherAFM);
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

        #region Datagrid and Dataform Events

        private void anathesiGrid_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            try
            {
                ΑΝΑΘΕΣΕΙΣ_ΣΜ sel_anathesi = anathesiGrid.SelectedItem as ΑΝΑΘΕΣΕΙΣ_ΣΜ;

                if (sel_anathesi == null) return;

                var anatheseis = from dba in db.ΑΝΑΘΕΣΕΙΣ_ΣΜs
                                 where dba.ΑΝΑΘΕΣΗ_ΚΩΔ == sel_anathesi.ΑΝΑΘΕΣΗ_ΚΩΔ
                                 orderby dba.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                                 select dba;

                anathesiForm.ItemsSource = anatheseis.ToList();
                anathesiForm.CommandButtonsVisibility = DataFormCommandButtonsVisibility.All;

                // set global variables in AnathesiModel
                // required for editing existing record
                SelectedTerm.Term = (int)sel_anathesi.ΕΞΑΜΗΝΟ;
                SelectedLesson.Lesson = (int)sel_anathesi.ΜΑΘΗΜΑ;
            }
            catch { }
        }

        private void anathesiGrid_Loaded(object sender, RoutedEventArgs e)
        {
            anathesiForm.CommandButtonsVisibility = DataFormCommandButtonsVisibility.All;
        }

        private void anathesiForm_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            // set Teacher PersistentData
            afm = SelectedTeacher.TeacherAFM;
            school_year = SelectedSchoolYear.SchoolYear;

            if (AnathesiPersistentDataSM.AnatheseisExistSM(afm, school_year) == false)
            {
                UserFunctions.ShowAdminMessage("Δεν βρέθηκαν αντίστοιχες αναθέσεις για μεταφορά κοινών στοιχείων.");
                AnathesiPersistentDataSM.Term = 1;
            }
            AnathesiPersistentDataSM.SetDefaultModelFieldsSM(afm, school_year);

            isNewRec = true;
            AnathesiPersistentDataSM.NewRecord = true;
            //UserFunctions.ShowAdminMessage("Main Form new record loaded: NewRecord=" + AnathesiPersistentData.NewRecord);
        }

        private void anathesiForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            ΑΝΑΘΕΣΕΙΣ_ΣΜ sel_anathesi = anathesiGrid.SelectedItem as ΑΝΑΘΕΣΕΙΣ_ΣΜ;
            if (sel_anathesi == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει πρώτα να επιλέξετε ανάθεση από τον πίνακα.");
                e.Cancel = true;
                return;
            }
        }

        private void anathesiForm_EditEnding(object sender, EditEndingEventArgs e)
        {
            if (!ValidatePrimaryFields()) { e.Cancel = true; return; }

            if (e.EditAction == EditAction.Commit)
            {
                var anathesi = anathesiForm.CurrentItem as ΑΝΑΘΕΣΕΙΣ_ΣΜ;

                if (isNewRec == true)
                {
                    AnathesiPersistentDataSM.AddEntityFieldsSM(db, anathesi);
                    cm.CommitData(db);
                    RefreshGridData();
                    // need to reset it otherwise InsertOnSubmit throws an exception if we edit immediately the new record
                    isNewRec = false;
                    AnathesiPersistentDataSM.NewRecord = false;
                }
                else
                {
                    AnathesiPersistentDataSM.EditEntityFieldsSM(db, anathesi);
                    cm.CommitData(db);
                    RefreshGridData();
                }
            }
            else if (e.EditAction == EditAction.Cancel)
            {
                isNewRec = false;
                AnathesiPersistentDataSM.NewRecord = false;
            }
        }

        private void anathesiForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var sel_anathesi = anathesiForm.CurrentItem as ΑΝΑΘΕΣΕΙΣ_ΣΜ;

            e.Cancel = false;
            if ((MessageBox.Show("Είστε σίγουροι ότι θέλετε διαγραφή αυτής της εγγραφής;", Title,
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                db.ΑΝΑΘΕΣΕΙΣ_ΣΜs.DeleteOnSubmit(sel_anathesi);
                cm.CommitData(db);
                RefreshGridData();
            }
        }

        private void anathesiForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var anathesi = anathesiForm.CurrentItem as ΑΝΑΘΕΣΕΙΣ_ΣΜ;
            if (anathesi == null)
            {
                e.Cancel = false;
                return;
            }
            e.Cancel = false;   // it is set to true in case of errors

            anathesi.ΑΦΜ = SelectedTeacher.TeacherAFM;

            // Kerberos validations
            if (k.EmptyFieldsErrorSM(anathesi) == true) { e.Cancel = true; }
            else if (k.HireDateInSchoolYearSM(anathesi) == false) { e.Cancel = true; }    //you started with false
            else if (k.ValidLessonTypeSM(anathesi) == false) { e.Cancel = true; }
            else if (k.ValidLessonHoursSM(anathesi) == false) { e.Cancel = true; }
            else if (k.NullProsontaSM(anathesi) == true) { e.Cancel = true; }
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

        private void RefreshGridData()
        {
            string afm = SelectedTeacher.TeacherAFM;
            anathesiGrid.ItemsSource = am.LoadAnathesiDataSM(afm);
            anathesiForm.ItemsSource = am.LoadAnathesiDataSM(afm);
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


    }   // class AnatheseisPage
}
