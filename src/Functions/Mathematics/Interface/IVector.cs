using System.Collections.Generic;

namespace Functions.Mathematics.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVector : IEnumerable<double>
    {
        int Count { get; }

        double this[int index] { get; set; }

        IVector Clone();
    }
}
