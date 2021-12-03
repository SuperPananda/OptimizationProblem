using Functionals.Interface;
using Functions.Interface;
using Functions.Mathematics;
using Functions.Mathematics.Interface;
using System;
using System.Linq;

namespace Functionals
{
    /// <summary>
    /// l2 норма разности с требуемыми значениями в наборе точек
    /// </summary>
    public class L2Functional : IDifferentiableFunctional, ILeastSquaresFunctional
    {
        private static readonly MathOperation _math = new MathOperation();

        private readonly (IVector point, double value)[] _items;

        public L2Functional(params (IVector, double)[] points)
        {
            var received = points.ToArray();

            if (!received.Any())
            {
                throw new ArgumentException("Need at least one item");
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

            var result = f.Gradient(_items[0].point).Mult(f.Value(_items[0].point) - _items[0].value);

            return _items.Skip(1).Aggregate(result, (prev, curr) => prev.Add(f.Gradient(curr.point).Mult(f.Value(curr.point) - curr.value))).Div((Value(f)));
        }

        public IMatrix Jacobian(IFunction function)
        {
            if (!(function is IDifferentiableFunction f))
            {
                throw new Exception("Функция должна быть IDifferentiableFunction");
            } 

            return new Matrix(_items.Select(x => f.Gradient(x.point)));
        }

        public IVector Residual(IFunction function)
        {
            if (!(function is IDifferentiableFunction f))
            {
                throw new Exception("Функция должна быть IDifferentiableFunction");
            }

            return new Vector(_items.Select(x => f.Value(x.point) - x.value));
        }

        public double Value(IFunction function)
        {
            return Math.Pow(_math.Sum(_items, x => Math.Pow(function.Value(x.point) - x.value, 2)), 0.5d);
        }
    }
}
