// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using XNet.Cost.Utility;
using XNet.Regularization.Utility;
using XNet.XMath;

namespace XNet.Cost.Core
{
    /// <summary>
    /// "Quadratic Cost": Also known as "Mean Squared Error" or "Maximum Likelihood" or "Sum Squared Error"
    /// </summary>
    public class QuadraticCost : Utility.Cost
    {
        public QuadraticCost(QuadraticCostSettings settings) : base(settings) { }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override double Forward(Matrix Actual, Matrix Expected, MatrixData data, int layerCount)
        {
            double error = 0.0;
            if(Actual.rows != Expected.rows || Actual.cols != Expected.cols)
            {
                throw new MatrixException("Actual does not have the same size as Expected");
            }

            double regularizationValue = 0.0;
            for (int i = 0; i < layerCount; i++)
            {
                regularizationValue = RegularizationFunction.CalculateNorm(data.Data["W" + i.ToString()]);
            }

            for (int i = 0; i < Actual.rows; i++)
            {
                for (int j = 0; j < Actual.cols; j++)
                {
                    error += Math.Pow((Actual[i, j] - Expected[i, j]), 2);
                }
            }

            error /= 2;

            error += regularizationValue;

            BatchCost += error;
            return error;
        }

        public override Matrix Backward(Matrix Actual, Matrix Expected, MatrixData data, int layerCount)
        {
            if (Actual.rows != Expected.rows || Actual.cols != Expected.cols)
            {
                throw new MatrixException("Actual Matrix does not have the same size as The Expected Matrix");
            }

            return Actual - Expected;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return Type().ToString();
        }

        public override ECostType Type()
        {
            return ECostType.QuadraticCost;
        }
    }

    public class QuadraticCostSettings : CostSettings
    {
        public QuadraticCostSettings(ERegularizationType regularizationType, RegularizationSettings regularizationSettings) : base(regularizationType, regularizationSettings) { }
    }
}
