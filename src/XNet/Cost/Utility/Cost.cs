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
        public Cost(CostSettings settings) { }

        public Regularization.Utility.Regularization Regularization;

        public ERegularizationType RegularizationType { get; private set; }

        public abstract double Forward(Matrix Actual, Matrix Expected);

        public abstract Matrix Backward(Matrix Actual, Matrix Expected);

        public abstract ECostType Type();
    }

    public abstract class CostSettings
    {
    }
}
