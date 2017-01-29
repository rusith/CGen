using System.IO;

namespace CGen.Models
{
    public class CGenSettings
    {
        public string ProjectName { get; set; }
        public string ProjectNamespace { get; set; }
        public string PathToProjectFolder { get; set; }
        public string ConnectionString { get; set; }
    }
}
