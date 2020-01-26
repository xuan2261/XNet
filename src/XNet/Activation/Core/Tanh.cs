using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Tanh : Utility.Activation
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
            return (System.Math.Exp(input) - System.Math.Exp(-input)) / (System.Math.Exp(input) + System.Math.Exp(-input));
        }

        public override double Derivative(double x)
        {
            return 1 - System.Math.Pow((System.Math.Exp(x) - System.Math.Exp(-x)) / (System.Math.Exp(x) + System.Math.Exp(-x)), 2);
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.Arctan;
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
