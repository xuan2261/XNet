// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;
using XNet.Optimizer.Utility;

namespace XNet.Optimizer.Core
{
    public sealed class Adam : Utility.Optimizer
    {
        public double Alpha { get; set; }

        public Adam(AdamSettings settings) : base(settings)
        {
            Alpha = settings.Alpha;
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
            return EOptimizerType.Adam;
        }

        public override Matrix CalculateDeltaW(Matrix W, Matrix dJdW)
        {
            return null;
        }

        public override Matrix CalculateDeltaB(Matrix b, Matrix dJdb)
        {
            return null;
        }
    }

    public sealed class AdamSettings : OptimizerSettings
    {
        public double Alpha { get; set; }
        public double BetaPrimary { get; set; }
        public double BetaSecondary { get; set; }
        public double BetaPrimary_T { get; set; }
        public double BetaSecondary_T { get; set; }
        public double Epsilon { get; set; }
    }
}
