// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using XNet.Cost.Utility;
using XNet.Regularization.Utility;
using XNet.XMath;

namespace XNet.Cost.Core
{
    /// <summary>
    /// "Exponential Cost"
    /// </summary>
    public class ExponentionalCost : Utility.Cost
    {
        public double Tao { get; set; }

        public ExponentionalCost(ExponentionalCostSettings settings) : base(settings)
        {
            Tao = settings.Tao;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override double Forward(Matrix Actual, Matrix Expected, MatrixData data, int layerCount)
        {
            double error = 0.0;
            if (Actual.rows != Expected.rows || Actual.cols != Expected.cols)
            {
                throw new MatrixException("Actual Matrix does not have the same size as The Expected Matrix");
            }

            for (int i = 0; i < Actual.rows; i++)
            {
                for (int j = 0; j < Actual.cols; j++)
                {
                    error += Math.Pow((Actual[i, j] - Expected[i, j]), 2);
                }
            }

            error /= Tao;

            error = Math.Exp(error);

            error *= Tao;

            error += regularizationValue;

            BatchCost += error;
            return error;
        }

        public override Matrix Backward(Matrix Actual, Matrix Expected, MatrixData data, int layerCount)
        {
            double error = 0.0;
            if (Actual.rows != Expected.rows || Actual.cols != Expected.cols)
            {
                throw new MatrixException("Actual Matrix does not have the same size as The Expected Matrix");
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

            error /= Tao;

            error = Math.Exp(error);

            error *= Tao;
            
            Matrix gradMatrix = Actual.Duplicate();
            for (int i = 0; i < Actual.rows; i++)
            {
                for (int j = 0; j < Actual.cols; j++)
                {
                    gradMatrix[i, j] = (Actual[i, j] - Expected[i, j]) * error;
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
            return ECostType.CrossEntropyCost;
        }
    }

    public class ExponentionalCostSettings : CostSettings
    {
        public double Tao { get; set; }

        public ExponentionalCostSettings(double tao, ERegularizationType regularizationType, RegularizationSettings regularizationSettings) : base(regularizationType, regularizationSettings) { Tao = tao; }
    }
}
