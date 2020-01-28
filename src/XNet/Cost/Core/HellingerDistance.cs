// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using XNet.Cost.Utility;
using XNet.XMath;

namespace XNet.Cost.Core
{
    public class HellingerDistance : Utility.Cost
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override Matrix Forward(Matrix Actual, Matrix Expected)
        {
            Matrix errorMatrix = Actual.Duplicate();
            for (int i = 0; i < Actual.rows; i++)
            {
                for (int j = 0; j < Actual.cols; j++)
                {
                    errorMatrix[i, j] = 1 / Math.Sqrt(2) * Math.Pow((Math.Sqrt(Actual[i, j]) - Math.Sqrt(Expected[i, j])), 2);
                }
            }
            return errorMatrix;
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
