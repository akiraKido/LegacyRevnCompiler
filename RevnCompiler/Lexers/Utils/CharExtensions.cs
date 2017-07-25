using System.Collections.Generic;
using System.Linq;

namespace RevnCompiler.Lexers.Utils
{

    internal static class CharExtensions
    {
        internal static bool IsNumber(this char c)
        {
            return char.IsNumber(c);
        }

        internal static bool IsLetter(this char c)
        {
            return char.IsLetter(c);
        }

        internal static bool IsLetterOrDigit(this char c)
        {
            return char.IsLetterOrDigit(c);
        }

        internal static bool IsWhiteSpace(this char c)
        {
            return char.IsWhiteSpace(c);
        }

        internal static bool IsContainedIn(this char c, IEnumerable<char> list)
        {
            return list.Contains(c);
        }
        
        internal static bool IsContainedIn(this string c, IEnumerable<string> list)
        {
            return list.Contains(c);
        }
    }

}