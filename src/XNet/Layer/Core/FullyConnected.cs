// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Layer.Utility;
using XNet.XMath;
using XNet.Utility;

namespace XNet.Layer.Core
{
    public class FullyConnected : Layer.Utility.Layer
    {
        public int NeuronCount { get; private set; }

        public FullyConnected(int neuronCount, int index, Activation.Utility.ActivationSettings activationSettings, LayerSettings layerSettings) : base(index, activationSettings, layerSettings)
        {
            NeuronCount = neuronCount;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override void Forward(ref MatrixData input)
        {
            // Get previous a
            Matrix prev_A = input.Data["a" + (Index - 1).ToString()];

            // Get previous W
            Matrix prev_W = input.Data["W" + (Index - 1).ToString()];

            // Get previous B
            Matrix prev_b = input.Data["b" + (Index - 1).ToString()];

            // Calculate current Z and store it
            Matrix current_Z = Matrix.Transpose(prev_W) * prev_A + prev_b;
            input.Data["Z" + (Index).ToString()] = current_Z;

            // Calculate current a and store it
            Matrix current_A = Activation.Forward(current_Z);
            input.Data["a" + (Index).ToString()] = current_Z;

            // Formula For a[i]: g[i]((W_Transpose[i] . z[i-1]) + B[i])

            // Update the global data
            GlobalData = input;
        }

        public override void Backward(ref MatrixData input)
        {
            // Calculate current dz and store it
            Matrix current_dz = input.Data["da" + (Index).ToString()].ElementMul(Activation.Backward(input.Data["Z" + (Index).ToString()]));
            input.Data["dZ" + (Index).ToString()] = current_dz;

            // Calculate current dw and store it
            Matrix current_dw = input.Data["dZ" + (Index).ToString()].Dot(input.Data["a" + (Index - 1).ToString()]);
            input.Data["dW" + (Index).ToString()] = current_dw;

            // Calculate current db and store it
            Matrix current_db = current_dz;
            input.Data["db" + (Index).ToString()] = current_db;

            // Calculate previous da and store it
            Matrix prev_da = Matrix.Transpose(input.Data["W" + (Index).ToString()]).Dot(input.Data["dZ" + (Index).ToString()]);
            input.Data["da" + (Index - 1).ToString()] = prev_da;

            // Formula dZ[i]: ((W_Transpose[i+1] . dZ[i+1]) * g'[i](z[i]))

            // Update the global data
            GlobalData = input;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Dims InShape()
        {
            // These will be initialized in the Network class
            int x = GlobalData.Data["IS" + (Index).ToString()].rows;
            int y = GlobalData.Data["IS" + (Index).ToString()].cols;

            Dims dims = new Dims();
            dims.Values.Add(x);
            dims.Values.Add(y);

            return dims;
        }

        public override Dims OutShape()
        {
            // These will be initialized in the Network class
            int x = GlobalData.Data["OS" + (Index).ToString()].rows;
            int y = GlobalData.Data["OS" + (Index).ToString()].cols;

            Dims dims = new Dims();
            dims.Values.Add(x);
            dims.Values.Add(y);

            return dims;
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
