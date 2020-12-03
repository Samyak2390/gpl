using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the syntax of triangle command.
    /// </summary>
    class TriangleStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public Point[] Vertices { get; }

        /// <summary>
        /// Constructor initializing triangle command's parameters
        /// </summary>
        /// <param name="kind">Syntax kind</param>
        /// <param name="vertices">Array of three vertices as Point</param>
        public TriangleStatementSyntax(SyntaxKind kind, Point[] vertices)
        {
            Kind = kind;
            Vertices = vertices;
        }
    }
}
