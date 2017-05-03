using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class LogicalOrExpression
    {
        public MaybeValue<LogicalAndExpression> LogicalAndExpression { get; set; }

        public MaybeValue<LogicalOrExpression> Left { get; set; }

        public MaybeValue<LogicalAndExpression> Right { get; set; }
    }
}