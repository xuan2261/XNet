// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using XNet.Math;

namespace XNet.Activation.Utility
{
    public abstract class Activation
    {
        // Forward Method -> 
        public abstract Matrix Forward(Matrix input);

        // Backward Method <- (Derivative)
        public abstract Matrix Backward(Matrix input);

        // Activate Method for a single value
        public abstract double Activate(double input);

        // Derivative Method for a single value
        public abstract double Derivative(double input);

        // Gets the type of the Activation Function Class
        public abstract EActivationType Type();
        
    }

    public abstract class ActivationSettings
    {
        // Gets the type of the Activation Settings Class
        public abstract EActivationType Type();
    }
}
