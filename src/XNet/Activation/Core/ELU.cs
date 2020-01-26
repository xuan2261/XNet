using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class ELU : Utility.Activation
    {
        public double Alpha { get; set; }

        public ELU(double alpha)
        {
            Alpha = alpha;
        }

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
            if (input >= 0)
            {
                return input;
            }
            return Alpha * (System.Math.Exp(input) - 1);
        }

        public override double Derivative(double input)
        {
            if (input > 0)
            {
                return 1;
            }
            return Alpha * System.Math.Exp(input);
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.ELU;
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
