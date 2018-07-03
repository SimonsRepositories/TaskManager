using System;
using System.Windows;
using System.Windows.Data;
using System.Data;
using System.Data.OleDb;
using TaskManager.TaskDataSetTableAdapters;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskManager.Pages
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Page_Events : UserControl
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

        public Page_Events()
        {
            InitializeComponent();

            TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();
            
            // OLEDB-Driver 12.0 (Achtung: nur 32 Bit!) https://www.microsoft.com/en-us/download/details.aspx?id=54920
            tasks = tasksTableAdapter.GetData();

            this.DataContext = tasks;

            // Listbox:
            //(new Event page)
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            DataView.MoveCurrentToNext();
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            DataView.MoveCurrentToPrevious();
        }

        private void OnRowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                OleDbCommand cmdNewID = new OleDbCommand(
                    "SELECT @@IDENTITY", e.Command.Connection);
                e.Row["ID"] = (int)cmdNewID.ExecuteScalar();
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            TasksTableAdapter tasksTableAdapter =
                new TasksTableAdapter();
            tasksTableAdapter.Adapter.RowUpdated +=
                new OleDbRowUpdatedEventHandler(OnRowUpdated);
            int rows = tasksTableAdapter.Update(tasks);
            System.Windows.MessageBox.Show(
                rows + " Appointent(s) have been saved successfully!");
        }

        private void addNewButton_Click(object sender, RoutedEventArgs e)
        {
            tasks.AddTasksRow("New Task", "", "", DateTime.Now, DateTime.Now, "");
            taskListBox.SelectedIndex = tasks.Rows.Count - 1;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            TaskDataSet.TasksRow selectedRow =
                (TaskDataSet.TasksRow)
                ((DataRowView)taskListBox.SelectedItem).Row;
            string task = selectedRow.Eventname;
            string message = "Are you sure you want to delete the " +
                "task \"" + task + "\"?";
            if (MessageBox.Show(message, "Delete Task",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                selectedRow.Delete();
            }
        }

        private void Button_Search_Click(object sender, EventArgs e)
        {
            taskListBox.ItemsSource = tasks.Where(x => x.Eventname.ToLower().Contains(SearchValue.Text.ToLower()) ||
                                              x.Location.ToLower().Contains(SearchValue.Text.ToLower())
                                              );
        }

        private void SearchValue_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                Button_Search_Click(this, new RoutedEventArgs());
            }
        }
    }
}

