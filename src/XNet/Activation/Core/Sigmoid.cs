// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Sigmoid : Utility.Activation
    {
        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => 1.0 / (1 + System.Math.Exp(-input));

        public override double Derivative(double input) => (1.0 / (1 + System.Math.Exp(-input))) * (1 - (1.0 / (1 + System.Math.Exp(-input))));
        
        public override Utility.EActivationType Type() => Utility.EActivationType.Sigmoid;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}
