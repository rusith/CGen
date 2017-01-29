using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using CGen.Generator.Properties;
using CGen.Generator.Windows.Models;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System.IO;

namespace CGen.Generator.Windows
{
    public partial class Generator
    {
        private readonly GeneratorViewModel _context;

        public Generator()
        {
            _context = new GeneratorViewModel {DatabaseConnections = new ObservableCollection<DatabaseConnection>()};
            DataContext = _context;
            InitializeComponent();
        }

        private void OnManageButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var databaseConnectionManager = new DatabaseConnectionManager(_context);
                databaseConnectionManager.ShowDialog();
                DataContext = _context;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + "\n" + exception.StackTrace.ToString(),
                    "An error occurred  while managing database connections", MessageBoxButton.OK, MessageBoxImage.Error,
                    MessageBoxResult.OK);
            }
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _context.ProjectName = Settings.Default.ProjectName;
            _context.Namespace = Settings.Default.Namespace ?? "";
            _context.ProjectFile = Settings.Default.ProjectFile ?? "";
            var dbconnections = Settings.Default.DatabaseConnections;
            if (dbconnections==null || dbconnections.Count <= 0)
                return;
            foreach (
                var splitted in
                    dbconnections.Cast<string>()
                        .Select(connection => connection.Split(new[] {"|||||"}, StringSplitOptions.RemoveEmptyEntries))
                        .Where(splitted => splitted.Length > 1))
            {
                _context.DatabaseConnections.Add(new DatabaseConnection
                {
                    Name = splitted[0],
                    ConnectionString = splitted[1]
                });
            }
            _context.SelectedDatabaseConnection = _context.DatabaseConnections.First();
            DataContext = _context;
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Default.ProjectName = _context.ProjectName;
            Settings.Default.Namespace = _context.Namespace;
            Settings.Default.ProjectFile = _context.ProjectFile;

            if (_context.DatabaseConnections.Count <= 0) return;
            var list = new StringCollection();
            foreach (var connection in _context.DatabaseConnections)
                list.Add(string.Format("{0}|||||{1}", connection.Name, connection.ConnectionString));
            Settings.Default.DatabaseConnections = list;
            Settings.Default.Save();
        }


        private void OnBrowseButtonClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog {Filter = "Project File|*.csproj",Multiselect = false,Title = "Select project file"};
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult != true)
                return;
            var selected = openFileDialog.FileName;
            if (File.Exists(selected))
                _context.ProjectFile = selected;
            else
                MessageBox.Show("Selected file does not exists in the ", "File does not exists ", MessageBoxButton.OK,
                    MessageBoxImage.Error, MessageBoxResult.OK);
        }


    }
}
