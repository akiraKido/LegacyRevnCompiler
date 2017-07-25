using RevnCompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevnCompiler.Lexers;

namespace RevnConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokens = Lexer.GenerateTokens("10.0.1");
            Console.WriteLine(tokens.First().Value);
            Console.ReadLine();
        }
    }
}
