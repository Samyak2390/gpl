using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using gpl.Compiler.Syntax;
using System.Text.RegularExpressions;
using System.Drawing;

namespace gpl.Compiler
{
    public class Validator
    {
        private string[] _tokens;
        private SyntaxMap _syntaxMap;
        public ArrayList _diagnostics;

        public Validator(string[] tokens, ArrayList diagnostics)
        {
            _tokens = tokens;
            _syntaxMap = SyntaxMap.GetSyntaxMap();
            _diagnostics = diagnostics;
        }

        public StatementSyntax Validate()
        {
            if (_syntaxMap.HasSyntax(_tokens[0].ToLower()))
            {
                switch (_syntaxMap.GetKind(_tokens[0].ToLower()))
                {
                    case SyntaxKind.MoveToStatement:
                        try
                        {
                            int[] point = GetPoint(_tokens[1], _tokens[2]);
                            return new MoveToStatementSyntax(SyntaxKind.MoveToStatement, point);
                        }
                        catch (Exception e)
                        {
                            _diagnostics.Add($"Two Parameters required for <{_tokens[0]}>.");
                        }
                        break;

                    case SyntaxKind.DrawToStatement:
                        try
                        {
                            int[] point = GetPoint(_tokens[1], _tokens[2]);
                            return new DrawToStatementSyntax(SyntaxKind.DrawToStatement, point);
                        }
                        catch (Exception e)
                        {
                            _diagnostics.Add($"Two Parameters required for <{_tokens[0]}>.");
                        }
                        break;

                    case SyntaxKind.PenStatement:
                        try
                        {
                            if(Regex.IsMatch(_tokens[1], @"^[a-zA-Z]+$")){
                                string color = _tokens[1];
                                PenStatementSyntax pen = new PenStatementSyntax(SyntaxKind.PenStatement, color);
                                if(pen.Color == Color.Black)
                                {
                                    _diagnostics.Add($"{color} not found.");
                                }
                                return pen;
                            }
                            else
                            {
                                _diagnostics.Add("Color name must be alphabetic.");
                            }
                        }
                        catch (Exception e)
                        {
                            _diagnostics.Add($"<{_tokens[0]}> requires a color parameter.");
                        }
                        break;

                    case SyntaxKind.BrushStatement:
                        try
                        {
                            if (Regex.IsMatch(_tokens[1], @"^[a-zA-Z]+$"))
                            {
                                string color = _tokens[1];
                                BrushStatementSyntax brush = new BrushStatementSyntax(SyntaxKind.BrushStatement, color);
                                if (brush.Color == Color.Black)
                                {
                                    _diagnostics.Add($"{color} not found.");
                                }
                                return brush;
                            }
                            else
                            {
                                _diagnostics.Add("Color name must be alphabetic.");
                            }
                        }
                        catch (Exception e)
                        {
                            _diagnostics.Add($"<{_tokens[0]}> requires a color parameter.");
                        }
                        break;

                    case SyntaxKind.FillStatement:
                        try
                        {
                            if (Regex.IsMatch(_tokens[1], @"^[a-zA-Z]"))
                            {
                                string state = _tokens[1];
                                if(state.Equals("on") || state.Equals("off"))
                                {
                                    return new FillStatementSyntax(SyntaxKind.FillStatement, state);
                                }
                                else
                                {
                                    _diagnostics.Add($"<{_tokens[0]}> requires on/off parameter.");
                                }
                            }
                            else
                            {
                                _diagnostics.Add($"<{_tokens[0]}> requires on/off parameter.");
                            }
                        }
                        catch (Exception e)
                        {
                            _diagnostics.Add($"<{_tokens[0]}> requires on/off parameter.");
                        }
                        break;

                    case SyntaxKind.RectangleStatement:
                        try
                        {
                            int[] size = GetPoint(_tokens[1], _tokens[2]);
                            return new RectangleStatementSyntax(SyntaxKind.RectangleStatement, size);
                        }
                        catch (Exception e)
                        {
                            _diagnostics.Add($"Two Parameters required for <{_tokens[0]}>.");
                        }
                        break;

                    case SyntaxKind.CircleStatement:
                        try
                        {
                            string radius = _tokens[1];
                            try
                            {
                                if (int.TryParse(radius, out var r))
                                {
                                    return new CircleStatementSyntax(SyntaxKind.CircleStatement, r);
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                            catch (Exception e)
                            {
                                _diagnostics.Add($"Radius must be integer. <{radius}> given.");
                            }
                        }
                        catch (Exception e)
                        {
                            _diagnostics.Add($"Radius Parameters required for <{_tokens[0]}>.");
                        }
                        break;

                    case SyntaxKind.TriangleStatement:
                        try
                        {
                            Point vertex1 = GetVertex(_tokens[1], _tokens[2]);
                            Point vertex2 = GetVertex(_tokens[3], _tokens[4]);
                            Point vertex3 = GetVertex(_tokens[5], _tokens[6]);

                            if(!(vertex1.IsEmpty || vertex2.IsEmpty || vertex3.IsEmpty))
                            {
                                Point[] vertices = { vertex1, vertex2, vertex3 };
                                return new TriangleStatementSyntax(SyntaxKind.TriangleStatement, vertices);
                            }
                        }
                        catch(Exception e)
                        {
                            _diagnostics.Add($"Six integer Parameters required for <{_tokens[0]}>.");
                        }
                        break;


                }
            }
            else
            {
                _diagnostics.Add($"Command <{_tokens[0]}> doesn't exist.");
            }
            return new BadSyntax();
        }

        private int[] GetPoint(string X, string Y)
        {
            int[] point = new int[2];
            try
            {
                if (int.TryParse(X, out var x) && int.TryParse(Y, out var y))
                {
                    point[0] = x;
                    point[1] = y;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception error)
            {
                _diagnostics.Add($"<{_tokens[1]}> and <{_tokens[2]}> must be integers");
            }
           
            return point;
        }

        private Point GetVertex(string X, string Y)
        {
            try
            {
                if (int.TryParse(X, out var x) && int.TryParse(Y, out var y))
                {
                    return new Point(x, y);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception error)
            {
                _diagnostics.Add($"<{X}> and <{Y}> must be integers");
            }
            return new Point();
        }

    }
}
