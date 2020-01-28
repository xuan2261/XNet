using System;
using System.Collections.Generic;
using XNet.Layer.Utility;
using XNet.XMath;
using XNet.Utility;

namespace XNet.Layer.Core
{
    public class FullyConnected : Layer.Utility.Layer
    {
        public FullyConnected(Activation.Utility.ActivationSettings acs, LayerSettings ls) : base(acs, ls)
        {

        }

        public override List<List<Matrix>> Backward(List<Matrix> input_data, List<Matrix> input_grad)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override List<Matrix> Forward(List<Matrix> input)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Dims InShape()
        {
            return base.InShape();
        }

        public override Dims OutShape()
        {
            return base.OutShape();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override ELayerType Type()
        {
            throw new NotImplementedException();
        }
    }
}
