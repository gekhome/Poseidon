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
using System.ComponentModel;


namespace Poseidon.SqlConnections
{
    /// <summary>
    /// Interaction logic for Connection.xaml
    /// </summary>
    public partial class Connection : INotifyPropertyChanged
    {
        private SqlConnectionString _connstring;

        public Connection()
        {
            InitializeComponent();
        }

        public SqlConnectionString ConnString
        {
            get { return _connstring; }
            set
            {
                _connstring = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ConnString"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
