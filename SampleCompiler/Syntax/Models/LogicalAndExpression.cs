using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class LogicalAndExpression
    {
        public MaybeValue<EqualityExpression> EqualityExpression { get; set; }

        public MaybeValue<LogicalAndExpression> Left { get; set; }

        public MaybeValue<EqualityExpression> Right { get; set; }
    }
}