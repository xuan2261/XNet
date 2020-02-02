// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Regularization.Utility;
using XNet.XMath;

namespace XNet.Cost.Utility
{
    /// <summary>
    /// A cost function is a measure of "how good" a neural network did with respect to it's given training sample and the expected output. It also may depend on variables such as weights and biases.
    /// A cost function is a single value, not a vector, because it rates how good the neural network did as a whole.
    /// </summary>
    public abstract class Cost
    {
        // Second param might be changed to "RegularizationSettings" type
        public Cost(ERegularizationType regularizationType, double Lambda)
        {
            RegularizationType = regularizationType;

            switch (RegularizationType)
            {
                case ERegularizationType.Invalid:
                    throw new System.ArgumentException("Invalid Regularization Type selected, Use None instead.");
                case ERegularizationType.None:
                    Regularization = new Regularization.Core.None();
                    break;
                case ERegularizationType.L1:
                    Regularization = new Regularization.Core.L1(Lambda);
                    break;
                case ERegularizationType.L2:
                    Regularization = new Regularization.Core.L2(Lambda);
                    break;
                default:
                    throw new System.ArgumentException("Invalid Regularization Type selected, Use None instead.");
            }
        }

        public Regularization.Utility.Regularization Regularization;

        public ERegularizationType RegularizationType { get; private set; }

        public abstract double Forward(Matrix Actual, Matrix Expected);

        public abstract Matrix Backward(Matrix Actual, Matrix Expected);

        public abstract ECostType Type();
    }
}
