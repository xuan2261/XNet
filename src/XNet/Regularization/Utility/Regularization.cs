// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Regularization.Utility
{
    public abstract class Regularization
    {
        public Regularization(RegularizationSettings settings) { }

        public abstract double CalculateNorm(Matrix X);

        public abstract ERegularizationType Type();
    }

    public abstract class RegularizationSettings
    {
        public abstract ERegularizationType Type();
    }
}
