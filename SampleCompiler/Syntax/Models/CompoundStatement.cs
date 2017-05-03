using System.Collections.Generic;

namespace SampleCompiler.Syntax.Models
{
    public class CompoundStatement
    {
        public ICollection<Declaration> Declarations { get; set; }

        public ICollection<Statement> Statements { get; set; }
    }
}