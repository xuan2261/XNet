﻿// Copyright © 2020 Aryan Mousavi All Rights Reserved.


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
        public QuadraticCost(ERegularizationType regularizationType, double Lambda) : base(regularizationType, Lambda) { }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override double Forward(Matrix Actual, Matrix Expected)
        {
            double error = 0.0;
            if(Actual.rows != Expected.rows || Actual.cols != Expected.cols)
            {
                throw new MatrixException("Actual does not have the same size as Expected");
            }

            for (int i = 0; i < Actual.rows; i++)
            {
                for (int j = 0; j < Actual.cols; j++)
                {
                    error += Math.Pow((Actual[i, j] - Expected[i, j]), 2);
                }
            }

            error /= 2;

            return error;
        }

        public override Matrix Backward(Matrix Actual, Matrix Expected)
        {
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
}
