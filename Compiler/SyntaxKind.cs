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

        //Statements
        MoveToStatement,
        DrawToExpression,

        //Nodes
        CompilationUnit,

        //Expressions
        LiteralExpression,
    }
}
