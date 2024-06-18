using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using Telerik.Windows.Controls;
using System.ComponentModel;
using Poseidon.Model;
using Poseidon.Utilities;
using Poseidon.DataModel;
using Poseidon.Controls;

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for aenData.xaml
    /// </summary>
    public partial class aenData : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();
        private bool isNewRec = false;

        public aenData()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var aen = from a in db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs
                      select a;
            FormAenData.ItemsSource = aen.ToList();
        }

        #region Form Events

        private void FormAenData_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
            isNewRec = true;
        }

        private void FormAenData_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                if (isNewRec == true)
                {
                    var aen = this.FormAenData.CurrentItem as ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ;
                    db.ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗs.InsertOnSubmit(aen);
                    cm.CommitData(db);
                    // need to reset it otherwise InsertOnSubmit throws an exception if we edit immediately the new record
                    isNewRec = false;
                }
                else
                    cm.CommitData(db);
            }
       }

        private void FormAenData_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var aen = this.FormAenData.CurrentItem as ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ;

            if (aen == null) return;

            if (String.IsNullOrEmpty(aen.ΔΙΟΙΚΗΤΗΣ_ΟΝΟΜΑΤΕΠΩΝΥΜΟ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί το ονοματεπώνυμο του Διοικητή.");
            }

            if (String.IsNullOrEmpty(aen.ΔΙΟΙΚΗΤΗΣ_ΒΑΘΜΟΣ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί ο βαθμός του Διοικητή.");
            }

            if (String.IsNullOrEmpty(aen.ΔΙΟΙΚΗΤΗΣ_ΕΔΡΑ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί η έδρα του Διοικητή.");
            }

            if (String.IsNullOrEmpty(aen.ΠΛΗΡΟΦΟΡΙΕΣ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί ο υπεύθυνος πληροφοριών.");
            }

            if (String.IsNullOrEmpty(aen.ΔΙΕΥΘΥΝΤΗΣ_ΣΜ) && String.IsNullOrEmpty(aen.ΔΙΕΥΘΥΝΤΗΣ_ΣΠ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί τουλάχιστον ο διευθυντής μιας Σχολής.");
            }

            if (String.IsNullOrEmpty(aen.ΔΝΤΗΣ_ΒΑΘΜΟΣ_ΣΜ) && String.IsNullOrEmpty(aen.ΔΝΤΗΣ_ΒΑΘΜΟΣ_ΣΠ))
            {
                e.Cancel = true;
                UserFunctions.ShowAdminMessage("Πρέπει να συμπληρωθεί τουλάχιστον ο βαθμός του διευθυντή μιας Σχολής.");
            }

        }

        private void FormAenData_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var aen = this.FormAenData.CurrentItem as ΑΕΝ_ΔΙΑΧΕΙΡΙΣΗ;
            UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει διαγραφή. Μόνο επεξεργασία και προσθήκη εγγραφών.");
            e.Cancel = true;
        }


        #endregion

    }
}
