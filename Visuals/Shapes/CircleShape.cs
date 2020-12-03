using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using gpl.Compiler.Syntax;

namespace gpl.Visuals.Shapes
{
    /// <summary>
    /// Class representing the circle shape.
    /// </summary>
    class CircleShape : Shape
    {
        private int _radius;
        private Graphics _graphics;
        private bool _fillState;

        /// <summary>
        /// Constructor to initialize circle command's parameters.
        /// </summary>
        /// <param name="circle">Syntax object of circle</param>
        /// <param name="graphics">Graphics object used to draw</param>
        /// <param name="state">Fill state</param>
        public CircleShape(CircleStatementSyntax circle, Graphics graphics, bool state)
        {
            _radius = circle.Radius;
            _graphics = graphics;
            _fillState = state;
        }

        /// <summary>
        /// Method that draws the circle shape.
        /// </summary>
        /// <param name="pen">Pen object</param>
        /// <param name="brush">Brush Object</param>
        /// <param name="X">X coordinate</param>
        /// <param name="Y">Y coordinate</param>
        public override void Draw(Pen pen, SolidBrush brush, int X, int Y)
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
