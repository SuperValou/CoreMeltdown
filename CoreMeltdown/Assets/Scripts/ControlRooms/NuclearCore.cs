using System;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.ControlRooms
{
    public class NuclearCore : MonoBehaviour
    {
        public ControlPanel[] controlPanels;

        public float failureProbabilityPerSecond = 0.5f; // chance of a control panel to fail at each second

        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly Random _random = new Random();

        public float RadiationsPerSecond { get; private set; }

        void Start()
        {
            _stopwatch.Start();
        }

        void Update()
        {
            float failureProbability = Time.deltaTime * failureProbabilityPerSecond;
            failureProbability = Mathf.Clamp(failureProbability, 0, 1);

            if (_random.NextDouble() < failureProbability)
            {
                CreateFailure();
            }
            
            RadiationsPerSecond = controlPanels.Sum(panel => panel.GetRadioactivity());
        }
        
        private void CreateFailure()
        {
            var faultingPanelIndex = _random.Next(controlPanels.Length);
            controlPanels[faultingPanelIndex].TurnOn();
        }

        void OnDestroy()
        {
            _stopwatch.Stop();
        }
    }
}