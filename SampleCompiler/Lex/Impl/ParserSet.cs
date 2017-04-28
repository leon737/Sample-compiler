namespace SampleCompiler.Lex.Impl
{
    public class ParserSet : IParserSet
    {
        public ParserSet(IParser operatorParser, IParser keywordParser, IParser symbolParser)
        {
            OperatorParser = operatorParser;
            KeywordParser = keywordParser;
            SymbolParser = symbolParser;
        }

        public IParser OperatorParser { get; }

        public IParser KeywordParser { get; }

        public IParser SymbolParser { get; }
    }
}