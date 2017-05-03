namespace SampleCompiler.Syntax.Composers.Impl
{
    public abstract class ComposerBase : IComposer
    {
        protected readonly IComposerFactory ComposerFactory;

        protected ComposerBase(IComposerFactory composerFactory)
        {
            ComposerFactory = composerFactory;
        }
    }
}