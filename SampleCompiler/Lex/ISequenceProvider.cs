using System;

namespace SampleCompiler.Lex
{
    public interface ISequenceProvider<in TSource, TTarget>
        where TTarget: IEquatable<TTarget>
    {
        IForwardReadSequence<TTarget> CreateSequence(TSource source);
    }
}