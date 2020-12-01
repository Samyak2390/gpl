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
        public override SyntaxKind Kind { get; }
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
