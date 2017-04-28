using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Lex
{
    public interface ILexerState
    {
        Maybe<ILexerState> Token { get; }

        Maybe<string> TokenValue { get; }
            
        ILexerState Act();

        ILexerState Trim();

        bool TokenComplete { get; }
    }
}