using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Poseidon.Utilities;
using Poseidon.Model;

namespace Poseidon.Shell
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        static Loading loadingWin;
        static bool isLoadingWinCreated;


        #region Page Constructors

        public HomePage()
        {
            InitializeComponent();
            // we need this in the default constructor, otherwise
            // user is not shown when navigating back and forth to this page.
            txtLoginID.Text = LoginClass.Userid;
            GetUserInfo();
            //changeBackground("pack://application:,,,/Poseidon;component/Shell/Wallpapers/windows8.jpg");
        }

        public HomePage(string inputString)
        {
            InitializeComponent();
            //changeBackground("pack://application:,,,/Poseidon;component/Shell/Wallpapers/windows8.jpg");
            txtLoginID.Text = inputString;
            LoginClass.Userid = inputString; // set the global static variable to the username
            LoginClass.SetSchoolType();
            GetUserInfo();
        }

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


        private void GetUserInfo()
        {
            int userkey = LoginClass.UserKey;

            if (userkey == 0)
            {
                txtUserInfo.Text = "ΔΙΑΧΕΙΡΙΣΤΗΣ";
            }
            else
            {
                txtUserInfo.Text = "ΑΕΝ ΧΡΗΣΤΗΣ";
            }

        } // GetUserInfo

    }
}
