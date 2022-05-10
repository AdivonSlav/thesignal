using UnityEngine;
using TheSignal.Animation;
using TheSignal.Camera;

namespace TheSignal.Player.Input
{
    [RequireComponent(typeof(AnimatorManager))]
    public class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;
        private AnimatorManager animatorManager;
        private CinemachineController cinemachineController;

        [HideInInspector] public Vector2 movementInput;
        [HideInInspector] public float moveAmount;
        [HideInInspector] public float verticalInput;
        [HideInInspector] public float horizontalInput;
        [HideInInspector] public float strafingHorizontal;
        [HideInInspector] public float strafingVertical;
        [HideInInspector] public bool isRunning;
        [HideInInspector] public bool isJumping;
        [HideInInspector] public bool isAiming;
        [HideInInspector] public bool isFiring;
        [HideInInspector] public bool isInteracting;
        [HideInInspector] public bool isPressingK;

        [HideInInspector] public bool isSelecting;
        [HideInInspector] public bool isExiting;
        [HideInInspector] public bool isPressingTab;
        [HideInInspector] public bool isOpeningJournal;

        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
            cinemachineController = UnityEngine.Camera.main.GetComponent<CinemachineController>();
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
                playerControls.Player.Jump.performed += i => isJumping = i.ReadValueAsButton();
                playerControls.Player.Aim.performed += i => isAiming = i.ReadValueAsButton();
                playerControls.Player.Aim.canceled += i => isAiming = i.ReadValueAsButton();
                playerControls.Player.Fire1.performed += i => isFiring = i.ReadValueAsButton();
                playerControls.Player.Fire1.canceled += i => isFiring = i.ReadValueAsButton();
                playerControls.Player.Interact.performed += i => isInteracting = i.ReadValueAsButton();
                playerControls.Player.SlowMo.performed += i => isPressingK = i.ReadValueAsButton();
                playerControls.Player.SlowMo.canceled += i => isPressingK = i.ReadValueAsButton();

                playerControls.UI.Select.performed += i => isSelecting = i.ReadValueAsButton();
                playerControls.UI.Select.canceled += i => isSelecting = i.ReadValueAsButton();
                playerControls.UI.Exit.performed += i => isExiting = i.ReadValueAsButton();
                playerControls.UI.Exit.canceled += i => isExiting = i.ReadValueAsButton();
                playerControls.UI.Objectives.performed += i => isPressingTab = i.ReadValueAsButton();
                playerControls.UI.Objectives.canceled += i => isPressingTab = i.ReadValueAsButton();
                playerControls.UI.Journal.performed += i => isOpeningJournal = i.ReadValueAsButton();
                playerControls.UI.Journal.canceled += i => isOpeningJournal = i.ReadValueAsButton();
            }
    
            playerControls.Enable();
        }
    
        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            bool ignoreInput = cinemachineController.Paused();

            if (ignoreInput && playerControls.Player.enabled)
            {
                movementInput = Vector2.zero;
                playerControls.Player.Disable();
            }
            else if (!ignoreInput && !playerControls.Player.enabled)
                playerControls.Player.Enable();
        }

        public void HandleAllInputs()
        {
            HandleMovementInput();
            HandleAimingInput();
        }
    
        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateMovementValues(moveAmount, moveAmount, moveAmount, isRunning);
        }

        private void HandleAimingInput()
        {
            strafingHorizontal = movementInput.x;
            strafingVertical = movementInput.y;
            animatorManager.UpdateAimingValues(isAiming, strafingHorizontal, strafingVertical);
        }
    }
}

