using UnityEngine;

namespace Assets.Scripts
{
    public abstract class TopDown2DPlayerControllerBase : MonoBehaviour
    {
        public int playerIdentifier = 0;
        public float speed = 5f;
    
        private string _horizontalAxisName;
        private string _verticalAxisName;

        private float _horizontalInput;
        private float _verticalInput;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Rigidbody2D _rigidBody;

        public void InitAxis(int playerId)
        {
            playerIdentifier = playerId;
            _horizontalAxisName = "";
        }

        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            _horizontalInput = Input.GetAxis(_horizontalAxisName);
            _verticalInput = Input.GetAxis(_verticalAxisName);
        
            Vector3 movement = new Vector3(_horizontalInput, _verticalInput, 0f).normalized;
            
            if (movement.y > 0.01f)
            {
                _animator.SetBool("moveUp", true);
            }
        }

        void FixedUpdate()
        {
            var frameSpeed = speed;
            
            Vector3 movement = new Vector3(_horizontalInput, _verticalInput, 0f).normalized;
            Vector3 acceleration = movement * frameSpeed * Time.deltaTime;
            Vector3 newPos = gameObject.transform.position + acceleration;
            //rigidbody2d.MovePosition(newPos);
        }
    }
}
