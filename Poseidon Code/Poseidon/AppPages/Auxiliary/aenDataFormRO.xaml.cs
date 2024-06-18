using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Windows;
using System.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;
using Telerik.Windows.Controls;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;
using Poseidon.Controls;

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for aenDataFormRO.xaml
    /// </summary>
    public partial class aenDataFormRO : UserControl
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public aenDataFormRO()
        {
            InitializeComponent();
        }


    }   // class aenDataFormRO
}
