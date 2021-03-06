﻿// Copyright © 2020 Aryan Mousavi All Rights Reserved.

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
        public PairEventArgs(double primary, double secondary)
        {
            Primary = primary;
            Secondary = secondary;
        }

        public double Primary { get; private set; }
        public double Secondary { get; private set; }
    }

    /// <summary>
    /// Pairs two double values together
    /// The Call Event can be used to apply custom logic that works with those two values
    /// </summary>
    public class Pair
    {
        public Pair(double primary = 0.0f, double secondary = 0.0f)
        {
            Primary = primary;
            Secondary = secondary;
        }

        public void RaiseEvent() => Call?.Invoke(this, new PairEventArgs(Primary, Secondary));
 
        public double Primary { get; set; }

        public double Secondary { get; set; }

        public event PairEventHandler Call;
    }
}
