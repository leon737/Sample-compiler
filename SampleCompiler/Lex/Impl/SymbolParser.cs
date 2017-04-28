using Functional.Fluent.Extensions;
using Functional.Fluent.MonadicTypes;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Lex.Impl
{
    public class SymbolParser : ParserBase<Symbols, SymbolToken>
    {
        protected override Result<Symbols> MakeImpl(string value) =>
            value.Match()
                .With("{", Symbols.OpenBlock.ToMaybe())
                .With("}", Symbols.CloseBlock)
                .With("(", Symbols.OpenParenthesis)
                .With(")", Symbols.CloseParenthesis)
                .Else(Maybe<Symbols>.Nothing)
                .Do()
                .ToResult();

        protected override SymbolToken MakeToken(Symbols value) => new SymbolToken { Symbol = value };
    }
}