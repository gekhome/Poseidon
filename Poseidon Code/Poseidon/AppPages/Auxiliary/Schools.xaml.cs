using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Threading;
using Telerik.Windows.Controls;
using Poseidon.Utilities;
using Poseidon.Model;
using Poseidon.DataModel;
using Poseidon.Controls;

namespace Poseidon.AppPages.Auxiliary
{
    /// <summary>
    /// Interaction logic for Schools.xaml
    /// </summary>
    public partial class Schools : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();

        public Schools()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var schools = from p in db.ΣΧΟΛΕΣs
                          orderby p.ΣΧΟΛΗ_ΚΩΔ descending
                          select p;

            DFSchool.ItemsSource = schools.ToList();
        }

        #region DataForm Event Handling

        private void DFSchool_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            //var school = this.DFSchool.CurrentItem as ΣΧΟΛΗ;
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΣΧΟΛΗ_ΚΩΔ", "Κωδ.Σχολής");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΣΧΟΛΗ", "Σχολή");

            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΕΥΘΥΝΤΗΣ_ΟΝΟΜΑ", "Ονοματεπώνυμο Διευθυντή");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΕΥΘΥΝΤΗΣ_ΤΗΛΕΦΩΝΟ", "Τηλέφωνο Διευθυντή");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΕΥΘΥΝΤΗΣ_ΦΑΞ", "Φαξ Διευθυντή");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΕΥΘΥΝΤΗΣ_EMAIL", "E-mail Διευθυντή");

            e.DataField.Label = ((string)e.DataField.Label).Replace("ΥΠΟΔΙΕΥΘ_ΟΝΟΜΑ", "Ονοματεπώνυμο Υποδιευθυντή");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΥΠΟΔΙΕΥΘ_ΤΗΛΕΦΩΝΟ", "Τηλέφωνο Υποδιευθυντή");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΥΠΟΔΙΕΥΘ_ΦΑΞ", "Φαξ Υποδιευθυντή");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΥΠΟΔΙΕΥΘ_EMAIL", "E-mail Υποδιευθυντή");

            if (e.PropertyName == "ΣΧΟΛΗ_ΚΩΔ")
            {
                e.DataField.Visibility = Visibility.Collapsed;
                //e.DataField.IsEnabled = false;
            }
            if (e.PropertyName == "ΣΧΟΛΗ_ΣΥΝΤΟΜΟΓΡΑΦΙΑ")
            {
                e.DataField.Visibility = Visibility.Collapsed;
                //e.DataField.IsEnabled = false;
            }

            if (e.PropertyName == "ΣΧΟΛΗ")
            {
                e.DataField.IsReadOnly = true;
            }
            
        }

        private void DFSchool_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void DFSchool_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                //DialogParameters parameters = new DialogParameters();
                //parameters.Content = "Οι αλλαγές που έγιναν θα αποθηκευτούν.";
                //parameters.Header = "Προειδοποίηση";
                //parameters.OkButtonContent = "Κλείσιμο";
                //RadWindow.Alert(parameters);
                cm.CommitData(db);
            }

        }//DFSchool_EditEnding

        private void DFSchool_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
             UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει προσθήκη άλλης Σχολής.");
             e.Cancel = true;
             return;
        }

        private void DFSchool_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var school = this.DFSchool.CurrentItem as ΣΧΟΛΕΣ;
            UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει διαγραφή Σχολής. Μόνο επεξεργασία των στοιχείων.");
            e.Cancel = true;
        }

        private void DFSchool_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var school = this.DFSchool.CurrentItem as ΣΧΟΛΕΣ;

            // validate input fields
            // setup RadAlert parameters
            DialogParameters parameters = new DialogParameters();
            parameters.Header = "Σφάλμα";
            parameters.OkButtonContent = "Κλείσιμο";
            e.Cancel = false;

            // Στοιχεία Διευθυντή

            if (String.IsNullOrEmpty(school.ΔΙΕΥΘΥΝΤΗΣ_ΟΝΟΜΑ))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το ονοματεπώνυμο του Διευθυντή.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(school.ΔΙΕΥΘΥΝΤΗΣ_ΤΗΛΕΦΩΝΟ))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το τηλέφωνο του Διευθυντή.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(school.ΔΙΕΥΘΥΝΤΗΣ_ΦΑΞ))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το φαξ του Διευθυντή.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(school.ΔΙΕΥΘΥΝΤΗΣ_EMAIL))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το email του Διευθυντή.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            
            // Στοιχεία Υποδιευθυντή

            if (String.IsNullOrEmpty(school.ΥΠΟΔΙΕΥΘ_ΟΝΟΜΑ))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το ονοματεπώνυμο του Υποδιευθυντή.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(school.ΥΠΟΔΙΕΥΘ_ΤΗΛΕΦΩΝΟ))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το τηλέφωνο του Υποδιευθυντή.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(school.ΥΠΟΔΙΕΥΘ_ΦΑΞ))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το φαξ του Υποδιευθυντή.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (String.IsNullOrEmpty(school.ΥΠΟΔΙΕΥΘ_EMAIL))
            {
                parameters.Content = "Πρέπει να συμπληρωθεί το email του Υποδιευθυντή.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }

        } //DFSchool_ValidatingItem

        private bool ValidateDeleteSchool(int school_id)
        {
            return false;
        }

        #endregion


    }   // class Schools
}
