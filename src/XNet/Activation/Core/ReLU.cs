// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class ReLUS : Utility.Activation
    {
        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => System.Math.Max(input, 0);

        public override double Derivative(double input) => (input > 0) ? 1 : 0;

        public override Utility.EActivationType Type() => Utility.EActivationType.ReLU;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}
