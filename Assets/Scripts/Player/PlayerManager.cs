using UnityEngine;

namespace TheSignal.Player
{
    [RequireComponent(typeof(InputManager), typeof(PlayerLocomotion))]
    public class PlayerManager : MonoBehaviour
    {
        private InputManager inputManager;
        private PlayerLocomotion playerLocomotion;
        private Animator animator;

        [HideInInspector] public bool isInteracting;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            inputManager.HandleAllInputs();
        }

        private void FixedUpdate()
        {
            playerLocomotion.HandleAllMovement();
        }

        private void LateUpdate()
        {
            isInteracting = animator.GetBool("isInteracting");
        }
    }
}

