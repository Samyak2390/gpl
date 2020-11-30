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
        public RectangleShape(RectangleStatementSyntax rectangle, Graphics graphics)
        {
            _width = rectangle.Width;
            _height = rectangle.Height;
            _graphics = graphics;
        }

        public void Draw(Pen pen, int X, int Y)
        {
            //SolidBrush b = new SolidBrush(colour);
            //g.FillRectangle(b, x, y, width, height);
            _graphics.DrawRectangle(pen, X, Y, _width, _height);
        }
    }
}
