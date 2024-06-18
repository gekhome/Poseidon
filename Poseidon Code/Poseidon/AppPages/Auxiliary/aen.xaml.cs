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
using Poseidon.Model;
using Poseidon.Utilities;
using System.ComponentModel;
using Telerik.Windows.Controls;
using Poseidon.DataModel;
using Poseidon.Controls;

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for aenData.xaml
    /// </summary>
    public partial class aen : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        private bool isNewRec = false;

        public aen()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var aenQry = from aen in db.ΑΕΝs
                         select aen;
            DFaen.ItemsSource = aenQry.ToList();
        }

        private List<ΠΕΡΙΦΕΡΕΙΕΣ> GetPerifereies()
        {
            List<ΠΕΡΙΦΕΡΕΙΕΣ> preifereiaList = new List<ΠΕΡΙΦΕΡΕΙΕΣ>();

            var perifereies = from p in db.ΠΕΡΙΦΕΡΕΙΕΣs
                               orderby p.ΠΕΡΙΦΕΡΕΙΑ_ΚΩΔ
                               select p;

            return preifereiaList = perifereies.ToList();
        }

        private List<ΝΟΜΟΙ> GetNomoi()
        {
            List<ΝΟΜΟΙ> nomosList = new List<ΝΟΜΟΙ>();

            var nomoi = from n in db.ΝΟΜΟΙs
                              orderby n.ΝΟΜΟΣ
                              select n;

            return nomosList = nomoi.ToList();
        }

        private List<ΑΕΝ_ΕΛΛΑΔΑ> GetAen()
        {
            List<ΑΕΝ_ΕΛΛΑΔΑ> aenList = new List<ΑΕΝ_ΕΛΛΑΔΑ>();

            var aen = from a in db.ΑΕΝ_ΕΛΛΑΔΑs
                      orderby a.ΑΕΝ_ΕΠΩΝΥΜΙΑ
                      select a;
            return aenList = aen.ToList();

        }

        #region Dataform Events

        private void DFaen_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΕΠΩΝΥΜΙΑ", "Επωνυμία");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΕΥΘΥΝΣΗ", "Διεύθυνση (οδός, αρ.)");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΤΚ", "Τ.Κ.");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΟΛΗ", "Πόλη");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΕΡΙΦΕΡΕΙΑ", "Περιφέρεια");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΝΟΜΟΣ", "Νομός");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΤΗΛΕΦΩΝΑ", "Τηλέφωνα");
            e.DataField.Label = ((string)e.DataField.Label).Replace("FAX", "Fax");
            e.DataField.Label = ((string)e.DataField.Label).Replace("EMAIL", "E-mail");
            e.DataField.Label = ((string)e.DataField.Label).Replace("WEBSITE", "Ιστότοπος");


            if (e.PropertyName == "ΕΠΩΝΥΜΙΑ")
            {
                DataFormComboBoxField aenField = new DataFormComboBoxField();
                aenField.ItemsSource = GetAen();
                aenField.Label = "Επωνυμία";

                aenField.DisplayMemberPath = "ΑΕΝ_ΕΠΩΝΥΜΙΑ";
                aenField.SelectedValuePath = "ΑΕΝ_ΚΩΔ";
                aenField.DataMemberBinding = new Binding("ΕΠΩΝΥΜΙΑ");
                e.DataField = aenField;
            }

            if (e.PropertyName == "ΠΕΡΙΦΕΡΕΙΑ")
            {
                DataFormComboBoxField perField = new DataFormComboBoxField();
                perField.ItemsSource = GetPerifereies();
                perField.Label = "Περιφέρεια";

                perField.DisplayMemberPath = "ΠΕΡΙΦΕΡΕΙΑ";
                perField.SelectedValuePath = "ΠΕΡΙΦΕΡΕΙΑ_ΚΩΔ";
                perField.DataMemberBinding = new Binding("ΠΕΡΙΦΕΡΕΙΑ");
                e.DataField = perField;
            }

            if (e.PropertyName == "ΝΟΜΟΣ")
            {
                DataFormComboBoxField nomosField = new DataFormComboBoxField();
                nomosField.ItemsSource = GetNomoi();
                nomosField.Label = "Νομός";

                nomosField.DisplayMemberPath = "ΝΟΜΟΣ";
                nomosField.SelectedValuePath = "ΝΟΜΟΣ_ΚΩΔ";
                nomosField.DataMemberBinding = new Binding("ΝΟΜΟΣ");
                e.DataField = nomosField;
            }

        }

        private void DFaen_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
            // check who is logged-in if not admin show message and cancel edit
            if (LoginClass.UserKey > 0)
            {
                UserFunctions.ShowAdminMessage("Επεξεργασία δυνατή μόνο από τον διαχειριστή.");
                e.Cancel = true;
                return;
            }

            int aenRecs = (from a in db.ΑΕΝs
                       select a).Count();

            if (aenRecs > 0)
            {
                UserFunctions.ShowAdminMessage("Μόνο μια ΑΕΝ μπορεί να καταχωρηθεί.");
                e.Cancel = true;
                return;
            }

            isNewRec = true;

        }

        private void DFaen_BeginningEdit(object sender, CancelEventArgs e)
        {
            // check who is logged-in if not admin show message and cancel edit
            if (LoginClass.UserKey > 0)
            {
                UserFunctions.ShowAdminMessage("Επεξεργασία δυνατή μόνο από τον διαχειριστή.");
                e.Cancel = true;
            }

        }

        private void DFaen_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                //DialogParameters parameters = new DialogParameters();
                //parameters.Content = "Οι αλλαγές που έγιναν θα αποθηκευτούν.";
                //parameters.Header = "Προειδοποίηση";
                //parameters.OkButtonContent = "Κλείσιμο";
                //RadWindow.Alert(parameters);

                if (isNewRec == true)
                {
                    var aen = this.DFaen.CurrentItem as ΑΕΝ;
                    db.ΑΕΝs.InsertOnSubmit(aen);
                    cm.CommitData(db);
                    // need to reset it otherwise InsertOnSubmit throws an exception if we edit immediately the new record
                    isNewRec = false;
                }
                else
                    cm.CommitData(db);
            }
        }

        private void DFaen_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει διαγραφή ΑΕΝ, μόνο μεταβολή στοιχείων");
        }

        private void DFaen_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        #endregion


    }   // class aenData
}
