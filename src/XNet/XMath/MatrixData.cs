// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System.Collections.Generic;

namespace XNet.XMath
{
    public class MatrixData
    {
        public Dictionary<string, Matrix> Data { get; private set; }

        public MatrixData()
        {
            Data = new Dictionary<string, Matrix>();
        }
    }
}
