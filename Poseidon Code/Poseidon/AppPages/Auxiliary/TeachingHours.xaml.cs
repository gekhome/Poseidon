using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for TeachingHours.xaml
    /// </summary>
    public partial class TeachingHours : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public TeachingHours()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var teach_hours = from h in db.ΩΡΕΣs
                              select h;

            dataGrid.ItemsSource = teach_hours.ToList();
        }

        #region CRUD functions

        private void dataGrid_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            var row = e.Row as GridViewRow;
            ΩΡΕΣ teach_hour = row.Item as ΩΡΕΣ;

            if (e.EditAction == GridViewEditAction.Cancel)
            {
                return;
            }
            if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                int hour = teach_hour.ΩΡΑ;
                if (Dates.TeachHourExists(hour))
                {
                    LoadData(); // clear the collection from the new row
                    return;
                }

                db.ΩΡΕΣs.InsertOnSubmit(teach_hour);
            }
        }

        private void dataGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            dataGrid.CurrentCellInfo = new GridViewCellInfo(dataGrid.CurrentItem, dataGrid.Columns["txtTeachHour"]);
            dataGrid.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.BeginInsert();
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
                ΩΡΕΣ teach_hour = row as ΩΡΕΣ;
                db.ΩΡΕΣs.DeleteOnSubmit(teach_hour);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.BeginEdit();
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

        #region Validation rules

        private void dataGrid_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.Name == "txtTeachHour")
            {
                string hour = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(hour))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }


            if (e.Cell.Column.Name == "txtStartTime")
            {
                string start = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(start))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }

            if (e.Cell.Column.Name == "txtEndTime")
            {
                string end = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(end))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }
        }

        //private void dataGrid_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        //{
        //    ΣΧΟΛΙΚΟ_ΕΤΟΣ schoolYear = e.Row.DataContext as ΣΧΟΛΙΚΟ_ΕΤΟΣ;

        //    string sy_string = schoolYear.ΣΧΟΛ_ΕΤΟΣ;
        //    DateTime date1 = (DateTime)schoolYear.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ;
        //    DateTime date2 = (DateTime)schoolYear.ΣΧ_ΕΤΟΣ_ΛΗΞΗ;

        //    if (schoolYear.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ == schoolYear.ΣΧ_ΕΤΟΣ_ΛΗΞΗ)
        //    {
        //        GridViewCellValidationResult validationResult = new GridViewCellValidationResult();
        //        validationResult.PropertyName = "txtStartDate";
        //        validationResult.ErrorMessage = "Η έναρξη δεν μπορεί να έχει την ίδια τιμή με την λήξη.";
        //        e.ValidationResults.Add(validationResult);
        //        e.IsValid = false;
        //    }
        //    if (schoolYear.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ > schoolYear.ΣΧ_ΕΤΟΣ_ΛΗΞΗ)
        //    {
        //        GridViewCellValidationResult validationResult = new GridViewCellValidationResult();
        //        validationResult.PropertyName = "txtStartDate";
        //        validationResult.ErrorMessage = "Η έναρξη δεν μπορεί να είναι πιο μετά απο την λήξη.";
        //        e.ValidationResults.Add(validationResult);
        //        e.IsValid = false;
        //    }
        //    if (schoolYear.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ == null || schoolYear.ΣΧ_ΕΤΟΣ_ΛΗΞΗ == null)
        //    {
        //        GridViewCellValidationResult validationResult = new GridViewCellValidationResult();
        //        validationResult.PropertyName = "txtStartDate";
        //        validationResult.ErrorMessage = "Πρέπει να συμπληρωθούν και οι δύο ημερομηνίες.";
        //        e.ValidationResults.Add(validationResult);
        //        e.IsValid = false;
        //    }
        //    if (!Dates.VerifySchoolYear(sy_string, date1, date2))
        //    {
        //        e.IsValid = false;
        //    }
        //}

        #endregion




    }   // class TeachingHours
}
