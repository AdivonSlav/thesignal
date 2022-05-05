using UnityEngine;
using TheSignal.Animation;
using TheSignal.Player.Input;

namespace TheSignal.Player.Movement
{
    public class PlayerLocomotion : MonoBehaviour
    {
        [Header("Falling")]
        public float leapVelocity;
        [HideInInspector] public float inAirTimer;
        public float fallVelocity;
        public LayerMask groundLayer;
        public float raycastHeightOffset;

        [HideInInspector] public bool isGrounded;

        [Header("Movement Speeds")]
        public float walkSpeed;
        public float runSpeed;
        public float sprintSpeed;
        public float rotationSpeed;

        [HideInInspector] public bool isJumping;

        [Header("Jump Speeds")]
        public float jumpHeight;
        public float gravityAccel;
        
        private AnimatorManager animatorManager;
        private InputManager inputManager;

        private Vector3 moveDirection;
        private Transform cameraTransform;
        private Rigidbody playerRB;

        private bool isInteracting;

        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
            inputManager = GetComponent<InputManager>();
            playerRB = GetComponent<Rigidbody>();
            cameraTransform = Camera.main.transform;
        }

        public void HandleAllMovement()
        {
            HandleFalling();

            if (isInteracting)
                return;

            if (inputManager.isJumping)
            {
                inputManager.isJumping = false;
                HandleJump();
            }

            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            if (isJumping)
                return;

            moveDirection = cameraTransform.forward * inputManager.verticalInput;
            moveDirection += cameraTransform.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0.0f;

            if (inputManager.isRunning && inputManager.moveAmount > 0.5f && !inputManager.isAiming)
                moveDirection *= runSpeed;
            else
                moveDirection *= walkSpeed;

            Vector3 movementVelocity = moveDirection;
            playerRB.velocity = movementVelocity;
        }

        private void HandleRotation()
        {
            if (isJumping || inputManager.isAiming)
                return;

            Vector3 targetDirection = cameraTransform.forward * inputManager.verticalInput;
            targetDirection += cameraTransform.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0.0f;

            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            playerRB.MoveRotation(targetRotation);
        }

        private void HandleFalling()
        {
            RaycastHit hit =  new RaycastHit();

            // If the player is in the air, play the fall loop animation
            if (!isGrounded && !isJumping)
            {
                if (!isInteracting)
                    animatorManager.PlayAnimation("FallLoop", true);

                // Keeps track of how much time the player has spent in air
                // We add a small boost for the forward vector of the player to simulate leaping off a ledge
                // Then a downwards force that will increase the longer the player is in the air
                inAirTimer += Time.deltaTime;
                playerRB.AddForce(transform.forward * leapVelocity);
                playerRB.AddForce(Vector3.down * fallVelocity * inAirTimer);
            }

            CheckGrounded(ref hit);
        }
        
        public void HandleJump()
        {
            if (isGrounded)
            {
                animatorManager.animator.applyRootMotion = false;
                animatorManager.animator.SetBool(AnimatorManager.Jumping, true);
                animatorManager.PlayAnimation("JumpUp", false);

                var jumpVelocity = Mathf.Sqrt(-2.0f * gravityAccel * jumpHeight);
                var newVelocity = moveDirection;
                newVelocity.y = jumpVelocity;
                playerRB.AddForce(newVelocity, ForceMode.Impulse);
            }
        }

        private void CheckGrounded(ref RaycastHit hit)
        {
            Vector3 raycastStart = transform.position;
            raycastStart.y += raycastHeightOffset;
            
            if (Physics.SphereCast(raycastStart, 0.2f, Vector3.down, out hit, 1.0f, groundLayer))
            {
                
                if (!isGrounded && isInteracting)
                {
                    animatorManager.PlayAnimation("Land", true);
                    animatorManager.animator.applyRootMotion = true;
                }

                inAirTimer = 0;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        private void LateUpdate()
        {
            isInteracting = animatorManager.animator.GetBool(AnimatorManager.Interacting);
        }
    }
}

