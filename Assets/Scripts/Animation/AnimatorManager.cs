using UnityEngine;

namespace TheSignal.Animation
{
    public class AnimatorManager : MonoBehaviour
    {
        [HideInInspector] public Animator animator;

        private int horizontal;
        private int vertical;

        // Animator parameter IDs for more efective fetching
        public static readonly int Running = Animator.StringToHash("Running");
        public static readonly int isInteracting = Animator.StringToHash("isInteracting");
        public static readonly int isJumping = Animator.StringToHash("isJumping");
        public static readonly int isGrounded = Animator.StringToHash("isGrounded");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            horizontal = Animator.StringToHash("hInput");
            vertical = Animator.StringToHash("vInput");
        }

        public void PlayAnimation(string animationName, bool isInteracting)
        {
            animator.SetBool(AnimatorManager.isInteracting, isInteracting);
            animator.CrossFade(animationName, 0.25f);
        }

        public void UpdateMovementValues(float horizontalMovement, float verticalMovement, float moveAmount, bool isRunning)
        {
            // Animation snapping
            float snappedHorizontal = SnappedMovement(horizontalMovement);
            float snappedVertical = SnappedMovement(verticalMovement);

            animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
            animator.SetBool(Running, isRunning && moveAmount > 0.5f);
        }

        public void UpdateAimingValues(bool isAiming)
        {
            if (isAiming)
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1.0f, Time.deltaTime * 10.0f));
            else
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0.0f, Time.deltaTime * 10.0f));
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

