using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;

namespace Poseidon.AppPages.Teachers.Anatheseis
{
    /// <summary>
    /// Interaction logic for AnathesiForm.xaml
    /// </summary>
    public partial class AnathesiForm : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        public bool new_record;

        public AnathesiForm()
        {
            InitializeComponent();
            LoadData();
            new_record = AnathesiPersistentData.NewRecord;
        }

        private void LoadData()
        {
            var kladoi = from k in db.ΕΚΠ_ΚΛΑΔΟΙs
                         select k;
            cboKlados.ItemsSource = kladoi.ToList();

            var eidikotites = from e in db.ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣs
                              orderby e.ΒΑΘΜΙΔΑ
                              select e;
            cboEidikotis.ItemsSource = eidikotites.ToList();

            var posts = from p in db.ΕΚΠ_ΚΑΤΗΓΟΡΙΕΣs
                        select p;
            cboPost.ItemsSource = posts.ToList();

            var schools = from s in db.ΣΧΟΛΕΣs
                          select s;
            cboSchool.ItemsSource = schools.ToList();

            var schoolYears = from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                              orderby s.ΣΧΟΛ_ΕΤΟΣ descending
                              select s;
            cboSchoolYear.ItemsSource = schoolYears.ToList();

            var terms = from t in db.ΕΞΑΜΗΝΑs
                        select t;
            cboTerm.ItemsSource = terms.ToList();

            var lesson_types = from lt in db.ΜΑΘΗΜΑΤΑ_ΕΙΔΗs
                               select lt;
            cboLessonType.ItemsSource = lesson_types.ToList();

            // the loading depends on the school selection (different data sources)
            cboLesson.ItemsSource = null;

            var lesson_ta = from ta in db.ΜΑΘΗΜΑΤΑ_ΘΕs
                            select ta;
            cboLessonTA.ItemsSource = lesson_ta.ToList();

            var anatheseis = from a in db.ΑΝΑΘΕΣΕΙΣs
                             orderby a.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                             select a;

        }

        private void LoadLessons(int school)
        {
            if (school == 1)
            {
                var lessons = (from l in db.ΜΑΘΗΜΑΤΑ_ΣΠs
                              select l).ToList();
                cboLesson.ItemsSource = lessons;
            }
            else if (school == 2)
            {
                var lessons = (from l in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                               select l).ToList();
                cboLesson.ItemsSource = lessons;
            }
            else cboLesson.ItemsSource = null;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //UserFunctions.ShowAdminMessage("User control loaded: NewRecord=" + new_record);
            if (new_record == true)
            {
                // set Teacher PersistentData
                string afm = SelectedTeacher.TeacherAFM;
                int school_year = SelectedSchoolYear.SchoolYear;

                if (AnathesiPersistentData.AnatheseisExist(afm, school_year) == true) GetModelFields();
                txtAFM.Text = SelectedTeacher.TeacherAFM;
                txtLastName.Text = SelectedTeacher.TeacherLastName;
                txtFirstName.Text = SelectedTeacher.TeacherFirstName;
                cboSchoolYear.SelectedValue = SelectedSchoolYear.SchoolYear;
                cboSchool.SelectedValue = SelectedSchool.School;
                LoadLessons(SelectedSchool.School);
                DisplaySchoolYear(school_year);
            }
        }

        private void GetModelFields()
        {
            //cboTerm.SelectedValue = AnathesiPersistentDataEpas.Term;
            txtAFM.Text = AnathesiPersistentData.Afm;
            cboKlados.SelectedValue = AnathesiPersistentData.Klados;
            cboEidikotis.SelectedValue = AnathesiPersistentData.Eidikotita;
            cboPost.SelectedValue = AnathesiPersistentData.Post;
            cboSchoolYear.SelectedValue = AnathesiPersistentData.SchoolYear;
            cboLesson.SelectedValue = AnathesiPersistentData.Lesson;

        }

        public void DisplaySchoolYear(int sy_id)
        {
            var syear = (from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == sy_id
                         select new { s.ΣΧΟΛ_ΕΤΟΣ }).FirstOrDefault();

            txtSchoolYear.Text = syear.ΣΧΟΛ_ΕΤΟΣ;
        }


        #region Combo Events


        private void cboKlados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // we need this, otherwise the dropdown is fired even without user selection
            if (!cboKlados.IsLoaded) return;
            if (cboKlados.SelectedValue != null)
            {
                //cboEidikotita.ItemsSource = null;
                int selected_klados = (int)cboKlados.SelectedValue;

                var eidikotites = from t in db.ΕΚΠ_ΕΙΔΙΚΟΤΗΤΕΣs
                                  where t.ΒΑΘΜΙΔΑ == selected_klados
                                  orderby t.ΒΑΘΜΙΔΑ, t.ΕΙΔΙΚΟΤΗΤΑ
                                  select t;
                cboEidikotis.IsDropDownOpen = true;
                cboEidikotis.ItemsSource = eidikotites.ToList();
            }
        }

        private void cboSchool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // we need this, otherwise the dropdown is fired even without user selection
            if (!cboSchool.IsLoaded) return;
            if (cboSchool.SelectedValue != null)
            {
                if ((int)cboSchool.SelectedValue == 1)
                {
                    var lessons = from l in db.ΜΑΘΗΜΑΤΑ_ΣΠs
                                  orderby l.ΕΞΑΜΗΝΟ, l.ΜΑΘΗΜΑ
                                  select l;
                    cboLesson.ItemsSource = lessons.ToList();
                }
                else if ((int)cboSchool.SelectedValue == 2)
                {
                    var lessons = from l in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                                  orderby l.ΕΞΑΜΗΝΟ, l.ΜΑΘΗΜΑ
                                  select l;
                    cboLesson.ItemsSource = lessons.ToList();
                }
                else
                {
                    UserFunctions.RadWindowAlert("Άκυρη επιλογή Σχολής: " + (int)cboSchool.SelectedValue);
                }
            }

        }

        private void cboTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboTerm.IsLoaded) return;
            if (cboSchool.SelectedValue != null && cboTerm.SelectedValue != null)
            {
                if ((int)cboSchool.SelectedValue == 1)
                {
                    var lessons = from l in db.ΜΑΘΗΜΑΤΑ_ΣΠs
                                  where l.ΕΞΑΜΗΝΟ == (int)cboTerm.SelectedValue
                                  orderby l.ΕΞΑΜΗΝΟ, l.ΜΑΘΗΜΑ
                                  select l;
                    cboLesson.ItemsSource = lessons.ToList();
                }
                else if ((int)cboSchool.SelectedValue == 2)
                {
                    var lessons = from l in db.ΜΑΘΗΜΑΤΑ_ΣΜs
                                  where l.ΕΞΑΜΗΝΟ == (int)cboTerm.SelectedValue
                                  orderby l.ΕΞΑΜΗΝΟ, l.ΜΑΘΗΜΑ
                                  select l;
                    cboLesson.ItemsSource = lessons.ToList();
                }
                else
                {
                    UserFunctions.RadWindowAlert("Άκυρη επιλογή Σχολής, Εξαμήνου: " + 
                        (int)cboSchool.SelectedValue + ", " + (int)cboTerm.SelectedValue);
                }
            }
        }


        #endregion

    }   // class AnathesiForm
}
