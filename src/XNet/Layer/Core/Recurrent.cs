// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using XNet.Layer.Utility;
using XNet.XMath;

namespace XNet.Layer.Core
{
    public class Recurrent : Layer.Utility.Layer
    {
        public int NeuronCount { get; private set; }

        public Recurrent(int neuronCount, int index, Activation.Utility.ActivationSettings activationSettings) : base(neuronCount, index, activationSettings)
        {
            NeuronCount = neuronCount;
        }

        public override void Backward(ref MatrixData input)
        {
            throw new NotImplementedException();
        }

        public override void Forward(ref MatrixData input)
        {
            throw new NotImplementedException();
        }

        public override ELayerType Type()
        {
            throw new NotImplementedException();
        }
    }
}
