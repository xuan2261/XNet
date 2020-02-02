// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using XNet.Cost.Utility;
using XNet.Regularization.Utility;
using XNet.XMath;

namespace XNet.Cost.Core
{
    /// <summary>
    /// "Hellinger Distance": needs to have positive values, and ideally values between 0 and 1. The same is true for the following divergences.
    /// </summary>
    public class HellingerDistance : Utility.Cost
    {
        public HellingerDistance(ERegularizationType regularizationType, double Lambda) : base(regularizationType, Lambda) { }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override double Forward(Matrix Actual, Matrix Expected)
        {
            double error = 0.0;
            if (Actual.rows != Expected.rows || Actual.cols != Expected.cols)
            {
                throw new MatrixException("Actual does not have the same size as Expected");
            }

            for (int i = 0; i < Actual.rows; i++)
            {
                for (int j = 0; j < Actual.cols; j++)
                {
                    error += 1 / Math.Sqrt(2) * Math.Pow((Math.Sqrt(Actual[i, j]) - Math.Sqrt(Expected[i, j])), 2);
                }
            }

            return error;
        }

        public override Matrix Backward(Matrix Actual, Matrix Expected)
        {
            Matrix gradMatrix = Actual.Duplicate();
            for (int i = 0; i < Actual.rows; i++)
            {
                for (int j = 0; j < Actual.cols; j++)
                {
                    gradMatrix[i, j] = (Math.Sqrt(Actual[i, j]) - Math.Sqrt(Expected[i, j])) / (Math.Sqrt(2) * Math.Sqrt(Actual[i, j]));
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
            return ECostType.HelligerDistance;
        }
    }
}
