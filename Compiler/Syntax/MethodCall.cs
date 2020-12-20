using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the syntax of method call.
    /// </summary>
    public class MethodCall : StatementSyntax
    {
        /// <summary>
        /// Kind of the syntax.
        /// </summary>
        public override SyntaxKind Kind { get; }

        /// <summary>
        /// Name of the method.
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Integer parameters of the method.
        /// </summary>
        public string[] Parameters { get; }


        /// <summary>
        /// Constructor that initializes the default properties for the class.
        /// </summary>
        /// <param name="kind">Kind of the syntax.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">Integer parameters for the method.</param>
        public MethodCall(SyntaxKind kind, string methodName, string[] parameters)
        {
            Kind = kind;
            MethodName = methodName;
            Parameters = parameters;
        }
    }
}
