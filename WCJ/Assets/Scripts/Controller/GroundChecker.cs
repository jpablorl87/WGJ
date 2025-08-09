using UnityEngine;
namespace Player
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        private bool isGrounded = false;
        public bool IsGrounded => isGrounded;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger Enter: " + collision.gameObject.name);
            if (IsInGroundLayer(collision.gameObject))
            {
                isGrounded = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (IsInGroundLayer(collision.gameObject))
            {
                isGrounded = false;
            }
        }
        private bool IsInGroundLayer(GameObject obj)
        {
            return (groundLayer.value & (1 << obj.layer)) != 0;
        }
    }
}
