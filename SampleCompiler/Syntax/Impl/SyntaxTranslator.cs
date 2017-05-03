using System.Collections.Generic;
using SampleCompiler.Lex.Models;
using SampleCompiler.Syntax.Models;

namespace SampleCompiler.Syntax.Impl
{
    public class SyntaxTranslator : ISyntaxTranslator
    {
        private readonly IComposerFactory _composerFactory;

        public SyntaxTranslator(IComposerFactory composerFactory)
        {
            _composerFactory = composerFactory;
        }

        public TranslationUnit Translate(ICollection<Token> tokens) => _composerFactory.Create<TranslationUnit>().Compose(tokens).Result.As();
    }
}