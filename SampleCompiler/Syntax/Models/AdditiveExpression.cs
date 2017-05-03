using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class AdditiveExpression
    {
        public MaybeValue<MultiplicativeExpression> MultiplicativeExpression { get; set; }

        public MaybeValue<Operators> Operator { get; set; }

        public MaybeValue<AdditiveExpression> Left { get; set; }

        public MaybeValue<MultiplicativeExpression> Right { get; set; }
    }
}