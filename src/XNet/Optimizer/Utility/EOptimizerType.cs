using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XNet.Optimizer.Utility
{
    public enum EOptimizerType
    {
        Invalid,
        AdaDelta,
        AdaGrad,
        Adam,
        Adamax,
        GradientDescent,
        Momentum,
        Nadam,
        NesterovMomentum,
        RMSProp
    }
}
