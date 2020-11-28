using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gpl
{
    public partial class Form1 : Form
    {
        Bitmap canvasBitmap;
        Graphics g;
        SolidBrush sb;
        int xPos;
        int yPos;
        Pen pen;


        public Form1()
        {
            InitializeComponent();
            this.canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
            this.g = Graphics.FromImage(this.canvasBitmap);
            sb = new SolidBrush(ForeColor);
            this.pen = new Pen(Color.Red, 3);
           
            this.xPos = 0;
            this.yPos = 0;
            this.changeStartPos();
        }

     
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Bitmap point = new Bitmap(canvas.Width, canvas.Height);
            g = Graphics.FromImage(point);
            g.FillEllipse(this.sb, e.X, e.Y, 10, 10);

            
            canvas.Image = point;

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //sb = new SolidBrush(ForeColor);
            //g.FillEllipse(sb, 5, 5, 20, 20);
            g.DrawImageUnscaled(this.canvasBitmap, 0, 0);
        }

        private void changeStartPos()
        {
            Bitmap point = new Bitmap(canvas.Width, canvas.Height);
            Graphics g = Graphics.FromImage(point);
            g.FillEllipse(this.sb, this.xPos, this.yPos, 10, 10);
            canvas.Image = point;
        }

        private void cli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                string[] lines = cli.Text.Split('\n');
                int length = lines.Length;
                string token = lines[length - 2];
                string[] tokens = token.Split(' ');
                if (tokens[0].ToLower().Equals("moveto"))
                {
                    this.xPos = Int32.Parse(tokens[1]);
                    this.yPos = Int32.Parse(tokens[2]);
                    this.changeStartPos();
                }

                if (tokens[0].ToLower().Equals("drawto"))
                {
                    int a = Int32.Parse(tokens[1]);
                    int b = Int32.Parse(tokens[2]);
                    this.g.DrawLine(this.pen, this.xPos, this.yPos, a, b); 
                    //canvas.Image = canvasBitmap;
                    this.xPos = a;
                    this.yPos = b;
                    this.changeStartPos();
                    //Refresh();//From Form class
                }

            }
        }
    }
}
