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

namespace Poseidon.AppPages.Students.SM
{
    /// <summary>
    /// Interaction logic for Ptixiaki.xaml
    /// </summary>
    public partial class Ptixiaki : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        private int syear = 0;
        private int admin = 0;
        private int tmima = 0;
        private int Sxoli = 2;

        public Ptixiaki()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var syears = from sy in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         orderby sy.ΣΧΟΛ_ΕΤΟΣ descending
                         select sy;
            cboSchoolYear.ItemsSource = syears.ToList();

            var adminList = from a in db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs
                        orderby a.ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select a;
            cboDioikitis.ItemsSource = adminList.ToList();

            var tmimata = from t in db.ΤΜΗΜΑΤΑ_ΣΜs
                          select t;
            cboTmima.ItemsSource = tmimata.ToList();

            var students = from s in db.ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΜs
                           orderby s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                           select s;
            cboStudent1.ItemsSource = students.ToList();
            cboStudent2.ItemsSource = students.ToList();
            cboStudent3.ItemsSource = students.ToList();

            var teachers = from te in db.ViewΕΚΠΑΙΔΕΥΤΙΚΟΙs
                           orderby te.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                           select te;
            cboTeacher.ItemsSource = teachers.ToList();

            var result = from r in db.ΠΤΥΧΙΑΚΗ_ΑΠΟΤΕΛΕΣΜΑs
                         select r;
            cboResult.ItemsSource = result.ToList();

