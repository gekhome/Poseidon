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

namespace Poseidon.AppPages.Teachers.Bebeoseis
{
    /// <summary>
    /// Interaction logic for Bebeoseis.xaml
    /// </summary>
    public partial class Bebeoseis : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();
        Kerberos k = new Kerberos();
        private bool isNewRec;

        public Bebeoseis()
        {
            InitializeComponent();
            LoadData();
            isNewRec = false;
        }

        private void LoadData()
        {
            var trainers = from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           orderby t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ
                           select t;
            teacherGrid.ItemsSource = trainers.ToList();

            DataForm.ItemsSource = null;
        }

        #region Combos Data Sources

        private List<ΣΧΟΛΙΚΟ_ΕΤΟΣ> GetSchoolYears()
        {
            List<ΣΧΟΛΙΚΟ_ΕΤΟΣ> school_yearList = new List<ΣΧΟΛΙΚΟ_ΕΤΟΣ>();

            var school_years = from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                               orderby s.ΣΧΟΛ_ΕΤΟΣ descending
                               select s;

            return school_yearList = school_years.ToList();
        }

        private List<ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ> GetAenAdmin()
        {
            List<ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ> aenList = new List<ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ>();

            var aen = from a in db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs
                      orderby a.ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                      select a;

            return aenList = aen.ToList();
        }

        #endregion


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

            ShowTeacherInfo(sel_teacher);

            var bebeoseis = from t in db.ΕΚΠ_ΒΕΒΑΙΩΣΗs
                          where t.ΑΦΜ == afm
                          select t;

