using ComPorts.Models;
using System;
using System.Threading.Tasks;

namespace ComPorts.Services
{
    public interface IAlgorithmsService
    {
        public event Action<byte> OnLedsActivation;

        public Task RunRemotelyAsync(IAlgorithm algorithm);

        public Task RunLocallyAsync(byte signal);
    }
}
