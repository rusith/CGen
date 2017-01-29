using System;
using System.IO;
using CGen.Models;
using CGen.Templates;

namespace CGen.Base
{
    public class Generator
    {
        /// <summary>
        /// Generate code using given settings
        /// </summary>
        /// <param name="settings"></param>
        public static void Generate(CGenSettings settings)
        {
            if(!Directory.Exists(settings.PathToProjectFolder))
                throw new Exception("Folder does not exists");

            var template = new DALTemplate(settings);
            var dal = template.TransformText();
            File.WriteAllText(Path.Combine(settings.PathToProjectFolder,"DAL.cs"),dal);
        }
    }
}
