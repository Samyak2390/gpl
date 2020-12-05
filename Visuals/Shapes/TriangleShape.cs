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
    /// Class representing the syntax of triangle command.
    /// </summary>
    class TriangleShape: Shape
    {
        public Point[] Vertices { get; }
        private Graphics _graphics;
        private bool _fillState;

        /// <summary>
        /// Constructor that initializes the required parameters for traingle command.
        /// </summary>
        /// <param name="triangle">Object of Triangle Syntax</param>
        /// <param name="graphics">Object of graphics.</param>
        /// <param name="state">Fill State</param>
        public TriangleShape(TriangleStatementSyntax triangle, Graphics graphics, bool state)
        {
            Vertices = triangle.Vertices;
            _graphics = graphics;
            _fillState = state;
        }

        /// <summary>
        /// Method to draw Triangle.
        /// </summary>
        /// <param name="pen">Pen object</param>
        /// <param name="brush">SolidBrush object</param>
        /// <param name="X">X-coordinate</param>
        /// <param name="Y">Y-coordinate</param>
        public override void Draw(Pen pen, SolidBrush brush, int X, int Y)
        {
            if (_fillState)
            {
                _graphics.FillPolygon(brush, Vertices);
            }
            else
            {
                _graphics.DrawPolygon(pen, Vertices);
            }
        }
    }
}