            DataForm.ItemsSource = bebeoseis.ToList();
        }

        private void teacherGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (teacherGrid.ItemsSource == null) return;

            //teacherGrid.SelectedItem = teacherGrid.Items[0];
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

        private void ShowTeacherInfo(ΕΚΠΑΙΔΕΥΤΙΚΟΣ selectedTeacher)
        {
            var teacher = (from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           where t.ΑΦΜ == selectedTeacher.ΑΦΜ
                           select new { t.ΑΦΜ, t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ }).FirstOrDefault();

            txtTeacherInfo.Text = String.Format("ΑΦΜ: {0} - Ονοματεπώνυμο: {1} {2}", teacher.ΑΦΜ, teacher.ΕΠΩΝΥΜΟ, teacher.ΟΝΟΜΑ);
        }

        #endregion

        #region Dataform Events

        private void DataForm_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            //var bebeosi = this.DataForm.CurrentItem as ΠΡΟΚΗΡΥΞΗ;
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΒΕΒΑΙΩΣΗ_ΚΩΔ", "Κωδικός");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΦΜ", "ΑΦΜ");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΣΧΟΛΙΚΟ_ΕΤΟΣ", "Εκπαιδευτικό έτος");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΡΩΤΟΚΟΛΛΟ", "Αρ.Πρωτοκόλλου");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΗΜΕΡΟΜΗΝΙΑ", "Ημερομηνία");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΡΟΣΛΗΨΗ_ΩΣ", "Προσλήφθηκε ως");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΣΤΙΣ_ΕΙΔΙΚΟΤΗΤΕΣ", "Στις ειδικότητες");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΟΙΚΗΤΗΣ", "Ο Διοικητής");

            if (e.PropertyName == "ΒΕΒΑΙΩΣΗ_ΚΩΔ")
            {
                e.DataField.Visibility = Visibility.Collapsed;
            }
            if (e.PropertyName == "ΑΦΜ")
            {
                e.DataField.IsReadOnly = true;
            }
            if (e.PropertyName == "ΣΧΟΛΙΚΟ_ΕΤΟΣ")
            {
                DataFormComboBoxField schoolYearField = new DataFormComboBoxField();
                schoolYearField.ItemsSource = GetSchoolYears();
                schoolYearField.Label = "Εκπαιδευτικό Έτος";

                schoolYearField.DisplayMemberPath = "ΣΧΟΛ_ΕΤΟΣ";
                schoolYearField.SelectedValuePath = "ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ";
                schoolYearField.DataMemberBinding = new Binding("ΣΧΟΛΙΚΟ_ΕΤΟΣ");
                e.DataField = schoolYearField;
            }
            if (e.PropertyName == "ΔΙΟΙΚΗΤΗΣ")
            {
                DataFormComboBoxField aenAdminField = new DataFormComboBoxField();
                aenAdminField.ItemsSource = GetAenAdmin();
                aenAdminField.Label = "Ο Διοικητής";

                aenAdminField.DisplayMemberPath = "ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ";
                aenAdminField.SelectedValuePath = "ΚΩΔΙΚΟΣ";
                aenAdminField.DataMemberBinding = new Binding("ΔΙΟΙΚΗΤΗΣ");
                e.DataField = aenAdminField;
            }

        }

        private void DataForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ΕΚΠΑΙΔΕΥΤΙΚΟΣ sel_teacher = teacherGrid.SelectedItem as ΕΚΠΑΙΔΕΥΤΙΚΟΣ;
            if (sel_teacher == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει πρώτα να επιλέξετε εκπαιδευτικό από τον πίνακα.");
                e.Cancel = true;
                return;
            }
        }

        private void DataForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                ΕΚΠΑΙΔΕΥΤΙΚΟΣ sel_teacher = teacherGrid.SelectedItem as ΕΚΠΑΙΔΕΥΤΙΚΟΣ;
                if (sel_teacher == null)
                {
                    UserFunctions.ShowAdminMessage("Πρέπει πρώτα να επιλέξετε εκπαιδευτικό από τον πίνακα.");
                    e.Cancel = true;
                    return;
                }

                if (isNewRec == true)
                {
                    var bebeosi = this.DataForm.CurrentItem as ΕΚΠ_ΒΕΒΑΙΩΣΗ;
                    bebeosi.ΑΦΜ = sel_teacher.ΑΦΜ;
                    db.ΕΚΠ_ΒΕΒΑΙΩΣΗs.InsertOnSubmit(bebeosi);
                    cm.CommitData(db);
                    isNewRec = false;
                }
                else
                    cm.CommitData(db);
            }
        }//DataForm_EditEnding

        private void DataForm_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
            // check who is logged-in if not admin show message and cancel edit
            //if (LoginClass.UserKey > 0)
            //{
            //    UserFunctions.ShowAdminMessage("Επεξεργασία δυνατή μόνο από διαχειριστές.");
            //    e.Cancel = true;
            //    return;
            //}
            isNewRec = true;
        }

        private void DataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (LoginClass.UserKey > 0)
            {
                UserFunctions.ShowAdminMessage("Επεξεργασία δυνατή μόνο από διαχειριστές.");
                e.Cancel = true;
                return;
            }

            var bebeosi = this.DataForm.CurrentItem as ΕΚΠ_ΒΕΒΑΙΩΣΗ;
                if ((MessageBox.Show("Είστε σίγουροι ότι θέλετε διαγραφή αυτής της εγγραφής;", this.Title,
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
                {
                    e.Cancel = false;
                    db.ΕΚΠ_ΒΕΒΑΙΩΣΗs.DeleteOnSubmit(bebeosi);
                    cm.CommitData(db);
                }
            }

        private void DataForm_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var bebeosi = this.DataForm.CurrentItem as ΕΚΠ_ΒΕΒΑΙΩΣΗ;
            if (bebeosi == null)
            {
                e.Cancel = false;
                return;
            }

            // validate input fields
            string date = Convert.ToString(bebeosi.ΗΜΕΡΟΜΗΝΙΑ); //null value converted to ""
            // setup RadAlert parameters
            DialogParameters parameters = new DialogParameters();
            parameters.Header = "Σφάλμα";
            parameters.OkButtonContent = "Κλείσιμο";
            e.Cancel = false;

            if (date == "")
            {
                parameters.Content = "Πρέπει να συμπληρωθούν η ημερομηνία.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(bebeosi.ΠΡΩΤΟΚΟΛΛΟ))
            {
                parameters.Content = "Πρέπει να επιλεγεί καθεστώς Προκήρυξης.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(bebeosi.ΠΡΟΣΛΗΨΗ_ΩΣ))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το πεδίο Πρόσληψη ως.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(bebeosi.ΣΤΙΣ_ΕΙΔΙΚΟΤΗΤΕΣ))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το πεδίο Στις ειδικότητες.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }

        } //DataForm_DeletedItem


        #endregion

    }   // class Bebeoseis
}
