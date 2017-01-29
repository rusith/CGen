using System.IO;

namespace CGen.Models
{
    public class NGenSettings
    {
        public string ProjectName { get; set; }
        public string ProjectNamespace { get; set; }
        public DirectoryInfo RootDirectory { get; set; }
        public string ConnectionString { get; set; }
    }
}
