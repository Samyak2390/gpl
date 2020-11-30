using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gpl.Compiler.Syntax;
using System.Drawing;

namespace gpl.Visuals.Shapes
{
    public class RectangleShape : Shape
    {
        private int _width;
        private int _height;
        private Graphics _graphics;
        private bool _fillState;
        public RectangleShape(RectangleStatementSyntax rectangle, Graphics graphics, bool state)
        {
            _width = rectangle.Width;
            _height = rectangle.Height;
            _graphics = graphics;
            _fillState = state;
        }

        public void Draw(Pen pen, SolidBrush brush, int X, int Y)
        {
            if (_fillState)
            {
                _graphics.FillRectangle(brush, X, Y, _width, _height);
            }
            else
            {
                _graphics.DrawRectangle(pen, X, Y, _width, _height);
            }
        }
    }
}
