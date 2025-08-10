using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction dashAction;
        private InputAction sprintAction;
        private InputAction pauseAction;
        private float moveInputX;
        private bool sprintHeld;
        private bool pausePressed;
        // Buffer
        private float jumpBufferTime = 0.1f;
        private float dashBufferTime = 0.1f;
        private float jumpTimer;
        private float dashTimer;
        private void Awake()
        {
            var actions = playerInput.actions;
            moveAction = actions.FindAction("Move");
            jumpAction = actions.FindAction("Jump");
            dashAction = actions.FindAction("Dash");
            sprintAction = actions.FindAction("Sprint");
            pauseAction = actions.FindAction("Pause");
        }

        private void Update()
        {
            moveInputX = moveAction.ReadValue<float>();
            sprintHeld = sprintAction.IsPressed();
            pausePressed = pauseAction.triggered;
            if (jumpAction.triggered) jumpTimer = jumpBufferTime;
            if (dashAction.triggered) dashTimer = dashBufferTime;
            jumpTimer -= Time.deltaTime;
            dashTimer -= Time.deltaTime;
        }
        public float GetMoveInputX()
        {
            return moveInputX;
        }
        public bool IsSprintHeld()
        {
            return sprintHeld;
        }
        public bool IsPausePressed()
        {
            return pausePressed;
        }
        public bool ConsumeJumpPressed()
        {
            if (jumpTimer > 0f)
            {
                jumpTimer = 0f;
                return true;
            }
            return false;
        }
        public bool ConsumeDashPressed()
        {
            if (dashTimer > 0f)
            {
                dashTimer = 0f;
                return true;
            }
            return false;
        }
    }
}
