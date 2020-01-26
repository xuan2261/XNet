using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Logit : Utility.Activation
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
            return System.Math.Log(input / (1 - input));
        }

        public override double Derivative(double input)
        {
            return (-1 / System.Math.Pow(input, 2)) - (1 / (System.Math.Pow((1 - input), 2)));
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.Logit;
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
