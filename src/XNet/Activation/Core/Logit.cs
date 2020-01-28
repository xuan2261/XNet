// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Activation.Core
{
    public sealed class Logit : Utility.Activation
    {
        public Logit(LogitSettings settings = null) { }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => System.Math.Log(input / (1 - input));
        
        public override double Derivative(double input) => (-1 / System.Math.Pow(input, 2)) - (1 / (System.Math.Pow((1 - input), 2)));
        
        public override Utility.EActivationType Type() => Utility.EActivationType.Logit;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class LogitSettings : Utility.ActivationSettings
    {
        public override Utility.EActivationType Type() => Utility.EActivationType.Logit;
    }
}
