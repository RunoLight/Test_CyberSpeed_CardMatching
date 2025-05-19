using Application.Services;
using UnityEngine;

namespace Presentation.MonoBehaviour
{
    public class TimerBehaviour : UnityEngine.MonoBehaviour
    {
        private TimerService _timerService;

        public void Init(TimerService timerService)
        {
            _timerService = timerService;
        }

        private void Update()
        {
            _timerService.Tick(Time.deltaTime);
        }
    }
}