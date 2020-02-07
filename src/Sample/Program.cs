using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XNet.Activation.Core;
using XNet.Activation.Utility;

using XNet.Cost.Core;
using XNet.Cost.Utility;

using XNet.Layer.Core;
using XNet.Layer.Utility;

using XNet.Optimizer.Core;
using XNet.Optimizer.Utility;

using XNet.Regularization.Core;
using XNet.Regularization.Utility;

using XNet.Utility;
using XNet.XMath;
using XNet.Network;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
       {
            Network net = new Network();
            net.CreateLayer(2, ELayerType.FullyConnected, new XNet.Activation.Core.ReLUSettings());
            net.CreateLayer(4, ELayerType.FullyConnected, new XNet.Activation.Core.ReLUSettings());
            net.CreateLayer(1, ELayerType.FullyConnected, new XNet.Activation.Core.ReLUSettings());

            net.InitNetwork(
                ERegularizationType.L2, new L2Settings(0.5), ECostType.CrossEntropyCost,
                new CrossEntropyCostSettings(ERegularizationType.L2, new L2Settings(0.5)), 
                EOptimizerType.GradientDescent, new GradientDescentSettings(0.01));

            double err = 0.0;

            Matrix input0 = new Matrix(2, 1);
            input0[0, 0] = 0;
            input0[1, 0] = 0;
            Matrix output0 = new Matrix(1, 1);
            output0[0, 0] = 0;

            Matrix input1 = new Matrix(2, 1);
            input0[0, 0] = 0;
            input0[1, 0] = 1;
            Matrix output1 = new Matrix(1, 1);
            output0[0, 0] = 1;

            Matrix input2 = new Matrix(2, 1);
            input0[0, 0] = 1;
            input0[1, 0] = 0;
            Matrix output2 = new Matrix(1, 1);
            output0[0, 0] = 1;

            Matrix input3 = new Matrix(2, 1);
            input0[0, 0] = 1;
            input0[1, 0] = 1;

            Matrix output3 = new Matrix(1, 1);
            output0[0, 0] = 0;

            for (int i = 0; i < 10000; i++)
            {
                net.Forward(input0, output0, ref err);
                net.Backward();

                net.Forward(input1, output1, ref err);
                net.Backward();

                net.Forward(input2, output2, ref err);
                net.Backward();

                net.Forward(input3, output3, ref err);
                net.Backward();

                Console.WriteLine("Error: " + err.ToString());
                
            }

            Console.WriteLine(net.Forward(input0, output0, ref err).ToString());
            Console.WriteLine(net.Forward(input1, output1, ref err).ToString());
            Console.WriteLine(net.Forward(input2, output2, ref err).ToString());
            Console.WriteLine(net.Forward(input3, output3, ref err).ToString());
        }
    }
}
