using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poseidon.Model;
using Poseidon.Utilities;

namespace Poseidon.DataModel
{
    class Kerberos
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();
        //private int MAX_WEEKS = 15;

        #region ΣΜ ΑΝΑΘΕΣΕΙΣ

        public bool EmptyFieldsErrorSM(ΑΝΑΘΕΣΕΙΣ_ΣΜ anathesi)
        {
            bool error = false;

            // obligatory fields validations
            
            if (anathesi.ΚΛΑΔΟΣ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή του κλάδου.");
                error = true;
                return error;
            }

            if (anathesi.ΕΙΔΙΚΟΤΗΤΑ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή ειδικότητας εκπαιδευτικού.");
                error = true;
                return error;
            }

            if (anathesi.ΚΑΤΗΓΟΡΙΑ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή Θέσης.");
                error = true;
                return error;
            }

            //if (anathesi.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ == null)
            //{
            //    UserFunctions.ShowAdminMessage("Πρέπει να εισαχθεί ημ/νια Ανάθεσης.");
            //    error = true;
            //    return error;
            //}
            
            if (anathesi.ΕΞΑΜΗΝΟ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή του εξαμήνου.");
                error = true;
                return error;
            }

            if (anathesi.ΜΑΘΗΜΑ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή Μαθήματος.");
                error = true;
                return error;
            }

            if (anathesi.ΜΑΘΗΜΑ_ΕΙΔΟΣ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή είδους μαθήματος.");
                error = true;
                return error;
            }

            if (anathesi.ΘΕ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή Θεωρίας/Εφαρμογών.");
                error = true;
                return error;
            }

            if (anathesi.ΩΡΕΣ_ΕΒΔ == null || (anathesi.ΩΡΕΣ_ΕΒΔ <= 0 || anathesi.ΩΡΕΣ_ΕΒΔ > 33))
            {
                UserFunctions.ShowAdminMessage("Μη έγκυρος αριθμός ωρών/εβδ.");
                error = true;
                return error;
            }
            return error;
        }

        public bool HireDateInSchoolYearSM(ΑΝΑΘΕΣΕΙΣ_ΣΜ anathesi)
        {
            bool error = true;
            int school_year = (int)anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            var schoolYears = (from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                               where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == school_year
                               select new { s.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ, s.ΣΧ_ΕΤΟΣ_ΛΗΞΗ }).FirstOrDefault();

            if (anathesi.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ == null) return error;

            DateTime anathesiDate = (DateTime)anathesi.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ;

            if (anathesiDate < schoolYears.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ || anathesiDate > schoolYears.ΣΧ_ΕΤΟΣ_ΛΗΞΗ)
            {
                    UserFunctions.ShowAdminMessage("Η ημερομηνία ανάθεσης είναι εκτός του διδακτικού έτους.");
                    error = false;
                    return error;
            }
            
            return error;
        }

        public bool ValidLessonTypeSM(ΑΝΑΘΕΣΕΙΣ_ΣΜ anathesi)
        {
            bool isvalid = true;
            int lessonID = (int)anathesi.ΜΑΘΗΜΑ;
            int lesson_type = (int)anathesi.ΘΕ;

            var lesson = (from l in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                          where l.ΚΩΔΙΚΟΣ == lessonID
                          select l).FirstOrDefault();

                // Ο χρήστης επέλεξε Θεωρία αλλά δεν υπάρχει Θεωρία (ώρες = 0)
                bool rule1 = lesson_type == 1 && lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ == 0;
                // Ο χρήστης επέλεξε Εργαστήριο αλλα δεν υπάρχει Εργαστήριο (ώρες = 0)
                bool rule2 = lesson_type == 2 && lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ == 0;

                if (rule1 == true)
                {
                    isvalid = false;
                    const string error_msg = "Το μάθημα αυτό δεν έχει Θεωρία.";
                    UserFunctions.ShowAdminMessage(error_msg);
                }
                else if (rule2 == true)
                {
                    isvalid = false;
                    const string error_msg = "Το μάθημα αυτό δεν έχει Εφαρμογές.";
                    UserFunctions.ShowAdminMessage(error_msg);
                }

            return isvalid;
        }   // ValidLessonType

