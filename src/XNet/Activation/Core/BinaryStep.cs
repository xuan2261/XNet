using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class BinaryStep : Utility.Activation
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
            if (input < 0)
            {
                return 0;
            }
            return 1;
        }

        public override double Derivative(double input)
        {
            return 0;
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.BinaryStep;
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
