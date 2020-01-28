using System;
using System.Collections.Generic;
using XNet.Activation.Core;
using XNet.Activation.Utility;
using XNet.Math;
using XNet.Utility;

/******************************************\
 
    TODO: Create class for this.

    XNet Layer Data Architecture:
    
    Forward Prop:
        List<Matrix> mat;
        mat[0] = W;
        mat[1] = X;
        mat[2] = b;
    
    Backward Prop:
        List<List<Matrix> mat;
        mat[0] = MatrixData;
            mat[0][0] = W;
            mat[0][1] = X;
            mat[0][2] = b;
            
        mat[1] = MatrixGrad;
            mat[1][0] = W_grad;
            mat[1][1] = X_grad;
            mat[1][2] = b_grad;

\******************************************/

namespace XNet.Layer.Utility
{
    /// <summary>
    /// Base of all Layer classes
    /// </summary>
    public abstract class Layer
    {
        public EActivationType ActivationType { get; private set; }

        public Activation.Utility.Activation Activation { get; private set; }
        
        public LayerSettings LayerConfig { get; private set; }

        public Layer(ActivationSettings activationSettings, LayerSettings layerSettings)
        {
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

        /// <summary>
        /// This method needs to be overriden on sub-classes.
        /// </summary>
        /// <param name="input">Input Data</param>
        /// <returns>Output data</returns>
        public abstract List<Matrix> Forward(List<Matrix> input);

        /// <summary>
        /// This method needs to be overriden on sub-classes.
        /// </summary>
        /// <param name="input_data">Input Data</param>
        /// <param name="input_grad">Input Gradients</param>
        /// <returns>
        /// out[0] = Output Data 
        /// out[1] = Output Gradients
        /// </returns>
        public abstract List<List<Matrix>> Backward(List<Matrix> input_data, List<Matrix> input_grad);
        
        public abstract ELayerType Type();

        public virtual Dims InShape() => LayerConfig.InShape;

        public virtual Dims OutShape() => LayerConfig.OutShape;
    }

    public abstract class LayerSettings
    {
        public Dims InShape { get; private set; }
        public Dims OutShape { get; private set; }

        protected LayerSettings(Dims inShape, Dims outShape)
        {
            InShape = inShape;
            OutShape = outShape;
        }
    }
}
