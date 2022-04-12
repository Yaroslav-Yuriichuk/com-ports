namespace ComPorts.Models
{
    public interface IAlgorithm
    {
        public byte Signal { get; }

        public byte[] OrderOfLedsActivation { get; }

        public int Delay { get; }
    }
}
