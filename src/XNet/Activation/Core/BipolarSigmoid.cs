using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class BipolarSigmoid : Utility.Activation
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
            return -1 + 2 / (1 + System.Math.Exp(-input));
        }

        public override double Derivative(double input)
        {
            return 0.5 * (1 + (-1 + 2 / (1 + System.Math.Exp(-input)))) * (1 - (-1 + 2 / (1 + System.Math.Exp(-input))));
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.BipolarSigmoid;
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
