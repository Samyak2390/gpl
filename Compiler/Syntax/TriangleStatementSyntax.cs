using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gpl.Compiler.Syntax
{
    class TriangleStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public Point[] Vertices { get; }

        public TriangleStatementSyntax(SyntaxKind kind, Point[] vertices)
        {
            Kind = kind;
            Vertices = vertices;
        }
    }
}
