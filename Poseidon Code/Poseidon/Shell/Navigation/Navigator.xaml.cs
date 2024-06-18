using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Media.Effects;
using Poseidon.DataModel;
using Poseidon.Controls;

namespace Poseidon.Shell.Navigation
{
    /// <summary>
    /// Interaction logic for Navigator.xaml
    /// </summary>
    public partial class Navigator : UserControl
    {
        private CommitModel cm = new CommitModel();

        public static Brush defaultButtonBrush;
        public static Brush redButtonBrush;
        public static Brush greenButtonBrush;
        public static Brush yellowButtonBrush;
        public static Brush blueButtonBrush;
        public static Brush tealButtonBrush;
        public static Brush darkmagentaButtonBrush;

        public Navigator()
        {
            BrushConverter bc = new BrushConverter();
            defaultButtonBrush = (Brush)bc.ConvertFrom("#00A1F1");
            redButtonBrush = (Brush)bc.ConvertFrom("#F65314");
            yellowButtonBrush = (Brush)bc.ConvertFrom("#FFBB00");
            greenButtonBrush = (Brush)bc.ConvertFrom("#7CBB00");
            blueButtonBrush = (Brush)bc.ConvertFrom("#3947DE");
            tealButtonBrush = (Brush)bc.ConvertFrom("#008080");
            darkmagentaButtonBrush = (Brush)bc.ConvertFrom("#8B008B");

            InitializeComponent();
        }

        #region Tile Color Effects

        private void menu1_MouseEnter(object sender, MouseEventArgs e)
        {
            BorderClickEffect(menu1);
        }

        private void menu1_MouseLeave(object sender, MouseEventArgs e)
        {
            //menu1.Background = defaultButtonBrush;
            ResetBorder(menu1);
        }

        private void menu2_MouseEnter(object sender, MouseEventArgs e)
        {
            //menu2.Background = tealButtonBrush;
            BorderClickEffect(menu2);
        }

        private void menu2_MouseLeave(object sender, MouseEventArgs e)
        {
            //menu2.Background = defaultButtonBrush;
            ResetBorder(menu2);
        }

        private void menu3_MouseEnter(object sender, MouseEventArgs e)
        {
            //menu3.Background = darkmagentaButtonBrush;
            BorderClickEffect(menu3);
        }

        private void menu3_MouseLeave(object sender, MouseEventArgs e)
        {
            //menu3.Background = defaultButtonBrush;
            ResetBorder(menu3);
        }

        private void menu4_MouseEnter(object sender, MouseEventArgs e)
        {
            //menu4.Background = redButtonBrush;
            BorderClickEffect(menu4);
        }

        private void menu4_MouseLeave(object sender, MouseEventArgs e)
        {
            //menu4.Background = defaultButtonBrush;
            ResetBorder(menu4);
        }

        private void menu5_MouseEnter(object sender, MouseEventArgs e)
        {
            //menu5.Background = yellowButtonBrush;
            BorderClickEffect(menu5);
        }

        private void menu5_MouseLeave(object sender, MouseEventArgs e)
        {
            //menu5.Background = defaultButtonBrush;
            ResetBorder(menu5);
        }

        private void menu6_MouseEnter(object sender, MouseEventArgs e)
        {
            //menu6.Background = greenButtonBrush;
            BorderClickEffect(menu6);
        }

        private void menu6_MouseLeave(object sender, MouseEventArgs e)
        {
            menu6.Background = defaultButtonBrush;
            ResetBorder(menu6);
        }

        #endregion

        #region Tile Events

        private void Tile_Click(object sender, MouseButtonEventArgs e)
        {
            Border image = (Border)sender;

            string uri = "";
            switch (image.Name)
            {
                case "menu1":
                    //Έγινε επιλογή Σπουδαστών.
                    if (cm.PermitGrant() == true)
                    {
                        uri = "AppPages/Students/StudentsPage.xaml";
                    }
                    else uri = "";
                    break;
                case "menu2":
                    //Έγινε επιλογή στην σελίδα Εκπαιδευτικών.
                    uri = "AppPages/Teachers/TeachersPage.xaml";
                    break;
                case "menu3":
                    //Έγινε επιλογή στην σελίδα Προγράμματος.
                    uri = "AppPages/Programme/ProgrammeMain.xaml";
                    break;
                case "menu4":
                    //Έγινε επιλογή στην σελίδα Στατιστικών.
                    uri = "AppPages/Statistics/StatisticsPage.xaml";
                    break;
                case "menu5":
                    //Έγινε επιλογή στην σελίδα Εργαλείων.
                    if (cm.PermitGrant() == true)
                    {
                        uri = "AppPages/Tools/ToolsPage.xaml";
                    }
                    else uri = "";
                    break;
                case "menu6":
                    //Έγινε επιλογή στην σελίδα Ρυθμίσεων εφαρμογής.
                    cm.AdminMessage();
                    uri = "AppPages/Auxiliary/Auxiliary.xaml";;
                    break;

                default:
                    break;
            } //end switch
            if (uri != "")
            {
                NavigationService svc = NavigationService.GetNavigationService(image);
                if (svc != null)
                {
                    svc.Navigate(new Uri(uri, UriKind.Relative));
                }

            }
        }

        //private void HighlightBorder(Border theBorder)
        //{

        //    DropShadowEffect myDropShadowEffect = new DropShadowEffect() { Color = Colors.White, ShadowDepth = 5, Direction = 0, BlurRadius = 40, Opacity = 1};
        //    //OuterGlowEffect myGlow = new OuterGlowEffect()
        //    //{ GlowColor = Colors.WhiteSmoke, GlowSize = 20, Opacity = 1, Noise = 1 };
        //    //theBorder.Effect = myGlow;
        //    theBorder.Effect = myDropShadowEffect;
        //}

        private static void ResetBorder(Border theBorder)
        {
            theBorder.BorderThickness = new Thickness(2, 2, 2, 2);
            theBorder.BorderBrush = Brushes.Transparent;
            BrushConverter bc = new BrushConverter();
            theBorder.Background = defaultButtonBrush;

            //theBorder.Height = theBorder.Height + 10;
            //theBorder.Width = theBorder.Width + 10;

        }

        private static void BorderClickEffect(Border theBorder)
        {
            theBorder.BorderThickness = new Thickness(4, 4, 4, 4);
            theBorder.BorderBrush = Brushes.White;
            //theBorder.Height = theBorder.Height - 10;
            //theBorder.Width = theBorder.Width - 10;
        }

        #endregion


    }
}
