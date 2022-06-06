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
        private Vector3 _gravVelocity;

        private const float Gravity = -9.81f;

        private void Awake() => _controller = GetComponent<CharacterController>();

        private void Update()
        {
            var runningSpeed = Input.GetKey(KeyCode.LeftShift) ? runningSpeedMultiplier : 1f;

            if (groundCheck.isGrounded && _gravVelocity.y < 0)
                _gravVelocity.y = -2f;
            
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            transform.Rotate(0, horizontal * Time.deltaTime * turnSpeed, 0);

            var moveVelocity = new Vector3(0, 0, vertical);
            moveVelocity *= movementSpeed * runningSpeed * Time.deltaTime;

            _controller.Move(transform.rotation * moveVelocity);

            if (Input.GetButtonDown("Jump") && groundCheck.isGrounded)
                _gravVelocity.y = Mathf.Sqrt(jumpHeight * -2f * Gravity);
            
            _gravVelocity.y += Gravity * gravitationalForce * Time.deltaTime;
            _controller.Move(_gravVelocity * Time.deltaTime);
        }
    }
}