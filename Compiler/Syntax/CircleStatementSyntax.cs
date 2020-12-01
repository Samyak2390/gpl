using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the syntax of circle command.
    /// </summary>
    public class CircleStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public int Radius { get; }

        /// <summary>
        /// Constructor initializing the kind of syntax and initializing its parameters.
        /// </summary>
        /// <param name="kind">Kind of the syntax</param>
        /// <param name="radius">Size of circle to be drawn.</param>
        public CircleStatementSyntax(SyntaxKind kind, int radius)
        {
            Kind = kind;
            Radius = radius;
        }
    }
}
