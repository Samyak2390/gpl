using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gpl.Compiler.Syntax;
using gpl.Compiler;

namespace gpl.Visuals
{
    public class Painter
    {
        private StatementSyntax _statement;
        private Canvas _canvas;
        public Painter(Canvas canvas, StatementSyntax statement)
        {
            _canvas = canvas;
            _statement = statement;
        }

        public void Paint()
        {
            switch (_statement.Kind)
            {
                case SyntaxKind.MoveToStatement:
                    var statement = (MoveToStatementSyntax)_statement;
                    _canvas.MoveTo(statement.Point[0], statement.Point[1]);
                    break;
            }
        }
    }
}
