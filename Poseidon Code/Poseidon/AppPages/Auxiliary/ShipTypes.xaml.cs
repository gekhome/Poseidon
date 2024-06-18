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

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for ShipTypes.xaml
    /// </summary>
    public partial class ShipTypes : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public ShipTypes()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {

            //source for parent grid
            var categories = from c in db.ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑs
                         orderby c.ΚΑΤΗΓΟΡΙΑ_ΚΩΔ
                         select c;

            //source for child grid
            var subcategories = from sc in db.ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑs
                              orderby sc.ΚΑΤΗΓΟΡΙΑ_ΚΩΔ, sc.ΥΠΟΚΑΤΗΓΟΡΙΑ
                              select sc;

            //convert to ObservableCollection<T> where T is a type (class)
            var ocp = new ObservableCollection<ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑ>(categories.ToList());
            var occ = new ObservableCollection<ΠΛΟΙΟ_ΥΠΟΚΑΤΗΓΟΡΙΑ>(subcategories.ToList());

            parentGrid.ItemsSource = ocp;

            //childGrid.DataSource = occ; // we do not this and it doesn't work

        }

        #region CRUD Events

        private void dataGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            parentGrid.CurrentCellInfo = new GridViewCellInfo(parentGrid.CurrentItem, parentGrid.Columns["ΚΑΤΗΓΟΡΙΑ_ΛΕΚΤΙΚΟ"]);
            parentGrid.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            parentGrid.BeginInsert();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            parentGrid.BeginEdit();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            if (parentGrid.SelectedItems.Count == 0) { return; }
            // verify deletion from user
            string checkMessage = "Να γίνει διαγραφή των επιλεγμένων εγγραφών; " + "\n";

            if (MessageBox.Show(checkMessage, "Διαγραφή", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            { return; }

            UserFunctions.ShowAdminMessage("Η διαδικασία διαγραφής είναι προς το παρόν ακυρωμένη.");
            // proceed with deletion process
            foreach (var row in parentGrid.SelectedItems)
            {
                //ΚΛΑΔΟΣ kladoi = row as ΚΛΑΔΟΣ;
                //db.ΚΛΑΔΟΣs.DeleteOnSubmit(kladoi);
            }
        }

        private void parentGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            // this event is raised when row editing is done in both insert and edit modes
            if (e.EditAction == GridViewEditAction.Cancel)
            {
                return;
            }
            if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                var row = e.Row as GridViewRow;
                ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑ categories = row.Item as ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑ;

                // these two methods do the database udpating
                db.ΠΛΟΙΟ_ΚΑΤΗΓΟΡΙΑs.InsertOnSubmit(categories);
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

        private void parentGrid_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            if (e.Cell.Column.Name == "ΚΑΤΗΓΟΡΙΑ_ΛΕΚΤΙΚΟ")
            {
                string category = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(category))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }
        }

    }   // class ShipTypes
}
