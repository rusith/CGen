using System.Windows;
using CGen.Generator.Windows.Models;

namespace CGen.Generator.Windows
{
    /// <summary>
    /// Interaction logic for AddNewDatabaseConnection.xaml
    /// </summary>
    public partial class AddNewDatabaseConnection
    {
        public DatabaseConnection  Connection { get; set; }

        public AddNewDatabaseConnection()
        {
            InitializeComponent();
            Connection = new DatabaseConnection();
            DataContext = Connection;
        }

        public AddNewDatabaseConnection(DatabaseConnection connection)
        {
            InitializeComponent();
            Title = "Edit Database Connection";
            AddButton.Visibility = Visibility.Hidden;
            CancelButton.Content = "Done";
            CancelButton.Margin = AddButton.Margin;
            Connection = connection;
            DataContext = Connection;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Connection = DataContext as DatabaseConnection;
            Close();
        }
    }
}