        public bool ValidLessonHoursSM(ΑΝΑΘΕΣΕΙΣ_ΣΜ anathesi)
        {
            bool isvalid = true;
            int lessonID = (int)anathesi.ΜΑΘΗΜΑ;
            int lesson_type = (int)anathesi.ΜΑΘΗΜΑ_ΕΙΔΟΣ;

                var lesson = (from l in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                              where l.ΚΩΔΙΚΟΣ == lessonID
                              select l).FirstOrDefault();

                int hours_theory = (int)lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ;
                int hours_lab = (int)lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ;
                int hours_total = (int)lesson.ΣΥΝΟΛΟ_ΩΡΕΣ;
                int hours_week = (int)lesson.ΩΡΕΣ_ΕΒΔ;

                // Ο χρήστης επέλεξε είδος μαθήματος ΘΕΩΡΙΑ και οι ώρες/εβδ
                //// που καταχώρησε υπερβαίνουν αυτές του προγράμματος σπουδών.
                //bool rule1 = lesson_type == 1 && anathesi.ΩΡΕΣ_ΕΒΔ * MAX_WEEKS > hours_total;
                //// Ο χρήστης επέλεξε είδος μαθήματος ΕΡΓΑΣΤΗΡΙΟ και οι ώρες/εβδ
                //// που καταχώρησε υπερβαίνουν αυτές του προγράμματος σπουδών.
                //bool rule2 = lesson_type == 2 && anathesi.ΩΡΕΣ_ΕΒΔ * MAX_WEEKS > hours_total;

                bool rule1 = anathesi.ΩΡΕΣ_ΕΒΔ > hours_week;
                bool rule2 = anathesi.ΩΡΕΣ_ΕΒΔ <= 0;

                if (rule1 == true)
                {
                    isvalid = false;
                    string error_msg = "Οι ώρες/εβδομάδα ανάθεσης υπερβαίνουν αυτές του\n";
                    error_msg += String.Format("προγράμματος σπουδων για το μάθημα : {0}", hours_week);
                    UserFunctions.ShowAdminMessage(error_msg);
                }
                else if (rule2 == true)
                {
                    isvalid = false;
                    string error_msg = "Μη έγκυρος αριθμός ωρών/εβδομαδα ανάθεσης. \n";
                    error_msg += "Πρέπει να είναι μεγαλύτερος του μηδενός.";
                    UserFunctions.ShowAdminMessage(error_msg);
                }

            return isvalid;
        }   // ValidLessonHours

        public bool NullProsontaSM(ΑΝΑΘΕΣΕΙΣ_ΣΜ anathesi)
        {
            bool error = false;

            if (anathesi.ΜΕΤΑΠΤΥΧΙΑΚΟ == null)
            {
                UserFunctions.ShowAdminMessage("Η τιμή μεταπτυχιακού είναι κενή.");
                error = true;
                return error;
            }
            if (anathesi.ΔΙΔΑΚΤΟΡΙΚΟ == null)
            {
                UserFunctions.ShowAdminMessage("Η τιμή διδακτορικού είναι κενή.");
                error = true;
                return error;
            }

            return error;
        }

        #endregion


        #region ΣΠ ΑΒΑΘΕΣΕΙΣ

