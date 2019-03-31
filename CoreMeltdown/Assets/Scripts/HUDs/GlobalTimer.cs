using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.HUDs
{
    public class GlobalTimer : MonoBehaviour
    {
        public float maxSeconds = 90;
        public TimerHUD timerHUD;
        
        private readonly Stopwatch _stopwatch = new Stopwatch();
        
        public bool TimeIsUp { get; private set; }

        void Start()
        {
            _stopwatch.Start();
            TimeIsUp = false;
        }
        
        void Update()
        {
            float timeToDisplay = maxSeconds - (float) _stopwatch.Elapsed.TotalMilliseconds / 1000f;

            if (timeToDisplay <= 0)
            {
                timeToDisplay = 0;
                TimeIsUp = true;
            }

            timerHUD.DisplayTime(timeToDisplay);
        }

        public void Restart()
        {
            _stopwatch.Restart();
        }

        void OnDestroy()
        {
            _stopwatch.Stop();
        }
    }
}