﻿// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;
using XNet.Optimizer.Utility;

namespace XNet.Optimizer.Core
{
    public sealed class Momentum : Utility.Optimizer
    {
        public double Alpha { get; set; }
        public double Tao { get; set; }

        public Momentum(MomentumSettings settings) : base(settings)
        {
            Alpha = settings.Alpha;
            Tao = settings.Tao;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Type().ToString();
        }

        public override EOptimizerType Type()
        {
            return EOptimizerType.Momentum;
        }

        public override Matrix CalculateDeltaW(Matrix W, Matrix dJdW)
        {
            return null;
        }

        public override Matrix CalculateDeltaB(Matrix b, Matrix dJdb)
        {
            return null;
        }
    }

    public sealed class MomentumSettings : OptimizerSettings
    {
        public double Alpha { get; set; }
        public double Tao { get; set; }
    }
}
