using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Input;
using Telerik.Windows.Controls.Navigation;
using Telerik.Windows.Controls.GridView;
using System.Collections.ObjectModel;
using Poseidon.Utilities;
using Poseidon.DataModel;
using Poseidon.Controls;
using Poseidon.Model;
using Poseidon.AppPages.Printouts;

namespace Poseidon.AppPages.Students.SP
{
    /// <summary>
    /// Interaction logic for Apousies.xaml
    /// </summary>
    public partial class Apousies : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();
        private Kerberos k = new Kerberos();
        private StudentModel sm = new StudentModel();

        private DateTime dt;
        private int tmima_id;
        //private int term_id;
        private int syear;
        private int sxoli = 1;  // Σχολή Πλοιάρχων

        public Apousies()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var _syears = from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                          orderby s.ΣΧΟΛ_ΕΤΟΣ descending
                          select s;
            cboSchoolYear.ItemsSource = _syears.ToList();

            var _tmimata = from tt in db.ΤΜΗΜΑΤΑ_ΣΠs
                           select tt;
            cboTmima.ItemsSource = _tmimata.ToList();

            var numbers = from n in db.ΑΡΙΘΜΟΙs
                          select n;
            cboNumber.ItemsSource = numbers.ToList();

            var apousies_type = from at in db.ΑΠΟΥΣΙΕΣ_ΕΙΔΗs
                                select at;
            cboApousiaType.ItemsSource = apousies_type.ToList();

