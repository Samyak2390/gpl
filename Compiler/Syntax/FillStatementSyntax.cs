using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    class FillStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind {get;}
        public bool State { get; }

        public FillStatementSyntax(SyntaxKind kind, string state)
        {
            Kind = kind;
            State = state.Equals("on") ? true : false;
        }
    }
}
