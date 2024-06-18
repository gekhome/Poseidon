using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Threading;
using Poseidon.Utilities;
using Poseidon.DataModel;
using Poseidon.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Navigation;
using System.Collections.ObjectModel;

namespace Poseidon.AppPages.Teachers
{
    /// <summary>
    /// Interaction logic for TeachersPage.xaml
    /// </summary>
    public partial class TeachersPage : Page
    {
        static Loading loadingWin;
        static bool isLoadingWinCreated;
        private CommitModel cm = new CommitModel();

        public TeachersPage()
        {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(RadTabItem), RoutedEventHelper.CloseTabEvent, new RoutedEventHandler(OnCloseClicked));

        }

        public void OnCloseClicked(object sender, RoutedEventArgs args)
        {
            ClosableTabItem tabItem = (ClosableTabItem)args.Source; // get chosen tab
            ((UIElement)tabItem.Content).Visibility = Visibility.Collapsed; // collapse tab contents
            tabItem.Visibility = Visibility.Collapsed; // collapse tab

            //tabItem = sender as RadTabItem;
            //// Remove the item from the collection the control is bound to
            //tabItem.Visibility = Visibility.Collapsed;
            //tabItem.Content = null;
        }

        private void TabOpen(ClosableTabItem tabItem)
        {
            ((UIElement)tabItem.Content).Visibility = Visibility.Visible; // show its contents
            tabItem.Visibility = Visibility.Visible; // show the tab itself
            tabItem.IsSelected = true; // select it
        }


        #region ProgressBar Threading

        private void Page_Initialized(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                ProgressBarShow();

                System.Windows.Threading.Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            while (!isLoadingWinCreated) ;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (loadingWin != null)
            {
                loadingWin.CloseForm();
            }
            Cursor = Cursors.Arrow;
        }

        private static void ProgressBarShow()
        {
            loadingWin = new Loading()
            {
                Left = Convert.ToDouble(SystemParameters.PrimaryScreenWidth / 2.0 - 120.0),
                Top = Convert.ToDouble(SystemParameters.PrimaryScreenHeight - 240.0)
            };

            isLoadingWinCreated = true;
            loadingWin.Show();
        }

        #endregion

        #region TabItems Events

        private void item1_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem1);
        }

        private void item2_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem2);
        }

        private void item3_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem3);
        }

        private void item4_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem4);
        }

        private void item5_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem5);
        }

        private void item6_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem6);
        }

        private void item7_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem7);
        }

        private void item8_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem8);
        }

        private void item9_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem9);
        }

        private void item10_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem10);
        }

        private void item11_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem11);
        }

        private void item12_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem12);
        }


        private void item20_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem20);
        }

        #endregion

        private void item21_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem21);
        }

        private void item22_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem22);
        }

        private void item30_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem30);
        }


    }   //class TeachersPage
}
