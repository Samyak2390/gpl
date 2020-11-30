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
        public ArrayList Diagnostics = new ArrayList();

        public Validator(string[] tokens)
        {
            _tokens = tokens;
            _syntaxMap = SyntaxMap.GetSyntaxMap();
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
                            Diagnostics.Add($"Two Parameters required for <{_tokens[0]}>.");
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
                            Diagnostics.Add($"Two Parameters required for <{_tokens[0]}>.");
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
                                    Diagnostics.Add($"{color} not found.");
                                }
                                return pen;
                            }
                            else
                            {
                                Diagnostics.Add("Color name must be alphabetic.");
                            }
                        }
                        catch (Exception e)
                        {
                            Diagnostics.Add($"<{_tokens[0]}> requires a color parameter.");
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
                            Diagnostics.Add($"Two Parameters required for <{_tokens[0]}>.");
                        }
                        break;

                }
            }
            else
            {
                Diagnostics.Add($"Command <{_tokens[0]}> doesn't exist.");
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
                Diagnostics.Add($"<{_tokens[1]}> and <{_tokens[2]}> must be integers");
            }
           
            return point;
        }

    }
}
