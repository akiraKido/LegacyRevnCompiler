using System;
using System.Collections.Generic;
using RevnCompiler.Lexers;
using RevnCompiler.Parsers.Asts;

namespace RevnCompiler.Parsers
{

    public class Parser
    {
        public static string ParseCode(IEnumerable<Token> tokens)
        {
            var reader = new TokenReader(tokens);

            Ast finalAst = null;
            
            while (reader.HasNext())
            {
                reader.MoveNext();

                if ((string) reader.Current.Value == "func")
                {
                    finalAst = reader.ParseOutFunction();
                }
            }

            return finalAst?.GenerateIl();
        }

    }

    internal static class TokenReaderExtensions
    {
        internal static string ParseOutIdentifier(this TokenReader reader)
        {
            if (!( reader.Current is Identifier ))
            {
                // TODO: throw
            }
            var result = reader.Current;
            reader.MoveNext(); // identifier を消費
            return result.Value.ToString();
        }
        
        internal static FunctionAst ParseOutFunction(this TokenReader reader)
        {
            if ((string) reader.Current.Value == "func")
            {
                reader.MoveNext(); // func を消費
            }
            var name = reader.ParseOutIdentifier();
            var expressions = reader.ParseOutExpressions();
            return new FunctionAst(name, expressions);
        }
        
        internal static IEnumerable<Ast> ParseOutExpressions(this TokenReader reader)
        {
            // TODO
            throw new NotImplementedException();
        }
    }

}