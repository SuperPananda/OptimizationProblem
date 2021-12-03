using System.Collections.Generic;

namespace Functions.Mathematics.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMatrix
    {
        int Row { get; }

        List<int> Columns { get; }

        IVector this[int index] { get; set; }

        double this[int row, int column] { get; set; }

        IMatrix Clone();
    }
}
