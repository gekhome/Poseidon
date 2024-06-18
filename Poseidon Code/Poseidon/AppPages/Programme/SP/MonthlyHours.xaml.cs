using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Input;
using Telerik.Windows.Controls.Navigation;
using Telerik.Windows.Controls.GridView;
using System.Collections.ObjectModel;
using Poseidon.Utilities;
using Poseidon.DataModel;
using Poseidon.Controls;
using Poseidon.Model;
using Poseidon.AppPages.Printouts;

namespace Poseidon.AppPages.Programme.SP
{
    /// <summary>
    /// Interaction logic for MonthlyHours.xaml
    /// </summary>
    public partial class MonthlyHours : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public MonthlyHours()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var syears = from y in db.sqlΣΧΟΛΙΚΟ_ΕΤΟΣs
                         orderby y.ΣΧΟΛ_ΕΤΟΣ descending
                         select y;
            cboSchoolYear.ItemsSource = syears.ToList();

            var tmimata = from t in db.sqlΤΜΗΜΑΤΑ_ΣΠs
                          orderby t.ΤΜΗΜΑ_ΟΝΟΜΑΣΙΑ
                          select t;
            cboTmima.ItemsSource = tmimata.ToList();

            var grid_data = from g in db.sqlΤΠΗ_ΕΚΠ_ALL_ΣΠs
                            orderby g.ΣΧΟΛ_ΕΤΟΣ descending
                            select g;
            dataGrid.ItemsSource = grid_data.ToList();

        }

        private void cboSchoolYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!cboSchoolYear.IsLoaded) return;
            if (cboSchoolYear.SelectedValue != null)
            {
                string syear = cboSchoolYear.SelectedValue.ToString();
                var tmimata = from t in db.sqlΤΜΗΜΑΤΑ_ΣΠs
                              where t.ΣΧΟΛ_ΕΤΟΣ == syear
                              orderby t.ΤΜΗΜΑ_ΟΝΟΜΑΣΙΑ
                              select t;

                cboTmima.ItemsSource = tmimata.ToList();
            }
            else
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε διδ. έτος");
                return;
            }
        }

        #region Button Events

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            if (cboSchoolYear.SelectedValue == null || cboTmima.SelectedValue == null)
            {
                UserFunctions.ShowAdminMessage("Πρέπει να επιλέξετε διδακτικό έτος και τμήμα.");
                return;
            }

            if (cboSchoolYear.SelectedValue != null && cboTmima.SelectedValue != null)
            {
                string syear = cboSchoolYear.SelectedValue.ToString();
                string stmima = cboTmima.SelectedValue.ToString();

                var grid_data = from g in db.sqlΤΠΗ_ΕΚΠ_ALL_ΣΠs
                                where g.ΣΧΟΛ_ΕΤΟΣ == syear && g.ΤΜΗΜΑ_ΟΝΟΜΑΣΙΑ == stmima
                                orderby g.ΣΧΟΛ_ΕΤΟΣ descending
                                select g;
                dataGrid.ItemsSource = grid_data.ToList();
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            cboSchoolYear.SelectedValue = null;
            cboTmima.SelectedValue = null;

            LoadData();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            MonthHoursSP radwindow = new MonthHoursSP();
            radwindow.Show();
        }

        #endregion



    }   // class MonthlyHours
}
