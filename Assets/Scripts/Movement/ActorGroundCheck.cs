using UnityEngine;

namespace Movement
{
    public class ActorGroundCheck : MonoBehaviour
    {
        public bool isGrounded;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Terrain"))
            {
                isGrounded = true;
                PlayerMovement.onLanding();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Terrain"))
                isGrounded = false;
        }
    }
}
