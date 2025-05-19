using System;

namespace Application.Services
{
    public class TimerService
    {
        public event Action<double> TimeUpdated;

        private double _elapsed;
        private bool _isEnabled;

        public void Start() => _isEnabled = true;
        public void Stop() => _isEnabled = false;
        public void SetTime(double elapsedSeconds) => _elapsed = elapsedSeconds;
        public double GetTime() => _elapsed;
        
        public void Tick(float deltaTime)
        {
            if (!_isEnabled)
                return;

            _elapsed += deltaTime;
            TimeUpdated?.Invoke(_elapsed);
        }
    }
}