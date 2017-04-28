using Functional.Fluent.Extensions;

namespace SampleCompiler.Lex.Impl
{
    public class SymbolLexerState : LexerStateBase
    {
        public SymbolLexerState(IForwardReadSequence<char> sequence, char c, LexerStateBase lastState, IParserSet parsers) : base(sequence, c, lastState, parsers) { }

        public SymbolLexerState(IForwardReadSequence<char> sequence, string token, char c, LexerStateBase lastState, IParserSet parsers) : base(sequence, token, c, lastState, parsers) { }

        public override ILexerState Act() => 
            Sequence.Next().Match()
                .With(IsWhitespace, c => (ILexerState)new InitialLexerState(Sequence, this, Parsers))
                .With(IsSpecialSymbol, c => new SpecialSymbolLexerState(Sequence, c, this, Parsers))
                .Else(c => new SymbolLexerState(Sequence, TokenValue, c, this, Parsers))
                .Do();
    }
}