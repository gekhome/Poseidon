using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;
using System.ComponentModel;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;
using Poseidon.Controls;

namespace Poseidon.AppPages.Teachers
{
    /// <summary>
    /// Interaction logic for Teachers.xaml
    /// </summary>
    public partial class Teachers : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();
        private bool isNewRec = false;

        public Teachers()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // this loads the data of the grid and form.

            var trainers = from dbp in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           orderby dbp.ΕΠΩΝΥΜΟ, dbp.ΟΝΟΜΑ
                           select dbp;
            teacherGrid.ItemsSource = trainers.ToList();
            teacherForm.ItemsSource = trainers.ToList();

        }

        #region DataForm events

        private void teacherForm_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΦΜ", "Α.Φ.Μ. :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΟΥ", "Δ.Ο.Υ. :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΜΚΑ", "Α.Μ.Κ.Α. :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΕΠΩΝΥΜΟ", "Επώνυμο :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΟΝΟΜΑ", "Όνομα :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΑΤΡΩΝΥΜΟ", "Πατρώνυμο :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΜΗΤΡΩΝΥΜΟ", "Μητρώνυμο :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΟΥ", "Δ.Ο.Υ. :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΦΥΛΟ", "Φύλο  :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΔΤ", "Α.Δ.Τ. :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΗΜΝΙΑ_ΓΕΝΝΗΣΗ", "Ημ/νία γέννησης :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΝΣΗ_ΟΔΟΣ", "Διεύθυνση-Οδός, Αρ. :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΝΣΗ_ΤΚ", "Διεύθυνση-Τ.Κ. :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΝΣΗ_ΠΟΛΗ", "Διεύθυνση-Πόλη :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΝΣΗ_ΤΚ", "Διεύθυνση Τ.Κ. :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("EMAIL", "E-mail :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΤΗΛΕΦΩΝΟ1", "Τηλέφωνο (1) :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΤΗΛΕΦΩΝΟ2", "Τηλέφωνο (2) :");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΑΡΑΤΗΡΗΣΕΙΣ", "Παρατηρήσεις :");


            if (e.PropertyName == "ΦΥΛΟ")
            {
                DataFormComboBoxField sexField = new DataFormComboBoxField();
                sexField.ItemsSource = GetSex();
                sexField.Label = "Φύλο :";

                sexField.DisplayMemberPath = "ΦΥΛΟ";
                sexField.SelectedValuePath = "ΦΥΛΟ_ΚΩΔ";
                sexField.DataMemberBinding = new Binding("ΦΥΛΟ");
                e.DataField = sexField;
            }
        }

        private void teacherForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            var teacher = this.teacherForm.CurrentItem as ΕΚΠΑΙΔΕΥΤΙΚΟΣ;
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                if (isNewRec == true)
                {
                    if (ValidateInsertTeacher(teacher.ΑΦΜ))
                    {
                        db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs.InsertOnSubmit(teacher);
                        cm.CommitData(db);
                        ReloadData();       // refresh display
                        // need to reset it otherwise InsertOnSubmit throws an exception if we edit immediately the new record.
                        isNewRec = false;
                    }
                    // force a cancellation so that new erroneous record does not show in collection.
                    else teacherForm.CancelEdit();
                }
                else // editing an existing record
                {
                    //UserFunctions.ShowAdminMessage("Οι αλλαγές που έγιναν θα αποθηκευτούν.");
                    cm.CommitData(db);
                    ReloadData();       // refresh display
                }
            }
        }//teacherForm_EditEnding

        private void teacherForm_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
            //RadWindow.Alert("AddingNewItem is raised.");
            isNewRec = true;
        }

        private void teacherForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
            // RadWindow.Alert("DeletedItem Event fired.");
        }
        private void teacherForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var teacher = this.teacherForm.CurrentItem as ΕΚΠΑΙΔΕΥΤΙΚΟΣ;

            if ((MessageBox.Show("Είστε σίγουροι ότι θέλετε διαγραφή αυτής της εγγραφής;", this.Title,
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                if (ValidateDeleteTeacher(teacher.ΑΦΜ))
                {
                    e.Cancel = false;
                    db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs.DeleteOnSubmit(teacher);
                    cm.CommitData(db);
                    ReloadData();       // refresh display
                }
                else
                {
                    UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει διαγραφή. Υπάρχουν συσχετισμένες αναθέσεις.");
                    e.Cancel = true;
                }
            }
            else // user cancelled delete
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Η διαγραφή ακυρώθηκε από τον χρήστη.");
            }


        } //DataFormProk_DeletingItem


        // Sex combo data source
        private List<ΦΥΛΑ> GetSex()
        {
            List<ΦΥΛΑ> sexList = new List<ΦΥΛΑ>();

            var sex = from s in db.ΦΥΛΑs
                      select s;
            return sexList = sex.ToList();
        }

        #endregion

        #region validations

        /* -------------------
         * Κάνει έλεγχο αν υπάρχουν αιτήσεις συσχετισμένες
         * με το ΑΦΜ που πρόκειται να διαγραφεί.
         * Εάν υπάρχουν η διαγραφή ακυρώνεται.
         * -------------------
         */
        private bool ValidateDeleteTeacher(string safm)
        {

            // έλεγχος αν υπάρχουν αναθέσεις για το συγκεκριμένο ΑΦΜ
            var ac1 = (from c in db.ΑΝΑΘΕΣΕΙΣ_ΣΜs
                      where c.ΑΦΜ == safm
                      select c).Count();

            var ac2 = (from c in db.ΑΝΑΘΕΣΕΙΣ_ΣΠs
                       where c.ΑΦΜ == safm
                       select c).Count();

            if (safm == null || safm == "")
            {
                return false;
            }
            if (ac1 != 0 || ac2 != 0) return false;
            else return true;

        }

        /* -----------------
         * Κάνει έλεγχο αν το ΑΦΜ υπάρχει ήδη στη βάση δεδομένων.
         * Χρησιμοποιείται από τις λειτουργίες Edit και Insert.
         * Εάν υπάρχει η επεξεργασία ακυρώνεται.
         * -----------------
         */
        private bool ValidateInsertTeacher(string safm)
        {
            if (string.IsNullOrWhiteSpace(safm))
            {
                UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει εισαγωγή διότι το ΑΦΜ είναι κενό.");
                return false;
            }
            // έλεγχος αν το ΑΦΜ υπάρχει ήδη καταχωρημένο
            var recs = (from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                        where t.ΑΦΜ == safm
                        select t).Count();

            if (recs != 0)
            {
                UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει εισαγωγή διότι το ΑΦΜ υπάρχει ήδη.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Αυτό χρειάζεται γιατί ένα εσφαλμένο ΑΦΜ σε edit εξακολουθεί να
        /// φαίνεται στο collection (αν και δεν καταχωρείται στη βάση).
        /// Το QuerableCollectionView εξασφαλίζει το συγχρονισμό δύο Grids
        /// ή Grid και Form.
        /// </summary>
        private void ReloadData()
        {
            //ICollectionView teachers = null;
            var trainers = from dbp in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           orderby dbp.ΕΠΩΝΥΜΟ, dbp.ΟΝΟΜΑ
                           select dbp;
            //teachers = new QueryableCollectionView(trainers.ToList());
            teacherGrid.ItemsSource = trainers.ToList();
            teacherForm.ItemsSource = trainers.ToList();
        }

        #endregion

        #region DataForm Fields Validation

        /// <summary>
        /// Εδώ γίνεται η επικύρωση των δεδομένων που έχει εισάγει ο χρήστης.
        /// Οι κανόνες επικύρωσης είναι:
        /// 1. Το ΑΦΜ έγκυρο και όχι κενό.
        /// 2. Επώνυμο, όνομα, πατρώνυμο, μητρώνυμο όχι κενά.
        /// 3. Η ημερομηνία γέννησης συμπληρωμένη και έγκυρη.
        /// 4. Το φύλλο όχι κενό.
        /// 5. ΑΔΤ συμπληρωμένο, όχι κενό
        /// 6. Ένα τουλάχιστον τηλέφωνο συμπληρωμένο.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void teacherForm_ValidatingItem(object sender, CancelEventArgs e)
        {
            var _teacher = this.teacherForm.CurrentItem as ΕΚΠΑΙΔΕΥΤΙΚΟΣ;
            if (_teacher == null)
            {
                e.Cancel = false;
                return;
            }
            string _birthDate = Convert.ToString(_teacher.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ);    //null value converted to ""
            // acceptable age limits
            int _minAge = 20;
            int _maxAge = 100;

            // validate ΑΦΜ (not empty and valid)
            if (!afmRule.CheckAFM(_teacher.ΑΦΜ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Το ΑΦΜ δεν είναι έγκυρο.");
                return;
            }
            // validate lastname or firstname (not empty, not null)
            if (string.IsNullOrWhiteSpace(_teacher.ΕΠΩΝΥΜΟ) || string.IsNullOrWhiteSpace(_teacher.ΟΝΟΜΑ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί επώνυμο, όνομα.");
                return;
            }

            if (string.IsNullOrWhiteSpace(_teacher.ΜΗΤΡΩΝΥΜΟ) || string.IsNullOrWhiteSpace(_teacher.ΠΑΤΡΩΝΥΜΟ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί πατρώνυμο, μητρώνυμο.");
                return;
            }


            // validate birthdate
            if (string.IsNullOrWhiteSpace(_birthDate))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί η ημερομηνία γέννησης.");
                return;
            }
            if (!BirthDateRule.ValidBirthDate(_birthDate, _minAge, _maxAge))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Η ημερομηνία γέννησης είναι εκτός έγκυρων ορίων.");
                return;
            }
            // validate Sex
            if (_teacher.ΦΥΛΟ == null)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή φύλου.");
                return;
            }           
            // validate ΑΔΤ (not empty)
            if (string.IsNullOrWhiteSpace(_teacher.ΑΔΤ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί ο Αρ. Δελτίου Ταυτότητας.");
                return;
            }
            // validate ΕΡΓΑΣΙΑ (not null)
            if (_teacher.ΕΡΓΑΣΙΑ == null)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή εργασίας (Μ/Ε).");
                return;
            }  
            // validate Phone Number (at least one entered)
            if (string.IsNullOrWhiteSpace(_teacher.ΤΗΛΕΦΩΝΟ1) && string.IsNullOrWhiteSpace(_teacher.ΤΗΛΕΦΩΝΟ2))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί ένα τουλάχιστον τηλέφωνο.");
                return;
            }
        }

        private void teacherGrid_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            ΕΚΠΑΙΔΕΥΤΙΚΟΣ selectedTrainer = teacherGrid.SelectedItem as ΕΚΠΑΙΔΕΥΤΙΚΟΣ;

            if (selectedTrainer != null)
            {
                var trainer = from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                              where t.ΑΦΜ == selectedTrainer.ΑΦΜ
                              select t;

                teacherForm.ItemsSource = trainer.ToList();
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
                teacherForm.ItemsSource = trainers.ToList();
            }
        }

        private void btnFilterOff_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = null;
            var trainers = from dbp in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           orderby dbp.ΕΠΩΝΥΜΟ, dbp.ΟΝΟΜΑ
                           select dbp;

            teacherGrid.ItemsSource = trainers.ToList();
            teacherForm.ItemsSource = trainers.ToList();

            // select 1st row and not a random default
            //teacherGrid.SelectedItem = teacherGrid.Items[0];
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnFilterOn_Click(sender, e);
            }
        }

        #endregion

    }   // class Teachers
}
