using Functions.Interface;
using Functions.Mathematics.Interface;
using System;

namespace Functions
{
    public class DifferentiableFunction : Function, IDifferentiableFunction
    {
        protected readonly Func<IVector, IVector> df;

        public DifferentiableFunction(Func<IVector, double> f, Func<IVector, IVector> df) : base(f)
        {
            this.df = df;
        }

        public IVector Gradient(IVector point) => df(point);
    }
}
