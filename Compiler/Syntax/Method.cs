using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the method
    /// </summary>
    public class Method : StatementSyntax
    {
        /// <summary>
        /// Type of syntax
        /// </summary>
        public override SyntaxKind Kind { get; }

        /// <summary>
        /// Name of the method.
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Commands inside the method block.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// Parameter variables for Method.
        /// </summary>
        public string[] Parameters { get; }

        /// <summary>
        /// Constructor that initializes default properties of Method.
        /// </summary>
        /// <param name="kind">Kind of the syntax.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="body">Commands between the method block</param>
        /// <param name="parameters">Parameter variables for method</param>
        public Method(SyntaxKind kind, string methodName, string body, string[] parameters)
        {
            Kind = kind;
            MethodName = methodName;
            Body = body;
            Parameters = parameters;
        }
    }
}
