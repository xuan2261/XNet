// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XNet.XMath
{
    public delegate double MatrixMapFunction(double x);

    public partial class Matrix
    {
        public int rows;
        public int cols;
        public double[] mat;

        public Matrix L;
        public Matrix U;
        private int[] pi;
        private double detOfP = 1;

        // Matrix Class constructor
        public Matrix(int iRows, int iCols)
        {
            rows = iRows;
            cols = iCols;
            mat = new double[rows * cols];
        }

        public Matrix(double[,] matrix, bool rowMajor = true)
        {
            // NOTE copies elements
            if (rowMajor)
            {
                rows = matrix.GetLength(0);
                cols = matrix.GetLength(1);
                mat = matrix.Cast<double>().ToArray();
            }
            // column-major
            else
            {
                rows = matrix.GetLength(1);
                cols = matrix.GetLength(0);
                mat = new double[rows * cols];

                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        this[row, col] = matrix[matrix.GetLowerBound(0) + col, matrix.GetLowerBound(1) + row];
                    }
                }
            }
        }

        public bool IsSquare()
        {
            return (rows == cols);
        }

        // Access this matrix as a 2D array
        public double this[int iRow, int iCol]
        {
            get { return mat[iRow * cols + iCol]; }
            set { mat[iRow * cols + iCol] = value; }
        }

        public Matrix GetCol(int k)
        {
            Matrix m = new Matrix(rows, 1);
            for (int i = 0; i < rows; i++)
            {
                m[i, 0] = this[i, k];
            }

            return m;
        }

        public void SetCol(Matrix v, int k)
        {
            for (int i = 0; i < rows; i++)
            {
                this[i, k] = v[i, 0];
            }
        }

        // Function for LU decomposition
        public void MakeLU()
        {
            if (!IsSquare())
            {
                throw new MatrixException("The matrix is not square!");
            }

            L = GenerateIdentityMatrix(rows, cols);
            U = Duplicate();

            pi = new int[rows];
            for (int i = 0; i < rows; i++) pi[i] = i;

            double p = 0;
            double pom2;
            int k0 = 0;
            int pom1 = 0;

            for (int k = 0; k < cols - 1; k++)
            {
                p = 0;
                // find the row with the biggest pivot
                for (int i = k; i < rows; i++)
                {
                    if (System.Math.Abs(U[i, k]) > p)
                    {
                        p = System.Math.Abs(U[i, k]);
                        k0 = i;
                    }
                }
                if (p == 0)
                {
                    throw new MatrixException("The matrix is singular!");
                }

                // switch two rows in permutation matrix
                pom1 = pi[k]; pi[k] = pi[k0]; pi[k0] = pom1;

                for (int i = 0; i < k; i++)
                {
                    pom2 = L[k, i]; L[k, i] = L[k0, i]; L[k0, i] = pom2;
                }

                if (k != k0) detOfP *= -1;

                // Switch rows in U
                for (int i = 0; i < cols; i++)
                {
                    pom2 = U[k, i]; U[k, i] = U[k0, i]; U[k0, i] = pom2;
                }

                for (int i = k + 1; i < rows; i++)
                {
                    L[i, k] = U[i, k] / U[k, k];
                    for (int j = k; j < cols; j++)
                    {
                        U[i, j] = U[i, j] - L[i, k] * U[k, j];
                    }
                }
            }
        }

        // Function solves Ax = v in confirmity with solution vector "v"
        public Matrix SolveWith(Matrix v)
        {
            if (rows != cols) throw new MatrixException("The matrix is not square!");
            if (rows != v.rows) throw new MatrixException("Wrong number of results in solution vector!");
            if (v.cols != 1) throw new MatrixException("The solution vector v must be a column vector");
            if (L == null) MakeLU();

            Matrix b = new Matrix(rows, 1);
            // switch two items in "v" due to permutation matrix
            for (int i = 0; i < rows; i++)
            {
                b[i, 0] = v[pi[i], 0];
            }

            Matrix z = SubsForth(L, b);
            Matrix x = SubsBack(U, z);

            return x;
        }

        // TODO check for redundancy with MakeLU() and SolveWith()
        public void MakeRref()
        {
            // Function makes reduced echolon form

            int lead = 0;
            for (int r = 0; r < rows; r++)
            {
                if (cols <= lead) break;
                int i = r;
                while (this[i, lead] == 0)
                {
                    i++;
                    if (i == rows)
                    {
                        i = r;
                        lead++;
                        if (cols == lead)
                        {
                            lead--;
                            break;
                        }
                    }
                }
                for (int j = 0; j < cols; j++)
                {
                    double temp = this[r, j];
                    this[r, j] = this[i, j];
                    this[i, j] = temp;
                }
                double div = this[r, lead];
                for (int j = 0; j < cols; j++) this[r, j] /= div;
                for (int j = 0; j < rows; j++)
                {
                    if (j != r)
                    {
                        double sub = this[j, lead];
                        for (int k = 0; k < cols; k++) this[j, k] -= (sub * this[r, k]);
                    }
                }
                lead++;
            }
        }

        // Function returns the inverted matrix
        public Matrix Invert()
        {
            if (L == null) MakeLU();

            Matrix inv = new Matrix(rows, cols);

            for (int i = 0; i < rows; i++)
            {
                Matrix Ei = Matrix.GenerateZeroMatrix(rows, 1);
                Ei[i, 0] = 1;
                Matrix col = SolveWith(Ei);
                inv.SetCol(col, i);
            }
            return inv;
        }

        // Function for determinant
        public double Det()
        {
            if (L == null) MakeLU();
            double det = detOfP;
            for (int i = 0; i < rows; i++)
            {
                det *= U[i, i];
            }

            return det;
        }

        // Function returns permutation matrix "P" due to permutation vector "pi"
        public Matrix GetP()
        {
            if (L == null) MakeLU();

            Matrix matrix = GenerateZeroMatrix(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                matrix[pi[i], i] = 1;
            }
            return matrix;
        }

        // Function returns the copy of this matrix
        public Matrix Duplicate()
        {
            Matrix matrix = new Matrix(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = this[i, j];
                }
            }
            return matrix;
        }

        // Function returns matrix as a string
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    s.Append(String.Format("{0,5:E2}", this[i, j]) + " ");
                }
                s.AppendLine();
            }
            return s.ToString();
        }

        public Matrix Dot(Matrix m)
        {
            return Multiply(this, m);
        }

        public Matrix ElementMul(Matrix m)
        {
            if ((cols != m.cols) || (rows != m.rows))
            {
                throw new MatrixException("Wrong dimension of matrix!");
            }

            Matrix result = Duplicate();
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    result[i, j] = m[i, j] * this[i, j];
                }
            }
            return result;
        }
    }

    public partial class Matrix
    {

        // Function solves Ax = b for A as a lower triangular matrix
        public static Matrix SubsForth(Matrix A, Matrix b)
        {
            if (A.L == null) A.MakeLU();
            int n = A.rows;
            Matrix x = new Matrix(n, 1);

            for (int i = 0; i < n; i++)
            {
                x[i, 0] = b[i, 0];
                for (int j = 0; j < i; j++)
                {
                    x[i, 0] -= A[i, j] * x[j, 0];
                }
                x[i, 0] = x[i, 0] / A[i, i];
            }
            return x;
        }

        // Function solves Ax = b for A as an upper triangular matrix
        public static Matrix SubsBack(Matrix A, Matrix b)
        {
            if (A.L == null) A.MakeLU();
            int n = A.rows;
            Matrix x = new Matrix(n, 1);

            for (int i = n - 1; i > -1; i--)
            {
                x[i, 0] = b[i, 0];
                for (int j = n - 1; j > i; j--)
                {
                    x[i, 0] -= A[i, j] * x[j, 0];
                }
                x[i, 0] = x[i, 0] / A[i, i];
            }
            return x;
        }

        // Function generates the zero matrix
        public static Matrix GenerateZeroMatrix(int iRows, int iCols)
        {
            Matrix matrix = new Matrix(iRows, iCols);
            for (int i = 0; i < iRows; i++)
            {
                for (int j = 0; j < iCols; j++)
                {
                    matrix[i, j] = 0;
                }
            }
            return matrix;
        }

        // Function generates the identity matrix
        public static Matrix GenerateIdentityMatrix(int iRows, int iCols)
        {
            Matrix matrix = GenerateZeroMatrix(iRows, iCols);
            for (int i = 0; i < System.Math.Min(iRows, iCols); i++)
            {
                matrix[i, i] = 1;
            }
            return matrix;
        }

        // Function generates the random matrix
        public static Matrix RandomMatrix(int iRows, int iCols, int dispersion, EDistrubution distrubution)
        {
            if (distrubution == EDistrubution.Invalid)
            {
                throw new MatrixException("Invalid Random Distribution Mode!");
            }
            else if (distrubution == EDistrubution.Uniform)
            {
                Random random = new Random();
                Matrix matrix = new Matrix(iRows, iCols);
                for (int i = 0; i < iRows; i++)
                {
                    for (int j = 0; j < iCols; j++)
                    {
                        matrix[i, j] = random.Next(-dispersion, dispersion);
                    }
                }
                return matrix;
            }
            else if (distrubution == EDistrubution.Gaussian)
            {
                Random random = new Random(); //reuse this if you are generating many

                Matrix matrix = new Matrix(iRows, iCols);


                for (int i = 0; i < iRows; i++)
                {
                    for (int j = 0; j < iCols; j++)
                    {
                        //uniform(0,1] random doubles
                        double u1 = 1.0 - random.NextDouble();
                        double u2 = 1.0 - random.NextDouble();

                        //random normal(0,1)
                        double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) * System.Math.Sin(2.0 * System.Math.PI * u2);

                        //random normal(mean,stdDev^2)
                        double randNormal = 0 + dispersion * randStdNormal;


                        matrix[i, j] = randNormal;
                    }
                }

            }
            throw new MatrixException("Invalid Random Distribution Mode!");
        }

        // Function parses the matrix from string
        public static Matrix Parse(string matstr)
        {
            string s = NormalizeMatrixString(matstr);
            string[] rows = Regex.Split(s, "\r\n");
            string[] nums = rows[0].Split(' ');
            Matrix matrix = new Matrix(rows.Length, nums.Length);
            try
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    nums = rows[i].Split(' ');
                    for (int j = 0; j < nums.Length; j++)
                    {
                        matrix[i, j] = double.Parse(nums[j]);
                    }
                }
            }
            catch (FormatException)
            {
                throw new MatrixException("Wrong input format!");
            }
            return matrix;
        }

        // Matrix transpose, for any rectangular matrix
        public static Matrix Transpose(Matrix m)
        {
            Matrix t = new Matrix(m.cols, m.rows);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    t[j, i] = m[i, j];
                }
            }
            return t;
        }

        // Power matrix to exponent
        public static Matrix Power(Matrix m, int pow)
        {
            if (pow == 0)
            {
                return GenerateIdentityMatrix(m.rows, m.cols);
            }
            if (pow == 1)
            {
                return m.Duplicate();
            }
            if (pow == -1)
            {
                return m.Invert();
            }

            Matrix x;
            if (pow < 0) { x = m.Invert(); pow *= -1; }
            else x = m.Duplicate();

            Matrix ret = GenerateIdentityMatrix(m.rows, m.cols);
            while (pow != 0)
            {
                if ((pow & 1) == 1) ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        private static void SafeAplusBintoC(Matrix A, int xa, int ya, Matrix B, int xb, int yb, Matrix C, int size)
        {
            // rows
            for (int i = 0; i < size; i++)
            {
                // cols
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = 0;
                    if (xa + j < A.cols && ya + i < A.rows)
                    {
                        C[i, j] += A[ya + i, xa + j];
                    }
                    if (xb + j < B.cols && yb + i < B.rows)
                    {
                        C[i, j] += B[yb + i, xb + j];
                    }
                }
            }
        }

        private static void SafeAminusBintoC(Matrix A, int xa, int ya, Matrix B, int xb, int yb, Matrix C, int size)
        {
            // rows
            for (int i = 0; i < size; i++)
            {
                // cols
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = 0;
                    if (xa + j < A.cols && ya + i < A.rows)
                    {
                        C[i, j] += A[ya + i, xa + j];
                    }
                    if (xb + j < B.cols && yb + i < B.rows)
                    {
                        C[i, j] -= B[yb + i, xb + j];
                    }
                }
            }
        }

        private static void SafeACopytoC(Matrix A, int xa, int ya, Matrix C, int size)
        {
            // rows
            for (int i = 0; i < size; i++)
            {
                // cols
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = 0;
                    if (xa + j < A.cols && ya + i < A.rows)
                    {
                        C[i, j] += A[ya + i, xa + j];
                    }
                }
            }
        }

        private static void AplusBintoC(Matrix A, int xa, int ya, Matrix B, int xb, int yb, Matrix C, int size)
        {
            // rows
            for (int i = 0; i < size; i++)
            {
                // cols
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = A[ya + i, xa + j] + B[yb + i, xb + j];
                }
            }
        }

        private static void AminusBintoC(Matrix A, int xa, int ya, Matrix B, int xb, int yb, Matrix C, int size)
        {
            // rows
            for (int i = 0; i < size; i++)
            {
                // cols
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = A[ya + i, xa + j] - B[yb + i, xb + j];
                }
            }
        }

        private static void ACopytoC(Matrix A, int xa, int ya, Matrix C, int size)
        {
            // rows
            for (int i = 0; i < size; i++)
            {
                // cols
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = A[ya + i, xa + j];
                }
            }
        }

        // TODO assume matrix 2^N x 2^N and then directly call StrassenMultiplyRun(A,B,?,1,?)
        // Smart matrix multiplication
        private static Matrix StrassenMultiply(Matrix A, Matrix B)
        {
            if (A.cols != B.rows)
            {
                throw new MatrixException("Wrong dimension of matrix!");
            }
            Matrix R;

            int msize = System.Math.Max(System.Math.Max(A.rows, A.cols), System.Math.Max(B.rows, B.cols));

            int size = 1; int n = 0;
            while (msize > size) { size *= 2; n++; };
            int h = size / 2;


            Matrix[,] mField = new Matrix[n, 9];

            /*
             *  8x8, 8x8, 8x8, ...
             *  4x4, 4x4, 4x4, ...
             *  2x2, 2x2, 2x2, ...
             *  . . .
             */

            int z;
            // rows
            for (int i = 0; i < n - 4; i++)
            {
                z = (int)System.Math.Pow(2, n - i - 1);
                for (int j = 0; j < 9; j++) mField[i, j] = new Matrix(z, z);
            }

            SafeAplusBintoC(A, 0, 0, A, h, h, mField[0, 0], h);
            SafeAplusBintoC(B, 0, 0, B, h, h, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 1], 1, mField); // (A11 + A22) * (B11 + B22);

            SafeAplusBintoC(A, 0, h, A, h, h, mField[0, 0], h);
            SafeACopytoC(B, 0, 0, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 2], 1, mField); // (A21 + A22) * B11;

            SafeACopytoC(A, 0, 0, mField[0, 0], h);
            SafeAminusBintoC(B, h, 0, B, h, h, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 3], 1, mField); //A11 * (B12 - B22);

            SafeACopytoC(A, h, h, mField[0, 0], h);
            SafeAminusBintoC(B, 0, h, B, 0, 0, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 4], 1, mField); //A22 * (B21 - B11);

            SafeAplusBintoC(A, 0, 0, A, h, 0, mField[0, 0], h);
            SafeACopytoC(B, h, h, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 5], 1, mField); //(A11 + A12) * B22;

            SafeAminusBintoC(A, 0, h, A, 0, 0, mField[0, 0], h);
            SafeAplusBintoC(B, 0, 0, B, h, 0, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 6], 1, mField); //(A21 - A11) * (B11 + B12);

            SafeAminusBintoC(A, h, 0, A, h, h, mField[0, 0], h);
            SafeAplusBintoC(B, 0, h, B, h, h, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 7], 1, mField); // (A12 - A22) * (B21 + B22);

            // result
            R = new Matrix(A.rows, B.cols);

            /// C11
            // rows
            for (int i = 0; i < System.Math.Min(h, R.rows); i++)
            {
                // cols
                for (int j = 0; j < System.Math.Min(h, R.cols); j++)
                {
                    R[i, j] = mField[0, 1 + 1][i, j] + mField[0, 1 + 4][i, j] - mField[0, 1 + 5][i, j] + mField[0, 1 + 7][i, j];
                }
            }

            /// C12
            // rows
            for (int i = 0; i < System.Math.Min(h, R.rows); i++)
            {
                // cols
                for (int j = h; j < System.Math.Min(2 * h, R.cols); j++)
                {
                    R[i, j] = mField[0, 1 + 3][i, j - h] + mField[0, 1 + 5][i, j - h];
                }
            }

            /// C21
            // rows
            for (int i = h; i < System.Math.Min(2 * h, R.rows); i++)
            {
                // cols
                for (int j = 0; j < System.Math.Min(h, R.cols); j++)
                {
                    R[i, j] = mField[0, 1 + 2][i - h, j] + mField[0, 1 + 4][i - h, j];
                }
            }

            /// C22
            // rows
            for (int i = h; i < System.Math.Min(2 * h, R.rows); i++)
            {
                // cols
                for (int j = h; j < System.Math.Min(2 * h, R.cols); j++)
                {
                    R[i, j] = mField[0, 1 + 1][i - h, j - h] - mField[0, 1 + 2][i - h, j - h] + mField[0, 1 + 3][i - h, j - h] + mField[0, 1 + 6][i - h, j - h];
                }
            }
            return R;
        }

        // A * B into C, level of recursion, matrix field
        private static void StrassenMultiplyRun(Matrix A, Matrix B, Matrix C, int l, Matrix[,] f)
        {
            int size = A.rows;
            int h = size / 2;

            AplusBintoC(A, 0, 0, A, h, h, f[l, 0], h);
            AplusBintoC(B, 0, 0, B, h, h, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 1], l + 1, f); // (A11 + A22) * (B11 + B22);

            AplusBintoC(A, 0, h, A, h, h, f[l, 0], h);
            ACopytoC(B, 0, 0, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 2], l + 1, f); // (A21 + A22) * B11;

            ACopytoC(A, 0, 0, f[l, 0], h);
            AminusBintoC(B, h, 0, B, h, h, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 3], l + 1, f); //A11 * (B12 - B22);

            ACopytoC(A, h, h, f[l, 0], h);
            AminusBintoC(B, 0, h, B, 0, 0, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 4], l + 1, f); //A22 * (B21 - B11);

            AplusBintoC(A, 0, 0, A, h, 0, f[l, 0], h);
            ACopytoC(B, h, h, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 5], l + 1, f); //(A11 + A12) * B22;

            AminusBintoC(A, 0, h, A, 0, 0, f[l, 0], h);
            AplusBintoC(B, 0, 0, B, h, 0, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 6], l + 1, f); //(A21 - A11) * (B11 + B12);

            AminusBintoC(A, h, 0, A, h, h, f[l, 0], h);
            AplusBintoC(B, 0, h, B, h, h, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 7], l + 1, f); // (A12 - A22) * (B21 + B22);

            /// C11
            // rows
            for (int i = 0; i < h; i++)
            {
                // cols
                for (int j = 0; j < h; j++)
                {
                    C[i, j] = f[l, 1 + 1][i, j] + f[l, 1 + 4][i, j] - f[l, 1 + 5][i, j] + f[l, 1 + 7][i, j];
                }
            }

            /// C12
            // rows
            for (int i = 0; i < h; i++)
            {
                // cols
                for (int j = h; j < size; j++)
                {
                    C[i, j] = f[l, 1 + 3][i, j - h] + f[l, 1 + 5][i, j - h];
                }
            }
            /// C21
            // rows
            for (int i = h; i < size; i++)
            {
                // cols
                for (int j = 0; j < h; j++)
                {
                    C[i, j] = f[l, 1 + 2][i - h, j] + f[l, 1 + 4][i - h, j];
                }
            }

            /// C22
            // rows
            for (int i = h; i < size; i++)
            {
                // cols
                for (int j = h; j < size; j++)
                {
                    C[i, j] = f[l, 1 + 1][i - h, j - h] - f[l, 1 + 2][i - h, j - h] + f[l, 1 + 3][i - h, j - h] + f[l, 1 + 6][i - h, j - h];
                }
            }
        }
        // Simple matrix multiplication
        private static Matrix SimpleMul(Matrix m1, Matrix m2)
        {
            if (m1.cols != m2.rows)
            {
                throw new MatrixException("Wrong dimensions of matrix!");
            }

            Matrix result = GenerateZeroMatrix(m1.rows, m2.cols);
            for (int i = 0; i < result.rows; i++)
            {
                for (int j = 0; j < result.cols; j++)
                {
                    for (int k = 0; k < m1.cols; k++)
                    {
                        result[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return result;
        }

        // Matrix multiplication
        private static Matrix Multiply(Matrix m1, Matrix m2)
        {
            if (m1.cols != m2.rows)
            {
                throw new MatrixException("Wrong dimension of matrix!");
            }

            int msize = System.Math.Max(System.Math.Max(m1.rows, m1.cols), System.Math.Max(m2.rows, m2.cols));

            // simple multiplication faster for small matrices
            if (msize < 32)
            {
                return SimpleMul(m1, m2);
            }
            // simple multiplication faster for non square matrices
            if (!m1.IsSquare() || !m2.IsSquare())
            {
                return SimpleMul(m1, m2);
            }
            // Strassen multiplication is faster for large square matrix 2^N x 2^N
            // NOTE because of previous checks msize == m1.cols == m1.rows == m2.cols == m2.cols
            double exponent = System.Math.Log(msize) / System.Math.Log(2);
            if (System.Math.Pow(2, exponent) == msize)
            {
                return StrassenMultiply(m1, m2);
            }
            else
            {
                return SimpleMul(m1, m2);
            }
        }

        // Multiplication by constant n
        private static Matrix Multiply(double n, Matrix m)
        {
            Matrix r = new Matrix(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    r[i, j] = m[i, j] * n;
                }
            }
            return r;
        }

        // Addition
        private static Matrix Add(Matrix m1, Matrix m2)
        {
            if (m1.rows != m2.rows || m1.cols != m2.cols)
            {
                throw new MatrixException("Matrices must have the same dimensions!");
            }

            Matrix r = new Matrix(m1.rows, m1.cols);
            for (int i = 0; i < r.rows; i++)
            {
                for (int j = 0; j < r.cols; j++)
                {
                    r[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return r;
        }

        // From Andy - thank you! :)
        public static string NormalizeMatrixString(string matStr)
        {
            // Remove any multiple spaces
            while (matStr.IndexOf("  ") != -1)
            {
                matStr = matStr.Replace("  ", " ");
            }

            // Remove any spaces before or after newlines
            matStr = matStr.Replace(" \r\n", "\r\n");
            matStr = matStr.Replace("\r\n ", "\r\n");

            // If the data ends in a newline, remove the trailing newline.
            // Make it easier by first replacing \r\n’s with |’s then
            // restore the |’s with \r\n’s
            matStr = matStr.Replace("\r\n", "|");
            while (matStr.LastIndexOf("|") == (matStr.Length - 1))
            {
                matStr = matStr.Substring(0, matStr.Length - 1);
            }

            matStr = matStr.Replace("|", "\r\n");
            return matStr.Trim();
        }

        // Matrix - Matrix
        public static Matrix operator-(Matrix m)
        {
            return Multiply(-1, m);
        }

        // Matrix + Matrix
        public static Matrix operator+(Matrix m1, Matrix m2)
        {
            return Add(m1, m2);
        }

        // Matrix - Matrix
        public static Matrix operator-(Matrix m1, Matrix m2)
        {
            return Add(m1, -m2);
        }

        // Matrix * Matrix
        public static Matrix operator*(Matrix m1, Matrix m2)
        {
            return Multiply(m1, m2);
        }

        // Scaler * Matrix
        public static Matrix operator *(double n, Matrix m)
        {
            return Multiply(n, m);
        }

        // Maximum Norm
        public static double MaximumNorm(Matrix m)
        {
            double result = 0;
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    double value = m[i, j];

                    if (System.Math.Abs(value) > result)
                    {
                        result = System.Math.Abs(value);
                    }
                }
            }
            return result;
        }

        // Manhattan Norm
        public static double ManhattanNorm(Matrix m)
        {
            double result = 0;
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    double value = m[i, j];
                    result += System.Math.Abs(value);
                }
            }
            return result;
        }

        // Taxicab Norm
        public static double TaxicabNorm(Matrix m)
        {
            double result = 0;
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    double value = m[i, j];
                    result += System.Math.Abs(value);
                }
            }
            return result;
        }

        // PNorm
        public static double PNorm(Matrix m, double p)
        {
            double result = 0;
            if (p < 1)
            {
                for (int i = 0; i < m.rows; i++)
                {
                    for (int j = 0; j < m.cols; j++)
                    {
                        double value = m[i, j];
                        result += System.Math.Abs(value);
                    }
                }
                return result;
            }

            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    double value = m[i, j];
                    result += System.Math.Pow(System.Math.Abs(value), p);
                }
            }

            return System.Math.Pow(result, 1.0 / p);
        }

        // Euclidean Norm (Frobenius Norm)
        public static double EuclideanNorm(Matrix m)
        {
            double result = 0;
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    double value = m[i, j];
                    result += value * value;
                }
            }
            return System.Math.Sqrt(result);
        }

        // Frobenius Norm (Euclidean Norm)
        public static double FrobeniusNorm(Matrix m)
        {
            double result = 0;
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    double value = m[i, j];
                    result += value * value;
                }
            }
            return System.Math.Sqrt(result);
        }

        // Absolute Norm
        public static double AbsoluteNorm(Matrix m)
        {
            double result = 0;
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    double value = m[i, j];
                    result += System.Math.Abs(value);
                }
            }
            return result;
        }

        // Map
        public static Matrix Map(Matrix m, MatrixMapFunction func)
        {
            Matrix res = m.Duplicate();
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    res[i, j] = func(res[i, j]);
                }
            }
            return res;
        }
    }

    //  The class for exceptions
    public class MatrixException : Exception
    {
        public MatrixException(string Message)
            : base(Message)
        { }
    }
}