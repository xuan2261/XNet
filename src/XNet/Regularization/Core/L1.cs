using XNet.Math;
using XNet.Regularization.Utility;

namespace XNet.Regularization.Core
{
    public sealed class L1 : Utility.Regularization
    {
        public L1(double lambda) : base(lambda) { }

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override double CalculateNorm(Matrix X) => (Matrix.AbsoluteNorm(X) * Lambda);

        public override string ToString() => Type().ToString();

        public override ERegularizationType Type() => ERegularizationType.L1;
    }
}
