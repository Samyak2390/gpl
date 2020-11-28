using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gpl.Compiler
{
    public class SyntaxTree
    {
        public CompilationUnitSyntax Root { get; }
        public SyntaxTree(string text, bool fromCli)
        {
            var parser = new Parser(text, fromCli);
            var root = parser.ParseCompilationUnit();

            Root = root;
        }

        public void ShowTree()
        {
            string parseTree = Root.GetTree();
            MessageBox.Show(parseTree);
        }
    }
}
