using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    class WhileStatement : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public string[] Condition { get; }
        public string Variable1 { get; }
        public string CompareOperator { get; }
        public string Variable2 { get; }
        public List<string[]> Body { get; }
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
