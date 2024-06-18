using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Poseidon.Model;
using Poseidon.DataModel;
using Poseidon.Utilities;
using Poseidon.AppPages.Printouts;

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for Classes_SP.xaml
    /// </summary>
    public partial class Classes_SP : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public Classes_SP()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            var school_years = from e in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                               orderby e.ΣΧΟΛ_ΕΤΟΣ descending
                               select e;
            cboSchoolYear.ItemsSource = school_years.ToList();

            var terms = from k in db.ΕΞΑΜΗΝΑs
                        select k;
            cboTerm.ItemsSource = terms.ToList();

            var classes = from t in db.ΤΜΗΜΑΤΑ_ΣΠs
                          orderby t.ΔΙΔ_ΕΤΟΣ descending, t.ΕΞΑΜΗΝΟ
                          select t;

            //dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = classes.ToList();
        }

        #region CRUD functions

        private void dataGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            dataGrid.CurrentCellInfo = new GridViewCellInfo(dataGrid.CurrentItem, dataGrid.Columns["cboSchoolYear"]);
            dataGrid.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.BeginInsert();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.BeginEdit();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (dataGrid.SelectedItems.Count == 0) { return; }
            // verify deletion from user
            string checkMessage = "Να γίνει διαγραφή των επιλεγμένων εγγραφών; " + "\n";

            if (MessageBox.Show(checkMessage, "Διαγραφή", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            { return; }

            // proceed with deletion process

            foreach (var row in dataGrid.SelectedItems)
            {
                ΤΜΗΜΑΤΑ_ΣΠ tmima = row as ΤΜΗΜΑΤΑ_ΣΠ;
                if (ValidateDeleteClass(tmima))
                {
                    db.ΤΜΗΜΑΤΑ_ΣΠs.DeleteOnSubmit(tmima);
                }
            }
        }

        private void dataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            // this event is raised when row editing is done in both insert and edit modes
            if (e.EditAction == GridViewEditAction.Cancel)
            {
                return;
            }
            if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                var row = e.Row as GridViewRow;
                ΤΜΗΜΑΤΑ_ΣΠ tmima = row.Item as ΤΜΗΜΑΤΑ_ΣΠ;

                // these two methods do the database udpating
                db.ΤΜΗΜΑΤΑ_ΣΠs.InsertOnSubmit(tmima);            // insert new row into collection
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            // load the existing collection without new changes (no submit is done unless Save is pressed)
            LoadData();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cm.UserCanUpdate() == false)
            {
                LoadData();
                return;
            }
            cm.CommitData(db);
            LoadData();
        }

        #endregion

        #region Validations

        private void dataGrid_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.Name == "cboSchoolYear")
            {
                int syear = Convert.ToInt32(e.NewValue.ToString());
                if (syear == 0)
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }

            if (e.Cell.Column.Name == "cboTerm")
            {
                int term = Convert.ToInt32(e.NewValue.ToString());

                if (term == 0)
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }
            if (e.Cell.Column.Name == "ΤΜΗΜΑ")
            {
                string name = Convert.ToString(e.NewValue.ToString());
                if (String.IsNullOrWhiteSpace(name))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }
        }

        private void dataGrid_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
            ΤΜΗΜΑΤΑ_ΣΠ tmima = e.Row.DataContext as ΤΜΗΜΑΤΑ_ΣΠ;

            int _schoolyear = (int)tmima.ΔΙΔ_ΕΤΟΣ;
            if (_schoolyear == 0)
            {
                e.IsValid = false;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί διδακτικό έτος.");
            }

            int _term = (int)tmima.ΕΞΑΜΗΝΟ;
            if (_term == 0)
            {
                e.IsValid = false;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί εξάμηνο.");
            }

            string name = tmima.ΤΜΗΜΑ_ΟΝΟΜΑΣΙΑ;
            if (String.IsNullOrWhiteSpace(name))
            {
                e.IsValid = false;
                UserFunctions.ShowAdminMessage("Πρέπει να εισαχθεί ονομασία του τμήματος.");
            }

        }

        private bool ValidateDeleteClass(ΤΜΗΜΑΤΑ_ΣΠ tmima)
        {
            return true;
        }

        #endregion




    }   // class Classes_SP
}
