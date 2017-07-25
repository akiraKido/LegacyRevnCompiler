using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevnCompiler;
using RevnCompiler.Lexers;

namespace RevnCompilerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tokens = Lexer.GenerateTokens("\"hoge\" 10").ToList();
            Assert.AreEqual(tokens[0].Value, "hoge");
            Assert.AreEqual(tokens[1].Value, "10");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var tokens = Lexer.GenerateTokens("x = 10").ToList();
            Assert.AreEqual(tokens[0].Value, "x");
            Assert.AreEqual(tokens[1].Value, "=");
            Assert.AreEqual(tokens[2].Value, "10");
        }

        [TestMethod]
        public void TestMethod3()
        {
            var tokens = Lexer.GenerateTokens("fun");
            Assert.AreEqual(tokens.First().Value, "fun");
            Assert.AreEqual((tokens.First() as Identifier)?.IsReserved, true);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var tokens = Lexer.GenerateTokens("fun hoge: end").ToList();
            Assert.AreEqual(tokens[0].Value, "fun");
            Assert.AreEqual(tokens[1].Value, "hoge");
            Assert.AreEqual(tokens[2].Value, ":");
            Assert.AreEqual(tokens[3].Value, "end");
        }

        [TestMethod]
        public void TestMethod5()
        {
            var tokens = Lexer.GenerateTokens("10.1");
            Assert.AreEqual(tokens.First().Value, "10.1");

            Assert.ThrowsException<Exception>(() =>
            {
                tokens = Lexer.GenerateTokens("10.0.1");
                var value = tokens.First().Value;
            });
        }

        [TestMethod]
        public void TestComment()
        {
            var tokens = Lexer.GenerateTokens("// test test teetetes" + Environment.NewLine + "x + 10").ToList();
            Assert.AreEqual(tokens[0].Value, "x");
            Assert.AreEqual(tokens[1].Value, "+");
            Assert.AreEqual(tokens[2].Value, "10");

        }
    }
}
