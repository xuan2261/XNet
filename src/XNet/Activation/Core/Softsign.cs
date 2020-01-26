using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Softsign : Utility.Activation
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
            return input / (1 + System.Math.Abs(input));
        }

        public override double Derivative(double input)
        {
            return input / System.Math.Pow((1 + System.Math.Abs(input)), 2);
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.Softsign;
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
