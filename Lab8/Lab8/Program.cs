using System;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] v1 = new int[] { 3, 5, 1 };
            int[] v2 = new int[] { -2, 4, -1 };

            int dot = Matrix.DotProduct(v1, v2); // dot: 13

            int[,] matrix = new int[4, 6]
            {
                { 4, 6, 1, 0, 9, 2 },
                { 1, -2, 4, 5, 5, 9 },
                { 2, -8, -2, 1, -5, 2 },
                { 10, -10, 7, 7, 9, 5 },
            };
            int[,] transposed = Matrix.Transpose(matrix);

            int[,] matrix2 = new int[,]
            {
                { 2, 4, 6, 7 },
                { 4, -1, 5, 6 },
                { -5, 6, 1, 1 }
            };

            int[] row = Matrix.GetRowOrNull(matrix2, 0);
            int[] col = Matrix.GetColumnOrNull(matrix2, 1);

            int[] vector = new int[] { 1, -1, 5, 3 };
            int[] product = Matrix.MultiplyMatrixVectorOrNull(matrix2, vector); // product: [ 49, 48, -3 ]

            int[,] matrix3 = new int[,]
            {
                { 2, 4, -5 },
                { 4, -1, 6 },
                { 6, 5, 1 },
                { 7, 6, 1 }

            };
            int[] product2 = Matrix.MultiplyVectorMatrixOrNull(vector, matrix3); // product: [ 49, 48, -3 ]

            int[,] multiplicand = new int[2, 3]
            {
                { 2, 3, 1 },
                { 1, 4, 3 }
            };

            int[,] multiplier = new int[3, 2]
            {
                { 3, 4 },
                { 1, 1 },
                { 2, 5 }
            };

            int[,] product3 = Matrix.MultiplyOrNull(multiplicand, multiplier);
            // product:
            /*
            11     16
            13     23
            */


            int[,] A = new int[2, 2]
            {
                { 1, 2 },
                { 3, 4 }
            };

            int[,] B = new int[2, 2]
            {
                { 1, 2 },
                { 3, 4 }
            };

            int[,] product4 = Matrix.MultiplyOrNull(A, B);


            int[,] product5 = Matrix.GetIdentityMatrix(3);

        }
    }
}
