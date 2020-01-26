using XNet.Math;

namespace XNet.Regularization.Utility
{
    public abstract class Regularization
    {
        public double Lambda { get; set; }

        protected Regularization(double lambda)
        {
            Lambda = lambda;
        }

        public abstract double CalculateNorm(Matrix X);

        public abstract ERegularizationType Type();
    }
}
