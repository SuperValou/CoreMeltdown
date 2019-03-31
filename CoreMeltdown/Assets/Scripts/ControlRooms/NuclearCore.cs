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

        public float radioactivityPerPanel = 1;
        public float failureProbabilityPerSecond = 0.5f; // chance of a control panel to fail at each second

        public float failureProbabilityAcceleration = 1;

        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly Random _random = new Random();

        public float RadiationsPerSecond { get; private set; }

        void Start()
        {
            _stopwatch.Start();
        }

        void Update()
        {
            float factor = 1f - 1f / (1 + (float) _stopwatch.Elapsed.TotalSeconds);

            float failureProbability = Time.deltaTime * failureProbabilityPerSecond;
            failureProbability = Mathf.Clamp(failureProbability, 0, 1);

            if (_random.NextDouble() < failureProbability)
            {
                CreateFailure();
            }

            var failingPanels = controlPanels.Count(p => p.IsTurnedOn);
            RadiationsPerSecond = failingPanels * radioactivityPerPanel;
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