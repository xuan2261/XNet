using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class HardTanh : Utility.Activation
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
            if (input < -1)
            {
                return -1;
            }
            else if (input > 1)
            {
                return 1;
            }
            return input;
        }

        public override double Derivative(double input)
        {
            if (input < -1)
            {
                return 0;
            }
            else if (input > 1)
            {
                return 0;
            }
            return 1;
        }

        public override Utility.EActivationType Type()
        {
            return Utility.EActivationType.HardTanh;
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
