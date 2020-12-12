using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    class MethodCall : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public string MethodName { get; }
        public string[] Parameters { get; }

        public MethodCall(SyntaxKind kind, string methodName, string[] parameters)
        {
            Kind = kind;
            MethodName = methodName;
            Parameters = parameters;
        }
    }
}
