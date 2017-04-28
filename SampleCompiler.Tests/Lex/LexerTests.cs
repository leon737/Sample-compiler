using System.Linq;
using NUnit.Framework;
using SampleCompiler.Lex;
using SampleCompiler.Lex.Impl;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Tests.Lex
{

    [TestFixture]
    public class LexerTests
    {
        private readonly ILexer _lexer;

        public LexerTests()
        {
            _lexer = new Lexer(new SequenceProvider(), new LexerStateFactory(), new ParserSet(new OperatorParser(), new KeywordParser(), new SymbolParser()));
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

        [Test]
        public void Test_Operator_Expression()
        {
            string source = "< <= == >= > && || +- <+";

            var result = _lexer.Parse(source).ToList();

            Assert.AreEqual(11, result.Count);

            Assert.IsInstanceOf<OperatorToken>(result[0]);
            Assert.AreEqual(Operators.LessThan, ((OperatorToken)result[0]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[1]);
            Assert.AreEqual(Operators.LessOrEqual, ((OperatorToken)result[1]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[2]);
            Assert.AreEqual(Operators.Equals, ((OperatorToken)result[2]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[3]);
            Assert.AreEqual(Operators.GreaterOrEqual, ((OperatorToken)result[3]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[4]);
            Assert.AreEqual(Operators.GreaterThan, ((OperatorToken)result[4]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[5]);
            Assert.AreEqual(Operators.LogicalAnd, ((OperatorToken)result[5]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[6]);
            Assert.AreEqual(Operators.LogicalOr, ((OperatorToken)result[6]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[7]);
            Assert.AreEqual(Operators.Plus, ((OperatorToken)result[7]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[8]);
            Assert.AreEqual(Operators.Minus, ((OperatorToken)result[8]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[9]);
            Assert.AreEqual(Operators.LessThan, ((OperatorToken)result[9]).Operator);

            Assert.IsInstanceOf<OperatorToken>(result[10]);
            Assert.AreEqual(Operators.Plus, ((OperatorToken)result[10]).Operator);
        }

        [Test]
        public void Test_Keyword_Expression()
        {
            string source = "if (5 >= 2) { var x = 25 + 2 }";

            var result = _lexer.Parse(source).ToList();

            Assert.AreEqual(14, result.Count);

            Assert.IsInstanceOf<KeywordToken>(result[0]);
            Assert.AreEqual(Keywords.If, ((KeywordToken)result[0]).Keyword);

            Assert.IsInstanceOf<SymbolToken>(result[1]);
            Assert.AreEqual(Symbols.OpenParenthesis, ((SymbolToken)result[1]).Symbol);

            Assert.IsInstanceOf<IntegerToken>(result[2]);
            Assert.AreEqual(5, ((IntegerToken)result[2]).Value);

            Assert.IsInstanceOf<OperatorToken>(result[3]);
            Assert.AreEqual(Operators.GreaterOrEqual, ((OperatorToken)result[3]).Operator);

            Assert.IsInstanceOf<IntegerToken>(result[4]);
            Assert.AreEqual(2, ((IntegerToken)result[4]).Value);

            Assert.IsInstanceOf<SymbolToken>(result[5]);
            Assert.AreEqual(Symbols.CloseParenthesis, ((SymbolToken)result[5]).Symbol);

            Assert.IsInstanceOf<SymbolToken>(result[6]);
            Assert.AreEqual(Symbols.OpenBlock, ((SymbolToken)result[6]).Symbol);

            Assert.IsInstanceOf<KeywordToken>(result[7]);
            Assert.AreEqual(Keywords.Var, ((KeywordToken)result[7]).Keyword);

            Assert.IsInstanceOf<IdentifierToken>(result[8]);
            Assert.AreEqual("x", ((IdentifierToken)result[8]).Identifier);

            Assert.IsInstanceOf<OperatorToken>(result[9]);
            Assert.AreEqual(Operators.Assignment, ((OperatorToken)result[9]).Operator);

            Assert.IsInstanceOf<IntegerToken>(result[10]);
            Assert.AreEqual(25, ((IntegerToken)result[10]).Value);

            Assert.IsInstanceOf<OperatorToken>(result[11]);
            Assert.AreEqual(Operators.Plus, ((OperatorToken)result[11]).Operator);

            Assert.IsInstanceOf<IntegerToken>(result[12]);
            Assert.AreEqual(2, ((IntegerToken)result[12]).Value);

            Assert.IsInstanceOf<SymbolToken>(result[13]);
            Assert.AreEqual(Symbols.CloseBlock, ((SymbolToken)result[13]).Symbol);
        }
    }
}