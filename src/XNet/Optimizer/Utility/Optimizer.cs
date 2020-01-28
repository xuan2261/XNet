using XNet.XMath;

namespace XNet.Optimizer.Utility
{
    public abstract class Optimizer
    {
        public Regularization.Utility.ERegularizationType RegularizationType { get; private set; }

        public Regularization.Utility.Regularization RegularizationAgent { get; private set; }

        public Optimizer(Regularization.Utility.ERegularizationType regularizationType, double RegularizationLambda = 0.1)
        {
            RegularizationType = regularizationType;

            switch (regularizationType)
            {
                case Regularization.Utility.ERegularizationType.Invalid:
                    throw new System.Exception("Invalid Regularization!");
                case Regularization.Utility.ERegularizationType.None:
                    RegularizationAgent = new Regularization.Core.None();
                    break;
                case Regularization.Utility.ERegularizationType.L1:
                    RegularizationAgent = new Regularization.Core.L1(RegularizationLambda);
                    break;
                case Regularization.Utility.ERegularizationType.L2:
                    RegularizationAgent = new Regularization.Core.L2(RegularizationLambda);
                    break;
                default:
                    throw new System.Exception("Invalid Regularization!");
            }
        }
        public abstract Matrix CalculateDeltaW(Matrix W, Matrix dJdW);
        public abstract Matrix CalculateDeltaB(Matrix b, Matrix dJdb);
        public abstract EOptimizerType Type();
    }
}
