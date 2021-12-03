using Functions.Interface;
using Functions.Mathematics.Interface;
using System;

namespace Functions
{
    /// <summary>
    /// Полином n-й степени в одномерном пространстве
    /// </summary>
    public class Polynomial : IParametricFunction
    {
        public double Value(IVector parameters, IVector point)
        {
            if (point.Count != 1)
            {
                throw new ArgumentException("Point dimension must be 1", nameof(point));
            }

            var sum = 0d;

            for (var i = 0; i < parameters.Count; i++)
            {
                sum += parameters[i] * Math.Pow(point[0], parameters.Count - i - 1);
            }

            return sum;
        }

        public IFunction Bind(IVector parameters) => new Function(point => Value(parameters, point));
    }
}
