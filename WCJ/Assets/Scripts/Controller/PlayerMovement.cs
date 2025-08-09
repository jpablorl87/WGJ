using UnityEngine;
namespace Player
{
    //If the player doesn't have a rigidbody, there will be an alert
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        //We give some values
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float sprintMultiplier = 1.5f;
        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float dashForce = 12f;
        [SerializeField] private float dashCooldown = 1f;
        //We need a transfor to check if our player is grounded
        //[SerializeField] private Transform groundCheck;
        //A radius for a sphere for ground checking
        [SerializeField] private float groundRadius = 0.4f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform spineTransform;//Child contains animation
        [SerializeField] private AudioHandler audioHandler;//The audio manager
        [SerializeField] private GroundChecker groundChecker;
        private Rigidbody2D rb;
        private PlayerInputHandler inputHandler;
        private float dashTimer;
        private bool isGrounded;
        private void Awake()
        {
            //We initialize the rigidbody and the inputs
            rb = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<PlayerInputHandler>();
        }
        private void Update()
        {
            Debug.Log("Grounded: " + groundChecker.IsGrounded);
            Debug.Log("Jump Pressed: " + inputHandler.IsJumpPressed());
            dashTimer -= Time.deltaTime;
        }
        private void FixedUpdate()
        {
            //Check if it's grounded
            //CheckGrounded();
            //We use fixed update to maintain movement speed as a constant
            Jump();
            Move();
            Dash();
        }
        //And finally, the methods
        private void Move()
        {
            float direction = inputHandler.GetMoveInputX();
            float speed = inputHandler.IsSprintHeld() ? moveSpeed * sprintMultiplier : moveSpeed;
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            FlipCharacter(direction);
        }
        private void Jump()
        {
            if (inputHandler.IsJumpPressed() && groundChecker.IsGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                audioHandler.PlayJumpSound();
            }
        }
        private void Dash()
        {
            if (inputHandler.IsDashPressed() && dashTimer <= 0f)
            {
                float direction = Mathf.Sign(inputHandler.GetMoveInputX());

                if (direction != 0f)
                {
                    rb.AddForce(new Vector2(direction * dashForce, 0f), ForceMode2D.Impulse);
                    dashTimer = dashCooldown;
                    audioHandler.PlayDashSound();
                }
            }
        }
        private void FlipCharacter(float direction)
        {
            if (direction < 0f)
            {
                spineTransform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (direction > 0f)
            {
                spineTransform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        /*private void CheckGrounded()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        }
        private void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
            }
        }*/
    }
}
