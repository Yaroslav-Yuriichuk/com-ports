using System;
using System.Threading.Tasks;

namespace ComPorts.Services
{
    public interface IComPortsService
    {
        public event Action<byte> OnSignalRecieved;

        public Task<string[]> FindPortsAsync();
        public Task<bool> ActivateAsync(string port);
        public Task<bool> DeactivateAsync(string port);
        public Task SendAsync(byte singnal);
    }
}
