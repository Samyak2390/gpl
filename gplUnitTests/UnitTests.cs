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

    }
}
