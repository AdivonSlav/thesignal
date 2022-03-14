using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class MovementStateManager : MonoBehaviour
{
    #region MOVEMENT
    public float currentMoveSpeed;
    public float walkSpeed, walkBackSpeed;
    public float runSpeed, runBackSpeed;
    public float crouchSpeed, crouchBackSpeed;
    [HideInInspector] public Vector3 direction;
    private CharacterController controller;
    [HideInInspector] public float hInput;
    [HideInInspector] public float vInput;
    
    private MovementBaseState currentState;
    
    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator animator;

    private Transform cameraTransform;

    private PlayerInput playerInput;

    [HideInInspector] public InputAction moveAction;
    [HideInInspector] public InputAction jumpAction;
    [HideInInspector] public InputAction crouchAction;
    [HideInInspector] public InputAction sprintAction;

    #endregion

    #region GROUNDCHECK

    [SerializeField]private float groundYOffset = 0.0f;
    [SerializeField] private LayerMask groundMask;
    private Vector3 spherePosition;
    private bool isGrounded = false;

    #endregion

    #region GRAVITY

    [SerializeField] private float gravAcceleration = -9.81f; // Default Earth gravity
    private Vector3 velocity;

    #endregion

    #region JUMPING

    public float jumpForce = 8.0f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
        cameraTransform = Camera.main.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        crouchAction = playerInput.actions["Crouch"];
        sprintAction = playerInput.actions["Sprint"];

        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Gravity();

        animator.SetFloat("hInput", hInput);
        animator.SetFloat("vInput", vInput);
        
        if (isGrounded && jumpAction.triggered)
            velocity.y += jumpForce;
        
        currentState.UpdateState(this);
    }

    #region MOVEMENT_METHODS

    private void GetDirection()
    {
        // We get the direction of movement from the pressed keys and also take into account
        // the direction vector of the camera so the character will move to where the camera looks
        Vector2 input = moveAction.ReadValue<Vector2>();

        hInput = input.x;
        vInput = input.y;

        direction = new Vector3(input.x, 0.0f, input.y).normalized;
        direction = direction.x * cameraTransform.right.normalized + direction.z * cameraTransform.forward.normalized;
        direction.y = 0.0f;
    }

    private void Move()
    {
        GetDirection();
        controller.Move(direction * currentMoveSpeed * Time.deltaTime);

        // If player starts moving, rotate towards the direction vector
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = cameraTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 4.0f * Time.deltaTime);
        }
        
    }
    
    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    #endregion

    #region GRAVITY_METHODS

    private bool IsGrounded()
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);

        // In case any colliders overlap the sphere at the center of the player, we know it is the ground
        // Radius minus 0.05f to prevent the sphere from leaving the player character and confusing i.e. a wall with the ground
        if (Physics.CheckSphere(spherePosition, controller.radius - 0.05f, groundMask))
        {
            isGrounded = true;
            return true;    
        }

        isGrounded = false;
        return false;
    }

    private void Gravity()
    {
        if (!IsGrounded())
            velocity.y += gravAcceleration * Time.deltaTime;
        else if (velocity.y < 0)
            velocity.y = 0.0f;

        controller.Move(velocity * Time.deltaTime);
    }

    #endregion

    
    // Simply so we are able to see the sphere collider with the ground
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition, controller.radius-0.05f);
    }
}
