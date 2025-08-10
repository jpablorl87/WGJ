using Player;
using Spine.Unity;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioHandler audioHandler;
    [SerializeField] private Transform spineTransform;

    private string currentAnimation;
    private bool dashQueued;

    private void Start()
    {
        audioHandler.PlayBackground();
    }

    private void Update()
    {
        float xInput = inputHandler.GetMoveInputX();
        float yVelocity = rb.linearVelocity.y;

        FlipCharacter(xInput);

        if (inputHandler.ConsumeDashPressed())
        {
            dashQueued = true;
        }

        if (dashQueued)
        {
            SetAnimation("DASH-ANSI", false);
            dashQueued = false;
            return;
        }

        if (yVelocity > 0.1f)
        {
            SetAnimation("JUMP-ANSI", false);
        }
        else if (yVelocity < -0.1f)
        {
            SetAnimation("FALL-ANSI", false);
        }
        else if (Mathf.Abs(xInput) > 0.1f)
        {
            SetAnimation("RUN-ANSI", true);
        }
        else
        {
            SetAnimation("IDLE-ANSI", true);
        }
    }

    private void SetAnimation(string animationName, bool loop)
    {
        if (currentAnimation == animationName) return;
        skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
        currentAnimation = animationName;

        switch (animationName)
        {
            case "JUMP-ANSI":
                audioHandler.PlayJumpSound();
                break;
            case "DASH-ANSI":
                audioHandler.PlayDashSound();
                break;
            case "HIT-ANSI":
                audioHandler.PlayHitSound();
                break;
        }
    }

    public void ForcePlayAnimation(string animationName, bool loop)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, animationName, loop);
        currentAnimation = animationName;

        switch (animationName)
        {
            case "HIT-ANSI":
                audioHandler.PlayHitSound();
                break;
            case "JUMP-ANSI":
                audioHandler.PlayJumpSound();
                break;
            case "DASH-ANSI":
                audioHandler.PlayDashSound();
                break;
        }
    }

    private void FlipCharacter(float direction)
    {
        if (direction < 0f)
            spineTransform.localScale = new Vector3(-1f, 1f, 1f);
        else if (direction > 0f)
            spineTransform.localScale = new Vector3(1f, 1f, 1f);
    }
}