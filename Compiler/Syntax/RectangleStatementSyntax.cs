using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the rectangle command.
    /// </summary>
    public class RectangleStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public int Width { get; }
        public int Height { get; }

        /// <summary>
        /// Constructor initializing the rectangle command's parameters.
        /// </summary>
        /// <param name="kind">Syntax Kind</param>
        /// <param name="size">Size (Width and Height) integer array.</param>
        public RectangleStatementSyntax(SyntaxKind kind, int[] size)
        {
            Kind = kind;
            Width = size[0];
            Height = size[1];
        }
    }
}
