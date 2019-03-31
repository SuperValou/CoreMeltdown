using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUDs
{
    public class PulsableLabel : MonoBehaviour
    {
        public float intensity = 1;
        public float frequency = 1;
        public float threshold = 0.8f;

        private Vector3 _startingScale;

        private Text label;

        void Start()
        {
            label = GetComponent<Text>();
            if (label == null)
            {
                throw new ArgumentException($"Missing {nameof(Text)} on {this.gameObject.name} game object.");
            }

            _startingScale = this.transform.localScale;
        }

        void Update ()
        {
            double wave = Math.Sin(Math.PI * frequency * Time.realtimeSinceStartup);
            float spike = Mathf.Clamp01((float)Math.Abs(wave) - threshold);
        
            this.gameObject.transform.localScale = _startingScale + Vector3.one * spike * intensity;
        }
    }
}
