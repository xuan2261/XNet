// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Arctan : Utility.Activation
    {
        public Arctan(ArctanSettings settings = null) { }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => System.Math.Atan(input);
        
        public override double Derivative(double input) => 1 / (1 + System.Math.Pow(input, 2));

        public override Utility.EActivationType Type() => Utility.EActivationType.Arctan;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class ArctanSettings : Utility.ActivationSettings
    {
        public override Utility.EActivationType Type() => Utility.EActivationType.Arctan;
    }
}
