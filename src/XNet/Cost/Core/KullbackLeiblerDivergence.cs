// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using XNet.Cost.Utility;
using XNet.Regularization.Utility;
using XNet.XMath;

namespace XNet.Cost.Core
{
    /// <summary>
    /// "Kullback Leibler Divergence" Also known as "Information Divergence", "Information Gain", "Relative entropy", "KLIC", or "KL Divergence".
    /// </summary>
    public class KullbackLeiblerDivergence : Utility.Cost
    {
        public KullbackLeiblerDivergence(KullbackLeiblerDivergenceSettings settings) : base(settings) { }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override double Forward(Matrix Actual, Matrix Expected, MatrixData data, int layerCount)
        {
            double error = 0.0;
            if (Actual.rows != Expected.rows || Actual.cols != Expected.cols)
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
                    error += Expected[i, j] * Math.Log(Expected[i, j] / Actual[i, j]);
                }
            }

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

            Matrix gradMatrix = Actual.Duplicate();

            for (int i = 0; i < Actual.rows; i++)
            {
                for (int j = 0; j < Actual.cols; j++)
                {
                    gradMatrix[i, j] = -(Expected[i, j] / Actual[i, j]);
                }
            }

            return gradMatrix;
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
            return ECostType.KullbackLeiblerDivergence;
        }
    }

    public class KullbackLeiblerDivergenceSettings : CostSettings
    {
        public KullbackLeiblerDivergenceSettings(ERegularizationType regularizationType, RegularizationSettings regularizationSettings) : base(regularizationType, regularizationSettings) { }
    }
}
