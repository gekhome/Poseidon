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

namespace Poseidon.DataModel
{
    class TeacherModel
    {
        private CommitModel cm = new CommitModel();
        //private bool _lastRow = false;

        #region SM Methods

        public void TransferHourSM(PoseidonDataContext db, RadGridView dataGrid, bool saved, bool newRec)
        {
            if (!saved && newRec)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να προηγηθεί αποθήκευση πριν την μεταφορά ώρας.");
                return;
            }
            
            ΠΡΟΓΡΑΜΜΑ_ΣΜ _row = dataGrid.SelectedItem as ΠΡΟΓΡΑΜΜΑ_ΣΜ;
            if (_row == null || !IsLastRowSM(db, _row))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε την τελευταία γραμμή του πίνακα για μεταφορά.");
                return;
            }

            ΠΡΟΓΡΑΜΜΑ_ΣΜ NewRow = dataGrid.Items.AddNew() as ΠΡΟΓΡΑΜΜΑ_ΣΜ;
            SetNewRowDataSM( db,_row, NewRow);
            //_lastRow = false;
        }

        private bool IsLastRowSM(PoseidonDataContext db, ΠΡΟΓΡΑΜΜΑ_ΣΜ currentRow)
        {
            var lastrow = (from r in db.ΠΡΟΓΡΑΜΜΑ_ΣΜs
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


        private void SetNewRowDataSM(PoseidonDataContext db, ΠΡΟΓΡΑΜΜΑ_ΣΜ currentRow, ΠΡΟΓΡΑΜΜΑ_ΣΜ newRow)
        {
            newRow.ΤΜΗΜΑ = (int)currentRow.ΤΜΗΜΑ;
            newRow.ΗΜΕΡΟΜΗΝΙΑ = (DateTime)currentRow.ΗΜΕΡΟΜΗΝΙΑ;
            newRow.ΩΡΑ = (short)(currentRow.ΩΡΑ + 1);
            newRow.ΕΒΔΟΜΑΔΑ = (short)currentRow.ΕΒΔΟΜΑΔΑ;
            newRow.ΜΑΘΗΜΑ = (int)currentRow.ΜΑΘΗΜΑ;
            newRow.ΔΙΔ_ΕΡΓΟ = (short)currentRow.ΔΙΔ_ΕΡΓΟ;
            newRow.ΕΚΠΑΙΔΕΥΤΗΣ1 = currentRow.ΕΚΠΑΙΔΕΥΤΗΣ1;
            newRow.ΕΚΠΑΙΔΕΥΤΗΣ2 = currentRow.ΕΚΠΑΙΔΕΥΤΗΣ2;
            newRow.ΕΚΠΑΙΔΕΥΤΗΣ3 = currentRow.ΕΚΠΑΙΔΕΥΤΗΣ3;
            newRow.Π1 = currentRow.Π1;
            newRow.Π2 = currentRow.Π2;
            newRow.Π3 = currentRow.Π3;
            newRow.ΔΙΟΙΚΗΤΗΣ = currentRow.ΔΙΟΙΚΗΤΗΣ;

            //ShowRowData(newRow);

            db.ΠΡΟΓΡΑΜΜΑ_ΣΜs.InsertOnSubmit(newRow);
        }

        public void TransferWeekSM(PoseidonDataContext db, RadCalendar calendar, RadGridView dataGrid, DateTime dt, int tmima_id, int syear)
        {
            //UserFunctions.RadWindowAlert("dt = " + dt + " , syear = "+ syear);

            if (!ValidateDate(db, dt, syear) || tmima_id == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί διδ. έτος και τμήμα");
                return;
            }

            int lastweek = LastSavedWeekSM(db, tmima_id);
            if (lastweek == 0) return;
            DateTime firstDate = LastWeekFirstDateSM(db, tmima_id, lastweek);
            DateTime lastDate = firstDate.AddDays(4);
            int nextweek = lastweek + 1;

            if (calendar.SelectedDate == null)
            {
                calendar.SelectedDate = firstDate;
            }

            string msg = "Θα γίνει μεταφορά του ωρολόγιου προγρ. της τελ.εβδομάδας στην επόμενη.\n";
            msg += "Από την εβδ.# " + lastweek + " : " + firstDate.ToString("dd/MM/yyyy") + " - " + lastDate.ToString("dd/MM/yyyy") + "\n";
            msg += "στην εβδ.# " + nextweek + " : " + firstDate.AddDays(7).ToString("dd/MM/yyyy") + " - " + lastDate.AddDays(7).ToString("dd/MM/yyyy") + "\n";
            msg += "\nΝα γίνει η μεταφορά;";

            if (MessageBox.Show(msg, "Μεταφορά εβδ. προγράμματος", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
            { return; }

            TransferWeekProgrammmeSM(db, dataGrid, firstDate, tmima_id, lastweek);

        }   // method TransferWeekSM

        private void TransferWeekProgrammmeSM(PoseidonDataContext db, RadGridView dataGrid, DateTime sourceDate, int tmima, int sourceWeek)
        {
            DateTime targetFirstDate = sourceDate.AddDays(7);
            DateTime targetLastDate = targetFirstDate.AddDays(4);

            int iday = 0;
            DateTime targetDate = targetFirstDate;

            while (targetDate.AddDays(iday) <= targetLastDate)
            {
                TransferDaySM(db, dataGrid, sourceDate.AddDays(iday), targetDate.AddDays(iday), tmima, sourceWeek);
                iday++;
            }

            var programma = from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΜs
                            where p.ΗΜΕΡΟΜΗΝΙΑ == targetFirstDate && p.ΤΜΗΜΑ == tmima
                            select p;

            dataGrid.ItemsSource = programma.ToList();
        }

        private void TransferDaySM(PoseidonDataContext db, RadGridView dataGrid, DateTime sdate, DateTime tdate, int tmima, int sourceWeek)
        {
            var sourceDataDay = from s in db.ΠΡΟΓΡΑΜΜΑ_ΣΜs
                                where s.ΗΜΕΡΟΜΗΝΙΑ == sdate && s.ΤΜΗΜΑ == tmima && s.ΕΒΔΟΜΑΔΑ == sourceWeek
                                orderby s.ΤΜΗΜΑ, s.ΗΜΕΡΟΜΗΝΙΑ ascending
                                select s;

            int targetWeek = sourceWeek + 1;

            foreach (var row in sourceDataDay)
            {
                ΠΡΟΓΡΑΜΜΑ_ΣΜ newRow = dataGrid.Items.AddNew() as ΠΡΟΓΡΑΜΜΑ_ΣΜ;

                newRow.ΤΜΗΜΑ = (int)row.ΤΜΗΜΑ;
                newRow.ΗΜΕΡΟΜΗΝΙΑ = tdate;
                newRow.ΩΡΑ = row.ΩΡΑ;
                newRow.ΕΒΔΟΜΑΔΑ = (short)targetWeek;
                newRow.ΜΑΘΗΜΑ = (int)row.ΜΑΘΗΜΑ;
                newRow.ΔΙΔ_ΕΡΓΟ = (short)row.ΔΙΔ_ΕΡΓΟ;
                newRow.ΕΚΠΑΙΔΕΥΤΗΣ1 = row.ΕΚΠΑΙΔΕΥΤΗΣ1;
                newRow.ΕΚΠΑΙΔΕΥΤΗΣ2 = row.ΕΚΠΑΙΔΕΥΤΗΣ2;
                newRow.ΕΚΠΑΙΔΕΥΤΗΣ3 = row.ΕΚΠΑΙΔΕΥΤΗΣ3;
                newRow.Π1 = row.Π1;
                newRow.Π2 = row.Π2;
                newRow.Π3 = row.Π3;
                newRow.ΔΙΟΙΚΗΤΗΣ = row.ΔΙΟΙΚΗΤΗΣ;

                db.ΠΡΟΓΡΑΜΜΑ_ΣΜs.InsertOnSubmit(newRow);
                cm.CommitData(db);

            }

        }   // method TransferDay

        private int LastSavedWeekSM(PoseidonDataContext db, int tmima)
        {
            // πρώτα ελέγχουμε αν υπάρχει καταχωρημένο πρόγραμμα για το τμήμα
            int recs = (from p1 in db.ΠΡΟΓΡΑΜΜΑ_ΣΜs
                        where p1.ΤΜΗΜΑ == tmima
                        orderby p1.ΗΜΕΡΟΜΗΝΙΑ ascending
                        select p1).Count();

            if (recs == 0)
            {
                UserFunctions.ShowAdminMessage("Δεν βρέθηκε καταχωρημένο πρόγραμμα για το τμήμα αυτό.");
                return 0;
            }

            var programma = (from p2 in db.ΠΡΟΓΡΑΜΜΑ_ΣΜs
                             where p2.ΤΜΗΜΑ == tmima
                             orderby p2.ΗΜΕΡΟΜΗΝΙΑ descending
                             select p2).FirstOrDefault();

            int week = (int)programma.ΕΒΔΟΜΑΔΑ;
            return week;

        }

        public DateTime LastWeekFirstDateSM(PoseidonDataContext db, int tmima, int lastweek)
        {
            DateTime firstDate;

            var programma = (from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΜs
                             where p.ΤΜΗΜΑ == tmima && p.ΕΒΔΟΜΑΔΑ == lastweek
                             orderby p.ΗΜΕΡΟΜΗΝΙΑ ascending
                             select p).FirstOrDefault();

            firstDate = programma.ΗΜΕΡΟΜΗΝΙΑ;
            return firstDate;
        }

        #endregion

        #region SP Methods

        public void TransferHourSP(PoseidonDataContext db, RadGridView dataGrid, bool saved, bool newRec)
        {
            if (!saved && newRec)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να προηγηθεί αποθήκευση πριν την μεταφορά ώρας.");
                return;
            }

            ΠΡΟΓΡΑΜΜΑ_ΣΠ _row = dataGrid.SelectedItem as ΠΡΟΓΡΑΜΜΑ_ΣΠ;
            if (_row == null || !IsLastRowSP(db, _row))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε την τελευταία γραμμή του πίνακα για μεταφορά.");
                return;
            }

            ΠΡΟΓΡΑΜΜΑ_ΣΠ NewRow = dataGrid.Items.AddNew() as ΠΡΟΓΡΑΜΜΑ_ΣΠ;
            SetNewRowDataSP(db, _row, NewRow);
            //_lastRow = false;
        }

        private bool IsLastRowSP(PoseidonDataContext db, ΠΡΟΓΡΑΜΜΑ_ΣΠ currentRow)
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

        private void SetNewRowDataSP(PoseidonDataContext db, ΠΡΟΓΡΑΜΜΑ_ΣΠ currentRow, ΠΡΟΓΡΑΜΜΑ_ΣΠ newRow)
        {
            newRow.ΤΜΗΜΑ = (int)currentRow.ΤΜΗΜΑ;
            newRow.ΗΜΕΡΟΜΗΝΙΑ = (DateTime)currentRow.ΗΜΕΡΟΜΗΝΙΑ;
            newRow.ΩΡΑ = (short)(currentRow.ΩΡΑ + 1);
            newRow.ΕΒΔΟΜΑΔΑ = (short)currentRow.ΕΒΔΟΜΑΔΑ;
            newRow.ΜΑΘΗΜΑ = (int)currentRow.ΜΑΘΗΜΑ;
            newRow.ΔΙΔ_ΕΡΓΟ = (short)currentRow.ΔΙΔ_ΕΡΓΟ;
            newRow.ΕΚΠΑΙΔΕΥΤΗΣ1 = currentRow.ΕΚΠΑΙΔΕΥΤΗΣ1;
            newRow.ΕΚΠΑΙΔΕΥΤΗΣ2 = currentRow.ΕΚΠΑΙΔΕΥΤΗΣ2;
            newRow.ΕΚΠΑΙΔΕΥΤΗΣ3 = currentRow.ΕΚΠΑΙΔΕΥΤΗΣ3;
            newRow.Π1 = currentRow.Π1;
            newRow.Π2 = currentRow.Π2;
            newRow.Π3 = currentRow.Π3;
            newRow.ΔΙΟΙΚΗΤΗΣ = currentRow.ΔΙΟΙΚΗΤΗΣ;

            //ShowRowData(newRow);

            db.ΠΡΟΓΡΑΜΜΑ_ΣΠs.InsertOnSubmit(newRow);
        }

        public void TransferWeekSP(PoseidonDataContext db, RadCalendar calendar, RadGridView dataGrid, DateTime dt, int tmima_id, int syear)
        {
            //UserFunctions.RadWindowAlert("dt = " + dt + " , syear = "+ syear);

            if (!ValidateDate(db, dt, syear) || tmima_id == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί διδ. έτος και τμήμα");
                return;
            }

            int lastweek = LastSavedWeekSP(db, tmima_id);
            if (lastweek == 0) return;
            DateTime firstDate = LastWeekFirstDateSP(db, tmima_id, lastweek);
            DateTime lastDate = firstDate.AddDays(4);
            int nextweek = lastweek + 1;

            if (calendar.SelectedDate == null)
            {
                calendar.SelectedDate = firstDate;
            }

            string msg = "Θα γίνει μεταφορά του ωρολόγιου προγρ. της τελ.εβδομάδας στην επόμενη.\n";
            msg += "Από την εβδ.# " + lastweek + " : " + firstDate.ToString("dd/MM/yyyy") + " - " + lastDate.ToString("dd/MM/yyyy") + "\n";
            msg += "στην εβδ.# " + nextweek + " : " + firstDate.AddDays(7).ToString("dd/MM/yyyy") + " - " + lastDate.AddDays(7).ToString("dd/MM/yyyy") + "\n";
            msg += "\nΝα γίνει η μεταφορά;";

            if (MessageBox.Show(msg, "Μεταφορά εβδ. προγράμματος", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
            { return; }

            TransferWeekProgrammmeSP(db, dataGrid, firstDate, tmima_id, lastweek);

        }   // method TransferWeekSM

        private void TransferWeekProgrammmeSP(PoseidonDataContext db, RadGridView dataGrid, DateTime sourceDate, int tmima, int sourceWeek)
        {
            DateTime targetFirstDate = sourceDate.AddDays(7);
            DateTime targetLastDate = targetFirstDate.AddDays(4);

            int iday = 0;
            DateTime targetDate = targetFirstDate;

            while (targetDate.AddDays(iday) <= targetLastDate)
            {
                TransferDaySP(db, dataGrid, sourceDate.AddDays(iday), targetDate.AddDays(iday), tmima, sourceWeek);
                iday++;
            }

            var programma = from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                            where p.ΗΜΕΡΟΜΗΝΙΑ == targetFirstDate && p.ΤΜΗΜΑ == tmima
                            select p;

            dataGrid.ItemsSource = programma.ToList();
        }

        private void TransferDaySP(PoseidonDataContext db, RadGridView dataGrid, DateTime sdate, DateTime tdate, int tmima, int sourceWeek)
        {
            var sourceDataDay = from s in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                                where s.ΗΜΕΡΟΜΗΝΙΑ == sdate && s.ΤΜΗΜΑ == tmima && s.ΕΒΔΟΜΑΔΑ == sourceWeek
                                orderby s.ΤΜΗΜΑ, s.ΗΜΕΡΟΜΗΝΙΑ ascending
                                select s;

            int targetWeek = sourceWeek + 1;

            foreach (var row in sourceDataDay)
            {
                ΠΡΟΓΡΑΜΜΑ_ΣΠ newRow = dataGrid.Items.AddNew() as ΠΡΟΓΡΑΜΜΑ_ΣΠ;

                newRow.ΤΜΗΜΑ = (int)row.ΤΜΗΜΑ;
                newRow.ΗΜΕΡΟΜΗΝΙΑ = tdate;
                newRow.ΩΡΑ = row.ΩΡΑ;
                newRow.ΕΒΔΟΜΑΔΑ = (short)targetWeek;
                newRow.ΜΑΘΗΜΑ = (int)row.ΜΑΘΗΜΑ;
                newRow.ΔΙΔ_ΕΡΓΟ = (short)row.ΔΙΔ_ΕΡΓΟ;
                newRow.ΕΚΠΑΙΔΕΥΤΗΣ1 = row.ΕΚΠΑΙΔΕΥΤΗΣ1;
                newRow.ΕΚΠΑΙΔΕΥΤΗΣ2 = row.ΕΚΠΑΙΔΕΥΤΗΣ2;
                newRow.ΕΚΠΑΙΔΕΥΤΗΣ3 = row.ΕΚΠΑΙΔΕΥΤΗΣ3;
                newRow.Π1 = row.Π1;
                newRow.Π2 = row.Π2;
                newRow.Π3 = row.Π3;
                newRow.ΔΙΟΙΚΗΤΗΣ = row.ΔΙΟΙΚΗΤΗΣ;

                db.ΠΡΟΓΡΑΜΜΑ_ΣΠs.InsertOnSubmit(newRow);
                cm.CommitData(db);

            }

        }   // method TransferDay

        private int LastSavedWeekSP(PoseidonDataContext db, int tmima)
        {
            // πρώτα ελέγχουμε αν υπάρχει καταχωρημένο πρόγραμμα για το τμήμα
            int recs = (from p1 in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                        where p1.ΤΜΗΜΑ == tmima
                        orderby p1.ΗΜΕΡΟΜΗΝΙΑ ascending
                        select p1).Count();

            if (recs == 0)
            {
                UserFunctions.ShowAdminMessage("Δεν βρέθηκε καταχωρημένο πρόγραμμα για το τμήμα αυτό.");
                return 0;
            }

            var programma = (from p2 in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                             where p2.ΤΜΗΜΑ == tmima
                             orderby p2.ΗΜΕΡΟΜΗΝΙΑ descending
                             select p2).FirstOrDefault();

            int week = (int)programma.ΕΒΔΟΜΑΔΑ;
            return week;

        }

        public DateTime LastWeekFirstDateSP(PoseidonDataContext db, int tmima, int lastweek)
        {
            DateTime firstDate;

            var programma = (from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                             where p.ΤΜΗΜΑ == tmima && p.ΕΒΔΟΜΑΔΑ == lastweek
                             orderby p.ΗΜΕΡΟΜΗΝΙΑ ascending
                             select p).FirstOrDefault();

            firstDate = programma.ΗΜΕΡΟΜΗΝΙΑ;
            return firstDate;
        }


        #endregion

        #region Common SM, SP Methods

        public bool ValidateDate(PoseidonDataContext db, DateTime date, int syear)
        {
            var dates = (from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == syear
                         select new { s.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ, s.ΣΧ_ΕΤΟΣ_ΛΗΞΗ }).FirstOrDefault();

            if (dates == null)
            {
                UserFunctions.ShowAdminMessage("Δεν έχετε επιλέξει διδακτικό έτος. Πατήστε Esc και επιλέξατε διδ.έτος.");
                return false;
            }

            DateTime _date1 = (DateTime)dates.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ;
            DateTime _date2 = (DateTime)dates.ΣΧ_ΕΤΟΣ_ΛΗΞΗ;


            if (date < _date1 || date > _date2)
            {
                UserFunctions.ShowAdminMessage("Μη έγκυρη ημερομηνία. Πατήστε Esc και επιλέξατε έγκυρη ημ/νια.");
                return false;
            }
            else return true;

        }

        #endregion

    }   // class TeacherModel
}
