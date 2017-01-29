
using CGen.Models;

namespace CGen.Templates
{
    public partial class DALTemplate
    {
        private CGenSettings _settings ;

        public DALTemplate(CGenSettings settings)
        {
            _settings = settings;
        }
    }
}
