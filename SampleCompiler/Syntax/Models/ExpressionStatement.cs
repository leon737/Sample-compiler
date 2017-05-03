using System.Collections.Generic;
using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class ExpressionStatement
    {
        public MaybeValue<Expression> Expression { get; set; }
    }
}