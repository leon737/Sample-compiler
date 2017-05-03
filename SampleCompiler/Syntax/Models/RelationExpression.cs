using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class RelationExpression
    {
        public MaybeValue<AdditiveExpression> AdditiveExpression { get; set; }

        public MaybeValue<Operators> Operator { get; set; }

        public MaybeValue<RelationExpression> Left { get; set; }

        public MaybeValue<AdditiveExpression> Right { get; set; }
    }
}