            dataGrid.ItemsSource = null;

        }

        #region Selection Combos

        private void cboSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (!cboSchoolYear.IsLoaded) return;

            ΣΧΟΛΙΚΟ_ΕΤΟΣ school_year = cboSchoolYear.SelectedItem as ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            if (school_year == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί διδακτικό έτος.");
                return;
            }

            syear = school_year.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ;

            var tmimata = from t in db.ΤΜΗΜΑΤΑ_ΣΜs
                          where t.ΔΙΔ_ΕΤΟΣ == syear
                          select t;
            cboTmima.ItemsSource = tmimata.ToList();
        }

        private void cboDioikitis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboDioikitis.IsLoaded) return;
            ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ dioikitis = cboDioikitis.SelectedItem as ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ;

            if (dioikitis == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί ο Διοικητής.");
                return;
            }

            admin = dioikitis.ΚΩΔΙΚΟΣ;

        }

        private void cboTmima_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboTmima.IsLoaded) return;

            ΤΜΗΜΑΤΑ_ΣΜ tmimata = cboTmima.SelectedItem as ΤΜΗΜΑΤΑ_ΣΜ;

            if (tmimata == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί τμήμα.");
                return;
            }

            tmima = (int)tmimata.ΤΜΗΜΑ_ΚΩΔ;

            // set data source to students of the selected class
            var spoudastes = from s in db.ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΜs
                             where s.ΤΜΗΜΑ == tmima
                             orderby s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                             select s;
            cboStudent1.ItemsSource = spoudastes.ToList();
            cboStudent2.ItemsSource = spoudastes.ToList();
            cboStudent3.ItemsSource = spoudastes.ToList();

            var ptixiakes = from p in db.ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗs
                            where p.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == syear && p.ΤΜΗΜΑ == tmima && p.ΣΧΟΛΗ == Sxoli
                            orderby p.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ descending, p.ΤΜΗΜΑ
                            select p;
            dataGrid.ItemsSource = ptixiakes.ToList();

        }

        #endregion


        #region Button Events

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatePrimaryFields()) return;

            dataGrid.BeginInsert();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatePrimaryFields()) return;

            if (dataGrid.SelectedItems.Count == 0) { return; }
            // verify deletion from user
            string checkMessage = "Να γίνει διαγραφή των επιλεγμένων εγγραφών; " + "\n";

            if (MessageBox.Show(checkMessage, "Διαγραφή", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            { return; }

            // proceed with deletion process
            foreach (var row in dataGrid.SelectedItems)
            {
                ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ ptixiaki = row as ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ;
                db.ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗs.DeleteOnSubmit(ptixiaki);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatePrimaryFields()) return;

            dataGrid.BeginEdit();

        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatePrimaryFields()) return;

            if (cm.UserCanUpdate() == false)
            {
                RefreshData();
                return;
            }
            cm.CommitData(db);
            //RefreshData();

        }

        #endregion

        #region Data Grid Events

        private void dataGrid_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            if (!ValidatePrimaryFields()) return;

            dataGrid.CurrentCellInfo = new GridViewCellInfo(dataGrid.CurrentItem, dataGrid.Columns["cboTeacher"]);
            dataGrid.Focus();

        }

        private void dataGrid_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        {
            var row = e.Row as GridViewRow;
            ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ ptixiaki = row.Item as ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ;

            if (!ValidatePrimaryFields()) return;

            ptixiaki.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
            ptixiaki.ΔΙΟΙΚΗΤΗΣ = admin;
            ptixiaki.ΤΜΗΜΑ = tmima;
            ptixiaki.ΣΧΟΛΗ = Sxoli;

        }

        private void dataGrid_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            // this event is raised when row editing is done in both insert and edit modes
            if (e.EditAction == GridViewEditAction.Cancel)
            {
                return;
            }
            if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                var row = e.Row as GridViewRow;
                ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ ptixiaki_row = row.Item as ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ;

                if (!ValidatePrimaryFields()) return;

                ptixiaki_row.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
                ptixiaki_row.ΔΙΟΙΚΗΤΗΣ = admin;
                ptixiaki_row.ΤΜΗΜΑ = tmima;
                ptixiaki_row.ΣΧΟΛΗ = Sxoli;

                db.ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗs.InsertOnSubmit(ptixiaki_row);
            }
            else if (e.EditOperationType == GridViewEditOperationType.Edit)
            {
                var row = e.Row as GridViewRow;
                ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ ptixiaki_row = row.Item as ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ;

                if (!ValidatePrimaryFields()) return;

                ptixiaki_row.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
                ptixiaki_row.ΔΙΟΙΚΗΤΗΣ = admin;
                ptixiaki_row.ΤΜΗΜΑ = tmima;
                ptixiaki_row.ΣΧΟΛΗ = Sxoli;
            }
        }


        private void dataGrid_Deleting(object sender, Telerik.Windows.Controls.GridViewDeletingEventArgs e)
        {
            if (!ValidatePrimaryFields()) return;

            if (dataGrid.SelectedItems.Count == 0) { return; }
            // verify deletion from user
            string checkMessage = "Να γίνει διαγραφή των επιλεγμένων εγγραφών; " + "\n";

            if (MessageBox.Show(checkMessage, "Διαγραφή", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            { return; }

            // proceed with deletion process
            foreach (var row in dataGrid.SelectedItems)
            {
                ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ ptixiaki = row as ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ;
                db.ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗs.DeleteOnSubmit(ptixiaki);
            }
        }

        private void dataGrid_CellValidating(object sender, Telerik.Windows.Controls.GridViewCellValidatingEventArgs e)
        {
            ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ pt = e.Row.DataContext as ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ;

            if (e.Cell.Column.Name == "ptixiaki_title")
            {
                string _title = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(_title))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Πρέπει να εισαχθεί ο τίτλος της πτυχιακής εργασίας.";
                }
            }

            if (e.Cell.Column.Name == "cboTeacher")
            {
                string _teacher = e.NewValue.ToString();
                if(string.IsNullOrEmpty(_teacher))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Πρέπει να εισαχθεί ο επιβλέπων καθηγητής.";
                }
            }
        }

        private void dataGrid_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ ptixiaki = e.Row.DataContext as ΣΠΟΥΔΑΣΤΗΣ_ΠΤΥΧΙΑΚΗ;

            string _teacher = ptixiaki.ΚΑΘΗΓΗΤΗΣ;
            string _title = ptixiaki.ΠΤΥΧΙΑΚΗ_ΤΙΤΛΟΣ;

            string _student1 = ptixiaki.ΣΠΟΥΔΑΣΤΗΣ1;
            string _student2 = ptixiaki.ΣΠΟΥΔΑΣΤΗΣ2;
            string _student3 = ptixiaki.ΣΠΟΥΔΑΣΤΗΣ3;

            if (String.IsNullOrEmpty(_student1) && String.IsNullOrEmpty(_student2) && String.IsNullOrEmpty(_student3))
            {
                e.IsValid = false;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί τουλάχιστον ένας σπουδαστής.");
            }

            if (String.IsNullOrEmpty(_title))
            {
                e.IsValid = false;
                UserFunctions.ShowAdminMessage("Πρέπει να καταχωρηθεί το θέμα της πτυχιακής εργασίας.");
            }

            if (String.IsNullOrEmpty(_teacher))
            {
                e.IsValid = false;
                UserFunctions.ShowAdminMessage("Πρέπει να καταχωρηθεί ο επιβλέπων καθηγητής.");
            }

        }

        #endregion

        private void RefreshData()
        {
            var records = from r in db.ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΜs
                          where r.ΤΜΗΜΑ == tmima
                          select r;
            dataGrid.ItemsSource = records.ToList();

        }

        private bool ValidatePrimaryFields()
        {
            if (syear == 0 || tmima == 0 || admin == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή έτους, διοικητή και τμήματος.");
                return false;
            }
            else if (syear != 0 && tmima != 0 && admin != 0)
            {
                return true;
            }
            else return false;
        }

    }   // class Ptixiaki
}
