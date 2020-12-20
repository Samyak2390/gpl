using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class that represents all the syntax that are not valid.
    /// </summary>
    public class BadSyntax : StatementSyntax
    {
        /// <summary>
        /// Kind of the syntax
        /// </summary>
        public override SyntaxKind Kind { get; }

        /// <summary>
        /// Construtor to initialize the Syntax Kind.
        /// </summary>
        public BadSyntax()
        {
            Kind = SyntaxKind.BadSyntax;
        }
    }
}
