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
    /// Interaction logic for Eidikotites.xaml
    /// </summary>
    public partial class Eidikotites : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private ObservableCollection<ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ> oc = new ObservableCollection<ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ>();
        private CommitModel cm = new CommitModel();

        public Eidikotites()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {

            var eidikotites = from e in db.ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣs
                              orderby e.ΒΑΘΜΙΔΑ, e.ΕΙΔΙΚΟΤΗΤΑ
                              select e;
            var kladoi = from k in db.ΕΚΠ_ΚΛΑΔΟΙs
                         select k;

            /*
             * oc is the datagrid source and ock the combo data source
            */
            var oc = new ObservableCollection<ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ>(eidikotites.ToList());
            var ock = new ObservableCollection<ΕΚΠ_ΚΛΑΔΟΙ>(kladoi.ToList());

            //dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = oc;
            cbograde.ItemsSource = ock;
        }

        #region CRUD functions

        private void dataGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            dataGrid.CurrentCellInfo = new GridViewCellInfo(dataGrid.CurrentItem, dataGrid.Columns["ΕΙΔΙΚΟΤΗΤΑ"]);
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
                ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ eidikotites = row as ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ;
                db.ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣs.DeleteOnSubmit(eidikotites);
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
                ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ eidikotites = row.Item as ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ;           // cast it to object ΕΚΠ_ΕΚΠ_ΕΙΔΙΚΟΤΗΤΑ

                // these two methods do the database udpating
                db.ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣs.InsertOnSubmit(eidikotites);            // insert new row into collection
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
            if (e.Cell.Column.Name == "ΕΙΔΙΚΟΤΗΤΑ")
            {
                string iek_name = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(iek_name))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
            }
            if (e.Cell.Column.Name == "cbograde")
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
            ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ eidikotita = e.Row.DataContext as ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣ;

            int grade = Convert.ToInt32(eidikotita.ΒΑΘΜΙΔΑ);
            if (grade == 0)
            {
                e.IsValid = false;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί Βαθμίδα (ΠΕ, ΤΕ, ΔΕ).");
            }
        }

        #endregion

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            TeacherEidikotites radwindow = new TeacherEidikotites();
            radwindow.Show();
        }


    }
}
