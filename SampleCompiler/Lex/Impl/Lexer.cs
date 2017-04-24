using System;
using System.Collections.Generic;
using Functional.Fluent.Extensions;
using Functional.Fluent.Pattern;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Lex.Impl
{
    public class Lexer : ILexer
    {
        private readonly ISequenceProvider<string, char> _sequenceProvider;

        private readonly ILexerStateFactory _lexerStateFactory;

        public Lexer(ISequenceProvider<string, char> sequenceProvider, ILexerStateFactory lexerStateFactory)
        {
            _sequenceProvider = sequenceProvider;
            _lexerStateFactory = lexerStateFactory;
        }

        public IEnumerable<Token> Parse(string sourceCode)
        {
            var sequence = _sequenceProvider.CreateSequence(sourceCode);

            var state = _lexerStateFactory.CreateInitialState(sequence);

            while (sequence.HasNext())
            {
                state = state.Act();

                if (state.TokenComplete)
                    yield return MakeToken(state, sequence);
            }
            yield return MakeToken(_lexerStateFactory.CreateTailToken(sequence, state), sequence);
        }

        private Token MakeToken(ILexerState state, IForwardReadSequence<char> sequence) => 
            state.Token.With(v => v.TypeMatch()
                .With(Case.Is<NumberLexerState>(), _ => (Token)new IntegerToken() { Value = int.Parse(v.TokenValue.Value) })
                .With(Case.Is<SymbolLexerState>(), _ => v.TokenValue.Match()
                    .With("if", "while", "return", "var", MakeKeyword)
                    //todo: make identifier
                    .Do())
                .Else(_ => v.TokenValue.Match()
                        .With("=", "<", ">", c => MakeOperator(sequence.LookAhead('=') == '=' ? c + '=' : c.ToString()))
                        .With("&&", "||", "!=", "+", "-", "*", "/", MakeOperator)
                        .With("{", "(", ")", "}", MakeSymbol)
                        .Do()
                )
                .Do()
        );

        private Token MakeOperator(string source) =>
            new OperatorToken
            {
                Operator = source.Match()
                    .With("=", Operators.Assignment)
                    .With("==", Operators.Equals)
                    .With("!=", Operators.NotEquals)
                    .With("<=", Operators.LessOrEqual)
                    .With("<", Operators.LessThan)
                    .With(">=", Operators.GreaterOrEqual)
                    .With(">", Operators.GreaterThan)
                    .With("&&", Operators.LogicalAnd)
                    .With("||", Operators.LogicalOr)
                    .With("*", Operators.Multiply)
                    .With("/", Operators.Divide)
                    .With("+", Operators.Plus)
                    .With("-", Operators.Minus)
                    .ElseThrow<ArgumentException>()
                    .Do()
            };

        private Token MakeKeyword(string source) =>
            new KeywordToken
            {
                Keyword = source.Match()
                    .With("return", Keywords.Return)
                    .With("if", Keywords.If)
                    .With("while", Keywords.While)
                    .With("var", Keywords.Var)
                    .Do()
            };

        private Token MakeSymbol(string source) =>
            new SymbolToken
            {
                Symbol = source.Match()
                    .With("{", Symbols.OpenBlock)
                    .With("}", Symbols.CloseBlock)
                    .With("(", Symbols.OpenParenthesis)
                    .With(")", Symbols.CloseParenthesis)
                    .Do()
            };
    }
}