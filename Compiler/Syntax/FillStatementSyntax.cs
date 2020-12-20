using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the syntax of fill command.
    /// </summary>
    class FillStatementSyntax : StatementSyntax
    {
        /// <summary>
        /// Kind of the Syntax
        /// </summary>
        public override SyntaxKind Kind {get;}

        /// <summary>
        /// On/Off state of fill.
        /// </summary>
        public bool State { get; }

        /// <summary>
        /// Constructor initializing the command's parameters.
        /// </summary>
        /// <param name="kind">Syntax kind.</param>
        /// <param name="state">On or Off state of Fill command</param>
        public FillStatementSyntax(SyntaxKind kind, string state)
        {
            Kind = kind;
            State = state.Equals("on") ? true : false;
        }
    }
}
