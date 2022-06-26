using System;
using UnityEngine;

namespace Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float runningSpeedMultiplier;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float turnSpeed;
        [SerializeField] private ActorGroundCheck groundCheck;
        [SerializeField] private float gravitationalForce;

        private CharacterController _controller;
        private Animator _animator;
        private Vector3 _gravVelocity;
        private int _runBlendHash;
        private int _isJumpingHash;
        private bool _hasJumped;

        private const float Gravity = -9.81f;

        public static Action onLanding;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponentInChildren<Animator>();
            _runBlendHash = Animator.StringToHash("Run Blend");
            _isJumpingHash = Animator.StringToHash("isJumping");

            onLanding += Land;
        }

        private void Update()
        {
            var isRunning = Input.GetKey(KeyCode.LeftShift);
            var runningSpeed = isRunning ? runningSpeedMultiplier : 1f;

            if (groundCheck.isGrounded && _gravVelocity.y < 0)
                _gravVelocity.y = -2f;
            
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical") * runningSpeed;
            
            transform.Rotate(0, horizontal * Time.deltaTime * turnSpeed, 0);

            var moveVelocity = new Vector3(0, 0, vertical);
            moveVelocity *= movementSpeed * Time.deltaTime;
            
            if (vertical < 0)
                moveVelocity *= 0.7f;

            _controller.Move(transform.rotation * moveVelocity);
            
            var runBlend = 0;

            if (Mathf.Abs(moveVelocity.z) > 0)
                runBlend = 1;
            if (isRunning)
                runBlend *= 2;
            if (vertical < 0)
                runBlend *= -1;
            
            _animator.SetFloat(_runBlendHash, runBlend, 0.1f, Time.deltaTime);

            if (Input.GetButtonDown("Jump") && groundCheck.isGrounded)
            {
                _animator.SetBool(_isJumpingHash, true);
                _gravVelocity.y = Mathf.Sqrt(jumpHeight * -2f * Gravity);
            }
            
            _gravVelocity.y += Gravity * gravitationalForce * Time.deltaTime;
            _controller.Move(_gravVelocity * Time.deltaTime);
        }

        private void Land() => _animator.SetBool(_isJumpingHash, false);
    }
}