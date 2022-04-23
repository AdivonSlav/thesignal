using UnityEngine;
using TheSignal.Animation;

namespace TheSignal.Player.Input
{
    [RequireComponent(typeof(AnimatorManager))]
    public class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;
        private AnimatorManager animatorManager;
        private PlayerLocomotion playerLocomotion;
    
        [HideInInspector] public Vector2 movementInput;
        [HideInInspector] public float moveAmount;
        [HideInInspector] public float verticalInput;
        [HideInInspector] public float horizontalInput;
        [HideInInspector] public float strafingHorizontal;
        [HideInInspector] public float strafingVertical;
        [HideInInspector] public bool isRunning;
        [HideInInspector] public bool isSprinting;
        [HideInInspector] public bool isJumping;
        [HideInInspector] public bool isAiming;
        [HideInInspector] public bool isFiring;
        [HideInInspector] public bool isInteracting;
        [HideInInspector] public bool isPressingESC;
        [HideInInspector] public bool isPressingK;
        [HideInInspector] public bool isPressingI;

        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }
    
        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();
    
                // Simply storing the result of each key event
                playerControls.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.Player.Run.performed += i => isRunning = i.ReadValueAsButton();
                playerControls.Player.Run.canceled += i => isRunning = i.ReadValueAsButton();
                playerControls.Player.Sprint.performed += i => isSprinting = i.ReadValueAsButton();
                playerControls.Player.Sprint.canceled += i => isSprinting = i.ReadValueAsButton();
                playerControls.Player.Jump.performed += i => isJumping = i.ReadValueAsButton();
                playerControls.Player.Aim.performed += i => isAiming = i.ReadValueAsButton();
                playerControls.Player.Aim.canceled += i => isAiming = i.ReadValueAsButton();
                playerControls.Player.Fire1.performed += i => isFiring = i.ReadValueAsButton();
                playerControls.Player.Fire1.canceled += i => isFiring = i.ReadValueAsButton();
                playerControls.Player.Escape.performed += i => isPressingESC = i.ReadValueAsButton();
                playerControls.Player.Interact.performed += i => isInteracting = i.ReadValueAsButton();
                playerControls.Player.SlowMo.performed += i => isPressingK = i.ReadValueAsButton();
                playerControls.Player.SlowMo.canceled += i => isPressingK = i.ReadValueAsButton();
                playerControls.Player.ObjectiveTab.performed += i => isPressingI = i.ReadValueAsButton();
            }
    
            playerControls.Enable();
        }
    
        private void OnDisable()
        {
            playerControls.Disable();
        }
    
        public void HandleAllInputs()
        {
            HandleMovementInput();
            HandleJumpingInput();
            HandleAimingInput();
        }
    
        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateMovementValues(moveAmount, moveAmount, moveAmount, isRunning);
        }

        private void HandleJumpingInput()
        {
            if (isJumping)
            {
               isJumping = false;
               playerLocomotion.HandleJump();
            }
        }

        private void HandleAimingInput()
        {
            strafingHorizontal = movementInput.x;
            strafingVertical = movementInput.y;
            animatorManager.UpdateAimingValues(isAiming, strafingHorizontal, strafingVertical);
        }
    }
}

