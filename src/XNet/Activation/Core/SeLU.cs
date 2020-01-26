// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class SeLU : Utility.Activation
    {

        public const double Alpha = 1.6732632423543772848170429916717;

        public const double Lambda = 1.0507009873554804934193349852946;

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => (input > 0) ? input : Alpha * (System.Math.Exp(input) - 1);

        public override double Derivative(double input) => (input > 0) ? Lambda : Lambda * Alpha * System.Math.Exp(input);

        public override Utility.EActivationType Type() => Utility.EActivationType.SeLU;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}
