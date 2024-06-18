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
using Poseidon.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.Data.DataForm;
using System.ComponentModel;

namespace Poseidon.AppPages.Students
{
    /// <summary>
    /// Interaction logic for StudentSearch.xaml
    /// </summary>
    public partial class StudentSearch : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public StudentSearch()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // this loads the data of the grid and form.

            var students = from s in db.ViewΣΠΟΥΔΑΣΤΗΣs
                           orderby s.ΣΧΟΛΗ, s.ΟΝΟΜΑΤΕΠΩΝΥΜΟ 
                           select s;
            DataGrid.ItemsSource = students.ToList();
        }

    }
}
