using UnityEngine;

namespace TheSignal.Animation
{
    public class ResetJumping : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorManager.isJumping, false);
        }
    }
}