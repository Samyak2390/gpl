using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing If statement.
    /// </summary>
    public class IfStatementSyntax : StatementSyntax
    {
        /// <summary>
        /// Kind of syntax
        /// </summary>
        public override SyntaxKind Kind { get; }

        /// <summary>
        /// Array containing the condition of if statement.
        /// </summary>
        public string[] Condition { get; }

        /// <summary>
        /// Array of strings containing commands between if block.
        /// </summary>
        public List<string[]> Body { get; }

        /// <summary>
        /// Line number of if statement
        /// </summary>
        public int LineNum;

        /// <summary>
        /// Property that returns if condition is true or false.
        /// </summary>
        public bool? Run 
        {
            get
            {
                switch (Condition[1])
                {
                    case "<":
                        return Convert.ToInt32(Condition[0]) < Convert.ToInt32(Condition[2]);
                    case ">":
                        return Convert.ToInt32(Condition[0]) > Convert.ToInt32(Condition[2]);
                    case "!=":
                        return Convert.ToInt32(Condition[0]) != Convert.ToInt32(Condition[2]);
                    case "==":
                        return Convert.ToInt32(Condition[0]) == Convert.ToInt32(Condition[2]);
                    case "<=":
                        return Convert.ToInt32(Condition[0]) <= Convert.ToInt32(Condition[2]);
                    case ">=":
                        return Convert.ToInt32(Condition[0]) >= Convert.ToInt32(Condition[2]);
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Constructor that initializes the default properties of If statement.
        /// </summary>
        /// <param name="kind">Kind of syntax.</param>
        /// <param name="condition">Condition of If statement.</param>
        /// <param name="body">Commands between if block</param>
        /// <param name="lineNum">Line number of if statement.</param>
        public IfStatementSyntax(SyntaxKind kind, string[] condition, List<string[]> body, int lineNum)
        {
            Kind = kind;
            Condition = condition;
            Body = body;
            LineNum = lineNum;
        }   
    }
}
