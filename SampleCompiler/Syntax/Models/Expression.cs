using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class Expression
    {
        public MaybeValue<Expression> ConditionalExpression { get; set; }

        public MaybeValue<Identifier> LValue { get; set; }

        public MaybeValue<Expression> RValue { get; set; }
    }
}