using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CGen.Models;
using CGen.Properties;

namespace CGen.Base
{
    public class Templates
    {
        private static Templates _object = null;
        private Dictionary<string, Template>  _templates;
             
        private Templates(NGenSettings settings)
        {
            ReadTemplate(settings);   
        }

        public static Templates All(NGenSettings settings)
        {
             return _object ?? (_object = new Templates(settings)); 
        } 

        private void ReadTemplate(NGenSettings settings)
        {
            var lines = Resources.templates.Split(new [] { Environment.NewLine },StringSplitOptions.None).ToList();
            if(lines.Count<1)
                return;

            _templates = new Dictionary<string, Template>();

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                if(line.Length<=2 || !line.StartsWith("##"))
                    continue;

                var currentTemplateName = line.Substring(2);
                var tabCount = 0;
                if (currentTemplateName.Contains("$"))
                {
                    var index = currentTemplateName.IndexOf("$", StringComparison.Ordinal)+1;
                    tabCount = int.Parse(currentTemplateName.Substring(index));
                }
                var currentTemplateBuilder = new StringBuilder();
                string tabs = null;
                i++;
                while (i<(lines.Count-1))
                {
                    line = lines[i];
                    if (line == "###")
                        break;
                    
                    if (tabCount > 0)
                        currentTemplateBuilder.Append(tabs == null ? (tabs = CreateTabs(tabCount)) : tabs);
                    currentTemplateBuilder.AppendLine(line);
                    i++;
                }

                currentTemplateBuilder.Replace("$projectNs$", settings.ProjectNamespace);
                currentTemplateBuilder.Replace("$projectName$", settings.ProjectName);

                _templates.Add(currentTemplateName,new Template(currentTemplateBuilder));
            }
        }

        /// <summary>
        /// Get the template with the given name 
        /// </summary>
        /// <param name="name">name of the template </param>
        /// <returns></returns>
        public Template this[string name] => new Template(_templates[name]);

        private static string CreateTabs(int count)
        {
            if (count < 1)
                return "";
            var builder = new StringBuilder();
            for (var i = 0; i < count; i++)
                builder.Append("\t");
            return builder.ToString();
        }
    }
}
