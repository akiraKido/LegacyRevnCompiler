using System;
using RevnCompiler.Lexers.Utils;

namespace RevnCompiler.Lexers
{

    public abstract class Token
    {
        public object Value { get; }
        public int LineNumber { get; }
        public int EndPosition { get; }

        internal Token(CharReader reader, object value)
        {
            this.Value = value;
            this.LineNumber = reader.CurrentLine;
            this.EndPosition = reader.CurrentPosition;
        }
    }

    internal enum LiteralType
    {
        StringLiteral
        , Number
    }

    internal class LiteralToken : Token
    {
        internal LiteralType LiteralType { get; }

        internal LiteralToken(CharReader reader, string value, LiteralType literalType)
            : base(reader, value)
        {
            LiteralType = literalType;

            if (literalType == LiteralType.Number)
            {
                if (!float.TryParse(value, out var _))
                {
                    throw new Exception($"[{LineNumber}:{EndPosition}] Not a valid number: {value}");
                }
            }
        }
    }

    public class Identifier : Token
    {
        private static readonly string[] ReservedIdentifiers = {"namespace", "class", "fun", "static"};

        public bool IsReserved { get; }

        internal Identifier(CharReader reader, string identifier) : base(reader, identifier)
        {
            if (identifier.IsContainedIn(ReservedIdentifiers))
            {
                IsReserved = true;
            }
        }
    }

    internal class Operand : Token
    {
        public Operand(CharReader reader, object value) : base(reader, value) { }
    }

    internal class Block : Token
    {
        internal bool IsStart { get; }

        internal Block(CharReader reader, string value) : base(reader, value)
        {
            if (value == ":")
            {
                IsStart = true;
            }
        }
    }

}