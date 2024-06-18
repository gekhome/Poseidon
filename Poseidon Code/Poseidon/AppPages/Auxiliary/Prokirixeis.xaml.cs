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
    /// Interaction logic for Prokirixeis.xaml
    /// </summary>
    public partial class Prokirixeis : Page
    {
        private PoseidonDataContext db = new PoseidonDataContext();
        private CommitModel cm = new CommitModel();
        private bool isNewRec = false;

        public Prokirixeis()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var prok = from p in db.ΠΡΟΚΗΡΥΞΕΙΣs
                       orderby p.ΠΡΟΚΗΡΥΞΗ_ΚΩΔ descending
                       select p;

            DataFormProk.ItemsSource = prok.ToList();
        }

        #region Combos Data Sources

        private List<ΣΧΟΛΙΚΟ_ΕΤΟΣ> GetSchoolYears()
        {
            List<ΣΧΟΛΙΚΟ_ΕΤΟΣ> school_yearList = new List<ΣΧΟΛΙΚΟ_ΕΤΟΣ>();

            var school_years = from s in db.ΣΧΟΛΙΚΟ_ΕΤΟΣs
                               orderby s.ΣΧΟΛ_ΕΤΟΣ descending
                               select s;

            return school_yearList = school_years.ToList();
        }

        #endregion


        #region DataForm Event Handling

        private void DataFormProk_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            //var prokirixi = this.DataFormProk.CurrentItem as ΠΡΟΚΗΡΥΞΗ;
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΡΟΚΗΡΥΞΗ_ΚΩΔ", "Α/Α Προκήρυξης");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΡΟΚΗΡΥΞΗ_ΑΠ", "Α.Π. Προκήρυξης");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ", "Διδακτικό Έτος");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΕΝΑΡΞΗ", "Ημ/νία Έναρξης");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΛΗΞΗ", "Ημ/νία Λήξης");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΕΝ", "Α.Ε.Ν.");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΠΟΛΗ", "Πόλη");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΔΙΟΙΚΗΤΗΣ", "Διοικητής");
            e.DataField.Label = ((string)e.DataField.Label).Replace("ΑΔΑ", "ΑΔΑ");
            

            if (e.PropertyName == "ΠΡΟΚΗΡΥΞΗ_ΚΩΔ")
            {
                e.DataField.Visibility = Visibility.Hidden;
                //e.DataField.IsEnabled = false;
            }

            if (e.PropertyName == "ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ")
            {
                DataFormComboBoxField schoolYearField = new DataFormComboBoxField();
                schoolYearField.ItemsSource = GetSchoolYears();
                schoolYearField.Label = "Διδακτικό Έτος";

                schoolYearField.DisplayMemberPath = "ΣΧΟΛ_ΕΤΟΣ";
                schoolYearField.SelectedValuePath = "ΣΧΟΛ_ΕΤΟΣ_ΚΩΔ";
                schoolYearField.DataMemberBinding = new Binding("ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ");
                e.DataField = schoolYearField;
            }

        }

        private void DataFormProk_BeginningEdit(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void DataFormProk_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            if (e.EditAction == Telerik.Windows.Controls.Data.DataForm.EditAction.Commit)
            {
                DialogParameters parameters = new DialogParameters();
                parameters.Content = "Οι αλλαγές που έγιναν θα αποθηκευτούν.";
                parameters.Header = "Προειδοποίηση";
                parameters.OkButtonContent = "Κλείσιμο";
                RadWindow.Alert(parameters);

                if (isNewRec == true)
                {
                    var prokirixi = this.DataFormProk.CurrentItem as ΠΡΟΚΗΡΥΞΕΙΣ;
                    db.ΠΡΟΚΗΡΥΞΕΙΣs.InsertOnSubmit(prokirixi);
                    cm.CommitData(db);
                    //db.SubmitChanges();
                    // need to reset it otherwise InsertOnSubmit throws an exception if we edit immediately the new record
                    isNewRec = false;
                }
                else
                    cm.CommitData(db);
            }
        }//DataFormProk_EditEnding

        private void DataFormProk_AddingNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddingNewItemEventArgs e)
        {
            // check who is logged-in if not admin show message and cancel edit
            if (LoginClass.UserKey > 0)
            {
                UserFunctions.ShowAdminMessage("Επεξεργασία δυνατή μόνο από τον διαχειριστή.");
                e.Cancel = true;
                return;
            }
            isNewRec = true;
        }

        private void DataFormProk_DeletingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (LoginClass.UserKey > 0)
            {
                UserFunctions.ShowAdminMessage("Επεξεργασία δυνατή μόνο από διαχειριστές.");
                e.Cancel = true;
                return;
            }

            var prokirixi = this.DataFormProk.CurrentItem as ΠΡΟΚΗΡΥΞΕΙΣ;
                
            if ((MessageBox.Show("Είστε σίγουροι ότι θέλετε διαγραφή αυτής της εγγραφής;", this.Title,
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes))
            {
                    if (ValidateDeleteProkirixi(prokirixi.ΠΡΟΚΗΡΥΞΗ_ΚΩΔ))
                    {
                        e.Cancel = false;
                        db.ΠΡΟΚΗΡΥΞΕΙΣs.DeleteOnSubmit(prokirixi);
                        cm.CommitData(db);
                    }
                    else
                    {
                        UserFunctions.ShowAdminMessage("Δεν μπορεί να γίνει διαγραφή διότι υπάρχουν συσχετισμένες αναθέσεις.");
                        e.Cancel = true;
                    }
             }
          }


        private void DataFormProk_ValidatingItem(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var prokirixi = this.DataFormProk.CurrentItem as ΠΡΟΚΗΡΥΞΕΙΣ;
            if (prokirixi == null)
            {
                e.Cancel = false;
                return;
            }

            // validate input fields
            string dateStart = Convert.ToString(prokirixi.ΕΝΑΡΞΗ); //null value converted to ""
            string dateEnd = Convert.ToString(prokirixi.ΛΗΞΗ);     //null value converted to ""
            int schoolYearID = Convert.ToInt32(prokirixi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ);
            // setup RadAlert parameters
            DialogParameters parameters = new DialogParameters();
            parameters.Header = "Σφάλμα";
            parameters.OkButtonContent = "Κλείσιμο";
            e.Cancel = false;

            if (dateStart == "" || dateEnd == "")
            {
                parameters.Content = "Πρέπει να συμπληρωθούν και οι δύο ημερομηνίες";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (!Dates.ValidStartEndDates(dateStart, dateEnd))
            {
                parameters.Content = "Η αρχική ημερομηνία είναι μεγαλύτερη της τελικής";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (!Dates.InSameYear(dateStart, dateEnd))
            {
                parameters.Content = "Η αρχική και τελική ημερομηνία πρέπει να έχουν το ίδιο έτος";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (prokirixi.ΠΡΟΚΗΡΥΞΗ_ΑΠ == null)
            {
                parameters.Content = "Πρέπει να συμπληρωθεί ο αρ. πρωτοκόλλου της Προκήρυξης.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (prokirixi.ΔΙΔΑΚΤΙΚΟ_ΕΤΟΣ == null)
            {
                parameters.Content = "Πρέπει να γίνει επιλογή διδακτικού έτους.";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }
            else if (!Dates.InSchoolYear(dateStart, dateEnd, schoolYearID))
            {
                parameters.Content = "Οι ημερομηνίες προκήρυξης δεν συμβαδίζουν με το σχολικό έτος";
                RadWindow.Alert(parameters);
                e.Cancel = true;
            }

        } //DataFormProk_DeletedItem


        private bool ValidateDeleteProkirixi(int prokirixi_id)
        {
            //var anatheseis = (from p in db.ΑΝΑΘΕΣΕΙΣs
            //                where p.ΠΡΟΚΗΡΥΞΗ == prokirixi_id
            //                select p).Count();

            //if (anatheseis > 0) return false;
            //else return true;

            return true;
        }

        #endregion


    }   // class Prokirixeis
}
