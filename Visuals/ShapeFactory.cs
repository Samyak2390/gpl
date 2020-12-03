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
    /// <summary>
    /// Factor class for Shape that returns the Shape object depending upon
    /// type of Syntax
    /// </summary>
    public class ShapeFactory
    {
        public static Shape GetShape(StatementSyntax statement, Graphics graphics, bool fillState)
        {
            switch (statement)
            {
                case RectangleStatementSyntax rectangle:
                    return new RectangleShape( (RectangleStatementSyntax)statement, graphics, fillState);

                case CircleStatementSyntax circle:
                    return new CircleShape((CircleStatementSyntax)statement, graphics, fillState);

                default:
                    return new TriangleShape((TriangleStatementSyntax)statement, graphics, fillState);
            }
        }
    }
}
