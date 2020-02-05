using System;
using System.Collections.Generic;
using XNet.Layer.Utility;
using XNet.XMath;

namespace XNet.Network
{
    public abstract class Network
    {
        public Network()
        {
            Data = new MatrixData();
            Layers = new List<Layer.Utility.Layer>();
        }

        public Matrix Forward(Matrix input)
        {
            MatrixData data = Data;
            for (int i = 1; i < Layers.Count; i++)
            {
                Layers[i].Forward(ref data);
            }
            Data = data;

            return Data.Data["Z" + (Layers.Count).ToString()];
        }

        public double Backward(Matrix input)
        {
            MatrixData data = Data;
            for (int i = 1; i < Layers.Count; i++)
            {
                Layers[i].Backward(ref data);
            }
            Data = data;

            return Data.Data["E" + (Layers.Count).ToString()][0, 0];
        }

        public bool CreateLayer(int nCount, ELayerType type, Activation.Utility.ActivationSettings activationSettings)
        {
            Layer.Utility.Layer layer;
            switch (type)
            {
                case ELayerType.Invalid:
                    throw new ArgumentException("Invalid \"type\" argument.");
                case ELayerType.AveragePooling:
                    layer = new Layer.Core.AveragePooling(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.AverageUnpooling:
                    layer = new Layer.Core.AverageUnpooling(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.Convolutional:
                    layer = new Layer.Core.Convolutional(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.Deconvolutional:
                    layer = new Layer.Core.Deconvolutional(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.Dropout:
                    layer = new Layer.Core.Dropout(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.FullyConnected:
                    layer = new Layer.Core.FullyConnected(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.GatedRecurrent:
                    layer = new Layer.Core.GatedRecurrent(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.LSTM:
                    layer = new Layer.Core.LSTM(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.MaxPooling:
                    layer = new Layer.Core.MaxPooling(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.MaxUnpooling:
                    layer = new Layer.Core.MaxUnpooling(nCount, Layers.Count, activationSettings);
                    return true;
                case ELayerType.Recurrent:
                    layer = new Layer.Core.Recurrent(nCount, Layers.Count, activationSettings);
                    return true;
                default:
                    throw new ArgumentException("Invalid \"type\" argument.");
            }
        }

        public void InitNetwork()
        {
            Utility.Dims InShape;
            Utility.Dims OutShape;
            Utility.Dims WShape;

            for (int i = 0; i < Layers.Count - 1; i++)
            {
                InShape = new Utility.Dims(Layers[i].NCount, 1);
                OutShape = new Utility.Dims(Layers[i + 1].NCount, 1);
                WShape = new Utility.Dims(Layers[i].NCount, Layers[i + 1].NCount);
                Layers[i].SetSettings(new LayerSettings(InShape, OutShape, WShape));
            }

            InShape = new Utility.Dims(Layers[Layers.Count - 1].NCount, 1);
            OutShape = new Utility.Dims(Layers[Layers.Count - 1].NCount, 1);
            WShape = new Utility.Dims(0, 0);
            Layers[Layers.Count - 1].SetSettings(new LayerSettings(InShape, OutShape, WShape));


        }

        public MatrixData Data { get; private set; }

        public List<Layer.Utility.Layer> Layers { get; private set; }
    }
}
