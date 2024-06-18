using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Poseidon.Controls
{
    public partial class MessagePopup : UserControl
    {
        public MessagePopup()
        {
            InitializeComponent();
            base.CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Close,
                (sender, e) =>
                {
                    base.Visibility = Visibility.Collapsed;
                    e.Handled = true;
                },
                (sender, e) =>
                {
                    e.CanExecute = true;
                    e.Handled = true;
                }));
            this.IsVisibleChanged += MessagePopup_IsVisibleChanged;
            //this.messageText.Text = "Μήνυμα Συστήματος";
        }

        void MessagePopup_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Raise the Loaded event so that the intro animation runs.
            if (base.IsVisible)
                base.RaiseEvent(new RoutedEventArgs(FrameworkElement.LoadedEvent, this));
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);

            if (ApplicationCommands.Close.CanExecute(null, this))
                ApplicationCommands.Close.Execute(null, this);
        }
        public string ContentText
        { 
            get {return messageText.Text;}
            set { messageText.Text = value; }
        }
            
    }
}