        public bool EmptyFieldsErrorSP(ΑΝΑΘΕΣΕΙΣ_ΣΠ anathesi)
        {
            bool error = false;

            // obligatory fields validations

            if (anathesi.ΚΛΑΔΟΣ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή του κλάδου.");
                error = true;
                return error;
            }

            if (anathesi.ΕΙΔΙΚΟΤΗΤΑ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή ειδικότητας εκπαιδευτικού.");
                error = true;
                return error;
            }

            if (anathesi.ΚΑΤΗΓΟΡΙΑ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή Θέσης.");
                error = true;
                return error;
            }

            //if (anathesi.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ == null)
            //{
            //    UserFunctions.ShowAdminMessage("Πρέπει να εισαχθεί ημ/νια Ανάθεσης.");
            //    error = true;
            //    return error;
            //}

            if (anathesi.ΕΞΑΜΗΝΟ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή του εξαμήνου.");
                error = true;
                return error;
            }

            if (anathesi.ΜΑΘΗΜΑ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή Μαθήματος.");
                error = true;
                return error;
            }

            if (anathesi.ΜΑΘΗΜΑ_ΕΙΔΟΣ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή είδους μαθήματος.");
                error = true;
                return error;
            }

            if (anathesi.ΘΕ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή Θεωρίας/Εφαρμογών.");
                error = true;
                return error;
            }

            if (anathesi.ΩΡΕΣ_ΕΒΔ == null || (anathesi.ΩΡΕΣ_ΕΒΔ <= 0 || anathesi.ΩΡΕΣ_ΕΒΔ > 33))
            {
                UserFunctions.ShowAdminMessage("Μη έγκυρος αριθμός ωρών/εβδ.");
                error = true;
                return error;
            }
            return error;
        }

        public bool HireDateInSchoolYearSP(ΑΝΑΘΕΣΕΙΣ_ΣΠ anathesi)
        {
            bool error = true;
            int school_year = (int)anathesi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            var schoolYears = (from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                               where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == school_year
                               select new { s.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ, s.ΣΧ_ΕΤΟΣ_ΛΗΞΗ }).FirstOrDefault();

            if (anathesi.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ == null) return error;

            DateTime anathesiDate = (DateTime)anathesi.ΗΜΝΙΑ_ΠΡΟΣΛΗΨΗ;

            if (anathesiDate < schoolYears.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ || anathesiDate > schoolYears.ΣΧ_ΕΤΟΣ_ΛΗΞΗ)
            {
                UserFunctions.ShowAdminMessage("Η ημερομηνία ανάθεσης είναι εκτός του διδακτικού έτους.");
                error = false;
                return error;
            }

            return error;
        }

        public bool ValidLessonTypeSP(ΑΝΑΘΕΣΕΙΣ_ΣΠ anathesi)
        {
            bool isvalid = true;
            int lessonID = (int)anathesi.ΜΑΘΗΜΑ;
            int lesson_type = (int)anathesi.ΘΕ;

            var lesson = (from l in db.ΜΑΘΗΜΑΤΑ_ΣΠs
                          where l.ΚΩΔΙΚΟΣ == lessonID
                          select l).FirstOrDefault();

            // Ο χρήστης επέλεξε Θεωρία αλλά δεν υπάρχει Θεωρία (ώρες = 0)
            bool rule1 = lesson_type == 1 && lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ == 0;
            // Ο χρήστης επέλεξε Εργαστήριο αλλα δεν υπάρχει Εργαστήριο (ώρες = 0)
            bool rule2 = lesson_type == 2 && lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ == 0;

            if (rule1 == true)
            {
                isvalid = false;
                const string error_msg = "Το μάθημα αυτό δεν έχει Θεωρία.";
                UserFunctions.ShowAdminMessage(error_msg);
            }
            else if (rule2 == true)
            {
                isvalid = false;
                const string error_msg = "Το μάθημα αυτό δεν έχει Εφαρμογές.";
                UserFunctions.ShowAdminMessage(error_msg);
            }

            return isvalid;
        }   // ValidLessonType

