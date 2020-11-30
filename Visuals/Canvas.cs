using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using gpl.Compiler.Syntax;
using gpl.Visuals.Shapes;

namespace gpl.Visuals
{
    public class Canvas
    {
        private Graphics _graphics;
        private PictureBox _canvas;
        private Pen _pen;
        private SolidBrush _brush;
        private bool _fillState;

        public int X { get; set; }
        public int Y { get; set; }

        public Canvas(Graphics graphics, PictureBox canvas)
        {
            _graphics = graphics;
            _canvas = canvas;
            _pen = new Pen(Color.Black, 3);
            _brush = new SolidBrush(Color.Black);
            _fillState = false;
        }

        public void MoveTo(int X, int Y)
        {
            Bitmap point = new Bitmap(_canvas.Width, _canvas.Height);
            Graphics g = Graphics.FromImage(point);
            SolidBrush brush = new SolidBrush(Color.Black);
            g.FillEllipse(brush, X, Y, 7, 7);
            _canvas.Image = point;

            this.X = X;
            this.Y = Y;
        }

        public void DrawTo(int X, int Y)
        {
            _graphics.DrawLine(_pen, this.X, this.Y, X, Y);
            MoveTo(X, Y);
        }

        public void SetPen(Color color)
        {
            _pen = new Pen(color);
        }

        public void SetBrush(Color color)
        {
            _brush = new SolidBrush(color);
        }

        public void SetFillState(bool state)
        {
            _fillState = state;
        }

        public Graphics GetSetGraphics 
        { 
            get { return _graphics; }
            set { _graphics = value; }
        }

        public void Draw(StatementSyntax statement)
        {
            switch (statement)
            {
                case RectangleStatementSyntax rectangleSyntax:
                    RectangleShape rectangle = (RectangleShape)ShapeFactory.GetShape(rectangleSyntax, _graphics, _fillState);
                    rectangle.Draw(_pen, _brush, X, Y);
                    MoveTo(X+rectangleSyntax.Width, Y+rectangleSyntax.Height);
                    break;

                case CircleStatementSyntax circleSyntax:
                    CircleShape circle = (CircleShape)ShapeFactory.GetShape(circleSyntax, _graphics, _fillState);
                    circle.Draw(_pen, _brush, X, Y);
                    MoveTo(X, Y);
                    break;

                case TriangleStatementSyntax triangleSyntax:
                    TriangleShape triangle = (TriangleShape)ShapeFactory.GetShape(triangleSyntax, _graphics, _fillState);
                    triangle.Draw(_pen, _brush, X, Y);
                    MoveTo(X, Y);
                    break;
            }
        }

    }
}
