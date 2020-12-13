using gpl.Compiler;
using gpl.Compiler.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace gplUnitTests
{
    [TestClass]
    public class UnitTests
    {
        Dictionary<string, int> varMap = new Dictionary<string, int>();
        [TestMethod]
        /*
         * This method tests moveto command by passing expected tokens to validator.
         * If the validator return syntax type of MoveToStatementSyntax then test passes.
         * It also asserts that the returned syntax has expected properties.
         */

        public void TestMoveToCommand()
        {
            ArrayList diagnostics = new ArrayList();
            Validator valid = new Validator(
                new string[] { "moveto", "80", "100" },
                diagnostics,
                "moveto 80,100",
                varMap,
                new string[] { "moveto 80,100" },
                0);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.MoveToStatement);
            Assert.IsTrue(equal);
            var _statement = (MoveToStatementSyntax)statement;
            Assert.AreEqual(_statement.Point[0], 80);
            Assert.AreEqual(_statement.Point[1], 100);
        }

        /*
         * Here, invalid tokens are passed to validator/parser and it expects that validator returns BadSyntax type
         * and diagnostics arraylist contains the error message.
         */

        [TestMethod]
        public void TestCommandFail()
        {
            ArrayList diagnostics = new ArrayList();
            Validator valid = new Validator(
                new string[] { "invalid x x" },
                diagnostics,
                "invalid x x",
                varMap,
                new string[] { "invalid x x" },
                0);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.BadSyntax);
            bool hasError = diagnostics.Count > 0;
            Assert.IsTrue(equal);
            Assert.IsTrue(hasError);
        }

        /*
        * This method tests rectangle command by passing expected tokens to validator.
        * If the validator return syntax type of RectangleStatement then test passes.
        * It also asserts that the returned syntax has expected property values for Width and Height.
        */

        [TestMethod]
        public void TestRectangleCommand()
        {
            ArrayList diagnostics = new ArrayList();
            Validator valid = new Validator(
                new string[] { "rect", "80", "100" },
                diagnostics,
                "rect 80,100",
                varMap,
                new string[] { "rect 80,100" },
                0);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.RectangleStatement);
            Assert.IsTrue(equal);
            var _statement = (RectangleStatementSyntax)statement;
            Assert.AreEqual(_statement.Width, 80);
            Assert.AreEqual(_statement.Height, 100);
        }

        /*
        * This method tests a valid command by passing invalid parameter tokens to validator.
        * If the validator return syntax type of BadSyntax then test passes.
        * It also asserts that diagnostics arraylist contains the error message.
        */
        [TestMethod]
        public void TestIncorrectParameters()
        {
            ArrayList diagnostics = new ArrayList();
            Validator valid = new Validator(
                new string[] { "circle", "xyz", "abc" },
                diagnostics,
                "circle xyz, abc",
                varMap,
                new string[] { "circle xyz, abc" },
                0);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.BadSyntax);
            Assert.IsTrue(equal);
            bool hasError = diagnostics.Count > 0;
            Assert.IsTrue(hasError);
        }

        [TestMethod]
        /**
         * Test for variable initialization and asserting the kind of Syntax it returns.
         */
        public void TestVariableExpression()
        {
            ArrayList diagnostics = new ArrayList();
            Validator valid = new Validator(
                new string[] { "count", "=", "4" },
                diagnostics,
                "count = 4",
                varMap,
                new string[] { "count = 4" },
                0);
            StatementSyntax statement = valid.Validate();
            Assert.IsTrue(varMap.ContainsKey("count"));
            Assert.IsTrue(varMap["count"] == 4);
            varMap.Clear();
        }

        [TestMethod]
        /**
        * Test for if statement and asserting the kind of Syntax it returns.
        */
        public void TestIfStatement()
        {
            ArrayList diagnostics = new ArrayList();
            varMap.Add("count", 4);
            Validator valid = new Validator(
                new string[] { "if", "count", "==", "4", "moveto", "30", "30" },
                diagnostics,
                "if count == 4 moveto 30, 30",
                varMap,
                new string[] { "if count == 4 moveto 30, 30" },
                0);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.IfStatement);
            Assert.IsTrue(equal);
            IfStatementSyntax ifStatement = (IfStatementSyntax)statement;
            Assert.IsTrue((bool)ifStatement.Run);
            varMap.Clear();
        }

        [TestMethod]
        /**
        * Test for if statement and asserting the kind of Syntax it returns.
        */
        public void TestIfBlock()
        {
            ArrayList diagnostics = new ArrayList();
            Validator valid = new Validator(
                new string[] { "if", "100", "!=", "4" },
                diagnostics,
                "if 100 != 4",
                varMap,
                new string[] { "if 100 != 4", "circle 90", "rect 90, 90", "Endif" },
                0);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.IfStatement);
            Assert.IsTrue(equal);
            IfStatementSyntax ifStatement = (IfStatementSyntax)statement;
            Assert.IsTrue((bool)ifStatement.Run);
            Assert.IsTrue(ifStatement.Body.Count == 2);
        }

        [TestMethod]
        public void TestWhileStatement()
        {
            ArrayList diagnostics = new ArrayList();
            varMap.Add("radius", 50);
            Validator valid = new Validator(
                new string[] { "while","radius", "<", "250" },
                diagnostics,
                "while 50 < 250",
                varMap,
                new string[] { "while radius < 250", "circle radius", "radius = radius + 10", "endwhile" },
                0);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.WhileStatement);
            Assert.IsTrue(equal);
            WhileStatement whileStatement = (WhileStatement)statement;
           
            Assert.IsTrue(whileStatement.Body.Count == 2);
            varMap.Clear();
        }

        [TestMethod]
        public void TestMethod()
        {
            ArrayList diagnostics = new ArrayList();
            varMap.Add("radius", 50);
            Validator valid = new Validator(
                new string[] { "method", "mymethod()"},
                diagnostics,
                "method mymethod()",
                varMap,
                new string[] { "method mymethod()", "circle radius", "rect radius, radius", "endmethod" },
                0);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.Method);
            Assert.IsTrue(equal);

            Method method = (Method)statement;

            Assert.IsTrue(method.Body.Equals("circle radius\r\nrect radius, radius\r\n"));
            Assert.IsTrue(method.Parameters.Length == 0);
            varMap.Clear();
        }

        [TestMethod]
        public void TestMethodWithParams()
        {
            ArrayList diagnostics = new ArrayList();
            varMap.Add("radius", 50);
            Validator valid = new Validator(
                new string[] { "method", "mymethod(radius)" },
                diagnostics,
                "method mymethod(radius)",
                varMap,
                new string[] { "method mymethod(radius)", "circle radius", "rect radius, radius", "endmethod" },
                0);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.Method);
            Assert.IsTrue(equal);

            Method method = (Method)statement;

            Assert.IsTrue(method.Body.Equals("circle radius\r\nrect radius, radius\r\n"));
            Assert.IsTrue(method.Parameters.Length == 1);
            varMap.Clear();
        }
    }
}