        public bool ValidLessonHoursSP(ΑΝΑΘΕΣΕΙΣ_ΣΠ anathesi)
        {
            bool isvalid = true;
            int lessonID = (int)anathesi.ΜΑΘΗΜΑ;
            int lesson_type = (int)anathesi.ΜΑΘΗΜΑ_ΕΙΔΟΣ;

            var lesson = (from l in db.ΜΑΘΗΜΑΤΑ_ΣΠs
                          where l.ΚΩΔΙΚΟΣ == lessonID
                          select l).FirstOrDefault();

            int hours_theory = (int)lesson.Σ_ΩΡΕΣ_ΘΕΩΡΙΑ;
            int hours_lab = (int)lesson.Σ_ΩΡΕΣ_ΕΦΑΡΜΟΓΕΣ;
            int hours_total = (int)lesson.ΣΥΝΟΛΟ_ΩΡΕΣ;
            int hours_week = (int)lesson.ΩΡΕΣ_ΕΒΔ;

            // Ο χρήστης επέλεξε είδος μαθήματος ΘΕΩΡΙΑ και οι ώρες/εβδ
            //// που καταχώρησε υπερβαίνουν αυτές του προγράμματος σπουδών.
            //bool rule1 = lesson_type == 1 && anathesi.ΩΡΕΣ_ΕΒΔ * MAX_WEEKS > hours_total;
            //// Ο χρήστης επέλεξε είδος μαθήματος ΕΡΓΑΣΤΗΡΙΟ και οι ώρες/εβδ
            //// που καταχώρησε υπερβαίνουν αυτές του προγράμματος σπουδών.
            //bool rule2 = lesson_type == 2 && anathesi.ΩΡΕΣ_ΕΒΔ * MAX_WEEKS > hours_total;

            bool rule1 = anathesi.ΩΡΕΣ_ΕΒΔ > hours_week;
            bool rule2 = anathesi.ΩΡΕΣ_ΕΒΔ <= 0;

            if (rule1 == true)
            {
                isvalid = false;
                string error_msg = "Οι ώρες/εβδομάδα ανάθεσης υπερβαίνουν αυτές του\n";
                error_msg += "προγράμματος σπουδων για το μάθημα : (" + hours_week + " ώρες/εβδ)";
                UserFunctions.ShowAdminMessage(error_msg);
            }
            else if (rule2 == true)
            {
                isvalid = false;
                string error_msg = "Μη έγκυρος αριθμός ωρών/εβδομαδα ανάθεσης. \n";
                error_msg += "Πρέπει να είναι μεγαλύτερος του μηδενός.";
                UserFunctions.ShowAdminMessage(error_msg);
            }

            return isvalid;
        }   // ValidLessonHours

        #endregion


        #region ΣΜ ΩΡΟΛΟΓΙΟ ΠΡΟΓΡΑΜΜΑ

        public bool IsNullRowSM(ΠΡΟΓΡΑΜΜΑ_ΣΜ row)
        {
            if (row == null)
            {
                UserFunctions.ShowAdminMessage("Δεν υπάρχουν δεδομένα για καταχώρηση.");
                return true;
            }
            else return false;
        }

