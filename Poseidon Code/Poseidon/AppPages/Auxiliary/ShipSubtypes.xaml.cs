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
    /// Interaction logic for ShipSubtypes.xaml
    /// </summary>
    public partial class ShipSubtypes : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public ShipSubtypes()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            var subcategories = from s in db.ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑs
                                orderby s.ΚΑΤΗΓΟΡΙΑ_ΚΩΔ, s.ΥΠΟΚΑΤΗΓΟΡΙΑ
                                select s;

            var categories = from k in db.ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑs
                         select k;

            /*
             * oc is the datagrid source and ock the combo data source
            */
            var oc = new ObservableCollection<ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ>(subcategories.ToList());
            var ock = new ObservableCollection<ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑ>(categories.ToList());

            //dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = oc;
            cbocategory.ItemsSource = ock;
        }

        #region CRUD functions

        private void dataGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            dataGrid.CurrentCellInfo = new GridViewCellInfo(dataGrid.CurrentItem, dataGrid.Columns["ΥΠΟΚΑΤΗΓΟΡΙΑ"]);
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
                ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ subcategories = row as ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ;
                db.ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑs.DeleteOnSubmit(subcategories);
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
                ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ subcategories = row.Item as ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ;           // cast it to object ΕΚΠ_ΕΚΠ_ΕΙΔΙΚΟΤΗΤΑ

                // these two methods do the database udpating
                db.ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑs.InsertOnSubmit(subcategories);            // insert new row into collection
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
            /*
            string checkMessage = "Να γίνει αποθήκευση των μεταβολών; " + "\n";
            if (WPFMessageBox.Show(checkMessage, "Αποθήκευση", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
            { return; }
            */
            cm.CommitData(db);
            LoadData();
        }

        #endregion

        #region Validations

        private void dataGrid_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.Name == "ΥΠΟΚΑΤΗΓΟΡΙΑ")
            {
                string subcategory = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(subcategory))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }
            if (e.Cell.Column.Name == "cbocategory")
            {
                int grade = Convert.ToInt32(e.NewValue.ToString());

                if (grade == 0)
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }
        }

        private void dataGrid_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
            ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ subcategory = e.Row.DataContext as ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ;

            int main_category = Convert.ToInt32(subcategory.ΚΑΤΗΓΟΡΙΑ_ΚΩΔ);
            if (main_category == 0)
            {
                e.IsValid = false;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί η κύρια κατηγορία.");
            }
        }

        #endregion

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            ShipTypesUI radwindow = new ShipTypesUI();
            radwindow.Show();
        }


    }   // class ShipSubtypes
}
