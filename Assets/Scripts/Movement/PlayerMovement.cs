using System;
using System.Collections;
using PlayerInput;
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
        private bool _canRoll;

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
            // set run speed
            var isRunning = Input.GetKey(KeyCode.LeftShift);
            var runningSpeed = isRunning ? runningSpeedMultiplier : 1f;

            // keep the gravitational velocity of the player in place
            if (groundCheck.isGrounded && _gravVelocity.y < 0)
                _gravVelocity.y = -2f;

            // prevent the player from moving if the main menu is open
            if (PlayerInputController.menuActive) return;
            
            // check if the player is initiating a roll this frame
            if (Input.GetKeyDown(KeyCode.LeftControl) && _canRoll)
            {
                ForwardRoll();
                return;
            }
            
            // get X/Z movement
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical") * runningSpeed;
            
            // turn the player based on input
            transform.Rotate(0, horizontal * Time.deltaTime * turnSpeed, 0);

            // determine the player's movement speed
            var moveVelocity = new Vector3(0, 0, vertical);
            moveVelocity *= movementSpeed * Time.deltaTime;
            
            // reduce player's speed to 70% if they're moving backwards
            if (vertical < 0)
                moveVelocity *= 0.7f;

            // actually move the player
            _controller.Move(transform.rotation * moveVelocity);
            
            // runBlend triggers the animation blend tree for walking/running/idling
            var runBlend = 0;

            if (Mathf.Abs(moveVelocity.z) > 0)
                runBlend = 1;
            if (isRunning)
                runBlend *= 2;
            if (vertical < 0)
                runBlend *= -1;
            
            _animator.SetFloat(_runBlendHash, runBlend, 0.1f, Time.deltaTime);

            // if the player jumps, increase upward velocity
            if (Input.GetButtonDown("Jump") && groundCheck.isGrounded)
            {
                _animator.SetBool(_isJumpingHash, true);
                _gravVelocity.y = Mathf.Sqrt(jumpHeight * -2f * Gravity);
            }
            
            // move the player based on the upward and downward gravitational forces being imposed on it
            _gravVelocity.y += Gravity * gravitationalForce * Time.deltaTime;
            _controller.Move(_gravVelocity * Time.deltaTime);
        }

        private void Land() => _animator.SetBool(_isJumpingHash, false);

        private void ForwardRoll()
        {
            _canRoll = false;
            
            // handle the movement logic for the roll here
            
            StartCoroutine(ForwardRollTimer());
        }

        private IEnumerator ForwardRollTimer()
        {
            yield return new WaitForSeconds(1.2f);
            _canRoll = true;
        }
    }
}