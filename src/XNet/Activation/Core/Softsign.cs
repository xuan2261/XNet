// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Softsign : Utility.Activation
    {
        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => input / (1 + System.Math.Abs(input));

        public override double Derivative(double input) => input / System.Math.Pow((1 + System.Math.Abs(input)), 2);   

        public override Utility.EActivationType Type() => Utility.EActivationType.Softsign;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}
