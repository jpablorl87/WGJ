using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float sprintMultiplier = 1.5f;
        [SerializeField] private float jumpForce = 8f;
        [SerializeField] private float dashForce = 12f;
        [SerializeField] private float dashCooldown = 1f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;
        private Rigidbody2D rb;
        private PlayerInputHandler inputHandler;
        private float dashTimer;
        private bool isGrounded;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            inputHandler = GetComponent<PlayerInputHandler>();
        }

        private void Update()
        {
            CheckGrounded();
            dashTimer -= Time.deltaTime;
        }
        private void FixedUpdate()
        {
            Move();
            Jump();
            Dash();
        }
        private void Move()
        {
            float direction = inputHandler.GetMoveInputX();
            float speed = inputHandler.IsSprintHeld() ? moveSpeed * sprintMultiplier : moveSpeed;
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
            FlipCharacter(direction);
        }

        private void Jump()
        {
            if (inputHandler.IsJumpPressed() && isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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
                }
            }
        }
        private void FlipCharacter(float direction)
        {
            if (direction < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (direction > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        private void CheckGrounded()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        }
    }
}
