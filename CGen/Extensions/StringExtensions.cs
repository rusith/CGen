using System.Linq;

namespace CGen.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharLower(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            var firstChar = str.First();
            return (firstChar + "").ToLower() +str.Substring(1);
        }
    }
}
