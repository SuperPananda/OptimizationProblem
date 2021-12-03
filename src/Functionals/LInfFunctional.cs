using Functionals.Interface;
using Functions.Interface;
using Functions.Mathematics;
using Functions.Mathematics.Interface;
using System;
using System.Linq;

namespace Functionals
{
    /// <summary>
    /// linf норма разности с требуемыми значениями в наборе точек
    /// </summary>
    public class LInfFunctional : IFunctional
    {
        private static readonly MathOperation _math = new MathOperation();

        private readonly (IVector point, double value)[] _items;

        public LInfFunctional(params (IVector, double)[] points)
        {
            var received = points.ToArray();

            if (!received.Any())
            {
                throw new Exception("Нужно хотя бы одно значение");
            }

            _items = new (IVector, double)[received.LongLength];
            received.ToArray().CopyTo(_items, 0);
        }

        public double Value(IFunction function)
        {
            return _math.Max(_items, (x) => Math.Abs(function.Value(x.point) - x.value));
        }
    }
}
