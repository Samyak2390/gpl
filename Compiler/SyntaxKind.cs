using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    public enum SyntaxKind
    {
        BadToken,
        NumberToken,
        EndOfFileToken,
        WhiteSpaceToken,
        IdentifierToken,

        //KeyWords
        MoveToKeyword,
        DrawToKeyword,

        //Expressions
        PointExpression,
        MoveToExpression,
        DrawToExpression
    }
}
