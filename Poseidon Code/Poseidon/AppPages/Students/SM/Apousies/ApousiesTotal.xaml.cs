﻿using System;
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

namespace Poseidon.AppPages.Students.SM
{
    /// <summary>
    /// Interaction logic for ApousiesTotal.xaml
    /// </summary>
    public partial class ApousiesTotal : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();

        public ApousiesTotal()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var apousies = from a in db.ViewΑΠΟΥΣΙΕΣ_ΣΥΝΟΛΟ_ΟΡΙΟ_ΣΜs
                           orderby a.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ, a.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                           select a;

            dataGrid.ItemsSource = apousies.ToList();
        }

    }
}
