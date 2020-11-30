using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gpl.Compiler.Syntax;
using gpl.Visuals.Shapes;
using System.Drawing;

namespace gpl.Visuals
{
    public class ShapeFactory
    {
        public static Shape GetShape(StatementSyntax statement, Graphics graphics)
        {
            switch (statement)
            {
                //case RectangleStatementSyntax rectangle:
                default:
                    return new RectangleShape( (RectangleStatementSyntax)statement, graphics);
            }
        }
    }
}
