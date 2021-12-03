using Functions.Mathematics.Interface;

namespace Functions.Interface
{
    public interface IFunction
    {
        double Value(IVector point);
    }
}
