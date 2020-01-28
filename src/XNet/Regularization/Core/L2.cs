// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;
using XNet.Regularization.Utility;

namespace XNet.Regularization.Core
{
    public sealed class L2 : Utility.Regularization
    {
        public L2(double lambda) : base(lambda) { }

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override double CalculateNorm(Matrix X) => (Matrix.EuclideanNorm(X) * Lambda);

        public override string ToString() => Type().ToString();

        public override ERegularizationType Type() => ERegularizationType.L2;

    }
}
