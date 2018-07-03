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
using System.Windows.Shapes;
using TaskManager.TaskDataSetTableAdapters;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for NewEvent.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {

        private TaskDataSet.TasksDataTable tasks;

        private CollectionView dataView;
        internal CollectionView DataView
        {
            get
            {
                if (dataView == null)
                {
                    dataView = (CollectionView)
                        CollectionViewSource.GetDefaultView(
                        this.DataContext);
                }
                return dataView;
            }
        }

        public HomePage()
        {

            InitializeComponent();

            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();

            // OLEDB-Driver 12.0 (Achtung: nur 32 Bit!) https://www.microsoft.com/en-us/download/details.aspx?id=54920
            tasks = tasksTableAdapter.GetData();

            this.DataContext = tasks;

            // Listbox:
            Lst.ItemsSource = tasksTableAdapter.GetData();
            
        }

        private void FiltByName(object sender, RoutedEventArgs e)
        {
            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();

            tasks = tasksTableAdapter.GetData();

            // Listbox:
            Lst.ItemsSource = tasks.Where(x => x.Eventname.Contains("Inc"));
        }

        private void FiltToday(object sender, RoutedEventArgs e)
        {
            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();

            tasks = tasksTableAdapter.GetData();

            // Listbox:
            Lst.ItemsSource = tasks.Where(x => x.StartDate.Date == DateTime.Now.Date);
        }

        private void FiltWeek(object sender, RoutedEventArgs e)
        {
            
        }

        private void FiltMonth(object sender, RoutedEventArgs e)
        {

        }

        private void FiltYear(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();

            tasks = tasksTableAdapter.GetData();

            // Listbox:
            Lst.ItemsSource = tasks;
        }

        
    }
}
