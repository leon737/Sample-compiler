using Functional.Fluent.Extensions;
using Functional.Fluent.MonadicTypes;
using SampleCompiler.Lex.Models;

namespace SampleCompiler.Lex.Impl
{
    public abstract class ParserBase<TEnum, TToken> : IParser
        where TToken : Token
    {
        public virtual bool Is(string value) => MakeImpl(value).IsSucceed;

        public virtual Token Make(string value) => MakeImpl(value).SuccessValue(MakeToken);

        protected abstract Result<TEnum> MakeImpl(string value);

        protected abstract TToken MakeToken(TEnum value);
    }
}