// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.XMath;

namespace XNet.Cost.Utility
{
    public abstract class Cost
    {
        public abstract Matrix Forward(Matrix Actual, Matrix Expected);

        public abstract Matrix Backward(Matrix Actual, Matrix Expected);

        public abstract ECostType Type();
    }
}
