using UnityEngine;

namespace TheSignal.Animation.Reset
{
    public class ResetJumping : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(Animator.StringToHash("isJumping"), false);
        }
    }
}