// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Activation.Core
{
    public sealed class Softmax : Utility.Activation
    {
        public Softmax(SoftmaxSettings settings = null) { }

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

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => 0;

        public override double Derivative(double input) => System.Math.Exp(input) / SumExp * (1 - System.Math.Exp(input) / SumExp);

        public override Utility.EActivationType Type() => Utility.EActivationType.Softmax;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class SoftmaxSettings : Utility.ActivationSettings
    {
        public override Utility.EActivationType Type() => Utility.EActivationType.Softmax;
    }
}
