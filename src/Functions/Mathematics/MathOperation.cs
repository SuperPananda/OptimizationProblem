using System;
using System.Linq;

namespace Functions.Mathematics
{
    public class MathOperation
    {
        public double Sum<TIn>(TIn[] a, Func<TIn, double> f) => a.Aggregate(0d, (x, y) => x + f(y));

        public double Sign(double a) => a >= 0 ? 1 : -1;

        public double Dot(double[] a, double[] b)
        {
            if (a.Length != b.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            return a.Select((t, i) => b[i] * t).Sum();
        }

        public double Max<TIn>(TIn[] a, Func<TIn, double> f) => a.Max(f);

        public int Compare(double a, double b)
        {
            if (a < b)
            {
                return -1;
            }

            return a > b ? 1 : 0;
        }
    }
}
