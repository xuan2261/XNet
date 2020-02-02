// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;
using XNet.Regularization.Utility;

namespace XNet.Regularization.Core
{
    public sealed class L1 : Utility.Regularization
    {
        public double Lambda { get; set; }

        public L1(L1Settings settings) : base(settings) { Lambda = settings.Lambda; }

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override double CalculateNorm(Matrix X) => (Matrix.AbsoluteNorm(X) * Lambda);

        public override string ToString() => Type().ToString();

        public override ERegularizationType Type() => ERegularizationType.L1;
    }

    public sealed class L1Settings : RegularizationSettings
    {
        public L1Settings(double lambda)
        {
            Lambda = lambda;
        }

        public override ERegularizationType Type() => ERegularizationType.L1;

        public double Lambda { get; set; }
    }
}
