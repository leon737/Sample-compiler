using Functional.Fluent.MonadicTypes;

namespace SampleCompiler.Syntax.Models
{
    public class Statement
    {
        public MaybeValue<ExpressionStatement> ExpressionStatement { get; set; }

        public MaybeValue<CompoundStatement> CompoundStatement { get; set; }

        public MaybeValue<SelectionStatement> SelectionStatement { get; set; }

        public MaybeValue<IterationStatement> IterationStatement { get; set; }

        public MaybeValue<ReturnStatement> ReturnStatement { get; set; }
    }
}