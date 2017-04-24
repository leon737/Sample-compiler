using System;

namespace SampleCompiler.Lex
{
    public interface IForwardReadSequence<T>
        where T: IEquatable<T>
    {
        bool HasNext();

        T Next();

        T LookAhead(T c);
        
    }
}