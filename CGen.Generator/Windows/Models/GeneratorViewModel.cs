using System.Collections.ObjectModel;

namespace CGen.Generator.Windows.Models
{
    public class GeneratorViewModel : ModelBase
    {
        private string _namespace;
        private string _projectName;
        private ObservableCollection<DatabaseConnection> _databaseConnections;
        private DatabaseConnection _selectedDatabaseConnection;
        private string _projectFile;

        public string Namespace { get { return _namespace; } set { _namespace = value; Changed("Namespace"); } }
        public string ProjectName { get { return _projectName; } set { _projectName = value; Changed("ProjectName"); } }
        public ObservableCollection<DatabaseConnection> DatabaseConnections { get { return _databaseConnections; } set {_databaseConnections = value; Changed("DatabaseConnections");} }
        public DatabaseConnection SelectedDatabaseConnection { get { return _selectedDatabaseConnection; } set { _selectedDatabaseConnection = value; Changed("SelectedDatabaseConnection");} } 
        public string ProjectFile { get { return _projectFile; } set { _projectFile = value; Changed("ProjectFile"); } }
    }
}
