using System;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class TopDown2DPlayerController : MonoBehaviour
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        
        public float speed = 100000;
        public float drag = 10000;

        private Vector2 _movement;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Rigidbody2D _rigidBody2D;

        private Player _playerData = null;
        
        void Start()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            if (_rigidBody2D == null)
            {
                throw new ArgumentException($"Missing {nameof(Rigidbody2D)} on {this.gameObject.name} game object.");
            }

            _rigidBody2D.drag = drag;

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
        }

        public void Initialize(Player playerData)
        {
            if (_playerData != null)
            {
                throw new InvalidOperationException($"{_playerData.PlayerName} is already initialized.");
            }

            _playerData = playerData;
        }

        void Update()
        {
            if (_playerData == null)
            {
                return;
            }

            float horizontalInput = Input.GetAxis(HorizontalAxisName + "_" + _playerData.PlayerName);
            float verticalInput = Input.GetAxis(VerticalAxisName + "_" + _playerData.PlayerName);

            _movement = new Vector2(horizontalInput * speed, verticalInput * speed);

            UpdateMovementAnimation();
        }

        private void UpdateMovementAnimation()
        {
            const float movementThreshold = 0.001f;

            _animator.SetInteger(nameof(PlayerDirection), (int)PlayerDirection.NotMoving);

            if (_movement.y > movementThreshold)
            {
                _animator.SetInteger(nameof(PlayerDirection), (int) PlayerDirection.MovingUp);
            }
            else if (_movement.y < -movementThreshold)
            {
                _animator.SetInteger(nameof(PlayerDirection), (int)PlayerDirection.MovingDown);
            }

            if (_movement.x > movementThreshold)
            {
                _animator.SetInteger(nameof(PlayerDirection), (int)PlayerDirection.MovingRight);
            }
            else if (_movement.x < -movementThreshold)
            {
                _animator.SetInteger(nameof(PlayerDirection), (int)PlayerDirection.MovingLeft);
            }
        }

        void FixedUpdate()
        {
            _rigidBody2D.AddForce(_movement);
        }

        public Player GetPlayer()
        {
            if (_playerData == null)
            {
                throw new InvalidOperationException($"{_playerData.PlayerName} is not initialized.");
            }

            return _playerData;
        }
    }
}
