using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for NewTaskButton.xaml
    /// </summary>
    public partial class NewTaskButton : UserControl
    {
        public NewTaskButton()
        {
            InitializeComponent();
        }

        private void NewEvent_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new newEvent());
        }
    }
}
