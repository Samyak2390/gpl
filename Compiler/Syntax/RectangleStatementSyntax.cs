using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    public class RectangleStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public int Width { get; }
        public int Height { get; }


        public RectangleStatementSyntax(SyntaxKind kind, int[] size)
        {
            Kind = kind;
            Width = size[0];
            Height = size[1];
        }
    }
}
