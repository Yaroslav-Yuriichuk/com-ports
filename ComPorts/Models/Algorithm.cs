using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPorts.Models
{
    public abstract class Algorithm : IAlgorithm
    {
        public byte Signal { get; protected set; }
        public byte[] OrderOfLedsActivation { get; protected set; }
        public int Delay { get; protected set; }

        public static readonly IAlgorithm FirstAlgorithm = new FirstAlgorithm();
        public static readonly IAlgorithm SecondAlgorithm = new SecondAlgorithm();

        public static readonly byte AllDeactivatedLeds = 0x00;

        private static Dictionary<byte, IAlgorithm> _inputSignalToAlgirithm = new Dictionary<byte, IAlgorithm>()
        {
            { 0x01, FirstAlgorithm },
            { 0x02, SecondAlgorithm }
        };

        public static IAlgorithm GetAlgorithm(byte inputSignal)
        {
            try
            {
                return _inputSignalToAlgirithm[inputSignal];
            }
            catch (Exception)
            {
                return new FirstAlgorithm();
            }
        }
    }
}
