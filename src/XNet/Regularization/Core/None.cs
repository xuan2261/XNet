﻿using XNet.Math;
using XNet.Regularization.Utility;

namespace XNet.Regularization.Core
{
    public sealed class None : Utility.Regularization
    {
        public None() : base(0) { }

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override double CalculateNorm(Matrix X) => 1;

        public override string ToString() => Type().ToString();

        public override ERegularizationType Type() => ERegularizationType.L1;
    }
}