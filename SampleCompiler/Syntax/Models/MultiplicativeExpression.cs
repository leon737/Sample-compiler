namespace SampleCompiler.Syntax.Models
{
    public class MultiplicativeExpression
    {
        public Operators Operator { get; set; }

        public MultiplicativeExpression Left { get; set; }

        public PrimaryExpression Right { get; set; }
    }
}