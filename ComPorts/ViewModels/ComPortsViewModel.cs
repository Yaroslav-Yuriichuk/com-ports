using Autofac;
using ComPorts.Commands;
using ComPorts.DI;
using ComPorts.Models;
using ComPorts.Services;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ComPorts.ViewModels
{
    public class ComPortsViewModel : ViewModel
    {
        private const int LedsNumber = 8;

        public ComPortsViewModel()
        {
            DIInstaller.Build();

            Activate = new Command((obj) => OnActivate(), (obj) => !IsPortActivated && !string.IsNullOrEmpty(SelectedPort));
            Deactivate = new Command((obj) => OnDeactivate(), (obj) => IsPortActivated);
            RunFirstAlgorithm = new Command((obj) => OnFirstAlgorithmRun());
            RunSecondAlgorithm = new Command((obj) => OnSecondAlgorithmRun());

            _algorithmsService = DIInstaller.Container.Resolve<IAlgorithmsService>();
            _comPortsService = DIInstaller.Container.Resolve<IComPortsService>();

            _algorithmsService.OnLedsActivation += ActivateLeds;
            FindPorts();
        }

        ~ComPortsViewModel()
        {
            _comPortsService.DeactivateAsync(SelectedPort);
            _algorithmsService.OnLedsActivation -= ActivateLeds;
        }

        private IAlgorithmsService _algorithmsService;
        private IComPortsService _comPortsService;

        #region Properties

        private bool _isPortActivated = false;

        public bool IsPortActivated
        {
            set
            {
                if (_isPortActivated == value)
                {
                    return;
                }

                _isPortActivated = value;
                OnPropertyChanged();
            }
            get => _isPortActivated;
        }

        private string[] _ports;

        public string[] Ports
        {
            set
            {
                if (_ports == value)
                {
                    return;
                }

                _ports = value;
                OnPropertyChanged();
            }
            get => _ports;
        }

        private string _selectedPort = null;

        public string SelectedPort
        {
            set
            {
                if (_selectedPort == value)
                {
                    return;
                }

                _selectedPort = value;
                OnPropertyChanged();
            }
            get => _selectedPort;
        }

        private Led[] _leds = (from _ in Enumerable.Range(0, LedsNumber) select new Led()).ToArray();

        public Led[] Leds
        {
            set
            {
                if (_leds == value)
                {
                    return;
                }

                _leds = value;
                OnPropertyChanged();
            }
            get => _leds;
        }

        #endregion

        #region Commands

        public ICommand Activate { get; }

        private async void OnActivate()
        {
            IsPortActivated = await _comPortsService.ActivateAsync(SelectedPort);
        }

        public ICommand Deactivate { get; }

        private async void OnDeactivate()
        {
            IsPortActivated = !await _comPortsService.DeactivateAsync(SelectedPort);
        }

        public ICommand RunFirstAlgorithm { get; }

        public void OnFirstAlgorithmRun()
        {
            _algorithmsService.RunRemotelyAsync(Algorithm.FirstAlgorithm);
        }

        public ICommand RunSecondAlgorithm { get; }

        public void OnSecondAlgorithmRun()
        {
            _algorithmsService.RunRemotelyAsync(Algorithm.SecondAlgorithm);
        }

        #endregion

        #region Methods

        private async Task FindPorts()
        {
            Ports = await _comPortsService.FindPortsAsync();
        }

        private void ActivateLeds(byte ledsActivation)
        {
            var reactivatedLeds = (from _ in Enumerable.Range(0, LedsNumber) select new Led()).ToArray();

            for (int i = 0; i < LedsNumber; i++)
            {
                reactivatedLeds[LedsNumber - i - 1].IsActivated = (ledsActivation & 1) == 1;
                ledsActivation >>= 1;
            }

            Leds = reactivatedLeds;
        }

        #endregion
    }
}
