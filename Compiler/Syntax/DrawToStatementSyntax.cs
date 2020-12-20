using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the syntax of drawto command
    /// </summary>
    public class DrawToStatementSyntax : StatementSyntax
    {
        /// <summary>
        /// Kind of the syntax.
        /// </summary>
        public override SyntaxKind Kind { get; }

        /// <summary>
        /// Point with x and y co-ordinate upto which a straight line is drawn
        /// from current point.
        /// </summary>
        public int [] Point {get; }
        /// <summary>
        /// Constructor that initializes the command's parameters.
        /// </summary>
        /// <param name="kind">Syntax Kind</param>
        /// <param name="point">array of integers denoting a coordinate.</param>
        public DrawToStatementSyntax(SyntaxKind kind, int[] point)
        {
            Kind = kind;
            Point = point;
        }
    }
}
