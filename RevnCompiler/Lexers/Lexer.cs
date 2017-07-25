using System.Collections.Generic;
using RevnCompiler.Lexers.Utils;

namespace RevnCompiler.Lexers
{

    public class Lexer
    {
        
        private static readonly char[] Operands = {'+', '-', '/', '*', '='};

        public static IEnumerable<Token> GenerateTokens(string input)
        {
            var reader = new CharReader(input);

            while (reader.HasNext())
            {
                reader.SkipWhiteSpace();
                
                switch (reader.Current)
                {
                    // 文字列
                    case '"':
                        reader.MoveNext(); // " を消費
                        string result = reader.TakeWhile(c => c != '"');
                        reader.MoveNext(); // " を消費
                        yield return new LiteralToken(reader, result, LiteralType.StringLiteral);
                        continue;
                    case ':':
                        yield return new Block(reader, reader.Current.ToString());
                        reader.MoveNext(); // : を消費
                        continue;
                    case '/':
                        if (reader.Next == '/')
                        {
                            reader.ProceedWhile(c => c != '\n');
                        }
                        continue;
                }
                
                // オペランド
                if (reader.Current.IsContainedIn(Operands))
                {
                    yield return new Operand(reader, reader.Current.ToString());
                    reader.MoveNext(); // オペランドを消費
                    continue;
                }

                // 数字
                if (reader.Current.IsNumber())
                {
                    yield return new LiteralToken(
                        reader,
                        reader.TakeWhile(c => c.IsNumber() || c == '.'), 
                        LiteralType.Number);
                    continue;
                }

                // 変数等の文字
                if (reader.Current.IsLetter())
                {
                    string value = reader.TakeWhile(c => c.IsLetterOrDigit());
                    yield return value == "end"
                        ? (Token) new Block(reader, value) 
                        : new Identifier(reader, value);
                    continue;
                }
            }
        }

    }

}