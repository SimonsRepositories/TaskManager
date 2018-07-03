using System.Windows;
using System.Windows.Controls;

namespace TaskManager.Pages
{
    /// <summary>
    /// Interaction logic for BackButton.xaml
    /// </summary>
    public partial class Button_BackHome : UserControl
    {
        public Button_BackHome()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Page_Home());
        }
    }
}
