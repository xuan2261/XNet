using XNet.Math;

namespace XNet.Optimizer.Utility
{
    public abstract class Optimizer
    {
        public abstract Matrix OptimizeMatrix(Matrix X, Matrix dJdX);

        public abstract EOptimizerType Type();
    }
}
