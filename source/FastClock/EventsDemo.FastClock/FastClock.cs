using System;
using System.Windows.Threading;

namespace EventsDemo.FastClock
{
    public class FastClock
    {
        private bool _isRunning;
        private DateTime _time;
        private readonly DispatcherTimer _timer;
        public DateTime Time
        {
            get => _time;

            set
            {
                _time = value;
            }
        }
        private FastClock()
        {
            Time = DateTime.Now;
            _timer = new DispatcherTimer();
            _timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Time = Time.AddMinutes(1);
            OnOneMinuteIsOver(Time);
        }

        public static FastClock Instance { get; set; } = null;

        private static FastClock GetInstance()
        {
            if (Instance == null)
            {
                Instance = new FastClock();
            }
            return Instance;
        }

        private event EventHandler<DateTime> OneMinuteIsOver;

        public bool IsRunning
        {
            get => _isRunning;

            set
            {
                if (!_isRunning && value)
                {
                    _timer.Start();
                }
                else if (_isRunning && !value)
                {
                    _timer.Stop();
                }

                _isRunning = value;
            }
        }

        protected virtual void OnOneMinuteIsOver(DateTime time)
        {
            OneMinuteIsOver?.Invoke(this, time);
        }
    }
}
