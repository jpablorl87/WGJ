using UnityEngine;
using UnityEngine.InputSystem;
namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        //First, we call the player input of the player
        [SerializeField] private PlayerInput playerInput;
        //Now, we create an input action for every action of our player
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction dashAction;
        private InputAction sprintAction;
        private InputAction pauseAction;
        private float moveInputX;
        //Next, we create a bool for the actions that need it
        private bool jumpPressed;
        private bool dashPressed;
        private bool sprintHeld;
        private bool pausePressed;
        private void Awake()
        {
            //We initialize the actions of the action map
            var actions = playerInput.actions;
            moveAction = actions.FindAction("Move");
            jumpAction = actions.FindAction("Jump");
            dashAction = actions.FindAction("Dash");
            sprintAction = actions.FindAction("Sprint");
            pauseAction = actions.FindAction("Pause");
        }
        private void Update()
        {
            //We check if any of the buttons are pressed
            moveInputX = moveAction.ReadValue<float>();
            jumpPressed = jumpAction.triggered;
            dashPressed = dashAction.triggered;
            sprintHeld = sprintAction.IsPressed();
            pausePressed = pauseAction.triggered;
        }
        //Finally, every action has it's own method
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
        public bool IsPausePressed()
        {
            return pausePressed;
        }
    }
}
