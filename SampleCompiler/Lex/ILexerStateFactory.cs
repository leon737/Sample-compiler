namespace SampleCompiler.Lex
{
    public interface ILexerStateFactory
    {
        ILexerState CreateInitialState(IForwardReadSequence<char> sequence);

        ILexerState CreateTailToken(IForwardReadSequence<char> sequence, ILexerState state);
    }
}