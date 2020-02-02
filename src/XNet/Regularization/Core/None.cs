// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;
using XNet.Regularization.Utility;

namespace XNet.Regularization.Core
{
    public sealed class None : Utility.Regularization
    {
        public None() : base(null) { }

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override double CalculateNorm(Matrix X) => 0;

        public override string ToString() => Type().ToString();

        public override ERegularizationType Type() => ERegularizationType.None;
    }
}
