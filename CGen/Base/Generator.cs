using System.Text;
using CGen.Models;

namespace CGen.Base
{
    public class Generator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public static void Generate(NGenSettings settings)
        {
            
            var fileContent = new StringBuilder();
            var templates = Templates.All(settings);
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
