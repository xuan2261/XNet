// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Activation.Core
{
    public sealed class LReLU : Utility.Activation
    {
        public double Alpha { get; set; }

        public LReLU(LReLUSettings settings)
        {
            Alpha = settings.Alpha;
        }

        public override Matrix Forward(Matrix input) => Matrix.Map(input, Activate);

        public override Matrix Backward(Matrix input) => Matrix.Map(input, Derivative);

        public override double Activate(double input) => System.Math.Max(input, Alpha);

        public override double Derivative(double input) => (input > 0) ? 1 : Alpha;

        public override Utility.EActivationType Type() => Utility.EActivationType.LReLU;

        public override string ToString() => Type().ToString();

        public override bool Equals(object obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();
    }

    public sealed class LReLUSettings : Utility.ActivationSettings
    {
        public LReLUSettings(double alpha = 0.01)
        {
            Alpha = alpha;
        }

        public double Alpha { get; private set; }

        public override Utility.EActivationType Type() => Utility.EActivationType.LReLU;
    }
}
