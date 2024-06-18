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

namespace Poseidon.AppPages.Students
{
    /// <summary>
    /// Interaction logic for StudentData.xaml
    /// </summary>
    public partial class StudentData : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();
        private bool isNewRec = false;

        public StudentData()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // this loads the data of the grid and form.

            var students = from dbp in db.ΣΠΟΥΔΑΣΤΗΣs
                           orderby dbp.ΕΠΩΝΥΜΟ, dbp.ΟΝΟΜΑ
                           select dbp;
            DataForm.ItemsSource = students.ToList();

        }


        #region filter functions

        private void btnFilterOn_Click(object sender, RoutedEventArgs e)
        {
            if (!(txtSearch.Text == null || txtSearch.Text == ""))
            {
                var students = from t in db.ΣΠΟΥΔΑΣΤΗΣs
                               where t.ΕΠΩΝΥΜΟ.Contains(txtSearch.Text) || t.ΑΜΚΑ.Contains(txtSearch.Text)
                               select t;

                if (students.Count() == 0) UserFunctions.ShowAdminMessage("Δεν βρέθηκε καταχώρηση.");

                DataForm.ItemsSource = students.ToList();
            }

        }

        private void btnFilterOff_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = null;
            LoadData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnFilterOn_Click(sender, e);
            }
        }

        #endregion

        #region DataForm functions

        private void DataForm_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
            isNewRec = true;
        }

        private void DataForm_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void DataForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            var student = this.DataForm.CurrentItem as ΣΠΟΥΔΑΣΤΗΣ;
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                if (isNewRec == true)
                {
                    if (ValidateInsertStudent(student.ΑΜΚΑ))
                    {
                        db.ΣΠΟΥΔΑΣΤΗΣs.InsertOnSubmit(student);
                        cm.CommitData(db);
                        //ReloadData();       // refresh display
                        // need to reset it otherwise InsertOnSubmit throws an exception if we edit immediately the new record.
                        isNewRec = false;
                    }
                    // force a cancellation so that new erroneous record does not show in collection.
                    else DataForm.CancelEdit();
                }
                else // editing an existing record
                {
                    //UserFunctions.ShowAdminMessage("Οι αλλαγές που έγιναν θα αποθηκευτούν.");
                    cm.CommitData(db);
                }
            }
        }

        private void DataForm_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var student = this.DataForm.CurrentItem as ΣΠΟΥΔΑΣΤΗΣ;

            if ((MessageBox.Show("Είστε σίγουροι ότι θέλετε διαγραφή αυτής της εγγραφής;", this.Title,
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                if (ValidateDeleteStudent(student.ΑΜΚΑ))
                {
                    e.Cancel = false;
                    db.ΣΠΟΥΔΑΣΤΗΣs.DeleteOnSubmit(student);
                    cm.CommitData(db);
                    //ReloadData();       // refresh display
                }
                else
                {
                    UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει διαγραφή. Υπάρχουν συσχετισμένες Εγγραφές.");
                    e.Cancel = true;
                }
            }
            else // user cancelled delete
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Η διαγραφή ακυρώθηκε από τον χρήστη.");
            }

        }


        private void DataForm_ValidatingItem(object sender, CancelEventArgs e)
        {
            var _student = this.DataForm.CurrentItem as ΣΠΟΥΔΑΣΤΗΣ;
            if (_student == null)
            {
                e.Cancel = false;
                return;
            }
            string _birthDate = Convert.ToString(_student.ΗΜΝΙΑ_ΓΕΝΝΗΣΗ);    //null value converted to ""
            // acceptable age limits
            //int _minAge = 18;
            //int _maxAge = 28;

            // validate ΑΜΚΑ (not empty and valid)
            if (string.IsNullOrWhiteSpace(_student.ΑΜΚΑ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Το AMKA δεν πρέπει να είναι κενό.");
                return;
            }

            // validate lastname or firstname (not empty, not null)
            if (string.IsNullOrWhiteSpace(_student.ΕΠΩΝΥΜΟ) || string.IsNullOrWhiteSpace(_student.ΟΝΟΜΑ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί επώνυμο, όνομα.");
                return;
            }

            if (string.IsNullOrWhiteSpace(_student.ΜΗΤΡΩΝΥΜΟ) || string.IsNullOrWhiteSpace(_student.ΠΑΤΡΩΝΥΜΟ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί πατρώνυμο, μητρώνυμο.");
                return;
            }

            //// validate birthdate
            //if (string.IsNullOrWhiteSpace(_birthDate))
            //{
            //    e.Cancel = true;
            //    UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί η ημερομηνία γέννησης.");
            //    return;
            //}
            //if (!BirthDateRule.ValidBirthDate(_birthDate, _minAge, _maxAge))
            //{
            //    e.Cancel = true;
            //    UserFunctions.ShowAdminMessage("Η ημερομηνία γέννησης είναι εκτός έγκυρων ορίων.");
            //    return;
            //}

            // validate Sex
            if (_student.ΦΥΛΟ == null)
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή φύλου.");
                return;
            }
            // validate ΑΔΤ (not empty)
            if (string.IsNullOrWhiteSpace(_student.ΑΔΤ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί ο Αρ. Δελτίου Ταυτότητας.");
                return;
            }

            // validate Phone Number (at least one entered)
            if (string.IsNullOrWhiteSpace(_student.ΤΗΛΕΦΩΝΑ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί ένα τουλάχιστον τηλέφωνο.");
                return;
            }
        }

        #endregion

        #region Validations

        private bool ValidateDeleteStudent(string amka)
        {
            // έλεγχος αν υπάρχουν Εγγραφές για το συγκεκριμένο ΑΜΚΑ
            var ac1 = (from c in db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs
                       where c.ΑΜΚΑ == amka
                       select c).Count();

            if (amka == null || amka == "")
            {
                return false;
            }
            if (ac1 != 0)  return false;
            else return true;

        }

        private bool ValidateInsertStudent(string amka)
        {
            if (string.IsNullOrWhiteSpace(amka))
            {
                UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει εισαγωγή διότι το AMKA είναι κενό.");
                return false;
            }
            // έλεγχος αν το ΑΦΜ υπάρχει ήδη καταχωρημένο
            var recs = (from t in db.ΣΠΟΥΔΑΣΤΗΣs
                        where t.ΑΜΚΑ == amka
                        select t).Count();

            if (recs != 0)
            {
                UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει εισαγωγή διότι το AMKA υπάρχει ήδη.");
                return false;
            }
            return true;
        }

        #endregion
    }
}
