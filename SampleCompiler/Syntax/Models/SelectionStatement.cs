using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class SelectionStatement
    {
        public Expression Expression { get; set; }

        public Statement Statement { get; set; }

        public MaybeValue<Statement> ElseStatement { get; set; }
    }
}