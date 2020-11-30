using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpl.Compiler
{
    public class SyntaxMap
    {
        private static SyntaxMap _instance;
        private Dictionary<string, SyntaxKind> Commands;

        private static object syncLock = new object();
        protected SyntaxMap()
        {
            Commands = new Dictionary<string, SyntaxKind>
            {
                {"moveto", SyntaxKind.MoveToStatement },
                {"drawto", SyntaxKind.DrawToStatement },
                {"reset", SyntaxKind.ResetStatement },
                {"clear", SyntaxKind.ClearStatement },
                {"pen", SyntaxKind.PenStatement },
                {"rectangle", SyntaxKind.RectangleStatement },
                {"brush", SyntaxKind.BrushStatement },
                {"fill", SyntaxKind.FillStatement },
                {"circle", SyntaxKind.CircleStatement },
                {"triangle", SyntaxKind.TriangleStatement },
                {"run", SyntaxKind.RunStatement },
            };
        }

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

        public bool HasSyntax(string syntax)
        {
            return Commands.ContainsKey(syntax);
        }

        public SyntaxKind GetKind(string syntax)
        {
            if (HasSyntax(syntax)) return Commands[syntax];
            return SyntaxKind.BadSyntax;
        }
    }
}
