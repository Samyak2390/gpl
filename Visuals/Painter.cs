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
                    var moveto = (MoveToStatementSyntax)_statement;
                    _canvas.MoveTo(moveto.Point[0], moveto.Point[1]);
                    break;

                case SyntaxKind.DrawToStatement:
                    var drawto = (DrawToStatementSyntax)_statement;
                    _canvas.DrawTo(drawto.Point[0], drawto.Point[1]);
                    break;

                case SyntaxKind.PenStatement:
                    var pen = (PenStatementSyntax)_statement;
                    _canvas.SetPen(pen.Color);
                    break;

            }
        }
    }
}
