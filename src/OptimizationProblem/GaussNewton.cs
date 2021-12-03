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
    /// Алгоритм Гаусса-Ньютона
    /// </summary>
    public class GaussNewton : IOptimizator
    {
        private static readonly MathOperation _math = new MathOperation();

        private double Eps { get; set; }

        private int MaxIteration { get; set; }

        public GaussNewton(int maxIteration, double eps)
        {
            MaxIteration = maxIteration;
            Eps = eps;
        }

        public IVector Minimize(IFunctional objective, IParametricFunction function, IVector initialParameters, IVector minimumParameters = null, IVector maximumParameters = null)
        {
            if (!(objective is ILeastSquaresFunctional leastSquaresFunctional))
            {
                throw new ArgumentException("This optimizer accept only IDifferentiableFunctional", nameof(objective));
            }

            var golden = new GoldenSectionMethod();

            var x = initialParameters.Clone();

            var bindFunction = function.Bind((x));

            var iteration = 0;

            var residual = leastSquaresFunctional.Residual(bindFunction);

            var error = Math.Sqrt(_math.Dot(residual.ToArray(), residual.ToArray()));

            var oldError = 1000d;

            while (_math.Compare(error - oldError, Eps) == 1 || iteration++ < MaxIteration)
            {
                var jacobi = leastSquaresFunctional.Jacobian(bindFunction);

                var jacobiT = jacobi.Transpose();

                var jTj = jacobiT.Mult(jacobi);

                var jTj_1 = jTj.Inverse();

                var jTj_1jT = jTj_1.Mult(jacobiT);

                var temp = jTj_1jT.Mult(residual);

                var gamma = golden.FindMin(leastSquaresFunctional, function, x, temp, Eps);

                x.Sub(temp.Mult(gamma));

                oldError = error;

                bindFunction = function.Bind(x);

                residual = leastSquaresFunctional.Residual(bindFunction);

                error = Math.Sqrt(_math.Dot(residual.ToArray(), residual.ToArray()));
            }

            return x;
        }
    }
}
