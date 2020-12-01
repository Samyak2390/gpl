using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using gpl.Compiler.Syntax;

namespace gpl.Visuals.Shapes
{
    class TriangleShape: Shape
    {
        private Point[] _vertices;
        private Graphics _graphics;
        private bool _fillState;
        public TriangleShape(TriangleStatementSyntax triangle, Graphics graphics, bool state)
        {
            _vertices = triangle.Vertices;
            _graphics = graphics;
            _fillState = state;
        }

        public override void Draw(Pen pen, SolidBrush brush, int X, int Y)
        {
            if (_fillState)
            {
                _graphics.FillPolygon(brush, _vertices);
            }
            else
            {
                _graphics.DrawPolygon(pen, _vertices);
            }
        }
    }
}
