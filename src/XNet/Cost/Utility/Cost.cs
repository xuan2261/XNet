// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using XNet.Regularization.Core;
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
        public Cost(CostSettings settings)
        {
            BatchCost = 0;

            switch (settings.RegularizationType)
            {
                case ERegularizationType.Invalid:
                    throw new ArgumentException("Invalid Regularization Function Selected!");
                case ERegularizationType.None:
                    RegularizationFunction = new None();
                    break;
                case ERegularizationType.L1:
                    RegularizationFunction = new L1((L1Settings)settings.RegularizationSettings);
                    break;
                case ERegularizationType.L2:
                    RegularizationFunction = new L2((L2Settings)settings.RegularizationSettings);
                    break;
                default:
                    throw new ArgumentException("Invalid Regularization Function Selected!");
            }
        }

        public Regularization.Utility.Regularization RegularizationFunction;
        
        public abstract double Forward(Matrix Actual, Matrix Expected, MatrixData data, int layerCount);

        public abstract Matrix Backward(Matrix Actual, Matrix Expected, MatrixData data, int layerCount);

        public abstract ECostType Type();

        public double BatchCost { get; protected set; }

        public virtual void ResetCost() { BatchCost = 0; }
    }

    public abstract class CostSettings
    {
        public ERegularizationType RegularizationType { get; private set; }

        public RegularizationSettings RegularizationSettings { get; private set; }

        protected CostSettings(ERegularizationType regularizationType, RegularizationSettings regularizationSettings)
        {
            RegularizationType = regularizationType;
            RegularizationSettings = regularizationSettings;
        }
    }
}
