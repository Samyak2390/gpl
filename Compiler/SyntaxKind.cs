using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    /// <summary>
    /// Enum type enumerating the type of available commands.
    /// </summary>
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
        TriangleStatement,
        VariableExpression,
        IfStatement,
        WhileStatement,
        Method,
        MethodCall
    }
}
