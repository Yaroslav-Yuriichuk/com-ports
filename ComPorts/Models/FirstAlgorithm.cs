namespace ComPorts.Models
{
    public class FirstAlgorithm : Algorithm
    {
        public FirstAlgorithm()
        {
            Signal = 0x01;
            OrderOfLedsActivation = new byte[4] { 24, 36, 66, 129 };
            Delay = 500;
        }
    }
}
