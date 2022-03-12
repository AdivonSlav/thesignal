using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementStateManager : MonoBehaviour
{
    // Variables for movement
    public float movementSpeed = 3.0f;
    [HideInInspector] public Vector3 direction;
    private CharacterController controller;
    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;

    // Variables for ground check
    [SerializeField]private float groundYOffset = 0.0f;
    [SerializeField] private LayerMask groundMask;
    private Vector3 spherePosition = new Vector3(0.0f, 1.0f - 0.05f, 0.0f);

    // Variables for gravity
    [SerializeField] private float gravAcceleration = -9.81f; // Default Earth gravity
    private Vector3 velocity;

    // Variables for jumping
    public float jumpForce = 8.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();
        Move();
        Gravity();

        if (OnGround() && Input.GetKeyDown(KeyCode.Space))
            velocity.y += jumpForce;
    }

    private void GetDirection()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        var objTransform = transform;
        direction = objTransform.forward * verticalInput + objTransform.right * horizontalInput;
    }

    private void Move()
    {
        // Normalizing the direction vector to prevent movement being faster in diagonal directions
        controller.Move(direction.normalized * movementSpeed * Time.deltaTime);
    }

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

    // Simply so we are able to see the sphere collider with the ground
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition, controller.radius-0.05f);
    }
}
