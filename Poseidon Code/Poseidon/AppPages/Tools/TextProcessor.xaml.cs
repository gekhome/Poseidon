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
using Telerik.Windows.Controls.Data;
using Telerik.Windows.Controls.RibbonView;
using Telerik.Windows.Controls.RichTextBoxUI;
using Telerik.Windows.Documents;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.UI;

namespace Poseidon.AppPages.Tools
{
    /// <summary>
    /// Interaction logic for TextProcessor.xaml
    /// </summary>
    public partial class TextProcessor : UserControl
    {
        public TextProcessor()
        {
            InitializeComponent();
        }

        public RadDocument RadDocument
        {
            get
            {
                return this.radRichTextBox.Document;
            }
            set
            {
                SetupNewDocument(value);
                this.radRichTextBox.Document = value;

                //ExamplesDataContext dataContext = new ExamplesDataContext();

                //this.radRichTextBox.Document.MailMergeDataSource.ItemsSource = dataContext.Employees;
                //this.radRichTextBox.Users = dataContext.Users;
            }
        }

        private void SetupNewDocument(RadDocument document)
        {
            document.LayoutMode = DocumentLayoutMode.Paged;
            document.ParagraphDefaultSpacingAfter = 10;
            document.SectionDefaultPageMargin = new Padding(95);
        }

        private bool IsSupportedImageFormat(string extension)
        {
            if (extension != null)
            {
                extension = extension.ToLower();
            }

            return extension == ".jpg" ||
                extension == ".jpeg" ||
                extension == ".png" ||
                extension == ".bmp" ||
                extension == ".tif" ||
                extension == ".tiff" ||
                extension == ".ico" ||
                extension == ".gif" ||
                extension == ".wdp" ||
                extension == ".hdp";
        }

        //private void radRichTextBox_Drop(object sender, DragEventArgs e)
        //{
        //    string[] droppedFiles = e.Data.GetData(DataFormats.FileDrop) as string[];
        //    foreach (string droppedFile in droppedFiles)
        //    {
        //        string extension = System.IO.Path.GetExtension(droppedFile);
        //        if (IsSupportedImageFormat(extension))
        //        {
        //            FileInfo file = new FileInfo(droppedFile);

        //            using (Stream imageStream = file.OpenRead())
        //            {
        //                this.radRichTextBox.InsertImage(imageStream, extension);
        //            }
        //        }
        //    }
        //}

    } // class TextProcessor
}
