using XNet.XMath;
using XNet.Optimizer.Utility;
using XNet.Regularization.Utility;

namespace XNet.Optimizer.Core
{
    public sealed class GradientDescent : Utility.Optimizer
    {
        public double Alpha { get; set; }

        public GradientDescent(ERegularizationType regularizationType, double alpha) : base(regularizationType)
        {
            Alpha = alpha;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Type().ToString();
        }

        public override EOptimizerType Type()
        {
            return EOptimizerType.GradientDescent;
        }

        public override Matrix CalculateDeltaW(Matrix W, Matrix dJdW)
        {
            return (Alpha * (Matrix.Transpose(W) * dJdW));
        }

        public override Matrix CalculateDeltaB(Matrix b, Matrix dJdb)
        {
            return (Alpha * (Matrix.Transpose(b) * dJdb));
        }
    }
}
