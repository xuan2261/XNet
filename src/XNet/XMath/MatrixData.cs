using System.Collections.Generic;

/******************************************\
    
    XNet MatrixData Architecture:
    
    List<List<Matrix> mat;
    mat[0] = Feature Matrix;
        mat[0][0] = W;
        mat[0][1] = X;
        mat[0][2] = b;
        
    mat[1] = Gradients Matrix;
        mat[1][0] = W_grad;
        mat[1][1] = X_grad;
        mat[1][2] = b_grad;

    mat[2] = Utility Matrix;
        mat[2][0] = dA
    
\******************************************/


namespace XNet.XMath
{
    public class MatrixData
    {
        public MatrixData()
        {
            Matrices = new List<List<Matrix>>
            {
                Capacity = 3
            };

            // Feature Matrices
            Matrices.Add(new List<Matrix>());
            Matrices[0].Capacity = 3;
            Matrices[0].Add(new Matrix(1, 1)); // W placeholder
            Matrices[0].Add(new Matrix(1, 1)); // X placeholder
            Matrices[0].Add(new Matrix(1, 1)); // B placeholder

            // Gradients Matrices
            Matrices.Add(new List<Matrix>());
            Matrices[1].Capacity = 3;
            Matrices[1].Add(new Matrix(1, 1)); // W-Gradients placeholder
            Matrices[1].Add(new Matrix(1, 1)); // X-Gradients placeholder
            Matrices[1].Add(new Matrix(1, 1)); // B-Gradients placeholder

            // Utility Matrices
            Matrices.Add(new List<Matrix>());
            Matrices[2].Capacity = 1;
            Matrices[2].Add(new Matrix(1, 1)); // dA placeholder
        }

        public List<List<Matrix>> Matrices { get; private set; }

        Matrix GetW() => Matrices[0][0];

        void SetW(Matrix input) { Matrices[0][0] = input; }

        Matrix GetX() => Matrices[0][1];

        void SetX(Matrix input) { Matrices[0][1] = input; }

        Matrix GetB() => Matrices[0][2];

        void SetB(Matrix input) { Matrices[0][2] = input; }

        Matrix GetWGrad() => Matrices[1][0];

        void SetWGrad(Matrix input) { Matrices[1][0] = input; }

        Matrix GetXGrad() => Matrices[1][1];

        void SetXGrad(Matrix input) { Matrices[1][1] = input; }

        Matrix GetBGrad() => Matrices[1][2];

        void SetBGrad(Matrix input) { Matrices[1][2] = input; }

        Matrix GetDA() => Matrices[2][0];

        void SetDA(Matrix input) { Matrices[2][0] = input; }
    }
}
