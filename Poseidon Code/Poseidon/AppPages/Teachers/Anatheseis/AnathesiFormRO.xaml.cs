using System;
using System.Collections.Generic;
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
    /// Interaction logic for AnathesiFormRO.xaml
    /// </summary>
    public partial class AnathesiFormRO : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        AnathesiModel am = new AnathesiModel();

        public AnathesiFormRO()
        {
            InitializeComponent();
            LoadData();
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

        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string safm = SelectedTeacher.TeacherAFM;
            int school_year = SelectedSchoolYear.SchoolYear;
            string firstname = SelectedTeacher.TeacherFirstName;
            string lastname = SelectedTeacher.TeacherLastName;

            txtAFM.Text = safm;
            txtFirstName.Text = firstname;
            txtLastName.Text = lastname;

            if (school_year > 0)
                DisplaySchoolYear(school_year);
            else txtSchoolYear.Text = "";

        }

        public void DisplayTeacherName(string safm)
        {

            var teacher = (from t in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           where t.ΑΦΜ == safm
                           select new { t.ΕΠΩΝΥΜΟ, t.ΟΝΟΜΑ }).FirstOrDefault();

            txtFirstName.Text = teacher.ΟΝΟΜΑ;
            txtLastName.Text = teacher.ΕΠΩΝΥΜΟ;

        } // LoadTeacherName

        public void DisplaySchoolYear(int sy_id)
        {
            var syear = (from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == sy_id
                         select new { s.ΣΧΟΛ_ΕΤΟΣ }).FirstOrDefault();

            txtSchoolYear.Text = syear.ΣΧΟΛ_ΕΤΟΣ;
        }

        private void cboKlados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cboTerm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
