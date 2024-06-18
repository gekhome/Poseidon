using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Controls.Navigation;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;
using Poseidon.Controls;

namespace Poseidon.AppPages.Tools
{
    /// <summary>
    /// Interaction logic for BookShips.xaml
    /// </summary>
    public partial class BookShips : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private bool foldActivated;

        public BookShips()
        {
            InitializeComponent();
            //this.book1.Items.Clear();
            LoadData();
        }

        private void LoadData()
        {
            var ships = from s in db.ΠΛΟΙΑ_ΕΙΔΗs
                        select s;
            this.book1.ItemsSource = ships.ToList();


        }


        #region RadBook Events

        private void RadBook1_FoldActivated(object sender, FoldEventArgs e)
		{
			this.foldActivated = true;
		}

        private void RadBook1_FoldDeactivated(object sender, FoldEventArgs e)
        {
            this.foldActivated = false;
        }

        private void RadBookItemMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // open the pages on page click
            if (!this.foldActivated)
            {
                this.book1.RightPageIndex = 2;
            }
        }

        #endregion

    } // class BookShips
}
