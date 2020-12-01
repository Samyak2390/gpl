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
using System.Collections;

namespace gpl
{
    public partial class Form1 : Form
    {
        Bitmap canvasBitmap;
        Canvas visual;
        const int DEFAULT_COORDINATE = 0;
        public ArrayList diagnostics;
        string errorBag = "";

        public Form1()
        {
            InitializeComponent();
            diagnostics = new ArrayList();
            this.canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
            visual = new Canvas(Graphics.FromImage(this.canvasBitmap), canvas);
            visual.MoveTo(DEFAULT_COORDINATE, DEFAULT_COORDINATE);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(this.canvasBitmap, 0, 0);
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
            Validator valid = new Validator(tokens, diagnostics);
            StatementSyntax statement = valid.Validate();
            Painter painter = new Painter(visual, statement);

            if (diagnostics.Count <= 0)
            {
                painter.Paint();
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

        private void cli_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string command = cli.Text.Trim();
                string[] tokens = command.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 1)
                {
                    SingleCommand(tokens[0]);
                }

                if (tokens.Length > 0)
                {
                    ProcessCommand(tokens);
                }

                foreach (var error in diagnostics)
                {
                    errorBag += error + Environment.NewLine;
                }

                if (errorBag.Length > 0)
                {
                    MessageBox.Show(errorBag, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    diagnostics.Clear();
                    errorBag = "";
                }

                cli.Text = "";
                e.SuppressKeyPress = true;
            }

        }
    }
}
