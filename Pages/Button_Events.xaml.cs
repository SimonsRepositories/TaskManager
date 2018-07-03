using System.Windows;
using System.Windows.Controls;

namespace TaskManager.Pages
{
    /// <summary>
    /// Interaction logic for NewTaskButton.xaml
    /// </summary>
    public partial class Button_Events : UserControl
    {
        public Button_Events()
        {
            InitializeComponent();
        }

        private void NewEvent_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Page_Events());
        }
    }
}
