// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Activation.Core
{
    public sealed class BipolarSigmoid : Utility.Activation
    {
        public BipolarSigmoid(BipolarSigmoidSettings settings = null) : base(settings) { }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => -1 + 2 / (1 + System.Math.Exp(-input));

        public override double Derivative(double input) => 0.5 * (1 + (-1 + 2 / (1 + System.Math.Exp(-input)))) * (1 - (-1 + 2 / (1 + System.Math.Exp(-input))));

        public override Utility.EActivationType Type() => Utility.EActivationType.BipolarSigmoid;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class BipolarSigmoidSettings : Utility.ActivationSettings
    {
        public override Utility.EActivationType Type() => Utility.EActivationType.BipolarSigmoid;
    }
}
