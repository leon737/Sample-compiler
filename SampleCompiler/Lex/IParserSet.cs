namespace SampleCompiler.Lex
{
    public interface IParserSet
    {
        IParser OperatorParser { get; }

        IParser KeywordParser { get; }

        IParser SymbolParser { get; }
    }
}