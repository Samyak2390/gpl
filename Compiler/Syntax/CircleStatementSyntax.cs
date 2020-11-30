using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    public class CircleStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public int Radius { get; }
        public CircleStatementSyntax(SyntaxKind kind, int radius)
        {
            Kind = kind;
            Radius = radius;
        }
    }
}
