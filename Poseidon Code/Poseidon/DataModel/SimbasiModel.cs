using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poseidon.Model;
using Poseidon.Utilities;

namespace Poseidon.DataModel
{
    class SimbasiModel
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public List<ΕΚΠ_ΣΥΜΒΑΣΗ> LoadSimbasiData(string afm)
        {
            PoseidonDataContext db = new PoseidonDataContext();
            List<ΕΚΠ_ΣΥΜΒΑΣΗ> simbaseis = null;

            var simbaseis_data = from dba in db.ΕΚΠ_ΣΥΜΒΑΣΗs
                                  where dba.ΑΦΜ == afm
                                  orderby dba.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, dba.ΣΥΜΒΑΣΗ_ΚΩΔ
                                  select dba;
            simbaseis = simbaseis_data.ToList();
            //UserFunctions.RadWindowAlert("afm = "+afm);

            return simbaseis;
        }


    }   // class SimbasiModel

    public static class SimbasiTeacher
    {
        private static PoseidonDataContext db = new PoseidonDataContext();

        public static string AFM { get; set; }

        public static string LastName { get; set; }

        public static string FirstName { get; set; }


    } //class SelectedTeacher

    public static class SimbasiSchoolYear
    {
        private static PoseidonDataContext db = new PoseidonDataContext();

        public static int SYear { get; set; }

    } // class SelectedSchoolYear

    public static class SimbasiPersistentData
    {
        public static PoseidonDataContext db = new PoseidonDataContext();

        #region Class Properties

        public static string Afm { get; set; }
        public static int SchoolYear { get; set; }
        public static string City { get; set; }
        public static string Dioikitis { get; set; }
        public static string Prokirixi { get; set; }
        public static string Apofasi { get; set; }
        public static string Asfalisi { get; set; }
        public static int Commander { get; set; }

        public static string Protocol { get; set; }
        public static DateTime SimbasiDate { get; set; }
        public static string TeacherName { get; set; }
        public static string HireType { get; set; }
        public static string Eidikotita { get; set; }
        public static string ADA { get; set; }

        // Anathesi new record flag Property
        public static bool NewRecord { get; set; }

        #endregion

        #region Class Methods

        public static bool SimbaseisExist(int school_year)
        {
            bool result = false;

            var simbasi_cnt = (from s in db.ΕΚΠ_ΣΥΜΒΑΣΗs
                               where s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                                select s).Count();

            if (simbasi_cnt > 0) result = true;
            else result = false;
            return result;
        }

        public static void SetDefaultModelFields(int school_year)
        {
            var simbasi = (from s in db.ΕΚΠ_ΣΥΜΒΑΣΗs
                           where s.ΣΥΜΒΑΣΗ_ΚΩΔ > 0 && s.ΣΧΟΛΙΚΟ_ΕΤΟΣ == school_year
                           orderby s.ΣΥΜΒΑΣΗ_ΚΩΔ descending
                           select s).FirstOrDefault();

            if (simbasi != null)
            {
                //Afm = simbasi.ΑΦΜ;
                SchoolYear = (int)simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
                City = simbasi.ΠΟΛΗ;
                Dioikitis = simbasi.ΤΟΥ_ΔΙΟΙΚΗΤΗ;
                Prokirixi = simbasi.ΑΡ_ΠΡΟΚΗΡΥΞΗ;
                Apofasi = simbasi.ΑΡ_ΑΠΟΦΑΣΗ;
                Asfalisi = simbasi.ΑΣΦΑΛΙΣΤΙΚΟΣ_ΦΟΡΕΑΣ;
                Commander = (int)simbasi.ΔΙΟΙΚΗΤΗΣ;
                //Protocol = simbasi.ΠΡΩΤΟΚΟΛΛΟ;
                SimbasiDate = (DateTime)simbasi.ΗΜΕΡΟΜΗΝΙΑ;
                //TeacherName = simbasi.ΤΟΥ_ΕΚΠΑΙΔΕΥΤΙΚΟΥ;
                //HireType = simbasi.ΠΡΟΣΛΗΨΗ_ΩΣ;
                //Eidikotita = simbasi.ΕΙΔΙΚΟΤΗΤΑ;
                //ADA = simbasi.ΑΔΑ;
            }
            else
            {
                Afm = SimbasiTeacher.AFM;
                SchoolYear = SimbasiSchoolYear.SYear;
            }
        }

        public static void SetModelFields(ΕΚΠ_ΣΥΜΒΑΣΗ simbasi)
        {
            if (!String.IsNullOrEmpty(simbasi.ΑΦΜ) && Afm != simbasi.ΑΦΜ) Afm = simbasi.ΑΦΜ;

            if (simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ != 0 && SchoolYear != simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ)
                SchoolYear = Convert.ToInt32(simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ);

            if (!String.IsNullOrEmpty(simbasi.ΠΟΛΗ) && City != simbasi.ΠΟΛΗ) City = simbasi.ΠΟΛΗ;

            if (!String.IsNullOrEmpty(simbasi.ΤΟΥ_ΔΙΟΙΚΗΤΗ) && Dioikitis != simbasi.ΤΟΥ_ΔΙΟΙΚΗΤΗ) Dioikitis = simbasi.ΤΟΥ_ΔΙΟΙΚΗΤΗ;

            if (!String.IsNullOrEmpty(simbasi.ΑΡ_ΠΡΟΚΗΡΥΞΗ) && Prokirixi != simbasi.ΑΡ_ΠΡΟΚΗΡΥΞΗ) Prokirixi = simbasi.ΑΡ_ΠΡΟΚΗΡΥΞΗ;

            if (!String.IsNullOrEmpty(simbasi.ΑΡ_ΑΠΟΦΑΣΗ) && Apofasi != simbasi.ΑΡ_ΑΠΟΦΑΣΗ) Apofasi = simbasi.ΑΡ_ΑΠΟΦΑΣΗ;

            if (!String.IsNullOrEmpty(simbasi.ΑΣΦΑΛΙΣΤΙΚΟΣ_ΦΟΡΕΑΣ) && Asfalisi != simbasi.ΑΣΦΑΛΙΣΤΙΚΟΣ_ΦΟΡΕΑΣ) Asfalisi = simbasi.ΑΣΦΑΛΙΣΤΙΚΟΣ_ΦΟΡΕΑΣ;

            if (!String.IsNullOrEmpty(simbasi.ΠΡΩΤΟΚΟΛΛΟ) && Protocol != simbasi.ΠΡΩΤΟΚΟΛΛΟ) Protocol = simbasi.ΠΡΩΤΟΚΟΛΛΟ;

            if (simbasi.ΗΜΕΡΟΜΗΝΙΑ != null && SimbasiDate != (DateTime)simbasi.ΗΜΕΡΟΜΗΝΙΑ) SimbasiDate = (DateTime)simbasi.ΗΜΕΡΟΜΗΝΙΑ;

            if (simbasi.ΔΙΟΙΚΗΤΗΣ != 0 && Commander != simbasi.ΔΙΟΙΚΗΤΗΣ) Commander = Convert.ToInt32(simbasi.ΔΙΟΙΚΗΤΗΣ);

            //if (!String.IsNullOrEmpty(simbasi.ΤΟΥ_ΕΚΠΑΙΔΕΥΤΙΚΟΥ) && TeacherName != simbasi.ΤΟΥ_ΕΚΠΑΙΔΕΥΤΙΚΟΥ) TeacherName = simbasi.ΤΟΥ_ΕΚΠΑΙΔΕΥΤΙΚΟΥ;

            //if (!String.IsNullOrEmpty(simbasi.ΠΡΟΣΛΗΨΗ_ΩΣ) && HireType != simbasi.ΠΡΟΣΛΗΨΗ_ΩΣ) HireType = simbasi.ΠΡΟΣΛΗΨΗ_ΩΣ;

            //if (!String.IsNullOrEmpty(simbasi.ΕΙΔΙΚΟΤΗΤΑ) && Eidikotita != simbasi.ΕΙΔΙΚΟΤΗΤΑ) Eidikotita = simbasi.ΕΙΔΙΚΟΤΗΤΑ;

            //if (!String.IsNullOrEmpty(simbasi.ΑΔΑ) && ADA != simbasi.ΑΔΑ) ADA = simbasi.ΑΔΑ;

        }

        public static void SetEntityFields(ΕΚΠ_ΣΥΜΒΑΣΗ simbasi)
        {
            //simbasi.ΑΦΜ = Afm;
            simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ = SchoolYear;
            simbasi.ΠΟΛΗ = City;
            simbasi.ΤΟΥ_ΔΙΟΙΚΗΤΗ = Dioikitis;
            simbasi.ΑΡ_ΠΡΟΚΗΡΥΞΗ = Prokirixi;
            simbasi.ΑΡ_ΑΠΟΦΑΣΗ = Apofasi;
            simbasi.ΑΣΦΑΛΙΣΤΙΚΟΣ_ΦΟΡΕΑΣ = Asfalisi;
            simbasi.ΔΙΟΙΚΗΤΗΣ = Commander;
            //simbasi.ΠΡΩΤΟΚΟΛΛΟ = Protocol;
            simbasi.ΗΜΕΡΟΜΗΝΙΑ = SimbasiDate;
            //simbasi.ΤΟΥ_ΕΚΠΑΙΔΕΥΤΙΚΟΥ = TeacherName;
            //simbasi.ΠΡΟΣΛΗΨΗ_ΩΣ = HireType;
            //simbasi.ΕΙΔΙΚΟΤΗΤΑ = Eidikotita;
            //simbasi.ΑΔΑ = ADA;
        }

        public static void EditEntityFields(PoseidonDataContext dbui, ΕΚΠ_ΣΥΜΒΑΣΗ simbasi)
        {
            ΕΚΠ_ΣΥΜΒΑΣΗ toEdit = dbui.ΕΚΠ_ΣΥΜΒΑΣΗs.Single(s => s.ΣΥΜΒΑΣΗ_ΚΩΔ == simbasi.ΣΥΜΒΑΣΗ_ΚΩΔ);
            SetModelFields(simbasi);
            SetEntityFields(simbasi);

            toEdit.ΑΦΜ = simbasi.ΑΦΜ;
            toEdit.ΣΧΟΛΙΚΟ_ΕΤΟΣ = simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            toEdit.ΠΟΛΗ = simbasi.ΠΟΛΗ;
            toEdit.ΤΟΥ_ΔΙΟΙΚΗΤΗ = simbasi.ΤΟΥ_ΔΙΟΙΚΗΤΗ;
            toEdit.ΑΡ_ΠΡΟΚΗΡΥΞΗ = simbasi.ΑΡ_ΠΡΟΚΗΡΥΞΗ;
            toEdit.ΑΡ_ΑΠΟΦΑΣΗ = simbasi.ΑΡ_ΑΠΟΦΑΣΗ;
            toEdit.ΑΣΦΑΛΙΣΤΙΚΟΣ_ΦΟΡΕΑΣ = simbasi.ΑΣΦΑΛΙΣΤΙΚΟΣ_ΦΟΡΕΑΣ;
            toEdit.ΔΙΟΙΚΗΤΗΣ = simbasi.ΔΙΟΙΚΗΤΗΣ;
            toEdit.ΠΡΩΤΟΚΟΛΛΟ = simbasi.ΠΡΩΤΟΚΟΛΛΟ;
            toEdit.ΗΜΕΡΟΜΗΝΙΑ = simbasi.ΗΜΕΡΟΜΗΝΙΑ;
            toEdit.ΤΟΥ_ΕΚΠΑΙΔΕΥΤΙΚΟΥ = simbasi.ΤΟΥ_ΕΚΠΑΙΔΕΥΤΙΚΟΥ;
            toEdit.ΠΡΟΣΛΗΨΗ_ΩΣ = simbasi.ΠΡΟΣΛΗΨΗ_ΩΣ;
            toEdit.ΕΙΔΙΚΟΤΗΤΑ = simbasi.ΕΙΔΙΚΟΤΗΤΑ;
            toEdit.ΑΔΑ = simbasi.ΑΔΑ;



        }

        public static void AddEntityFields(PoseidonDataContext dbui, ΕΚΠ_ΣΥΜΒΑΣΗ simbasi)
        {
            SetModelFields(simbasi);
            SetEntityFields(simbasi);
            dbui.ΕΚΠ_ΣΥΜΒΑΣΗs.InsertOnSubmit(simbasi);
        }

        #endregion

    } // class AnathesiPersistentDataEpas

}
