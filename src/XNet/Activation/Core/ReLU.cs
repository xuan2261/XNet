// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Activation.Core
{
    public sealed class ReLU : Utility.Activation
    {
        public ReLU(ReLUSettings settings = null) : base(settings) { }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => System.Math.Max(input, 0);

        public override double Derivative(double input) => (input > 0) ? 1 : 0;

        public override Utility.EActivationType Type() => Utility.EActivationType.ReLU;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class ReLUSettings : Utility.ActivationSettings
    {
        public override Utility.EActivationType Type() => Utility.EActivationType.ReLU;
    }
}
