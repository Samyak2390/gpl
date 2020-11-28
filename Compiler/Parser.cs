using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace gpl.Compiler
{
    class Parser
    {
        private readonly ImmutableArray<SyntaxToken> _tokens;
        private SyntaxToken Current => Peek(0);
        private int _position;

        public Parser(string text, bool fromCli)
        {
            var lexer = new Lexer(text, fromCli);
            _tokens = lexer.GetTokens();
        }

        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[_tokens.Length - 1];

            return _tokens[index];
        }

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            if (Current.Kind == kind)
                return NextToken();

           //_diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);
            return new SyntaxToken(kind, Current.Position, null, null);
        }

        public CompilationUnitSyntax ParseCompilationUnit()
        {
            var statement = ParseStatement();
            var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
            return new CompilationUnitSyntax(statement, endOfFileToken);
        }

        private StatementSyntax ParseStatement()
        {
            switch (Current.Kind)
            {
                //case SyntaxKind.MoveToKeyword:
                default:
                    return ParseMoveToStatement();
                //default:
                    //return 
            }
        }

        private StatementSyntax ParseMoveToStatement()
        {
            var keyword = MatchToken(SyntaxKind.MoveToKeyword);
            var xCoordinate = ParseCoordinate();
            var yCoordinate = ParseCoordinate();

            return new MoveToStatementSyntax(keyword, xCoordinate, yCoordinate);
        }

        private ExpressionSyntax ParseCoordinate()
        {
            switch (Current.Kind)
            {
                //case SyntaxKind.NumberToken:
                default:
                    return ParseNumberLiteral();
            }
        }

        private ExpressionSyntax ParseNumberLiteral()
        {
            var numberToken = MatchToken(SyntaxKind.NumberToken);
            return new LiteralExpressionSyntax(numberToken);
        }
    }
}
