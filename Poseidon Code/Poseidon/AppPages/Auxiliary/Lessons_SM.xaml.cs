using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Linq;
using System.Windows;
using System.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Poseidon.Model;
using Poseidon.Utilities;
using System.ComponentModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Poseidon.DataModel;
using Poseidon.Controls;
using Poseidon.AppPages.Printouts;

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for Lessons_SM.xaml
    /// </summary>
    public partial class Lessons_SM : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public Lessons_SM()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var term = from t in db.ΕΞΑΜΗΝΑs
                       select t;
            cboTerm.ItemsSource = term.ToList();

            var lessons = from l in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                          orderby l.ΜΑΘΗΜΑ
                          select l;
            lessonsGrid.ItemsSource = lessons.ToList();
        }

        #region CRUD events

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!IsSelected()) return;

            if (lessonsGrid.SelectedItems.Count == 0) { return; }
            // verify deletion from user
            string checkMessage = "Να γίνει διαγραφή των επιλεγμένων εγγραφών; " + "\n";

            if (MessageBox.Show(checkMessage, "Διαγραφή", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            { return; }

            // proceed with deletion process
            foreach (var row in lessonsGrid.SelectedItems)
            {
                ΜΑΘΗΜΑΤΑ_ΣΜ lessons = row as ΜΑΘΗΜΑΤΑ_ΣΜ;
                db.ΜΑΘΗΜΑΤΑ_ΣΜs.DeleteOnSubmit(lessons);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!IsSelected()) return;
            lessonsGrid.BeginInsert();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!IsSelected()) return;
            lessonsGrid.BeginEdit();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            ShowSelected();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cm.UserCanUpdate() == false)
            {
                LoadData();
                return;
            }
            /*
            string checkMessage = "Να γίνει αποθήκευση των μεταβολών; " + "\n";
            if (WPFMessageBox.Show(checkMessage, "Αποθήκευση", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
            { return; }
            */
            cm.CommitData(db);
            ShowSelected();
        }

        #endregion

        #region LOB functions

        private void eidikotitaGrid_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            ShowSelected();
        }

        private void cboTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowSelected();
        }

        private void ShowSelected()
        {
            ΕΞΑΜΗΝΑ selected_term = cboTerm.SelectedItem as ΕΞΑΜΗΝΑ;

            // make sure that something is selected in the two "filter grids", if the user hasn't made a selection,
            // otherwise we get a null exception.

            if (selected_term == null)
            {
                selected_term = ((ObservableCollection<ΕΞΑΜΗΝΑ>)cboTerm.ItemsSource).First();
            }

            var lessons = from dbe in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                          where dbe.ΕΞΑΜΗΝΟ == selected_term.ΕΞΑΜΗΝΟ_ΚΩΔ
                          orderby dbe.ΜΑΘΗΜΑ
                          select dbe;

            ObservableCollection<ΜΑΘΗΜΑΤΑ_ΣΜ> oce = new ObservableCollection<ΜΑΘΗΜΑΤΑ_ΣΜ>(lessons.ToList());
            lessonsGrid.ItemsSource = oce;
        }

        private void lessonsGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {

            if (!IsSelected())
            {
                e.Cancel = true;
                return;
            }

            lessonsGrid.CurrentCellInfo = new GridViewCellInfo(lessonsGrid.CurrentItem, lessonsGrid.Columns["stcw_fct"]);
            lessonsGrid.Focus();
        }

        private void lessonsGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            ΕΞΑΜΗΝΑ selected_term = cboTerm.SelectedItem as ΕΞΑΜΗΝΑ;

            // this event is raised when row editing is done in both insert and edit modes
            if (e.EditAction == GridViewEditAction.Cancel)
            {
                return;
            }
            if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                var row = e.Row as GridViewRow;
                ΜΑΘΗΜΑΤΑ_ΣΜ lesson = row.Item as ΜΑΘΗΜΑΤΑ_ΣΜ;

                // set the PK values by code (ΠΡΟΚ_ΙΕΚ_ΕΙΔΙΚΟΤΗΤΑ is an all-key relation)
                // these two values are not available in the UI.
                lesson.ΕΞΑΜΗΝΟ = selected_term.ΕΞΑΜΗΝΟ_ΚΩΔ;

                if (lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ == null) lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ = 0;
                if (lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ == null) lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ = 0;
                if (lesson.ΩΡΕΣ_ΕΒΔ == null) lesson.ΩΡΕΣ_ΕΒΔ = 0;
                lesson.ΣΥΝΟΛΟ_ΩΡΕΣ = Convert.ToInt16(lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ + lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ);

                // check for PK constraint before insert (sql exception)
                //if (!ValidateInsertLesson(lesson))
                //{
                //    LoadData(); // refresh the collection
                //    return;     // do not insert
                //}

                // these two methods do the database udpating
                db.ΜΑΘΗΜΑΤΑ_ΣΜs.InsertOnSubmit(lesson);
            }
            else if (e.EditOperationType == GridViewEditOperationType.Edit)
            {
                var row = e.Row as GridViewRow;
                ΜΑΘΗΜΑΤΑ_ΣΜ lesson = row.Item as ΜΑΘΗΜΑΤΑ_ΣΜ;

                lesson.ΕΞΑΜΗΝΟ = selected_term.ΕΞΑΜΗΝΟ_ΚΩΔ;

                if (lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ == null) lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ = 0;
                if (lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ == null) lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ = 0;
                if (lesson.ΩΡΕΣ_ΕΒΔ == null) lesson.ΩΡΕΣ_ΕΒΔ = 0;
                lesson.ΣΥΝΟΛΟ_ΩΡΕΣ = Convert.ToInt16(lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ + lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ);
            }
        }

        private void lessonsGrid_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            if (!IsSelected())
            {
                e.Cancel = true;
                return;
            }
        }

        private void lessonsGrid_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            if (!IsSelected())
            {
                e.Cancel = true;
                return;
            }
        }

        private bool IsSelected()
        {
            ΕΞΑΜΗΝΑ selected_term = cboTerm.SelectedItem as ΕΞΑΜΗΝΑ;

            if (selected_term == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί Εξάμηνο.");
                return false;
            }
            else return true;
        }

        private bool ValidateInsertLesson(ΜΑΘΗΜΑΤΑ_ΣΠ lesson)
        {
            var recs = (from p in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                        where p.ΕΞΑΜΗΝΟ == lesson.ΕΞΑΜΗΝΟ &&
                              p.ΚΩΔΙΚΟΣ == lesson.ΚΩΔΙΚΟΣ
                        select p).Count();
            if (recs != 0)
            {
                UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει εισαγωγή διότι η καταχώρηση υπάρχει ήδη.");
                return false;
            }
            return true;
        }

        #endregion

        #region Validations

        private void lessonsGrid_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.Name == "lesson")
            {
                string strlesson = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(strlesson))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }


        } // class lessonGrid_CellValidating

        private void lessonsGrid_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {

            ΜΑΘΗΜΑΤΑ_ΣΜ lesson_row = e.Row.DataContext as ΜΑΘΗΜΑΤΑ_ΣΜ;

            if (String.IsNullOrEmpty(lesson_row.ΜΑΘΗΜΑ))
            {
                GridViewCellValidationResult validationResult = new GridViewCellValidationResult();
                validationResult.ErrorMessage = "Πρέπει να εισαχθεί το μάθημα.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
                return;
            }

            // ο χρήστης δεν έβαλε ώρες ούτε Θ ούτε Ε
            if (lesson_row.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ == null && lesson_row.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ == null)
            {
                GridViewCellValidationResult validationResult = new GridViewCellValidationResult();
                validationResult.ErrorMessage = "Πρέπει να εισαχθούν ώρες Θεωρίας ή/και Εφαρμογών.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
                return;
            }

            if (lesson_row.ΩΡΕΣ_ΕΒΔ == null)
            {
                GridViewCellValidationResult validationResult = new GridViewCellValidationResult();
                validationResult.ErrorMessage = "Πρέπει να εισαχθούν οι ώρες/εβδομάδα.";
                e.ValidationResults.Add(validationResult);
                e.IsValid = false;
                return;
            }

        }

        #endregion

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            LessonsSM radwindow = new LessonsSM();
            radwindow.Show();
        }






    }   // class Lessons_SM
}
