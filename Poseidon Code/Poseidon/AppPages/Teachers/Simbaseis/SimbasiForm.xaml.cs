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
using Telerik.Windows.Controls;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;

namespace Poseidon.AppPages.Teachers.Simbaseis
{
    /// <summary>
    /// Interaction logic for SimbasiForm.xaml
    /// </summary>
    public partial class SimbasiForm : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        public bool new_record;

        public SimbasiForm()
        {
            InitializeComponent();
            LoadData();
            new_record = SimbasiPersistentData.NewRecord;
        }

        private void LoadData()
        {
            var syear = (from sy in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                         where sy.ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ == SimbasiSchoolYear.SYear
                         select new { sy.ΣΧΟΛ_ΕΤΟΣ }).FirstOrDefault();

            var admin = from a in db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs
                        orderby a.ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select a;
            cboAdmin.ItemsSource = admin.ToList();

            txtAFM.Text = SimbasiTeacher.AFM;
            txtSchoolYear.Text = syear.ΣΧΟΛ_ΕΤΟΣ;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (new_record == true)
            {
                // set Teacher PersistentData
                string afm = SimbasiTeacher.AFM;
                int school_year = SimbasiSchoolYear.SYear;
                if (SimbasiPersistentData.SimbaseisExist(school_year) == true) GetModelFields();
            
                txtAFM.Text = SimbasiTeacher.AFM;
                DisplaySchoolYear(school_year);             
            }
        }

        private void GetModelFields()
        {
            txtAFM.Text = SimbasiPersistentData.Afm;
            txtDioikitis.Text = SimbasiPersistentData.Dioikitis;
            txtProkirixi.Text = SimbasiPersistentData.Prokirixi;
            txtApofasi.Text = SimbasiPersistentData.Apofasi;
            txtCity.Text = SimbasiPersistentData.City;
            txtAsfalisi.Text = SimbasiPersistentData.Asfalisi;

            txtADA.Text = SimbasiPersistentData.ADA;
            txtEidikotita.Text = SimbasiPersistentData.Eidikotita;
            txtHireType.Text = SimbasiPersistentData.HireType;
            txtTeacher.Text = SimbasiPersistentData.TeacherName;
            dpSimbasiDate.SelectedDate = SimbasiPersistentData.SimbasiDate;
            txtProtocol.Text = SimbasiPersistentData.Protocol;
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


    }   // class SimbasiForm
}
