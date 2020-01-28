// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;
using XNet.Activation.Core;
using XNet.Activation.Utility;
using XNet.XMath;
using XNet.Utility;

namespace XNet.Layer.Utility
{
    /// <summary>
    /// Base of all Layer classes
    /// </summary>
    public abstract class Layer
    {
        public MatrixData GlobalData { get; protected set; }

        public EActivationType ActivationType { get; private set; }

        public Activation.Utility.Activation Activation { get; private set; }
        
        public LayerSettings LayerConfig { get; private set; }

        public Layer(int index, ActivationSettings activationSettings, LayerSettings layerSettings)
        {
            Index = index;

            ActivationType = activationSettings.Type();

            LayerConfig = layerSettings;

            // Activation Setup
            switch (activationSettings.Type())
            {
                case EActivationType.Invalid:
                    Activation = null;
                    throw new ArgumentException("Activation Type Invalid.");
                case EActivationType.Arctan:
                    Activation = new Arctan();
                    break;
                case EActivationType.BinaryStep:
                    Activation = new BinaryStep();
                    break;
                case EActivationType.BipolarSigmoid:
                    Activation = new BipolarSigmoid();
                    break;
                case EActivationType.ELU:
                    Activation = new ELU((ELUSettings)activationSettings);
                    break;
                case EActivationType.HardSigmoid:
                    Activation = new HardSigmoid();
                    break;
                case EActivationType.HardTanh:
                    Activation = new HardTanh();
                    break;
                case EActivationType.Identity:
                    Activation = new Identity();
                    break;
                case EActivationType.Logit:
                    Activation = new Logit();
                    break;
                case EActivationType.LReLU:
                    Activation = new LReLU((LReLUSettings)activationSettings);
                    break;
                case EActivationType.Mish:
                    Activation = new Mish();
                    break;
                case EActivationType.ReLU:
                    Activation = new ReLU();
                    break;
                case EActivationType.SeLU:
                    Activation = new SeLU();
                    break;
                case EActivationType.Sigmoid:
                    Activation = new Sigmoid();
                    break;
                case EActivationType.Softmax:
                    Activation = new Softmax();
                    break;
                case EActivationType.Softplus:
                    Activation = new Softplus();
                    break;
                case EActivationType.Softsign:
                    Activation = new Softsign();
                    break;
                case EActivationType.Tanh:
                    Activation = new Tanh();
                    break;
                default:
                    throw new ArgumentException("Activation Type Invalid.");
            }
        }
        
        public abstract void Forward(ref MatrixData input);

        public abstract void Backward(ref MatrixData input);
        
        public abstract ELayerType Type();

        public virtual Dims InShape() => LayerConfig.InShape;

        public virtual Dims OutShape() => LayerConfig.OutShape;

        public int Index { get; private set; }
    }

    public abstract class LayerSettings
    {
        public Dims InShape { get; private set; }
        public Dims OutShape { get; private set; }

        public LayerSettings(Dims inShape, Dims outShape)
        {
            InShape = inShape;
            OutShape = outShape;
        }
    }
}
