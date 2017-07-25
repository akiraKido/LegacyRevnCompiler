using System;
using System.Collections.Generic;
using System.Linq;
using RevnCompiler.Lexers;

namespace RevnCompiler.Parsers
{

    internal class TokenReader
    {
        private readonly List<Token> _tokens;
        private int _index = -1;
        
        internal TokenReader(IEnumerable<Token> tokens)
        {
            _tokens = tokens.ToList();
        }

        internal bool HasNext() => _index < _tokens.Count;

        internal void MoveNext()
        {
            if (HasNext()) {_index++;}
        }

        internal Token Current => HasNext() ? _tokens[_index] : null;

        internal IEnumerable<Token> TakeWhile(Func<Token, bool> predicate)
        {
            int startPos = _index;
            while (Current != null && predicate(Current))
            {
                MoveNext();
            }
            return _tokens.Skip(startPos).Take(_index - startPos);
        }
    }

}