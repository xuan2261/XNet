using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class LReLU : Utility.Activation
    {
        public double Alpha { get; set; }

        public LReLU(double alpha)
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
            return System.Math.Max(input, Alpha);
        }

        public override double Derivative(double input)
        {
            if (input > 0)
            {
                return 1;
            }
            return Alpha;
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.LReLU;
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
