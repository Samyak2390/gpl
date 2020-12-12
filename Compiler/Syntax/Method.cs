using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    class Method : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public string MethodName { get; }
        public string Body { get; }
        public string[] Parameters { get; }

        public Method(SyntaxKind kind, string methodName, string body, string[] parameters)
        {
            Kind = kind;
            MethodName = methodName;
            Body = body;
            Parameters = parameters;
        }
    }
}
