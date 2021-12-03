using Functionals.Interface;
using Functions.Interface;
using Functions.Mathematics.Interface;

namespace OptimizationProblem.Interface
{
    public interface IOptimizator
    {
        IVector Minimize(IFunctional objective,
                         IParametricFunction function,
                         IVector initialParameters,
                         IVector minimumParameters = default,
                         IVector maximumParameters = default);
    }
}
