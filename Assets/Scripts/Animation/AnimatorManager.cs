using UnityEngine;

namespace TheSignal.Animation
{
    public class AnimatorManager : MonoBehaviour
    {
        private Animator animator;

        private int horizontal;
        private int vertical;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            horizontal = Animator.StringToHash("hInput");
            vertical = Animator.StringToHash("vInput");
        }

        public void PlayAnimation(string animationName, bool isInteracting)
        {
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(animationName, 0.25f);
        }

        public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, float moveAmount, bool isRunning)
        {
            // Animation snapping
            float snappedHorizontal = SnappedMovement(horizontalMovement);
            float snappedVertical = SnappedMovement(verticalMovement);

            animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
            animator.SetBool("Running", isRunning && moveAmount > 0.5f);
        }

        private float SnappedMovement(float movement)
        {
            if (movement > 0.0f && movement < 0.55f)
                return 0.5f;
            if (movement > 0.55f)
                return 1.0f;
            if (movement < 0.0f && movement > -0.55f)
                return -0.5f;
            if (movement < -0.55f)
                return -1.0f;

            return 0.0f;
        }

    }
}

