using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    class Lexer
    {
        private readonly string _text;
        private int _position;
        private int _start;
        private SyntaxKind _kind;
        private object _value;

        public Lexer(string text, bool fromCli)
        {
            if(fromCli)
            {
                string[] lines = text.Split('\n');
                _text = lines[lines.Length - 2];
            }
            else
            {
                _text = text;
            }
        }

        private char Current
        {
            get
            {
                if (_position >= _text.Length) return '\0';
                return _text[_position];
            }
        }

        private SyntaxToken Lex()
        {
            try
            {
                _start = _position;
                _kind = SyntaxKind.BadToken;
                _value = null;

                switch (Current)
                {
                    case '\0':
                        _kind = SyntaxKind.EndOfFileToken;
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        ReadNumber();
                        break;
                    case ' ':
                    case '\t':
                    case '\n':
                    case '\r':
                        ReadWhiteSpace();
                        break;
                    default:
                        if (char.IsLetter(Current)) ReadIdentifierOrKeyword();
                        else if (char.IsWhiteSpace(Current)) ReadWhiteSpace();
                        else
                        {
                            _position++;
                            throw new NotSupportedException();
                        }
                        break;
                }
            }catch(Exception e)
            {
                var E = e;
            }
            var length = _position - _start;
            var text = _text.Substring(_start, length);

            return new SyntaxToken(_kind, _start, text, _value);
        }

        private void ReadNumber()
        {
            try
            {
                while (char.IsDigit(Current)) _position++;

                var length = _position - _start;
                var text = _text.Substring(_start, length);
                if (!int.TryParse(text, out var value))
                    throw new Exception();

                _value = value;
                _kind = SyntaxKind.NumberToken;
            }catch(Exception e)
            {
                var E = e;
            }
        }

        private void ReadWhiteSpace()
        {
            while (char.IsWhiteSpace(Current)) _position++;
            _kind = SyntaxKind.WhiteSpaceToken;
        }

        private void ReadIdentifierOrKeyword()
        {
            while (char.IsLetter(Current)) _position++;

            var length = _position - _start;
            var text = _text.Substring(_start, length);
            _kind = GetKeyWordKind(text);
        }

        private SyntaxKind GetKeyWordKind(string text)
        {
            var tempText = text.ToLower();
            switch (tempText)
            {
                case "moveto":
                    return SyntaxKind.MoveToKeyword;
                case "drawto":
                    return SyntaxKind.DrawToKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }

        public ImmutableArray<SyntaxToken>  GetTokens()
        {
            var tokens = new List<SyntaxToken>();
            SyntaxToken token;

            do
            {
                token = Lex();

                if(token.Kind != SyntaxKind.WhiteSpaceToken &&
                    token.Kind != SyntaxKind.BadToken)
                {
                    tokens.Add(token);
                }

            } while (token.Kind != SyntaxKind.EndOfFileToken);

            return tokens.ToImmutableArray();
        }
    }
}
