using Prism.Commands;
using Prism.Mvvm;
using Stopwatch.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

namespace Stopwatch.ViewModel
{
    public class StopwatchViewModel : BindableBase
    {
        private readonly StopwatchModel _model;
        private ObservableCollection<string> _laps;
        private bool _isRunning;

        public StopwatchViewModel()
        {
            _model = new StopwatchModel();
            _laps = new ObservableCollection<string>();
            StartCommand = new DelegateCommand(Start);
            StopCommand = new DelegateCommand(Stop);
            ResetCommand = new DelegateCommand(Reset);
            LapCommand = new DelegateCommand(Lap);
            _model.PropertyChanged += Model_PropertyChanged;
        }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand LapCommand { get; private set; }

        public string ElapsedTime
        {
            get => _model.ElapsedTime.ToString(@"mm\:ss\.ff", CultureInfo.InvariantCulture);
        }

        public ObservableCollection<string> Laps
        {
            get => _laps;
            set
            {
                _laps = value;
                RaisePropertyChanged(nameof(Laps));
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    RaisePropertyChanged(nameof(IsRunning));
                }
            }
        }

        private void Start()
        {
            _model.Start();
            IsRunning = true;
        }

        private void Stop()
        {
            _model.Stop();
            IsRunning = false;
        }

        private void Reset()
        {
            _model.Reset();
            Laps.Clear();
            RaisePropertyChanged(nameof(ElapsedTime));
        }

        private void Lap()
        {
            _laps.Add(_model.ElapsedTime.ToString(@"mm\:ss\.ff", CultureInfo.InvariantCulture));
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(ElapsedTime));
        }

    }
}
