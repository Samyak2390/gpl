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
        /// <summary>
        /// syntax kind for invalid syntax
        /// </summary>
        BadSyntax,

        /// <summary>
        /// syntax kind for moveto statement
        /// </summary>
        MoveToStatement,

        /// <summary>
        /// syntax kind for drawto statement
        /// </summary>
        DrawToStatement,

        /// <summary>
        /// syntax kind for reset command
        /// </summary>
        ResetStatement,

        /// <summary>
        /// syntax kind for clear command
        /// </summary>
        ClearStatement,

        /// <summary>
        /// syntax kind for pen command
        /// </summary>
        PenStatement,

        /// <summary>
        /// syntax kind for brush command
        /// </summary>
        BrushStatement,

        /// <summary>
        /// syntax kind for fill command
        /// </summary>
        FillStatement,

        /// <summary>
        /// syntax kind for rect command
        /// </summary>
        RectangleStatement,

        /// <summary>
        /// syntax kind for run command
        /// </summary>
        RunStatement,

        /// <summary>
        /// syntax kind for circle command
        /// </summary>
        CircleStatement,

        /// <summary>
        /// syntax kind for triangle command
        /// </summary>
        TriangleStatement,

        /// <summary>
        /// syntax kind for variable initialization
        /// </summary>
        VariableExpression,

        /// <summary>
        /// syntax kind for if statement
        /// </summary>
        IfStatement,

        /// <summary>
        /// syntax kind for while statement
        /// </summary>
        WhileStatement,

        /// <summary>
        /// syntax kind for method declaration
        /// </summary>
        Method,

        /// <summary>
        /// syntax kind for method call
        /// </summary>
        MethodCall
    }
}
