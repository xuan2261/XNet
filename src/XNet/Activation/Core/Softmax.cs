using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Softmax : Utility.Activation
    {
        public double SumExp { get; private set; }

        public Softmax()
        {
            SumExp = 0.0;
        }

        public override Matrix Forward(Matrix input)
        {
            Matrix res = input.Duplicate();
            SumExp = 0.0;

            if (input.cols == 1)
            {
                for (int j = 0; j < input.cols; j++)
                {
                    SumExp += System.Math.Exp(input[0, j]);
                }

                for (int j = 0; j < res.cols; j++)
                {
                    res[0, j] = System.Math.Exp(input[0, j]) / SumExp;
                }
            }

            return res;
        }

        public override Matrix Backward(Matrix input)
        {
            return Matrix.Map(input, Derivative);
        }

        public override double Activate(double input)
        {
            return 0;
        }

        public override double Derivative(double input)
        {
            double softmax = System.Math.Exp(input) / SumExp;
            return softmax * (1 - softmax);
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.Softmax;
        }

        public override string ToString()
        {
            return Type().ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
