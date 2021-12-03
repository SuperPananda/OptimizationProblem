using Functions.Interface;
using Functions.Mathematics.Interface;

namespace Functionals.Interface
{
    public interface ILeastSquaresFunctional : IFunctional
    {
        IVector Residual(IFunction function);

        IMatrix Jacobian(IFunction function);
    }
}
