using UnityEngine;

public class PlayerAnimation
{
    
    private Animator animator;

    public PlayerAnimation(Animator animator)
    {
        this.animator = animator;
    }
    private void Update() {
        
    }

    public void UpdateAnimations(bool isUnderwater, bool isWalking)
    {
        if ( isUnderwater)
        {
            if (isWalking)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isJumping", false);
                animator.SetBool("isSwimming", false);
            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isSwimming", false);
            }
        }
        else
        {
            if (isWalking)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isSwimming", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isSwimming", false);
            }
        }
    }

    public void TriggerJump()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isJumping", true);
        animator.SetBool("isSwimming", false);
    }
}