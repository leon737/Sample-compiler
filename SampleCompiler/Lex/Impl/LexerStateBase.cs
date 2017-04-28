using Functional.Fluent.Extensions;
using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Lex.Impl
{
    public abstract class LexerStateBase : ILexerState
    {
        protected readonly IForwardReadSequence<char> Sequence;

        protected readonly IParserSet Parsers;

        public Maybe<string> TokenValue { get; }

        protected Maybe<LexerStateBase> LastState { get; }

        public Maybe<ILexerState> Token => LastState.Value;

        protected LexerStateBase(IForwardReadSequence<char> sequence, LexerStateBase lastState, IParserSet parsers)
        {
            Sequence = sequence;
            Parsers = parsers;
            LastState = lastState.ToMaybe();
            TokenValue = Maybe<string>.Nothing;
        }

        protected LexerStateBase(IForwardReadSequence<char> sequence, char c, LexerStateBase lastState, IParserSet parsers) : this(sequence, lastState, parsers)
        {
            TokenValue = c.ToString();
        }

        protected LexerStateBase(IForwardReadSequence<char> sequence, string token, char c, LexerStateBase lastState, IParserSet parsers) : this(sequence, lastState, parsers)
        {
            TokenValue = token + c;
        }

        protected LexerStateBase(IForwardReadSequence<char> sequence, string token, LexerStateBase lastState, IParserSet parsers) : this(sequence, lastState, parsers)
        {
            TokenValue = token;
        }

        public abstract ILexerState Act();

        public virtual ILexerState Trim() => this;

        public virtual bool TokenComplete => LastState.HasValue && LastState.Value.GetType() != typeof(InitialLexerState) && GetType() != LastState.Value.GetType();

        protected bool IsWhitespace(char c) => char.IsWhiteSpace(c);

        protected bool IsDigit(char c) => char.IsDigit(c);

        protected bool IsSpecialSymbol(char c) =>
            c.Match()
                .With('=', '<', '>', '(', ')', '{', '}', '+', '-', '*', '/', true)
                .With('&', '|', true)
                .Else(false)
                .Do();
    }
}