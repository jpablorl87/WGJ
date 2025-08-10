using UnityEngine;
using Spine.Unity;
namespace Player
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        //Skeleton animation for spine
        [SerializeField] private SkeletonAnimation skeletonAnimation;
        //Inputs
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private Rigidbody2D rb;
        private void Update()
        {
            float xInput = inputHandler.GetMoveInputX();
            float yVelocity = rb.linearVelocity.y;
            if (inputHandler.ConsumeDashPressed())
            {
                SetAnimation("dash", false);
                return;
            }
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
                    SetAnimation("sprint", true);
                }
                else
                {
                    SetAnimation("run", true);
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
