using XNet.Math;
using XNet.Optimizer.Utility;

namespace XNet.Optimizer.Core
{
    public sealed class AdaDelta : Utility.Optimizer
    {
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
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Type().ToString();
        }

        public override EOptimizerType Type()
        {
            return EOptimizerType.AdaDelta;
        }
    }
}
