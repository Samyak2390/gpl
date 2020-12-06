using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    /// <summary>
    /// Singleton class that initializes the hashmap storing key value pair of 
    /// available commands and its respective type.
    /// </summary>
    public class SyntaxMap
    {
        private static SyntaxMap _instance;
        private Dictionary<string, SyntaxKind> Commands;

        private static object syncLock = new object();

        /// <summary>
        /// Constructor that initializes Commands hashmap with available commands.
        /// </summary>
        protected SyntaxMap()
        {
            Commands = new Dictionary<string, SyntaxKind>
            {
                {"moveto", SyntaxKind.MoveToStatement },
                {"drawto", SyntaxKind.DrawToStatement },
                {"reset", SyntaxKind.ResetStatement },
                {"clear", SyntaxKind.ClearStatement },
                {"pen", SyntaxKind.PenStatement },
                {"rect", SyntaxKind.RectangleStatement },
                {"brush", SyntaxKind.BrushStatement },
                {"fill", SyntaxKind.FillStatement },
                {"circle", SyntaxKind.CircleStatement },
                {"triangle", SyntaxKind.TriangleStatement },
                {"run", SyntaxKind.RunStatement },
                { "if", SyntaxKind.IfStatement},
            };
        }

        /// <summary>
        /// Provides the instance of Singleton SyntaxMap.
        /// </summary>
        /// <returns>Instance of Singleton SyntaxMap.</returns>
        public static SyntaxMap GetSyntaxMap()
        {
            if(_instance == null)
            {
                lock (syncLock)
                {
                    if(_instance == null)
                    {
                        _instance = new SyntaxMap();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// Checks whether the given string is a valid command.
        /// </summary>
        /// <param name="syntax">String to be checked for valid command.</param>
        /// <returns>true if command is valid, false otherwise.</returns>
        public bool HasSyntax(string syntax)
        {
            return Commands.ContainsKey(syntax);
        }

        /// <summary>
        /// Provides the kind of the command.
        /// </summary>
        /// <param name="syntax">String for which kind is required.</param>
        /// <returns>Enum type of syntax kind.</returns>
        public SyntaxKind GetKind(string syntax)
        {
            if (HasSyntax(syntax)) return Commands[syntax];
            return SyntaxKind.BadSyntax;
        }
    }
}
