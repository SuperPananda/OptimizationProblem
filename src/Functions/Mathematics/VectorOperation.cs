using Functions.Mathematics.Interface;
using System;

namespace Functions.Mathematics
{
    public static class VectorOperation
    {
        public static IVector Mult(this IVector a, double b)
        {
            for (var i = 0; i < a.Count; i++)
            {
                a[i] = a[i] * b;
            }

            return a;
        }

        public static IVector Div(this IVector a, double b)
        {
            for (var i = 0; i < a.Count; i++)
            {
                a[i] = a[i] / (b + double.Epsilon);
            }

            return a;
        }


        public static IVector Add(this IVector a, IVector b)
        {
            if (a.Count != b.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < a.Count; i++)
            {
                a[i] = a[i] + b[i];
            }

            return a;
        }

        public static IVector Sub(this IVector a, IVector b)
        {
            if (a.Count != b.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (var i = 0; i < a.Count; i++)
            {
                a[i] = a[i] - b[i];
            }

            return a;
        }

        public static IVector AddWithCloning(this IVector a, IVector b)
        {
            return a.Clone().Add(b);
        }

        public static IVector MultWithCloning(this IVector a, double b)
        {
            return a.Clone().Mult(b);
        }
    }
}
