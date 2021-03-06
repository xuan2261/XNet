﻿using System;
using System.Collections.Generic;
using XNet.XMath;

using XNet.Cost.Core;
using XNet.Cost.Utility;

using XNet.Layer.Core;
using XNet.Layer.Utility;

using XNet.Optimizer.Core;
using XNet.Optimizer.Utility;

using XNet.Activation.Core;
using XNet.Activation.Utility;

using XNet.Regularization.Core;
using XNet.Regularization.Utility;

namespace XNet.Network
{
    public class Network
    {
        public Network()
        {
            Data = new MatrixData();
            Layers = new List<Layer.Utility.Layer>();
        }

        public Matrix Forward(Matrix input, Matrix expected, ref double error)
        {
            // Initiate the Input Matrix Data in MatrixData class
            Data.Data["a0"] = input;
            MatrixData data = Data;
            
            for (int i = 1; i < Layers.Count; i++)
            {
                Layers[i].Forward(ref data);
            }

            Data = data;

            m_actual = Data.Data["a" + (Layers.Count - 1).ToString()];
            m_expected = expected;

            error = CostFunction.Forward(Data.Data["a" + (Layers.Count - 1).ToString()], expected, Data, Layers.Count);

            return Data.Data["a" + (Layers.Count - 1).ToString()];

        }

        public double Backward()
        {
            Data.Data["da" + (Layers.Count - 1).ToString()] = CostFunction.Backward(m_actual, m_expected, Data, Layers.Count);

            MatrixData data = Data;
            for (int i = Layers.Count - 1; i > 0; i--)
            {
                Layers[i].Backward(ref data);
            }
            Data = data;

            // CostFunction.ResetCost();

            return CostFunction.BatchCost;
        }

        public void Optimize()
        {
            MatrixData data = Data;
            for (int i = 1; i < Layers.Count; i++)
            {
                Data.Data["W" + i.ToString()] -= Matrix.Transpose(OptimizerFunction.CalculateDeltaW(Data.Data["W" + i.ToString()], Data.Data["dW" + i.ToString()]));
                Data.Data["b" + i.ToString()] -= OptimizerFunction.CalculateDeltaB(Data.Data["b" + i.ToString()], Data.Data["db" + i.ToString()]);
            }
            Data = data;
        }

