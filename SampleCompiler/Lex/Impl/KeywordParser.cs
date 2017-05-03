using Functional.Fluent.Extensions;
using Functional.Fluent.MonadicTypes;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Lex.Impl
{
    public class KeywordParser : ParserBase<Keywords, KeywordToken>
    {
        protected override Result<Keywords> MakeImpl(string value) =>
            value.Match()
                .With("return", Keywords.Return.ToMaybe())
                .With("if", Keywords.If)
                .With("else", Keywords.Else)
                .With("while", Keywords.While)
                .With("var", Keywords.Var)
                .Else(Maybe<Keywords>.Nothing)
                .Do()
                .ToResult();

        protected override KeywordToken MakeToken(Keywords value) => new KeywordToken { Keyword = value };
    }
}