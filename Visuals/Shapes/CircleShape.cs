using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using gpl.Compiler.Syntax;

namespace gpl.Visuals.Shapes
{
    class CircleShape : Shape
    {
        private int _radius;
        private Graphics _graphics;
        private bool _fillState;
        public CircleShape(CircleStatementSyntax circle, Graphics graphics, bool state)
        {
            _radius = circle.Radius;
            _graphics = graphics;
            _fillState = state;
        }

        public void Draw(Pen pen, SolidBrush brush, int X, int Y)
        {
            if (_fillState)
            {
                _graphics.FillEllipse(brush, X, Y, _radius, _radius);
            }
            else
            {
                _graphics.DrawEllipse(pen, X, Y, _radius, _radius);
            }
        }
    }
}
