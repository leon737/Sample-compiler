using System;

namespace SampleCompiler.Lex
{
    public interface IForwardReadSequence<out T>
        where T: IEquatable<T>
    {
        bool HasNext();

        T Next();
    }
}