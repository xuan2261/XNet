// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;
using XNet.Optimizer.Utility;

namespace XNet.Optimizer.Core
{
    public sealed class GradientDescent : Utility.Optimizer
    {
        public double Alpha { get; set; }

        public GradientDescent(GradientDescentSettings settings) : base(settings)
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
            return EOptimizerType.GradientDescent;
        }

        public override Matrix CalculateDeltaW(Matrix W, Matrix dJdW)
        {
            return (Alpha * (Matrix.Transpose(W).ElementMul(dJdW)));
        }

        public override Matrix CalculateDeltaB(Matrix b, Matrix dJdb)
        {
            return (Alpha * (b.ElementMul(dJdb)));
        }
    }

    public sealed class GradientDescentSettings : OptimizerSettings
    {
        public double Alpha { get; private set; }

        public GradientDescentSettings(double alpha)
        {
            Alpha = alpha;
        }
    }
}
