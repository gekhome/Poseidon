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


namespace Poseidon.AppPages.Teachers.Simbaseis
{
    /// <summary>
    /// Interaction logic for SimbasiFormRO.xaml
    /// </summary>
    public partial class SimbasiFormRO : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        SimbasiModel sm = new SimbasiModel();

        public SimbasiFormRO()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var admin = from a in db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs
                        orderby a.ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select a;
            cboAdmin.ItemsSource = admin.ToList();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string safm = SimbasiTeacher.AFM;
            int school_year = SimbasiSchoolYear.SYear;

            if (school_year > 0) DisplaySchoolYear(school_year);
            else txtSchoolYear.Text = "";

            LoadData();
        }

        public void DisplaySchoolYear(int sy_id)
        {
            var syear = (from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         where s.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == sy_id
                         select new { s.ΣΧΟΛ_ΕΤΟΣ }).FirstOrDefault();

            txtSchoolYear.Text = syear.ΣΧΟΛ_ΕΤΟΣ;
        }

        private void cboAdmin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }   // class SimbasiFormRO
}
