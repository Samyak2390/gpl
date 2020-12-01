using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace gpl.Visuals.Shapes
{
    /// <summary>
    /// Abstract class representing all the available shapes
    /// </summary>
    public abstract class Shape
    {
        public abstract void Draw(Pen pen, SolidBrush brush, int X, int Y);
    }
}
