using System.Collections.Generic;
using Functional.Fluent.MonadicTypes;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Syntax.Composers
{
    public interface IComposer
    {
        
    }

    public interface IComposer<TResult> : IComposer
    {
        ComposeResult<TResult> Compose(ICollection<Token> tokens);
    }
}