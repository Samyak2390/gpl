using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    public enum SyntaxKind
    {
        BadSyntax,
        MoveToStatement,
        DrawToStatement,
        ResetStatement,
        ClearStatement,
        PenStatement,
        BrushStatement,
        FillStatement,
        RectangleStatement,
        RunStatement,
        CircleStatement,
        TriangleStatement
    }
}
