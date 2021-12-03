using Functions.Mathematics.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Functions.Mathematics
{
    public class Matrix : IMatrix
    {
        private readonly IVector[] _components;

        public Matrix(int rows, int columns, params double[] defaultValuesForRows)
        {
            if (rows < 0)
            {
                throw new ArgumentException("Количество строк не должно быть отрицательным", nameof(rows));
            }

            if (columns < 0)
            {
                throw new ArgumentException("Количество столбцов не должно быть отрицательным", nameof(columns));
            }

            var valuesForRows = defaultValuesForRows?.ToArray();

            if (valuesForRows.Length != 0 && valuesForRows.Length != rows)
            {
                throw new ArgumentException("Мало элементов", nameof(defaultValuesForRows));
            }

            _components = new IVector[rows];

            if (valuesForRows.Length == 0)
            {
                for (var i = 0L; i < rows; i++)
                {
                    _components[i] = new Vector(columns);
                }
            }
            else
            {
                for (var i = 0; i < rows; i++)
                {
                    _components[i] = new Vector(columns, valuesForRows[i]);
                }
            }

            Row = _components.Length;
            Columns = new List<int>(_components.Select(x => x.Count).ToArray());
        }

        public Matrix(params IVector[] components)
        {
            var incoming = components.ToArray();

            this._components = new IVector[incoming.LongCount()];

            for (var i = 0; i < this._components.LongLength; i++)
            {
                this._components[i] = incoming.ElementAt(i).Clone();
            }

            Row = this._components.Length;
            Columns = new List<int>(incoming.Select(x => x.Count()).ToArray());
        }

        public Matrix(IEnumerable<IVector> components)
        {
            var incoming = components.ToArray();

            this._components = new IVector[incoming.LongCount()];

            for (var i = 0; i < this._components.LongLength; i++)
            {
                this._components[i] = incoming.ElementAt(i).Clone();
            }

            Row = this._components.Length;
            Columns = new List<int>(incoming.Select(x => x.Count()).ToArray());
        }

        public int Row { get; }

        public List<int> Columns { get; }

        public double this[int row, int column]
        {
            get => Row > row && Columns[row] > column
                ? _components[row][column]
                : throw new ArgumentOutOfRangeException(nameof(row) + " or " + nameof(column));
            set
            {
                if (Row <= row || Columns[row] <= column)
                {
                    throw new ArgumentOutOfRangeException(nameof(row) + " or " + nameof(column));
                }

                _components[row][column] = value;
            }
        }

        public IMatrix Clone()
        {
            return new Matrix(_components);
        }

        public IVector this[int row]
        {
            get =>
                Row > row ? _components[row] : throw new ArgumentOutOfRangeException(nameof(row));
            set
            {
                if (Row <= row)
                {
                    throw new ArgumentOutOfRangeException(nameof(row));
                }

                _components[row] = value;
            }
        }
    }
}
