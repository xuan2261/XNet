﻿// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Core
{
    public sealed class LReLU : Utility.Activation
    {
        public double Alpha { get; set; }

        public LReLU(double alpha)
        {
            Alpha = alpha;
        }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => System.Math.Max(input, Alpha);

        public override double Derivative(double input) => (input > 0) ? 1 : Alpha;

        public override Utility.EActivationType Type() => Utility.EActivationType.Arctan;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }
}
