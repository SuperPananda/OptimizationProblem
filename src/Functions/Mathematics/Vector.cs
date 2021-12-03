using Functions.Mathematics.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Functions.Mathematics
{
    public class Vector : IVector
    {
        private readonly double[] _components;

        public Vector(params double[] components)
        {
            var incoming = components.ToArray();
            _components = new double[incoming.Length];
            incoming.ToArray().CopyTo(_components, 0);
            Count = _components.Length;
        }

        public Vector(IEnumerable<double> components)
        {
            var incoming = components.ToArray();
            _components = new double[incoming.Length];
            incoming.ToArray().CopyTo(_components, 0);
            Count = _components.Length;
        }

        public Vector(int size, double initializeValue = default)
        {
            _components = new double[size];

            Count = size;

            if (Equals(initializeValue, default)) return;

            for (var i = 0L; i < _components.LongLength; i++)
            {
                _components[i] = initializeValue;
            }
        }

        public int Count { get; }

        public double this[int index]
        {
            get => Count > index ? _components[index] : throw new ArgumentOutOfRangeException(nameof(index));
            set
            {
                if (Count <= index)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                _components[index] = value;
            }
        }

        public IVector Clone()
        {
            return new Vector(_components);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)_components).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
