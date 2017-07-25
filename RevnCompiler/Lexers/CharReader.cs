using System;
using System.Linq;
using RevnCompiler.Lexers.Utils;

namespace RevnCompiler.Lexers
{

    internal class CharReader
    {
        internal int CurrentLine
        {
            get
            {
                string substring = _input.Substring(0, _index);
                return substring.Split('\n').Length;
            }
        }

        internal int CurrentPosition
        {
            get
            {
                string substring = _input.Substring(0, _index);
                return substring.Split('\n').Last().Length;
            }
        }
        #endif

        private readonly string _input;
        private int _index;

        internal CharReader(string input)
        {
            _input = input;
        }

        internal bool HasNext() => _index < _input.Length;

        internal void MoveNext()
        {
            if (HasNext()) _index++;
        }

        internal char Next => HasNext() ? _input[_index + 1] : '\0';

        internal char Current => HasNext() ? _input[_index] : '\0';

        internal string Take(int begin, int end)
        {
            int length = end - begin;
            return _input.Substring(begin, length);
        }

        internal void ProceedWhile(Func<char, bool> predicate)
        {
            while (predicate(Current) && HasNext())
            {
                MoveNext();
            }
        }

        internal string TakeWhile(Func<char, bool> predicate)
        {
            int startIndex = _index;
            while (predicate(Current) && HasNext())
            {
                MoveNext();
            }
            int endIndex = _index;
            return Take(startIndex, endIndex);
        }

        internal void SkipWhiteSpace()
        {
            while (Current.IsWhiteSpace() && HasNext())
            {
                MoveNext();
            }
        }
    }

}