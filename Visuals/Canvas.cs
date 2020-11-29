using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace gpl.Visuals
{
    public class Canvas
    {
        private Graphics _graphics;
        private PictureBox _canvas;
        private Pen _pen;
        
        public int X { get; set; }
        public int Y { get; set; }

        public Canvas(Graphics graphics, PictureBox canvas)
        {
            _graphics = graphics;
            _canvas = canvas;
            _pen = new Pen(Color.Black, 3);
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

        public Graphics GetSetGraphics 
        { 
            get { return _graphics; }
            set { _graphics = value; }
        }

    }
}
