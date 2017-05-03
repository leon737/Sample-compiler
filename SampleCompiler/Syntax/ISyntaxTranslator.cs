using System.Collections.Generic;
using SampleCompiler.Lex.Models;
using SampleCompiler.Syntax.Models;

namespace SampleCompiler.Syntax
{
    public interface ISyntaxTranslator
    {
        TranslationUnit Translate(ICollection<Token> tokens);
    }
}
