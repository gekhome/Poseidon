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
    /// Interaction logic for Bathmoi.xaml
    /// </summary>
    public partial class Bathmoi : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        private int syear = 0;
        private int tmima = 0;
        private string amka;
        private int Sxoli = 2;
        private int dioikitis;
        //private bool isNewRec;

        public Bathmoi()
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

            var admin = from a in db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs
                        orderby a.ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select a;
            cboDioikitis.ItemsSource = admin.ToList();

            var tmimata = from t in db.ΤΜΗΜΑΤΑ_ΣΜs
                          select t;
            cboTmima.ItemsSource = tmimata.ToList();

            var students = from s in db.ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΜs
                           orderby s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                           select s;
            cboStudent.ItemsSource = students.ToList();
        }

        #region Selection Combos Events

        private void cboSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboSchoolYear.IsLoaded) return;

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
            ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ admin = cboDioikitis.SelectedItem as ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ;
            dioikitis = admin.ΚΩΔΙΚΟΣ;
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
            cboStudent.ItemsSource = spoudastes.ToList();

            // set data source to lessons of the selected class (term)
            var classes = (from c in db.sqlΤΜΗΜΑΤΑ_ΣΜs
                           where c.ΤΜΗΜΑ_ΚΩΔ == tmima
                           select new { c.ΕΞΑΜΗΝΟ_ΚΩΔ }).FirstOrDefault();

            int term = (int)classes.ΕΞΑΜΗΝΟ_ΚΩΔ;

            var lessons = from le in db.ViewΜΑΘΗΜΑΤΑ_ΣΜs
                          where le.ΕΞΑΜΗΝΟ_ΚΩΔ == term
                          select le;
            cboLesson.ItemsSource = lessons.ToList();

            dataGrid.ItemsSource = null;

        }

        private void cboStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboStudent.IsLoaded) return;

            ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΜ student = cboStudent.SelectedItem as ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΜ;

            if (student == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί σπουδαστής.");
                return;
            }

            amka = student.ΑΜΚΑ;

            var records = from r in db.ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙs
                          where r.ΑΜΚΑ == amka && r.ΤΜΗΜΑ == tmima
                          select r;
            dataGrid.ItemsSource = records.ToList();

        }

        #endregion

        #region dataGrid Events

        private void dataGrid_AddingNewDataItem(object sender, GridViewAddingNewEventArgs e)
        {
            dataGrid.CurrentCellInfo = new GridViewCellInfo(dataGrid.CurrentItem, dataGrid.Columns["cboLesson"]);
            dataGrid.Focus();
        }

        private void dataGrid_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            var row = e.Row as GridViewRow;
            ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ grades = row.Item as ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ;

            if (!ValidatePrimaryFields()) return;
            if (grades == null) return;

            grades.ΑΜΚΑ = amka;
            grades.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
            grades.ΔΙΟΙΚΗΤΗΣ = dioikitis;
            grades.ΤΜΗΜΑ = tmima;
            grades.ΣΧΟΛΗ = Sxoli;
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
                ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ grades_row = row.Item as ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ;

                if (!ValidatePrimaryFields()) return;

                grades_row.ΑΜΚΑ = amka;
                grades_row.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
                grades_row.ΔΙΟΙΚΗΤΗΣ = dioikitis;
                grades_row.ΤΜΗΜΑ = tmima;
                grades_row.ΣΧΟΛΗ = Sxoli;

                CalculateGrades(grades_row);

                db.ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙs.InsertOnSubmit(grades_row);
            }
            else if (e.EditOperationType == GridViewEditOperationType.Edit)
            {
                var row = e.Row as GridViewRow;
                ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ grades_row = row.Item as ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ;

                if (!ValidatePrimaryFields()) return;

                grades_row.ΑΜΚΑ = amka;
                grades_row.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
                grades_row.ΔΙΟΙΚΗΤΗΣ = dioikitis;
                grades_row.ΤΜΗΜΑ = tmima;
                grades_row.ΣΧΟΛΗ = Sxoli;

                CalculateGrades(grades_row);
            }

        }

        private void dataGrid_Deleting(object sender, GridViewDeletingEventArgs e)
        {
            if (dataGrid.SelectedItems.Count == 0) { return; }
            // verify deletion from user
            string checkMessage = "Να γίνει διαγραφή των επιλεγμένων εγγραφών; " + "\n";

            if (MessageBox.Show(checkMessage, "Διαγραφή", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            { return; }

            // proceed with deletion process
            foreach (var row in dataGrid.SelectedItems)
            {
                ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ grades = row as ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ;
                db.ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙs.DeleteOnSubmit(grades);
            }
        }

        private void dataGrid_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {
            ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ grades = e.Row.DataContext as ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ;
            string lesson_type;
            bool theory;
            bool app;

            // distinguish 3 lesson types (Θ, Ε, ΘΕ)
            var lessons = (from le in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                           where le.ΚΩΔΙΚΟΣ == grades.ΜΑΘΗΜΑ
                           select new { le.ΘΕ }).FirstOrDefault();

            if (lessons == null) return;
            lesson_type = lessons.ΘΕ;
            if (lesson_type == "Θ") { theory = true; app = false; }
            else if (lesson_type == "Ε") { app = true; theory = false; }
            else { theory = true; app = true; }

            if (e.Cell.Column.Name == "ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΘΕΩΡΙΑ")
            {
                if (theory == true)
                {
                    string grade = e.NewValue.ToString();
                    if (String.IsNullOrWhiteSpace(grade))
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                    }
                }
                else
                {
                    string grade = e.NewValue.ToString();
                    if (!String.IsNullOrWhiteSpace(grade))
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Δεν μπορεί να εισαχθεί τιμή γιατί το μάθημα δεν έχει Θεωρία.";
                    }
                }
            }

            if (e.Cell.Column.Name == "ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΠΡΑΚΤΙΚΗ")
            {
                if (app == true)
                {
                    string grade = e.NewValue.ToString();
                    if (String.IsNullOrWhiteSpace(grade))
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                    }
                }
                else
                {
                    string grade = e.NewValue.ToString();
                    if (!String.IsNullOrWhiteSpace(grade))
                    {
                        e.IsValid = false;
                        e.ErrorMessage = "Δεν μπορεί να εισαχθεί τιμή γιατί το μάθημα δεν έχει Εφαρμογές.";
                    }
                }
            }

            if (e.Cell.Column.Name == "ΒΑΘΜΟΣ_ΠΡΟΟΔΟΣ")
            {
                string grade = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(grade))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή.";
                }
             }

        }   // dataGrid_CellValidating

        private void dataGrid_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
            ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ grades = e.Row.DataContext as ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ;

            double proodos = Convert.ToDouble(grades.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΣ);
            double grade_theory = Convert.ToDouble(grades.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΘΕΩΡΙΑ);
            double grade_app = Convert.ToDouble(grades.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΠΡΑΚΤΙΚΗ);

            string lesson_type;
            bool theory;
            bool app;

            // distinguish 3 lesson types (Θ, Ε, ΘΕ)
            var lessons = (from le in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                           where le.ΚΩΔΙΚΟΣ == grades.ΜΑΘΗΜΑ
                           select new { le.ΘΕ }).FirstOrDefault();

            if (lessons == null) return;
            lesson_type = lessons.ΘΕ;
            if (lesson_type == "Θ") { theory = true; app = false; }
            else if (lesson_type == "Ε") { app = true; theory = false; }
            else { theory = true; app = true; }

            if(theory == true && (grade_theory < 0.0 || grade_theory > 10.0))
            {
                UserFunctions.ShowAdminMessage("Μη έγκυρη τιμή βαθμού θεωρίας (πρέπει να είναι από 0 έως 10).");
                e.IsValid = false;
            }
            if (app == true && (grade_app < 0.0 || grade_app > 10.0))
            {
                UserFunctions.ShowAdminMessage("Μη έγκυρη τιμή βαθμού εφαρμογών (πρέπει να είναι από 0 έως 10).");
                e.IsValid = false;
            }
            if (proodos < 0.0 || proodos > 10.0)
            {
                UserFunctions.ShowAdminMessage("Μη έγκυρη τιμή βαθμού προόδου (πρέπει να είναι από 0 έως 10).");
                e.IsValid = false;
            }
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
                ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ grades = row as ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ;
                db.ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙs.DeleteOnSubmit(grades);
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
            RefreshData();
        }

        #endregion

        private void RefreshData()
        {
            var records = from r in db.ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙs
                          where r.ΑΜΚΑ == amka && r.ΤΜΗΜΑ == tmima
                          select r;
            dataGrid.ItemsSource = records.ToList();

        }

        private void CalculateGrades(ΣΠΟΥΔΑΣΤΗΣ_ΒΑΘΜΟΙ row)
        {
            string lesson_type;
            bool theory;
            bool app;
            double avgTheoryApp;
            double avg;

            // convert null values to 0, otherwise calculated fields are null
            if (row.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΣ == null) row.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΣ = 0;
            if (row.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΘΕΩΡΙΑ == null) row.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΘΕΩΡΙΑ = 0;
            if (row.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΠΡΑΚΤΙΚΗ == null) row.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΠΡΑΚΤΙΚΗ = 0;

            // distinguish 3 lesson types (Θ, Ε, ΘΕ)
            var lessons = (from le in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                           where le.ΚΩΔΙΚΟΣ == row.ΜΑΘΗΜΑ
                           select new { le.ΘΕ }).FirstOrDefault();

            if (lessons == null) return;

            lesson_type = lessons.ΘΕ;

            if (lesson_type == "Θ") { theory = true; app = false; }
            else if (lesson_type == "Ε") { app = true; theory = false; }
            else { theory = true; app = true; }

            if (theory == true && app == true)
            {
                avgTheoryApp = ((double)row.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΘΕΩΡΙΑ + (double)row.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΠΡΑΚΤΙΚΗ) / 2.0;
                row.ΒΑΘΜΟΣ_ΜΟ_ΘΕ = Math.Round(avgTheoryApp, 1);

                avg = ((double)row.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΣ + (double)row.ΒΑΘΜΟΣ_ΜΟ_ΘΕ) / 2.0;
                row.ΒΑΘΜΟΣ_ΜΟ = Math.Round(avg, 1);
            }
            else if (theory == true && app == false)
            {
                avgTheoryApp = (double)row.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΘΕΩΡΙΑ;
                row.ΒΑΘΜΟΣ_ΜΟ_ΘΕ = Math.Round(avgTheoryApp, 1);

                avg = ((double)row.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΣ + (double)row.ΒΑΘΜΟΣ_ΜΟ_ΘΕ) / 2.0;
                row.ΒΑΘΜΟΣ_ΜΟ = Math.Round(avg, 1);
            }
            else if (theory == false && app == true)
            {
                avgTheoryApp = (double)row.ΒΑΘΜΟΣ_ΕΞΕΤΑΣΗ_ΠΡΑΚΤΙΚΗ;
                row.ΒΑΘΜΟΣ_ΜΟ_ΘΕ = Math.Round(avgTheoryApp, 1);

                avg = ((double)row.ΒΑΘΜΟΣ_ΠΡΟΟΔΟΣ + (double)row.ΒΑΘΜΟΣ_ΜΟ_ΘΕ) / 2.0;
                row.ΒΑΘΜΟΣ_ΜΟ = Math.Round(avg, 2);
            }

        }   // method CalculateGrades

        private bool ValidatePrimaryFields()
        {
            if (amka == null || syear == 0 || tmima == 0 || dioikitis == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή έτους, διοικητή, τμήματος και σπουδαστή.");
                return false;
            }
            else if (amka != null && syear != 0 && tmima != 0 && dioikitis != 0)
            {
                return true;
            }
            else return false;
        }

    }
}