        public bool CreateLayer(int nCount, ELayerType type, ActivationSettings activationSettings)
        {
            Layer.Utility.Layer layer;
            switch (type)
            {
                case ELayerType.Invalid:
                    throw new ArgumentException("Invalid \"type\" argument.");
                case ELayerType.AveragePooling:
                    layer = new AveragePooling(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.AverageUnpooling:
                    layer = new AverageUnpooling(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.Convolutional:
                    layer = new Convolutional(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.Deconvolutional:
                    layer = new Deconvolutional(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.Dropout:
                    layer = new Dropout(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.FullyConnected:
                    layer = new FullyConnected(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.GatedRecurrent:
                    layer = new GatedRecurrent(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.LSTM:
                    layer = new LSTM(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.MaxPooling:
                    layer = new MaxPooling(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.MaxUnpooling:
                    layer = new MaxUnpooling(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                case ELayerType.Recurrent:
                    layer = new Recurrent(nCount, Layers.Count, activationSettings);
                    Layers.Add(layer);
                    return true;
                default:
                    throw new ArgumentException("Invalid \"type\" argument.");
            }
        }

        public void InitNetwork(ECostType costType, CostSettings costSettings, EOptimizerType optimizerType, OptimizerSettings optimizerSettings)
        {
            Utility.Dims InShape;
            Utility.Dims OutShape;
            Utility.Dims WShape;

            for (int i = 1; i < Layers.Count; i++)
            {
                Data.Data["a" + i.ToString()] = new Matrix(Layers[i].NCount, 1);
                InShape = new Utility.Dims(Layers[i].NCount, 1);

                Data.Data["b" + i.ToString()] = Matrix.RandomMatrix(Layers[i].NCount, 1, 1, EDistrubution.Gaussian);

                OutShape = new Utility.Dims(Layers[i].NCount, 1);

                Data.Data["W" + i.ToString()] = Matrix.RandomMatrix(Layers[i - 1].NCount, Layers[i].NCount, 1, EDistrubution.Gaussian);
                WShape = new Utility.Dims(Layers[i - 1].NCount, Layers[i].NCount);

                Layers[i].SetSettings(new LayerSettings(InShape, OutShape, WShape));
            }

            Data.Data["a0"] = new Matrix(Layers[0].NCount, 1);
            InShape = new Utility.Dims(Layers[0].NCount, 1);

            Data.Data["b0"] = new Matrix(Layers[0].NCount, 1);
            OutShape = new Utility.Dims(Layers[0].NCount, 1);

            Data.Data["W0"] = new Matrix(Layers[0].NCount * Layers[1].NCount , Layers[1].NCount);
            WShape = new Utility.Dims(Layers[0].NCount * Layers[1].NCount , Layers[1].NCount);

            Layers[0].SetSettings(new LayerSettings(InShape, OutShape, WShape));

            switch (costType)
            {
                case ECostType.Invalid:
                    throw new ArgumentException("Invalid Cost Function Selected!");
                case ECostType.CrossEntropyCost:
                    CostFunction = new CrossEntropyCost((CrossEntropyCostSettings)costSettings);
                    break;
                case ECostType.ExponentionalCost:
                    CostFunction = new ExponentionalCost((ExponentionalCostSettings)costSettings);
                    break;
                case ECostType.GeneralizedKullbackLeiblerDivergence:
                    CostFunction = new GeneralizedKullbackLeiblerDivergence((GeneralizedKullbackLeiblerDivergenceSettings)costSettings);
                    break;
                case ECostType.HellingerDistance:
                    CostFunction = new HellingerDistance((HellingerDistanceSettings)costSettings);
                    break;
                case ECostType.ItakuraSaitoDistance:
                    CostFunction = new ItakuraSaitoDistance((ItakuraSaitoDistanceSettings)costSettings);
                    break;
                case ECostType.KullbackLeiblerDivergence:
                    CostFunction = new KullbackLeiblerDivergence((KullbackLeiblerDivergenceSettings)costSettings);
                    break;
                case ECostType.QuadraticCost:
                    CostFunction = new QuadraticCost((QuadraticCostSettings)costSettings);
                    break;
                default:
                    throw new ArgumentException("Invalid Cost Function Selected!");
            }

            switch (optimizerType)
            {
                case EOptimizerType.Invalid:
                    throw new ArgumentException("Invalid Optimizer Function Selected!");
                case EOptimizerType.AdaDelta:
                    OptimizerFunction = new AdaDelta((AdaDeltaSettings)optimizerSettings);
                    break;
                case EOptimizerType.AdaGrad:
                    OptimizerFunction = new AdaGrad((AdaGradSettings)optimizerSettings);
                    break;
                case EOptimizerType.Adam:
                    OptimizerFunction = new Adam((AdamSettings)optimizerSettings);
                    break;
                case EOptimizerType.Adamax:
                    OptimizerFunction = new Adamax((AdamaxSettings)optimizerSettings);
                    break;
                case EOptimizerType.GradientDescent:
                    OptimizerFunction = new GradientDescent((GradientDescentSettings)optimizerSettings);
                    break;
                case EOptimizerType.Momentum:
                    OptimizerFunction = new Momentum((MomentumSettings)optimizerSettings);
                    break;
                case EOptimizerType.Nadam:
                    OptimizerFunction = new Nadam((NadamSettings)optimizerSettings);
                    break;
                case EOptimizerType.NesterovMomentum:
                    OptimizerFunction = new NesterovMomentum((NesterovMomentumSettings)optimizerSettings);
                    break;
                case EOptimizerType.RMSProp:
                    OptimizerFunction = new RMSProp((RMSPropSettings)optimizerSettings);
                    break;
                default:
                    throw new ArgumentException("Invalid Optimizer Function Selected!");
            }
        }

        public Regularization.Utility.Regularization RegularizationFunction { get; private set; }

        public Cost.Utility.Cost CostFunction { get; private set; }

        public Optimizer.Utility.Optimizer OptimizerFunction { get; private set; }

        public MatrixData Data { get; private set; }

        public List<Layer.Utility.Layer> Layers { get; private set; }

        private Matrix m_actual;

        private Matrix m_expected;

    }
}
