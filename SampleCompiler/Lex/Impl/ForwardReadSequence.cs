using System;
using System.Collections.Generic;

namespace SampleCompiler.Lex.Impl
{
    public class ForwardReadSequence<T> : IForwardReadSequence<T>
        where T: IEquatable<T>
    {
        private readonly IList<T> _list;

        private int _index = 0;

        private readonly int _length;

        public ForwardReadSequence(IList<T> list)
        {
            _list = list;
            _length = list.Count;
        }

        public bool HasNext() => _index < _length;

        public T Next() => _list[_index++];
    }
}