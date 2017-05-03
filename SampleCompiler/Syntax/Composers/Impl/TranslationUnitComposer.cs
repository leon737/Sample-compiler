using System.Collections.Generic;
using System.Linq;
using Functional.Fluent.Extensions;
using SampleCompiler.Helpers;
using SampleCompiler.Lex.Models;
using SampleCompiler.Syntax.Models;

namespace SampleCompiler.Syntax.Composers.Impl
{
    public class TranslationUnitComposer : ComposerBase, ITranslationUnitComposer
    {
        public TranslationUnitComposer(IComposerFactory composerFactory) : base(composerFactory) { }

        public ComposeResult<TranslationUnit> Compose(ICollection<Token> tokens)
        {
            var result = new TranslationUnit();

            var declarationComposer = ComposerFactory.Create<Declaration>();
            result.Declarations = InfiniteSequence.Create<int>()
                .Select(v =>
                {
                    var r = declarationComposer.Compose(tokens);
                    tokens = r.Tokens;
                    return r.Result;
                })
                .TakeWhile(v => v.HasValue)
                .Select(v => v.Value)
                .ToList();

            var statementComposer = ComposerFactory.Create<Statement>();
            result.Statements = InfiniteSequence.Create<int>()
                .Select(v =>
                {
                    var r = statementComposer.Compose(tokens);
                    tokens = r.Tokens;
                    return r.Result;
                })
                .TakeWhile(v => v.HasValue)
                .Select(v => v.Value)
                .ToList();

            return new ComposeResult<TranslationUnit> {Result = result.ToMaybe(), Tokens = tokens};
        }
    }
}