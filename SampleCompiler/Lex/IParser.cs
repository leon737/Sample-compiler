using SampleCompiler.Lex.Models;

namespace SampleCompiler.Lex
{
    public interface IParser
    {
        bool Is(string value);

        Token Make(string value);
    }
}