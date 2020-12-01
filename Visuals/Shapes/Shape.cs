using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gpl.Visuals.Shapes
{
    public abstract class Shape
    {
        public abstract void Draw(Pen pen, SolidBrush brush, int X, int Y);
    }
}
