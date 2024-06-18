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

namespace Poseidon.AppPages.Programme.SP
{
    /// <summary>
    /// Interaction logic for Orologio.xaml
    /// </summary>
    public partial class Orologio : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();
        private Kerberos k = new Kerberos();
        private TeacherModel tm = new TeacherModel();

        private DateTime dt;
        private int tmima_id;
        private int term_id;
        private int commander;

        private int syear;
        private bool saved = false;
        private bool isNewRec;

        public Orologio()
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

            var admin = from a in db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs
                        orderby a.ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select a;
            cboCommander.ItemsSource = admin.ToList();

            var _hours = from h in db.ΩΡΕΣs
                         select h;
            cboHour.ItemsSource = _hours.ToList();

            //var _lessons = from l in db.ΜΑΘΗΜΑΤΑ_ΣΠs
            //               orderby l.ΜΑΘΗΜΑ
            //               select l;
            //cboLesson.ItemsSource = _lessons.ToList();

            var _ergo = from e in db.ΔΙΔΑΚΤΙΚΟ_ΕΡΓΟs
                        select e;
            cboErgo.ItemsSource = _ergo.ToList();

            var _weeks = from w in db.ΕΒΔΟΜΑΔΕΣs
                         select w;
            cboWeek.ItemsSource = _weeks.ToList();

            var _teachers = from t in db.ViewΕΚΠΑΙΔΕΥΤΙΚΟΙs
                            orderby t.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                            select t;

            cboTeacher1.ItemsSource = _teachers.ToList();
            cboTeacher2.ItemsSource = _teachers.ToList();
            cboTeacher3.ItemsSource = _teachers.ToList();

