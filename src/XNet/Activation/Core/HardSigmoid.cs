// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class HardSigmoid : Utility.Activation
    {
        public HardSigmoid(HardSigmoidSettings settings = null) { }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => (input < 0) ? 0 : (input < 1) ? input : 1;

        public override double Derivative(double input) => (input > 1 || input < 0) ? 0 : 1;

        public override Utility.EActivationType Type() => Utility.EActivationType.HardSigmoid;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class HardSigmoidSettings : Utility.ActivationSettings
    {
        public override Utility.EActivationType Type() => Utility.EActivationType.HardSigmoid;
    }
}
