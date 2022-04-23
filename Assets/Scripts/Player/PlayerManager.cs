
using System.Runtime.CompilerServices;
using TheSignal.Animation;
using TheSignal.Player.Combat;
using UnityEngine;

using TheSignal.Player.Input;
using UnityEditor;

namespace TheSignal.Player
{
    [RequireComponent(typeof(InputManager), typeof(PlayerLocomotion), typeof(Animator))]
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

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void Update()
        {
            inputManager.HandleAllInputs();
        }
        private void FixedUpdate()
        {
            playerAiming.HandleAiming();
            playerShooting.HandleShooting();
            playerLocomotion.HandleAllMovement();
        }

        private void LateUpdate()
        {
            isInteracting = animator.GetBool(AnimatorManager.Interacting);
            playerLocomotion.isJumping = animator.GetBool(AnimatorManager.Jumping);
            animator.SetBool(AnimatorManager.Grounded, playerLocomotion.isGrounded);
        }
    }
}

