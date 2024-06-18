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

namespace Poseidon.AppPages.Teachers.Anatheseis.SP
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
            new_record = AnathesiPersistentDataSP.NewRecord;
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

            var lessons = (from l in db.ΜΑΘΗΜΑΤΑ_ΣΠs
                           select l).ToList();
            cboLesson.ItemsSource = lessons;

            var lesson_ta = from ta in db.ΜΑΘΗΜΑΤΑ_ΘΕs
                            select ta;
            cboLessonTA.ItemsSource = lesson_ta.ToList();

            var anatheseis = from a in db.ΑΝΑΘΕΣΕΙΣ_ΣΜs
                             orderby a.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                             select a;

            var syear = (from sy in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         where sy.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == SelectedSchoolYear.SchoolYear
                         select new { sy.ΣΧΟΛ_ΕΤΟΣ }).FirstOrDefault();

            // load Teacher data in textboxes
            txtAFM.Text = SelectedTeacher.TeacherAFM;
            txtLastName.Text = SelectedTeacher.TeacherLastName;
            txtFirstName.Text = SelectedTeacher.TeacherFirstName;

            // load school year in textbox
            txtSchoolYear.Text = syear.ΣΧΟΛ_ΕΤΟΣ;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //UserFunctions.ShowAdminMessage("User control loaded: NewRecord=" + new_record);
            if (new_record == true)
            {
                // set Teacher PersistentData
                string afm = SelectedTeacher.TeacherAFM;
                int school_year = SelectedSchoolYear.SchoolYear;

                if (AnathesiPersistentDataSP.AnatheseisExistSP(afm, school_year) == true) GetModelFields();
                txtAFM.Text = SelectedTeacher.TeacherAFM;
                txtLastName.Text = SelectedTeacher.TeacherLastName;
                txtFirstName.Text = SelectedTeacher.TeacherFirstName;
                cboSchoolYear.SelectedValue = SelectedSchoolYear.SchoolYear;
                DisplaySchoolYear(school_year);
            }
        }

        private void GetModelFields()
        {
            //cboTerm.SelectedValue = AnathesiPersistentDataEpas.Term;
            txtAFM.Text = AnathesiPersistentDataSP.Afm;
            cboKlados.SelectedValue = AnathesiPersistentDataSP.Klados;
            cboEidikotis.SelectedValue = AnathesiPersistentDataSP.Eidikotita;
            cboPost.SelectedValue = AnathesiPersistentDataSP.Post;
            cboSchoolYear.SelectedValue = AnathesiPersistentDataSP.SchoolYear;
            //cboLesson.SelectedValue = AnathesiPersistentDataSP.Lesson;
            Msc.IsChecked = AnathesiPersistentDataSP.Msc;
            Phd.IsChecked = AnathesiPersistentDataSP.Phd;

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


        private void cboTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboTerm.IsLoaded) return;
            if (cboTerm.SelectedValue != null)
            {
                var lessons = from l in db.ΜΑΘΗΜΑΤΑ_ΣΠs
                              where l.ΕΞΑΜΗΝΟ == (int)cboTerm.SelectedValue
                              orderby l.ΕΞΑΜΗΝΟ, l.ΜΑΘΗΜΑ
                              select l;
                cboLesson.ItemsSource = lessons.ToList();
            }
        }

        #endregion

    }   // class AnathesiForm
}
