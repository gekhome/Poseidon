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

namespace Poseidon.AppPages.Teachers
{
    /// <summary>
    /// Interaction logic for TeacherSearch.xaml
    /// </summary>
    public partial class TeacherSearch : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public TeacherSearch()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // this loads the data of the grid and form.

            var trainers = from dbp in db.ΕΚΠΑΙΔΕΥΤΙΚΟΣs
                           orderby dbp.ΕΠΩΝΥΜΟ, dbp.ΟΝΟΜΑ
                           select dbp;
            teacherGrid.ItemsSource = trainers.ToList();
        }


        #region Teacher DataGrid events

        private void teacherGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (teacherGrid.ItemsSource == null) return;

            teacherGrid.SelectedItem = teacherGrid.Items[0];
        }

        /*--------------
         * This is required. Clicking on the row details button does NOT
         * select the row and this causes exception thrown in Save event and
         * validation function. Save event looks for the SelectedItem.
         *--------------
         */
        private void teacherGrid_RowDetailsVisibilityChanged(object sender, GridViewRowDetailsEventArgs e)
        {
            teacherGrid.SelectedItem = e.Row.Item;
        }

        #endregion

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

    }   //class TeacherSearch
}
