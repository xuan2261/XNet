// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Activation.Core
{
    public sealed class HardTanh : Utility.Activation
    {
        public HardTanh(HardTanhSettings settings = null) { }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => (input < -1) ? -1 : (input > 1) ? 1 : input;

        public override double Derivative(double input) => (input < -1) ? 0 : (input > 1) ? 0 : 1;

        public override Utility.EActivationType Type() => Utility.EActivationType.HardTanh;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class HardTanhSettings : Utility.ActivationSettings
    {
        public override Utility.EActivationType Type() => Utility.EActivationType.HardTanh;
    }
}
