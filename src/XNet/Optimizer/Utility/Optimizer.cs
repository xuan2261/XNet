// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Optimizer.Utility
{
    public abstract class Optimizer
    {
        public Optimizer(OptimizerSettings settings) {}
        public abstract Matrix CalculateDeltaW(Matrix W, Matrix dJdW);
        public abstract Matrix CalculateDeltaB(Matrix b, Matrix dJdb);
        public abstract EOptimizerType Type();
    }

    public abstract class OptimizerSettings
    {
    }
}
