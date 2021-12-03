using Functionals.Interface;
using Functions.Interface;
using Functions.Mathematics;
using Functions.Mathematics.Interface;
using OptimizationProblem.Interface;
using System;
using System.Linq;

namespace OptimizationProblem
{
    /// <summary>
    /// Метод градиентного спуска
    /// </summary>
    public class GradientDescentMethod : IOptimizator
    {
        private static readonly MathOperation _math = new MathOperation();

        private double Eps { get; set; }

        private int MaxIteration { get; set; }

        public GradientDescentMethod(int maxIteration, double eps)
        {
            MaxIteration = maxIteration;
            Eps = eps;
        }

        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters, IVector minimumParameters = null, IVector maximumParameters = null)
        {
            if (!(objective is IDifferentiableFunctional differentiableFunctional))
            {
                throw new ArgumentException("This optimizer accept only IDifferentiableFunctional", nameof(objective));
            }

            var golden = new GoldenSectionMethod();

            var xOld = initialParameters.Clone();

            var bindFunction = function.Bind(xOld);
            var fiNew = differentiableFunctional.Gradient(bindFunction);
            var xNew = initialParameters.Clone();
            var k = 0;
            var normOld = Math.Sqrt(_math.Dot(fiNew.ToArray(), fiNew.ToArray()));
            var normNew = 1000d;
            while (k++ < MaxIteration && (_math.Compare(Math.Sqrt(_math.Dot(fiNew.ToArray(), fiNew.ToArray())), Eps) == 1) && (_math.Compare(normNew - normOld, Eps) == 1))
            {
                bindFunction = function.Bind(xNew);
                fiNew = differentiableFunctional.Gradient(bindFunction);
                normOld = normNew;
                normNew = Math.Sqrt(_math.Dot(fiNew.ToArray(), fiNew.ToArray()));

                var gamma = golden.FindMin(differentiableFunctional, function, xOld, fiNew, Eps);

                xOld = xNew.Clone();
                xNew = xOld.Sub(fiNew.MultWithCloning(gamma));
            }

            return xNew;
        }
    }
}
