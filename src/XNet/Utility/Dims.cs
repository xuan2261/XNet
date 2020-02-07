// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System.Collections.Generic;

namespace XNet.Utility
{
    /// <summary>
    /// Class used to hold the Dimentions of a storage class.
    /// The length of the List "Dim" will determine the number of dimentions.
    /// The value of each element will determine the width of that dimention.
    /// </summary>
    public class Dims
    {
        public List<int> Values { get; private set; }

        public Dims()
        {
            Values = new List<int>
            {
                Capacity = 2
            };
        }

        public Dims(int x, int y)
        {
            Values = new List<int>
            {
                x,
                y
            };
        }
    }
}
