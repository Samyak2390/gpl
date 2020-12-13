using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler.Syntax
{
    class IfStatementSyntax : StatementSyntax
    {
        public override SyntaxKind Kind { get; }
        public string[] Condition { get; }
        public List<string[]> Body { get; }
        public int LineNum;
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

        public IfStatementSyntax(SyntaxKind kind, string[] condition, List<string[]> body, int lineNum)
        {
            Kind = kind;
            Condition = condition;
            Body = body;
            LineNum = lineNum;
        }   
    }
}
