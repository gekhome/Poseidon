using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Spreadsheet;
using Telerik.Windows.Controls.Spreadsheet.Commands;
using Telerik.Windows.Controls.Spreadsheet.Utilities;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;

namespace Poseidon.AppPages.Tools
{
    /// <summary>
    /// Interaction logic for Spreadsheet.xaml
    /// </summary>
    public partial class Spreadsheet : UserControl
    {
        IRadSheetEditor activeSheetEditor;

        static Spreadsheet()
        {
            WorkbookFormatProvidersManager.RegisterFormatProvider(new XlsxFormatProvider());
        }


        public Spreadsheet()
        {
            InitializeComponent();
            this.radSpreadsheet.ActiveSheetEditorChanged += this.RadSpreadsheet_ActiveSheetEditorChanged;
        }

        public string CurrentTheme
        {
            get
            {
                return ThemeHelper.DefaultThemeName;
            }
        }

        private void RadSpreadsheet_ActiveSheetEditorChanged(object sender, EventArgs e)
        {
            if (this.activeSheetEditor != null)
            {
                this.activeSheetEditor.UICommandExecuted -= this.ActiveSheetEditor_UICommandExecuted;
            }

            this.activeSheetEditor = this.radSpreadsheet.ActiveSheetEditor;

            if (this.activeSheetEditor != null)
            {
                this.activeSheetEditor.UICommandExecuted += this.ActiveSheetEditor_UICommandExecuted;
                this.ChangeScaleFactorIfTouchTheme();
            }
        }

        void ActiveSheetEditor_UICommandExecuted(object sender, UICommandExecutedEventArgs e)
        {
            if (!this.radSpreadsheet.IsKeyboardFocusWithin())
            {
                this.radSpreadsheet.Focus();
            }
        }

        private void RadRibbonGroup_LaunchDialog(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadRibbonGroup group = (RadRibbonGroup)sender;
            this.activeSheetEditor.CommandDescriptors.ShowFormatCellsDialog.Command.Execute(group.Header);
        }

        private void Example_ThemeChanged(object sender, System.EventArgs e)
        {
            this.ApplyThemeSpecificSettings();
        }

        private void ApplyThemeSpecificSettings()
        {
            if (this.CurrentTheme == "Expression_Dark")
            {
                IconManager.ChangeIconsSet(IconsSet.Dark);
            }
            else
            {
                IconManager.ChangeIconsSet(IconsSet.Light);
            }
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ThemeHelper.GetInstance().ThemeChanged += this.Example_ThemeChanged;
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ThemeHelper.GetInstance().ThemeChanged -= this.Example_ThemeChanged;
        }

        private void ChangeScaleFactorIfTouchTheme()
        {
            if (this.CurrentTheme == "Windows8Touch")
            {
                this.radSpreadsheet.ActiveSheetEditor.ScaleFactor = new System.Windows.Size(1.5, 1.5);
            }
            else
            {
                this.radSpreadsheet.ActiveSheetEditor.ScaleFactor = new System.Windows.Size(1, 1);
            }
        }




    }   // class Spreadsheet
}
