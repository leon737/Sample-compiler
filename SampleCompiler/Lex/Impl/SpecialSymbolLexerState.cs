using Functional.Fluent.Extensions;

namespace SampleCompiler.Lex.Impl
{
    public class SpecialSymbolLexerState : LexerStateBase
    {
        public SpecialSymbolLexerState(IForwardReadSequence<char> sequence, char c, LexerStateBase lastState, IParserSet parsers) : base(sequence, c, lastState, parsers) { }

        public SpecialSymbolLexerState(IForwardReadSequence<char> sequence, string token, char c, LexerStateBase lastState, IParserSet parsers) : base(sequence, token, c, lastState, parsers) { }

        public SpecialSymbolLexerState(IForwardReadSequence<char> sequence, string token, LexerStateBase lastState, IParserSet parsers) : base(sequence, token, lastState, parsers) { }

        public override ILexerState Act() =>
            Sequence.Next().Match()
                .With(IsWhitespace, c => (ILexerState)new InitialLexerState(Sequence, this, Parsers))
                .With(IsDigit, c => new NumberLexerState(Sequence, c, this, Parsers))
                .With(IsSpecialSymbol, c => new SpecialSymbolLexerState(Sequence, TokenValue, c, this, Parsers))
                .Else(c => new SymbolLexerState(Sequence, c, this, Parsers))
                .Do();

        public override bool TokenComplete => base.TokenComplete 
                                              || (Token.With(x => x.TokenValue).With(x => Parsers.OperatorParser.Is(x)).IsNull(false) 
                                                  && TokenValue.With(x => !Parsers.OperatorParser.Is(x)).IsNull(false));

        public override ILexerState Trim()
        {
            if (LastState.HasValue && LastState.Value.GetType() == typeof(SpecialSymbolLexerState))
                return new SpecialSymbolLexerState(Sequence, TokenValue.With(v => v.Substring(Token.With(x => x.TokenValue.With(z => z.Length).Value))), LastState.Value, Parsers);
            return this;
        }
    }
}