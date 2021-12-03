using Functionals;
using Functions;
using Functions.Mathematics;
using System;
using System.Text.Json;

namespace OptimizationProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            var point1 = new Vector(0d);
            var point2 = new Vector(2d);
            var point3 = new Vector(3d);
            var point4 = new Vector(1d);

            var functional = new L1Functional((point1, 1d), (point2, 9d), (point3, 16d), (point4, 4d));
            var optimizer = new MonteCarloMethod(1000, 1e-14);
            var value = optimizer.Minimize(functional, new LinearFunction(), new Vector(new[] { 1d, 0d }));

            Console.WriteLine(JsonSerializer.Serialize(value));
        }
    }
}
