// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class Tanh : Utility.Activation
    {
        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => (System.Math.Exp(input) - System.Math.Exp(-input)) / (System.Math.Exp(input) + System.Math.Exp(-input));
        
        public override double Derivative(double x) => 1 - System.Math.Pow((System.Math.Exp(x) - System.Math.Exp(-x)) / (System.Math.Exp(x) + System.Math.Exp(-x)), 2);

        public override Utility.EActivationType Type() => Utility.EActivationType.Arctan;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);
       
        public override int GetHashCode() => base.GetHashCode();
        
    }
}
