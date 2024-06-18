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

namespace Poseidon.AppPages.Teachers
{
    /// <summary>
    /// Interaction logic for TeacherFormRO.xaml
    /// </summary>
    public partial class TeacherFormRO : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public TeacherFormRO()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            // data source for the combo
            var sex = from s in db.ΦΥΛΑs
                      orderby s.ΦΥΛΟ_ΚΩΔ
                      select s;

            cbosex.ItemsSource = sex.ToList();
            changeSexPhoto();

            var work = from w in db.ΕΚΠ_ΕΡΓΑΣΙΑs
                       select w;
            cbowork.ItemsSource = work.ToList();

        }

        private void cbosex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeSexPhoto();
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

    }
}
