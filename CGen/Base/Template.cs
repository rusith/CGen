using System.Text;

namespace CGen.Base
{
    public class Template
    {
        private readonly StringBuilder _builder;

        public Template(StringBuilder str)
        {
            _builder = str;
        }

        public Template(Template template)
        {
            _builder = new StringBuilder(template._builder.ToString());
        }

        /// <summary>
        /// Set a variable value of the template
        /// </summary>
        /// <param name="name">value name to set</param>
        /// <param name="replacement">value to set</param>
        /// <returns></returns>
        public Template Set(string name, string replacement)
        {
            _builder.Replace("$" + name + "$", replacement);
            return this;
        }

        public string Build()
        {
            var str = _builder.ToString();
            return str;
        }
    }
}
