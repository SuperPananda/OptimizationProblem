using Functions.Interface;
using Functions.Mathematics.Interface;

namespace Functionals.Interface
{
    public interface IDifferentiableFunctional : IFunctional
    {
        IVector Gradient(IFunction function);
    }
}
