namespace SampleCompiler.Lex.Impl
{
    public class LexerStateFactory : ILexerStateFactory
    {
        public ILexerState CreateInitialState(IForwardReadSequence<char> sequence) => new InitialLexerState(sequence, null);

        public ILexerState CreateTailToken(IForwardReadSequence<char> sequence, ILexerState state) => new InitialLexerState(sequence, (LexerStateBase)state);
    }
}