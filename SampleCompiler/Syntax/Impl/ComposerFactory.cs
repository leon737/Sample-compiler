using Functional.Fluent.Extensions;
using Functional.Fluent.Helpers;
using SampleCompiler.Syntax.Composers;
using SampleCompiler.Syntax.Composers.Impl;
using SampleCompiler.Syntax.Models;

namespace SampleCompiler.Syntax.Impl
{
    public class ComposerFactory : IComposerFactory
    {
        public IComposer<TResult> Create<TResult>() => 
            (IComposer<TResult>)Funcs.Get<IComposerFactory, IComposer>(this).ToM().With(f => typeof(TResult).Match()
                .With(typeof(TranslationUnit), _ => f.New<TranslationUnitComposer>())
                .Do())
            .Value;
    }
}