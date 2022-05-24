using TheSignal.Animation;
using TheSignal.Managers;
using TheSignal.Player.Combat;
using UnityEngine;
using TheSignal.Player.Input;
using TheSignal.Player.Movement;

namespace TheSignal.Player
{
    [RequireComponent(typeof(InputManager), typeof(PlayerLocomotion), typeof(Animator))]
    public class PlayerManager : TrackedEntity
    {
        private InputManager inputManager;
        private PlayerLocomotion playerLocomotion;
        private Animator animator;
        private PlayerAiming playerAiming;
        private PlayerShooting playerShooting;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animator = GetComponent<Animator>();
            playerAiming = GetComponent<PlayerAiming>();
            playerShooting = GetComponent<PlayerShooting>();
        }

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void Update()
        {
            if (!this.isRunning)
                return;
            
            inputManager.HandleAllInputs();
        }
        private void FixedUpdate()
        {
            if (!this.isRunning)
                return;
            
            playerAiming.HandleAiming();
            playerShooting.HandleShooting();
            playerLocomotion.HandleAllMovement();
        }

        private void LateUpdate()
        {
            
            if (!this.isRunning)
                return;
            
            playerLocomotion.isJumping = animator.GetBool(AnimatorManager.Jumping);
            animator.SetBool(AnimatorManager.Grounded, playerLocomotion.isGrounded);
        }
    }
}

