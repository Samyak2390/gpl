using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using gpl.Compiler.Syntax;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace gpl.Compiler
{
    /// <summary>
    /// Class that validates the given command and returns the object of respective command
    /// if validation passes. Otherwise, returns the list of errors occured as a string.
    /// </summary>
    public class Validator
    {
        private string[] _tokens;
        private SyntaxMap _syntaxMap;
        private string _rawCommand;
        private string[] _rawLines;
        private bool _commandFound;
        private int _executingLine;
        private Dictionary<string, int> _varMap;
        /// <summary>
        /// Stores any type of errors that occur while executing commands.
        /// </summary>
        public ArrayList _diagnostics;

        /// <summary>
        /// Constructor that initializes mappings of available commands and their types,
        /// along with the passed reference of diagnostics and array of command.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="diagnostics"></param>
        /// <param name="rawCommand"></param>
        public Validator(string[] tokens, ArrayList diagnostics, string rawCommand, Dictionary<string, int> varMap, string[] rawLines, int executingLine)
        {
            _tokens = tokens;
            _syntaxMap = SyntaxMap.GetSyntaxMap();
            _diagnostics = diagnostics;
            _rawCommand = rawCommand;
            _varMap = varMap;
            _rawLines = rawLines;
            _executingLine = executingLine;
        }

        /// <summary>
        /// Method that validates the given command stored in _tokens and returns the respective object of 
        /// type of command provided or adds errors to dianostic array list if validation fails.
        /// </summary>
        /// <returns></returns>
        public StatementSyntax Validate()
        {
            //checking provided command against listed commands in hash map
            if (_syntaxMap.HasSyntax(_tokens[0].ToLower()))
            {
                //Finding the type of the command and returning its object with provided parameters as its properties
                switch (_syntaxMap.GetKind(_tokens[0].ToLower()))
                {
                    case SyntaxKind.Method:
                        try
                        {
                            string methodName = "";
                            string methodBody = "";
                            //List<string> parameters = new List<string>();
                            string[] parameters = new string[10];

                            string[] methodTokens = _rawCommand.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
                            try
                            {
                                //method without params
                                //TODO have to revise this regex
                                if (Regex.IsMatch(methodTokens[1], @"^[a-zA-Z]+\([a-zA-Z,]*[a-zA-Z]*\)"))
                                {
                                    string temp = "";
                                    Regex regex = new Regex(@"\((.*)\)");
                                    Match match = regex.Match(methodTokens[1]);
                                    if(match.Value.Length > 2)
                                    {
                                        string paramString = match.Value.Substring(1, match.Value.Length - 2);
                                        parameters = paramString.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                                    }

                                    foreach (char c in methodTokens[1])
                                    {
                                        if (Regex.IsMatch(c.ToString(), @"[a-zA-Z]"))
                                        {
                                            temp += c;
                                        }
                                        else
                                        {
                                            if (temp.Length >= 1) methodName=temp;
                                            temp = "";
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    _diagnostics.Add($"Invalid method declaration at line {_executingLine + 1}");
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {
                                _diagnostics.Add($"method name is expected at line {_executingLine + 1}");
                            }

                            _executingLine++;
                            while (_rawLines[_executingLine].Trim().ToLower() != "endmethod")
                            {
                                methodBody += _rawLines[_executingLine] + Environment.NewLine;
                                _executingLine++;
                                if (_executingLine > _rawLines.Length - 1)
                                {
                                    throw new IndexOutOfRangeException();
                                }
                            }

                            Form1.executingLine = _executingLine;

                            return new Method(SyntaxKind.Method, methodName, methodBody, parameters);
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"endmethod is expected at line {_executingLine + 1}");
                        }
                        
                        break;
                    case SyntaxKind.WhileStatement:
                        try
                        {
                            List<string[]> body = new List<string[]>();

                            int? value1 = EvaluateVariable(_tokens[1]);
                            int? value2 = EvaluateVariable(_tokens[3]);

                            string firstValue = value1 == null ? _tokens[1] : value1.ToString();
                            string secondValue = value2 == null ? _tokens[3] : value2.ToString();

                            string[] condition = ValidateCondition(firstValue, _tokens[2], secondValue);

                            if (_diagnostics.Count > 0) break;

                            _executingLine++;
                            while (_rawLines[_executingLine].Trim().ToLower() != "endloop")
                            {
                                if (Regex.IsMatch(_rawLines[_executingLine].Replace("\t", ""), @"^[a-zA-Z]+[\s]*="))
                                {
                                    body.Add(new string[] { _rawLines[_executingLine].Replace("\t", "") });
                                }
                                else
                                {
                                    body.Add(ParseCommand(_rawLines[_executingLine]));
                                }
                                _executingLine++;
                                if (_executingLine > _rawLines.Length - 1)
                                {
                                    throw new IndexOutOfRangeException();
                                }
                            }
                            //may need to increment executing line -> no need coz its incremented after process command
                            Form1.executingLine = _executingLine;

                            return new WhileStatement(SyntaxKind.WhileStatement, condition, body, _tokens[1], _tokens[2], _tokens[3]);

                        }
                        catch (IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"endloop is expected at line {_executingLine + 1}");
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        break;

                    case SyntaxKind.IfStatement:
                        try
                        {
                            List<string[]> body = new List<string[]>();

                            int? value1 = EvaluateVariable(_tokens[1]);
                            int? value2 = EvaluateVariable(_tokens[3]);

                            string firstValue = value1 == null ? _tokens[1] : value1.ToString();
                            string secondValue = value2 == null ? _tokens[3] : value2.ToString();

                            string[] condition = ValidateCondition(firstValue, _tokens[2], secondValue);

                            if (_diagnostics.Count > 0) break;

                            if (_tokens.Length >= 5) //Process for single line if statement
                            {
                                List<string> tempStatement = new List<string>();
                                for(int i = 4; i<_tokens.Length; i++)
                                {
                                    tempStatement.Add(_tokens[i]);
                                }
                                body.Add(tempStatement.ToArray());
                            }
                            else
                            {
                                _executingLine++;
                                while (_rawLines[_executingLine].Trim().ToLower() != "endif")
                                {
                                    //For variable initialization
                                    if (Regex.IsMatch(_rawLines[_executingLine].Replace("\t", ""), @"^[a-zA-Z]+[\s]*="))
                                    {
                                        body.Add(new string[] { _rawLines[_executingLine].Replace("\t", "") });
                                    }
                                    else
                                    {
                                        body.Add(ParseCommand(_rawLines[_executingLine]));
                                    }
                                    _executingLine++;
                                    if(_executingLine > _rawLines.Length-1)
                                    {
                                        throw new IndexOutOfRangeException();
                                    }
                                }
                                Form1.executingLine = _executingLine;
                            }

                            return new IfStatementSyntax(SyntaxKind.IfStatement, condition, body);

                        }
                        catch(IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"endif is expected at line {_executingLine + 1}"); 
                        }
                        catch(Exception e)
                        {
                            //_diagnostics.Add();
                            MessageBox.Show(e.Message);
                        }
                        break;

                    case SyntaxKind.MoveToStatement:
                        try
                        {
                            int? point1 = EvaluateVariable(_tokens[1]);
                            int? point2 = EvaluateVariable(_tokens[2]);
                            
                            string pointX = point1 == null ? _tokens[1] : point1.ToString();
                            string pointY = point2 == null ? _tokens[2] : point2.ToString();
                            int[] point = GetPoint(pointX, pointY);
                            _commandFound = true;
                            return new MoveToStatementSyntax(SyntaxKind.MoveToStatement, point);
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"Two Parameters required for <{_tokens[0]}>.");
                        }
                        break;

                    case SyntaxKind.DrawToStatement:
                        try
                        {
                            int? point1 = EvaluateVariable(_tokens[1]);
                            int? point2 = EvaluateVariable(_tokens[2]);

                            string pointX = point1 == null ? _tokens[1] : point1.ToString();
                            string pointY = point2 == null ? _tokens[2] : point2.ToString();
                            int[] point = GetPoint(pointX, pointY);
                            _commandFound = true;
                            return new DrawToStatementSyntax(SyntaxKind.DrawToStatement, point);
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"Two Parameters required for <{_tokens[0]}>.");
                        }
                        break;

                    case SyntaxKind.PenStatement:
                        try
                        {
                            if(Regex.IsMatch(_tokens[1], @"^[a-zA-Z]+$")){
                                string color = _tokens[1];
                                _commandFound = true;
                                PenStatementSyntax pen = new PenStatementSyntax(SyntaxKind.PenStatement, color);
                                if(pen.Color == Color.Black)
                                {
                                    _diagnostics.Add($"{color} color not found.");
                                }
                                return pen;
                            }
                            else
                            {
                                _diagnostics.Add("Color name must be alphabetic.");
                            }
                        }
                        catch (IndexOutOfRangeException e)
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
                                _commandFound = true;
                                if (brush.Color == Color.Black)
                                {
                                    _diagnostics.Add($"{color} color not found.");
                                }
                                return brush;
                            }
                            else
                            {
                                _diagnostics.Add("Color name must be alphabetic.");
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"<{_tokens[0]}> requires a color parameter.");
                        }
                        break;

                    case SyntaxKind.FillStatement:
                        try
                        {
                            if (Regex.IsMatch(_tokens[1], @"^[a-zA-Z]+$"))
                            {
                                string state = _tokens[1];
                                _commandFound = true;
                                if (state.Equals("on") || state.Equals("off"))
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
                        catch (IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"<{_tokens[0]}> requires on/off parameter.");
                        }
                        break;

                    case SyntaxKind.RectangleStatement:
                        try
                        {
                            int? width = EvaluateVariable(_tokens[1]);
                            int? height = EvaluateVariable(_tokens[2]);

                            string widthX = width == null ? _tokens[1] : width.ToString();
                            string heightY = height == null ? _tokens[2] : height.ToString();
                            int[] size = GetPoint(widthX, heightY);
                            _commandFound = true;
                            return new RectangleStatementSyntax(SyntaxKind.RectangleStatement, size);
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"Two Parameters required for <{_tokens[0]}>.");
                        }
                        break;

                    case SyntaxKind.CircleStatement:
                        try
                        {
                            int? size = EvaluateVariable(_tokens[1]);
                            string radius = size == null ? _tokens[1] : size.ToString();
                            //check radius is a-zA-Z and is acutally a variable
                            _commandFound = true;
                            try
                            {
                                if (int.TryParse(radius, out var r))
                                {
                                    return new CircleStatementSyntax(SyntaxKind.CircleStatement, r);
                                }
                                else
                                {
                                    throw new FormatException();
                                }
                            }
                            catch (FormatException e)
                            {
                                _diagnostics.Add($"Radius must be integer. <{radius}> given.");
                            }
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"Radius Parameters required for <{_tokens[0]}>.");
                        }
                        break;

                    case SyntaxKind.TriangleStatement:
                        try
                        {
                            int? vertexA = EvaluateVariable(_tokens[1]);
                            int? vertexB = EvaluateVariable(_tokens[2]);
                            int? vertexC = EvaluateVariable(_tokens[3]);
                            int? vertexD = EvaluateVariable(_tokens[4]);
                            int? vertexE = EvaluateVariable(_tokens[5]);
                            int? vertexF = EvaluateVariable(_tokens[6]);

                            string vertexa = vertexA == null ? _tokens[1] : vertexA.ToString();
                            string vertexb = vertexB == null ? _tokens[2] : vertexB.ToString();
                            string vertexc = vertexC == null ? _tokens[3] : vertexC.ToString();
                            string vertexd = vertexD == null ? _tokens[4] : vertexD.ToString();
                            string vertexe = vertexE == null ? _tokens[5] : vertexE.ToString();
                            string vertexf = vertexF == null ? _tokens[6] : vertexF.ToString();

                            Point vertex1 = GetVertex(vertexa, vertexb);
                            Point vertex2 = GetVertex(vertexc, vertexd);
                            Point vertex3 = GetVertex(vertexe, vertexf);
                            _commandFound = true;

                            if (!(vertex1.IsEmpty || vertex2.IsEmpty || vertex3.IsEmpty))
                            {
                                Point[] vertices = { vertex1, vertex2, vertex3 };
                                return new TriangleStatementSyntax(SyntaxKind.TriangleStatement, vertices);
                            }
                        }
                        catch(IndexOutOfRangeException e)
                        {
                            _diagnostics.Add($"Six integer Parameters required for <{_tokens[0]}>.");
                        }
                        break;
                }
            }
            else if (Regex.IsMatch(_rawCommand, @"^[a-zA-Z]+\([0-9,]*[0-9]*\)"))//check for method call
            {
                string methodName = "";
                string temp = "";

                Regex regex = new Regex(@"\((.*)\)");
                Match match = regex.Match(_rawCommand);
                string[] parameters = new string[10];
                if (match.Value.Length > 2)
                {
                    string paramString = match.Value.Substring(1, match.Value.Length - 2);
                    parameters = paramString.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                }

                foreach (char c in _rawCommand)
                {
                    if (Regex.IsMatch(c.ToString(), @"[a-zA-Z]"))
                    {
                        temp += c;
                    }
                    else
                    {
                        if (temp.Length >= 1) methodName = temp;
                        temp = "";
                        break;
                    }
                }

                return new MethodCall(SyntaxKind.MethodCall, methodName, parameters);

            }
            else if (Regex.IsMatch(_rawCommand, @"^[a-zA-Z]+[\s]*=") && !_commandFound)
            {
                CheckIfVariable(_rawCommand);
            }
            else
            {
                _diagnostics.Add($"Command <{_tokens[0]}> doesn't exist.");
            }
            return new BadSyntax();
        }

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

        private string[] ValidateCondition(string firstValue, string compareOperator, string secondValue)
        {
            List<string> condition = new List<string>();
            if (int.TryParse(firstValue, out var value))
            {
                condition.Add(firstValue);
            }
            else
            {
                _diagnostics.Add(firstValue + " should be integer");
            }

            if (compareOperator == "<=" || compareOperator == "==" || compareOperator == ">=" || compareOperator == ">" || compareOperator == "<" || compareOperator == "!=")
            {
                condition.Add(compareOperator);
            }
            else
            {
                _diagnostics.Add(compareOperator + " is invalid Conditional Operator.");
            }

            if (int.TryParse(secondValue, out var valuee))
            {
                condition.Add(secondValue);
            }
            else
            {
                _diagnostics.Add(secondValue + "should be integer");
            }
            return condition.ToArray();
        }

        private int? EvaluateVariable(string variable)
        {
            if (Regex.IsMatch(variable, @"[a-zA-Z]+"))
            {
                try
                {
                    return _varMap[variable];
                }
                catch (KeyNotFoundException e)
                {
                    _diagnostics.Add($"Undefined Variable: {_tokens[1]}");
                }
            }
            return null;
        }

        /// <summary>
        /// Method that checks if given strings are integers.
        /// </summary>
        /// <param name="X">X co-ordinate</param>
        /// <param name="Y">Y co-ordinate</param>
        /// <returns>Array of integers[X,Y].</returns>
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
                    throw new FormatException();
                }
            }
            catch (FormatException error)
            {
                _diagnostics.Add($"<{_tokens[1]}> and <{_tokens[2]}> must be integers");
            }
           
            return point;
        }

        /// <summary>
        /// Method that checks if given strings are integers.
        /// </summary>
        /// <param name="X">X co-ordinate</param>
        /// <param name="Y">Y co-ordinate</param>
        /// <returns>A Point object.</returns>
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
                    throw new FormatException();
                }
            }
            catch (FormatException error)
            {
                _diagnostics.Add($"<{X}> and <{Y}> must be integers");
            }
            return new Point();
        }

        private void CheckIfVariable(string rawCommand)
        {
            try
            {
                string[] varTokens = rawCommand.Split(new string[] { "=" }, System.StringSplitOptions.RemoveEmptyEntries);
                string expression = varTokens[1].Trim();
                string variableName = varTokens[0].Trim();
                //var = 3
                if (int.TryParse(expression, out var num)){
                    if (_varMap.ContainsKey(variableName))
                    {
                        _varMap[variableName] = num;
                    }
                    else
                    {
                        _varMap.Add(variableName, num);
                    }
                }
                //var = 3 + (5 * 3)
                else if(Regex.IsMatch(expression, @"^[^a-zA-Z]+$"))
                {
                    int result = Convert.ToInt32(new DataTable().Compute(expression, null));
                    if (_varMap.ContainsKey(variableName))
                    {
                        _varMap[variableName] = result;
                    }
                    else
                    {
                        _varMap.Add(variableName, result);
                    }
                }
                //var = 3
                //count = var + 3
                else
                {
                    List<string> variables = new List<string>();
                    string tempVar = "";
                    string newExpression = "";

                    foreach (char c in expression)
                    {
                        if (!Regex.IsMatch(c.ToString(), @"^[+\-\/*\s]+$"))
                        {
                            tempVar += c;
                        }
                        else
                        {
                            if (tempVar.Length >= 1) variables.Add(tempVar);
                            tempVar = "";
                        }
                    }
                    if (tempVar.Length >= 1) variables.Add(tempVar);

                    foreach (string variable in variables)
                    {
                        if(Regex.IsMatch(variable, @"^[a-zA-Z]+$"))
                        {
                            if (_varMap.ContainsKey(variable))
                            {
                                int value = _varMap[variable];
                                if (newExpression.Length <= 0)
                                    newExpression = expression.Replace(variable, value.ToString());
                                else
                                    newExpression = newExpression.Replace(variable, value.ToString());
                            }
                            else
                            {
                                _diagnostics.Add($"Undefined variable: {variable}.");
                            }
                        }

                    }

                    if(newExpression.Length >= 1 && Regex.IsMatch(newExpression, @"^[^a-zA-Z]+$"))
                    {
                        int result = Convert.ToInt32(new DataTable().Compute(newExpression, null));
                        if (_varMap.ContainsKey(variableName))
                        {
                            _varMap[variableName] = result;
                        }
                        else
                        {
                            _varMap.Add(variableName, result);
                        }
                    }

                }
            }
            catch (EvaluateException e)
            {
                _diagnostics.Add("Invalid Expression." );
            }
            catch(SyntaxErrorException error)
            {
                _diagnostics.Add($"Invalid Syntax: <{rawCommand}> for variable initialization.");
            }
        }

    }
}
