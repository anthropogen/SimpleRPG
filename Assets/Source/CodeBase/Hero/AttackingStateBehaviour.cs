using SimpleRPG.Hero;
using UnityEngine;

public class AttackingStateBehaviour : StateMachineBehaviour
{
    private PlayerAnimator playerAnimator;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (playerAnimator == null)
            playerAnimator = animator.GetComponent<PlayerAnimator>();
        playerAnimator.IsAttacking = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        playerAnimator.IsAttacking = false;
    }
}

