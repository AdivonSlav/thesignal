using UnityEngine;
using TheSignal.Animation;

using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace TheSignal.Player
{
    public class PlayerLocomotion : MonoBehaviour
{
    private PlayerManager playerManager;
    private AnimatorManager animatorManager;
    private InputManager inputManager;

    private Vector3 moveDirection;
    private Transform cameraTransform;
    private Rigidbody playerRB;

    [Header("Falling")]
    public float inAirTimer;
    public float leapVelocity;
    public float fallVelocity;
    public LayerMask groundLayer;
    public float raycastHeightOffset;

    [HideInInspector] public bool isGrounded;

    [Header("Movement Speeds")]
    public float walkSpeed;
    public float runSpeed;
    public float sprintSpeed;
    public float rotationSpeed;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
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
        moveDirection = cameraTransform.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraTransform.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0.0f;

        if (inputManager.isRunning && inputManager.moveAmount > 0.5f)
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
        RaycastHit hit;
        Vector3 raycastStart = transform.position;
        raycastStart.y += raycastHeightOffset;

        // If the player is in the air, play the fall loop animation
        if (!isGrounded)
        {
            if (!playerManager.isInteracting)
                animatorManager.PlayAnimation("FallLoop", true);

            Debug.Log($"Interacted: {playerManager.isInteracting}");

            // Keeps track of how much time the player has spent in air
            // We add a small boost for the forward vector of the player to simulate leaping off a ledge
            // Then a downwards force that will increase the longer the player is in the air
            inAirTimer += Time.deltaTime;
            playerRB.AddForce(transform.forward * leapVelocity);
            playerRB.AddForce(Vector3.down * fallVelocity * inAirTimer);
        }


        // Here we check whether we're grounded or not
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

    private void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        start.y += raycastHeightOffset;
        Gizmos.DrawWireSphere(start, 0.2f);
    }
}
}

