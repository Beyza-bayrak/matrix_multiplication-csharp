using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix_multiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Normal matrix multiplication:\n");

            int[,] matrix1 =
            {
                {1,2,3,4},{2,2,3,4},{1,3,3,4},{1,5,3,4}
            };

            int[,] matrix2 =
            {
                {1,2,3,4},{1,2,3,4},{1,2,3,4},{1,8,3,4}
            };

            int[,] normalResult = MultiplyNormal(matrix1, matrix2);
            PrintMatrix(normalResult);

            Console.WriteLine("Recursive matrix multiplication:\n");
            int[,] recursiveResult = RecursiveMultiply(matrix1, matrix2);
            PrintMatrix(recursiveResult);
        }

        // Normal matrix multiplication
        public static int[,] MultiplyNormal(int[,] A, int[,] B)
        {
            if (A.GetLength(1) != B.GetLength(0))
                throw new Exception("Matrix dimensions are not compatible.");

            int[,] C = new int[A.GetLength(0), B.GetLength(1)];

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < B.GetLength(1); j++)
                {
                    int sum = 0;
                    for (int k = 0; k < A.GetLength(1); k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    C[i, j] = sum;
                }
            }
            return C;
        }

        // Recursive matrix multiplication
        public static int[,] RecursiveMultiply(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] C = new int[n, n];

            if (n == 1)
            {
                C[0, 0] = A[0, 0] * B[0, 0];
                return C;
            }

            int half = n / 2;

            int[,] A11 = SplitMatrix(A, 0, 0, half);
            int[,] A12 = SplitMatrix(A, 0, half, half);
            int[,] A21 = SplitMatrix(A, half, 0, half);
            int[,] A22 = SplitMatrix(A, half, half, half);

            int[,] B11 = SplitMatrix(B, 0, 0, half);
            int[,] B12 = SplitMatrix(B, 0, half, half);
            int[,] B21 = SplitMatrix(B, half, 0, half);
            int[,] B22 = SplitMatrix(B, half, half, half);

            int[,] C11 = AddMatrices(RecursiveMultiply(A11, B11), RecursiveMultiply(A12, B21));
            int[,] C12 = AddMatrices(RecursiveMultiply(A11, B12), RecursiveMultiply(A12, B22));
            int[,] C21 = AddMatrices(RecursiveMultiply(A21, B11), RecursiveMultiply(A22, B21));
            int[,] C22 = AddMatrices(RecursiveMultiply(A21, B12), RecursiveMultiply(A22, B22));

            CombineMatrix(C11, C, 0, 0);
            CombineMatrix(C12, C, 0, half);
            CombineMatrix(C21, C, half, 0);
            CombineMatrix(C22, C, half, half);

            return C;
        }

        static int[,] SplitMatrix(int[,] m, int startRow, int startCol, int size)
        {
            int[,] result = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    result[i, j] = m[startRow + i, startCol + j];
            return result;
        }

        static int[,] AddMatrices(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] C = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    C[i, j] = A[i, j] + B[i, j];
            return C;
        }

        static void CombineMatrix(int[,] source, int[,] target, int rowOffset, int colOffset)
        {
            int n = source.GetLength(0);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    target[rowOffset + i, colOffset + j] = source[i, j];
        }

        public static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write(matrix[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

