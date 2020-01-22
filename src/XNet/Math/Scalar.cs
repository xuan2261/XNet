namespace XNet.Math
{
    /// <summary>
    /// Simple Scalar class used to store floating point values.
    /// Note: This class can be set to be constant upon instantiation.
    /// </summary>
    public class Scalar
    {
        public Scalar(float val = 0, bool isConst = false)
        {
            Value = val;
            IsConst = isConst;
        }

        public float Value
        {
            get
            {
                return Value;
            }
            set
            {
                if (!IsConst)
                {
                    Value = value;
                }
            }
        }
        public bool IsConst { get; private set; }
    }
}
