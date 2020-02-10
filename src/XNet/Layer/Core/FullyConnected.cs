// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Layer.Utility;
using XNet.XMath;
using XNet.Utility;

namespace XNet.Layer.Core
{
    public class FullyConnected : Layer.Utility.Layer
    {
        public int NeuronCount { get; private set; }

        public FullyConnected(int neuronCount, int index, Activation.Utility.ActivationSettings activationSettings) : base(neuronCount, index, activationSettings)
        {
            NeuronCount = neuronCount;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        
        public override void Forward(ref MatrixData input)
        {
            // First layer will be a-1
            // Get previous a
            Matrix prev_A = input.Data["a" + (Index - 1).ToString()];

            // Get previous W
            Matrix W = input.Data["W" + (Index).ToString()];

            // Get previous b
            Matrix b = input.Data["b" + (Index).ToString()];

            // Calculate Z and store it
            Matrix Z = Matrix.Transpose(W).Dot((prev_A)) + b;
            input.Data["Z" + (Index).ToString()] = Z;

            // Calculate a and store it
            Matrix A = Activation.Forward(Z);
            input.Data["a" + (Index).ToString()] = Z;

            // Formula For a[i]: g[i]((W_Transpose[i] . z[i-1]) + B[i])

            // Update the global data
            GlobalData = input;
        }

        public override void Backward(ref MatrixData input)
        {
            // The only thing we need to put into the Input Matrix is the last calculated da
            // Calculate dz and store it
            Matrix dz = input.Data["da" + (Index).ToString()].ElementMul(Activation.Backward(input.Data["Z" + (Index).ToString()]));
            input.Data["dZ" + (Index).ToString()] = dz;

            // Calculate dw and store it
            Matrix dw = input.Data["dZ" + (Index).ToString()].Dot(Matrix.Transpose(input.Data["a" + (Index - 1).ToString()]));
            input.Data["dW" + (Index).ToString()] = dw;

            // Calculate db and store it
            Matrix db = dz;
            input.Data["db" + (Index).ToString()] = db;

            // Calculate previous da and store it
            Matrix prev_da = input.Data["W" + (Index).ToString()].Dot(input.Data["dZ" + (Index).ToString()]);
            input.Data["da" + (Index - 1).ToString()] = prev_da;

            // Formula dZ[i]: ((W_Transpose[i+1] . dZ[i+1]) * g'[i](z[i]))

            // Update the global data
            GlobalData = input;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override ELayerType Type()
        {
            return ELayerType.FullyConnected;
        }
    }
}
