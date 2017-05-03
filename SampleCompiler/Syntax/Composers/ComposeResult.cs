using System.Collections.Generic;
using Functional.Fluent.MonadicTypes;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Syntax.Composers
{
    public class ComposeResult<TResult>
    {
        public MaybeValue<TResult> Result { get; set; }

        public ICollection<Token> Tokens { get; set; }
    }
}