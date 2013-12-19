using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransCAM
{
    public class Regression
    {
        public static double[] DoRegression(double[,] X, double[] y)
        {
            var XTXInv = Inverse(MatrixMultiple(Transpose(X), X));
            var XTXInvXT = MatrixMultiple(XTXInv, Transpose(X));
            double[,] Y_temp = new double[y.Length, 1];
            for (int i = 0; i < y.Length; i++)
                Y_temp[i, 0] = y[i];
            var theta = MatrixMultiple(XTXInvXT, Y_temp);
            double[] betas = new double[X.GetUpperBound(1)+1];
            for (int i = 0; i < theta.GetUpperBound(0) + 1; i++)
                betas[i] = theta[i, 0];
            return betas;
        }

        protected static double[,] MatrixMultiple(double[,] A, double[,] B)
        {
            long rows_a = A.GetUpperBound(0) + 1;
            long rows_b = B.GetUpperBound(0) + 1;
            long cols_a = A.GetUpperBound(1) + 1;
            long cols_b = B.GetUpperBound(1) + 1;
            if (cols_a != rows_b)
                throw new Exception("Improper dimensions for multiplication");
            double[,] C = new double[rows_a, cols_b];
            for (int row = 0; row < rows_a; row++)
            {
                for (int col = 0; col < cols_b; col++)
                {
                    C[row, col] = 0.0;
                    for (int i = 0; i < cols_a; i++)
                    {
                        C[row, col] += A[row, i] * B[i, col];
                    }
                }
            }
            return C;
        }

        protected static double[,] Transpose(double[,] matrix)
        {
            long rows = matrix.GetUpperBound(0) + 1;
            long cols = matrix.GetUpperBound(1) + 1;
            double[,] t = new double[cols, rows];
            for (long r = 0; r < cols; r++)
            {
                for (long c = 0; c < rows; c++)
                {
                    t[r, c] = matrix[c, r];
                }
            }
            return t;
        }

        protected static double[,] Duplicate(double[,] matrix)
        {
            long rows = matrix.GetUpperBound(0) + 1;
            long cols = matrix.GetUpperBound(1) + 1;
            double[,] d = new double[cols, rows];
            for (long r = 0; r < rows; r++)
            {
                for (long c = 0; c < cols; c++)
                {
                    d[c, r] = matrix[c, r];
                }
            }
            return d;
        }

        protected static double[,] Inverse(double[,] matrix)
        {
            // Doolittle's algorithm first for decomposition
            int rows = matrix.GetUpperBound(0) + 1;
            int cols = matrix.GetUpperBound(1) + 1; ; 
            if (rows != cols)
                throw new Exception("Not a square matrix");

            double[,] A = Duplicate(matrix); // our decomposition will lead to the upper matrix
            int[] P = new int[rows];  
            for (int i = 0; i < rows; i++) { P[i] = i; }

            //for each column
            for (int n = 0; n < cols-1; n++) 
            {
                //Find largest value in row
                double max = 0;
                int pivot = -1;
                double a_in;
                for (int i = n + 1; i < rows; i++)
                {
                    a_in = Math.Abs(A[i,n]);
                    if (a_in > max)
                    {
                        max = a_in;
                        pivot = i;
                    }
                }

                if (pivot != n) 
                {
                    //switch rows
                    for (int j = 0; j < cols; j++)
                    {
                        double tmp = A[pivot, j];
                        A[pivot, j] = A[n, j];
                        A[n, j] = tmp;
                    }

                    //Keep track of how the rows have been switched
                    int tmp2 = P[pivot]; 
                    P[pivot] = P[n];
                    P[n] = tmp2;
                }

                double a_nn = A[n,n];
                if (Math.Abs(a_nn) < Double.Epsilon) // if diagonal after swap is zero . . .
                    throw new Exception("Matrix is singular.");

                for (int i=n+1; i < rows; i++)
                {
                    double l_in = - A[i,n] / a_nn;
                    A[i, n] = - l_in;
                    for (int p = n + 1; p < rows; p++)
                    {
                        A[i, p] += l_in * A[n,p];
                    }
                }
            } // main j loop

            //Decomposed, now do inverse
            double[,] A_inv = Duplicate(matrix);
            double[] F = new double[rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; ++j)
                {
                    if (i == P[j])
                        F[j] = 1.0;
                    else
                        F[j] = 0.0;
                }

                double[] x = Solve(A, F); // 

                for (int j = 0; j < rows; ++j)
                    A_inv[j,i] = x[j];
            }

            return A_inv;
        }

        /// <summary>
        /// Solve LUx = F 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="F"></param>
        /// <returns></returns>
        static double[] Solve(double[,] LU, double[] F) 
        {
            double[] x = new double[F.Length];
            F.CopyTo(x, 0);

            // solve Ly = F 
            for (int i = 1; i < F.Length; i++)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                {
                    sum -= LU[i,j] * x[j];
                }
                x[i] = sum;
            }

            //solve Ux = y 
            x[F.Length - 1] /= LU[F.Length - 1,F.Length - 1];
            for (int i = F.Length - 2; i >= 0; i--)
            {
                double sum = x[i];
                for (int j = i + 1; j < F.Length; j++)
                {
                    sum -= LU[i,j] * x[j];
                }
                x[i] = sum / LU[i,i];
            }

            return x;
        } 
    }
}
