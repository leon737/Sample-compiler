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
        private readonly IParserSet _parsers;

        public Lexer(ISequenceProvider<string, char> sequenceProvider, ILexerStateFactory lexerStateFactory, IParserSet parsers)
        {
            _sequenceProvider = sequenceProvider;
            _lexerStateFactory = lexerStateFactory;
            _parsers = parsers;
        }

        public IEnumerable<Token> Parse(string sourceCode)
        {
            var sequence = _sequenceProvider.CreateSequence(sourceCode);

            var state = _lexerStateFactory.CreateInitialState(sequence, _parsers);

            while (sequence.HasNext())
            {
                state = state.Act();

                if (state.TokenComplete)
                {
                    yield return MakeToken(state);
                    state = state.Trim();
                }
            }
            yield return MakeToken(_lexerStateFactory.CreateTailToken(sequence, state, _parsers));
        }

        private Token MakeToken(ILexerState state) => 
            state.Token.With(v => v.TypeMatch()
                .With(Case.Is<NumberLexerState>(), _ => (Token)new IntegerToken() { Value = int.Parse(v.TokenValue.Value) })
                .With(Case.Is<SymbolLexerState>(), _ => v.TokenValue.Match()
                    .With(_parsers.KeywordParser.Is, _parsers.KeywordParser.Make)
                    .Else(id => new IdentifierToken { Identifier = id })
                    .Do())
                .Else(_ => v.TokenValue.Match()
                        .With(_parsers.SymbolParser.Is, _parsers.SymbolParser.Make)
                        .With(_parsers.OperatorParser.Is, _parsers.OperatorParser.Make)
                        .Do()
                )
                .Do()
        );
    }
}