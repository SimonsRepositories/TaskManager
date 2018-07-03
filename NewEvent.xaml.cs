using System;
using System.Windows;
using System.Windows.Data;
using System.Data;
using System.Data.OleDb;
using TaskManager.TaskDataSetTableAdapters;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class newEvent : UserControl
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

        public newEvent()
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
                "Changes saved back to the database, " +
                rows + " row(s) updated.");
        }

        private void addNewButton_Click(object sender, RoutedEventArgs e)
        {
            tasks.AddTasksRow("New Task", "", "", new DateTime(), new DateTime(), "");
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
    }
}

