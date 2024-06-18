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

namespace Poseidon.AppPages.Students
{
    /// <summary>
    /// Interaction logic for StudentDataFormRO.xaml
    /// </summary>
    public partial class StudentDataFormRO : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public StudentDataFormRO()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            // data source for the combos
            var sex = from s in db.ΦΥΛΑs
                      orderby s.ΦΥΛΟ_ΚΩΔ
                      select s;

            cbosex.ItemsSource = sex.ToList();
            changeSexPhoto();

            var sxoles = from s in db.ΣΧΟΛΕΣs
                         select s;
            cboSxoli.ItemsSource = sxoles.ToList();

            var eisodos = from ei in db.ΕΙΣΟΔΟΣ_ΕΙΔΗs
                          select ei;
            cboEisodos.ItemsSource = eisodos.ToList();

            var exodos = from ee in db.ΕΞΟΔΟΣ_ΕΙΔΗs
                         select ee;
            cboExodos.ItemsSource = exodos.ToList();

            var nationalities = from n in db.ΥΠΗΚΟΟΤΗΤΕΣs
                                select n;
            cboNationality.ItemsSource = nationalities.ToList();

        }

        private void changeSexPhoto()
        {
            if (cbosex.SelectedIndex == 0)
            {
                SexPhoto.Source = new BitmapImage(new Uri(@"pack://application:,,,/Poseidon;component/Shell/Images/Other/person_male.png"));
            }
            else if (cbosex.SelectedIndex == 1)
            {
                SexPhoto.Source = new BitmapImage(new Uri(@"pack://application:,,,/Poseidon;component/Shell/Images/Other/person_female.png"));
            }
            else
            {
                SexPhoto.Source = new BitmapImage(new Uri(@"pack://application:,,,/Poseidon;component/Shell/Images/Other/person_unknown.png"));
            }
        }

        private void cbosex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeSexPhoto();
        }

    }
}
