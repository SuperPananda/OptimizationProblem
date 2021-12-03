using Functions.Interface;
using Functions.Mathematics;
using Functions.Mathematics.Interface;
using System;
using System.Linq;

namespace Functions
{
    /// <summary>
    /// Линейная функция в n-мерном пространстве
    /// </summary>
    public class LinearFunction : IParametricFunction
    {
        private static readonly MathOperation _math = new MathOperation();

        public IVector Gradient(IVector parameters, IVector point)
        {
            if (point.Count != parameters.Count - 1)
            {
                throw new ArgumentException(nameof(point));
            }

            return new Vector(parameters.Select((x, i) => i == parameters.Count - 1 ? 1d : point[i]));
        }

        public double Value(IVector parameters, IVector point)
        {
            if (point.Count != parameters.Count - 1)
            {
                throw new ArgumentException(nameof(point));
            }

            return parameters[parameters.Count - 1] + _math.Dot(point.ToArray(), parameters.Take(parameters.Count - 1).ToArray());
        }

        public IFunction Bind(IVector parameters) => new DifferentiableFunction(point => Value(parameters, point),
                                                                                point => Gradient(parameters, point));
    }
}
