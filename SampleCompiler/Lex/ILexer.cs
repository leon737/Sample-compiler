using System.Collections.Generic;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Lex
{
    public interface ILexer
    {
        IEnumerable<Token> Parse(string sourceCode);
    }
}