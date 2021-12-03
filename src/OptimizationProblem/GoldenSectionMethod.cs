using Functionals.Interface;
using Functions.Interface;
using System;
using Functions.Mathematics.Interface;
using Functions.Mathematics;

namespace OptimizationProblem
{
    public class GoldenSectionMethod
    {
        private static readonly MathOperation _math = new MathOperation();

        public double FindMin(IFunctional functional, IParametricFunction function, IVector s, IVector p, double eps)
        {
            var a = 0.0;
            var b = 1e2;

            var x = a + (0.5 * (3 - Math.Sqrt(5)) * (b - a));

            var y = (b - x) + a;

            var fx = function.Bind(s.AddWithCloning(p.MultWithCloning(x * -1.0)));

            var fy = function.Bind(s.AddWithCloning(p.MultWithCloning(y * -1.0)));

            while (_math.Compare(Math.Abs(b - a), 1e-5) == 1)
            {
                var valueX = functional.Value(fx);

                var valueY = functional.Value(fy);

                if (_math.Compare(valueX, valueY) == -1)
                {
                    b = y;
                    y = x;
                    fy = fx;
                    x = (b + a) - y;
                    var addWithCloning = s.AddWithCloning(p.MultWithCloning(x * -1));
                    fx = function.Bind(addWithCloning);
                }
                else
                {
                    a = x;
                    x = y;
                    fx = fy;
                    y = (b + a) - x;

                    fy = function.Bind(s.AddWithCloning(p.MultWithCloning(y * -1)));
                }
            }
            return (a + b) / (2 + double.Epsilon);
        }
    }
}
