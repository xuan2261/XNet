using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Mish : Utility.Activation
    {
        public override Matrix Forward(Matrix input)
        {
            return Matrix.Map(input, Activate);
        }

        public override Matrix Backward(Matrix input)
        {
            return Matrix.Map(input, Derivative);
        }

        public override double Activate(double input)
        {
            return input * System.Math.Tanh(System.Math.Log(1 + System.Math.Exp(input)));
        }

        public override double Derivative(double input)
        {
            double epsilon = 2 * System.Math.Exp(input) + System.Math.Exp(2 * input) + 2;
            double omega = 4 * (input + 1) + 4 * System.Math.Exp(2 * input) + System.Math.Exp(3 * input) + System.Math.Exp(input) * (4 * input + 6);

            return ((System.Math.Exp(input) * omega) / System.Math.Pow(epsilon, 2));
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.Mish;
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
