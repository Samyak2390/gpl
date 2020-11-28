using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    public class MoveToStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind => SyntaxKind.MoveToStatement;
        public SyntaxToken Keyword { get; }
        public ExpressionSyntax X { get; }
        public ExpressionSyntax Y { get; }

        public MoveToStatementSyntax(SyntaxToken keyword, ExpressionSyntax xCoordinate, ExpressionSyntax yCoordinate)
        {
            Keyword = keyword;
            X = xCoordinate;
            Y = yCoordinate;
        }

    }
}
