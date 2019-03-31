using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.HUDs
{
    public class GlobalTimer : MonoBehaviour
    {
        public float maxSeconds = 90;
        public HeadUpDisplayManager headUpDisplayManager;
        
        private readonly Stopwatch _stopwatch = new Stopwatch();
        
        void Start()
        {
            _stopwatch.Start();
        }
        
        void Update()
        {
            float timeToDisplay = maxSeconds - (float) _stopwatch.Elapsed.TotalMilliseconds / 1000f;

            if (timeToDisplay <= 0)
            {
                timeToDisplay = 0;
                Invoke(nameof(LoadVictoryScreen), 2);
            }

            headUpDisplayManager.DisplayTime(timeToDisplay);
        }

        public void LoadVictoryScreen()
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
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