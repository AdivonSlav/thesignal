using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

using TheSignal.Animation;
using TheSignal.Player.Combat;
using TheSignal.Player.Input;
using UnityEngine.Animations.Rigging;

namespace TheSignal.Player
{
    public class PlayerLocomotion : MonoBehaviour
    {
        private PlayerManager playerManager;
        private AnimatorManager animatorManager;
        private InputManager inputManager;
        private PlayerAiming playerAiming;

        private Vector3 moveDirection;
        private Transform cameraTransform;
        private Rigidbody playerRB;

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

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
            animatorManager = GetComponent<AnimatorManager>();
            inputManager = GetComponent<InputManager>();
            playerAiming = GetComponent<PlayerAiming>();
            playerRB = GetComponent<Rigidbody>();
            cameraTransform = Camera.main.transform;
        }

        public void HandleAllMovement()
        {
            HandleFalling();

            if (playerManager.isInteracting)
                return;

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
            else if (inputManager.isSprinting)
                moveDirection *= sprintSpeed;
            else
                moveDirection *= walkSpeed;

            Vector3 movementVelocity = moveDirection;
            playerRB.velocity = movementVelocity;
        }

        private void HandleRotation()
        {
            if (isJumping || inputManager.isAiming)
                return;

            Vector3 targetDirection = Vector3.zero;

            targetDirection = cameraTransform.forward * inputManager.verticalInput;
            targetDirection = targetDirection + cameraTransform.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0.0f;

            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }

        private void HandleFalling()
        {
            RaycastHit hit =  new RaycastHit();

            // If the player is in the air, play the fall loop animation
            if (!isGrounded && !isJumping)
            {
                if (!playerManager.isInteracting)
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
                animatorManager.animator.SetBool(AnimatorManager.Jumping, true);
                animatorManager.PlayAnimation("JumpUp", false);

                float jumpVelocity = Mathf.Sqrt(-2.0f * gravityAccel * jumpHeight);
                Vector3 playerVelocity = moveDirection;
                playerVelocity.y = jumpVelocity;
                playerRB.velocity = playerVelocity;
            }
        }

        private void CheckGrounded(ref RaycastHit hit)
        {
            Vector3 raycastStart = transform.position;
            raycastStart.y += raycastHeightOffset;
            
            if (Physics.SphereCast(raycastStart, 0.2f, Vector3.down, out hit, 1.0f, groundLayer))
            {
                if (!isGrounded && playerManager.isInteracting)
                    animatorManager.PlayAnimation("Land", true);

                inAirTimer = 0;
                isGrounded = true;
            }
            else
                isGrounded = false;
        }
        
    }
}

