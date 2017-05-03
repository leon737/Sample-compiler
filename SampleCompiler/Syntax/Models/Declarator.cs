using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class Declarator
    {
        public Identifier Identifier { get; set; }

        public MaybeValue<Expression> Expression { get; set; }
    }
}