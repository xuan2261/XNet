using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class SeLU : Utility.Activation
    {

        public const double Alpha = 1.6732632423543772848170429916717;

        public const double Lambda = 1.0507009873554804934193349852946;

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
            if (input > 0)
            {
                return input;
            }
            return Alpha * (System.Math.Exp(input) - 1);
        }

        public override double Derivative(double input)
        {
            if (input > 0)
            {
                return Lambda;
            }
            return Lambda * Alpha * System.Math.Exp(input);
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.SeLU;
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
