using System;
using System.Linq;
using System.Windows;
using CGen.Generator.Windows.Models;

namespace CGen.Generator.Windows
{
    /// <summary>
    /// Interaction logic for DatabaseConnectionManager.xaml
    /// </summary>
    public partial class DatabaseConnectionManager
    {
        private readonly GeneratorViewModel _context;

        public DatabaseConnectionManager(GeneratorViewModel viewModel)
        {
            InitializeComponent();
            _context = viewModel;
            DataContext = _context;
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var newDtabaseConnection = new AddNewDatabaseConnection();
                var result = newDtabaseConnection.ShowDialog();
                if (result != true)
                    return;

                var connection = newDtabaseConnection.Connection;
                if (_context.DatabaseConnections.Where(c => c.Name == connection.Name).ToList().Count > 0)
                {
                    MessageBox.Show("Connection with name "+connection.Name+" already exist",
                       "name already exists", MessageBoxButton.OK, MessageBoxImage.Error,
                       MessageBoxResult.OK);
                    return;
                }
                _context.DatabaseConnections.Add(connection);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception.StackTrace,
                    "An error occurred  while adding new connection ", MessageBoxButton.OK, MessageBoxImage.Error,
                    MessageBoxResult.OK);
            }
        }

        private void OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            if(_context.SelectedDatabaseConnection==null)
                return;

            try
            {
                var newDtabaseConnection = new AddNewDatabaseConnection(_context.SelectedDatabaseConnection);
                newDtabaseConnection.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception.StackTrace,
                   "An error occurred  while editing a connection ", MessageBoxButton.OK, MessageBoxImage.Error,
                   MessageBoxResult.OK);
            }
        }


        private void OnRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            if (_context.SelectedDatabaseConnection == null)
                return;

            try
            {
                _context.DatabaseConnections.Remove(_context.SelectedDatabaseConnection);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception.StackTrace,
                   "An error occurred  while removing a connection ", MessageBoxButton.OK, MessageBoxImage.Error,
                   MessageBoxResult.OK);
            }
        }
    }
}
