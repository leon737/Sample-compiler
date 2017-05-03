using System.Collections.Generic;

namespace SampleCompiler.Helpers
{
    public static class InfiniteSequence
    {
        public static IEnumerable<T> Create<T>()
        {
            for (;;)
                yield return default(T);
        }
    }
}