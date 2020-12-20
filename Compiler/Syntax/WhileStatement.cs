using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    /// <summary>
    /// Class representing the syntax of while statement.
    /// </summary>
    public class WhileStatement : StatementSyntax
    {
        /// <summary>
        /// Kind of the syntax.
        /// </summary>
        public override SyntaxKind Kind { get; }

        /// <summary>
        /// Condition required to execute the while statement.
        /// </summary>
        public string[] Condition { get; }

        /// <summary>
        /// Left operand in the condition.
        /// </summary>
        public string Variable1 { get; }

        /// <summary>
        /// The comparison operator.
        /// </summary>
        public string CompareOperator { get; }

        /// <summary>
        /// Right operand of the condition.
        /// </summary>
        public string Variable2 { get; }

        /// <summary>
        /// Command inside the while block.
        /// </summary>
        public List<string[]> Body { get; }

        /// <summary>
        /// Method that returns true/false after checking the condition.
        /// </summary>
        /// <param name="var1">left operand integer.</param>
        /// <param name="var2">right operand integer.</param>
        /// <returns></returns>
        public bool? Run(int var1, int var2)
        {
           
            switch (CompareOperator)
            {
                case "<":
                    return var1 < var2;
                case ">":
                    return var1 > var2;
                case "!=":
                    return var1 != var2;
                case "==":
                    return var1 == var2;
                case "<=":
                    return var1 <= var2;
                case ">=":
                    return var1 >= var2;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Constructor that initializes the properties for this class.
        /// </summary>
        /// <param name="kind">kind of statement.</param>
        /// <param name="condition">Condition of while statement.</param>
        /// <param name="body">Commands between the while block.</param>
        /// <param name="var1">Left integer operand.</param>
        /// <param name="compareOperator">Comparison operator.</param>
        /// <param name="var2">Right integer operand.</param>
        public WhileStatement(SyntaxKind kind, string[] condition, List<string[]> body, string var1, string compareOperator, string var2)
        {
            Kind = kind;
            Condition = condition;
            Body = body;
            Variable1 = var1;
            Variable2 = var2;
            CompareOperator = compareOperator;
        }
    }
}
