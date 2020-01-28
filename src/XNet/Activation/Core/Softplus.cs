// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Activation.Core
{
    public sealed class Softplus : Utility.Activation
    {
        public Softplus(SoftplusSettings settings = null) { }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => System.Math.Log(1 + System.Math.Exp(input));

        public override double Derivative(double input) => System.Math.Exp(input) / (1 + System.Math.Exp(input));

        public override Utility.EActivationType Type() => Utility.EActivationType.Softplus;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class SoftplusSettings : Utility.ActivationSettings
    {
        public override Utility.EActivationType Type() => Utility.EActivationType.Softplus;
    }
}