            //var programma = from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
            //                select p;
            dataGrid.ItemsSource = null; // programma.ToList();
        }

        #region Combo and Calendar Events

        private void calendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dt = (DateTime)calendar.SelectedDate;

            string sDate = dt.ToString("dd/MM/yyyy");
            txtSelectedDate.Text = Convert.ToString(sDate);

            var programma = from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                            where p.ΗΜΕΡΟΜΗΝΙΑ == dt && p.ΤΜΗΜΑ == tmima_id
                            select p;
            int recs = (from r in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                        where r.ΗΜΕΡΟΜΗΝΙΑ == dt && r.ΤΜΗΜΑ == tmima_id
                        select r).Count();

            dataGrid.ItemsSource = programma.ToList();
            if (recs > 0) ShowLastWeek();

        }

        private void cboSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboSchoolYear.IsLoaded) return;
            if (cboSchoolYear.SelectedValue != null)
            {
                syear = (int)cboSchoolYear.SelectedValue;

                // set calendar to school year
                //var syears = (from sy in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                //             where sy.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == syear
                //             select new { sy.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ, sy.ΣΧ_ΕΤΟΣ_ΛΗΞΗ }).FirstOrDefault();

                //DateTime _minDate = (DateTime)syears.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ;
                //DateTime _maxDate = (DateTime)syears.ΣΧ_ΕΤΟΣ_ΛΗΞΗ;

                //calendar.DisplayDateStart = _minDate;
                //calendar.DisplayDateEnd = _maxDate;

                // set tmima to school year
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

                var _tmima = (from t in db.ΤΜΗΜΑΤΑ_ΣΠs
                             where t.ΤΜΗΜΑ_ΚΩΔ == tmima_id
                             select new {t.ΕΞΑΜΗΝΟ}).FirstOrDefault();

                term_id = (int)_tmima.ΕΞΑΜΗΝΟ;

                var lessons = from l in db.ViewΜΑΘΗΜΑΤΑ_ΣΠs
                              where l.ΕΞΑΜΗΝΟ_ΚΩΔ == term_id
                              select l;
                cboLesson.ItemsSource = lessons.ToList();

                var grid_data = from g in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                                where g.ΤΜΗΜΑ == tmima_id
                                select g;

                dataGrid.ItemsSource = null; // grid_data.ToList();
                txtLastWeek.Text = "";
                txtFirstDate.Text = "Από:";
                txtLastDate.Text = "Έως:";
            }
        }

        private void cboCommander_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboCommander.IsLoaded) return;
            if (cboCommander.SelectedValue != null)
            {
                int selected_admin = (int)cboTmima.SelectedValue;
                commander = selected_admin;
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
                ΠΡΟΓΡΑΜΜΑ_ΣΠ programme = row as ΠΡΟΓΡΑΜΜΑ_ΣΠ;
                db.ΠΡΟΓΡΑΜΜΑ_ΣΠs.DeleteOnSubmit(programme);
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
            ShowLastWeek();
            saved = true;
            isNewRec = false;
        }

        #endregion

        #region DataGrid Events

        private void dataGrid_AddingNewDataItem(object sender, Telerik.Windows.Controls.GridView.GridViewAddingNewEventArgs e)
        {
            if (!ValidatePrimaryFields()) return;

            dataGrid.CurrentCellInfo = new GridViewCellInfo(dataGrid.CurrentItem, dataGrid.Columns["cboHour"]);
            dataGrid.Focus();
            //var _tmima = (from t in db.ΤΜΗΜΑΤΑ_ΣΠs
            //              where t.ΤΜΗΜΑ_ΚΩΔ == tmima_id
            //              select new { t.ΕΞΑΜΗΝΟ }).FirstOrDefault();

            //var lessons = from l in db.ΜΑΘΗΜΑΤΑ_ΣΠs
            //              where l.ΕΞΑΜΗΝΟ == term_id
            //              select l;
            //cboLesson.ItemsSource = lessons.ToList();

            // reset save flag
            saved = false;
            isNewRec = true;
        }

        private void dataGrid_BeginningEdit(object sender, GridViewBeginningEditRoutedEventArgs e)
        {
            // reset save flag
            saved = false;

            var row = e.Row as GridViewRow;
            ΠΡΟΓΡΑΜΜΑ_ΣΠ programme = row.Item as ΠΡΟΓΡΑΜΜΑ_ΣΠ;

            if (!ValidatePrimaryFields())
            {
                e.Cancel = true;
                return;
            }
            else
            {
                e.Cancel = false;
                if (programme != null)
                {
                    programme.ΗΜΕΡΟΜΗΝΙΑ = dt;
                    programme.ΤΜΗΜΑ = tmima_id;
                    programme.ΔΙΟΙΚΗΤΗΣ = commander;

                }

                //UserFunctions.RadWindowAlert("date:" + programme.ΗΜΕΡΟΜΗΝΙΑ + " tmima:" + programme.ΤΜΗΜΑ);
            }
        }

        private void dataGrid_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
        {
            if (!ValidatePrimaryFields()) return;

            // reset save flag
            saved = false;

            var row = e.Row as GridViewRow;
            ΠΡΟΓΡΑΜΜΑ_ΣΠ programme = row.Item as ΠΡΟΓΡΑΜΜΑ_ΣΠ;

            if (e.EditAction == GridViewEditAction.Cancel)
            {
                return;
            }
            if (e.EditOperationType == GridViewEditOperationType.Insert)
            {
                programme.ΗΜΕΡΟΜΗΝΙΑ = dt;
                programme.ΤΜΗΜΑ = tmima_id;
                programme.ΔΙΟΙΚΗΤΗΣ = commander;

                if (!k.ValidatePK_SP(programme))
                {
                    LoadData(); // clear the collection from the new row
                    return;
                }
                db.ΠΡΟΓΡΑΜΜΑ_ΣΠs.InsertOnSubmit(programme);
                //isNewRec = false;
            }
            if (e.EditOperationType == GridViewEditOperationType.Edit)
            {
                programme.ΗΜΕΡΟΜΗΝΙΑ = dt;
                programme.ΤΜΗΜΑ = tmima_id;
                programme.ΔΙΟΙΚΗΤΗΣ = commander;

            }
        }

        private void dataGrid_CellValidating(object sender, GridViewCellValidatingEventArgs e)
        {

        }

        private void dataGrid_RowValidating(object sender, GridViewRowValidatingEventArgs e)
        {
            ΠΡΟΓΡΑΜΜΑ_ΣΠ programma = e.Row.DataContext as ΠΡΟΓΡΑΜΜΑ_ΣΠ;

            if(k.IsNullRowSP(programma))
            {
                e.IsValid = false;
                return;
            }

            if(k.EmptyFieldsExistSP(programma))
            {
                e.IsValid = false;
                return;
            }

        }

        #endregion

        #region Transfer functions

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (isNewRec && !saved)
            {
                UserFunctions.ShowAdminMessage("Πρεπει να προηγηθεί αποθήκευση πριν την μεταφορά διδακτικής ώρας.");
                return;
            }

            tm.TransferHourSP(db, dataGrid, saved, isNewRec);
            saved = false;
        }

        private void ShowRowData(ΠΡΟΓΡΑΜΜΑ_ΣΠ Row)
        {
            string message = "";
            string sDate = Row.ΗΜΕΡΟΜΗΝΙΑ.ToString("dd/MM/yyyy");
            message = "Ώρα:" + Row.ΩΡΑ + " Εβδ.:" + Row.ΕΒΔΟΜΑΔΑ + " Ημερομηνία:" + sDate;
            MessageBox.Show(message);
        }

        private void btnTransferWeek_Click(object sender, RoutedEventArgs e)
        {
            tm.TransferWeekSP(db, calendar, dataGrid, dt, tmima_id, syear);
            ShowLastWeek();
        }

        #endregion

        private void RefreshData()
        {
            var programma = from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                            where p.ΗΜΕΡΟΜΗΝΙΑ == dt && p.ΤΜΗΜΑ == tmima_id
                            select p;
            dataGrid.ItemsSource = programma.ToList();
        }

        private void ShowLastWeek()
        {
            var prog = (from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                        where p.ΤΜΗΜΑ == tmima_id
                        orderby p.ΕΒΔΟΜΑΔΑ descending
                        select p).First();
            if (prog == null)
            {
                txtLastWeek.Text = Convert.ToString(0);
                return;
            }

            int last_week = (int)prog.ΕΒΔΟΜΑΔΑ;
            txtLastWeek.Text = Convert.ToString(last_week);

            DateTime date1 = tm.LastWeekFirstDateSP(db, tmima_id, last_week);
            DateTime date2 = date1.AddDays(4);

            txtFirstDate.Text = date1.ToString("dd/MM/yyyy");
            txtLastDate.Text = date2.ToString("dd/MM/yyyy");
        }

        private bool IsLastRow(ΠΡΟΓΡΑΜΜΑ_ΣΠ currentRow)
        {
            var lastrow = (from r in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                           where r.ΗΜΕΡΟΜΗΝΙΑ == currentRow.ΗΜΕΡΟΜΗΝΙΑ && r.ΤΜΗΜΑ == currentRow.ΤΜΗΜΑ
                           orderby r.ΩΡΑ descending
                           select new { r.ΩΡΑ }).FirstOrDefault();

            if (currentRow.ΩΡΑ != lastrow.ΩΡΑ)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε την τελευταία καταχωρημένη ώρα για μεταφορά.");
                return false;
            }
            else return true;
        }

        private bool ValidatePrimaryFields()
        {
            if (syear == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί διδακτικό έτος.");
                return false;
            }
            else if (tmima_id == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί τμήμα.");
                return false;
            }
            else if (commander == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί ο Διοικητής.");
                return false;
            }
            else if (!tm.ValidateDate(db, dt, syear))
            {
                UserFunctions.ShowAdminMessage("Η ημερομηνία είναι εκτός ορίων του διδακτικού έτους.");
                return false;
            }
            else return true;
        }


    }   // class Orologio
}
