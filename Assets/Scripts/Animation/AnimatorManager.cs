using System;
using TheSignal.Camera;
using UnityEngine;

namespace TheSignal.Animation
{
    public class AnimatorManager : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
        private AnimatorStateInfo stateInfo;
        private CinemachineController cinemachineController;

        // Animator parameter IDs for more effective fetching
        private static readonly int horizontalInput = Animator.StringToHash("hInput");
        private static readonly int verticalInput = Animator.StringToHash("vInput");
        private static readonly int strafingHorizontal = Animator.StringToHash("hStrafing");
        private static readonly int strafingVertical = Animator.StringToHash("vStrafing");
        private static readonly int Running = Animator.StringToHash("Running");
        public static readonly int Interacting = Animator.StringToHash("isInteracting");
        public static readonly int Jumping = Animator.StringToHash("isJumping");
        public static readonly int Grounded = Animator.StringToHash("isGrounded");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            cinemachineController = UnityEngine.Camera.main.GetComponent<CinemachineController>();
        }

        private void Update()
        {
            if (!cinemachineController.Paused())
                return;
            
            animator.SetFloat(horizontalInput, 0.0f, 0.1f, Time.deltaTime);
            animator.SetFloat(verticalInput, 0.0f, 0.1f, Time.deltaTime);
        }

        public void PlayAnimation(string animationName, bool isInteracting)
        {
            animator.SetBool(Interacting, isInteracting);
            animator.CrossFade(animationName, 0.25f);
        }

        public void UpdateMovementValues(float horizontalMovement, float verticalMovement, float moveAmount, bool isRunning)
        {
            // Animation snapping
            float snappedHorizontal = SnappedMovement(horizontalMovement);
            float snappedVertical = SnappedMovement(verticalMovement);

            animator.SetFloat(horizontalInput, snappedHorizontal, 0.1f, Time.deltaTime);
            animator.SetFloat(verticalInput, snappedVertical, 0.1f, Time.deltaTime);
            animator.SetBool(Running, isRunning && moveAmount > 0.5f);
        }

        public void UpdateAimingValues(bool isAiming, float horizontal, float vertical)
        {
            float targetWeight = 0.0f;

            if (isAiming)
            {
                targetWeight = 1.0f;
                float strafingHorizontalSnapped = SnappedMovement(horizontal);
                float strafingVerticalSnapped = SnappedMovement(vertical);
                
                animator.SetFloat(strafingHorizontal, strafingHorizontalSnapped, 0.1f, Time.deltaTime);
                animator.SetFloat(strafingVertical, strafingVerticalSnapped, 0.1f, Time.deltaTime);
            }
            
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), targetWeight, Time.deltaTime * 10.0f));
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

        private void OnAnimatorMove()
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(2);
            
            // if (!stateInfo.IsTag("FallingJumping"))
            //     // animator.ApplyBuiltinRootMotion();
        }
    }
}

