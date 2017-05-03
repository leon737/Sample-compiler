using SampleCompiler.Syntax.Composers;

namespace SampleCompiler.Syntax
{
    public interface IComposerFactory
    {
        IComposer<TResult> Create<TResult>();
    }
}