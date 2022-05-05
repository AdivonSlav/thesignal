using UnityEngine;

namespace TheSignal.Animation.Reset
{
    public class ResetBool : StateMachineBehaviour
    {
        public string isInteractingBool;
        public bool isInteractingStatus;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(isInteractingBool, isInteractingStatus);
        }
    } 
}

