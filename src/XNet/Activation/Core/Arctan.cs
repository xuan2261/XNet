using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Arctan : Utility.Activation
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
            return System.Math.Atan(input);
        }

        public override double Derivative(double input)
        {
            return 1 / (1 + System.Math.Pow(input, 2));
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
