using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using gpl.Compiler;
using gpl.Compiler.Syntax;
using gpl.Visuals;

namespace gpl
{
    public partial class Form1 : Form
    {
        Bitmap canvasBitmap;
        Canvas visual;

        public Form1()
        {
            InitializeComponent();
            
            this.canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
            visual = new Canvas(Graphics.FromImage(this.canvasBitmap), canvas);
            visual.MoveTo(0, 0);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(this.canvasBitmap, 0, 0);
        }

        private void cli_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                string[] lines = cli.Text.Split('\n');
                string command = lines[lines.Length - 2].Trim();
                string[] tokens = command.Split(new string[] {" "}, System.StringSplitOptions.RemoveEmptyEntries);
                if(tokens.Length > 0)
                {
                    Validator valid = new Validator(tokens);
                    StatementSyntax statement = valid.Validate();
                    Painter painter = new Painter(visual, statement);

                    if(valid.Diagnostics.Count <= 0)
                    {
                        painter.Paint();
                    }
                    else
                    {
                        string Errors = "";
                        foreach(var error in valid.Diagnostics)
                        {
                            Errors += error + Environment.NewLine;
                        }
                        MessageBox.Show(Errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
/*

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
*/