        public bool EmptyFieldsExistSM(ΠΡΟΓΡΑΜΜΑ_ΣΜ programma)
        {
            bool error = false;

            if (programma.ΗΜΕΡΟΜΗΝΙΑ == null)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή ημερομηνίας. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (programma.ΩΡΑ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή ώρας. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (programma.ΤΜΗΜΑ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή τμήματος. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (programma.ΜΑΘΗΜΑ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή μαθήματος. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (programma.ΔΙΔ_ΕΡΓΟ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή διδ.έργου. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (String.IsNullOrEmpty(programma.ΕΚΠΑΙΔΕΥΤΗΣ1) && String.IsNullOrEmpty(programma.ΕΚΠΑΙΔΕΥΤΗΣ2) && String.IsNullOrEmpty(programma.ΕΚΠΑΙΔΕΥΤΗΣ3))
            {
                error = true;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί τουλάχιστον ένας εκπ/κός. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            return error;
        }

        public bool ValidatePK_SM(ΠΡΟΓΡΑΜΜΑ_ΣΜ programme)
        {
            int tmima = (int)programme.ΤΜΗΜΑ;
            DateTime date = (DateTime)programme.ΗΜΕΡΟΜΗΝΙΑ;
            int hour = (int)programme.ΩΡΑ;

            int rec_count = (from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΜs
                             where p.ΤΜΗΜΑ == tmima && p.ΗΜΕΡΟΜΗΝΙΑ == date && p.ΩΡΑ == hour
                             select p).Count();

            if (rec_count > 0)
            {
                UserFunctions.ShowAdminMessage("Η καταχώρηση δεν μπορεί να γίνει διότι προκύπτει διπλότυπη εγγραφή.");
                return false;
            }
            else return true;
        }

        #endregion

        #region ΣΠ ΩΡΟΛΟΓΙΟ ΠΡΟΓΡΑΜΜΑ

        public bool IsNullRowSP(ΠΡΟΓΡΑΜΜΑ_ΣΠ row)
        {
            if (row == null)
            {
                UserFunctions.ShowAdminMessage("Δεν υπάρχουν δεδομένα για καταχώρηση.");
                return true;
            }
            else return false;
        }

        public bool EmptyFieldsExistSP(ΠΡΟΓΡΑΜΜΑ_ΣΠ programma)
        {
            bool error = false;

            if (programma.ΗΜΕΡΟΜΗΝΙΑ == null)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή ημερομηνίας. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (programma.ΩΡΑ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή ώρας. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (programma.ΤΜΗΜΑ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή τμήματος. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (programma.ΜΑΘΗΜΑ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή μαθήματος. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (programma.ΔΙΔ_ΕΡΓΟ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή διδ.έργου. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (String.IsNullOrEmpty(programma.ΕΚΠΑΙΔΕΥΤΗΣ1) && String.IsNullOrEmpty(programma.ΕΚΠΑΙΔΕΥΤΗΣ2) && String.IsNullOrEmpty(programma.ΕΚΠΑΙΔΕΥΤΗΣ3))
            {
                error = true;
                UserFunctions.ShowAdminMessage("Πρέπει να επιλεγεί τουλάχιστον ένας εκπαιδευτικός. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            return error;
        }

        public bool ValidatePK_SP(ΠΡΟΓΡΑΜΜΑ_ΣΠ programme)
        {
            int tmima = (int)programme.ΤΜΗΜΑ;
            DateTime date = (DateTime)programme.ΗΜΕΡΟΜΗΝΙΑ;
            int hour = (int)programme.ΩΡΑ;

            if (tmima == 0 || date == null || hour == 0)
            {
                string msg = "Δεν έχουν επιλεγεί τμήμα ή ημερομηνία ή ώρα.";
                UserFunctions.ShowAdminMessage(msg);
                return false;
            }

            int rec_count = (from p in db.ΠΡΟΓΡΑΜΜΑ_ΣΠs
                             where p.ΤΜΗΜΑ == tmima && p.ΗΜΕΡΟΜΗΝΙΑ == date && p.ΩΡΑ == hour
                             select p).Count();

            if (rec_count > 0)
            {
                UserFunctions.ShowAdminMessage("Η καταχώρηση δεν μπορεί να γίνει διότι προκύπτει διπλότυπη εγγραφή.");
                return false;
            }
            else return true;
        }

        #endregion

        #region ΣΥΜΒΑΣΕΙΣ

        public bool EmptyFieldsError(ΕΚΠ_ΣΥΜΒΑΣΗ simbasi)
        {
            bool error = false;

            // obligatory fields validations

            if (simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ == 0)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να γίνει επιλογή του διδακτικού έτους.");
                error = true;
                return error;
            }

            if (simbasi.ΗΜΕΡΟΜΗΝΙΑ == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να καταχωρηθεί η ημερομηνία.");
                error = true;
                return error;
            }

            if (String.IsNullOrEmpty(simbasi.ΠΡΩΤΟΚΟΛΛΟ))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί ο αρ. πρωτοκόλλου.");
                error = true;
                return error;
            }

            if (String.IsNullOrEmpty(simbasi.ΠΟΛΗ))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί η πόλη.");
                error = true;
                return error;
            }

            if (String.IsNullOrEmpty(simbasi.ΤΟΥ_ΔΙΟΙΚΗΤΗ))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί o διοικητής.");
                error = true;
                return error;
            }

            if (String.IsNullOrEmpty(simbasi.ΤΟΥ_ΕΚΠΑΙΔΕΥΤΙΚΟΥ))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί o εκπαιδευτικός.");
                error = true;
                return error;
            }

            if (String.IsNullOrEmpty(simbasi.ΑΡ_ΠΡΟΚΗΡΥΞΗ))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί o αρ. προκήρυξης.");
                error = true;
                return error;
            }

            if (String.IsNullOrEmpty(simbasi.ΑΡ_ΑΠΟΦΑΣΗ))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί o αρ. απόφασης.");
                error = true;
                return error;
            }

            if (String.IsNullOrEmpty(simbasi.ΠΡΟΣΛΗΨΗ_ΩΣ))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί τo πεδίο πρόσληψη ως.");
                error = true;
                return error;
            }

            if (String.IsNullOrEmpty(simbasi.ΕΙΔΙΚΟΤΗΤΑ))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί τo πεδίο ειδικότητα.");
                error = true;
                return error;
            }

            if (String.IsNullOrEmpty(simbasi.ΑΣΦΑΛΙΣΤΙΚΟΣ_ΦΟΡΕΑΣ))
            {
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί ο ασφαλιστικός φορέας.");
                error = true;
                return error;
            }


            return error;
        }

        public bool SimbasiDateInSchoolYear(ΕΚΠ_ΣΥΜΒΑΣΗ simbasi)
        {
            bool error = true;
            int school_year;
            if (simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ == null) school_year = SimbasiSchoolYear.SYear;
            else school_year = (int)simbasi.ΣΧΟΛΙΚΟ_ΕΤΟΣ;

            var schoolYears = (from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                               where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == school_year
                               select new { s.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ, s.ΣΧ_ΕΤΟΣ_ΛΗΞΗ }).FirstOrDefault();

            if (simbasi.ΗΜΕΡΟΜΗΝΙΑ == null) return error;

            DateTime simbasiDate = (DateTime)simbasi.ΗΜΕΡΟΜΗΝΙΑ;

            if (simbasiDate < schoolYears.ΣΧ_ΕΤΟΣ_ΕΝΑΡΞΗ || simbasiDate > schoolYears.ΣΧ_ΕΤΟΣ_ΛΗΞΗ)
            {
                UserFunctions.ShowAdminMessage("Η ημερομηνία σύμβασης είναι εκτός του διδακτικού έτους.");
                error = false;
                return error;
            }

            return error;
        }

        #endregion

        #region ΑΠΟΥΣΙΕΣ

        public bool IsNullRowApousies(ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ row)
        {
            if (row == null)
            {
                UserFunctions.ShowAdminMessage("Δεν υπάρχουν δεδομένα για καταχώρηση.");
                return true;
            }
            else return false;
        }

        public bool EmptyFieldsExistApousies(ΣΠΟΥΔΑΣΤΗΣ_ΑΠΟΥΣΙΕΣ apousies)
        {
            bool error = false;

            if (apousies.ΗΜΕΡΟΜΗΝΙΑ == null)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή ημερομηνίας. Η καταχώρηση ακυρώθηκε.");
                return error;
            }
            if (apousies.ΤΜΗΜΑ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή τμήματος. Η καταχώρηση ακυρώθηκε.");
                return error;
            }

            if (String.IsNullOrWhiteSpace(apousies.ΑΜΚΑ) || String.IsNullOrEmpty(apousies.ΑΜΚΑ))
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή σπουδαστή. Η καταχώρηση ακυρώθηκε.");
                return error;
            }

            if (apousies.ΜΑΘΗΜΑ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή μαθήματος. Η καταχώρηση ακυρώθηκε.");
                return error;
            }

            if (apousies.ΑΠΟΥΣΙΕΣ_ΩΡΕΣ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή ωρών απουσίας. Η καταχώρηση ακυρώθηκε.");
                return error;
            }

            if (apousies.ΑΠΟΥΣΙΕΣ_ΕΙΔΟΣ == 0)
            {
                error = true;
                UserFunctions.ShowAdminMessage("Άκυρη τιμή είδους απουσίας. Η καταχώρηση ακυρώθηκε.");
                return error;
            }

            return error;

        }

        #endregion

    }   // class Kerberos
}
