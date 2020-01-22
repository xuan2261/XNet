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
        public XNet.Network.Utility.ENetworkType NetworkType { get; set; }
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

            tosave = NetworkType.ToString() + Environment.NewLine;

            switch (NetworkType)
            {
                case Network.Utility.ENetworkType.Invalid:
                    {
                        tosave = "Invalid";
                        break;
                    }
                case Network.Utility.ENetworkType.AE:
                    {
                        tosave = "AE";
                        break;
                    }
                case Network.Utility.ENetworkType.DBM:
                    {
                        tosave = "DBM";
                        break;
                    }
                case Network.Utility.ENetworkType.DCIGN:
                    {
                        tosave = "DCIGN";
                        break;
                    }
                case Network.Utility.ENetworkType.DCN:
                    {
                        tosave = "DCN";
                        break;
                    }
                case Network.Utility.ENetworkType.DFFN:
                    {
                        tosave = "DFFN";
                        break;
                    }
                case Network.Utility.ENetworkType.DN:
                    {
                        tosave = "InvDNalid";
                        break;
                    }
                case Network.Utility.ENetworkType.DRN:
                    {
                        tosave = "DRN";
                        break;
                    }
                case Network.Utility.ENetworkType.ELM:
                    {
                        tosave = "ELM";
                        break;
                    }
                case Network.Utility.ENetworkType.ESN:
                    {
                        tosave = "ESN";
                        break;
                    }
                case Network.Utility.ENetworkType.GAN:
                    {
                        tosave = "GAN";
                        break;
                    }
                case Network.Utility.ENetworkType.GRU:
                    {
                        tosave = "GRU";
                        break;
                    }
                case Network.Utility.ENetworkType.KN:
                    {
                        tosave = "KN";
                        break;
                    }
                case Network.Utility.ENetworkType.LSM:
                    {
                        tosave = "LSM";
                        break;
                    }
                case Network.Utility.ENetworkType.LSTM:
                    {
                        tosave = "LSTM";
                        break;
                    }
                case Network.Utility.ENetworkType.NTM:
                    {
                        tosave = "NTM";
                        break;
                    }
                case Network.Utility.ENetworkType.RBM:
                    {
                        tosave = "RBM";
                        break;
                    }
                case Network.Utility.ENetworkType.RNN:
                    {
                        tosave = "RNN";
                        break;
                    }
                case Network.Utility.ENetworkType.SVM:
                    {
                        tosave = "SVM";
                        break;
                    }
                default:
                    {
                        tosave = "Invalid";
                        break;
                    }
            }

            tosave += Environment.NewLine;

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

            if (arch[0] == "AE")
            {
                NetworkType = Network.Utility.ENetworkType.AE;
            }
            else if (arch[0] == "DBM")
            {
                NetworkType = Network.Utility.ENetworkType.DBM;
            }
            else if (arch[0] == "DCIGN")
            {
                NetworkType = Network.Utility.ENetworkType.DCIGN;
            }
            else if (arch[0] == "DCN")
            {
                NetworkType = Network.Utility.ENetworkType.DCN;
            }
            else if (arch[0] == "DFFN")
            {
                NetworkType = Network.Utility.ENetworkType.DFFN;
            }
            else if (arch[0] == "DN")
            {
                NetworkType = Network.Utility.ENetworkType.DN;
            }
            else if (arch[0] == "DRN")
            {
                NetworkType = Network.Utility.ENetworkType.DRN;
            }
            else if (arch[0] == "ELM")
            {
                NetworkType = Network.Utility.ENetworkType.ELM;
            }
            else if (arch[0] == "ESN")
            {
                NetworkType = Network.Utility.ENetworkType.ESN;
            }
            else if (arch[0] == "GAN")
            {
                NetworkType = Network.Utility.ENetworkType.GAN;
            }
            else if (arch[0] == "GRU")
            {
                NetworkType = Network.Utility.ENetworkType.GRU;
            }
            else if (arch[0] == "KN")
            {
                NetworkType = Network.Utility.ENetworkType.KN;
            }
            else if (arch[0] == "LSM")
            {
                NetworkType = Network.Utility.ENetworkType.LSM;
            }
            else if (arch[0] == "LSTM")
            {
                NetworkType = Network.Utility.ENetworkType.LSTM;
            }
            else if (arch[0] == "NTM")
            {
                NetworkType = Network.Utility.ENetworkType.NTM;
            }
            else if (arch[0] == "RBM")
            {
                NetworkType = Network.Utility.ENetworkType.RBM;
            }
            else if (arch[0] == "RNN")
            {
                NetworkType = Network.Utility.ENetworkType.RNN;
            }
            else if (arch[0] == "SVM")
            {
                NetworkType = Network.Utility.ENetworkType.SVM;
            }
            else
            {
                NetworkType = Network.Utility.ENetworkType.Invalid;
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
                    throw new ArgumentException();
                }
            }
        }
    }
}
