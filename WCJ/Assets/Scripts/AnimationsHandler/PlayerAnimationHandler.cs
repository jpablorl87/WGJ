using UnityEngine;
using Spine.Unity;
namespace Player
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private Rigidbody2D rb;
        private void Update()
        {
            float xInput = inputHandler.GetMoveInputX();
            float yVelocity = rb.linearVelocity.y;
            if (yVelocity > 0.1f)
            {
                SetAnimation("jump", false);
            }
            else if (yVelocity < -0.1f)
            {
                SetAnimation("fall", false);
            }
            else if (Mathf.Abs(xInput) > 0.1f)
            {
                if (inputHandler.IsSprintHeld())
                {
                    SetAnimation("run", true);
                }
                else
                {
                    SetAnimation("walk", true);
                }
            }
            else
            {
                SetAnimation("idle", true);
            }
        }
        private void SetAnimation(string animationName, bool loop)
        {
            if (skeletonAnimation.AnimationName != animationName)
            {
                skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
            }
        }
    }
}
