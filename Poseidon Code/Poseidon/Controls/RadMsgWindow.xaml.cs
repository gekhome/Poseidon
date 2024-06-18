using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace Poseidon.Controls
{
    /// <summary>
    /// Interaction logic for RadMsgWindow.xaml
    /// </summary>
    public partial class RadMsgWindow : RadWindow
    {
        public RadMsgWindow()
        {
            InitializeComponent();
        }

        public string WindowText
        {
            get { return this.DisplayMessage.Text; }
            set { DisplayMessage.Text = value; }
        }


        private void OnButtonAcceptClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        private void OnButtonCancelClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

    }
}
