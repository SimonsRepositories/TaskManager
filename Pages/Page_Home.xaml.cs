using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using TaskManager.TaskDataSetTableAdapters;

namespace TaskManager.Pages
{
    /// <summary>
    /// Interaction logic for NewEvent.xaml
    /// </summary>
    public partial class Page_Home : UserControl
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

        public Page_Home()
        {

            InitializeComponent();

            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();

            // OLEDB-Driver 12.0 (Achtung: nur 32 Bit!) https://www.microsoft.com/en-us/download/details.aspx?id=54920
            tasks = tasksTableAdapter.GetData();

            this.DataContext = tasks;

            // Listbox:
            Lst.ItemsSource = tasksTableAdapter.GetData();

            //---------------           

            Groups = new ObservableCollection<string>();


            foreach (var item in tasksTableAdapter.GetData())
            {
                Groups.Add(item.Category);
            }

            cboCategoryTypes.ItemsSource = Groups.Distinct();
            cboCategoryTypes.SelectedValue = 1;

           

        }

        public ObservableCollection<string> Groups { get; set; }


        private void Filter_Today(object sender, RoutedEventArgs e)
        {
            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();
            tasks = tasksTableAdapter.GetData();

            //-------------------------------------------

            // Listbox:
            Lst.ItemsSource = tasks.Where(x => x.StartDate.Date == DateTime.Now.Date);

            Title.Content = "today";
        }

        private void Filter_Week(object sender, RoutedEventArgs e)
        {
            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();
            tasks = tasksTableAdapter.GetData();

            //-------------------------------------------

            DateTime Firstday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1);
            DateTime Endaday = Firstday.AddDays(6);

            // Listbox:
            Lst.ItemsSource = tasks.Where(x => x.StartDate.Date >= Firstday && x.EndDate.Date <= Endaday);

            Title.Content = "week";
        }

        private void Filter_Month(object sender, RoutedEventArgs e)
        {
            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();
            tasks = tasksTableAdapter.GetData();

            //-------------------------------------------

            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            DateTime firstDayMonth = new DateTime(year, month, 1);
            DateTime lastDayMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            // Listbox:
            Lst.ItemsSource = tasks.Where(x => x.StartDate.Date >= firstDayMonth && x.EndDate.Date <= lastDayMonth);

            Title.Content = "month";

        }

        private void Filter_Year(object sender, RoutedEventArgs e)
        {
            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();
            tasks = tasksTableAdapter.GetData();

            //-------------------------------------------

            int year = DateTime.Now.Year;
            DateTime firstDayYear = new DateTime(year, 1, 1);
            DateTime lastDayYear = new DateTime(year, 12, 31);

            // Listbox:
            Lst.ItemsSource = tasks.Where(x => x.StartDate.Date >= firstDayYear && x.EndDate.Date <= lastDayYear);

            Title.Content = "year";

        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();
            tasks = tasksTableAdapter.GetData();

            // Listbox:
            Lst.ItemsSource = tasks;

            Title.Content = "Overview";
        }

        private void Button_Suchen_Click(object sender, RoutedEventArgs e)
        {
            // Listbox:
            Lst.ItemsSource = tasks.Where(x => x.Eventname.ToLower().Contains(SearchValue.Text.ToLower()) ||
                                               x.Location.ToLower().Contains(SearchValue.Text.ToLower())
            );
        }

        private void cboEmpTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Listbox:
            Lst.ItemsSource = tasks.Where(x => x.Category == cboCategoryTypes.SelectedValue.ToString());
        }

        private void SearchValue_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                Button_Suchen_Click(this, new RoutedEventArgs());
            }
        }

        private void Help_Button(object sender, RoutedEventArgs e)
        {
            Page_Help win2 = new Page_Help();
            win2.Show();
        }

        private void About_Button(object sender, RoutedEventArgs e)
        {
            Page_About win2 = new Page_About();
            win2.Show();
        }
    }
}
