// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Identity : Utility.Activation
    {
        public Identity(IdentitySettings settings = null) { }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => input;

        public override double Derivative(double input) => 1;

        public override Utility.EActivationType Type() => Utility.EActivationType.Identity;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class IdentitySettings : Utility.ActivationSettings
    {
        public override Utility.EActivationType Type() => Utility.EActivationType.Identity;
    }
}
