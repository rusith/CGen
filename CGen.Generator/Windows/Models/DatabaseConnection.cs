namespace CGen.Generator.Windows.Models
{
    /// <summary>
    /// Represents a database connection
    /// </summary>
    public class DatabaseConnection: ModelBase
    {
        private string _name;
        private string _connectionString;

        public string Name { get { return _name; } set { _name = value; Changed("Name"); } }
        public string ConnectionString { get { return _connectionString; }  set { _connectionString = value; Changed("ConnectionString"); } } 
    }
}
