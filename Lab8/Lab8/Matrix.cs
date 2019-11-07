namespace Lab8
{
    public static class Matrix
    {
        public static int DotProduct(int[] v1, int[] v2)
        {
            int docProduct = 0;
            for (int i = 0; i < v1.Length; i++)
            {
                docProduct += v1[i] * v2[i];
            }
            return docProduct;
        }
        public static int[,] Transpose(int[,] matrix)
        {
            int[,] transposed = new int[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    transposed[j, i] = matrix[i, j];
                }

            }
            return transposed;
        }

        public static int[] GetRowOrNull(int[,] matrix, int row)
        {
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);
            if (0 <= row && row < rowCount)
            {
                int[] ret = new int[colCount];
                for (int i = 0; i < colCount; i++)
                {
                    ret[i] = matrix[row, i];
                }
                return ret;
            }
            else
            {
                return null;
            }
        }
        public static int[] GetColumnOrNull(int[,] matrix, int col)
        {
            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);
            if (0 <= col && col < colCount)
            {
                int[] ret = new int[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    ret[i] = matrix[i, col];
                }
                return ret;
            }
            else
            {
                return null;
            }
        }
        public static int[] MultiplyMatrixVectorOrNull(int[,] matrix, int[] vector)
        {
            if (matrix.GetLength(1) != vector.Length)
            {
                return null;
            }

            int[] ret = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                ret[i] = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    ret[i] += matrix[i, j] * vector[j];
                }
            }
            return ret;
        }
        public static int[] MultiplyVectorMatrixOrNull(int[] vector, int[,] matrix)
        {
            if (matrix.GetLength(0) != vector.Length)
            {
                return null;
            }

            int[] ret = new int[matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                ret[i] = 0;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    ret[i] += vector[j] * matrix[j, i];
                }
            }
            return ret;
        }
        public static int[,] MultiplyOrNull(int[,] multiplicandMatrix, int[,] multiplierMatrix)
        {
            if (multiplicandMatrix.GetLength(1) != multiplierMatrix.GetLength(0))
            {
                return null;
            }

            int row = 0;
            int col = 0;
            int[,] ret = new int[multiplicandMatrix.GetLength(0), multiplierMatrix.GetLength(1)];
            while (true)
            {
                ret[row, col] = 0;
                for (int i = 0; i < multiplicandMatrix.GetLength(1); i++)
                {
                    for (int j = 0; j < multiplierMatrix.GetLength(0); j++)
                    {
                        ret[row, col] += multiplicandMatrix[row, j] * multiplierMatrix[j, col];
                    }
                    if (col < multiplierMatrix.GetLength(1) - 1)
                    {
                        col++;
                    }
                    else
                    {
                        col = 0;
                        row++;
                    }
                    if (row == multiplicandMatrix.GetLength(0))
                    {
                        return ret;
                    }
                }
            }
        }
    }
}
