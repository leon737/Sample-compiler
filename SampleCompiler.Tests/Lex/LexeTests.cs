using System.Linq;
using NUnit.Framework;
using SampleCompiler.Lex;
using SampleCompiler.Lex.Impl;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Tests.Lex
{

    [TestFixture]
    public class LexeTests
    {
        private readonly ILexer _lexer;

        public LexeTests()
        {
            _lexer = new Lexer(new SequenceProvider(), new LexerStateFactory());
        }

        [Test]
        public void Test_Math_Expression()
        {
            string source = "1+20 - 301 *    51 /42";

            var result = _lexer.Parse(source).ToList();

            Assert.AreEqual(9, result.Count);

            Assert.IsInstanceOf<IntegerToken>(result[0]);
            Assert.AreEqual(1, ((IntegerToken)result[0]).Value);

            Assert.IsInstanceOf<OperatorToken>(result[1]);
            Assert.AreEqual(Operators.Plus, ((OperatorToken)result[1]).Operator);

            Assert.IsInstanceOf<IntegerToken>(result[2]);
            Assert.AreEqual(20, ((IntegerToken)result[2]).Value);

            Assert.IsInstanceOf<OperatorToken>(result[3]);
            Assert.AreEqual(Operators.Minus, ((OperatorToken)result[3]).Operator);

            Assert.IsInstanceOf<IntegerToken>(result[4]);
            Assert.AreEqual(301, ((IntegerToken)result[4]).Value);

            Assert.IsInstanceOf<OperatorToken>(result[5]);
            Assert.AreEqual(Operators.Multiply, ((OperatorToken)result[5]).Operator);

            Assert.IsInstanceOf<IntegerToken>(result[6]);
            Assert.AreEqual(51, ((IntegerToken)result[6]).Value);

            Assert.IsInstanceOf<OperatorToken>(result[7]);
            Assert.AreEqual(Operators.Divide, ((OperatorToken)result[7]).Operator);

            Assert.IsInstanceOf<IntegerToken>(result[8]);
            Assert.AreEqual(42, ((IntegerToken)result[8]).Value);


        }
    }
}