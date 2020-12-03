using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gpl.Compiler.Syntax;
using System.Drawing;

namespace gpl.Visuals.Shapes
{
    /// <summary>
    /// Class representing the rectangle shape.
    /// </summary>
    public class RectangleShape : Shape
    {
        private int _width;
        private int _height;
        private Graphics _graphics;
        private bool _fillState;

        /// <summary>
        /// Constructor initializing the required parameters for drawing the rectangle.
        /// </summary>
        /// <param name="rectangle">Object of rectangle syntax</param>
        /// <param name="graphics">Graphics object used to draw rectangle</param>
        /// <param name="state">Fill state</param>
        public RectangleShape(RectangleStatementSyntax rectangle, Graphics graphics, bool state)
        {
            _width = rectangle.Width;
            _height = rectangle.Height;
            _graphics = graphics;
            _fillState = state;
        }

        /// <summary>
        /// Method used to draw rectangle.
        /// </summary>
        /// <param name="pen">Pen Object</param>
        /// <param name="brush">SolidBrush object</param>
        /// <param name="X">X-Coordinate</param>
        /// <param name="Y">Y-Coordinate</param>
        public override void Draw(Pen pen, SolidBrush brush, int X, int Y)
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
