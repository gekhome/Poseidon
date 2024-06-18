using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poseidon.Model;
using Poseidon.Utilities;


namespace Poseidon.DataModel
{

    class AnathesiModel
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public IList<ΑΝΑΘΕΣΕΙΣ_ΣΜ> LoadAnathesiDataSM(string afm)
        {
            PoseidonDataContext db = new PoseidonDataContext();
            IList<ΑΝΑΘΕΣΕΙΣ_ΣΜ> anatheseis = null;
            int school_year = SelectedSchoolYear.SchoolYear;
            int user_key = LoginClass.UserKey;

                var anatheseis_data = from dba in db.ΑΝΑΘΕΣΕΙΣ_ΣΜs
                                      where dba.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year && dba.ΑΦΜ == afm
                                      orderby dba.ΣΧΟΛΙΚΟ_ΕΤΟΣ, dba.ΑΝΑΘΕΣΗ_ΚΩΔ
                                      select dba;
                anatheseis = anatheseis_data.ToList();

            return anatheseis;
        }

        public IList<ΑΝΑΘΕΣΕΙΣ_ΣΠ> LoadAnathesiDataSP(string afm)
        {
            PoseidonDataContext db = new PoseidonDataContext();
            IList<ΑΝΑΘΕΣΕΙΣ_ΣΠ> anatheseis = null;
            int school_year = SelectedSchoolYear.SchoolYear;
            int user_key = LoginClass.UserKey;

            var anatheseis_data = from dba in db.ΑΝΑΘΕΣΕΙΣ_ΣΠs
                                  where dba.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year && dba.ΑΦΜ == afm
                                  orderby dba.ΣΧΟΛΙΚΟ_ΕΤΟΣ, dba.ΑΝΑΘΕΣΗ_ΚΩΔ
                                  select dba;
            anatheseis = anatheseis_data.ToList();

            return anatheseis;
        }

    }   //class AnathesiModel

    public static class SelectedTeacher
    {
        private static PoseidonDataContext db = new PoseidonDataContext();

        public static string TeacherAFM { get; set; }

        public static string TeacherLastName { get; set; }

        public static string TeacherFirstName { get; set; }


    } //class SelectedTeacher

    public static class SelectedSchoolYear
    {
        private static PoseidonDataContext db = new PoseidonDataContext();

        public static int SchoolYear { get; set; }

    } // class SelectedSchoolYear

    public static class SelectedTerm
    {
        private static PoseidonDataContext db = new PoseidonDataContext();
        public static int Term { get; set; }

    } // class SelectedTerm

    public static class SelectedLesson
    {
        private static PoseidonDataContext db = new PoseidonDataContext();
        public static int Lesson { get; set; }

    } // class SelectedTerm

    public static class AnathesiPersistentDataSM
    {
        public static PoseidonDataContext db = new PoseidonDataContext();

        #region Class Properties SM

        public static string Afm { get; set; }
        public static int Klados { get; set; }
        public static int Eidikotita { get; set; }
        public static int SchoolYear { get; set; }
        public static int Term { get; set; }
        public static int Post { get; set; }
        //public static int Lesson {get; set; }
        public static bool Msc { get; set; }
        public static bool Phd { get; set; }

        // Anathesi new record flag Property
        public static bool NewRecord { get; set; }

        #endregion

        #region Class Methods

        public static bool AnatheseisExistSM(string afm, int school_year)
        {
            bool result = false;

            var anathesi_cnt = (from a in db.ΑΝΑΘΕΣΕΙΣ_ΣΜs
                                where a.ΑΦΜ == afm && a.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                                select a).Count();

            if (anathesi_cnt > 0) result = true;
            else result = false;
            return result;
        }

        public static void SetDefaultModelFieldsSM(string afm, int school_year)
        {
            var anathesi = (from an in db.ΑΝΑΘΕΣΕΙΣ_ΣΜs
                            where an.ΑΝΑΘΕΣΗ_ΚΩΔ != 0 && an.ΑΦΜ == afm && an.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                            orderby an.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                            select an).FirstOrDefault();

            if (anathesi != null)
            {
                Afm = anathesi.ΑΦΜ;
                Klados = (int)anathesi.ΚΛΑΔΟΣ;
                Eidikotita = (int)anathesi.ΕΙΔΙΚΟΤΗΤΑ;
                SchoolYear = (int)anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
                Term = (int)anathesi.ΕΞΑΜΗΝΟ;
                Post = (int)anathesi.ΚΑΤΗΓΟΡΙΑ;
                //Lesson = (int)anathesi.ΜΑΘΗΜΑ;
                Msc = (bool)anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ;
                Phd = (bool)anathesi.ΔΙΔΑΚΤΟΡΙΚΟ;
            }
            else
            {
                Afm = SelectedTeacher.TeacherAFM;
                SchoolYear = SelectedSchoolYear.SchoolYear;
            }
        }

        public static void SetModelFieldsSM(ΑΝΑΘΕΣΕΙΣ_ΣΜ anathesi)
        {
            if (anathesi.ΑΦΜ != null && Afm != anathesi.ΑΦΜ) Afm = anathesi.ΑΦΜ;

            if (anathesi.ΚΛΑΔΟΣ != 0 && Klados != anathesi.ΚΛΑΔΟΣ)
                Klados = Convert.ToInt32(anathesi.ΚΛΑΔΟΣ);

            if (anathesi.ΕΙΔΙΚΟΤΗΤΑ != 0 && Eidikotita != anathesi.ΕΙΔΙΚΟΤΗΤΑ)
                Eidikotita = Convert.ToInt32(anathesi.ΕΙΔΙΚΟΤΗΤΑ);

            if (anathesi.ΚΑΤΗΓΟΡΙΑ != 0 && Post != anathesi.ΚΑΤΗΓΟΡΙΑ)
                Post = Convert.ToInt32(anathesi.ΚΑΤΗΓΟΡΙΑ);

            if (anathesi.ΕΞΑΜΗΝΟ != 0 && Term != anathesi.ΕΞΑΜΗΝΟ)
                Term = Convert.ToInt32(anathesi.ΕΞΑΜΗΝΟ);

            if (anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ != 0 && SchoolYear != anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ)
                SchoolYear = Convert.ToInt32(anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ);

            //if (anathesi.ΜΑΘΗΜΑ != 0 && Lesson != anathesi.ΜΑΘΗΜΑ)
            //    Lesson = Convert.ToInt32(anathesi.ΜΑΘΗΜΑ);

            if (anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ != null && Msc != anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ) Msc = (bool)anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ;

            if (anathesi.ΔΙΔΑΚΤΟΡΙΚΟ != null && Phd != anathesi.ΔΙΔΑΚΤΟΡΙΚΟ) Phd = (bool)anathesi.ΔΙΔΑΚΤΟΡΙΚΟ;

        }

        public static void SetEntityFieldsSM(ΑΝΑΘΕΣΕΙΣ_ΣΜ anathesi)
        {
            anathesi.ΑΦΜ = Afm;
            anathesi.ΚΛΑΔΟΣ = Klados;
            anathesi.ΕΙΔΙΚΟΤΗΤΑ = Eidikotita;
            anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ = SchoolYear;
            anathesi.ΕΞΑΜΗΝΟ = Term;
            anathesi.ΚΑΤΗΓΟΡΙΑ = Post;
            //anathesi.ΜΑΘΗΜΑ = Lesson;
            anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ = Msc;
            anathesi.ΔΙΔΑΚΤΟΡΙΚΟ = Phd;
        }

        public static void EditEntityFieldsSM(PoseidonDataContext dbui, ΑΝΑΘΕΣΕΙΣ_ΣΜ anathesi)
        {
            ΑΝΑΘΕΣΕΙΣ_ΣΜ toEdit = dbui.ΑΝΑΘΕΣΕΙΣ_ΣΜs.Single(s => s.ΑΝΑΘΕΣΗ_ΚΩΔ == anathesi.ΑΝΑΘΕΣΗ_ΚΩΔ);
            SetModelFieldsSM(anathesi);
            SetEntityFieldsSM(anathesi);

            toEdit.ΑΦΜ = anathesi.ΑΦΜ;
            toEdit.ΚΛΑΔΟΣ = anathesi.ΚΛΑΔΟΣ;
            toEdit.ΕΙΔΙΚΟΤΗΤΑ = anathesi.ΕΙΔΙΚΟΤΗΤΑ;
            toEdit.ΣΧΟΛΙΚΟ_ΕΤΟΣ = anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            toEdit.ΕΞΑΜΗΝΟ = anathesi.ΕΞΑΜΗΝΟ;
            toEdit.ΚΑΤΗΓΟΡΙΑ = anathesi.ΚΑΤΗΓΟΡΙΑ;
            toEdit.ΜΑΘΗΜΑ = anathesi.ΜΑΘΗΜΑ;
            toEdit.ΜΕΤΑΠΤΥΧΙΑΚΟ = anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ;
            toEdit.ΔΙΔΑΚΤΟΡΙΚΟ = anathesi.ΔΙΔΑΚΤΟΡΙΚΟ;
        }

        public static void AddEntityFieldsSM(PoseidonDataContext dbui, ΑΝΑΘΕΣΕΙΣ_ΣΜ anathesi)
        {
            SetModelFieldsSM(anathesi);
            SetEntityFieldsSM(anathesi);
            dbui.ΑΝΑΘΕΣΕΙΣ_ΣΜs.InsertOnSubmit(anathesi);
        }

        #endregion

    } // class AnathesiPersistentDataEpas

    public static class AnathesiPersistentDataSP
    {
        public static PoseidonDataContext db = new PoseidonDataContext();

        #region Class Properties SP

        public static string Afm { get; set; }
        public static int Klados { get; set; }
        public static int Eidikotita { get; set; }
        public static int SchoolYear { get; set; }
        public static int Term { get; set; }
        public static int Post { get; set; }
        //public static int Lesson { get; set; }
        public static bool Msc { get; set; }
        public static bool Phd { get; set; }

        // Anathesi new record flag Property
        public static bool NewRecord { get; set; }

        #endregion

        #region Class Methods

        public static bool AnatheseisExistSP(string afm, int school_year)
        {
            bool result = false;

            var anathesi_cnt = (from a in db.ΑΝΑΘΕΣΕΙΣ_ΣΠs
                                where a.ΑΦΜ == afm && a.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                                select a).Count();

            if (anathesi_cnt > 0) result = true;
            else result = false;
            return result;
        }

        public static void SetDefaultModelFieldsSP(string afm, int school_year)
        {
            var anathesi = (from an in db.ΑΝΑΘΕΣΕΙΣ_ΣΠs
                            where an.ΑΝΑΘΕΣΗ_ΚΩΔ != 0 && an.ΑΦΜ == afm && an.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                            orderby an.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                            select an).FirstOrDefault();

            if (anathesi != null)
            {
                Afm = anathesi.ΑΦΜ;
                Klados = (int)anathesi.ΚΛΑΔΟΣ;
                Eidikotita = (int)anathesi.ΕΙΔΙΚΟΤΗΤΑ;
                SchoolYear = (int)anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
                Term = (int)anathesi.ΕΞΑΜΗΝΟ;
                Post = (int)anathesi.ΚΑΤΗΓΟΡΙΑ;
                //Lesson = (int)anathesi.ΜΑΘΗΜΑ;
                Msc = (bool)anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ;
                Phd = (bool)anathesi.ΔΙΔΑΚΤΟΡΙΚΟ;

            }
            else
            {
                Afm = SelectedTeacher.TeacherAFM;
                SchoolYear = SelectedSchoolYear.SchoolYear;
            }
        }

        public static void SetModelFieldsSP(ΑΝΑΘΕΣΕΙΣ_ΣΠ anathesi)
        {
            if (anathesi.ΑΦΜ != null && Afm != anathesi.ΑΦΜ) Afm = anathesi.ΑΦΜ;

            if (anathesi.ΚΛΑΔΟΣ != 0 && Klados != anathesi.ΚΛΑΔΟΣ)
                Klados = Convert.ToInt32(anathesi.ΚΛΑΔΟΣ);

            if (anathesi.ΕΙΔΙΚΟΤΗΤΑ != 0 && Eidikotita != anathesi.ΕΙΔΙΚΟΤΗΤΑ)
                Eidikotita = Convert.ToInt32(anathesi.ΕΙΔΙΚΟΤΗΤΑ);

            if (anathesi.ΚΑΤΗΓΟΡΙΑ != 0 && Post != anathesi.ΚΑΤΗΓΟΡΙΑ)
                Post = Convert.ToInt32(anathesi.ΚΑΤΗΓΟΡΙΑ);

            if (anathesi.ΕΞΑΜΗΝΟ != 0 && Term != anathesi.ΕΞΑΜΗΝΟ)
                Term = Convert.ToInt32(anathesi.ΕΞΑΜΗΝΟ);

            if (anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ != 0 && SchoolYear != anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ)
                SchoolYear = Convert.ToInt32(anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ);

            //if (anathesi.ΜΑΘΗΜΑ != 0 && Lesson != anathesi.ΜΑΘΗΜΑ)
            //    Lesson = Convert.ToInt32(anathesi.ΜΑΘΗΜΑ);

            if (anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ != null && Msc != anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ) Msc = (bool)anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ;

            if (anathesi.ΔΙΔΑΚΤΟΡΙΚΟ != null && Phd != anathesi.ΔΙΔΑΚΤΟΡΙΚΟ) Phd = (bool)anathesi.ΔΙΔΑΚΤΟΡΙΚΟ;

        }

        public static void SetEntityFieldsSP(ΑΝΑΘΕΣΕΙΣ_ΣΠ anathesi)
        {
            anathesi.ΑΦΜ = Afm;
            anathesi.ΚΛΑΔΟΣ = Klados;
            anathesi.ΕΙΔΙΚΟΤΗΤΑ = Eidikotita;
            anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ = SchoolYear;
            anathesi.ΕΞΑΜΗΝΟ = Term;
            anathesi.ΚΑΤΗΓΟΡΙΑ = Post;
            //anathesi.ΜΑΘΗΜΑ = Lesson;
            anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ = Msc;
            anathesi.ΔΙΔΑΚΤΟΡΙΚΟ = Phd;
        }

        public static void EditEntityFieldsSP(PoseidonDataContext dbui, ΑΝΑΘΕΣΕΙΣ_ΣΠ anathesi)
        {
            ΑΝΑΘΕΣΕΙΣ_ΣΠ toEdit = dbui.ΑΝΑΘΕΣΕΙΣ_ΣΠs.Single(s => s.ΑΝΑΘΕΣΗ_ΚΩΔ == anathesi.ΑΝΑΘΕΣΗ_ΚΩΔ);
            SetModelFieldsSP(anathesi);
            SetEntityFieldsSP(anathesi);

            toEdit.ΑΦΜ = anathesi.ΑΦΜ;
            toEdit.ΚΛΑΔΟΣ = anathesi.ΚΛΑΔΟΣ;
            toEdit.ΕΙΔΙΚΟΤΗΤΑ = anathesi.ΕΙΔΙΚΟΤΗΤΑ;
            toEdit.ΣΧΟΛΙΚΟ_ΕΤΟΣ = anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            toEdit.ΕΞΑΜΗΝΟ = anathesi.ΕΞΑΜΗΝΟ;
            toEdit.ΚΑΤΗΓΟΡΙΑ = anathesi.ΚΑΤΗΓΟΡΙΑ;
            toEdit.ΜΑΘΗΜΑ = anathesi.ΜΑΘΗΜΑ;
            toEdit.ΜΕΤΑΠΤΥΧΙΑΚΟ = anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ;
            toEdit.ΔΙΔΑΚΤΟΡΙΚΟ = anathesi.ΔΙΔΑΚΤΟΡΙΚΟ;
        }

        public static void AddEntityFieldsSP(PoseidonDataContext dbui, ΑΝΑΘΕΣΕΙΣ_ΣΠ anathesi)
        {
            SetModelFieldsSP(anathesi);
            SetEntityFieldsSP(anathesi);
            dbui.ΑΝΑΘΕΣΕΙΣ_ΣΠs.InsertOnSubmit(anathesi);
        }

        #endregion

    } // class AnathesiPersistentDataEpas

}
