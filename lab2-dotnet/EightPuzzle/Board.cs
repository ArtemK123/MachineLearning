using System;
using System.Collections.Generic;

namespace EightPuzzle
{
    internal readonly struct Board : IEquatable<Board>
    {
        private readonly int[,] matrix;

        public Board(int[,] matrix)
        {
            this.matrix = matrix;
        }

        public static Board FinalBoard { get; } = new Board(new[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 0 }
        });

        public int RowsCount => matrix.GetLength(0);

        public int ColumnsCount => matrix.GetLength(1);

        public IReadOnlyCollection<Board> GetNeighbors()
        {
            (int, int) zeroPosition = GetPosition(matrix, 0);

            List<Board> neighbors = new List<Board>();

            if (zeroPosition.Item1 > 0)
            {
                neighbors.Add(new Board(Swap(matrix, zeroPosition, (zeroPosition.Item1 - 1, zeroPosition.Item2)))); // shift up
            }

            if (zeroPosition.Item1 < 2)
            {
                neighbors.Add(new Board(Swap(matrix, zeroPosition, (zeroPosition.Item1 + 1, zeroPosition.Item2)))); // shift bottom
            }

            if (zeroPosition.Item2 > 0)
            {
                neighbors.Add(new Board(Swap(matrix, zeroPosition, (zeroPosition.Item1, zeroPosition.Item2 - 1)))); // shift left
            }

            if (zeroPosition.Item2 < 2)
            {
                neighbors.Add(new Board(Swap(matrix, zeroPosition, (zeroPosition.Item1, zeroPosition.Item2 + 1)))); // shift right
            }

            return neighbors;
        }

        public int GetValue(int i, int j)
        {
            return matrix[i, j];
        }

        public bool Equals(Board other)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != other.matrix[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Board other && Equals(other);
        }

        public override int GetHashCode()
        {
            int hash = 0;

            // 9^0*matrix[0, 0] + 9^1*matrix[0, 1] + 9^2*matrix[0, 2]
            // 9^3*matrix[1, 0] + 9^4*matrix[1, 1] + 9^5*matrix[1, 2]
            // 9^6*matrix[2, 0] + 9^7*matrix[2, 1] + 9^8*matrix[2, 2]

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int power = i * 3 + j;
                    int multiplier = (int)Math.Pow(9, power);
                    hash += multiplier * matrix[i, j];
                }
            }

            return hash;
        }

        public override string ToString()
        {
            string result = string.Empty;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string line = string.Empty;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    line += $"{matrix[i, j]}, ";
                }

                result += line + Environment.NewLine;
            }

            return result;
        }

        private static int[,] Swap(int[,] originMatrix, (int, int) cell1, (int, int) cell2)
        {
            int[,] modifiedMatrix = new int[originMatrix.GetLength(0), originMatrix.GetLength(1)];
            Array.Copy(originMatrix, modifiedMatrix, originMatrix.Length);

            modifiedMatrix[cell1.Item1, cell1.Item2] = originMatrix[cell2.Item1, cell2.Item2];
            modifiedMatrix[cell2.Item1, cell2.Item2] = originMatrix[cell1.Item1, cell1.Item2];

            return modifiedMatrix;
        }

        private static (int, int) GetPosition(int[,] matrix, int value)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        return (i, j);
                    }
                }
            }

            throw new ArgumentException($"{value} is not found in matrix");
        }
    }
}