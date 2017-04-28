using System;
using Functional.Fluent.Extensions;

namespace SampleCompiler.Lex.Impl
{
    public class NumberLexerState : LexerStateBase
    {
        public NumberLexerState(IForwardReadSequence<char> sequence, char c, LexerStateBase lastState, IParserSet parsers) : base(sequence, c, lastState, parsers) { }

        public NumberLexerState(IForwardReadSequence<char> sequence, string token, char c, LexerStateBase lastState, IParserSet parsers) : base(sequence, token, c, lastState, parsers) { }

        public override ILexerState Act() =>
            Sequence.Next().Match()
                .With(IsWhitespace, c => (ILexerState)new InitialLexerState(Sequence, this, Parsers))
                .With(IsDigit, c => new NumberLexerState(Sequence, TokenValue, c, this, Parsers))
                .With(IsSpecialSymbol, c => new SpecialSymbolLexerState(Sequence, c, this, Parsers))
                .ElseThrow<ArgumentException>()
                .Do();
    }
}