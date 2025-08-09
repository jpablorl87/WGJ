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
        [SerializeField] private float dashForce = 20f;
        [SerializeField] private float dashCooldown = 1f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform spineTransform;//Child contains animation
        [SerializeField] private AudioHandler audioHandler;//The audio manager
        [SerializeField] private GroundChecker groundChecker;
        [SerializeField] private float dashDuration = 0.2f;
        private bool isDashing = false;
        private float dashTimeRemaining;
        private float dashDirection;
        private Rigidbody2D rb;
        private PlayerInputHandler inputHandler;
        private float dashTimer;
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
            //We use fixed update to maintain movement speed as a constant
            Dash();
            Jump();
            Move();
        }
        //And finally, the methods
        private void Move()
        {
            if (isDashing) return;
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
            if (inputHandler.IsDashPressed() && dashTimer <= 0f && !isDashing)
            {
                dashDirection = Mathf.Sign(inputHandler.GetMoveInputX());
                if (dashDirection == 0f)
                    dashDirection = transform.localScale.x > 0 ? 1f : -1f;//fallback

                isDashing = true;
                dashTimeRemaining = dashDuration;
                dashTimer = dashCooldown;
                audioHandler.PlayDashSound();
            }
            if (isDashing)
            {
                rb.linearVelocity = new Vector2(dashDirection * dashForce, 0f);
                dashTimeRemaining -= Time.fixedDeltaTime;

                if (dashTimeRemaining <= 0f)
                {
                    isDashing = false;
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
    }
}
