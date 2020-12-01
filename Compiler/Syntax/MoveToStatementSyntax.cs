using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the move to command.
    /// </summary>
    public class MoveToStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public int [] Point {get; }

        /// <summary>
        /// Constructor initializing the command's parameters.
        /// </summary>
        /// <param name="kind">Syntax kind</param>
        /// <param name="point">array of two integers acting as a coordinate.</param>
        public MoveToStatementSyntax(SyntaxKind kind, int[] point)
        {
            Kind = kind;
            Point = point;
        }
    }
}
