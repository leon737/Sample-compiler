using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class EqualityExpression
    {
        public MaybeValue<RelationExpression> RelationExpression { get; set; }

        public MaybeValue<Operators> Operator { get; set; }

        public MaybeValue<EqualityExpression> Left { get; set; }

        public MaybeValue<RelationExpression> Right { get; set; }
    }
}