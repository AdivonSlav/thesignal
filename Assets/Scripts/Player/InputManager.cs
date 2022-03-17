using UnityEngine;
using TheSignal.Animation;

namespace TheSignal.Player
{
    [RequireComponent(typeof(AnimatorManager))]
    public class InputManager : MonoBehaviour
    {
        private PlayerControls playerControls;
        private AnimatorManager animatorManager;
    
        [HideInInspector] public Vector2 movementInput;
        [HideInInspector] public float moveAmount;
        [HideInInspector] public float verticalInput;
        [HideInInspector] public float horizontalInput;
        [HideInInspector] public bool isRunning;
        [HideInInspector] public bool isSprinting;
    
        private void Awake()
        {
            animatorManager = GetComponent<AnimatorManager>();
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
        }
    
        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateAnimatorValues(0.0f, moveAmount, moveAmount, isRunning);
        }
    }
}

