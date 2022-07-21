using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;

namespace foxTasker
{
    public partial class ActionWindow : Window
    {
        ActionManager _actionManager { get; }
        public ActionWindow(ActionManager actionManager)
        {
            InitializeComponent();
            _actionManager = actionManager;
            DataContext = _actionManager;

            // this.Closing += new CancelEventHandler(ActionWindow_Closed);
        }
        private void actSelectChanged(object sender, SelectionChangedEventArgs obj)
        {
            string? selectedAct = null;
            ComboBox? senderCB = sender as ComboBox;
            if (senderCB != null) { selectedAct = senderCB.SelectedItem as string; }
            if (selectedAct != null) _actionManager.selectedActType = selectedAct;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = true;
        // void ActionWindow_Closed(object? sender, CancelEventArgs e)
        // {
        //     _actionManager.clearProperties();
        // }
    }
}