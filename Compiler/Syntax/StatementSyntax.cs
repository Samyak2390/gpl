using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    public abstract class StatementSyntax
    {
        public abstract SyntaxKind Kind { get; }
    }
}
