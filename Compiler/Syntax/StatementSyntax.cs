using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Abstract class representing all syntax commands with common property Kind.
    /// </summary>
    public abstract class StatementSyntax
    {
        public abstract SyntaxKind Kind { get; }
    }
}
