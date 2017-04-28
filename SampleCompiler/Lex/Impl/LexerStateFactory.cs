namespace SampleCompiler.Lex.Impl
{
    public class LexerStateFactory : ILexerStateFactory
    {
        public ILexerState CreateInitialState(IForwardReadSequence<char> sequence, IParserSet parsers) => new InitialLexerState(sequence, null, parsers);

        public ILexerState CreateTailToken(IForwardReadSequence<char> sequence, ILexerState state, IParserSet parsers) => new InitialLexerState(sequence, (LexerStateBase)state, parsers);
    }
}