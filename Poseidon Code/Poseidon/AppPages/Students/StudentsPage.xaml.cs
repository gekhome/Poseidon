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

namespace Poseidon.AppPages.Students
{
    /// <summary>
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        static Loading loadingWin;
        static bool isLoadingWinCreated;
        private CommitModel cm = new CommitModel();

        public StudentsPage()
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

        // ΣΠΟΥΔΑΣΤΕΣ
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

        // ΕΓΓΡΑΦΕΣ
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

        // ΒΑΘΜΟΛΟΓΙΕΣ
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

        private void item13_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem13);
        }

        private void item14_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem14);
        }

        private void item15_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem15);
        }


        // ΑΠΟΥΣΙΕΣ
        private void item20_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem20);
        }

        private void item21_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem21);
        }

        private void item22_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem22);
        }

        private void item23_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem23);
        }

        private void item24_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem24);
        }

        private void item30_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem30);
        }

        private void item31_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem31);
        }

        private void item32_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem32);
        }

        private void item33_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem33);
        }
        private void item34_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem34);
        }


        // ΠΡΑΚΤΙΚΗ ΑΣΚΗΣΗ (ΚΕΠ)
        private void item40_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem40);
        }

        private void item41_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem41);
        }

        private void item42_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem42);
        }

        private void item43_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem43);
        }

        private void item44_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem44);
        }

        private void item45_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem45);
        }

        // ΠΤΥΧΙΑΚΕΣ
        private void item50_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem50);
        }

        private void item51_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem51);
        }

        private void item52_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem52);
        }

        private void item53_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem53);
        }

        private void item54_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem54);
        }

        private void item55_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem55);
        }

        // ΕΝΤΥΠΑ
        private void item60_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItem60);
        }

        #endregion


    }   // class StudentsPage
}
