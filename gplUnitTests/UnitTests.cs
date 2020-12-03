using Microsoft.VisualStudio.TestTools.UnitTesting;
using gpl.Compiler;
using gpl.Compiler.Syntax;
using System.Collections;
using System;

namespace gplUnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        /*
         * This method tests moveto command by passing expected tokens to validator.
         * If the validator return syntax type of MoveToStatementSyntax then test passes.
         * It also asserts that the returned syntax has expected properties.
         */

        public void TestMoveToCommand()
        {
            ArrayList diagnostics = new ArrayList();
            Validator valid = new Validator(new string[] {"moveto", "80", "100"}, diagnostics);
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
            Validator valid = new Validator(new string[] {"invalid x x"}, diagnostics);
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
            Validator valid = new Validator(new string[] { "rectangle", "80", "100" }, diagnostics);
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
            Validator valid = new Validator(new string[] { "circle", "xyz", "abc" }, diagnostics);
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
            Validator valid = new Validator(new string[] { "count", "=", "4" }, diagnostics);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.VariableExpression);
            Assert.IsTrue(equal);
        }

        [TestMethod]
        /**
        * Test for if statement and asserting the kind of Syntax it returns.
        */
        public void TestIfStatement()
        {
            ArrayList diagnostics = new ArrayList();
            Validator valid = new Validator(new string[] { "if", "count", "==", "4", "then", "moveto", "30", "30" }, diagnostics);
            StatementSyntax statement = valid.Validate();
            bool equal = Enum.Equals(statement.Kind, SyntaxKind.IfStatement);
            Assert.IsTrue(equal);
        }

    }
}
