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
using System.Text.RegularExpressions;

namespace gpl
{
    /// <summary>
    /// Main Form Class to initialize all components like textbox, picture box etc.
    /// It contains code for all event driven programmes.
    /// </summary>
    public partial class Form1 : Form
    {
        Bitmap canvasBitmap;
        Canvas visual;
        const int DEFAULT_COORDINATE = 0;
        string rawCommand;
        string[] rawLines;
        public static int executingLine { get; set; }
        Dictionary<string, int> _varMap = new Dictionary<string, int>();
        /// <summary>
        /// Stores any type of errors that occur while executing commands.
        /// </summary>
        public ArrayList diagnostics;
        string errorBag = "";

        /// <summary>
        /// Constructor that initializes the form, makes a bitmap of picture box's size.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            diagnostics = new ArrayList();
            this.canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
            visual = new Canvas(Graphics.FromImage(this.canvasBitmap), canvas);
            visual.MoveTo(DEFAULT_COORDINATE, DEFAULT_COORDINATE);
        }

        /// <summary>
        /// Paint event for the picture box - canvas.
        /// </summary>
        /// <param name="sender">Instance of object class</param>
        /// <param name="e">Pant event Arguments</param>
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(this.canvasBitmap, 0, 0);
        }

        /// <summary>
        /// Method to execute the single worded commands.
        /// </summary>
        /// <param name="token">Command to be executed</param>
        public void SingleCommand(string token)
        {
            switch (token.ToLower())
            {
                //moves pen to co-ordinate (0, 0)
                case "reset":
                    visual.MoveTo(DEFAULT_COORDINATE, DEFAULT_COORDINATE);
                    break;

                //clears the drawing screen
                case "clear":
                    this.canvasBitmap.Dispose();
                    this.canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
                    visual.GetSetGraphics = Graphics.FromImage(this.canvasBitmap);
                    Refresh();
                    break;

                //command that executes multiline commands from editor
                case "run":
                    ProcessCommands();
                    break;
            }
        }

        /// <summary>
        /// Method that executes multiline commands
        /// </summary>
        public void ProcessCommands()
        {
            rawLines = editor.Text.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            executingLine = 0;
            while(executingLine < rawLines.Length)
            {
                string trimmedLine = rawLines[executingLine].Trim();
                string[] tokensArray = ParseCommand(trimmedLine);
                rawCommand = trimmedLine;
                ProcessCommand(tokensArray);
                executingLine++;
            }
            _varMap.Clear();
        }

        /// <summary>
        /// Method that takes command and its parameters as an array, validates it and draws to canvas
        /// if there are no errors.
        /// </summary>
        /// <param name="tokens"></param>
        public void ProcessCommand(string[] tokens)
        {
            Validator valid = new Validator(tokens, diagnostics, rawCommand, _varMap, rawLines, executingLine);
            StatementSyntax statement = valid.Validate();
            if (statement.Kind == SyntaxKind.IfStatement)
            {
                IfStatementSyntax ifStatement = (IfStatementSyntax)statement;
                if (ifStatement.Run != null && (bool)ifStatement.Run)
                {
                    foreach(string[] command in ifStatement.Body)
                    {
                        ProcessCommand(command);
                    }
                }
            }
            else if (statement.Kind == SyntaxKind.WhileStatement)
            {
                WhileStatement whileStatement = (WhileStatement)statement;
                string variable1 = whileStatement.Variable1;
                string variable2 = whileStatement.Variable2;
                int? num1=null, num2=null;
                if (int.TryParse(variable1, out var var1))
                {
                    num1 = var1;
                }
                else
                {
                    if (_varMap.ContainsKey(variable1))
                    {
                        num1 = _varMap[variable1];
                    }
                }
                if (int.TryParse(variable2, out var var2))
                {
                    num2 = var2;
                }
                else
                {
                    if (_varMap.ContainsKey(variable2))
                    {
                        num1 = _varMap[variable2];
                    }
                }

                while ((bool)whileStatement.Run((int)num1, (int)num2))
                {
                    foreach (string[] command in whileStatement.Body)
                    {
                        if (command.Length == 1) rawCommand = command[0];
                        ProcessCommand(command);
                    }

                    if (int.TryParse(variable1, out var var11))
                    {
                        num1 = var11;
                    }
                    else
                    {
                        if (_varMap.ContainsKey(variable1))
                        {
                            num1 = _varMap[variable1];
                        }
                    }
                    if (int.TryParse(variable2, out var var22))
                    {
                        num2 = var22;
                    }
                    else
                    {
                        if (_varMap.ContainsKey(variable2))
                        {
                            num1 = _varMap[variable2];
                        }
                    }
                }
            }
            else
            {
                Painter painter = new Painter(visual, statement);

                if (diagnostics.Count <= 0)
                {
                    painter.Paint();
                }
            }
        }

        /// <summary>
        /// Click event that opens the file dialog that helps in choosing the location
        /// to save the code written in editor.
        /// </summary>
        /// <param name="sender">Instance of object class</param>
        /// <param name="e">Click Event Arguments</param>
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

        /// <summary> 
        /// Click event that opens the file dialog that helps in choosing the location
        /// to load the text file in code editor.
        /// </summary>
        /// <param name="sender">Instance of object class</param>
        /// <param name="e">Click Event Arguments</param>
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


        /// <summary>
        /// Takes a line of command and breaks it down to the array of tokens.
        /// </summary>
        /// <param name="rawCommand">A line of command</param>
        /// <returns>An array of string with expected commands and its parameters</returns>
        private string[] ParseCommand(string rawCommand)
        {
            ArrayList tokens = new ArrayList();
            string temp = "";

            foreach (char c in rawCommand)
            {
                if (!Regex.IsMatch(c.ToString(), @"^[,\s]+$"))
                {
                    temp += c;
                }
                else
                {
                    if (temp.Length >= 1) tokens.Add(temp);
                    temp = "";
                }
            }

            if (temp.Length >= 1) tokens.Add(temp);
            temp = "";

            return (string[])tokens.ToArray(typeof(string));
        }

        /// <summary>
        /// Key down event which is executed in press of Enter from keyboard.
        /// Decides which type of command to executes and shows errors from commands
        /// in message box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cli_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rawCommand = cli.Text.Trim();
                string[] tokensArray = ParseCommand(rawCommand);

                if (tokensArray.Length == 1)
                {
                    SingleCommand(tokensArray[0]);
                }

                if (tokensArray.Length > 0)
                {
                    ProcessCommand(tokensArray);
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
