
using TheSignal.Animation;
using TheSignal.Player.Combat;
using UnityEngine;

using TheSignal.Player.Input;

namespace TheSignal.Player
{
    [RequireComponent(typeof(InputManager), typeof(PlayerLocomotion))]
    public class PlayerManager : MonoBehaviour
    {
        private InputManager inputManager;
        private PlayerLocomotion playerLocomotion;
        private Animator animator;
        private PlayerAiming playerAiming;
        private PlayerShooting playerShooting;


        [HideInInspector] public bool isInteracting;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animator = GetComponent<Animator>();
            playerAiming = GetComponent<PlayerAiming>();
            playerShooting = GetComponent<PlayerShooting>();
        }
        private void Update()
        {
            inputManager.HandleAllInputs();
        }
        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovement();
            playerAiming.HandleAiming();
        }

        private void LateUpdate()
        {
            playerShooting.HandleShooting();
            isInteracting = animator.GetBool(AnimatorManager.Interacting);
            playerLocomotion.isJumping = animator.GetBool(AnimatorManager.Jumping);
            animator.SetBool(AnimatorManager.Grounded, playerLocomotion.isGrounded);
        }
    }
}

