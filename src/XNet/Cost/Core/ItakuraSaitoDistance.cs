﻿// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using XNet.Cost.Utility;
using XNet.XMath;

namespace XNet.Cost.Core
{
    public class ItakuraSaitoDistance : Utility.Cost
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
                    errorMatrix[i, j] = (Expected[i, j] / Actual[i, j]) - Math.Log(Expected[i, j] - Actual[i, j]) - 1;
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
                    gradMatrix[i, j] = (Actual[i, j] - Expected[i, j]) / Math.Pow(Actual[i, j], 2);
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
            return ECostType.ItakuraSaitoDistance;
        }
    }
}
