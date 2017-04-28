using Functional.Fluent.Extensions;
using Functional.Fluent.MonadicTypes;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Lex.Impl
{
    public class OperatorParser : ParserBase<Operators, OperatorToken>
    {
        protected override Result<Operators> MakeImpl(string value) =>
            value.Match()
            .With("=", Operators.Assignment.ToMaybe())
            .With("==", Operators.Equals)
            .With("!=", Operators.NotEquals)
            .With("<=", Operators.LessOrEqual)
            .With("<", Operators.LessThan)
            .With(">=", Operators.GreaterOrEqual)
            .With(">", Operators.GreaterThan)
            .With("&&", Operators.LogicalAnd)
            .With("||", Operators.LogicalOr)
            .With("*", Operators.Multiply)
            .With("/", Operators.Divide)
            .With("+", Operators.Plus)
            .With("-", Operators.Minus)
            .Else(Maybe<Operators>.Nothing)
            .Do()
            .ToResult();

        protected override OperatorToken MakeToken(Operators value) => new OperatorToken { Operator = value };
    }
}