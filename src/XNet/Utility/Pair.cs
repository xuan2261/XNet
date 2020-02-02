// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;

namespace XNet.Utility
{
    /// <summary>
    /// Pair Call Event handler.
    /// </summary>
    /// <param name="sender">Sender Object</param>
    /// <param name="args">Sender Arguments</param>
    public delegate void PairEventHandler(Pair sender, PairEventArgs args);
    
    /// <summary>
    /// Event Argument class for Pair's Call event.
    /// </summary>
    public class PairEventArgs : EventArgs
    {
        public PairEventArgs(float primary, float secondary)
        {
            Primary = primary;
            Secondary = secondary;
        }

        public float Primary { get; private set; }
        public float Secondary { get; private set; }
    }

    /// <summary>
    /// Pairs two floating point values together
    /// The Call Event can be used to apply custom logic that works with those two values
    /// </summary>
    public class Pair
    {
        public Pair(float primary = 0.0f, float secondary = 0.0f)
        {
            Primary = primary;
            Secondary = secondary;
        }

        public void RaiseEvent() => Call?.Invoke(this, new PairEventArgs(Primary, Secondary));
 
        public float Primary { get; set; }
        public float Secondary { get; set; }

        public event PairEventHandler Call;
    }
}
