using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poseidon.Model;
using Poseidon.Utilities;

namespace Poseidon.DataModel
{
    class StudentModel
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public List<ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ> LoadEgrafesData(string amka, int school_year)
        {
            PoseidonDataContext db = new PoseidonDataContext();
            List<ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ> egrafes = null;

            var egrafes_data = from e in db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs
                               where e.ΑΜΚΑ == amka && e.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == school_year
                                 orderby e.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ descending, e.ΚΩΔΙΚΟΣ
                                 select e;
            egrafes = egrafes_data.ToList();

            return egrafes;
        }

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

    }   // class StudentModel

    public static class SelectedStudent
    {
        private static PoseidonDataContext db = new PoseidonDataContext();

        public static string AMKA { get; set; }

        public static string LastName { get; set; }

        public static string FirstName { get; set; }


    } //class SelectedStudent


    public static class EgrafesSchoolYear
    {
        private static PoseidonDataContext db = new PoseidonDataContext();

        public static int SchoolYear { get; set; }

    } // class SelectedSchoolYear


    public static class EgrafesSchool
    {
        private static PoseidonDataContext db = new PoseidonDataContext();

        public static int School { get; set; }
    }


    public static class EgrafesPersistentData
    {
        public static PoseidonDataContext db = new PoseidonDataContext();

        #region Class Properties

        public static string Amka { get; set; }
        public static int School { get; set; }
        public static int SchoolYear { get; set; }
        public static int Tmima { get; set; }
        public static int Fitisi { get; set; }
        public static DateTime EgrafiDate { get; set; }

        // Egrafi new record flag Property
        public static bool NewRecord { get; set; }

        #endregion

        #region Class Methods

        public static bool EgrafesExistSM(int school_year)
        {
            bool result = false;

            var egrafi_cnt = (from s in db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs
                               where s.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == school_year && s.ΣΧΟΛΗ == 2
                               select s).Count();

            if (egrafi_cnt > 0) result = true;
            else result = false;
            return result;
        }

        public static void EgrafiSetDefaultModelFieldsSM(int school_year)
        {
            var egrafi = (from s in db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs
                           where s.ΚΩΔΙΚΟΣ > 0 && s.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == school_year && s.ΣΧΟΛΗ == 2
                           orderby s.ΚΩΔΙΚΟΣ descending
                           select s).FirstOrDefault();

            if (egrafi != null)
            {
                Amka = egrafi.ΑΜΚΑ;
                School = (int)egrafi.ΣΧΟΛΗ;
                SchoolYear = (int)egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ;
                Tmima = (int)egrafi.ΤΜΗΜΑ;
                Fitisi = (int)egrafi.ΦΟΙΤΗΣΗ;
                EgrafiDate = (DateTime)egrafi.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ;

            }
            else
            {
                Amka = SelectedStudent.AMKA;
                School = EgrafesSchoolYear.SchoolYear;
                SchoolYear = EgrafesSchoolYear.SchoolYear;
            }
        }

        public static bool EgrafesExistSP(int school_year)
        {
            bool result = false;

            var egrafi_cnt = (from s in db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs
                              where s.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == school_year && s.ΣΧΟΛΗ == 1
                              select s).Count();

            if (egrafi_cnt > 0) result = true;
            else result = false;
            return result;
        }

        public static void EgrafiSetDefaultModelFieldsSP(int school_year)
        {
            var egrafi = (from s in db.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs
                          where s.ΚΩΔΙΚΟΣ > 0 && s.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == school_year && s.ΣΧΟΛΗ == 1
                          orderby s.ΚΩΔΙΚΟΣ descending
                          select s).FirstOrDefault();

            if (egrafi != null)
            {
                Amka = egrafi.ΑΜΚΑ;
                School = (int)egrafi.ΣΧΟΛΗ;
                SchoolYear = (int)egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ;
                Tmima = (int)egrafi.ΤΜΗΜΑ;
                Fitisi = (int)egrafi.ΦΟΙΤΗΣΗ;
                EgrafiDate = (DateTime)egrafi.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ;

            }
            else
            {
                Amka = SelectedStudent.AMKA;
                School = EgrafesSchoolYear.SchoolYear;
                SchoolYear = EgrafesSchoolYear.SchoolYear;
            }
        }


        public static void EgrafiSetModelFields(ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ egrafi)
        {
            if (!String.IsNullOrEmpty(egrafi.ΑΜΚΑ) && Amka != egrafi.ΑΜΚΑ) Amka = egrafi.ΑΜΚΑ;

            if (egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ != 0 && SchoolYear != egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ)
                SchoolYear = Convert.ToInt32(egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ);

            if (egrafi.ΣΧΟΛΗ != null && School != egrafi.ΣΧΟΛΗ) School = (int)egrafi.ΣΧΟΛΗ;

            if (egrafi.ΤΜΗΜΑ != null && Tmima != egrafi.ΤΜΗΜΑ) Tmima = (int)egrafi.ΤΜΗΜΑ;

            if (egrafi.ΦΟΙΤΗΣΗ != null && Fitisi != egrafi.ΦΟΙΤΗΣΗ) Fitisi = (int)egrafi.ΦΟΙΤΗΣΗ;

            if (egrafi.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ != null && EgrafiDate != (DateTime)egrafi.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ) EgrafiDate = (DateTime)egrafi.ΗΜΝΙΑ_ΕΓΓΡΑΦΗ;
        }

        public static void EgrafiSetEntityFields(ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ egrafi)
        {
            egrafi.ΑΜΚΑ = Amka;
            egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = SchoolYear;
            egrafi.ΣΧΟΛΗ = School;
            egrafi.ΤΜΗΜΑ = Tmima;
            egrafi.ΦΟΙΤΗΣΗ = Fitisi;
        }

        public static void EgrafiEditEntityFields(PoseidonDataContext dbui, ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ egrafi)
        {
            ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ toEdit = dbui.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs.Single(s => s.ΚΩΔΙΚΟΣ == egrafi.ΚΩΔΙΚΟΣ);
            EgrafiSetModelFields(egrafi);
            EgrafiSetEntityFields(egrafi);

            toEdit.ΑΜΚΑ = egrafi.ΑΜΚΑ;
            toEdit.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ = egrafi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ;
            toEdit.ΣΧΟΛΗ = egrafi.ΣΧΟΛΗ;
            toEdit.ΤΜΗΜΑ = egrafi.ΤΜΗΜΑ;
            toEdit.ΦΟΙΤΗΣΗ = egrafi.ΦΟΙΤΗΣΗ;
        }

        public static void EgrafiAddEntityFields(PoseidonDataContext dbui, ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣ egrafi)
        {
            EgrafiSetModelFields(egrafi);
            EgrafiSetEntityFields(egrafi);
            dbui.ΣΠΟΥΔΑΣΤΗΣ_ΕΓΓΡΑΦΕΣs.InsertOnSubmit(egrafi);
        }

        #endregion


    } // class AnathesiPersistentDataEpas

}
