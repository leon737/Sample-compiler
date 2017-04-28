namespace SampleCompiler.Lex
{
    public interface ILexerStateFactory
    {
        ILexerState CreateInitialState(IForwardReadSequence<char> sequence, IParserSet parsers);

        ILexerState CreateTailToken(IForwardReadSequence<char> sequence, ILexerState state, IParserSet parsers);
    }
}