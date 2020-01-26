using XNet.Math;
using XNet.Optimizer.Utility;

namespace XNet.Optimizer.Core
{
    public sealed class GradientDescent : Utility.Optimizer
    {
        public double Alpha { get; set; }

        public GradientDescent(double alpha)
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

        public override Matrix OptimizeMatrix(Matrix X, Matrix dJdX)
        {            
            Matrix deltaX = (Alpha * (Matrix.Transpose(X) * dJdX));
            return deltaX;
        }

        public override string ToString()
        {
            return Type().ToString();
        }

        public override EOptimizerType Type()
        {
            return EOptimizerType.GradientDescent;
        }
    }
}
