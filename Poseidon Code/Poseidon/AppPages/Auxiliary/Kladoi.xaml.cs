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

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for Kladoi.xaml
    /// </summary>
    public partial class Kladoi : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public Kladoi()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {

            //source for parent grid
            var kladoi = from dbp in db.ΕΚΠ_ΚΛΑΔΟΙs
                         orderby dbp.ΚΛΑΔΟΣ_ΚΩΔ
                         select dbp;

            //source for child grid
            var eidikotites = from dbc in db.ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣs
                              orderby dbc.ΒΑΘΜΙΔΑ, dbc.ΕΙΔΙΚΟΤΗΤΑ
                              select dbc;

            //convert to ObservableCollection<T> where T is a type (class)
            var ocp = new ObservableCollection<ΕΚΠ_ΚΛΑΔΟΙ>(kladoi.ToList());
            var occ = new ObservableCollection<ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ>(eidikotites.ToList());

            parentGrid.ItemsSource = ocp;

            //childGrid.DataSource = occ; // we do not this and it doesn't work

        }

        #region CRUD Events

        private void dataGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            parentGrid.CurrentCellInfo = new GridViewCellInfo(parentGrid.CurrentItem, parentGrid.Columns["ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ"]);
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
                ΕΚΠ_ΚΛΑΔΟΙ kladoi = row.Item as ΕΚΠ_ΚΛΑΔΟΙ;           // cast it to object ΕΤΟΣ_ΑΦΕΙΣΟΔΗΜΑ

                // these two methods do the database udpating
                db.ΕΚΠ_ΚΛΑΔΟΙs.InsertOnSubmit(kladoi);            // insert new row into collection
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
            if (e.Cell.Column.Name == "ΚΛΑΔΟΣ_ΛΕΚΤΙΚΟ")
            {
                string iek_name = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(iek_name))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }
        }

    }
}
