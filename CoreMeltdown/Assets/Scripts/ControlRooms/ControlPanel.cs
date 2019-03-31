using System;
using System.Diagnostics;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.ControlRooms
{
    public class ControlPanel : MonoBehaviour
    {
        public float radioactiveAcceleration = 1;
        public float radioactiveIntensity = 1;

        public AudioClip alarm;
        public AudioClip click;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private AudioSource _audioSource;

        public bool IsTurnedOn { get; private set; }
        
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
            {
                throw new ArgumentException($"Missing {nameof(SpriteRenderer)} on {this.gameObject.name} game object.");
            }

            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                throw new ArgumentException($"Missing {nameof(Animator)} on {this.gameObject.name} game object.");
            }

            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                throw new ArgumentException($"Missing {nameof(AudioSource)} on {this.gameObject.name} game object.");
            }

            _audioSource.clip = alarm;
        }

        public void TurnOn()
        {
            if (IsTurnedOn)
            {
                return;
            }

            IsTurnedOn = true;
            _animator.SetBool("Alarm", true);

            _stopwatch.Restart();
            
            _audioSource.Play();
        }

        public void TurnOff()
        {
            if (!IsTurnedOn)
            {
                return;
            }

            IsTurnedOn = false;
            _animator.SetBool("Alarm", false);

            _stopwatch.Reset();

            _audioSource.Stop();
            _audioSource.PlayOneShot(click);
        }

        void OnTriggerEnter2D(Collider2D collider2D)
        {
            var playerController = collider2D.gameObject.GetComponent<TopDown2DPlayerController>();
            if (playerController == null)
            {
                return;
            }

            TurnOff();
        }

        void OnDestroy()
        {
            _stopwatch.Stop();
        }

        public float GetRadioactivity()
        {
            if (!IsTurnedOn)
            {
                return 0;
            }
            
            float factor = (float) _stopwatch.Elapsed.TotalMilliseconds / 1000 * radioactiveAcceleration;
            float radioactivity = radioactiveIntensity * factor;
            return radioactivity;
        }
    }
}
