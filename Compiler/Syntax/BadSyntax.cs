using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    public class BadSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }

        public BadSyntax()
        {
            Kind = SyntaxKind.BadSyntax;
        }
    }
}
