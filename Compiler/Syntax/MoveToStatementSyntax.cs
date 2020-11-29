using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    public class MoveToStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public int [] Point {get; }

        public MoveToStatementSyntax(SyntaxKind kind, int[] point)
        {
            Kind = kind;
            Point = point;
        }
    }
}
