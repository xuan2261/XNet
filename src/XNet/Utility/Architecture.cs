// Copyright © 2020 Aryan Mousavi All Rights Reserved.

using System;

namespace XNet.Utility
{
    /// <summary>
    /// Custom exception class.
    /// Can be thrown by Architecture class
    /// </summary>
    public class InvalidArchitecture : Exception
    {
        private string message;

        public string GetMessage()
        {
            return message;
        }

        private void SetMessage(string value)
        {
            message = value;
        }
        
        public InvalidArchitecture(string message)
        {
            SetMessage(message);
        }  
    }

    /// <summary>
    /// Utility class for saving and loading network architecture and type.
    /// </summary>
    public class Architecture
    {
        public Dims Infrastructure { get; set; }

        public Architecture()
        {
            Infrastructure = new Dims();
        }

        /// <summary>
        /// Compiles the data within the class into a string
        /// </summary>
        /// <returns>String containing the data</returns>
        public string Save()
        {
            string tosave = string.Empty;
            foreach (int item in Infrastructure.Values)
            {
                tosave += item.ToString() + Environment.NewLine;
            }
            return tosave;
        }

        /// <summary>
        /// Compiles the data within the string into the data structures of the class
        /// </summary>
        /// <param name="structure">String containing the data</param>
        public void Load(string structure)
        {
            string[] delim = { Environment.NewLine };
            string[] arch = structure.Split(delim, StringSplitOptions.RemoveEmptyEntries);

            if(arch.Length < 1)
            {
                throw new InvalidArchitecture("Architecture does not include network type.");
            }

            Infrastructure.Values.Clear();

            foreach (string item in arch)
            {
                if (int.TryParse(item, out int result))
                {
                    Infrastructure.Values.Add(result);
                }
                else
                {
                    throw new ArgumentException("Could not parse string to int.");
                }
            }
        }
    }
}
