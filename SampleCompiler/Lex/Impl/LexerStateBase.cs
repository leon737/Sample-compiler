using System;
using Functional.Fluent.Extensions;
using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Lex.Impl
{
    public abstract class LexerStateBase : ILexerState
    {
        protected readonly IForwardReadSequence<char> Sequence;

        public Maybe<string> TokenValue { get; }

        //private Maybe<string> _tokenValue;

        private Maybe<LexerStateBase> LastState { get; }

        public Maybe<ILexerState> Token => LastState.Value;

        //public Maybe<string> ILexerStateTokenValue
        //{
        //    get { return _tokenValue; }
        //}

        protected LexerStateBase(IForwardReadSequence<char> sequence, LexerStateBase lastState)
        {
            Sequence = sequence;
            LastState = lastState.ToMaybe();
            TokenValue = Maybe<string>.Nothing;
        }

        protected LexerStateBase(IForwardReadSequence<char> sequence, char c, LexerStateBase lastState) : this(sequence, lastState)
        {
            TokenValue = c.ToString();
        }

        protected LexerStateBase(IForwardReadSequence<char> sequence, string token, char c, LexerStateBase lastState) : this(sequence, lastState)
        {
            TokenValue = token + c;
        }

        public abstract ILexerState Act();

        public bool TokenComplete => LastState.HasValue && LastState.Value.GetType() != typeof(InitialLexerState) && GetType() != LastState.Value.GetType();

        protected bool IsWhitespace(char c) => char.IsWhiteSpace(c);

        protected bool IsDigit(char c) => char.IsDigit(c);

        protected bool IsSpecialSymbol(char c) =>
            c.Match()
                .With('=', '<', '>', '(', ')', '{', '}', '+', '-', '*', '/', true)
                .Else(false)
                .Do();
    }

    public class InitialLexerState : LexerStateBase
    {
        public InitialLexerState(IForwardReadSequence<char> sequence, LexerStateBase lastState) : base(sequence, lastState) { }

        public override ILexerState Act() =>
            Sequence.Next().Match()
                .With(IsWhitespace, c => (ILexerState)new InitialLexerState(Sequence, this))
                .With(IsDigit, c => new NumberLexerState(Sequence, c, this))
                .With(IsSpecialSymbol, c => new SpecialSymbolLexerState(Sequence, c, this))
                .Else(c => new SymbolLexerState(Sequence, c, this))
                .Do();
    }

    public class SymbolLexerState : LexerStateBase
    {
        public SymbolLexerState(IForwardReadSequence<char> sequence, char c, LexerStateBase lastState) : base(sequence, c, lastState) { }

        public SymbolLexerState(IForwardReadSequence<char> sequence, string token, char c, LexerStateBase lastState) : base(sequence, token, c, lastState) { }

        public override ILexerState Act() => 
            Sequence.Next().Match()
                .With(IsWhitespace, c => (ILexerState)new InitialLexerState(Sequence, this))
                .With(IsSpecialSymbol, c => new SpecialSymbolLexerState(Sequence, c, this))
                .Else(c => new SpecialSymbolLexerState(Sequence, TokenValue, c, this))
                .Do();
    }

    public class NumberLexerState : LexerStateBase
    {
        public NumberLexerState(IForwardReadSequence<char> sequence, char c, LexerStateBase lastState) : base(sequence, c, lastState) { }

        public NumberLexerState(IForwardReadSequence<char> sequence, string token, char c, LexerStateBase lastState) : base(sequence, token, c, lastState) { }

        public override ILexerState Act() =>
            Sequence.Next().Match()
                .With(IsWhitespace, c => (ILexerState)new InitialLexerState(Sequence, this))
                .With(IsDigit, c => new NumberLexerState(Sequence, TokenValue, c, this))
                .With(IsSpecialSymbol, c => new SpecialSymbolLexerState(Sequence, c, this))
                .ElseThrow<ArgumentException>()
                .Do();
    }

    public class SpecialSymbolLexerState : LexerStateBase
    {
        public SpecialSymbolLexerState(IForwardReadSequence<char> sequence, char c, LexerStateBase lastState) : base(sequence, c, lastState) { }

        public SpecialSymbolLexerState(IForwardReadSequence<char> sequence, string token, char c, LexerStateBase lastState) : base(sequence, token, c, lastState) { }

        public override ILexerState Act() =>
            Sequence.Next().Match()
                .With(IsWhitespace, c => (ILexerState)new InitialLexerState(Sequence, this))
                .With(IsDigit, c => new NumberLexerState(Sequence, c, this))
                .With(IsSpecialSymbol, c => new SpecialSymbolLexerState(Sequence, TokenValue, c, this))
                .Else(c => new SymbolLexerState(Sequence, c, this))
                .Do();
    }
}