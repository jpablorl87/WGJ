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
        private float moveInputX;
        private bool jumpPressed;
        private bool dashPressed;
        private bool sprintHeld;
        private void Awake()
        {
            var actions = playerInput.actions;
            moveAction = actions.FindAction("Move");
            jumpAction = actions.FindAction("Jump");
            dashAction = actions.FindAction("Dash");
            sprintAction = actions.FindAction("Sprint");
        }
        private void Update()
        {
            moveInputX = moveAction.ReadValue<float>();
            jumpPressed = jumpAction.triggered;
            dashPressed = dashAction.triggered;
            sprintHeld = sprintAction.IsPressed();
        }
        public float GetMoveInputX()
        {
            return moveInputX;
        }
        public bool IsJumpPressed()
        {
            return jumpPressed;
        }
        public bool IsDashPressed()
        {
            return dashPressed;
        }
        public bool IsSprintHeld()
        {
            return sprintHeld;
        }
    }
}
