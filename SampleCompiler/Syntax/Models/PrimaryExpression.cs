using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class PrimaryExpression
    {
        public MaybeValue<Identifier> Identifier { get; set; }

        public MaybeValue<int> IntegerConstant { get; set; }

        public MaybeValue<Expression> Expression { get; set; }
    }
}