using System;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;

namespace ComPorts.Services
{
    public class ComPortsService : IComPortsService
    {
        public event Action<byte> OnSignalRecieved;

        private SerialPort _port;

        public ComPortsService()
        {
            _port = new SerialPort();
            _port.DataReceived += OnDataRecieved;
        }

        ~ComPortsService()
        {
            _port.DataReceived -= OnDataRecieved;
        }

        public async Task<bool> ActivateAsync(string port)
        {
            try
            {
                _port.PortName = port;
                _port.Open();
            }
            catch
            {
                MessageBox.Show($"Unable to open port {port}", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public async Task<bool> DeactivateAsync(string port)
        {
            try
            {
                _port.Close();
            }
            catch
            {
                MessageBox.Show($"Unable to close port {port}", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public async Task<string[]> FindPortsAsync()
        {
            return SerialPort.GetPortNames();
        }

        public async Task SendAsync(byte signal)
        {
            byte[] data = new byte[1] { signal };
            _port.Write(data, 0, 1);
        }

        private void OnDataRecieved(object sender, SerialDataReceivedEventArgs e)
        {
            byte input = (byte)_port.ReadByte();
            Console.WriteLine(input);
            OnSignalRecieved?.Invoke(input);
        }
    }
}
