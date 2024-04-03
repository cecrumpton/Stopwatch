using Prism.Mvvm;
using System;
using System.Timers;

namespace Stopwatch.Model
{
    public class StopwatchModel : BindableBase
    {
        private readonly Timer _timer;
        private TimeSpan _elapsedTime;

        public StopwatchModel()
        {
            _timer = new Timer(10);
            _timer.Elapsed += TimerElapsed;
        }

        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            private set
            {
                if(_elapsedTime != value) 
                {
                    _elapsedTime = value;
                    RaisePropertyChanged(nameof(ElapsedTime));
                }
            }
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            ElapsedTime = ElapsedTime.Add(TimeSpan.FromMilliseconds(10));
        }
        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Reset()
        {
            ElapsedTime = TimeSpan.Zero;
        }

        //public event PropertyChangedEventHandler? PropertyChanged;

        //protected virtual void OnPropertyChanged(string? propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
