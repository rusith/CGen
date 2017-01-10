using System.Text;
using CGen.Mobels;

namespace CGen.Base
{
    public class Generator
    {
        public static void Generate(NGenSettings settings)
        {
            var fileContent = new StringBuilder();
            var templates = Templates.All;
            var dalTemplate = templates["DAL"];

            //Core
            var iconnectionManager = templates["ICONNECTIONMANAGER"];
            var iconnectionContainer = templates["ICONNECTIONCONTAINER"];
            var idbContext = templates["IDBCONTEXT"];

            dalTemplate.Set("iConnectionManager", iconnectionManager.Build())
                .Set("iConnctionContainer",iconnectionContainer.Build())
                .Set("iDbContext",idbContext.Build());
        }
    }
}
