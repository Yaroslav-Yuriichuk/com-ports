using ComPorts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComPorts.Services
{
    public class AlgorithmsService : IAlgorithmsService
    {
        public event Action<byte> OnLedsActivation;

        private IComPortsService _comPortService;
        private Task _ledsActivationTask;

        public AlgorithmsService(IComPortsService comPortsService)
        {
            _comPortService = comPortsService;
            _comPortService.OnSignalRecieved += OnSignalRecieved;
        }

        public async Task RunRemotelyAsync(IAlgorithm algorithm)
        {
            await _comPortService.SendAsync(algorithm.Signal);
        }

        public async Task RunLocallyAsync(byte signal)
        {
            IAlgorithm algorithm = Algorithm.GetAlgorithm(signal);

            foreach (byte ledActivation in algorithm.OrderOfLedsActivation)
            {
                OnLedsActivation?.Invoke(ledActivation);
                await Task.Delay(algorithm.Delay);
            }
            OnLedsActivation?.Invoke(Algorithm.AllDeactivatedLeds);
        }

        public void OnSignalRecieved(byte signal)
        {
            if (_ledsActivationTask == null || _ledsActivationTask.IsCompleted)
            {
                _ledsActivationTask = RunLocallyAsync(signal);
            }
        }
    }
}
