using Functions.Mathematics.Interface;
using System;
using System.Linq;

namespace Functions.Mathematics
{
    public static class MatrixOperation
    {
        public static IMatrix Transpose(this IMatrix a)
        {
            var matrixC = new Matrix(a.Columns.Max(), a.Row);

            for (var i = 0; i < a.Row; i++)
            {
                for (var j = 0; j < a.Columns[i]; j++)
                {
                    matrixC[j, i] = a[i, j];
                }
            }

            return matrixC;
        }

        public static IMatrix Mult(this IMatrix a, IMatrix b)
        {
            if (a.Columns.Max() != b.Row)
            {
                throw new Exception("Количество столбцов не равно количеству строк");
            }

            var matrixC = new Matrix(a.Row, b.Columns.Max());

            for (var i = 0; i < a.Row; i++)
            {
                for (var j = 0; j < b.Columns[i]; j++)
                {
                    for (var k = 0; k < a.Columns.Count; k++)
                    {
                        matrixC[i, j] = matrixC[i, j] + (a[i, k] * b[k, j]);
                    }
                }
            }

            return matrixC;
        }

        public static IMatrix CreateSubMatrix(this IMatrix matrix, int excludingRow, int excludingCol)
        {
            var mat = new Matrix(matrix.Row - 1, matrix.Columns.Count - 1);
            var r = -1;

            for (var i = 0; i < matrix.Row; i++)
            {
                if (i == excludingRow)
                {
                    continue;
                }

                r++;
                var c = -1;

                for (var j = 0; j < matrix.Columns.Count; j++)
                {
                    if (j == excludingCol)
                        continue;

                    mat[r, ++c] = matrix[i, j];
                }
            }

            return mat;
        }

        public static int ChangeSign(int i) => i % 2 == 0 ? 1 : -1;

        public static double Determinant(this IMatrix matrix)
        {
            if (matrix.Row != matrix.Columns.Count)
                throw new Exception("Матрица должна быть квадратной");

            if (matrix.Row == 1 && matrix.Columns.Count == 1)
            {
                return matrix[0, 0];
            }

            if (matrix.Row == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            double sum = 0d;

            for (int i = 0; i < matrix.Columns.Count; i++)
            {
                sum = sum + (double)ChangeSign(i) * matrix[0, i] * Determinant(CreateSubMatrix(matrix, 0, i));
            }

            return sum;
        }

        public static IMatrix Cofactor(this IMatrix matrix)
        {
            var mat = new Matrix(matrix.Row, matrix.Columns.Count);

            for (var i = 0; i < matrix.Row; i++)
            {
                for (var j = 0; j < matrix.Columns.Count; j++)
                {
                    mat[i, j] = ChangeSign(i) * ChangeSign(j) * Determinant(CreateSubMatrix(matrix, i, j));
                }
            }

            return mat;
        }

        public static IMatrix Mult(this IMatrix matrix, double constant)
        {
            for (var i = 0; i < matrix.Row; i++)
            {
                for (var j = 0; j < matrix.Columns.Count; j++)
                {
                    matrix[i, j] = matrix[i][j] * constant;
                }
            }
            return matrix;
        }

        public static IMatrix MultWithCopy(this IMatrix matrix, double constant)
        {
            return matrix.Clone().Mult(constant);
        }

        public static IMatrix Inverse(this IMatrix matrix)
        {
            return Transpose(Cofactor(matrix)).MultWithCopy(1.0 / (Determinant(matrix) + double.Epsilon));
            
        }

        public static IVector Mult(this IMatrix a, IVector b)
        {
            if (a.Columns.Max() != b.Count)
            {
                throw new Exception("Количество столбцов не равно количеству в векторе");
            }

            var vector = new Vector(a.Row);

            for (var i = 0; i < a.Row; i++)
            {
                for (var j = 0; j < a.Columns[i]; j++)
                {
                    vector[i] = vector[i] + a[i, j] * b[j];
                }
            }

            return vector;
        }
    }
}
