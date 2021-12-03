using Functions.Mathematics.Interface;

namespace Functions.Interface
{
    public interface IDifferentiableFunction : IFunction
    {
        IVector Gradient(IVector point);
    }
}
