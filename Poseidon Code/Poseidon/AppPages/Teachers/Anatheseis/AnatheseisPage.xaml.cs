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

namespace Poseidon.AppPages.Teachers.Anatheseis
{
    /// <summary>
    /// Interaction logic for AnatheseisPage.xaml
    /// </summary>
    public partial class AnatheseisPage : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        AnathesiModel am = new AnathesiModel();
        private CommitModel cm = new CommitModel();
        Kerberos k = new Kerberos();
        private bool isNewRec;


        public AnatheseisPage()
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

            var schools = from s in db.ΣΧΟΛΕΣs
                          select s;
            cboSchool.ItemsSource = schools.ToList();

            var terms = from t in db.ΕΞΑΜΗΝΑs
                        select t;
            cboterm.ItemsSource = terms.ToList();

            // Load grid and form data
            var anatheseis = from a in db.ΑΝΑΘΕΣΕΙΣs
                             orderby a.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                             select a;

            var trainers = from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           orderby t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ
                           select t;

            anathesiGrid.ItemsSource = anatheseis.ToList();
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

            string afm = sel_teacher.ΑΦΜ;
            string lastname = sel_teacher.ΕΠΩΝΥΜΟ;
            string firstname = sel_teacher.ΟΝΟΜΑ;

            // set the property to the AFM of the current teacher
            SelectedTeacher.TeacherAFM = afm;
            SelectedTeacher.TeacherLastName = lastname;
            SelectedTeacher.TeacherFirstName = firstname;

            //ObservableCollection<ΑΙΤΗΣΗ> oca = new ObservableCollection<ΑΙΤΗΣΗ>(am.LoadAitisiData(afm));
            if (cboSchoolYear.SelectedIndex > 0)
            {
                anathesiGrid.ItemsSource = am.LoadAnathesiData(afm);
                anathesiForm.ItemsSource = am.LoadAnathesiData(afm);
                ShowTeacherInfo(sel_teacher);
            }
            else
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε και διδακτικό έτος.");
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
            int school_year = Convert.ToInt32(syear.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ);

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
                anathesiGrid.ItemsSource = am.LoadAnathesiData(SelectedTeacher.TeacherAFM);
                anathesiForm.ItemsSource = am.LoadAnathesiData(SelectedTeacher.TeacherAFM);
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
                ΑΝΑΘΕΣΕΙΣ sel_anathesi = anathesiGrid.SelectedItem as ΑΝΑΘΕΣΕΙΣ;

                if (sel_anathesi == null) return;

                var anatheseis = from dba in db.ΑΝΑΘΕΣΕΙΣs
                                 where dba.ΑΝΑΘΕΣΗ_ΚΩΔ == sel_anathesi.ΑΝΑΘΕΣΗ_ΚΩΔ
                                 orderby dba.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                                 select dba;

                anathesiForm.ItemsSource = anatheseis.ToList();
                anathesiForm.CommandButtonsVisibility = DataFormCommandButtonsVisibility.All;

                // set global variables in AnathesiModel
                // required for editing existing record
                SelectedSchool.School = (int)sel_anathesi.ΣΧΟΛΗ;
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
            // set Teacher PersistentData
            string afm = SelectedTeacher.TeacherAFM;
            int school_year = SelectedSchoolYear.SchoolYear;

            if (AnathesiPersistentData.AnatheseisExist(afm, school_year) == false)
            {
                UserFunctions.ShowAdminMessage("Δεν βρέθηκαν αντίστοιχες Αναθέσεις για μεταφορά.");
                AnathesiPersistentData.Term = 1;
            }
            AnathesiPersistentData.SetDefaultModelFields(afm, school_year);

            isNewRec = true;
            AnathesiPersistentData.NewRecord = true;
            //UserFunctions.ShowAdminMessage("Main Form new record loaded: NewRecord=" + AnathesiPersistentData.NewRecord);
        }

        private void anathesiForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ΑΝΑΘΕΣΕΙΣ sel_anathesi = anathesiGrid.SelectedItem as ΑΝΑΘΕΣΕΙΣ;
            if (sel_anathesi == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει πρώτα να επιλέξετε ανάθεση από τον πίνακα.");
                e.Cancel = true;
                return;
            }
        }

        private void anathesiForm_EditEnding(object sender, EditEndingEventArgs e)
        {
            if (e.EditAction == EditAction.Commit)
            {
                var anathesi = anathesiForm.CurrentItem as ΑΝΑΘΕΣΕΙΣ;

                if (isNewRec == true)
                {
                    AnathesiPersistentData.AddEntityFields(db, anathesi);
                    cm.CommitData(db);
                    RefreshGridData();
                    // need to reset it otherwise InsertOnSubmit throws an exception if we edit immediately the new record
                    isNewRec = false;
                    AnathesiPersistentData.NewRecord = false;
                }
                else
                {
                    AnathesiPersistentData.EditEntityFields(db, anathesi);
                    cm.CommitData(db);
                    RefreshGridData();
                }
            }
            else if (e.EditAction == EditAction.Cancel)
            {
                isNewRec = false;
                AnathesiPersistentData.NewRecord = false;
            }
        }

        private void anathesiForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var sel_anathesi = anathesiForm.CurrentItem as ΑΝΑΘΕΣΕΙΣ;

            e.Cancel = false;
            if ((MessageBox.Show("Είστε σίγουροι ότι θέλετε διαγραφή αυτής της εγγραφής;", Title,
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                db.ΑΝΑΘΕΣΕΙΣs.DeleteOnSubmit(sel_anathesi);
                cm.CommitData(db);
                RefreshGridData();
            }
        }

        private void anathesiForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var anathesi = anathesiForm.CurrentItem as ΑΝΑΘΕΣΕΙΣ;
            if (anathesi == null)
            {
                e.Cancel = false;
                return;
            }
            e.Cancel = false;   // it is set to true in case of errors

            anathesi.ΑΦΜ = SelectedTeacher.TeacherAFM;

            // Kerberos validations
            if (k.EmptyFieldsError(anathesi) == true) { e.Cancel = true; }
            else if (k.HireDateInSchoolYearEpas(anathesi) == false) { e.Cancel = true; }    //you started with false
            else if (k.ValidLessonType(anathesi) == false) { e.Cancel = true; }
            else if (k.ValidLessonHours(anathesi) == false) { e.Cancel = true; }
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
            anathesiGrid.ItemsSource = am.LoadAnathesiData(afm);
            anathesiForm.ItemsSource = am.LoadAnathesiData(afm);
        }


    }   // class AnatheseisPage
}
