using XNet.XMath;

namespace XNet.Optimizer.Utility
{
    public abstract class Optimizer
    {
        public abstract Matrix CalculateDeltaW(Matrix W, Matrix dJdW);
        public abstract Matrix CalculateDeltaB(Matrix b, Matrix dJdb);
        public abstract EOptimizerType Type();
    }
}