            dataGrid.ItemsSource = null; // apousies.ToList();

        }

        #region Combo and Calendar Events

        private void calendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dt = (DateTime)calendar.SelectedDate;

            string sDate = dt.ToString("dd/MM/yyyy");
            txtSelectedDate.Text = Convert.ToString(sDate);

            var apousies = from a in db.ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣs
                           where a.ΗΜΕΡΟΜΗΝΙΑ == dt && a.ΤΜΗΜΑ == tmima_id && a.ΣΧΟΛΗ == sxoli
                           select a;
            int recs = (from r in db.ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣs
                        where r.ΗΜΕΡΟΜΗΝΙΑ == dt && r.ΤΜΗΜΑ == tmima_id && r.ΣΧΟΛΗ == sxoli
                        select r).Count();

            if (recs == 0) UserFunctions.ShowAdminMessage("Δεν βρέθηκαν εγγραφές απουσιών για την επιλεγμένη ημερομηνία.");

            // ------------------
            // Εδώ πρέπει να φορτώνονται τα μαθήματα της συγκεκριμένης
            // ημέρας από το ωρολόγιο πρόγραμμα μαθημάτων, αντί για όλα
            // του τμήματος (εξαμήνου).
            // ------------------

            SetDateLessons(tmima_id, dt);

            dataGrid.ItemsSource = apousies.ToList();
        }

        private void cboSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboSchoolYear.IsLoaded) return;
            if (cboSchoolYear.SelectedValue != null)
            {
                syear = (int)cboSchoolYear.SelectedValue;

                var tmimata = from t in db.ΤΜΗΜΑΤΑ_ΣΠs
                              where t.ΔΙΔ_ΕΤΟΣ == syear
                              select t;
                if (tmimata == null)
                {
                    UserFunctions.ShowAdminMessage("Δεν βρέθηκαν τμήματα για το επιλεγμένο διδακτικό έτος");
                    return;
                }
                cboTmima.ItemsSource = tmimata.ToList();
            }
        }

        private void cboTmima_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboTmima.IsLoaded) return;
            if (cboTmima.SelectedValue != null)
            {
                int selected_tmima = (int)cboTmima.SelectedValue;
                tmima_id = selected_tmima;

                dataGrid.ItemsSource = null; // grid_data.ToList();

                var students = from s in db.ViewΣΠΟΥΔΑΣΤΗΣ_ΤΜΗΜΑ_ΣΠs
                               where s.ΤΜΗΜΑ == tmima_id
                               orderby s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                               select s;

                cboStudent.ItemsSource = students.ToList();
            }
        }

        #endregion

        #region Button Events

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
                ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ apousies = row as ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ;
                db.ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣs.DeleteOnSubmit(apousies);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.BeginEdit();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            // load the existing collection without new changes (no submit is done unless Save is pressed)
            RefreshData();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cm.UserCanUpdate() == false)
            {
                LoadData();
                return;
            }

            cm.CommitData(db);
            RefreshData();
        }

        #endregion

        #region dataGrid Events

        private void dataGrid_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            if (!sm.ValidateDate(db, dt, syear))
            {
                return;
            }

            dataGrid.CurrentCellInfo = new GridViewCellInfo(dataGrid.CurrentItem, dataGrid.Columns["cboLesson"]);
            dataGrid.Focus();

            SetDateLessons(tmima_id, dt);
        }

        private void dataGrid_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        {
            var row = e.Row as GridViewRow;
            ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ apousies = row.Item as ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ;

            if (!sm.ValidateDate(db, dt, syear))
            {
                e.Cancel = true;
                return;
            }

            if (syear == 0 || tmima_id == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί διδ. έτος και τμήμα");
                e.Cancel = true;
                return;
            }

            else
            {
                e.Cancel = false;
                if (apousies != null)
                {
                    apousies.ΗΜΕΡΟΜΗΝΙΑ = dt;
                    apousies.ΤΜΗΜΑ = tmima_id;
                    apousies.ΣΧΟΛΗ = sxoli;
                    apousies.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;
                }

                //UserFunctions.RadWindowAlert("date:" + programme.ΗΜΕΡΟΜΗΝΙΑ + " tmima:" + programme.ΤΜΗΜΑ);
            }

        }

        private void dataGrid_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            var row = e.Row as GridViewRow;
            ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ apousies = row.Item as ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ;

            if (e.EditAction == GridViewEditAction.Cancel)
            {
                return;
            }
            if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                if (syear == 0 || tmima_id == 0)
                {
                    UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί διδ. έτος και τμήμα");
                    return;
                }

                apousies.ΗΜΕΡΟΜΗΝΙΑ = dt;
                apousies.ΤΜΗΜΑ = tmima_id;
                apousies.ΣΧΟΛΗ = sxoli;
                apousies.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;

                db.ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣs.InsertOnSubmit(apousies);
            }
            if (e.EditOperationType == GridViewEditOperationType.Edit)
            {
                if (dt == null || tmima_id == 0)
                {
                    UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί διδ. έτος και τμήμα");
                    return;
                }
                apousies.ΗΜΕΡΟΜΗΝΙΑ = dt;
                apousies.ΤΜΗΜΑ = tmima_id;
                apousies.ΣΧΟΛΗ = sxoli;
                apousies.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = syear;

            }
        }

        private void dataGrid_CellValidating(object sender, Telerik.Windows.Controls.GridViewCellValidatingEventArgs e)
        {

            ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ apousies = e.Row.DataContext as ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ;
            if (apousies == null) return;


            if (e.Cell.Column.Name == "cboStudent")
            {
                if (e.NewValue == null)
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή";
                    return;
                }
                string student = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(student) || String.IsNullOrEmpty(student))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει έχει γίνει επιλογή σπουδαστή.";
                }
            }

            if (e.Cell.Column.Name == "cboLesson")
            {
                if (e.NewValue == null)
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή";
                    return;
                }

                string lesson = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(lesson) || String.IsNullOrEmpty(lesson))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει έχει γίνει επιλογή μαθήματος.";
                }
            }

            if (e.Cell.Column.Name == "cboNumber")
            {
                if (e.NewValue == null)
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή";
                    return;
                }

                string hours = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(hours) || String.IsNullOrEmpty(hours))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει έχει γίνει επιλογή ωρών απουσίας.";
                }
            }

            if (e.Cell.Column.Name == "cboApousiaType")
            {
                if (e.NewValue == null)
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει εισαχθεί τιμή";
                    return;
                }

                string apousia_type = e.NewValue.ToString();
                if (String.IsNullOrWhiteSpace(apousia_type) || String.IsNullOrEmpty(apousia_type))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Δεν έχει έχει γίνει επιλογή είδους απουσίας.";
                }
            }

        }

        private void dataGrid_RowValidating(object sender, Telerik.Windows.Controls.GridViewRowValidatingEventArgs e)
        {
            ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ apousies = e.Row.DataContext as ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ;

            if (k.IsNullRowApousies(apousies))
            {
                e.IsValid = false;
                return;
            }

            if (k.EmptyFieldsExistApousies(apousies)
                )
            {
                e.IsValid = false;
                return;
            }

            int lesson_hours = (from l in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                                where l.ΤΜΗΜΑ == tmima_id && l.ΗΜΕΡΟΜΗΝΙΑ == dt && l.ΜΑΘΗΜΑ == apousies.ΜΑΘΗΜΑ
                                select l).Count();
            //UserFunctions.RadWindowAlert("lesson_hours = " + lesson_hours);

            if (apousies.ΑΠΟΥΣΙΕΣ_ΩΡΕΣ > lesson_hours)
            {
                e.IsValid = false;
                UserFunctions.ShowAdminMessage("Οι ώρες απουσίας υπερβαίνουν αυτές του μαθήματος για την επιλεγμένη ημέρα.");
            }

        }

        #endregion


        private void SetDateLessons(int tmima, DateTime dt)
        {
            if (!sm.ValidateDate(db, dt, syear))
            {
                return;
            }

            var lessons = from l in db.ViewΠΡΟΓΡΑΜΜΑ_ΣΠs
                          where l.ΤΜΗΜΑ == tmima && l.ΗΜΕΡΟΜΗΝΙΑ == dt
                          select l;
            cboLesson.ItemsSource = lessons.ToList();

        }

        private void RefreshData()
        {
            var apousies = from p in db.ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣs
                           where p.ΗΜΕΡΟΜΗΝΙΑ == dt && p.ΤΜΗΜΑ == tmima_id && p.ΣΧΟΛΗ == sxoli
                           select p;
            dataGrid.ItemsSource = apousies.ToList();
        }

    }   // class Apousies
}
