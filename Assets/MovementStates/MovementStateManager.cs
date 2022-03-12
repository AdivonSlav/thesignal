using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementStateManager : MonoBehaviour
{
    #region MOVEMENT
    public float currentMoveSpeed;
    public float walkSpeed, walkBackSpeed;
    public float runSpeed, runBackSpeed;
    public float crouchSpeed, crouchBackSpeed;
    [HideInInspector] public Vector3 direction;
    private CharacterController controller;
    [HideInInspector]public float hInput = 0.0f;
    [HideInInspector]public float vInput = 0.0f;
    
    private MovementBaseState currentState;
    
    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();

    [HideInInspector] public Animator animator;

    #endregion

    #region GROUNDCHECK

    [SerializeField]private float groundYOffset = 0.0f;
    [SerializeField] private LayerMask groundMask;
    private Vector3 spherePosition;

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
        animator = GetComponentInChildren<Animator>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();
        Move();
        Gravity();

        animator.SetFloat("hInput", hInput);
        animator.SetFloat("vInput", vInput);
        
        if (OnGround() && Input.GetKeyDown(KeyCode.Space))
            velocity.y += jumpForce;
        
        currentState.UpdateState(this);
    }

    #region MOVEMENT_METHODS

    private void GetDirection()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        var objTransform = transform;
        direction = objTransform.forward * vInput + objTransform.right * hInput;
    }

    private void Move()
    {
        // Normalizing the direction vector to prevent movement being faster in diagonal directions
        controller.Move(direction.normalized * currentMoveSpeed * Time.deltaTime);
    }

    #endregion

    #region GRAVITY_METHODS

    private bool OnGround()
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);

        // In case any colliders overlap the sphere at the center of the player, we know it is the ground
        // Radius minus 0.05f to prevent the sphere from leaving the player character and confusing i.e. a wall with the ground
        if (Physics.CheckSphere(spherePosition, controller.radius - 0.05f, groundMask))
            return true;    
        return false;
    }

    private void Gravity()
    {
        if (!OnGround())
            velocity.y += gravAcceleration * Time.deltaTime;
        else if (velocity.y < 0)
            velocity.y = -2.0f;

        controller.Move(velocity * Time.deltaTime);
    }

    #endregion

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    
    // Simply so we are able to see the sphere collider with the ground
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition, controller.radius-0.05f);
    }
}
