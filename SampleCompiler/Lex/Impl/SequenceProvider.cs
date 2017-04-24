namespace SampleCompiler.Lex.Impl
{
    public class SequenceProvider: ISequenceProvider<string, char>
    {
        public IForwardReadSequence<char> CreateSequence(string source) => new ForwardReadSequence<char>(source.ToCharArray());
    }
}