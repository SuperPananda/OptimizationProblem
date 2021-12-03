using Functionals.Interface;
using Functions.Interface;
using Functions.Mathematics;
using Functions.Mathematics.Interface;
using System;
using System.Linq;

namespace Functionals
{
    /// <summary>
    /// l1 норма разности с требуемыми значениями в наборе точек
    /// </summary>
    public class L1Functional : IDifferentiableFunctional
    {
        private static readonly MathOperation _math = new MathOperation();

        private readonly (IVector point, double value)[] _items;

        public L1Functional(params (IVector, double)[] points)
        {
            var received = points.ToArray();

            if (!received.Any())
            {
                throw new Exception("Нужно хотя бы одно значение");
            }

            _items = new (IVector, double)[received.LongLength];
            received.ToArray().CopyTo(_items, 0);
        }

        public IVector Gradient(IFunction function)
        {
            if (!(function is IDifferentiableFunction f))
            {
                throw new Exception("Функция должна быть IDifferentiableFunction");
            }

            var sub = f.Value(_items[0].point) - _items[0].value;

            var first = f.Gradient(_items[0].point).Mult(_math.Sign(sub));

            return _items.Skip(1).Aggregate(first, (prev, curr) => prev.Add(f.Gradient(curr.point)
                                                         .Mult(_math.Sign(f.Value(curr.point) - curr.value))));
        }

        public double Value(IFunction function)
        {
            return _math.Sum(_items, x => Math.Abs(function.Value(x.point) - x.value));
        }
    }
}
