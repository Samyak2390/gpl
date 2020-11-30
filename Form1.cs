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
using System.IO;
using System.Security;

namespace gpl
{
    public partial class Form1 : Form
    {
        Bitmap canvasBitmap;
        Canvas visual;
        const int DEFAULT_COORDINATE = 0;

        public Form1()
        {
            InitializeComponent();
            
            this.canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
            visual = new Canvas(Graphics.FromImage(this.canvasBitmap), canvas);
            visual.MoveTo(DEFAULT_COORDINATE, DEFAULT_COORDINATE);
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
                if(tokens.Length == 1)
                {
                    SingleCommand(tokens[0]);
                }

                if(tokens.Length > 0)
                {
                    ProcessCommand(tokens);
                }
            }
        }

        public void SingleCommand(string token)
        {
            switch (token.ToLower())
            {
                case "reset":
                    visual.MoveTo(DEFAULT_COORDINATE, DEFAULT_COORDINATE);
                    break;

                case "clear":
                    this.canvasBitmap.Dispose();
                    this.canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
                    visual.GetSetGraphics = Graphics.FromImage(this.canvasBitmap);
                    Refresh();
                    break;

                case "run":
                    ProcessCommands();
                    break;
            }
        }

        public void ProcessCommands()
        {
            string[] lines = editor.Text.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach(string line in lines)
            {
                string trimmedLine = line.Trim();
                string[] tokens = trimmedLine.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
                ProcessCommand(tokens);
            }
        }

        public void ProcessCommand(string[] tokens)
        {
            Validator valid = new Validator(tokens);
            StatementSyntax statement = valid.Validate();
            Painter painter = new Painter(visual, statement);

            if (valid.Diagnostics.Count <= 0)
            {
                painter.Paint();
            }
            else
            {
                string Errors = "";
                foreach (var error in valid.Diagnostics)
                {
                    Errors += error + Environment.NewLine;
                }
                MessageBox.Show(Errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "Code.txt";
            save.Filter = "Text File | *.txt";

            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.OpenFile());
                string text = editor.Text;
                writer.Write(text);
                writer.Dispose();
                writer.Close();
            }
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog load = new OpenFileDialog();
            load.Filter = "txt files (*.txt)|*.txt";

            if (load.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string line = "";
                    string finalText = "";
                    var sr = new StreamReader(load.FileName);
                    while ((line = sr.ReadLine()) != null)
                    {
                        finalText += line + Environment.NewLine;
                    }
                    editor.Text = finalText;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}
