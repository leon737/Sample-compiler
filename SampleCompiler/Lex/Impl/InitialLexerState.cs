using Functional.Fluent.Extensions;

namespace SampleCompiler.Lex.Impl
{
    public class InitialLexerState : LexerStateBase
    {
        public InitialLexerState(IForwardReadSequence<char> sequence, LexerStateBase lastState, IParserSet parsers) : base(sequence, lastState, parsers) { }

        public override ILexerState Act() =>
            Sequence.Next().Match()
                .With(IsWhitespace, c => (ILexerState)new InitialLexerState(Sequence, this, Parsers))
                .With(IsDigit, c => new NumberLexerState(Sequence, c, this, Parsers))
                .With(IsSpecialSymbol, c => new SpecialSymbolLexerState(Sequence, c, this, Parsers))
                .Else(c => new SymbolLexerState(Sequence, c, this, Parsers))
                .Do();
    }
}