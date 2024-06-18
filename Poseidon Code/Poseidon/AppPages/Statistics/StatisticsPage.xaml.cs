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

namespace Poseidon.AppPages.Statistics
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        static Loading loadingWin;
        static bool isLoadingWinCreated;
        private CommitModel cm = new CommitModel();

        public StatisticsPage()
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

        #region ΕΚΠΑΙΔΕΥΤΙΚΟΙ

        #region SP TEACHER GRAPHS SCREEN

        private void SPitem01_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPTask1);
        }

        private void SPitem02_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPTask2);
        }

        private void SPitem03_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPTask3);
        }

        private void SPitem04_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPTask4);
        }

        private void SPitem05_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPTask5);
        }

        #endregion

        #region SP TEACHER GRAPHS PRINTER

        private void SPReport1_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPReport1);
        }

        private void SPReport2_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPReport2);
        }

        private void SPReport3_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPReport3);
        }

        private void SPReport4_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPReport4);
        }

        private void SPReport5_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPReport5);
        }

        #endregion

        #region SM TEACHER GRAPHS SCREEN

        private void SMitem01_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMTask1);
        }

        private void SMitem02_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMTask2);
        }

        private void SMitem03_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMTask3);
        }

        private void SMitem04_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMTask4);
        }

        private void SMitem05_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMTask5);
        }

        #endregion

        #region SM TEACHER GRAPHS PRINTER

        private void SMReport1_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMReport1);
        }

        private void SMReport2_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMReport2);
        }

        private void SMReport3_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMReport3);
        }

        private void SMReport4_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMReport4);
        }

        private void SMReport5_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMReport5);
        }

        #endregion

        #region SP+SM TEACHER GRAPHS SCREEN

        private void ALLitem01_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL1);
        }

        private void ALLSitem02_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL2);
        }

        private void ALLitem03_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL3);
        }

        private void ALLitem04_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL4);
        }

        private void ALLitem05_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL5);
        }

        #endregion

        #region SP+SM TEACHER GRAPHS PRINTER

        private void ALLitem10_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL10);
        }

        private void ALLSitem11_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL11);
        }

        private void ALLitem12_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL12);
        }

        private void ALLitem13_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL13);
        }

        private void ALLitem14_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemALL14);
        }

        #endregion

        #endregion

        #region ΣΠΟΥΔΑΣΤΕΣ

        #region SP STUDENT GRAPHS SCREEN

        private void SPstud01_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSPstudent01);
        }

        private void SPstud02_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSPstudent02);
        }

        private void SPstud03_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSPstudent03);
        }

        private void SPstud04_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSPstudent04);
        }

        #endregion

        #region SM STUDENT GRAPHS SCREEN

        private void SMstud01_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSMstudent01);
        }

        private void SMstud02_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSMstudent02);
        }

        private void SMstud03_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSMstudent03);
        }

        private void SMstud04_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSMstudent04);
        }

        #endregion

        #region SP STUDENT GRAPHS PRINTER

        private void SPstudReport1_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSPstudent10);
        }

        private void SPstudReport2_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSPstudent11);
        }

        private void SPstudReport3_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSPstudent12);
        }

        private void SPstudReport4_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSPReport4);
        }

        #endregion

        #region SM STUDENT GRAPHS PRINTER

        private void SMstudReport1_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSMstudent10);
        }

        private void SMstudReport2_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSMstudent11);
        }

        private void SMstudReport3_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabSMstudent12);
        }

        private void SMstudReport4_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabItemSMReport4);
        }

        #endregion

        #region SP+SM STUDENT GRAPHS SCREEN

        private void ALLstud01_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent01);
        }

        private void ALLstud02_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent02);
        }

        private void ALLstud03_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent03);
        }

        private void ALLstud04_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent04);
        }

        private void ALLstud05_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent05);
        }

        #endregion

        #region SP+SM STUDENT GRAPHS PRINTER

        private void ALLstud10_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent10);
        }

        private void ALLstud11_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent11);
        }

        private void ALLstud12_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent12);
        }

        private void ALLstud13_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent13);
        }

        private void ALLstud14_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TabOpen(tabALLstudent14);
        }

        #endregion

        #endregion


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



    }   // class StatisticsPage
}
