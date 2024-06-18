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
using System.Collections.ObjectModel;
using Poseidon.Model;

namespace Poseidon.AppPages.Teachers
{
    /// <summary>
    /// Interaction logic for TeacherDetail.xaml
    /// </summary>
    public partial class TeacherDetail : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private ObservableCollection<ΦΥΛΑ> oc = new ObservableCollection<ΦΥΛΑ>();
        public TeacherDetail()
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
            var ocsex = new ObservableCollection<ΦΥΛΑ>(sex.ToList());
            cbosex.ItemsSource = ocsex;

            changeSexPhoto();

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

    }   // class TeacherDetail
}
