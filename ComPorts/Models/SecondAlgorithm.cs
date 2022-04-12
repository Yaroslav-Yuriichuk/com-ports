namespace ComPorts.Models
{
    public class SecondAlgorithm : Algorithm
    {
        public SecondAlgorithm()
        {
            Signal = 0x02;
            OrderOfLedsActivation = new byte[] { 128, 64, 32, 16, 8, 4, 2, 1 };
            Delay = 500;
        }
    }
}
