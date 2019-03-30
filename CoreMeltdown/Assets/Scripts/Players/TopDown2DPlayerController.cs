using System;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class TopDown2DPlayerController : MonoBehaviour
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        
        public string playerIdentifier = string.Empty;
        public float speed = 100000;
        public float drag = 10000;

        private Vector2 _movement;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Rigidbody2D _rigidBody2D;

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

        void Update()
        {
            float horizontalInput = Input.GetAxis(HorizontalAxisName + playerIdentifier);
            float verticalInput = Input.GetAxis(VerticalAxisName + playerIdentifier);

            _movement = new Vector2(horizontalInput * speed, verticalInput * speed);

            UpdateMovementAnimation();
            
        }

        private void UpdateMovementAnimation()
        {
            const float movementThreshold = 0.001f;
            
            if (_movement.y > movementThreshold)
            {
                _animator.SetInteger(nameof(PlayerMovement), (int) PlayerMovement.MovingUp);
            }
            else if (_movement.y < -movementThreshold)
            {
                _animator.SetInteger(nameof(PlayerMovement), (int)PlayerMovement.MovingDown);
            }

            if (_movement.x > movementThreshold)
            {
                _animator.SetInteger(nameof(PlayerMovement), (int)PlayerMovement.MovingRight);
            }
            else if (_movement.x < -movementThreshold)
            {
                _animator.SetInteger(nameof(PlayerMovement), (int)PlayerMovement.MovingLeft);
            }
        }

        void FixedUpdate()
        {
            _rigidBody2D.AddForce(_movement);
        }
    }
}
